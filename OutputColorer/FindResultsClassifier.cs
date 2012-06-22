using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Talk2Bits.OutputColorer
{
    public class FindResultsClassifier : IClassifier
    {
        private IClassificationTypeRegistryService _classificationTypeRegistry;
        private Lazy<DTE2> _dte = new Lazy<DTE2>(Utility.GetService<DTE, DTE2>);

        public FindResultsClassifier(IClassificationTypeRegistryService classificationTypeRegistry)
        {
            _classificationTypeRegistry = classificationTypeRegistry;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged = delegate { };

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var snapshot = span.Snapshot;
            var spans = new List<ClassificationSpan>();

            try
            {
                if (snapshot.Length == 0)
                    return spans;

                var startno = span.Start.GetContainingLine().LineNumber;
                var endno = (span.End - 1).GetContainingLine().LineNumber;

                for (var i = startno; i <= endno; i++)
                {
                    var line = snapshot.GetLineFromLineNumber(i);
                    var text = line.GetText();

                    string searchTerm = null;

                    if (_dte.Value.Find != null)
                        searchTerm = _dte.Value.Find.FindWhat;

                    if (string.IsNullOrEmpty(searchTerm))
                        return spans;

                    // Skiping part that contains file name.
                    var skipMatch = Regex.Match(text, @"^.*([^\/\\]+\.\w*).*\(.*\):");
                    var skipPart = skipMatch.Success ? skipMatch.Captures[0].Length : 0;
                    var index = text.IndexOf(searchTerm, skipPart, StringComparison.CurrentCultureIgnoreCase);

                    while (index >= 0)
                    {
                        var sx = new SnapshotSpan(snapshot, line.Extent.Start + index, searchTerm.Length);
                        var type = _classificationTypeRegistry.GetClassificationType(ClassificationDefinitions.FindResultDefinition);

                        if (type != null)
                            spans.Add(new ClassificationSpan(sx, type));

                        index += searchTerm.Length;
                        index = text.IndexOf(searchTerm, index, StringComparison.CurrentCultureIgnoreCase);
                    }                    
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