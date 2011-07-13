using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace OutputColorer
{
    public static class OutputClassifierDefinitions
    {
        internal const string Error = "OutputColorer.Error";
        internal const string Warning = "OutputColorer.Warning";
        internal const string Success = "OutputColorer.Success";
        internal const string Noise = "OutputColorer.Noise";
        internal const string StackTrace = "OutputColorer.Delimiter";

        [Export]
        [Name(Warning)]
        internal static ClassificationTypeDefinition WarningDefinition;

        [Export]
        [Name(Error)]
        internal static ClassificationTypeDefinition ErrorDefinition;

        [Export]
        [Name(StackTrace)]
        internal static ClassificationTypeDefinition StackTraceDefinition;

        [Export]
        [Name(Success)]
        internal static ClassificationTypeDefinition SuccessDefinition;

        [Export]
        [Name(Noise)]
        internal static ClassificationTypeDefinition NoiseDefinition;        

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = Warning)]
        [Name(Warning)]
        [UserVisible(true)]
        internal sealed class OutputWarningFormat : ClassificationFormatDefinition
        {
            public OutputWarningFormat()
            {
                ForegroundCustomizable = true;
                ForegroundColor = Color.FromRgb(128, 128, 0);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
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
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = StackTrace)]
        [Name(StackTrace)]
        [UserVisible(true)]
        internal sealed class OutputStackTraceFormat : ClassificationFormatDefinition
        {
            public OutputStackTraceFormat()
            {
                ForegroundCustomizable = true;
                ForegroundColor = Color.FromRgb(100, 0, 0);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = Success)]
        [Name(Success)]
        [UserVisible(true)]
        internal sealed class OutputSuccessFormat : ClassificationFormatDefinition
        {
            public OutputSuccessFormat()
            {
                ForegroundCustomizable = true;
                ForegroundColor = Colors.Green;
                IsBold = true;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = Noise)]
        [Name(Noise)]
        [DisplayName(Noise)]
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