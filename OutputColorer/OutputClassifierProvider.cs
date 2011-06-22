using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace OutputColorer
{
    [ContentType("output")]
    [Export(typeof(IClassifierProvider))]    
    public class OutputClassifierProvider : IClassifierProvider
    {
        private const string OutputContentType = "Output";
        private const string BuildOutputContentType = "BuildOutput";

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry;

        private static BuildOutputClassifier _buildOutputClassifier;
        private static OutputClassifier _outputClassifier;
       
        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (buffer.ContentType.IsOfType(BuildOutputContentType))
                return _buildOutputClassifier ?? (_buildOutputClassifier = new BuildOutputClassifier(ClassificationRegistry));

            if (buffer.ContentType.IsOfType(OutputContentType))
                return _outputClassifier ?? (_outputClassifier = new OutputClassifier(ClassificationRegistry));

            return null;
        }
    }
}