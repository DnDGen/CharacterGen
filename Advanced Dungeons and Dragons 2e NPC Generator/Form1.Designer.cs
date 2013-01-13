namespace NPCGen
{
    partial class NPCGenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NPCGenForm));
            this.ClassCombo = new System.Windows.Forms.ComboBox();
            this.ClassLvlCombo = new System.Windows.Forms.ComboBox();
            this.RaceCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GenderCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RollMethodCombo = new System.Windows.Forms.ComboBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.AlignmentCombo = new System.Windows.Forms.ComboBox();
            this.MetaRaceCombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClassCombo
            // 
            this.ClassCombo.FormattingEnabled = true;
            this.ClassCombo.Location = new System.Drawing.Point(81, 93);
            this.ClassCombo.Name = "ClassCombo";
            this.ClassCombo.Size = new System.Drawing.Size(248, 21);
            this.ClassCombo.Sorted = true;
            this.ClassCombo.TabIndex = 0;
            // 
            // ClassLvlCombo
            // 
            this.ClassLvlCombo.FormattingEnabled = true;
            this.ClassLvlCombo.Location = new System.Drawing.Point(82, 120);
            this.ClassLvlCombo.Name = "ClassLvlCombo";
            this.ClassLvlCombo.Size = new System.Drawing.Size(248, 21);
            this.ClassLvlCombo.TabIndex = 1;
            // 
            // RaceCombo
            // 
            this.RaceCombo.FormattingEnabled = true;
            this.RaceCombo.Location = new System.Drawing.Point(82, 12);
            this.RaceCombo.Name = "RaceCombo";
            this.RaceCombo.Size = new System.Drawing.Size(248, 21);
            this.RaceCombo.Sorted = true;
            this.RaceCombo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Tan;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(43, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Race";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Tan;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(43, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Class";
            // 
            // GenderCombo
            // 
            this.GenderCombo.FormattingEnabled = true;
            this.GenderCombo.Location = new System.Drawing.Point(82, 66);
            this.GenderCombo.Name = "GenderCombo";
            this.GenderCombo.Size = new System.Drawing.Size(248, 21);
            this.GenderCombo.Sorted = true;
            this.GenderCombo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Tan;
            this.label2.Location = new System.Drawing.Point(12, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Roll Method";
            // 
            // RollMethodCombo
            // 
            this.RollMethodCombo.FormattingEnabled = true;
            this.RollMethodCombo.Location = new System.Drawing.Point(82, 174);
            this.RollMethodCombo.Name = "RollMethodCombo";
            this.RollMethodCombo.Size = new System.Drawing.Size(248, 21);
            this.RollMethodCombo.Sorted = true;
            this.RollMethodCombo.TabIndex = 12;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateButton.Location = new System.Drawing.Point(82, 201);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(121, 38);
            this.GenerateButton.TabIndex = 13;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // OutputText
            // 
            this.OutputText.Location = new System.Drawing.Point(336, 12);
            this.OutputText.Name = "OutputText";
            this.OutputText.Size = new System.Drawing.Size(390, 276);
            this.OutputText.TabIndex = 14;
            this.OutputText.Text = "";
            // 
            // ResetButton
            // 
            this.ResetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetButton.Location = new System.Drawing.Point(208, 201);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(121, 38);
            this.ResetButton.TabIndex = 15;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Tan;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(23, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Alignment";
            // 
            // AlignmentCombo
            // 
            this.AlignmentCombo.FormattingEnabled = true;
            this.AlignmentCombo.Location = new System.Drawing.Point(82, 147);
            this.AlignmentCombo.Name = "AlignmentCombo";
            this.AlignmentCombo.Size = new System.Drawing.Size(248, 21);
            this.AlignmentCombo.Sorted = true;
            this.AlignmentCombo.TabIndex = 17;
            // 
            // MetaRaceCombo
            // 
            this.MetaRaceCombo.FormattingEnabled = true;
            this.MetaRaceCombo.Location = new System.Drawing.Point(82, 39);
            this.MetaRaceCombo.Name = "MetaRaceCombo";
            this.MetaRaceCombo.Size = new System.Drawing.Size(248, 21);
            this.MetaRaceCombo.Sorted = true;
            this.MetaRaceCombo.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Tan;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(33, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Gender";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Tan;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(16, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Meta-Race";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Tan;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(43, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Level";
            // 
            // NPCGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(738, 300);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MetaRaceCombo);
            this.Controls.Add(this.AlignmentCombo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.RollMethodCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GenderCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RaceCombo);
            this.Controls.Add(this.ClassLvlCombo);
            this.Controls.Add(this.ClassCombo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NPCGenForm";
            this.Text = "NPC Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ClassCombo;
        private System.Windows.Forms.ComboBox ClassLvlCombo;
        private System.Windows.Forms.ComboBox RaceCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox GenderCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox RollMethodCombo;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.RichTextBox OutputText;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox AlignmentCombo;
        private System.Windows.Forms.ComboBox MetaRaceCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

