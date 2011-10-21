using System.ComponentModel.Composition;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Talk2Bits.OutputColorer
{
    internal sealed class ClassificationDefinitions
    {
        public const string FindResultDefinition = "OutputColorer.FindResultHightlight";

        [Export]
        [Name(FindResultDefinition)]
        internal static ClassificationTypeDefinition FindResultHightlightDefinition;

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = FindResultDefinition)]
        [Name(FindResultDefinition)]
        [UserVisible(true)]
        internal sealed class FindResultHightlightFormat : ClassificationFormatDefinition
        {
            public FindResultHightlightFormat()
            {
                ForegroundColor = Colors.Blue;
                IsBold = true;
            }
        }
    }
}