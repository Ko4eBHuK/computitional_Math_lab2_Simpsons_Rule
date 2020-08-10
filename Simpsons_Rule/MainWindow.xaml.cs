using System;
using System.Windows;
using System.Windows.Controls;

namespace Simpsons_Rule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void SolveTheIntegral(object sender, RoutedEventArgs e)
        {
            double upperLimitValue = Double.Parse(UpperLimit.Text.Replace(".", ","));
            double lowerLimitValue = Double.Parse(LowerLimit.Text.Replace(".", ","));
            double accuracy = Math.Abs(Double.Parse(Accuracy.Text.Replace(".", ",")));
            int functionIndex = FunctionsList.SelectedIndex;

            OutputConsole.Text += "Выбрана функция f(x)=";
            switch (functionIndex)
            {
                case 0:
                    OutputConsole.Text += "x^2+14x-4";
                    break;
                case 1:
                    OutputConsole.Text += "e^(12x)";
                    break;
                case 2:
                    OutputConsole.Text += "x-0.158";
                    break;
                case 3:
                    OutputConsole.Text += "7sin(x-9)";
                    break;
            }
            OutputConsole.Text += "\n";

            var solution = new SimpsonsRuleMethod(lowerLimitValue, upperLimitValue, accuracy, functionIndex);

            OutputConsole.Text += "Приближённое значение интеграла равно ";
            OutputConsole.Text += Math.Round(solution.IntegralValueN, 8);
            OutputConsole.Text += "\n";

            OutputConsole.Text += "Погрешность решения равна ";
            OutputConsole.Text += Math.Round(solution.CalculatedAccuracy, 8);
            OutputConsole.Text += "\n";

            if(solution.CalculatedAccuracy <= accuracy)
            {
                OutputConsole.Text += "Результат достигнут с количеством шагов равным ";
                OutputConsole.Text += solution.StepCount;
                OutputConsole.Text += "\n";
            }
            else
            {
                OutputConsole.Text += "Количество шагов для достижения требуемой точности\nпревышает миллион.\n";
                OutputConsole.Text += "Метод Симпсона не является оптимальным для данного\nинтеграла на данных пределах";
                OutputConsole.Text += "\n";
            }


            OutputConsole.Text += "Цена шага равна ";
            OutputConsole.Text += solution.StepValue;

            OutputConsole.Text += "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
            ConsoleScrollViewer.ScrollToEnd();
        }

        private void LimitsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            double reserveLimitValue;
            TextBox checkedTextBox = (TextBox)sender;

            try
            {
                reserveLimitValue = Double.Parse(checkedTextBox.Text.Replace(".",","));
            }
            catch
            {
                checkedTextBox.Text = "0";
                MessageBox.Show("Значение для предела выставленно некорректно.\nОно будет сброшено в ноль.");
            }

        }

        private void Backhitch(object sender, RoutedEventArgs e)
        {
            OutputConsole.Text = "";
            UpperLimit.Text = "1";
            LowerLimit.Text = "0";
            FunctionsList.SelectedIndex = 0;
        }
    }
}
