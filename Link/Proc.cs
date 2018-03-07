using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


/// <summary>
/// 执行操作系统命令对象
/// </summary>
public class Proc {

    /// <summary>
    /// 进程对象
    /// </summary>
    protected Process proc;

    /// <summary>
    /// 运行出错操作
    /// </summary>
    /// <param name="msg"></param>
    public event ProcRunHandler onError;
    public delegate void ProcRunHandler ( Exception exception );

    public Proc () {

        proc = new System.Diagnostics.Process ();
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
    }

    /// <summary>
    /// 执行系统命令
    /// </summary>
    /// <param name="name">程序名</param>
    /// <param name="param">参数</param>
    /// <param name="back">是否是后台进程</param>
    /// <returns>
    /// 如果是后台进程，返回运行结果
    /// 如果是前台进程，返回空字符串
    /// 如果要执行的命令不存在，返回 null
    /// </returns>
    public string Run ( string name, string param = "", bool back = true ) {

        // 检查 参数
        if (string.IsNullOrEmpty (name))
            return null;
#if DEBUG
        "{0} {1}".format (name, param).log ();
#endif

        proc.StartInfo.FileName = name;
        proc.StartInfo.Arguments = param.Trim ();
        proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory ();

        if (back) {
            // 创建一个无窗口进程
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;
        }
        else {
            // 有窗口
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.RedirectStandardOutput = false;
        }

        try {
            // 执行命令
            proc.Start ();

        }
        catch (Exception e) {

            if (onError != null)
                onError (e);

            // 出現异常，返回 null
            return null;
        }

        if (back) {
            // 等待命令结束
            proc.WaitForExit ();


            // 获取结果
            string str = proc.StandardOutput.ReadToEnd ();

            if (str != null)
                str = str.Trim ();

            return str;
        }
        else
            return "";

    }

    /// <summary>
    /// 使用命令行执行命令
    /// </summary>
    public string Cmd ( string cmd ) {

        return Run ("cmd", "/c " + cmd, true);
    }
}
