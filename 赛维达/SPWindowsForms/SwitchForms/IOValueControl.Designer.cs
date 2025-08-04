
namespace SPWindowsForms.SwitchForms
{
    partial class IOValueControl
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
            this.iotext = new Sunny.UI.UILabel();
            this.ioname = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // iotext
            // 
            this.iotext.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iotext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.iotext.Location = new System.Drawing.Point(63, 3);
            this.iotext.Name = "iotext";
            this.iotext.Size = new System.Drawing.Size(147, 23);
            this.iotext.TabIndex = 251;
            this.iotext.Text = "DI1.1";
            this.iotext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ioname
            // 
            this.ioname.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ioname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ioname.Location = new System.Drawing.Point(4, 10);
            this.ioname.Name = "ioname";
            this.ioname.Size = new System.Drawing.Size(53, 23);
            this.ioname.TabIndex = 250;
            this.ioname.Text = "DI1.1";
            this.ioname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IOValueControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.iotext);
            this.Controls.Add(this.ioname);
            this.Name = "IOValueControl";
            this.Size = new System.Drawing.Size(211, 36);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel iotext;
        private Sunny.UI.UILabel ioname;
    }
}
