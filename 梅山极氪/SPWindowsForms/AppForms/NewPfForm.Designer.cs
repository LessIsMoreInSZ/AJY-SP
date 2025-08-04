
namespace SPWindowsForms
{
    partial class NewPfForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiTextBox_pfname = new Sunny.UI.UITextBox();
            this.uiListBox_copyName = new Sunny.UI.UIListBox();
            this.uiLabel_pf = new Sunny.UI.UILabel();
            this.uiLabel_copy = new Sunny.UI.UILabel();
            this.uiCheckBox_copy = new Sunny.UI.UICheckBox();
            this.uiLabel_copyName = new Sunny.UI.UILabel();
            this.uiButton_OK = new Sunny.UI.UIButton();
            this.uiButton_Cancel = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiTextBox_pfname
            // 
            this.uiTextBox_pfname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_pfname.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox_pfname.Location = new System.Drawing.Point(230, 14);
            this.uiTextBox_pfname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox_pfname.MaxLength = 16;
            this.uiTextBox_pfname.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox_pfname.Name = "uiTextBox_pfname";
            this.uiTextBox_pfname.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_pfname.ShowText = false;
            this.uiTextBox_pfname.Size = new System.Drawing.Size(314, 29);
            this.uiTextBox_pfname.TabIndex = 0;
            this.uiTextBox_pfname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox_pfname.Watermark = "";
            // 
            // uiListBox_copyName
            // 
            this.uiListBox_copyName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBox_copyName.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiListBox_copyName.ItemSelectForeColor = System.Drawing.Color.White;
            this.uiListBox_copyName.Location = new System.Drawing.Point(230, 106);
            this.uiListBox_copyName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBox_copyName.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox_copyName.Name = "uiListBox_copyName";
            this.uiListBox_copyName.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBox_copyName.ShowText = false;
            this.uiListBox_copyName.Size = new System.Drawing.Size(314, 511);
            this.uiListBox_copyName.TabIndex = 1;
            this.uiListBox_copyName.Text = "uiListBox1";
            this.uiListBox_copyName.Visible = false;
            // 
            // uiLabel_pf
            // 
            this.uiLabel_pf.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel_pf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel_pf.Location = new System.Drawing.Point(12, 16);
            this.uiLabel_pf.Name = "uiLabel_pf";
            this.uiLabel_pf.Size = new System.Drawing.Size(206, 23);
            this.uiLabel_pf.TabIndex = 2;
            this.uiLabel_pf.Text = "配方名";
            this.uiLabel_pf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel_copy
            // 
            this.uiLabel_copy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel_copy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel_copy.Location = new System.Drawing.Point(12, 61);
            this.uiLabel_copy.Name = "uiLabel_copy";
            this.uiLabel_copy.Size = new System.Drawing.Size(206, 23);
            this.uiLabel_copy.TabIndex = 3;
            this.uiLabel_copy.Text = "是否复制其他配方2";
            this.uiLabel_copy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiCheckBox_copy
            // 
            this.uiCheckBox_copy.CheckBoxSize = 20;
            this.uiCheckBox_copy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiCheckBox_copy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiCheckBox_copy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiCheckBox_copy.Location = new System.Drawing.Point(218, 57);
            this.uiCheckBox_copy.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBox_copy.Name = "uiCheckBox_copy";
            this.uiCheckBox_copy.Size = new System.Drawing.Size(42, 29);
            this.uiCheckBox_copy.TabIndex = 4;
            this.uiCheckBox_copy.CheckedChanged += new System.EventHandler(this.uiCheckBox_copy_CheckedChanged);
            // 
            // uiLabel_copyName
            // 
            this.uiLabel_copyName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel_copyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel_copyName.Location = new System.Drawing.Point(12, 108);
            this.uiLabel_copyName.Name = "uiLabel_copyName";
            this.uiLabel_copyName.Size = new System.Drawing.Size(206, 23);
            this.uiLabel_copyName.TabIndex = 5;
            this.uiLabel_copyName.Text = "选择复制的配方3";
            this.uiLabel_copyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel_copyName.Visible = false;
            // 
            // uiButton_OK
            // 
            this.uiButton_OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_OK.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton_OK.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton_OK.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.uiButton_OK.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton_OK.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton_OK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_OK.LightColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            this.uiButton_OK.Location = new System.Drawing.Point(560, 14);
            this.uiButton_OK.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_OK.Name = "uiButton_OK";
            this.uiButton_OK.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton_OK.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.uiButton_OK.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton_OK.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton_OK.Size = new System.Drawing.Size(100, 35);
            this.uiButton_OK.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton_OK.TabIndex = 6;
            this.uiButton_OK.Text = "OK";
            this.uiButton_OK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_OK.Click += new System.EventHandler(this.uiButton_OK_Click);
            // 
            // uiButton_Cancel
            // 
            this.uiButton_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_Cancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_Cancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_Cancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.uiButton_Cancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiButton_Cancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiButton_Cancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_Cancel.LightColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.uiButton_Cancel.Location = new System.Drawing.Point(560, 63);
            this.uiButton_Cancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Cancel.Name = "uiButton_Cancel";
            this.uiButton_Cancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_Cancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.uiButton_Cancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiButton_Cancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiButton_Cancel.Size = new System.Drawing.Size(100, 35);
            this.uiButton_Cancel.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton_Cancel.TabIndex = 7;
            this.uiButton_Cancel.Text = "Cancel";
            this.uiButton_Cancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_Cancel.Click += new System.EventHandler(this.uiButton_Cancel_Click);
            // 
            // NewPfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(674, 647);
            this.ControlBox = false;
            this.Controls.Add(this.uiButton_Cancel);
            this.Controls.Add(this.uiButton_OK);
            this.Controls.Add(this.uiLabel_copyName);
            this.Controls.Add(this.uiCheckBox_copy);
            this.Controls.Add(this.uiLabel_copy);
            this.Controls.Add(this.uiLabel_pf);
            this.Controls.Add(this.uiListBox_copyName);
            this.Controls.Add(this.uiTextBox_pfname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewPfForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UITextBox uiTextBox_pfname;
        private Sunny.UI.UIListBox uiListBox_copyName;
        private Sunny.UI.UILabel uiLabel_pf;
        private Sunny.UI.UILabel uiLabel_copy;
        private Sunny.UI.UICheckBox uiCheckBox_copy;
        private Sunny.UI.UILabel uiLabel_copyName;
        private Sunny.UI.UIButton uiButton_OK;
        private Sunny.UI.UIButton uiButton_Cancel;
    }
}