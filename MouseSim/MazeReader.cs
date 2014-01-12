using System;
using System.Collections.Generic;
using System.IO;

namespace MouseSim
{
    static class MazeReader
    {
        /* 
         * 仕様
         * 
         * 1行目: 迷路のsize
         * 2行目: スタート地点のxとy(0-based)と向きの上下左右→0123
         * 3行目: ゴールエリアのxとy(0-based)とwidthとheight
         * 4行目からsize+3行目までは、1行にsize個の数字が並んでおり、
         * 各セルの上と左に壁があるかどうかで0から3の数字を書き込む
         * 
         * 0: 上にも左にも壁がない
         * 1: 上に壁があるが左にはない
         * 2: 上には壁がないが左にはある
         * 3: 上にも左にも壁がある
         * 
         */

        public static Maze Load(string ifname)
        {
            var lines = File.ReadAllLines(ifname);

            char[] separator = { ' ' };
            string[] seq;

            int linePtr = 0;

            int size;
            if (int.TryParse(lines[linePtr++], out size) == false)
            {
                throw new IOException("迷路ファイルの1行目が不正です。");
            }

            if (lines.Length != size + 3)
            {
                throw new IOException("迷路ファイルの行数がおかしいです");
            }

            int startX, startY, dir;
            seq = lines[linePtr++].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (int.TryParse(seq[0], out startX) == false || int.TryParse(seq[1], out startY) == false || int.TryParse(seq[2], out dir) == false)
            {
                throw new IOException("迷路ファイルの2行目が不正です。");
            }

            int goalX, goalY, goalW, goalH;
            seq = lines[linePtr++].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (int.TryParse(seq[0], out goalX) == false || int.TryParse(seq[1], out goalY) == false || int.TryParse(seq[2], out goalW) == false || int.TryParse(seq[3], out goalH) == false)
            {
                throw new IOException("迷路ファイルの3行目が不正です。");
            }

            var maze = new Maze(size, startX, startY, (Direction)dir, goalX, goalY, goalW, goalH);
            for (int y = 0; y < size; y++)
            {
                string line = lines[linePtr++];
                if (line.Length != size)
                {
                    throw new IOException(string.Format("迷路ファイルの{0}行目が不正です。", 1 + 3 + y));
                }

                for (int x = 0; x < size; x++)
                {
                    int value;
                    if (int.TryParse(line.Substring(x, 1), out value) && 0 <= value && value <= 3)
                    {
                        if (value == 1 || value == 3)
                        {
                            maze.AddWall(x, y, Direction.Top);
                        }

                        if (value == 2 || value == 3)
                        {
                            maze.AddWall(x, y, Direction.Left);
                        }
                    }
                    else
                    {
                        string msg = string.Format("迷路ファイルの内容が不正です。({0}行目の{1}文字目がおかしいです", 1 + 3 + y, x + 1);
                        throw new IOException(msg);
                    }
                }
            }

            return maze;
        }
    }
}
