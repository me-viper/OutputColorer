using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell;

namespace Talk2Bits.OutputColorer.Controls
{
    [Guid(GuidList.GuidOptionsPageString)]
    public class OutputColorerOptionsPage : DialogPage
    {
        private OutputColorerOptions _colorerOptions;

        public OutputColorerOptionsPage()
        {
            _colorerOptions = new OutputColorerOptions();
            _colorerOptions.Location = new Point(0, 0);
            _colorerOptions.OptionsPage = this;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override IWin32Window Window
        {
            get { return _colorerOptions; }
        }

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();
            Console.WriteLine();
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();
            Console.WriteLine();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(ColorerFormatSettingsCollectionConverter))]
        public Collection<ColorerFormatSetting> BuildOutputSettings
        {
            get { return new Collection<ColorerFormatSetting>(_colorerOptions.BuildOuptutSetting.ToList()); }
            set { _colorerOptions.BuildOuptutSetting = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(ColorerFormatSettingsCollectionConverter))]
        public Collection<ColorerFormatSetting> DebugOutputSettings
        {
            get { return new Collection<ColorerFormatSetting>(_colorerOptions.DebugOuptutSetting.ToList()); }
            set { _colorerOptions.DebugOuptutSetting = value; }
        }
    }
}