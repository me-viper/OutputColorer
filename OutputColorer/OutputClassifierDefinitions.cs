using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media;

using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

using Color = System.Windows.Media.Color;

namespace OutputColorer
{
    internal class OutputClassifierDefinitions
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

        internal class OutputColorerFormat : ClassificationFormatDefinition
        {
            protected OutputColorerFormat(
                string displayName,
                Color? defaultForegroundColor) : this(displayName, defaultForegroundColor, Colors.White)
            {
            }

            protected OutputColorerFormat(
                string displayName, 
                Color? defaultForegroundColor,
                Color? defaultBackgroundColor)
            {
                DisplayName = displayName;
                
                var formatInfo = OutputClassifierProvider.GetFontAndColor(displayName);
                ForegroundColor = formatInfo.ForegroundColor ?? defaultForegroundColor;
                BackgroundColor = formatInfo.BackGroundColor ?? defaultBackgroundColor;
                IsBold = formatInfo.IsBold;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = OutputClassifierDefinitions.Warning)]
        [Name(OutputClassifierDefinitions.Warning)]
        [UserVisible(true)]
        internal sealed class OutputWarningFormat : OutputColorerFormat
        {
            public OutputWarningFormat() : base(Warning, Color.FromRgb(128, 128, 0))
            {
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = OutputClassifierDefinitions.Error)]
        [Name(OutputClassifierDefinitions.Error)]
        [UserVisible(true)]
        internal sealed class OutputErrorFormat : OutputColorerFormat
        {
            public OutputErrorFormat() : base(Error, Colors.Red)
            {
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = OutputClassifierDefinitions.StackTrace)]
        [Name(OutputClassifierDefinitions.StackTrace)]
        [UserVisible(true)]
        internal sealed class OutputStackTraceFormat : OutputColorerFormat
        {
            public OutputStackTraceFormat() : base(StackTrace, Color.FromRgb(100, 0, 0))
            {
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [Order(Before = Priority.Default)]
        [ClassificationType(ClassificationTypeNames = OutputClassifierDefinitions.Success)]
        [Name(OutputClassifierDefinitions.Success)]
        [UserVisible(true)]
        internal sealed class OutputSuccessFormat : OutputColorerFormat
        {
            public OutputSuccessFormat() : base(Success, Colors.Green)
            {
            }
        }

        [Export(typeof(EditorFormatDefinition))]        
        [ClassificationType(ClassificationTypeNames = OutputClassifierDefinitions.Noise)]
        [Name(OutputClassifierDefinitions.Noise)]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        internal sealed class OutputNoiseFormat : OutputColorerFormat
        {
            public OutputNoiseFormat() : base(Noise, Colors.Gray)
            {
            }
        } 
    }    
}