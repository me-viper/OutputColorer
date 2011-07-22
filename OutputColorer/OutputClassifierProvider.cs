using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace OutputColorer
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("output")]
    public class OutputClassifierProvider : IClassifierProvider
    {
        private const string DebugOutputContentType = "DebugOutput";
        private const string BuildOutputContentType = "BuildOutput";        

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry;

        private static BuildOutputClassifier _buildOutputClassifier;
        private static OutputClassifier _debugOutputClassifier;

        private static Dictionary<string, FormatInfo> _fontsAndColors;

        public OutputClassifierProvider()
        {
            Debug.WriteLine("aa");
        }

        internal static FormatInfo GetFontAndColor(string classificationType)
        {
            if (_fontsAndColors == null || !_fontsAndColors.ContainsKey(classificationType))
                return new FormatInfo();
            
            return _fontsAndColors[classificationType];
        }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (buffer.ContentType.IsOfType(BuildOutputContentType))
                return _buildOutputClassifier ?? (_buildOutputClassifier = new BuildOutputClassifier(ClassificationRegistry));
                //return new BuildOutputClassifier(ClassificationRegistry);

            if (buffer.ContentType.IsOfType(DebugOutputContentType))
                return _debugOutputClassifier ?? (_debugOutputClassifier = new OutputClassifier(ClassificationRegistry));
                //return new OutputClassifier(ClassificationRegistry);

            return null;
        }
    }
}