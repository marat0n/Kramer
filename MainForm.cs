using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kramer
{
    public partial class MainForm : Form
    {
        private IEnumerable<Control> _inputBoxes;


        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            _inputBoxes = Controls.OfType<Control>().Where(x => x is TextBox);
        }


        private bool ValidateInputs()
        {
            foreach (var inputBox in _inputBoxes)
            {
                string inputText = inputBox.Text.Trim();
                if (!int.TryParse(inputText, out int parsedInput) || inputText == string.Empty)
                {
                    return false;
                }
            }

            return true;
        }


        private string CalculateKramer()
        {
            List<int> matrixData = (from item in _inputBoxes
                                    where item.Name[0] == 'A'
                                    select int.Parse(item.Text)).ToList();
            matrixData.Reverse();
            
            List<int> freeMembers = (from item in _inputBoxes
                                     where item.Name.Contains("FreeMember")
                                     select int.Parse(item.Text)).ToList();
            freeMembers.Reverse();

            Matrix3x3 matrix = new Matrix3x3(matrixData);
            decimal systemDet = matrix.Determinant;

            matrix.ReplaceColumn(0, freeMembers);
            decimal xDet = matrix.Determinant;
            
            matrix = new Matrix3x3(matrixData);
            matrix.ReplaceColumn(1, freeMembers);
            decimal yDet = matrix.Determinant;
            
            matrix = new Matrix3x3(matrixData);
            matrix.ReplaceColumn(2, freeMembers);
            decimal zDet = matrix.Determinant;
            
            if (systemDet != 0)
            {
                decimal x = xDet / systemDet;
                decimal y = yDet / systemDet;
                decimal z = zDet / systemDet;

                return $"Система линейных уравнений имеет единственное решение\nx: {x};  y: {y};  z: {z}.";
            }
            else
            {
                if (xDet == 0 || yDet == 0 || zDet == 0) 
                    return "Система линейных уравнений имеет бесчисленное множество решений";
                return "Система линейных уравнений не имеет решений";
            }
        }


        private void GetAnswer(object sender, MouseEventArgs e)
        {
            string answer = string.Empty;

            if (!ValidateInputs())
            {
                answer = "Все входные поля должны быть заполнены и включать в себя только числа!";
            }
            else
            {
                answer = CalculateKramer();
            }

            ResultLabel.Text = answer;
        }

        private void ClearResult(object sender, EventArgs e)
        {
            ResultLabel.Text = string.Empty;
        }
    }
}
