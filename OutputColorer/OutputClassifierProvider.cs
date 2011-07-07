using System;
using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace OutputColorer
{
    [ContentType("output")]
    [Export(typeof(IClassifierProvider))]    
    public class OutputClassifierProvider : IClassifierProvider
    {
        private const string DebugOutputContentType = "DebugOutput";
        private const string BuildOutputContentType = "BuildOutput";

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry;

        private static BuildOutputClassifier _buildOutputClassifier;
        private static OutputClassifier _debugOutputClassifier;
       
        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (buffer.ContentType.IsOfType(BuildOutputContentType))
                return _buildOutputClassifier ?? (_buildOutputClassifier = new BuildOutputClassifier(ClassificationRegistry));

            if (buffer.ContentType.IsOfType(DebugOutputContentType))
                return _debugOutputClassifier ?? (_debugOutputClassifier = new OutputClassifier(ClassificationRegistry));

            return null;
        }
    }
}