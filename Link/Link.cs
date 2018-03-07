using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Security.Permissions;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows.Forms;


public class Link : Proc {

    /// <summary>
    /// 链接方式
    /// </summary>
    public enum Type {
        ParentOnly,
        TopDirectoryOnly,
        AllDirectories
    }

    /// <summary>
    /// 创建链接
    /// </summary>
    /// <param name="src">源路径</param>
    /// <param name="target">目标路径</param>
    /// <param name="type">链接方式</param>
    /// <returns>是否创建成功</returns>
    public bool Create ( string src, string target, Type type = Type.ParentOnly ) {

        // 源路径 是文件
        if (File.Exists (src)) {
            return LinkFile (src, target);
        }
        // 源路径 是文件夹
        else if (Directory.Exists (src)) {
            return LinkDir (src, target, type);
        }
        else
            return false;
    }

    internal bool LinkFile ( string src_file, string target_dir ) {

        // 准备文件名
        string name = Path.GetFileName (src_file);
        name = Path.Combine (target_dir, name);

        if (File.Exists (name))
            File.Delete (name);

        // 准备文件夹
        if (!Directory.Exists (target_dir))
            Directory.CreateDirectory (target_dir);



        if (Path.GetPathRoot (src_file) != Path.GetPathRoot (target_dir)) {

            if (!IsAdministrator ()) {
                if (MessageBox.Show ("文件链接在不同一分区需要管理员权限，是否重新启动", "", MessageBoxButtons.OKCancel)
                    == DialogResult.OK) {
                    RunAsAdministrator ();
                }

                return false;
            }

            return !string.IsNullOrEmpty (Cmd (string.Format ("mklink \"{0}\" \"{1}\"", name, src_file)));

        }
        else {
            return !string.IsNullOrEmpty (Cmd (string.Format ("mklink /h \"{0}\" \"{1}\"", name, src_file)));
        }
    }

    internal bool LinkDir ( string src, string target ) {
        string name = Path.GetFileName (src);
        name = Path.Combine (target, name);

        if (Directory.Exists (name))
            Directory.Delete (name);

        return !string.IsNullOrEmpty (Cmd (string.Format ("mklink /j \"{0}\" \"{1}\"", name, src)));
    }

    internal bool LinkDir ( string src, string target, Type type ) {

        // 只处理父文件夹
        if (type == Type.ParentOnly) {
            return LinkDir (src, target);
        }

        // 处理顶层目录
        else if (type == Type.TopDirectoryOnly) {
            foreach (string file in Directory.GetFiles (src)) {
                LinkFile (file, target);
            }

            foreach (string dir in Directory.GetDirectories (src)) {
                LinkDir (dir, target);
            }

            return true;
        }

        // 处理所有子文件
        else {
            string dir;
            foreach (string file in Directory.GetFiles (src, "*", SearchOption.AllDirectories)) {

                // 获取新的路径
                dir = file.Replace (src, "");
                dir = dir.Substring (1);
                dir = Path.GetDirectoryName (dir);

                LinkFile (file, Path.Combine (target, dir));

            }
        }

        return true;
    }



    private static bool IsAdministrator () {
        WindowsIdentity identity = WindowsIdentity.GetCurrent ();
        WindowsPrincipal principal = new WindowsPrincipal (identity);
        return principal.IsInRole (WindowsBuiltInRole.Administrator);
    }

    private static void RunAsAdministrator () {
        if (IsAdministrator () == false) {
            // Restart program and run as admin
            var exeName = System.Diagnostics.Process.GetCurrentProcess ().MainModule.FileName;
            ProcessStartInfo startInfo = new ProcessStartInfo (exeName);
            startInfo.Verb = "runas";
            System.Diagnostics.Process.Start (startInfo);
            //Application.Current.Shutdown ();
            return;
        }
    }
}
