using System.IO;

namespace MouseSim
{
    static class MouseMazeReader
    {
        /* 
         * 仕様
         * 
         * 1行目には迷路のsizeが書かれている
         * 2行目からsize+1行目までは、size個の数字が並んでおり、
         * 各セルの上と左に壁があるかどうかで0から3の数字を書き込む
         * 
         * 0: 上にも左にも壁がない
         * 1: 上に壁があるが左にはない
         * 2: 上には壁がないが左にはある
         * 3: 上にも左にも壁がある
         * 
         */

        public static MouseMaze Load(string ifname)
        {
            MouseMaze maze;
            using (var reader = new StreamReader(ifname))
            {
                try
                {
                    int size;
                    {
                        string line;
                        try
                        {
                            line = reader.ReadLine();
                        }
                        catch (IOException ex)
                        {
                            throw new IOException("1行目の読み込み中にエラーが発生しました", ex);
                        }

                        if (int.TryParse(line, out size) == false)
                        {
                            throw new IOException("迷路ファイルの1行目が不正です。");
                        }
                    }
                    

                    maze = new MouseMaze(size);

                    for (int i = 0; i < size; i++)
                    {
                        string line;
                        try
                        {
                            line = reader.ReadLine();
                        }
                        catch (IOException ex)
                        {
                            throw new IOException((i + 1).ToString() + "行目の読み込み中にエラーが発生しました", ex);
                        }
                        

                        if (line.Length != size)
                        {
                            throw new IOException("迷路ファイルの内容が不正です。(1行目に書かれたsizeと内容が一致しません)");
                        }

                        for (int j = 0; j < size; j++)
                        {
                            int value;
                            if (int.TryParse(line.Substring(j, 1), out value) && 0 <= value && value <= 3)
                            {
                                if (value == 1 || value == 3)
                                {
                                    maze.AddWall(j, i, Direction.Top);
                                }

                                if (value == 2 || value == 3)
                                {
                                    maze.AddWall(j, i, Direction.Left);
                                }
                            }
                            else
                            {
                                string msg = string.Format("迷路ファイルの内容が不正です。({0}行目の{1}文字目がおかしいです", i + 1, j + 1);
                                throw new IOException(msg);
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    throw new IOException("迷路ファイルの読み込み中にエラーが発生しました", ex);
                }
            }

            return maze;
        }
    }
}
