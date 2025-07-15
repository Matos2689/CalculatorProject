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
            ButtonMultiply = new Button();
            ButtonDivide = new Button();
            ButtonClear = new Button();
            buttonCalculate = new Button();
            ButtonNumber1 = new Button();
            textBoxExpression = new TextBox();
            ButtonNumber2 = new Button();
            ButtonNumber4 = new Button();
            ButtonNumber3 = new Button();
            ButtonNumber5 = new Button();
            ButtonNumber6 = new Button();
            ButtonNumber7 = new Button();
            ButtonNumber8 = new Button();
            ButtonNumber9 = new Button();
            ButtonNumber0 = new Button();
            ButtonPoint = new Button();
            ButtonMeter = new Button();
            ButtonMillimeter = new Button();
            ButtonCentimeters = new Button();
            ButtonKilometers = new Button();
            ButtonExit = new Button();
            ButtonLiter = new Button();
            ButtonMilliLiters = new Button();
            ButtonCentiliter = new Button();
            ButtonKiloliter = new Button();
            ButtonGram = new Button();
            ButtonMilligram = new Button();
            ButtonCentigram = new Button();
            ButtonKilogram = new Button();
            ButtonSaveJson = new Button();
            ButtonSaveXml = new Button();
            ButtonSaveSQL = new Button();
            ButtonPercentage = new Button();
            SuspendLayout();
            // 
            // ButtomAdd
            // 
            ButtomAdd.Cursor = Cursors.Hand;
            ButtomAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtomAdd.Location = new Point(428, 413);
            ButtomAdd.Name = "ButtomAdd";
            ButtomAdd.Size = new Size(94, 29);
            ButtomAdd.TabIndex = 1;
            ButtomAdd.Text = "+";
            ButtomAdd.UseVisualStyleBackColor = true;
            ButtomAdd.Click += ButtomAdd_Click;
            // 
            // ButtomSubtract
            // 
            ButtomSubtract.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtomSubtract.Location = new Point(428, 363);
            ButtomSubtract.Name = "ButtomSubtract";
            ButtomSubtract.Size = new Size(94, 29);
            ButtomSubtract.TabIndex = 2;
            ButtomSubtract.Text = "-";
            ButtomSubtract.UseVisualStyleBackColor = true;
            ButtomSubtract.Click += ButtomSubtract_Click;
            // 
            // ButtonMultiply
            // 
            ButtonMultiply.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonMultiply.Location = new Point(428, 312);
            ButtonMultiply.Name = "ButtonMultiply";
            ButtonMultiply.Size = new Size(94, 29);
            ButtonMultiply.TabIndex = 3;
            ButtonMultiply.Text = "x";
            ButtonMultiply.UseVisualStyleBackColor = true;
            ButtonMultiply.Click += ButtonMultiply_Click;
            // 
            // ButtonDivide
            // 
            ButtonDivide.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonDivide.Location = new Point(428, 263);
            ButtonDivide.Name = "ButtonDivide";
            ButtonDivide.Size = new Size(94, 29);
            ButtonDivide.TabIndex = 4;
            ButtonDivide.Text = "/";
            ButtonDivide.UseVisualStyleBackColor = true;
            ButtonDivide.Click += ButtonDivide_Click;
            // 
            // ButtonClear
            // 
            ButtonClear.BackColor = Color.Silver;
            ButtonClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonClear.Location = new Point(71, 229);
            ButtonClear.Name = "ButtonClear";
            ButtonClear.Size = new Size(94, 29);
            ButtonClear.TabIndex = 5;
            ButtonClear.Text = "Clear";
            ButtonClear.UseVisualStyleBackColor = false;
            ButtonClear.Click += ButtonClear_Click;
            // 
            // buttonCalculate
            // 
            buttonCalculate.BackColor = SystemColors.InactiveCaption;
            buttonCalculate.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonCalculate.Location = new Point(428, 458);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(94, 29);
            buttonCalculate.TabIndex = 6;
            buttonCalculate.Text = "=";
            buttonCalculate.UseVisualStyleBackColor = false;
            buttonCalculate.Click += buttonCalculate_Click;
            // 
            // ButtonNumber1
            // 
            ButtonNumber1.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonNumber1.Location = new Point(71, 365);
            ButtonNumber1.Name = "ButtonNumber1";
            ButtonNumber1.Size = new Size(94, 29);
            ButtonNumber1.TabIndex = 7;
            ButtonNumber1.Text = "1";
            ButtonNumber1.UseVisualStyleBackColor = true;
            ButtonNumber1.Click += ButtonNumber1_Click;
            // 
            // textBoxExpression
            // 
            textBoxExpression.BackColor = SystemColors.InactiveCaptionText;
            textBoxExpression.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxExpression.ForeColor = Color.FromArgb(255, 255, 128);
            textBoxExpression.Location = new Point(71, 58);
            textBoxExpression.Name = "textBoxExpression";
            textBoxExpression.Size = new Size(429, 34);
            textBoxExpression.TabIndex = 9;
            textBoxExpression.TextChanged += textBoxExpression_TextChanged;
            // 
            // ButtonNumber2
            // 
            ButtonNumber2.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonNumber2.Location = new Point(188, 363);
            ButtonNumber2.Name = "ButtonNumber2";
            ButtonNumber2.Size = new Size(94, 29);
            ButtonNumber2.TabIndex = 10;
            ButtonNumber2.Text = "2";
            ButtonNumber2.UseVisualStyleBackColor = true;
            ButtonNumber2.Click += ButtonNumber2_Click;
            // 
            // ButtonNumber4
            // 
            ButtonNumber4.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonNumber4.Location = new Point(71, 313);
            ButtonNumber4.Name = "ButtonNumber4";
            ButtonNumber4.Size = new Size(94, 29);
            ButtonNumber4.TabIndex = 12;
            ButtonNumber4.Text = "4";
            ButtonNumber4.UseVisualStyleBackColor = true;
            ButtonNumber4.Click += ButtonNumber4_Click;
            // 
            // ButtonNumber3
            // 
            ButtonNumber3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonNumber3.Location = new Point(305, 362);
            ButtonNumber3.Name = "ButtonNumber3";
            ButtonNumber3.Size = new Size(94, 29);
            ButtonNumber3.TabIndex = 20;
            ButtonNumber3.Text = "3";
            ButtonNumber3.UseVisualStyleBackColor = true;
            ButtonNumber3.Click += ButtonNumber3_Click;
            // 
            // ButtonNumber5
            // 
            ButtonNumber5.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonNumber5.Location = new Point(188, 313);
            ButtonNumber5.Name = "ButtonNumber5";
            ButtonNumber5.Size = new Size(94, 29);
            ButtonNumber5.TabIndex = 21;
            ButtonNumber5.Text = "5";
            ButtonNumber5.UseVisualStyleBackColor = true;
            ButtonNumber5.Click += ButtonNumber5_Click;
            // 
            // ButtonNumber6
            // 
            ButtonNumber6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonNumber6.Location = new Point(305, 313);
            ButtonNumber6.Name = "ButtonNumber6";
            ButtonNumber6.Size = new Size(94, 29);
            ButtonNumber6.TabIndex = 22;
            ButtonNumber6.Text = "6";
            ButtonNumber6.UseVisualStyleBackColor = true;
            ButtonNumber6.Click += ButtonNumber6_Click;
            // 
            // ButtonNumber7
            // 
            ButtonNumber7.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonNumber7.Location = new Point(71, 264);
            ButtonNumber7.Name = "ButtonNumber7";
            ButtonNumber7.Size = new Size(94, 29);
            ButtonNumber7.TabIndex = 23;
            ButtonNumber7.Text = "7";
            ButtonNumber7.UseVisualStyleBackColor = true;
            ButtonNumber7.Click += ButtonNumber7_Click;
            // 
            // ButtonNumber8
            // 
            ButtonNumber8.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonNumber8.Location = new Point(188, 264);
            ButtonNumber8.Name = "ButtonNumber8";
            ButtonNumber8.Size = new Size(94, 29);
            ButtonNumber8.TabIndex = 24;
            ButtonNumber8.Text = "8";
            ButtonNumber8.UseVisualStyleBackColor = true;
            ButtonNumber8.Click += ButtonNumber8_Click;
            // 
            // ButtonNumber9
            // 
            ButtonNumber9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonNumber9.Location = new Point(305, 263);
            ButtonNumber9.Name = "ButtonNumber9";
            ButtonNumber9.Size = new Size(94, 29);
            ButtonNumber9.TabIndex = 25;
            ButtonNumber9.Text = "9";
            ButtonNumber9.UseVisualStyleBackColor = true;
            ButtonNumber9.Click += ButtonNumber9_Click;
            // 
            // ButtonNumber0
            // 
            ButtonNumber0.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonNumber0.Location = new Point(188, 414);
            ButtonNumber0.Name = "ButtonNumber0";
            ButtonNumber0.Size = new Size(94, 29);
            ButtonNumber0.TabIndex = 26;
            ButtonNumber0.Text = "0";
            ButtonNumber0.UseVisualStyleBackColor = true;
            ButtonNumber0.Click += ButtonNumber0_Click;
            // 
            // ButtonPoint
            // 
            ButtonPoint.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold);
            ButtonPoint.Location = new Point(305, 413);
            ButtonPoint.Name = "ButtonPoint";
            ButtonPoint.Size = new Size(94, 29);
            ButtonPoint.TabIndex = 27;
            ButtonPoint.Text = ".";
            ButtonPoint.UseVisualStyleBackColor = true;
            ButtonPoint.Click += ButtonPoint_Click;
            // 
            // ButtonMeter
            // 
            ButtonMeter.BackColor = Color.LightSteelBlue;
            ButtonMeter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonMeter.Location = new Point(75, 98);
            ButtonMeter.Name = "ButtonMeter";
            ButtonMeter.Size = new Size(54, 29);
            ButtonMeter.TabIndex = 28;
            ButtonMeter.Text = "m";
            ButtonMeter.UseVisualStyleBackColor = false;
            ButtonMeter.Click += ButtonMeter_Click;
            // 
            // ButtonMillimeter
            // 
            ButtonMillimeter.BackColor = Color.LightSteelBlue;
            ButtonMillimeter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonMillimeter.Location = new Point(135, 98);
            ButtonMillimeter.Name = "ButtonMillimeter";
            ButtonMillimeter.Size = new Size(56, 29);
            ButtonMillimeter.TabIndex = 29;
            ButtonMillimeter.Text = "mm";
            ButtonMillimeter.UseVisualStyleBackColor = false;
            ButtonMillimeter.Click += ButtonMillimeter_Click;
            // 
            // ButtonCentimeters
            // 
            ButtonCentimeters.BackColor = Color.LightSteelBlue;
            ButtonCentimeters.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonCentimeters.Location = new Point(197, 98);
            ButtonCentimeters.Name = "ButtonCentimeters";
            ButtonCentimeters.Size = new Size(54, 29);
            ButtonCentimeters.TabIndex = 30;
            ButtonCentimeters.Text = "cm";
            ButtonCentimeters.UseVisualStyleBackColor = false;
            ButtonCentimeters.Click += ButtonCentimeters_Click;
            // 
            // ButtonKilometers
            // 
            ButtonKilometers.BackColor = Color.LightSteelBlue;
            ButtonKilometers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonKilometers.Location = new Point(257, 98);
            ButtonKilometers.Name = "ButtonKilometers";
            ButtonKilometers.Size = new Size(54, 29);
            ButtonKilometers.TabIndex = 31;
            ButtonKilometers.Text = "km";
            ButtonKilometers.UseVisualStyleBackColor = false;
            ButtonKilometers.Click += ButtonKilometers_Click;
            // 
            // ButtonExit
            // 
            ButtonExit.BackColor = Color.DarkOrange;
            ButtonExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonExit.Location = new Point(493, 12);
            ButtonExit.Name = "ButtonExit";
            ButtonExit.Size = new Size(97, 29);
            ButtonExit.TabIndex = 32;
            ButtonExit.Text = "Exit";
            ButtonExit.UseVisualStyleBackColor = false;
            ButtonExit.Click += ButtonExit_Click;
            // 
            // ButtonLiter
            // 
            ButtonLiter.BackColor = Color.LightSteelBlue;
            ButtonLiter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonLiter.Location = new Point(75, 168);
            ButtonLiter.Name = "ButtonLiter";
            ButtonLiter.Size = new Size(54, 29);
            ButtonLiter.TabIndex = 33;
            ButtonLiter.Text = "l";
            ButtonLiter.UseVisualStyleBackColor = false;
            ButtonLiter.Click += ButtonLiter_Click;
            // 
            // ButtonMilliLiters
            // 
            ButtonMilliLiters.BackColor = Color.LightSteelBlue;
            ButtonMilliLiters.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonMilliLiters.Location = new Point(135, 168);
            ButtonMilliLiters.Name = "ButtonMilliLiters";
            ButtonMilliLiters.Size = new Size(56, 29);
            ButtonMilliLiters.TabIndex = 34;
            ButtonMilliLiters.Text = "ml";
            ButtonMilliLiters.UseVisualStyleBackColor = false;
            ButtonMilliLiters.Click += ButtonMilliLiters_Click;
            // 
            // ButtonCentiliter
            // 
            ButtonCentiliter.BackColor = Color.LightSteelBlue;
            ButtonCentiliter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonCentiliter.Location = new Point(197, 168);
            ButtonCentiliter.Name = "ButtonCentiliter";
            ButtonCentiliter.Size = new Size(54, 29);
            ButtonCentiliter.TabIndex = 35;
            ButtonCentiliter.Text = "cl";
            ButtonCentiliter.UseVisualStyleBackColor = false;
            ButtonCentiliter.Click += ButtonCentiliter_Click;
            // 
            // ButtonKiloliter
            // 
            ButtonKiloliter.BackColor = Color.LightSteelBlue;
            ButtonKiloliter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonKiloliter.Location = new Point(257, 168);
            ButtonKiloliter.Name = "ButtonKiloliter";
            ButtonKiloliter.Size = new Size(54, 29);
            ButtonKiloliter.TabIndex = 36;
            ButtonKiloliter.Text = "kl";
            ButtonKiloliter.UseVisualStyleBackColor = false;
            ButtonKiloliter.Click += ButtonKiloliter_Click;
            // 
            // ButtonGram
            // 
            ButtonGram.BackColor = Color.LightSteelBlue;
            ButtonGram.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonGram.Location = new Point(75, 133);
            ButtonGram.Name = "ButtonGram";
            ButtonGram.Size = new Size(54, 29);
            ButtonGram.TabIndex = 37;
            ButtonGram.Text = "g";
            ButtonGram.UseVisualStyleBackColor = false;
            ButtonGram.Click += ButtonGram_Click;
            // 
            // ButtonMilligram
            // 
            ButtonMilligram.BackColor = Color.LightSteelBlue;
            ButtonMilligram.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonMilligram.Location = new Point(135, 133);
            ButtonMilligram.Name = "ButtonMilligram";
            ButtonMilligram.Size = new Size(56, 29);
            ButtonMilligram.TabIndex = 38;
            ButtonMilligram.Text = "mg";
            ButtonMilligram.UseVisualStyleBackColor = false;
            ButtonMilligram.Click += ButtonMilligram_Click;
            // 
            // ButtonCentigram
            // 
            ButtonCentigram.BackColor = Color.LightSteelBlue;
            ButtonCentigram.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonCentigram.Location = new Point(197, 133);
            ButtonCentigram.Name = "ButtonCentigram";
            ButtonCentigram.Size = new Size(54, 29);
            ButtonCentigram.TabIndex = 39;
            ButtonCentigram.Text = "cg";
            ButtonCentigram.UseVisualStyleBackColor = false;
            ButtonCentigram.Click += ButtonCentigram_Click;
            // 
            // ButtonKilogram
            // 
            ButtonKilogram.BackColor = Color.LightSteelBlue;
            ButtonKilogram.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ButtonKilogram.Location = new Point(257, 133);
            ButtonKilogram.Name = "ButtonKilogram";
            ButtonKilogram.Size = new Size(54, 29);
            ButtonKilogram.TabIndex = 40;
            ButtonKilogram.Text = "kg";
            ButtonKilogram.UseVisualStyleBackColor = false;
            ButtonKilogram.Click += ButtonKilogram_Click;
            // 
            // ButtonSaveJson
            // 
            ButtonSaveJson.Location = new Point(317, 98);
            ButtonSaveJson.Name = "ButtonSaveJson";
            ButtonSaveJson.Size = new Size(129, 29);
            ButtonSaveJson.TabIndex = 41;
            ButtonSaveJson.Text = "Save to JSON";
            ButtonSaveJson.UseVisualStyleBackColor = true;
            ButtonSaveJson.Click += ButtonSaveJson_Click;
            // 
            // ButtonSaveXml
            // 
            ButtonSaveXml.Location = new Point(317, 133);
            ButtonSaveXml.Name = "ButtonSaveXml";
            ButtonSaveXml.Size = new Size(129, 29);
            ButtonSaveXml.TabIndex = 42;
            ButtonSaveXml.Text = "Save to XML";
            ButtonSaveXml.UseVisualStyleBackColor = true;
            ButtonSaveXml.Click += ButtonSaveXml_Click;
            // 
            // ButtonSaveSQL
            // 
            ButtonSaveSQL.Location = new Point(317, 168);
            ButtonSaveSQL.Name = "ButtonSaveSQL";
            ButtonSaveSQL.Size = new Size(129, 29);
            ButtonSaveSQL.TabIndex = 43;
            ButtonSaveSQL.Text = "Save to SQL";
            ButtonSaveSQL.UseVisualStyleBackColor = true;
            ButtonSaveSQL.Click += ButtonSaveSQL_Click;
            // 
            // ButtonPercentage
            // 
            ButtonPercentage.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonPercentage.Location = new Point(71, 414);
            ButtonPercentage.Name = "ButtonPercentage";
            ButtonPercentage.Size = new Size(94, 29);
            ButtonPercentage.TabIndex = 44;
            ButtonPercentage.Text = "%";
            ButtonPercentage.UseVisualStyleBackColor = true;
            ButtonPercentage.Click += ButtonPercentage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(602, 514);
            Controls.Add(ButtonPercentage);
            Controls.Add(ButtonSaveSQL);
            Controls.Add(ButtonSaveXml);
            Controls.Add(ButtonSaveJson);
            Controls.Add(ButtonKilogram);
            Controls.Add(ButtonCentigram);
            Controls.Add(ButtonMilligram);
            Controls.Add(ButtonGram);
            Controls.Add(ButtonKiloliter);
            Controls.Add(ButtonCentiliter);
            Controls.Add(ButtonMilliLiters);
            Controls.Add(ButtonLiter);
            Controls.Add(ButtonExit);
            Controls.Add(ButtonKilometers);
            Controls.Add(ButtonCentimeters);
            Controls.Add(ButtonMillimeter);
            Controls.Add(ButtonMeter);
            Controls.Add(ButtonPoint);
            Controls.Add(ButtonNumber0);
            Controls.Add(ButtonNumber9);
            Controls.Add(ButtonNumber8);
            Controls.Add(ButtonNumber7);
            Controls.Add(ButtonNumber6);
            Controls.Add(ButtonNumber5);
            Controls.Add(ButtonNumber3);
            Controls.Add(ButtonNumber4);
            Controls.Add(ButtonNumber2);
            Controls.Add(textBoxExpression);
            Controls.Add(ButtonNumber1);
            Controls.Add(buttonCalculate);
            Controls.Add(ButtonClear);
            Controls.Add(ButtonDivide);
            Controls.Add(ButtonMultiply);
            Controls.Add(ButtomSubtract);
            Controls.Add(ButtomAdd);
            Cursor = Cursors.Hand;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button ButtomAdd;
        private Button ButtomSubtract;
        private Button ButtonMultiply;
        private Button ButtonDivide;
        private Button ButtonClear;
        private Button buttonCalculate;
        private Button ButtonNumber1;
        private TextBox textBoxExpression;
        private Button ButtonNumber2;
        private Button ButtonNumber4;
        private Button ButtonNumber3;
        private Button ButtonNumber5;
        private Button ButtonNumber6;
        private Button ButtonNumber7;
        private Button ButtonNumber8;
        private Button ButtonNumber9;
        private Button ButtonNumber0;
        private Button ButtonPoint;
        private Button ButtonMeter;
        private Button ButtonMillimeter;
        private Button ButtonCentimeters;
        private Button ButtonKilometers;
        private Button ButtonExit;
        private Button ButtonLiter;
        private Button ButtonMilliLiters;
        private Button ButtonCentiliter;
        private Button ButtonKiloliter;
        private Button ButtonGram;
        private Button ButtonMilligram;
        private Button ButtonCentigram;
        private Button ButtonKilogram;
        private Button ButtonSaveJson;
        private Button ButtonSaveXml;
        private Button ButtonSaveSQL;
        private Button ButtonPercentage;
    }
}
