using System.Collections.Generic;
using System.ComponentModel.Composition;

using EnvDTE;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Talk2Bits.OutputColorer
{
    /// <summary>
    /// Provides classifiers for Output window.
    /// </summary>
    [Export(typeof(IClassifierProvider))]
    [ContentType("output")]
    public class OutputClassifierProvider : IClassifierProvider
    {
        private const string DebugOutputContentType = "DebugOutput";
        private const string BuildOutputContentType = "BuildOutput";        

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        [Import]
        internal SVsServiceProvider ServiceProvider = null;

        private static BuildOutputClassifier _buildOutputClassifier;
        private static OutputClassifier _debugOutputClassifier;        

        /// <summary>
        /// Gets the classifier for specified text buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>Classifier for specified text buffer.</returns>
        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            var dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            var configuration = dte.Properties["Output Colorer", "General"];
            var buildSettings = (IEnumerable<ColorerFormatSetting>)configuration.Item("BuildOutputSettings").Value;
            var debugSettings = (IEnumerable<ColorerFormatSetting>)configuration.Item("DebugOutputSettings").Value;            

            if (buffer.ContentType.IsOfType(BuildOutputContentType))
                return _buildOutputClassifier ?? (_buildOutputClassifier = new BuildOutputClassifier(ClassificationRegistry, buildSettings));

            if (buffer.ContentType.IsOfType(DebugOutputContentType))
                return _debugOutputClassifier ?? (_debugOutputClassifier = new OutputClassifier(ClassificationRegistry, debugSettings));

            return null;
        }
    }
}