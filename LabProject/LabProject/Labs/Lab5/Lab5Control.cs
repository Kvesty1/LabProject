using LabProject.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LabProject.Labs
{
    public partial class Lab5Control : UserControl, ILabModule
    {
        private const double GOLDEN_RATIO = 0.61803398874989484820458683436564;

        public int LabNumber => 5;
        public string LabTitle => "Лабораторная работа №5: Вычисление определенного интеграла";

        public Lab5Control()
        {
            InitializeComponent();
            InitializeUI();
            this.Dock = DockStyle.Fill;

            // Устанавливаем начальные значения
            txtFunction.Text = "x^2";
            txtA.Text = "0";
            txtB.Text = "2";
            txtEpsilon.Text = "0.0001";
            chkRectangles.Checked = true;
            chkTrapezoidal.Checked = true;
            chkSimpson.Checked = true;
        }

        private void InitializeUI()
        {
            // Подключение обработчиков событий
            calculateMenuItem.Click += CalculateMenuItem_Click;
            clearMenuItem.Click += ClearMenuItem_Click;
            generateDataMenuItem.Click += GenerateTestDataToolStripMenuItem_Click;

            // Настройка графика
            chartMain.Paint += ChartMain_Paint;
        }

        private void CalculateMenuItem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                string function = txtFunction.Text.Trim();
                double a = ParseDouble(txtA.Text);
                double b = ParseDouble(txtB.Text);
                double epsilon = ParseDouble(txtEpsilon.Text);

                // Очищаем предыдущие результаты
                txtResults.Clear();
                chartMain.Series.Clear();
                chartMain.ChartAreas[0].AxisX.Minimum = a;
                chartMain.ChartAreas[0].AxisX.Maximum = b;

                // Рисуем функцию
                PlotFunction(function, a, b);

                // Выполняем вычисления по выбранным методам
                if (chkRectangles.Checked)
                {
                    ExecuteMethod("Метод прямоугольников", function, a, b, epsilon,
                        (f, x1, x2, n) => RectangleMethod(f, x1, x2, n));
                }

                if (chkTrapezoidal.Checked)
                {
                    ExecuteMethod("Метод трапеций", function, a, b, epsilon,
                        (f, x1, x2, n) => TrapezoidalMethod(f, x1, x2, n));
                }

                if (chkSimpson.Checked)
                {
                    ExecuteMethod("Метод Симпсона", function, a, b, epsilon,
                        (f, x1, x2, n) => SimpsonMethod(f, x1, x2, n));
                }

                DisplayResults();
                UpdateStatus("Вычисление завершено успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Ошибка при вычислении");
            }
        }

        private double ParseDouble(string text)
        {
            return double.Parse(text.Replace(",", "."), CultureInfo.InvariantCulture);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFunction.Text))
            {
                MessageBox.Show("Введите функцию", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFunction.Focus();
                return false;
            }

            if (!double.TryParse(txtA.Text.Replace(",", "."), NumberStyles.Any,
                CultureInfo.InvariantCulture, out double a))
            {
                MessageBox.Show("Некорректное значение нижней границы", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtA.Focus();
                return false;
            }

            if (!double.TryParse(txtB.Text.Replace(",", "."), NumberStyles.Any,
                CultureInfo.InvariantCulture, out double b))
            {
                MessageBox.Show("Некорректное значение верхней границы", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtB.Focus();
                return false;
            }

            if (a >= b)
            {
                MessageBox.Show("Нижняя граница должна быть меньше верхней", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!double.TryParse(txtEpsilon.Text.Replace(",", "."), NumberStyles.Any,
                CultureInfo.InvariantCulture, out double epsilon) || epsilon <= 0)
            {
                MessageBox.Show("Точность должна быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEpsilon.Focus();
                return false;
            }

            if (!chkRectangles.Checked && !chkTrapezoidal.Checked && !chkSimpson.Checked)
            {
                MessageBox.Show("Выберите хотя бы один метод вычисления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ExecuteMethod(string methodName, string function, double a, double b,
            double epsilon, Func<Func<double, double>, double, double, int, double> integrationMethod)
        {
            try
            {
                UpdateStatus($"Выполняется {methodName}...");
                Application.DoEvents();

                Func<double, double> f = x => EvaluateFunction(function, x);
                double fa = f(a);
                double fb = f(b);

                if (double.IsInfinity(fa) || double.IsNaN(fa) ||
                    double.IsInfinity(fb) || double.IsNaN(fb))
                {
                    throw new ArgumentException("Функция не определена на границах интервала");
                }

                int optimalN = FindOptimalN(f, a, b, epsilon, integrationMethod);
                double result = integrationMethod(f, a, b, optimalN);

                // Добавляем результат в историю
                resultsHistory.Add(new IntegrationResult
                {
                    MethodName = methodName,
                    Result = result,
                    OptimalN = optimalN,
                    Epsilon = epsilon
                });

                // Визуализируем разбиение
                VisualizePartitions(methodName, f, a, b, optimalN);
            }
            catch (Exception ex)
            {
                resultsHistory.Add(new IntegrationResult
                {
                    MethodName = methodName,
                    Result = double.NaN,
                    OptimalN = 0,
                    Epsilon = epsilon,
                    Error = ex.Message
                });
            }
        }

        private int FindOptimalN(Func<double, double> f, double a, double b,
            double epsilon, Func<Func<double, double>, double, double, int, double> integrationMethod)
        {
            int n = 4;
            double prevResult = integrationMethod(f, a, b, n);
            double currentResult;
            List<int> history = new List<int> { n };

            do
            {
                n *= 2;
                currentResult = integrationMethod(f, a, b, n);
                double error = Math.Abs(currentResult - prevResult);

                history.Add(n);

                if (error < epsilon)
                    break;

                prevResult = currentResult;

                if (n > 1000000)
                    break;
            } while (true);

            // Добавляем историю в результаты
            txtResults.AppendText($"История подбора N для:\r\n");
            txtResults.AppendText($"{string.Join(" → ", history)}\r\n\r\n");

            return n;
        }

        private double RectangleMethod(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                double x = a + i * h + h / 2;
                sum += f(x);
            }
            return sum * h;
        }

        private double TrapezoidalMethod(Func<double, double> f, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = (f(a) + f(b)) / 2;
            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += f(x);
            }
            return sum * h;
        }

        private double SimpsonMethod(Func<double, double> f, double a, double b, int n)
        {
            if (n % 2 != 0) n++;
            double h = (b - a) / n;
            double sum = f(a) + f(b);
            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += f(x) * (i % 2 == 0 ? 2 : 4);
            }
            return sum * h / 3;
        }

        private double EvaluateFunction(string function, double x)
        {
            try
            {
                return SimpleEvaluate(function, x);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Ошибка в функции: {ex.Message}");
            }
        }

        private double SimpleEvaluate(string expression, double xValue)
        {
            try
            {
                expression = expression.Trim().ToLower();
                if (expression == "x") return xValue;
                if (expression == "x^2") return xValue * xValue;
                if (expression == "x^3") return xValue * xValue * xValue;
                if (expression == "x^4") return Math.Pow(xValue, 4);
                if (expression == "x^x") return Math.Pow(xValue, xValue);
                if (expression == "2^x") return Math.Pow(2, xValue);
                if (expression == "e^x") return Math.Exp(xValue);
                if (expression == "exp(x)") return Math.Exp(xValue);
                if (expression == "sin(x)") return Math.Sin(xValue);
                if (expression == "cos(x)") return Math.Cos(xValue);
                if (expression == "tan(x)") return Math.Tan(xValue);
                if (expression == "ln(x)") return xValue > 0 ? Math.Log(xValue) : throw new ArgumentException("ln(x) не определен для x <= 0");
                if (expression == "log(x)") return xValue > 0 ? Math.Log10(xValue) : throw new ArgumentException("log(x) не определен для x <= 0");
                if (expression == "sqrt(x)") return xValue >= 0 ? Math.Sqrt(xValue) : throw new ArgumentException("sqrt(x) не определен для x < 0");
                if (expression == "abs(x)") return Math.Abs(xValue);
                if (expression == "1/x") return xValue != 0 ? 1 / xValue : throw new ArgumentException("1/x не определен при x=0");

                string expr = PrepareExpression(expression, xValue);
                return CalculateExpression(expr);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Не удалось вычислить '{expression}' при x={xValue}: {ex.Message}");
            }
        }

        private string PrepareExpression(string expression, double xValue)
        {
            string expr = expression.ToLower();
            expr = expr.Replace("^", "**");
            expr = expr.Replace("pi", Math.PI.ToString(CultureInfo.InvariantCulture))
                      .Replace("e", Math.E.ToString(CultureInfo.InvariantCulture));
            expr = expr.Replace("x", xValue.ToString(CultureInfo.InvariantCulture));
            expr = expr.Replace("sin(", "Math.Sin(")
                      .Replace("cos(", "Math.Cos(")
                      .Replace("tan(", "Math.Tan(")
                      .Replace("exp(", "Math.Exp(")
                      .Replace("ln(", "Math.Log(")
                      .Replace("log(", "Math.Log10(")
                      .Replace("sqrt(", "Math.Sqrt(")
                      .Replace("abs(", "Math.Abs(");
            expr = AddMultiplicationSigns(expr);
            return expr;
        }

        private string AddMultiplicationSigns(string expr)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            for (int i = 0; i < expr.Length; i++)
            {
                result.Append(expr[i]);
                if (i < expr.Length - 1)
                {
                    char current = expr[i];
                    char next = expr[i + 1];
                    if (char.IsDigit(current) && (next == '(' || char.IsLetter(next)))
                    {
                        result.Append('*');
                    }
                    else if (current == ')' && char.IsDigit(next))
                    {
                        result.Append('*');
                    }
                }
            }
            return result.ToString();
        }

        private double CalculateExpression(string expr)
        {
            try
            {
                DataTable table = new DataTable();
                return Convert.ToDouble(table.Compute(expr, ""));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Ошибка вычисления: {ex.Message}");
            }
        }

        private void PlotFunction(string function, double a, double b)
        {
            Series series = new Series
            {
                Name = "f(x)",
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2
            };

            int points = 200;
            double step = (b - a) / points;
            List<double> validY = new List<double>();

            for (int i = 0; i <= points; i++)
            {
                double x = a + i * step;
                try
                {
                    double y = EvaluateFunction(function, x);
                    if (!double.IsInfinity(y) && !double.IsNaN(y))
                    {
                        series.Points.AddXY(x, y);
                        validY.Add(y);
                    }
                }
                catch
                {
                }
            }

            if (series.Points.Count > 0)
            {
                if (validY.Count > 0)
                {
                    double minY = double.MaxValue;
                    double maxY = double.MinValue;
                    foreach (double y in validY)
                    {
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                    double range = maxY - minY;
                    if (range > 0)
                    {
                        chartMain.ChartAreas[0].AxisY.Minimum = minY - range * 0.1;
                        chartMain.ChartAreas[0].AxisY.Maximum = maxY + range * 0.1;
                    }
                }
                chartMain.Series.Add(series);
            }
            else
            {
                MessageBox.Show($"Не удалось построить график функции {function} на интервале [{a}, {b}]",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void VisualizePartitions(string methodName, Func<double, double> f, double a, double b, int n)
        {
            Color[] colors = { Color.Red, Color.Green, Color.Purple, Color.Orange };
            Color color = colors[(chartMain.Series.Count - 1) % colors.Length];

            Series series = new Series
            {
                Name = $"{methodName} (n={n})",
                ChartType = SeriesChartType.Point,
                Color = color,
                MarkerSize = 5,
                MarkerStyle = MarkerStyle.Circle
            };

            double h = (b - a) / n;
            for (int i = 0; i <= n; i++)
            {
                double x = a + i * h;
                try
                {
                    double y = f(x);
                    if (!double.IsInfinity(y) && !double.IsNaN(y))
                    {
                        series.Points.AddXY(x, y);
                    }
                }
                catch
                {
                }
            }

            chartMain.Series.Add(series);
        }

        private void DisplayResults()
        {
            txtResults.AppendText("=== РЕЗУЛЬТАТЫ ВЫЧИСЛЕНИЯ ===\r\n\r\n");
            foreach (var result in resultsHistory)
            {
                txtResults.AppendText($"{result.MethodName}:\r\n");
                if (!string.IsNullOrEmpty(result.Error))
                {
                    txtResults.AppendText($"ОШИБКА: {result.Error}\r\n");
                }
                else
                {
                    txtResults.AppendText($"Значение интеграла: {result.Result:F8}\r\n");
                    txtResults.AppendText($"Число разбиений: {result.OptimalN}\r\n");
                    txtResults.AppendText($"Точность: {result.Epsilon}\r\n");
                }
                txtResults.AppendText("\r\n");
            }
        }

        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            txtFunction.Text = "x^2";
            txtA.Text = "0";
            txtB.Text = "2";
            txtEpsilon.Text = "0.0001";
            chkRectangles.Checked = true;
            chkTrapezoidal.Checked = true;
            chkSimpson.Checked = true;
            txtResults.Clear();
            chartMain.Series.Clear();
            resultsHistory.Clear();
            chartMain.ChartAreas[0].AxisX.Minimum = 0;
            chartMain.ChartAreas[0].AxisX.Maximum = 2;
            chartMain.ChartAreas[0].AxisY.Minimum = 0;
            chartMain.ChartAreas[0].AxisY.Maximum = 5;
            UpdateStatus("Поля очищены. Готово к работе");
        }

        private void GenerateTestDataToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Random rand = new Random();
            string[] testFunctions = {
                "x^2",
                "sin(x)",
                "cos(x)",
                "2*x+1",
                "x/2",
                "x^3",
                "1/(x+1)",
                "sqrt(x+1)",
                "exp(-x)",
                "x^x"
            };

            int index = rand.Next(testFunctions.Length);
            txtFunction.Text = testFunctions[index];

            if (txtFunction.Text == "1/(x+1)")
            {
                txtA.Text = "0";
                txtB.Text = (rand.NextDouble() * 3 + 1).ToString("F2", CultureInfo.InvariantCulture);
            }
            else if (txtFunction.Text.Contains("sqrt"))
            {
                txtA.Text = "-1";
                txtB.Text = (rand.NextDouble() * 4).ToString("F2", CultureInfo.InvariantCulture);
            }
            else if (txtFunction.Text == "x^x")
            {
                txtA.Text = "0.1";
                txtB.Text = (rand.NextDouble() * 2 + 0.5).ToString("F2", CultureInfo.InvariantCulture);
            }
            else if (txtFunction.Text.Contains("sin") || txtFunction.Text.Contains("cos"))
            {
                txtA.Text = "0";
                txtB.Text = Math.PI.ToString("F2", CultureInfo.InvariantCulture);
            }
            else if (txtFunction.Text == "exp(-x)")
            {
                txtA.Text = "0";
                txtB.Text = (rand.NextDouble() * 3 + 1).ToString("F2", CultureInfo.InvariantCulture);
            }
            else
            {
                txtA.Text = (rand.NextDouble() * 2).ToString("F2", CultureInfo.InvariantCulture);
                txtB.Text = (double.Parse(txtA.Text, CultureInfo.InvariantCulture) + rand.NextDouble() * 3 + 1).ToString("F2", CultureInfo.InvariantCulture);
            }

            txtEpsilon.Text = (Math.Pow(10, -rand.Next(3, 6))).ToString("E2", CultureInfo.InvariantCulture);
            UpdateStatus("Тестовые данные сгенерированы");
        }

        private void ChartMain_Paint(object? sender, PaintEventArgs e)
        {
            // Дополнительные настройки графика можно добавить здесь
        }

        private void UpdateStatus(string message)
        {
            // Обновляем статус в MainForm
            if (Parent?.Parent is Shell.MainForm mainForm)
            {
                mainForm.SetStatus(message);
            }
        }

        public void OnShow()
        {
            UpdateStatus("Лабораторная работа №5 активна");
        }

        public void OnHide()
        {
            // Сохранение состояния при необходимости
        }

        private List<IntegrationResult> resultsHistory = new List<IntegrationResult>();

        private class IntegrationResult
        {
            public string? MethodName { get; set; }
            public double Result { get; set; }
            public int OptimalN { get; set; }
            public double Epsilon { get; set; }
            public string? Error { get; set; }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Вычисление определенного интеграла численными методами\n\n" +
                     "В программе реализованы три метода вычисления определенного интеграла:\n\n" +

                     "1. Метод прямоугольников:\n" +
                     "   • Заменяет криволинейную трапецию на ступенчатую фигуру\n" +
                     "   • Значение интеграла приближается суммой площадей прямоугольников\n" +
                     "   • Точки вычисления функции находятся посередине каждого интервала\n" +
                     "   • Порядок точности: O(h²)\n\n" +

                     "2. Метод трапеций:\n" +
                     "   • Заменяет кривую ломаной линией\n" +
                     "   • Интеграл приближается суммой площадей трапеций\n" +
                     "   • Узлы вычисления функции находятся на границах интервалов\n" +
                     "   • Порядок точности: O(h²), но с меньшей константой ошибки\n\n" +

                     "3. Метод Симпсона (парабол):\n" +
                     "   • На каждом отрезке кривая аппроксимируется параболой\n" +
                     "   • Требует четного числа интервалов\n" +
                     "   • Самый точный из трех методов\n" +
                     "   • Порядок точности: O(h⁴)\n\n" +

                     "Поддерживаемые операции:\n" +
                     "• +, -, *, / - арифметические операции\n" +
                     "• ^ - возведение в степень\n" +
                     "• ( ) - скобки для группировки\n" +
                     "• sin(x), cos(x), tan(x) - тригонометрические функции\n" +
                     "• sqrt(x) - квадратный корень\n" +
                     "• exp(x) - экспонента\n" +
                     "• ln(x), log(x), log10(x) - логарифмы\n" +
                     "• abs(x) - модуль числа\n\n" +

                     "Примеры функций:\n" +
                     "• x^2\n" +
                     "• sin(x)\n" +
                     "• x/2 + 1\n" +
                     "• sqrt(x+1)\n" +
                     "• exp(-x)\n" +
                     "• 1/(x^2+1)\n\n" +

                     "Параметры:\n" +
                     "• a, b - границы интегрирования (a < b)\n" +
                     "• ε - точность вычислений (ε > 0)\n\n";

            MessageBox.Show(message, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}