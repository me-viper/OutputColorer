using System;
using System.Drawing;

namespace Talk2Bits.OutputColorer
{
    [Serializable]
    public class ColorerFormatSetting
    {
        public ColorerFormatSetting()
        {
            ForeColor = Color.Black;
            BackColor = Color.White;
        }

        public string Id { get; set; }
        public string Regex { get; set; }
        public bool IsBold { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }

        public System.Windows.Media.Color ForegroundColor
        {
            get { return System.Windows.Media.Color.FromArgb(ForeColor.A, ForeColor.R, ForeColor.G, ForeColor.B); }
        }

        public System.Windows.Media.Color BackgroundColor
        {
            get { return System.Windows.Media.Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B); }
        }
    }
}