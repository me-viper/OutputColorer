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
    [ContentType("FindResults")]
    public class OutputClassifierProvider : IClassifierProvider
    {
        private const string DebugOutputContentType = "DebugOutput";
        private const string BuildOutputContentType = "BuildOutput";
        private const string FindResultsContentType = "FindResults";

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        [Import]
        internal SVsServiceProvider ServiceProvider = null;

        private static OutputClassifier _buildOutputClassifier;
        private static OutputClassifier _debugOutputClassifier;        
        private static IClassifier _findResultsClassifier;        

        /// <summary>
        /// Gets the classifier for specified text buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>Classifier for specified text buffer.</returns>
        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            var dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            var configuration = dte.Properties["Output Colorer", "General"];

            if (buffer.ContentType.IsOfType(BuildOutputContentType))
            {
                if (_buildOutputClassifier == null)
                {
                    var settings = (IEnumerable<ColorerFormatSetting>)configuration.Item("BuildOutputSettings").Value;
                    _buildOutputClassifier = new OutputClassifier(ClassificationRegistry, settings);
                }

                return _buildOutputClassifier;
            }

            if (buffer.ContentType.IsOfType(DebugOutputContentType))
            {
                if (_debugOutputClassifier == null)
                {
                    var settings = (IEnumerable<ColorerFormatSetting>)configuration.Item("DebugOutputSettings").Value;
                    _debugOutputClassifier = new OutputClassifier(ClassificationRegistry, settings);
                }
            
                return _debugOutputClassifier;
            }

            if (buffer.ContentType.IsOfType(FindResultsContentType))
            {
                return _findResultsClassifier ?? (_findResultsClassifier = new FindResultsClassifier(ClassificationRegistry));
            }

            return null;
        }
    }
}