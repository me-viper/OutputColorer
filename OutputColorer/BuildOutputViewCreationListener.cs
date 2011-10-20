using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Talk2Bits.OutputColorer
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("BuildOutput")]
    [TextViewRole(PredefinedTextViewRoles.Interactive)]
    internal class BuildOutputViewCreationListener : BaseOutputViewCreationListener
    {
        protected override string ConfigurationName
        {
            get { return "BuildOutputSettings"; }
        }
    }
}
