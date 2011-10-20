using System.Collections.Generic;
using System.ComponentModel.Composition;

using EnvDTE;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;

namespace Talk2Bits.OutputColorer
{
    internal abstract class BaseOutputViewCreationListener : IWpfTextViewCreationListener
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IClassificationFormatMapService ClassificaitonFormatMap = null;

        [Import]
        internal SVsServiceProvider ServiceProvider = null;

        protected abstract string ConfigurationName { get; }

        public void TextViewCreated(IWpfTextView textView)
        {
            var dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            var configuration = dte.Properties["Output Colorer", "General"];
            var debugSettings = (IEnumerable<ColorerFormatSetting>)configuration.Item(ConfigurationName).Value;

            var formatMap = ClassificaitonFormatMap.GetClassificationFormatMap(textView);

            foreach (var setting in debugSettings)
            {
                var type = ClassificationTypeRegistry.CreateClassificationType(setting.ClassificationType, new IClassificationType[] { });
                var format = formatMap.DefaultTextProperties
                    .SetBackground(setting.BackgroundColor)
                    .SetForeground(setting.ForegroundColor)
                    .SetBold(setting.IsBold);

                formatMap.SetTextProperties(type, format);
            }
        }
    }
}