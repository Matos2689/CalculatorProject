using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CalculatorMethods.BusinessLogic;
using CalculatorMethods.Contracts;
using CalculatorMethods.Persistance;
using UnitsNet;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private static readonly string JsonPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SaveMathlog.json");

        private static readonly string XmlPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SaveMathlog.xml");

        // Connection string to SQL
        private const string ConnStr =
            "Server=.;Database=SQL_Calculator_DB;Trusted_Connection=True;Encrypt=False;";

        private readonly Calculator _calculator = new();

        public Form1()
        {
            InitializeComponent();
            textBoxExpression.Text = "";
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ButtomAdd_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText(" + ");

        }
        private void ButtomSubtract_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText(" - ");
        }

        private void ButtonMultiply_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText(" * ");
        }

        private void ButtonDivide_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText(" / ");
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxExpression.Clear();
        }

        private void textBoxExpression_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonClearBox_Click(object sender, EventArgs e)
        {
            textBoxExpression.Clear();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            var expression = textBoxExpression.Text.Trim();

            if (string.IsNullOrEmpty(expression))
            {
                MessageBox.Show("Please enter a valid expression.");
                return;
            }

            try
            {
                var logItem = _calculator.Calculate(expression);
                textBoxExpression.Clear();

                if (logItem.Type == MathLogTypes.NumericBased)
                    textBoxExpression.Text = logItem.NumericResult.ToString().Replace(" ", "");
                else
                    textBoxExpression.Text = logItem.QuantityResult.ToString().Replace(" ", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void ButtonNumber1_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("1");
        }

        private void ButtonNumber2_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("2");
        }

        private void ButtonNumber3_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("3");
        }

        private void ButtonNumber4_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("4");
        }

        private void ButtonNumber5_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("5");
        }

        private void ButtonNumber6_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("6");
        }

        private void ButtonNumber7_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("7");
        }

        private void ButtonNumber8_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("8");
        }

        private void ButtonNumber9_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("9");
        }

        private void ButtonNumber0_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("0");
        }

        private void ButtonPoint_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText(".");
        }

        private void ButtonMeter_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("m");
        }

        private void ButtonMillimeter_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("mm");
        }

        private void ButtonCentimeters_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("cm");
        }

        private void ButtonKilometers_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("km");
        }

        private void ButtonLiter_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("l");
        }

        private void ButtonMilliLiters_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("ml");
        }

        private void ButtonCentiliter_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("cl");
        }

        private void ButtonKiloliter_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("kl");
        }

        private void ButtonGram_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("g");
        }

        private void ButtonMilligram_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("mg");
        }

        private void ButtonCentigram_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("cg");
        }

        private void ButtonKilogram_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("kg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonSaveJson_Click(object sender, EventArgs e)
        {
            var jsonRepo = new JsonRepositoryManager();
            try
            {
                jsonRepo.Save(_calculator.MathLog, JsonPath);
                MessageBox.Show("History saved to SaveMathlog.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to JSON: {ex.Message}");
            }
        }

        private void ButtonSaveXml_Click(object sender, EventArgs e)
        {
            var xmlRepo = new XmlRepositoryManager();

            try
            {
                xmlRepo.Save(_calculator.MathLog, XmlPath);
                MessageBox.Show("History saved to SaveMathlog.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to XML: {ex.Message}");
            }
        }

        private void ButtonSaveSQL_Click(object sender, EventArgs e)
        {
            var sqlRepo = new AdoNetRepositoryManager(ConnStr);
            try
            {
                sqlRepo.Save(_calculator.MathLog, null!);
                MessageBox.Show("History saved to SQL database.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to SQL: {ex.Message}");
            }
        }

        private void ButtonPercentage_Click(object sender, EventArgs e)
        {
            textBoxExpression.AppendText("% ");
        }

        private void ButtonBackSpaces_Click(object sender, EventArgs e)
        {
            var tb = textBoxExpression;
            if (tb.TextLength == 0) return;

            // remove o último char
            tb.Text = tb.Text.Substring(0, tb.TextLength - 1);

            // reposiciona o caret no fim
            tb.SelectionStart = tb.Text.Length;
            tb.SelectionLength = 0;
            tb.Focus();
        }
    }
}
