using LabProject.Core;
using NCalc;
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
    public partial class Lab3Control : UserControl, ILabModule
    {
        protected const double GOLDEN_RATIO = 0.61803398874989484820458683436564;
        public int LabNumber => 3;
        public string LabTitle => "Лабораторная работа №3: Метод золотого сечения";

        public Lab3Control()
        {
            InitializeComponent();
            InitializeUI();
            this.Dock = DockStyle.Fill;

            // Значения по умолчанию
            txtA.Text = "0";
            txtB.Text = "5";
            txtEpsilon.Text = "0.001";
            txtFunction.Text = "x^2 - 4*x + 4";
            radioMin.Checked = true;
        }

        private void InitializeUI()
        {
            // Подключение обработчиков событий
            calculateMenuItem.Click += CalculateMenuItem_Click;
            clearMenuItem.Click += ClearMenuItem_Click;
            aboutMenuItem3.Click += AboutMenuItem_Click;

            // Настройка графика
            graphPictureBox.Paint += GraphPictureBox_Paint;
        }

        private void CalculateMenuItem_Click(object? sender, EventArgs e)
        {
            DoCalculate();
        }

        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            ClearAll();
        }

        private void AboutMenuItem_Click(object? sender, EventArgs e)
        {
            string message = "Метод золотого сечения для поиска экстремумов функции\n" +
                             "Поддерживаемые операции:\n" +
                             "• +, -, *, / - арифметические операции\n" +
                             "• ^ - возведение в степень (только целые степени)\n" +
                             "• ( ) - скобки для группировки\n" +
                             "Примеры функций:\n" +
                             "• x^2 - 4*x + 4\n" +
                             "• sin(x) + cos(x)\n" +
                             "• x^3 - 6*x^2 + 9*x + 2\n" +
                             "Золотое сечение: φ = (√5 - 1)/2 ≈ 0.618";

            MessageBox.Show(message, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DoCalculate()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFunction.Text))
                {
                    MessageBox.Show("Введите функцию f(x)", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(txtA.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double a))
                {
                    MessageBox.Show("Некорректное значение a", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(txtB.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double b))
                {
                    MessageBox.Show("Некорректное значение b", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(txtEpsilon.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double epsilon))
                {
                    MessageBox.Show("Некорректное значение точности ε", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (epsilon <= 0)
                {
                    MessageBox.Show("Точность должна быть положительным числом", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (a >= b)
                {
                    MessageBox.Show("a должно быть меньше b", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool findMinimum = radioMin.Checked;
                Func<double, double> function = ParseFunction(txtFunction.Text);

                // Проверка функции на корректность
                try
                {
                    double test1 = function(a);
                    double test2 = function(b);
                    double test3 = function((a + b) / 2);

                    if (double.IsNaN(test1) || double.IsInfinity(test1) ||
                        double.IsNaN(test2) || double.IsInfinity(test2) ||
                        double.IsNaN(test3) || double.IsInfinity(test3))
                    {
                        throw new Exception("Функция возвращает нечисловое значение");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка в функции: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double extremumX;
                double extremumY;

                if (findMinimum)
                {
                    extremumX = GoldenRatioMin(function, a, b, epsilon);
                }
                else
                {
                    extremumX = GoldenRatioMax(function, a, b, epsilon);
                }

                extremumY = function(extremumX);

                string extremumType = findMinimum ? "минимума" : "максимума";
                txtResult.Text = $"Найденный {extremumType}:\n" +
                                 $"x = {extremumX:F6}\n" +
                                 $"f(x) = {extremumY:F6}\n" +
                                 $"Точность: ε = {epsilon}\n" +
                                 $"Интервал: [{a}, {b}]\n" +
                                 $"Метод: Золотое сечение (φ = {GOLDEN_RATIO:F4})";

                // Обновляем статус в главной форме
                UpdateStatus($"Расчет завершен: {extremumType} найден");

                // Перерисовываем график
                graphPictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Ошибка вычислений");
            }
        }

        private void ClearAll()
        {
            txtA.Text = "0";
            txtB.Text = "5";
            txtEpsilon.Text = "0.001";
            txtFunction.Text = "x^2 - 4*x + 4";
            radioMin.Checked = true;
            txtResult.Clear();

            // Очищаем график
            graphPictureBox.Invalidate();
            UpdateStatus("Данные очищены");
        }

        private void UpdateStatus(string message)
        {
            // Обновляем статус в MainForm
            if (Parent?.Parent is Shell.MainForm mainForm)
            {
                mainForm.SetStatus(message);
            }
        }

        private Func<double, double> ParseFunction(string functionString)
        {
            return x =>
            {
                try
                {
                    string expression = PrepareExpression(functionString, x);
                    object result = new DataTable().Compute(expression, null);

                    if (result is double) return (double)result;
                    if (result is decimal) return (double)(decimal)result;
                    if (result is int) return (double)(int)result;

                    return Convert.ToDouble(result);
                }
                catch
                {
                    throw new ArgumentException("Некорректное выражение функции");
                }
            };
        }

        private string PrepareExpression(string expression, double xValue)
        {
            string result = expression;
            result = result.Replace(" ", "");
            result = ConvertPowerToMultiplication(result);
            result = result.Replace("x", xValue.ToString(System.Globalization.CultureInfo.InvariantCulture));
            result = Regex.Replace(result, @"(\d),(\d)", "$1.$2");
            return result;
        }

        private string ConvertPowerToMultiplication(string expression)
        {
            string result = expression;
            string pattern = @"([a-zA-Z0-9\.\(\)]+)\^(\d+)";
            MatchCollection matches = Regex.Matches(result, pattern);
            List<Match> matchList = new List<Match>();

            foreach (Match match in matches)
            {
                matchList.Add(match);
            }

            for (int i = matchList.Count - 1; i >= 0; i--)
            {
                Match match = matchList[i];
                string baseExpr = match.Groups[1].Value;
                int exponent = int.Parse(match.Groups[2].Value);
                string multiplication = "";

                for (int j = 0; j < exponent; j++)
                {
                    if (j > 0) multiplication += "*";
                    multiplication += baseExpr;
                }

                if (exponent == 1)
                {
                    multiplication = baseExpr;
                }
                else if (exponent == 0)
                {
                    multiplication = "1";
                }

                result = result.Remove(match.Index, match.Length);
                result = result.Insert(match.Index, multiplication);
            }

            return result;
        }

        private double GoldenRatioMin(Func<double, double> f, double a, double b, double epsilon)
        {
            double x1 = b - GOLDEN_RATIO * (b - a);
            double x2 = a + GOLDEN_RATIO * (b - a);
            double f1 = f(x1);
            double f2 = f(x2);

            while (Math.Abs(b - a) > epsilon)
            {
                if (f1 < f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = b - GOLDEN_RATIO * (b - a);
                    f1 = f(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + GOLDEN_RATIO * (b - a);
                    f2 = f(x2);
                }
            }

            return (a + b) / 2;
        }

        private double GoldenRatioMax(Func<double, double> f, double a, double b, double epsilon)
        {
            double x1 = b - GOLDEN_RATIO * (b - a);
            double x2 = a + GOLDEN_RATIO * (b - a);
            double f1 = f(x1);
            double f2 = f(x2);

            while (Math.Abs(b - a) > epsilon)
            {
                if (f1 > f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = b - GOLDEN_RATIO * (b - a);
                    f1 = f(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + GOLDEN_RATIO * (b - a);
                    f2 = f(x2);
                }
            }

            return (a + b) / 2;
        }

        private void GraphPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFunction.Text))
                return;

            try
            {
                if (double.TryParse(txtA.Text.Replace(",", "."),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double a) &&
                    double.TryParse(txtB.Text.Replace(",", "."),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double b) &&
                    double.TryParse(txtEpsilon.Text.Replace(",", "."),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double epsilon))
                {
                    bool findMinimum = radioMin.Checked;
                    Func<double, double> function = ParseFunction(txtFunction.Text);

                    // Построение графика
                    DrawGraph(e.Graphics, function, a, b, findMinimum);
                }
            }
            catch
            {
                // Ошибка в построении графика
                MessageBox.Show("Ошибка в построении графика", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void DrawGraph(Graphics g, Func<double, double> f, double a, double b, bool isMinimum)
        {
            double intervalA = a;
            double intervalB = b;

            double step = (intervalB - intervalA) / 200;
            double minY = double.MaxValue;
            double maxY = double.MinValue;
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            for (double x = intervalA; x <= intervalB; x += step)
            {
                try
                {
                    double y = f(x);
                    xValues.Add(x);
                    yValues.Add(y);

                    if (!double.IsNaN(y) && !double.IsInfinity(y))
                    {
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
                catch
                {
                    // Ошибка вычисления функции
                    MessageBox.Show("Ошибка вычисления функции", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Если значения не определены, устанавливаем диапазон по умолчанию
            if (double.IsInfinity(minY) || double.IsNaN(minY) ||
                double.IsInfinity(maxY) || double.IsNaN(maxY) ||
                Math.Abs(maxY - minY) < 1e-10)
            {
                minY = -10;
                maxY = 10;
            }

            double yRange = maxY - minY;
            minY -= yRange * 0.1;
            maxY += yRange * 0.1;
            yRange = maxY - minY;

            float scaleX = graphPictureBox.Width / (float)(b - a);
            float scaleY = graphPictureBox.Height / (float)yRange;

            // Рисуем оси координат
            Pen axisPen = new Pen(Color.Black, 1);
            float yZero = graphPictureBox.Height - (float)(-minY) * scaleY;

            if (yZero > 0 && yZero < graphPictureBox.Height)
            {
                g.DrawLine(axisPen, 0, yZero, graphPictureBox.Width, yZero);

                // Метки на оси X
                for (double x = a; x <= b; x += (b - a) / 10)
                {
                    float screenX = (float)(x - a) * scaleX;
                    g.DrawLine(axisPen, screenX, yZero - 3, screenX, yZero + 3);
                    g.DrawString(x.ToString("F1"), new Font("Arial", 7),
                        Brushes.Black, screenX - 10, yZero + 5);
                }
            }

            float xZero = (float)(-a) * scaleX;
            if (xZero > 0 && xZero < graphPictureBox.Width)
            {
                g.DrawLine(axisPen, xZero, 0, xZero, graphPictureBox.Height);

                // Метки на оси Y
                for (double y = minY; y <= maxY; y += yRange / 10)
                {
                    float screenY = graphPictureBox.Height - (float)(y - minY) * scaleY;
                    g.DrawLine(axisPen, xZero - 3, screenY, xZero + 3, screenY);
                    g.DrawString(y.ToString("F1"), new Font("Arial", 7),
                        Brushes.Black, xZero + 5, screenY - 7);
                }
            }

            // Рисуем график функции
            Pen graphPen = new Pen(Color.Blue, 2);
            PointF? prevPoint = null;

            for (int i = 0; i < xValues.Count; i++)
            {
                try
                {
                    double x = xValues[i];
                    double y = yValues[i];

                    if (double.IsNaN(y) || double.IsInfinity(y))
                    {
                        prevPoint = null;
                        continue;
                    }

                    float screenX = (float)(x - a) * scaleX;
                    float screenY = graphPictureBox.Height - (float)(y - minY) * scaleY;
                    PointF currentPoint = new PointF(screenX, screenY);

                    if (prevPoint.HasValue)
                    {
                        g.DrawLine(graphPen, prevPoint.Value, currentPoint);
                    }

                    prevPoint = currentPoint;
                }
                catch
                {
                    prevPoint = null;
                }
            }

            // Если есть точка экстремума, отображаем её
            try
            {
                double extremumX;
                double extremumY;

                if (double.TryParse(txtA.Text, out intervalA) &&
                    double.TryParse(txtB.Text, out intervalB) &&
                    double.TryParse(txtEpsilon.Text, out double epsilonValue))
                {
                    Func<double, double> function = ParseFunction(txtFunction.Text);

                    if (isMinimum)
                    {
                        extremumX = GoldenRatioMin(function, intervalA, intervalB, epsilonValue);
                    }
                    else
                    {
                        extremumX = GoldenRatioMax(function, intervalA, intervalB, epsilonValue);
                    }

                    extremumY = function(extremumX);

                    float screenExtremumX = (float)(extremumX - intervalA) * scaleX;
                    float screenExtremumY = graphPictureBox.Height - (float)(extremumY - minY) * scaleY;

                    Brush pointBrush = isMinimum ? Brushes.Red : Brushes.Green;
                    Color pointColor = isMinimum ? Color.Red : Color.Green;

                    g.FillEllipse(pointBrush, screenExtremumX - 5, screenExtremumY - 5, 10, 10);
                    g.DrawEllipse(new Pen(Color.Black, 1), screenExtremumX - 5, screenExtremumY - 5, 10, 10);

                    string label = isMinimum
                        ? $"min: ({extremumX:F3}, {extremumY:F3})"
                        : $"max: ({extremumX:F3}, {extremumY:F3})";

                    g.DrawString(label, new Font("Arial", 8, FontStyle.Bold),
                        new SolidBrush(pointColor), screenExtremumX + 10, screenExtremumY - 15);
                }
            }
            catch
            {
                // Ошибка при отображении точки экстремума
                MessageBox.Show("Ошибка при отображении точки экстремума", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Добавляем заголовок и информацию
            string modeText = isMinimum ? "Поиск минимума" : "Поиск максимума";
            g.DrawString($"f(x) = {txtFunction.Text}",
                new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 10, 10);

            g.DrawString(modeText,
                new Font("Arial", 9, FontStyle.Bold),
                isMinimum ? Brushes.Red : Brushes.Green, 10, 30);

            g.DrawString($"Интервал: [{a:F2}, {b:F2}]",
                new Font("Arial", 9), Brushes.Black, 10, 50);

            g.DrawString($"Точность: ε = {txtEpsilon.Text}",
                new Font("Arial", 9), Brushes.Black, 10, 70);

            g.DrawString($"Метод: Золотое сечение (φ ≈ {GOLDEN_RATIO:F4})",
                new Font("Arial", 8), Brushes.DarkBlue, 10, 90);
        }

        public void OnShow()
        {
            UpdateStatus("Лабораторная работа №3 активна");
        }

        public void OnHide()
        {
            // Сохранение состояния при необходимости
        }
    }
}