using System.Collections.Generic;
using cmd.Commands;
using cmd.Runner.Arguments;

namespace cmd.Runner.Shells
{
    internal class Posh : IRunner
    {
        public string Run(IRunOptions runOptions)
        {
            return new ProcessRunner().Run(runOptions);
        }

        public string BuildArgument(Argument argument)
        {
            throw new System.NotImplementedException();
        }

        public IDictionary<string, string> EnvironmentVariables { get; set; }

        public ICommando GetCommand()
        {
            throw new System.NotImplementedException();
        }

        public IArgumentBuilder ArgumentBuilder { get; protected set; }
    }
}