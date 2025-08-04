
namespace SPWindowsForms.SwitchForms
{
    partial class AirFilterControl
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
            this.left_label = new Sunny.UI.UIMarkLabel();
            this.txt_alarm = new Sunny.UI.UITextBox();
            this.btn_ql = new Sunny.UI.UIButton();
            this.label_yunxing = new Sunny.UI.UILabel();
            this.btn_use = new Sunny.UI.UIButton();
            this.label_leiji = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // left_label
            // 
            this.left_label.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.left_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.left_label.Location = new System.Drawing.Point(3, 6);
            this.left_label.Name = "left_label";
            this.left_label.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.left_label.Size = new System.Drawing.Size(229, 23);
            this.left_label.TabIndex = 104;
            this.left_label.Text = "uiMarkLabel3";
            this.left_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_alarm
            // 
            this.txt_alarm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_alarm.Font = new System.Drawing.Font("Arial", 10.5F);
            this.txt_alarm.Location = new System.Drawing.Point(607, 3);
            this.txt_alarm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_alarm.MinimumSize = new System.Drawing.Size(1, 16);
            this.txt_alarm.Name = "txt_alarm";
            this.txt_alarm.Padding = new System.Windows.Forms.Padding(5);
            this.txt_alarm.ShowText = false;
            this.txt_alarm.Size = new System.Drawing.Size(118, 28);
            this.txt_alarm.TabIndex = 120;
            this.txt_alarm.Text = " ";
            this.txt_alarm.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txt_alarm.Watermark = "";
            this.txt_alarm.TextChanged += new System.EventHandler(this.txt_alarm_TextChanged);
            // 
            // btn_ql
            // 
            this.btn_ql.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ql.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btn_ql.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btn_ql.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.btn_ql.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_ql.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_ql.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ql.ForeColor = System.Drawing.Color.Black;
            this.btn_ql.LightColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.btn_ql.Location = new System.Drawing.Point(504, 3);
            this.btn_ql.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_ql.Name = "btn_ql";
            this.btn_ql.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btn_ql.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.btn_ql.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_ql.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_ql.Size = new System.Drawing.Size(96, 28);
            this.btn_ql.Style = Sunny.UI.UIStyle.Custom;
            this.btn_ql.TabIndex = 119;
            this.btn_ql.Text = "CL";
            this.btn_ql.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ql.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_ql_MouseDown);
            this.btn_ql.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_ql_MouseUp);
            // 
            // label_yunxing
            // 
            this.label_yunxing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_yunxing.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_yunxing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label_yunxing.Location = new System.Drawing.Point(365, 6);
            this.label_yunxing.Name = "label_yunxing";
            this.label_yunxing.Size = new System.Drawing.Size(121, 23);
            this.label_yunxing.TabIndex = 118;
            this.label_yunxing.Text = "uiLabel2";
            this.label_yunxing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_use
            // 
            this.btn_use.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_use.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btn_use.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btn_use.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.btn_use.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_use.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_use.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_use.ForeColor = System.Drawing.Color.Black;
            this.btn_use.LightColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.btn_use.Location = new System.Drawing.Point(732, 3);
            this.btn_use.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_use.Name = "btn_use";
            this.btn_use.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btn_use.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.btn_use.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_use.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.btn_use.Size = new System.Drawing.Size(124, 28);
            this.btn_use.Style = Sunny.UI.UIStyle.Custom;
            this.btn_use.TabIndex = 111;
            this.btn_use.Text = "Dis";
            this.btn_use.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_use.Click += new System.EventHandler(this.btn_use_Click);
            // 
            // label_leiji
            // 
            this.label_leiji.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_leiji.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_leiji.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label_leiji.Location = new System.Drawing.Point(238, 6);
            this.label_leiji.Name = "label_leiji";
            this.label_leiji.Size = new System.Drawing.Size(121, 23);
            this.label_leiji.TabIndex = 121;
            this.label_leiji.Text = "uiLabel2";
            this.label_leiji.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AirFilterControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label_leiji);
            this.Controls.Add(this.btn_use);
            this.Controls.Add(this.txt_alarm);
            this.Controls.Add(this.btn_ql);
            this.Controls.Add(this.label_yunxing);
            this.Controls.Add(this.left_label);
            this.Name = "AirFilterControl";
            this.Size = new System.Drawing.Size(863, 34);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIMarkLabel left_label;
        private Sunny.UI.UITextBox txt_alarm;
        private Sunny.UI.UIButton btn_ql;
        private Sunny.UI.UILabel label_yunxing;
        private Sunny.UI.UIButton btn_use;
        private Sunny.UI.UILabel label_leiji;
    }
}
