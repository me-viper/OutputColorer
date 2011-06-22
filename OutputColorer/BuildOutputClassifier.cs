using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace OutputColorer
{
    public class BuildOutputClassifier : IClassifier
    {
        private IClassificationTypeRegistryService _classificationTypeRegistry;

        internal BuildOutputClassifier(IClassificationTypeRegistryService registry)
        {
            _classificationTypeRegistry = registry;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        /// <summary>
        /// Classify the given spans, which, for diff files, classifies
        /// a line at a time.
        /// </summary>
        /// <param name="span">The span of interest in this projection buffer.</param>
        /// <returns>The list of <see cref="ClassificationSpan"/> as contributed by the source buffers.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;

            var spans = new List<ClassificationSpan>();

            if(snapshot.Length == 0)
                return spans;

            var startno = span.Start.GetContainingLine().LineNumber;
            var endno = (span.End - 1).GetContainingLine().LineNumber;

            for (var i = startno; i <= endno; i++)
            {
                ITextSnapshotLine line = snapshot.GetLineFromLineNumber(i);
                IClassificationType type = null;
                string text = line.GetText();

                if (text.Contains("Failed"))
                    type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Error);
                else if (text.Contains("warning"))
                    type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Warning);
                else
                    type = _classificationTypeRegistry.GetClassificationType("text");
                
                if (type != null)
                    spans.Add(new ClassificationSpan(line.Extent, type));
            }

            return spans;
        }
    }
}