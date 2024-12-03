namespace WinFormsApp
{
    partial class Form2
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
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            pictureBox1 = new PictureBox();
            listBox1 = new ListBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            dateTimePicker1 = new DateTimePicker();
            domainUpDown1 = new DomainUpDown();
            label6 = new Label();
            label7 = new Label();
            textBox2 = new TextBox();
            label8 = new Label();
            textBox4 = new TextBox();
            label9 = new Label();
            label10 = new Label();
            richTextBox1 = new RichTextBox();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(19, 47);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(83, 19);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(19, 87);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(83, 19);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "checkBox2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(19, 128);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(83, 19);
            checkBox3.TabIndex = 3;
            checkBox3.Text = "checkBox3";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(643, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(145, 135);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(652, 236);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 94);
            listBox1.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(697, 404);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 7;
            button1.Text = "push auction";
            button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 109);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 8;
            label1.Text = "Comic";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1, 69);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 9;
            label2.Text = "Manga";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 29);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 10;
            label3.Text = "Standart book";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 254);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(120, 23);
            textBox3.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 236);
            label4.Name = "label4";
            label4.Size = new Size(27, 15);
            label4.TabIndex = 13;
            label4.Text = "title";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(619, 199);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 14;
            label5.Text = "book details";
            label5.Click += label5_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(602, 365);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 15;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // domainUpDown1
            // 
            domainUpDown1.Location = new Point(12, 197);
            domainUpDown1.Name = "domainUpDown1";
            domainUpDown1.Size = new Size(120, 23);
            domainUpDown1.TabIndex = 16;
            domainUpDown1.Text = "domainUpDown1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(578, 347);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 17;
            label6.Text = "set Timer";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(-2, 179);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 18;
            label7.Text = "genre";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 365);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 19;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(-2, 347);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 20;
            label8.Text = "size of book";
            label8.Click += label8_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(12, 307);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(-2, 290);
            label9.Name = "label9";
            label9.Size = new Size(58, 15);
            label9.TabIndex = 22;
            label9.Text = "relizeDate";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(602, 9);
            label10.Name = "label10";
            label10.Size = new Size(88, 15);
            label10.TabIndex = 23;
            label10.Text = "picture of book";
            label10.Click += label10_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(205, 29);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(322, 248);
            richTextBox1.TabIndex = 24;
            richTextBox1.Text = "";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(175, 9);
            label11.Name = "label11";
            label11.Size = new Size(57, 15);
            label11.TabIndex = 25;
            label11.Text = "summery";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label11);
            Controls.Add(richTextBox1);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(textBox4);
            Controls.Add(label8);
            Controls.Add(textBox2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(domainUpDown1);
            Controls.Add(dateTimePicker1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Controls.Add(pictureBox1);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private PictureBox pictureBox1;
        private ListBox listBox1;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox3;
        private Label label4;
        private Label label5;
        private DateTimePicker dateTimePicker1;
        private DomainUpDown domainUpDown1;
        private Label label6;
        private Label label7;
        private TextBox textBox2;
        private Label label8;
        private TextBox textBox4;
        private Label label9;
        private Label label10;
        private RichTextBox richTextBox1;
        private Label label11;
    }
}