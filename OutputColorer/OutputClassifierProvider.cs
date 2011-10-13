using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

using EnvDTE;

using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Formatting;
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

        [Import]
        internal IClassificationFormatMapService ClassificationFormatMap = null;

        [Import] 
        internal IStandardClassificationService StandardClassificationService = null;

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
            
            var formatMap = ClassificationFormatMap.GetClassificationFormatMap(buffer.ContentType.TypeName);
            //var baseClassification = StandardClassificationService.NaturalLanguage;
            var baseClassification = StandardClassificationService.Keyword;
            
            //formatMap.BeginBatchUpdate();

            //foreach (var setting in buildSettings)
            //{
            //    var type = ClassificationRegistry.CreateClassificationType(setting.ClassificationType, new[] { baseClassification });
            //    //var z = formatMap.GetTextProperties(StandardClassificationService.Keyword);

            //    //var format = formatMap.DefaultTextProperties
            //    //    .SetBold(setting.IsBold)
            //    //    .SetForeground(setting.ForegroundColor)
            //    //    .SetBackground(setting.BackgroundColor);

            //    var format = formatMap.DefaultTextProperties
            //        .SetBold(true)
            //        .SetForeground(Colors.Red)
            //        .SetBackground(Colors.Yellow);

            //    formatMap.SetExplicitTextProperties(StandardClassificationService.Keyword, format);
            //    //formatMap.SetTextProperties(StandardClassificationService.Keyword, format);
            //    break;
            //}

            //formatMap.EndBatchUpdate();            

            if (buffer.ContentType.IsOfType(BuildOutputContentType))
            {
                return buffer.Properties.GetOrCreateSingletonProperty(
                    () => new BuildOutputClassifier(ClassificationRegistry, buildSettings)
                    );
                //return _buildOutputClassifier ?? (_buildOutputClassifier = new BuildOutputClassifier(ClassificationRegistry, buildSettings));
            }

            if (buffer.ContentType.IsOfType(DebugOutputContentType))
                return _debugOutputClassifier ?? (_debugOutputClassifier = new OutputClassifier(ClassificationRegistry));

            return null;
        }
    }
}