using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

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

            _colorerOptions.BuildOuptutSetting = GetBuildOutputDefaultConfiguration();
            _colorerOptions.DebugOuptutSetting = GetDebugOutputDefaultConfiguration();
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override IWin32Window Window
        {
            get { return _colorerOptions; }
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            _colorerOptions.Apply(e.ApplyBehavior);
            base.OnApply(e);
        }

        public override void ResetSettings()
        {
            BuildOutputSettings = GetBuildOutputDefaultConfiguration();
            DebugOutputSettings = GetDebugOutputDefaultConfiguration();
        }

        public override void SaveSettingsToXml(IVsSettingsWriter writer)
        {
            var typeConverter = new ColorerFormatSettingsCollectionConverter();

            var buildSettings = typeConverter.ConvertTo(BuildOutputSettings, typeof(string)) as string;

            if (buildSettings != null)
                writer.WriteSettingString("BuildOutputSettings", buildSettings);

            var debugSettings = typeConverter.ConvertTo(DebugOutputSettings, typeof(string)) as string;

            if (debugSettings != null)
                writer.WriteSettingString("DebugOutputSettings", debugSettings);
        }

        public override void LoadSettingsFromXml(IVsSettingsReader reader)
        {
            var typeConverter = new ColorerFormatSettingsCollectionConverter();

            string buildSettingsString;
            reader.ReadSettingString("BuildOutputSettings", out buildSettingsString);

            if (!string.IsNullOrWhiteSpace(buildSettingsString))
                BuildOutputSettings = (Collection<ColorerFormatSetting>)typeConverter.ConvertFrom(buildSettingsString);

            string debugSettingsString;
            reader.ReadSettingString("BuildOutputSettings", out debugSettingsString);

            if (!string.IsNullOrWhiteSpace(buildSettingsString))
                BuildOutputSettings = (Collection<ColorerFormatSetting>)typeConverter.ConvertFrom(debugSettingsString);
        }

        private static Collection<ColorerFormatSetting> GetBuildOutputDefaultConfiguration()
        {
            var buildOutputSettings = new Collection<ColorerFormatSetting>();

            buildOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^={10}\sBuild:.*, 0 failed.*$",
                        ForeColor = Color.HotPink,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.BuildSucceded",
                        IsBold = true
                    }
                );

            buildOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^.+: warning \w*:.+$",
                        ForeColor = Color.Olive,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.BuildWarning",
                        IsBold = false
                    }
                );

            buildOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^.+: error \w+:.+$",
                        ForeColor = Color.Red,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.BuildError",
                        IsBold = true
                    }
                );
            
            buildOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^={10}\sBuild:.*, [^0]\d? failed.*$",
                        ForeColor = Color.Red,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.BuildFailed",
                        IsBold = true
                    }
                );

            buildOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^------ Build started:",
                        ForeColor = Color.Green,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.BuildStarted",
                        IsBold = true
                    }
                );

            return buildOutputSettings;
        }

        private static Collection<ColorerFormatSetting> GetDebugOutputDefaultConfiguration()
        {
            var debugOutputSettings = new Collection<ColorerFormatSetting>();

            debugOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^\'.+\'\s+.+: (?:Loaded|Cannot find or open the PDB file).*$",
                        ForeColor = Color.Silver,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.DebugLoadPDB",
                        IsBold = false
                    }
                );

            debugOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^The (?:thread|program) .+ has exited with code .+$",
                        ForeColor = Color.Silver,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.DebugThread",
                        IsBold = false
                    }
                );

            debugOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^.+: error \w+:.+$",
                        ForeColor = Color.Red,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.BuildError",
                        IsBold = true
                    }
                );

            debugOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^\s+at",
                        ForeColor = Color.Red,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.DebugException",
                        IsBold = true
                    }
                );

            debugOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^A first chance exception of type",
                        ForeColor = Color.Red,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.DebugException2",
                        IsBold = true
                    }
                );

            debugOutputSettings.Add(
                new ColorerFormatSetting
                    {
                        Regex = @"^\s*--- End of inner exception stack trace ---",
                        ForeColor = Color.Red,
                        BackColor = Color.White,
                        ClassificationType = "OutputColorer.DebugException3",
                        IsBold = true
                    }
                );

            return debugOutputSettings;
        }
    }
}