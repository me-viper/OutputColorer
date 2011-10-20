using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell;

namespace Talk2Bits.OutputColorer.Controls
{
    public partial class OutputColorerOptions : UserControl
    {
        public OutputColorerOptions()
        {
            InitializeComponent();

            _buildGridView.CellValidating += GridViewOnCellValidating;
            _buildGridView.CellEndEdit += GridViewOnCellEndEdit;
            _buildGridView.CellBeginEdit += GridViewOnCellBeginEdit;

            _debugGridView.CellValidating += GridViewOnCellValidating;
            _debugGridView.CellEndEdit += GridViewOnCellEndEdit;
            _debugGridView.CellBeginEdit += GridViewOnCellBeginEdit;
        }

        public OutputColorerOptionsPage OptionsPage { get; set; }

        public IEnumerable<ColorerFormatSetting> BuildOuptutSetting
        {
            get { return _buildSettingsBindingSource.Cast<ColorerFormatSetting>(); }
            set
            {
                if (value != null)
                {
                    _buildSettingsBindingSource.Clear();

                    foreach (var cfs in value)
                        _buildSettingsBindingSource.Add(cfs);
                }
            }
        }

        public IEnumerable<ColorerFormatSetting> DebugOuptutSetting
        {
            get { return _debugSettingsBindingSource.Cast<ColorerFormatSetting>(); }
            set
            {
                if (value != null)
                {
                    _debugSettingsBindingSource.Clear();

                    foreach (var cfs in value)
                        _debugSettingsBindingSource.Add(cfs);
                }
            }
        }

        public void Apply(DialogPage.ApplyKind applyKind)
        {
            if (applyKind != DialogPage.ApplyKind.Apply)
            {
                _buildGridView.CancelEdit();
                _debugGridView.CancelEdit();
            }
            else
            {
                _buildGridView.EndEdit();
                _debugGridView.EndEdit();
            }
            
        }

        private static void GridViewOnCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var gridView = (DataGridView)sender;

            var cell = gridView.Rows[e.RowIndex].Cells[0];
            var bindingSource = (BindingSource)gridView.DataSource;

            if (string.IsNullOrWhiteSpace(cell.Value as string))
                ((ColorerFormatSetting)bindingSource.Current).ClassificationType = string.Format("OutputColorer.{0}", Guid.NewGuid());
        }

        private static void GridViewOnCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var gridView = (DataGridView)sender;

            if (!string.Equals(gridView.Columns[e.ColumnIndex].HeaderText, "Regex", StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                var regex = e.FormattedValue.ToString();

                if (string.IsNullOrWhiteSpace(regex))
                {
                    e.Cancel = true;
                    return;
                }

                Regex.IsMatch("test", regex);
            }
            catch (ArgumentException)
            {
                gridView.Rows[e.RowIndex].ErrorText = "Invalid regular expression";
                e.Cancel = true;
            }
        }

        private static void GridViewOnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var gridView = (DataGridView)sender;

            gridView.Rows[e.RowIndex].ErrorText = string.Empty;
        }
    }
}
