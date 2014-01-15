using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MouseSim
{
    /*
     * 仕様
     * 
     * Agentとの通信は標準入出力を用いて行う
     * 
     * Simulator -> Agent
     * 
     * 1行目: size
     * 2行目: start_x start_y start_dir
     * 3行目: goal_x goal_y goal_w goal_h
     * 
     * そのあとは壁情報クエリが移動情報と引き換えに対話的に送られる
     * 前 左 下 右 (Agentから見て) で壁があったら1、なかったら0
     * 
     * Agent -> Simulator
     * 
     * "コマンド(1文字) 引数1 引数2 ..." という情報を壁情報と引き換えに対話的に送る
     * 
     * F x -> xマス前進する
     * L   -> 左に回転する
     * R   -> 右に回転する
     * X   -> 終了する
     * S   -> リセット
     * #   -> コメント
     */
    public class AgentCommunicator : IDisposable
    {
        Process p;

        public AgentCommunicator(string workdir, string launch_cmd)
        {
            p = new Process();

            p.StartInfo.WorkingDirectory = workdir;
            p.StartInfo.FileName = launch_cmd;

            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            // exeを実行するときは書かなくてもいいらしい
            // ref: http://www.atmarkit.co.jp/fdotnet/dotnettips/654nowinexec/nowinexec.html
            // ただし、ウィンドウを開かないとゾンビ化する可能性が高まるので、表示させることにする
            p.StartInfo.CreateNoWindow = false;

            // exeを実行するときにはfalseにしないと "結果的に" cmd.exeが開いてしまう
            // ref: http://www.atmarkit.co.jp/fdotnet/dotnettips/654nowinexec/nowinexec.html
            p.StartInfo.UseShellExecute = false;
        }

        // デストラクタ: Dipose()を呼んだときに呼び出されるやつ
        // ファイナライザ: ガベコレのときに呼び出されるやつ

        // アンマネージドソースをデストラクタで解放 -> OK
        // マネージドソースをデストラクタで解放 -> OK
        // アンマネージドソースをファイナライザで解放 -> OK
        // マネージドソースをファイナライザで解放 -> NG (既に解放されていることがある)
        public void Dispose()
        {
            if (p != null)
            {
                p.Dispose();
            }

            // 書かないとパフォーマンスが下がるそうなので念のため
            // ぶっちゃけ不要な気もしなくもない
            // ref: http://msdn.microsoft.com/ja-jp/library/fs2xkftw(v=VS.80).aspx
            GC.SuppressFinalize(this);
        }

        public void Launch()
        {
            if (p == null) return;

            p.Start();
        }

        public void Kill()
        {
            if (p == null) return;

            if (p.HasExited == false)
            {
                p.Kill();
                p.WaitForExit();
            }

            p = null;
        }

        public void Transmit(string msg)
        {
            if (p == null) return;

            p.StandardInput.WriteLine(msg);
        }

        public Task<string> RecieveAsync()
        {
            return p.StandardOutput.ReadLineAsync();
        }
    }
}
