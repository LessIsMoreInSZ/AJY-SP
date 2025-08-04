
namespace SPWindowsForms.SwitchForms
{
    partial class EWeiHuControl
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
            this.leftButton_1 = new Sunny.UI.UIButton();
            this.right_label_1 = new Sunny.UI.UILabel();
            this.uiLabelName = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // leftButton_1
            // 
            this.leftButton_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftButton_1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.leftButton_1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.leftButton_1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.leftButton_1.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.leftButton_1.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.leftButton_1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftButton_1.ForeColor = System.Drawing.Color.Black;
            this.leftButton_1.LightColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.leftButton_1.Location = new System.Drawing.Point(57, 3);
            this.leftButton_1.MinimumSize = new System.Drawing.Size(1, 1);
            this.leftButton_1.Name = "leftButton_1";
            this.leftButton_1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.leftButton_1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(163)))), ((int)(((byte)(163)))));
            this.leftButton_1.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.leftButton_1.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.leftButton_1.Size = new System.Drawing.Size(100, 37);
            this.leftButton_1.Style = Sunny.UI.UIStyle.Custom;
            this.leftButton_1.TabIndex = 86;
            this.leftButton_1.Tag = "0";
            this.leftButton_1.Text = "uiButton1";
            this.leftButton_1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // right_label_1
            // 
            this.right_label_1.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.right_label_1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.right_label_1.Location = new System.Drawing.Point(703, 10);
            this.right_label_1.Name = "right_label_1";
            this.right_label_1.Size = new System.Drawing.Size(155, 23);
            this.right_label_1.TabIndex = 88;
            this.right_label_1.Text = "uiLabel2";
            this.right_label_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabelName
            // 
            this.uiLabelName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabelName.Location = new System.Drawing.Point(3, 9);
            this.uiLabelName.Name = "uiLabelName";
            this.uiLabelName.Size = new System.Drawing.Size(38, 23);
            this.uiLabelName.TabIndex = 87;
            this.uiLabelName.Text = "D10";
            this.uiLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EWeiHuControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.right_label_1);
            this.Controls.Add(this.uiLabelName);
            this.Controls.Add(this.leftButton_1);
            this.Name = "EWeiHuControl";
            this.Size = new System.Drawing.Size(863, 49);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton leftButton_1;
        private Sunny.UI.UILabel right_label_1;
        private Sunny.UI.UILabel uiLabelName;
    }
}
