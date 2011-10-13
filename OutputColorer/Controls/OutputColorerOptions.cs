using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Talk2Bits.OutputColorer.Controls
{
    public partial class OutputColorerOptions : UserControl
    {
        public OutputColorerOptions()
        {
            InitializeComponent();
        }

        public OutputColorerOptionsPage OptionsPage { get; set; }

        public IEnumerable<ColorerFormatSetting> Settings
        {
            get { return _colorerFormatSettingBindingSource.Cast<ColorerFormatSetting>(); }
            set
            {
                if (value != null)
                {
                    _colorerFormatSettingBindingSource.Clear();

                    foreach (var cfs in value)
                        _colorerFormatSettingBindingSource.Add(cfs);
                }
            }
        }
    }
}
