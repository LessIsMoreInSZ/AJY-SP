
namespace SPWindowsForms
{
    partial class CopyOutForm
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
            this.uiLabel_pfName = new Sunny.UI.UILabel();
            this.uiListBox_pfName = new Sunny.UI.UIListBox();
            this.uiButton_Cancel = new Sunny.UI.UIButton();
            this.uiButton_OK = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiLabel_pfName
            // 
            this.uiLabel_pfName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel_pfName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel_pfName.Location = new System.Drawing.Point(19, 12);
            this.uiLabel_pfName.Name = "uiLabel_pfName";
            this.uiLabel_pfName.Size = new System.Drawing.Size(206, 23);
            this.uiLabel_pfName.TabIndex = 11;
            this.uiLabel_pfName.Text = "选择复制的配方";
            this.uiLabel_pfName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiListBox_pfName
            // 
            this.uiListBox_pfName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiListBox_pfName.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiListBox_pfName.ItemSelectForeColor = System.Drawing.Color.White;
            this.uiListBox_pfName.Location = new System.Drawing.Point(232, 11);
            this.uiListBox_pfName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBox_pfName.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox_pfName.Name = "uiListBox_pfName";
            this.uiListBox_pfName.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBox_pfName.ShowText = false;
            this.uiListBox_pfName.Size = new System.Drawing.Size(314, 580);
            this.uiListBox_pfName.TabIndex = 10;
            this.uiListBox_pfName.Text = "uiListBox1";
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
            this.uiButton_Cancel.Location = new System.Drawing.Point(571, 61);
            this.uiButton_Cancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Cancel.Name = "uiButton_Cancel";
            this.uiButton_Cancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiButton_Cancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.uiButton_Cancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiButton_Cancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiButton_Cancel.Size = new System.Drawing.Size(100, 35);
            this.uiButton_Cancel.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton_Cancel.TabIndex = 13;
            this.uiButton_Cancel.Text = "Cancel";
            this.uiButton_Cancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_Cancel.Click += new System.EventHandler(this.uiButton_Cancel_Click);
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
            this.uiButton_OK.Location = new System.Drawing.Point(571, 12);
            this.uiButton_OK.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_OK.Name = "uiButton_OK";
            this.uiButton_OK.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiButton_OK.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.uiButton_OK.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton_OK.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.uiButton_OK.Size = new System.Drawing.Size(100, 35);
            this.uiButton_OK.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton_OK.TabIndex = 12;
            this.uiButton_OK.Text = "OK";
            this.uiButton_OK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_OK.Click += new System.EventHandler(this.uiButton_OK_Click);
            // 
            // CopyOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(697, 605);
            this.Controls.Add(this.uiButton_Cancel);
            this.Controls.Add(this.uiButton_OK);
            this.Controls.Add(this.uiLabel_pfName);
            this.Controls.Add(this.uiListBox_pfName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CopyOutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CopyOutForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel_pfName;
        private Sunny.UI.UIListBox uiListBox_pfName;
        private Sunny.UI.UIButton uiButton_Cancel;
        private Sunny.UI.UIButton uiButton_OK;
    }
}