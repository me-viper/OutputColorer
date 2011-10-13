namespace Talk2Bits.OutputColorer.Controls
{
    partial class OutputColorerOptions
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._tabPageBuild = new System.Windows.Forms.TabPage();
            this._tabPageOutput = new System.Windows.Forms.TabPage();
            this._colorerFormatSettingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._columnSettingId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._columnRegex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isBoldDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.foreColorDataGridViewTextBoxColumn = new Talk2Bits.OutputColorer.Controls.ColorPickerColumn();
            this.backColorDataGridViewTextBoxColumn = new Talk2Bits.OutputColorer.Controls.ColorPickerColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colorPickerColumn1 = new Talk2Bits.OutputColorer.Controls.ColorPickerColumn();
            this.colorPickerColumn2 = new Talk2Bits.OutputColorer.Controls.ColorPickerColumn();
            this.tabControl1.SuspendLayout();
            this._tabPageBuild.SuspendLayout();
            this._tabPageOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._colorerFormatSettingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._tabPageBuild);
            this.tabControl1.Controls.Add(this._tabPageOutput);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(750, 377);
            this.tabControl1.TabIndex = 0;
            // 
            // _tabPageBuild
            // 
            this._tabPageBuild.Controls.Add(this.dataGridView1);
            this._tabPageBuild.Location = new System.Drawing.Point(4, 22);
            this._tabPageBuild.Name = "_tabPageBuild";
            this._tabPageBuild.Padding = new System.Windows.Forms.Padding(3);
            this._tabPageBuild.Size = new System.Drawing.Size(742, 351);
            this._tabPageBuild.TabIndex = 0;
            this._tabPageBuild.Text = "tabPageBuild";
            this._tabPageBuild.UseVisualStyleBackColor = true;
            // 
            // _tabPageOutput
            // 
            this._tabPageOutput.Controls.Add(this._dataGridView);
            this._tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this._tabPageOutput.Name = "_tabPageOutput";
            this._tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this._tabPageOutput.Size = new System.Drawing.Size(742, 351);
            this._tabPageOutput.TabIndex = 1;
            this._tabPageOutput.Text = "tabPageOutput";
            this._tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // _colorerFormatSettingBindingSource
            // 
            this._colorerFormatSettingBindingSource.DataSource = typeof(Talk2Bits.OutputColorer.ColorerFormatSetting);
            // 
            // _dataGridView
            // 
            this._dataGridView.AutoGenerateColumns = false;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._columnSettingId,
            this._columnRegex,
            this.isBoldDataGridViewCheckBoxColumn,
            this.foreColorDataGridViewTextBoxColumn,
            this.backColorDataGridViewTextBoxColumn});
            this._dataGridView.DataSource = this._colorerFormatSettingBindingSource;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(3, 3);
            this._dataGridView.MultiSelect = false;
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridView.Size = new System.Drawing.Size(736, 345);
            this._dataGridView.TabIndex = 1;
            // 
            // _columnSettingId
            // 
            this._columnSettingId.DataPropertyName = "Id";
            this._columnSettingId.HeaderText = "Id";
            this._columnSettingId.Name = "_columnSettingId";
            this._columnSettingId.Visible = false;
            // 
            // _columnRegex
            // 
            this._columnRegex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._columnRegex.DataPropertyName = "Regex";
            this._columnRegex.HeaderText = "Regex";
            this._columnRegex.Name = "_columnRegex";
            // 
            // isBoldDataGridViewCheckBoxColumn
            // 
            this.isBoldDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.isBoldDataGridViewCheckBoxColumn.DataPropertyName = "IsBold";
            this.isBoldDataGridViewCheckBoxColumn.HeaderText = "Is Bold";
            this.isBoldDataGridViewCheckBoxColumn.Name = "isBoldDataGridViewCheckBoxColumn";
            this.isBoldDataGridViewCheckBoxColumn.Width = 45;
            // 
            // foreColorDataGridViewTextBoxColumn
            // 
            this.foreColorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.foreColorDataGridViewTextBoxColumn.DataPropertyName = "ForeColor";
            this.foreColorDataGridViewTextBoxColumn.HeaderText = "Fore Color";
            this.foreColorDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.foreColorDataGridViewTextBoxColumn.Name = "foreColorDataGridViewTextBoxColumn";
            this.foreColorDataGridViewTextBoxColumn.ReadOnly = true;
            this.foreColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.foreColorDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // backColorDataGridViewTextBoxColumn
            // 
            this.backColorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.backColorDataGridViewTextBoxColumn.DataPropertyName = "BackColor";
            this.backColorDataGridViewTextBoxColumn.HeaderText = "Back Color";
            this.backColorDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.backColorDataGridViewTextBoxColumn.Name = "backColorDataGridViewTextBoxColumn";
            this.backColorDataGridViewTextBoxColumn.ReadOnly = true;
            this.backColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.backColorDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn1,
            this.colorPickerColumn1,
            this.colorPickerColumn2});
            this.dataGridView1.DataSource = this._colorerFormatSettingBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(736, 345);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Regex";
            this.dataGridViewTextBoxColumn2.HeaderText = "Regex";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsBold";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Is Bold";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 45;
            // 
            // colorPickerColumn1
            // 
            this.colorPickerColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colorPickerColumn1.DataPropertyName = "ForeColor";
            this.colorPickerColumn1.HeaderText = "Fore Color";
            this.colorPickerColumn1.MinimumWidth = 100;
            this.colorPickerColumn1.Name = "colorPickerColumn1";
            this.colorPickerColumn1.ReadOnly = true;
            this.colorPickerColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colorPickerColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colorPickerColumn2
            // 
            this.colorPickerColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colorPickerColumn2.DataPropertyName = "BackColor";
            this.colorPickerColumn2.HeaderText = "Back Color";
            this.colorPickerColumn2.MinimumWidth = 100;
            this.colorPickerColumn2.Name = "colorPickerColumn2";
            this.colorPickerColumn2.ReadOnly = true;
            this.colorPickerColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colorPickerColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // OutputColorerOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "OutputColorerOptions";
            this.Size = new System.Drawing.Size(750, 377);
            this.tabControl1.ResumeLayout(false);
            this._tabPageBuild.ResumeLayout(false);
            this._tabPageOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._colorerFormatSettingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _tabPageBuild;
        private System.Windows.Forms.TabPage _tabPageOutput;
        private System.Windows.Forms.BindingSource _colorerFormatSettingBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private ColorPickerColumn colorPickerColumn1;
        private ColorPickerColumn colorPickerColumn2;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnSettingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn _columnRegex;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isBoldDataGridViewCheckBoxColumn;
        private ColorPickerColumn foreColorDataGridViewTextBoxColumn;
        private ColorPickerColumn backColorDataGridViewTextBoxColumn;

    }
}
