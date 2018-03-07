using cmds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program {
    static void Main ( string[] args ) {

        if (args.Length == 0)
            return;

        string invokedVerb;
        IOptions invokedVerbInstance = null;

        var opts = new Options ();

        if (!CommandLine.Parser.Default.ParseArguments (args, opts,
            ( verb, subOptions ) => {
                // if parsing succeeds the verb name and correct instance
                // will be passed to onVerbCommand delegate (string,object)
                invokedVerb = verb;
                invokedVerbInstance = subOptions as IOptions;
            })) {
            Environment.Exit (CommandLine.Parser.DefaultExitCodeFail);
        }

        invokedVerbInstance.Run ();
    }
}