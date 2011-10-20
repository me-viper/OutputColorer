using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Talk2Bits.OutputColorer
{
    [DataContract]    
    public class ColorerFormatSetting
    {
        public ColorerFormatSetting()
        {
            ForeColor = Color.Black;
            BackColor = Color.White;
        }

        [DataMember]
        public string ClassificationType { get; set; }
        
        [DataMember]
        public string Regex { get; set; }
        
        [DataMember]
        public bool IsBold { get; set; }
        
        [DataMember]
        public Color ForeColor { get; set; }
        
        [DataMember]
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