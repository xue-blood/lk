using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;
using Microsoft.Win32;

namespace cmds {

    public interface IOptions {
        bool Run ();
    }

    public class Options {

        [VerbOption ("select", HelpText = "选择要创建链接的路径")]
        public SelectOptions selectVerb {
            get;
            set;
        }


        [VerbOption ("create", HelpText = "创建链接")]
        public CreateOptions createVerb {
            get;
            set;
        }
    }

    public class SelectOptions : IOptions {

        [Option ('p', "path", Required = true, HelpText = "要创建链接路径")]
        public string path {
            get;
            set;
        }

        public bool Run () {
            //accessing the CurrentUser root element  
            //and adding "OurSettings" subkey to the "SOFTWARE" subkey  
            RegistryKey key = Registry.CurrentUser.CreateSubKey (@"SOFTWARE\LinkSettings");

            //storing the values  
            string org = key.GetValue ("Source") as string;
            key.SetValue ("Source", org + path + ";", RegistryValueKind.ExpandString);
            key.Close ();
            return true;
        }
    }
    public class CreateOptions : IOptions {

        [Option ('p', "path", Required = true, HelpText = "创建链接目标路径")]
        public string path {
            get;
            set;
        }

        public bool Run () {

            //accessing the CurrentUser root element  
            //and adding "OurSettings" subkey to the "SOFTWARE" subkey  
            RegistryKey key = Registry.CurrentUser.CreateSubKey (@"SOFTWARE\LinkSettings");

            //storing the values  
            string srcs = key.GetValue ("Source") as string;
            key.SetValue ("Source", "", RegistryValueKind.ExpandString);
            key.Close ();
            if (string.IsNullOrWhiteSpace (srcs))
                return false;

            Link lk = new Link ();
            foreach (string src in srcs.Split (';')) {
                lk.Create (src, path);
            }

            return true;
        }
    }
}
