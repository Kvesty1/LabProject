using LabProject.Core;
using LabProject.Shell;
using NCalc;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LabProject.Labs
{
    public partial class Lab1Control : UserControl, ILabModule
    {
        public int LabNumber => 1;
        public string LabTitle => "Лабораторная работа №1: Метод дихотомии";

        public Lab1Control()
        {
            InitializeComponent();
            InitializeComponentEvents();
            SetupChart();
            this.Dock = DockStyle.Fill; // Критично для масштабирования

            // Заполняем поля значениями по умолчанию
            txtA.Text = "-5";
            txtB.Text = "5";
            txtE.Text = "0.001";
            txtFunc.Text = "x^2 + x - 6";
        }

        private void InitializeComponentEvents()
        {
            рассчитатьToolStripMenuItem.Click += РассчитатьToolStripMenuItem_Click;
            очиститьToolStripMenuItem.Click += ОчиститьToolStripMenuItem_Click;

            // Добавляем обработчики для Enter в текстовых полях
            txtA.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) DoCalculate(); };
            txtB.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) DoCalculate(); };
            txtE.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) DoCalculate(); };
            txtFunc.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) DoCalculate(); };
        }

        private void SetupChart()
        {
            chart.ChartAreas[0].AxisX.Title = "X";
            chart.ChartAreas[0].AxisY.Title = "f(x)";
            chart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart.Series["f"].Color = Color.Blue;
            chart.Series["min"].Color = Color.Red;
        }

        // Метод вычисления
        private void DoCalculate()
        {
            try
            {
                if (!double.TryParse(txtA.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double a))
                { MessageBox.Show("Неверный формат a"); return; }
                if (!double.TryParse(txtB.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double b))
                { MessageBox.Show("Неверный формат b"); return; }
                if (!double.TryParse(txtE.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double epsi))
                { MessageBox.Show("Неверный формат e"); return; }
                if (epsi <= 0)
                { MessageBox.Show("e должно быть > 0"); return; }
                if (!(a < b))
                { MessageBox.Show("a должно быть меньше b"); return; }

                string formula = txtFunc.Text.Trim();
                if (formula == "")
                { MessageBox.Show("Введите формулу f(x)"); return; }

                Func<double, double> eval;
                try
                {
                    eval = BuildEvaluator(formula);
                    _ = eval(a);
                    _ = eval(b);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка в формуле: " + ex.Message);
                    return;
                }

                try
                {
                    var (xmin, fmin, it) = DichotomyMin(a, b, epsi, eval);
                    lblResult.Text = $"xmin = {xmin:F8}, f(xmin) = {fmin:F8}, итераций = {it}";
                    Plot(eval, a, b, xmin, fmin);

                    // Обновляем статус в MainForm
                    UpdateStatus($"Расчет завершен: xmin = {xmin:F8}, f(xmin) = {fmin:F8}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка вычислений: " + ex.Message);
                    UpdateStatus("Ошибка вычислений");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Критическая ошибка");
            }
        }

        // Метод очистки
        private void ClearAll()
        {
            txtA.Clear();
            txtB.Clear();
            txtE.Clear();
            txtFunc.Clear();
            lblResult.Text = "Результат:";
            chart.Series["f"].Points.Clear();
            chart.Series["min"].Points.Clear();
            UpdateStatus("Данные очищены");
        }

        // ------------ Построение выражения -----------------
        private Func<double, double> BuildEvaluator(string text)
        {
            string adjusted = ConvertCaretToPow(text);
            return (x) =>
            {
                var expr = new NCalc.Expression(adjusted, EvaluateOptions.IgnoreCase);
                expr.Parameters["x"] = x;
                expr.EvaluateFunction += (name, args) =>
                {
                    string n = name.ToLower();
                    double p0() => Convert.ToDouble(args.Parameters[0].Evaluate());
                    double p1() => Convert.ToDouble(args.Parameters[1].Evaluate());
                    args.Result = n switch
                    {
                        "sin" => Math.Sin(p0()),
                        "cos" => Math.Cos(p0()),
                        "tan" => Math.Tan(p0()),
                        "sqrt" => Math.Sqrt(p0()),
                        "exp" => Math.Exp(p0()),
                        "log" => Math.Log(p0()),
                        "ln" => Math.Log(p0()),
                        "log10" => Math.Log10(p0()),
                        "abs" => Math.Abs(p0()),
                        "pow" => Math.Pow(p0(), p1()),
                        _ => throw new Exception("Неизвестная функция: " + name)
                    };
                };
                return Convert.ToDouble(expr.Evaluate());
            };
        }

        private string ConvertCaretToPow(string input)
        {
            try
            {
                while (input.Contains("^"))
                {
                    int pos = input.IndexOf("^");
                    // --- левая часть ---
                    int leftEnd = pos - 1;
                    if (leftEnd < 0) throw new Exception();
                    int leftStart = leftEnd;
                    if (input[leftEnd] == ')')
                    {
                        int balance = 1;
                        leftStart--;
                        while (leftStart >= 0 && balance > 0)
                        {
                            if (input[leftStart] == ')') balance++;
                            if (input[leftStart] == '(') balance--;
                            leftStart--;
                        }
                        leftStart++;
                    }
                    else
                    {
                        while (leftStart >= 0 &&
                            (char.IsLetterOrDigit(input[leftStart]) ||
                             input[leftStart] == '.' ||
                             input[leftStart] == 'x'))
                            leftStart--;
                        leftStart++;
                    }
                    // --- правая часть ---
                    int rightStart = pos + 1;
                    if (rightStart >= input.Length) throw new Exception();
                    int rightEnd = rightStart;
                    if (input[rightStart] == '(')
                    {
                        int balance = 1;
                        rightEnd++;
                        while (rightEnd < input.Length && balance > 0)
                        {
                            if (input[rightEnd] == '(') balance++;
                            if (input[rightEnd] == ')') balance--;
                            rightEnd++;
                        }
                    }
                    else
                    {
                        while (rightEnd < input.Length &&
                            (char.IsLetterOrDigit(input[rightEnd]) ||
                             input[rightEnd] == '.' ||
                             input[rightEnd] == '-' ||
                             input[rightEnd] == 'x'))
                            rightEnd++;
                    }
                    string left = input.Substring(leftStart, leftEnd - leftStart + 1);
                    string right = input.Substring(rightStart, rightEnd - rightStart);
                    input = input.Substring(0, leftStart)
                        + $"Pow({left},{right})"
                        + input.Substring(rightEnd);
                }
                return input;
            }
            catch
            {
                throw new Exception("Некорректная запись степени. Проверьте формулу.");
            }
        }

        // ---------------- Метод дихотомии -------------------
        private (double, double, int) DichotomyMin(double a, double b, double eps, Func<double, double> f)
        {
            double l = a, r = b;
            double delta = eps / 4;
            int it = 0;

            while ((r - l) / 2 > eps)
            {
                it++;
                double mid = (l + r) / 2;
                double x1 = mid - delta;
                double x2 = mid + delta;
                double f1 = f(x1);
                double f2 = f(x2);

                if (f1 <= f2) r = x2;
                else l = x1;

                if (it > 1000000) throw new Exception("Слишком много итераций");
            }

            double xmin = (l + r) / 2;
            return (xmin, f(xmin), it);
        }

        // ---------------- Построение графика -------------------
        private void Plot(Func<double, double> f, double a, double b, double xmin, double fmin)
        {
            chart.Series["f"].Points.Clear();
            chart.Series["min"].Points.Clear();

            int pts = 400;
            double step = (b - a) / (pts - 1);

            for (int i = 0; i < pts; i++)
            {
                double x = a + step * i;
                try
                {
                    double y = f(x);
                    chart.Series["f"].Points.AddXY(x, y);
                }
                catch
                {
                    // Пропускаем точки, где функция не определена
                }
            }

            chart.Series["min"].Points.AddXY(xmin, fmin);
            chart.ChartAreas[0].AxisX.Minimum = a;
            chart.ChartAreas[0].AxisX.Maximum = b;

            // Автоматически подбираем масштаб по Y
            chart.ChartAreas[0].RecalculateAxesScale();
        }

        // Кнопка Рассчитать
        private void РассчитатьToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            DoCalculate();
        }

        // Кнопка Очистки
        private void ОчиститьToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            ClearAll();
        }

        public void OnShow()
        {
            UpdateStatus("Лабораторная работа №1 активна");
        }

        public void OnHide()
        {
            // Сохранение состояния при необходимости
        }

        private void UpdateStatus(string message)
        {
            // Если Lab1Control находится в MainForm, обновляем статус-бар
            if (Parent?.Parent is MainForm mainForm)
            {
                mainForm.SetStatus(message);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Автоматически подстраиваем размеры элементов под новое окно
            if (chart != null)
            {
                chart.Width = this.Width - 40;
                chart.Height = this.Height - chart.Top - 20;
            }
        }
    }
}