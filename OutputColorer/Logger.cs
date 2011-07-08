using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.Shell;

namespace OutputColorer
{
    internal sealed class Logger
    {
        private const string PaneName = "OutputColorer";

        private static Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger(), LazyThreadSafetyMode.ExecutionAndPublication);
        private OutputWindowPane _loggerPane;

        private Logger()
        {
            var dte = Utility.GetService<DTE, DTE2>();
            var outputPanes = dte.ToolWindows.OutputWindow.OutputWindowPanes;

            _loggerPane = (
                from outputPane in outputPanes.OfType<OutputWindowPane>()
                where string.Equals(outputPane.Name, PaneName, StringComparison.Ordinal)
                select outputPane
                ).FirstOrDefault() ?? outputPanes.Add(PaneName);
        }

        public static Logger Instance
        {
            get { return _instance.Value; }
        }

        public void Write(Exception error)
        {
            _loggerPane.OutputString(error.ToString());
        }

        [Conditional("DEBUG")]
        public void Trace(string text)
        {
            _loggerPane.OutputString(text + Environment.NewLine);
        }
    }
}