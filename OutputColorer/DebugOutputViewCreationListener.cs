using System;
using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Talk2Bits.OutputColorer
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("DebugOutput")]
    [TextViewRole(PredefinedTextViewRoles.Interactive)]
    internal class DebugOutputViewCreationListener : BaseOutputViewCreationListener
    {
        protected override string ConfigurationName
        {
            get { return "DebugOutputSettings"; }
        }
    }
}