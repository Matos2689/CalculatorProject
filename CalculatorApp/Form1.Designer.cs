namespace CalculatorApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ButtomAdd = new Button();
            ButtomSubtract = new Button();
            SuspendLayout();
            // 
            // ButtomAdd
            // 
            ButtomAdd.Location = new Point(84, 173);
            ButtomAdd.Name = "ButtomAdd";
            ButtomAdd.Size = new Size(94, 29);
            ButtomAdd.TabIndex = 1;
            ButtomAdd.Text = "+";
            ButtomAdd.UseVisualStyleBackColor = true;
            ButtomAdd.Click += ButtomAdd_Click;
            // 
            // ButtomSubtract
            // 
            ButtomSubtract.Location = new Point(246, 179);
            ButtomSubtract.Name = "ButtomSubtract";
            ButtomSubtract.Size = new Size(94, 29);
            ButtomSubtract.TabIndex = 2;
            ButtomSubtract.Text = "-";
            ButtomSubtract.UseVisualStyleBackColor = true;
            ButtomSubtract.Click += ButtomSubtract_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtomSubtract);
            Controls.Add(ButtomAdd);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Button ButtomAdd;
        private Button ButtomSubtract;
    }
}
