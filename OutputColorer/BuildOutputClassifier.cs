using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace OutputColorer
{
    internal sealed class BuildOutputClassifier : IClassifier
    {
        private IClassificationTypeRegistryService _classificationTypeRegistry;

        internal BuildOutputClassifier(IClassificationTypeRegistryService registry)
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
        /// Classify the given spans, which, for diff files, classifies
        /// a line at a time.
        /// </summary>
        /// <param name="span">The span of interest in this projection buffer.</param>
        /// <returns>The list of <see cref="ClassificationSpan"/> as contributed by the source buffers.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;

            var spans = new List<ClassificationSpan>();

            try
            {
                if(snapshot.Length == 0)
                    return spans;
            
                var startno = span.Start.GetContainingLine().LineNumber;
                var endno = (span.End - 1).GetContainingLine().LineNumber;

                for (var i = startno; i <= endno; i++)
                {
                    ITextSnapshotLine line = snapshot.GetLineFromLineNumber(i);
                    IClassificationType type = null;
                    string text = line.GetText();

                    if (text.Contains("- Failed  -"))
                        type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Error);
                    else if (text.Contains("- Success -"))
                        type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Success);
                    else if (Regex.IsMatch(text, @"^.+: error \w+:.+$"))
                        type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Error);                    
                    else if (Regex.IsMatch(text, @"^.+: warning \w+:.+$"))
                        type = _classificationTypeRegistry.GetClassificationType(OutputClassifierDefinitions.Warning);
                    else if (Regex.IsMatch(text, @"^={10}\sBuild:.*$"))
                    {
                        if (text.Contains(", 0 failed,"))
                        {
                            type = _classificationTypeRegistry.GetClassificationType(
                                OutputClassifierDefinitions.Success
                                );
                        }
                        else
                        {
                            type = _classificationTypeRegistry.GetClassificationType(
                                OutputClassifierDefinitions.Error
                                );
                        }

                    }

                    if (type != null)
                        spans.Add(new ClassificationSpan(line.Extent, type));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(ex);
            }

            return spans;
        }
    }
}