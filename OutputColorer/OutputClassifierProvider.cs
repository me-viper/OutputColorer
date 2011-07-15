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

namespace OutputColorer
{
    public class TextManagerEvents : IVsTextManagerEvents
    {
        public void OnRegisterMarkerType(int iMarkerType)
        {
            
        }

        public void OnRegisterView(IVsTextView pView)
        {
            
        }

        public void OnUnregisterView(IVsTextView pView)
        {
            
        }

        public void OnUserPreferencesChanged(
            VIEWPREFERENCES[] pViewPrefs, 
            FRAMEPREFERENCES[] pFramePrefs, 
            LANGPREFERENCES[] pLangPrefs, 
            FONTCOLORPREFERENCES[] pColorPrefs)
        {
            
            Debug.WriteLine("OnUserPreferencesChanged");
        }
    }
    
    [Export(typeof(IClassifierProvider))]
    [ContentType("output")]
    public class OutputClassifierProvider : IClassifierProvider
    {
        private const string DebugOutputContentType = "DebugOutput";
        private const string BuildOutputContentType = "BuildOutput";        

        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry;

        //[Import]
        //internal IEditorFormatMapService FormatMapService;

        private static BuildOutputClassifier _buildOutputClassifier;
        private static OutputClassifier _debugOutputClassifier;

        private static Dictionary<string, FormatInfo> _fontsAndColors;
        
        public OutputClassifierProvider()
        {
            _fontsAndColors = Utility.GetColorAndFontSettings();
            //var textManager = ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager)) as IVsTextManager2;

            //IConnectionPointContainer container = textManager as IConnectionPointContainer;
            //IConnectionPoint textManagerEventsConnection = null;
            //Guid eventGuid = typeof(IVsTextManagerEvents).GUID;
            //container.FindConnectionPoint(ref eventGuid, out textManagerEventsConnection);
            //var textManagerEvents = new TextManagerEvents();
            //uint textManagerCookie;
            //textManagerEventsConnection.Advise(textManagerEvents, out textManagerCookie);            
        }

        internal static FormatInfo GetFontAndColor(string classificationType)
        {
            if (_fontsAndColors == null || !_fontsAndColors.ContainsKey(classificationType))
                return new FormatInfo();

            return _fontsAndColors[classificationType];
        }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (_fontsAndColors == null)
                _fontsAndColors = Utility.GetColorAndFontSettings();

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