
namespace SPWindowsForms.SwitchForms
{
    partial class PGsVacControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_title = new Sunny.UI.UIMarkLabel();
            this.label_gongchen = new Sunny.UI.UILabel();
            this.label_guochen = new Sunny.UI.UILabel();
            this.txt_low = new Sunny.UI.UITextBox();
            this.txt_high = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label_title.Location = new System.Drawing.Point(4, 6);
            this.label_title.Name = "label_title";
            this.label_title.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label_title.Size = new System.Drawing.Size(86, 23);
            this.label_title.TabIndex = 105;
            this.label_title.Text = "TANK1.PG";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_gongchen
            // 
            this.label_gongchen.BackColor = System.Drawing.Color.Gold;
            this.label_gongchen.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_gongchen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label_gongchen.Location = new System.Drawing.Point(94, 6);
            this.label_gongchen.Name = "label_gongchen";
            this.label_gongchen.Size = new System.Drawing.Size(80, 23);
            this.label_gongchen.TabIndex = 106;
            this.label_gongchen.Text = "99999";
            this.label_gongchen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_guochen
            // 
            this.label_guochen.BackColor = System.Drawing.Color.LightGreen;
            this.label_guochen.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_guochen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label_guochen.Location = new System.Drawing.Point(179, 6);
            this.label_guochen.Name = "label_guochen";
            this.label_guochen.Size = new System.Drawing.Size(80, 23);
            this.label_guochen.TabIndex = 107;
            this.label_guochen.Text = "99999";
            this.label_guochen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_low
            // 
            this.txt_low.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_low.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_low.Location = new System.Drawing.Point(264, 6);
            this.txt_low.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_low.MinimumSize = new System.Drawing.Size(1, 16);
            this.txt_low.Name = "txt_low";
            this.txt_low.Padding = new System.Windows.Forms.Padding(5);
            this.txt_low.ShowText = false;
            this.txt_low.Size = new System.Drawing.Size(80, 23);
            this.txt_low.TabIndex = 121;
            this.txt_low.Text = " ";
            this.txt_low.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txt_low.Watermark = "";
            this.txt_low.TextChanged += new System.EventHandler(this.txt_low_TextChanged);
            // 
            // txt_high
            // 
            this.txt_high.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_high.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_high.Location = new System.Drawing.Point(349, 6);
            this.txt_high.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_high.MinimumSize = new System.Drawing.Size(1, 16);
            this.txt_high.Name = "txt_high";
            this.txt_high.Padding = new System.Windows.Forms.Padding(5);
            this.txt_high.ShowText = false;
            this.txt_high.Size = new System.Drawing.Size(80, 23);
            this.txt_high.TabIndex = 122;
            this.txt_high.Text = " ";
            this.txt_high.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txt_high.Watermark = "";
            this.txt_high.TextChanged += new System.EventHandler(this.txt_high_TextChanged);
            // 
            // PGsVacControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txt_high);
            this.Controls.Add(this.txt_low);
            this.Controls.Add(this.label_guochen);
            this.Controls.Add(this.label_gongchen);
            this.Controls.Add(this.label_title);
            this.Name = "PGsVacControl";
            this.Size = new System.Drawing.Size(430, 34);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIMarkLabel label_title;
        private Sunny.UI.UILabel label_gongchen;
        private Sunny.UI.UILabel label_guochen;
        private Sunny.UI.UITextBox txt_low;
        private Sunny.UI.UITextBox txt_high;
    }
}
