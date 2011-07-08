using System;
using System.ComponentModel.Composition;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace OutputColorer
{
    public class OutputClassifierDefinitions
    {
        internal const string Error = "OutputColorer.Error";
        internal const string Warning = "OutputColorer.Warning";
        internal const string Success = "OutputColorer.Success";
        internal const string Noise = "OutputColorer.Noise";
        internal const string StackTrace = "OutputColorer.Delimiter";

        [Export]
        [Name("output")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition OutputContentTypeDefinition;

        [Export]
        [Name(Warning)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition WarningDefinition;

        [Export]
        [Name(Error)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition ErrorDefinition;

        [Export]
        [Name(StackTrace)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition StackTraceDefinition;

        [Export]
        [Name(Success)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition SuccessDefinition;

        [Export]
        [Name(Noise)]
        [BaseDefinition("output")]
        internal static ClassificationTypeDefinition NoiseDefinition;

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = Warning)]
        [Name(Warning)]
        [UserVisible(true)]
        internal sealed class OutputWarningFormat : ClassificationFormatDefinition
        {
            public OutputWarningFormat()
            {
                ForegroundColor = Color.FromRgb(128, 128, 0);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = Error)]
        [Name(Error)]
        [UserVisible(true)]
        internal sealed class OutputErrorFormat : ClassificationFormatDefinition
        {
            public OutputErrorFormat()
            {
                ForegroundColor = Colors.Red;
                IsBold = true;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = StackTrace)]
        [Name(StackTrace)]
        [UserVisible(true)]
        internal sealed class OutputStackTraceFormat : ClassificationFormatDefinition
        {
            public OutputStackTraceFormat()
            {
                ForegroundColor = Color.FromRgb(100, 0, 0);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = Success)]
        [Name(Success)]
        [UserVisible(true)]
        internal sealed class OutputSuccessFormat : ClassificationFormatDefinition
        {
            public OutputSuccessFormat()
            {
                ForegroundColor = Colors.Green;
                IsBold = true;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = Noise)]
        [Name(Noise)]
        [UserVisible(true)]
        internal sealed class OutputNoiseFormat : ClassificationFormatDefinition
        {
            public OutputNoiseFormat()
            {
                ForegroundColor = Colors.Gray;
            }
        } 
    }
}