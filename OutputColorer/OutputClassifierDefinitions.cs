using System;
using System.ComponentModel.Composition;
using System.Windows.Media;

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
        internal const string Delimiter = "OutputColorer.Delimiter";

        [Export]
        [Name(Warning)]
        internal static ClassificationTypeDefinition WarningDefinition = null;

        [Export]
        [Name(Error)]
        internal static ClassificationTypeDefinition ErrorDefinition = null;

        [Export]
        [Name(Delimiter)]
        internal static ClassificationTypeDefinition DelimiterDefinition = null;

        [Export]
        [Name(Success)]
        internal static ClassificationTypeDefinition SuccessDefinition = null;

        [Export]
        [Name(Noise)]
        internal static ClassificationTypeDefinition NoiseDefinition = null;

        internal class OutputColorerFormat : ClassificationFormatDefinition
        {
            private static IOutputColorerConfigurationService _colorerConfiguration =
                new OutputColorerConfigurationService();

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

                var formatInfo = _colorerConfiguration.GetFontAndColor(displayName);
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
        [ClassificationType(ClassificationTypeNames = OutputClassifierDefinitions.Delimiter)]
        [Name(OutputClassifierDefinitions.Delimiter)]
        [UserVisible(true)]
        internal sealed class OutputStackTraceFormat : OutputColorerFormat
        {
            public OutputStackTraceFormat() : base(Delimiter, Color.FromRgb(100, 0, 0))
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