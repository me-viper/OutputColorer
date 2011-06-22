using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace OutputColorer
{
    public class OutputClassifier : IClassifier
    {
        private IClassificationTypeRegistryService _classificationTypeRegistry;

        internal OutputClassifier(IClassificationTypeRegistryService registry)
        {
            _classificationTypeRegistry = registry;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        /// <summary>
        /// Classify debug output spans.
        /// </summary>
        /// <param name="span">The span of interest in this projection buffer.</param>
        /// <returns>The list of <see cref="ClassificationSpan"/> as contributed by the source buffers.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;

            var spans = new List<ClassificationSpan>();

            if (snapshot.Length == 0)
                return spans;
            
            IClassificationType type = null;
            string text = span.GetText().TrimStart();

            if (text.StartsWith("at") || 
                text.StartsWith("A first chance exception of type") ||
                text.Contains("--- End of inner exception stack trace ---") || 
                text.Contains("Exception:"))
            {
                type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Error);
            }

            if (type != null)
                spans.Add(new ClassificationSpan(span, type));
            
            return spans;
        }

    }
}