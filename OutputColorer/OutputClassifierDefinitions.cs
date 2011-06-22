using System;
using System.ComponentModel.Composition;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace OutputColorer
{
    public class OutputClassifierDefinitions
    {
        internal const string BuildFailed = "output.build.failed";
        internal const string BuildWarning = "output.build.warning";

        [Export]
        [Name("output")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition OutputContentTypeDefinition;

        [Export]
        [Name(BuildWarning)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition BuildWarningDefinition;

        [Export]
        [Name(BuildFailed)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition BuildFailedDefinition;        

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = BuildWarning)]
        [Name(BuildWarning)]
        internal sealed class BuildWarningFormat : ClassificationFormatDefinition
        {
            public BuildWarningFormat()
            {
                ForegroundColor = Color.FromRgb(0x44, 0xBB, 0xBB);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = BuildFailed)]
        [Name(BuildFailed)]
        internal sealed class BuildFailedFormat : ClassificationFormatDefinition
        {
            public BuildFailedFormat()
            {
                ForegroundColor = Colors.Red;
                IsBold = true;
            }
        }        
    }
}