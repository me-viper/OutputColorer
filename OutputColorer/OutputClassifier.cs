using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace OutputColorer
{
    internal sealed class OutputClassifier : IClassifier
    {
        private IClassificationTypeRegistryService _classificationTypeRegistry;

        internal OutputClassifier(IClassificationTypeRegistryService registry)
        {
            _classificationTypeRegistry = registry;
        }

        /// <summary>
        /// Ocurs when the classification of a span of text has changed.
        /// </summary>
        /// <remarks>
        /// This event does not need to be raised for newly-inserted text.
        /// However, it should be raised if any text other than that which was actually inserted has been reclassified.
        /// It should also be raised if the deletion of text causes the remaining
        /// text to be reclassified.</remarks>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged = delegate { };

        /// <summary>
        /// Classify debug output spans.
        /// </summary>
        /// <param name="span">The span of interest in this projection buffer.</param>
        /// <returns>The list of <see cref="ClassificationSpan"/> as contributed by the source buffers.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;

            var spans = new List<ClassificationSpan>();

            try
            {
                if (snapshot.Length == 0)
                    return spans;

                IClassificationType type = null;
                string text = span.GetText().TrimStart();

                if (text.Contains("--- End of inner exception stack trace ---"))
                {
                    type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Delimiter);
                }
                else if (text.StartsWith("System.Windows.Data Error:"))
                {
                    type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Error);
                }
                else if (text.StartsWith("at") || 
                            text.StartsWith("A first chance exception of type") || 
                                text.Contains("Exception:"))
                {
                    type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Error);
                }
                else if (Regex.IsMatch(text, "^The (?:thread|program) .+ has exited with code .+$"))
                {
                    type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Noise);
                }
                else if (Regex.IsMatch(text, @"^\'.+\'\s+.+: (?:Loaded|Cannot find or open the PDB file).*$"))
                {
                    type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Noise);
                }                

                if (type != null)
                    spans.Add(new ClassificationSpan(span, type));
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex);
            }
            
            return spans;
        }

    }
}