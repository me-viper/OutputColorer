using System;
using System.ComponentModel.Composition;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace OutputColorer
{
    public class OutputClassifierDefinitions
    {
        internal const string Error = "output.error";
        internal const string Warning = "output.warning";

        [Export]
        [Name("output")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition OutputContentTypeDefinition;

        [Export]
        [Name(Warning)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition BuildWarningDefinition;

        [Export]
        [Name(Error)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition BuildFailedDefinition;

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = Warning)]
        [Name(Warning)]
        internal sealed class OutputWarningFormat : ClassificationFormatDefinition
        {
            public OutputWarningFormat()
            {
                ForegroundColor = Color.FromRgb(0x44, 0xBB, 0xBB);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = Error)]
        [Name(Error)]
        internal sealed class OutputErrorFormat : ClassificationFormatDefinition
        {
            public OutputErrorFormat()
            {
                ForegroundColor = Colors.Red;
                IsBold = true;
            }
        }        
    }
}