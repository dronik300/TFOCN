
namespace lr_1
{
    partial class Com_port
    {
 
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.InputBox = new System.Windows.Forms.TextBox();
            this.ComboBox = new System.Windows.Forms.ComboBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputBox
            // 
            this.InputBox.Enabled = false;
            this.InputBox.Location = new System.Drawing.Point(0, 21);
            this.InputBox.Multiline = true;
            this.InputBox.Name = "InputBox";
            this.InputBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.InputBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.InputBox.Size = new System.Drawing.Size(341, 104);
            this.InputBox.TabIndex = 0;
            this.InputBox.WordWrap = false;
            this.InputBox.TextChanged += new System.EventHandler(this.InputBox_TextChanged);
            // 
            // ComboBox
            // 
            this.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox.FormattingEnabled = true;
            this.ComboBox.Location = new System.Drawing.Point(232, 394);
            this.ComboBox.Name = "ComboBox";
            this.ComboBox.Size = new System.Drawing.Size(121, 28);
            this.ComboBox.TabIndex = 1;
            this.ComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged_1);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(232, 365);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(121, 23);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.ColumnWidth = 10;
            this.OutputBox.FormattingEnabled = true;
            this.OutputBox.HorizontalScrollbar = true;
            this.OutputBox.ItemHeight = 20;
            this.OutputBox.Location = new System.Drawing.Point(0, 21);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.OutputBox.Size = new System.Drawing.Size(341, 104);
            this.OutputBox.TabIndex = 4;
            this.OutputBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InputBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 125);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            this.groupBox1.AutoSizeChanged += new System.EventHandler(this.groupBox1_Enter);
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OutputBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 146);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            this.groupBox2.AutoSizeChanged += new System.EventHandler(this.Form1_Load);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 290);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(341, 69);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Tag = "";
            this.richTextBox1.Text = "Control/Debug";
            // 
            // Com_port
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(365, 434);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ComboBox);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Com_port";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Com_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.ComboBox ComboBox;
        private System.Windows.Forms.ListBox OutputBox;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

