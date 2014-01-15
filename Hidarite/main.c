#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <assert.h>

#ifdef _MSC_VER
#pragma warning(disable:4996)
#endif

void ReadHeader(
	int *size,
	int *start_x, int *start_y, int *start_dir,
	int *goal_x, int *goal_y, int *goal_w, int *goal_h);

// �O �� �� �E (�}�E�X���猩��)
void ReadWallInfo(int buf[4]);

void RotateRight(int *dir);
void RotateLeft(int *dir);
bool IsGoal(
	const int pos_x, const int pos_y,
	const int goal_x, const int goal_y, const int goal_w, const int goal_h);
void GoForward(int *pos_x, int *pos_y, const int pos_dir);

void SubmitRestart(void);
void SubmitStop(void);
void SubmitTurnLeft(void);
void SubmitGoForward(const int num_blocks);
void SubmitTurnRight(void);

int main(int argc, const char *argv[]) {
	int size;
	int start_x, start_y, start_dir;
	int goal_x, goal_y, goal_w, goal_h;

	int pos_x, pos_y, pos_dir;

	puts("# Hello, MouseSim!");
	fflush(stdout);

	ReadHeader(
		&size,
		&start_x, &start_y, &start_dir,
		&goal_x, &goal_y, &goal_w, &goal_h);

	pos_x = start_x;
	pos_y = start_y;
	pos_dir = start_dir;

	while (true) {
		int wall_info[4];
		int wall_info2[4];

		fprintf(stderr, "x = %d, y = %d\n", pos_x, pos_y);

		if (IsGoal(pos_x, pos_y, goal_x, goal_y, goal_w, goal_h))
		{
			SubmitStop();
			break;
		}

		ReadWallInfo(wall_info);
		// ���ɕǂ��Ȃ��Ƃ��́A����������1�}�X�O�i
		if (wall_info[1] == 0) {
			RotateLeft(&pos_dir);
			SubmitTurnLeft();

			ReadWallInfo(wall_info2);

			GoForward(&pos_x, &pos_y, pos_dir);
			SubmitGoForward(1);
		}
		// �O�ɕǂ��Ȃ��Ƃ��́A�����͂��̂܂܂�1�}�X�O�i
		else if (wall_info[0] == 0) {
			GoForward(&pos_x, &pos_y, pos_dir);
			SubmitGoForward(1);
		}
		// ���ɂ��O�ɂ��ǂ�����Ƃ��͉E������
		else {
			RotateRight(&pos_dir);
			SubmitTurnRight();
		}
	}

	return EXIT_SUCCESS;
}

void ReadHeader(
	int *size,
	int *start_x, int *start_y, int *start_dir,
	int *goal_x, int *goal_y, int *goal_w, int *goal_h) {
	scanf("%d", size);
	scanf("%d %d %d", start_x, start_y, start_dir);
	scanf("%d %d %d %d", goal_x, goal_y, goal_w, goal_h);
}

void RotateRight(int *dir) {
	*dir = (*dir + 4 - 1) % 4;
}

void RotateLeft(int *dir) {
	*dir = (*dir + 1) % 4;
}

bool IsGoal(
	const int pos_x, const int pos_y,
	const int goal_x, const int goal_y, const int goal_w, const int goal_h) {
	return goal_x <= pos_x && pos_x < goal_x + goal_w && goal_y <= pos_y && pos_y < goal_y + goal_h;
}

void GoForward(int *pos_x, int *pos_y, const int pos_dir) {
	static const int dx[4] = { 0, -1, 0, 1 };
	static const int dy[4] = { -1, 0, 1, 0 };
	assert(0 <= pos_dir && pos_dir < 4);
	*pos_x += dx[pos_dir];
	*pos_y += dy[pos_dir];
}

void SubmitRestart(void) {
	puts("S");
	fflush(stdout);
}

void SubmitStop(void) {
	puts("X");
	fflush(stdout);
}

void SubmitTurnLeft(void) {
	puts("L");
	fflush(stdout);
}

void SubmitGoForward(const int num_blocks) {
	printf("F %d\n", num_blocks);
	fflush(stdout);
}

void SubmitTurnRight(void) {
	puts("R");
	fflush(stdout);
}

void ReadWallInfo(int buf[4]) {
	int i;
	for (i = 0; i < 4; i++) {
		scanf("%d", &buf[i]);
	}
}
