using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 字符串扩展
/// </summary>
public static class StringExtension {

    /// <summary>
    /// 按错误打印
    /// </summary>
    /// <param name="str"></param>
    public static void loge ( this string str ) {
        Console.WriteLine (str);
    }


    /// <summary>
    /// 按警告打印
    /// </summary>
    /// <param name="str"></param>
    public static void logw ( this string str ) {
        Console.WriteLine (str);
    }

    /// <summary>
    /// 普通打印
    /// </summary>
    /// <param name="str"></param>
    public static void log ( this string str ) {
        Console.WriteLine (str);
    }

    /// <summary>
    /// 格式化字符串
    /// </summary>
    /// <param name="str">格式化控制字符串</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static string format ( this string str, params object[] param ) {
        return string.Format (str, param);
    }


    /// <summary>
    /// 获取数据中的换行
    /// </summary>
    /// <param name="lines"></param>
    /// <returns></returns>
    public static string lineEnd ( this string lines ) {
        if (lines.IndexOf ("\r\n") != -1)
            return "\r\n";

        if (lines.IndexOf ("\n") != -1)
            return "\n";

        if (lines.IndexOf ("\r") != -1)
            return "\r";

        return null;

    }

}

public static class Extension {

    /// <summary>
    /// Waits asynchronously for the process to exit.
    /// </summary>
    /// <param name="process">The process to wait for cancellation.</param>
    /// <param name="cancellationToken">A cancellation token. If invoked, the task will return 
    /// immediately as canceled.</param>
    /// <returns>A Task representing waiting for the process to end.</returns>
    public static Task WaitForExitAsync ( this Process process,
        CancellationToken cancellationToken = default(CancellationToken) ) {
        var tcs = new TaskCompletionSource<object> ();
        process.EnableRaisingEvents = true;
        process.Exited += ( sender, args ) => tcs.TrySetResult (null);
        if (cancellationToken != default (CancellationToken))
            cancellationToken.Register (tcs.SetCanceled);

        return tcs.Task;
    }
}