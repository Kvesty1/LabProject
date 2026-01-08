using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using LabProject.Core;

namespace LabProject.Labs
{
    public partial class Lab2Control : UserControl, ILabModule
    {
        private double[]? vectorX;
        public int LabNumber => 2;
        public string LabTitle => "Лабораторная работа №2";

        public void OnShow() { }
        public void OnHide() { }

        public Lab2Control()
        {
            InitializeComponent();

            // Устанавливаем начальный размер матрицы 2 Х 2
            numericUpDownN.Value = 2;

            SetupDataGrids();

            this.Dock = DockStyle.Fill;
        }

        private void SetupDataGrids()
        {
            int n = (int)numericUpDownN.Value; // Размер матрицы из NumericUpDown

            // Матрица A (n столбцов) + вектор B (1 столбец)
            dataGridViewAB.ColumnCount = n + 1;
            dataGridViewAB.RowCount = n;

            // Вектор X
            dataGridViewX.ColumnCount = 1;
            dataGridViewX.RowCount = n;
            dataGridViewX.Columns[0].HeaderText = "X";
            dataGridViewX.ReadOnly = true;

            UpdateHeaders();
        }

        private void UpdateHeaders()
        {
            int n = dataGridViewAB.RowCount;

            if (dataGridViewAB.ColumnCount < n + 1)
                return;

            for (int i = 0; i < n; i++)
            {
                dataGridViewAB.Columns[i].HeaderText = $"A[{i + 1}]";
            }

            dataGridViewAB.Columns[n].HeaderText = "B";
        }

        private void numericUpDownN_ValueChanged(object sender, EventArgs e)
        {
            int n = (int)numericUpDownN.Value;
            dataGridViewAB.ColumnCount = n + 1;
            dataGridViewAB.RowCount = n;
            dataGridViewX.RowCount = n;
            UpdateHeaders();
        }

        private double[,] GetMatrixA()
        {
            int n = dataGridViewAB.RowCount;
            double[,] A = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (dataGridViewAB[j, i].Value == null)
                        A[i, j] = 0;
                    else if (!double.TryParse(dataGridViewAB[j, i].Value.ToString(), out A[i, j]))
                        throw new Exception($"Некорректное значение в ячейке A[{i + 1},{j + 1}]");
                }
            }
            return A;
        }

        private double[] GetVectorB()
        {
            int n = dataGridViewAB.RowCount;
            double[] B = new double[n];

            for (int i = 0; i < n; i++)
            {
                int colIndex = n; // Последний столбец
                if (dataGridViewAB[colIndex, i].Value == null)
                    B[i] = 0;
                else if (!double.TryParse(dataGridViewAB[colIndex, i].Value.ToString(), out B[i]))
                    throw new Exception($"Некорректное значение в ячейке B[{i + 1}]");
            }
            return B;
        }

        private void DisplayVectorX(double[] X)
        {
            vectorX = X;
            dataGridViewX.RowCount = X.Length;
            for (int i = 0; i < X.Length; i++)
            {
                dataGridViewX[0, i].Value = Math.Round(X[i], 6);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private async void GaussToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await SolveAsync(Solver.Gauss, "Гаусса");
        }

        private async void JordanGaussToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await SolveAsync(Solver.JordanGauss, "Жардана-Гаусса");
        }

        private async void CramerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await SolveAsync(Solver.Cramer, "Крамера");
        }

        private async Task SolveAsync(Func<double[,], double[], double[]> method, string methodName)
        {
            try
            {
                double[,] A = GetMatrixA();
                double[] B = GetVectorB();

                for (int i = 0; i < B.Length; i++)
                    B[i] = -B[i];

                toolStripStatusLabel1.Text = "Вычисление...";
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                var sw = Stopwatch.StartNew();
                double det = Solver.Determinant(A);

                if (Math.Abs(det) < 1e-12)
                    throw new Exception("Система вырождена (det(A) = 0). Решение невозможно.");

                double[] X = await Task.Run(() => method(A, B));
                sw.Stop();
                DisplayVectorX(X);
                toolStripStatusLabel1.Text = $"Метод {methodName}: {sw.ElapsedMilliseconds} мс";
                toolStripProgressBar1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Ошибка";
                toolStripProgressBar1.Visible = false;
            }
        }

        private void GenerateRandomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = dataGridViewAB.RowCount;
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridViewAB[j, i].Value =
                        Math.Round(rand.NextDouble() * 20 - 10, 2);
                }
                dataGridViewAB[n, i].Value =
                    Math.Round(rand.NextDouble() * 20 - 10, 2);
            }
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = dataGridViewAB.RowCount;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    dataGridViewAB[j, i].Value = "";
                }
                dataGridViewX[0, i].Value = "";
            }
            toolStripStatusLabel1.Text = "Готово";
        }

        private void ExportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vectorX == null)
            {
                MessageBox.Show(
                    "Вектор X ещё не вычислен. Сначала решите систему.",
                    "Экспорт невозможен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                ExcelHelper.ExportToExcel(GetMatrixA(), GetVectorB(), vectorX);
                toolStripStatusLabel1.Text = "Данные экспортированы в Excel";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var (A, B) = ExcelHelper.ImportFromExcel();
                if (A != null && B != null)
                {
                    numericUpDownN.Value = B.Length;
                    for (int i = 0; i < B.Length; i++)
                    {
                        for (int j = 0; j < B.Length; j++)
                        {
                            dataGridViewAB[j, i].Value = A[i, j];
                        }
                        dataGridViewAB[B.Length, i].Value = B[i];
                    }
                    toolStripStatusLabel1.Text = "Данные импортированы из Excel";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void лаб2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Lab2Control_Load(object sender, EventArgs e)
        {

        }

        private void aboutMenuItem2_Click(object sender, EventArgs e)
        {
            string message = "Численные методы решения систем линейных алгебраических уравнений (СЛАУ)\n\n" +
                     "В программе реализовано три метода решения СЛАУ вида A∙X + B = 0:\n\n" +
                     "1. Метод Гаусса:\n" +
                     "   • Состоит из прямого хода (приведение к треугольному виду) и обратного хода (нахождение решения)\n" +
                     "   • Обеспечивает высокую точность при условии выбора главного элемента\n" +
                     "   • Вычислительная сложность: O(n³)\n" +
                     "   • Устойчив к ошибкам округления при правильной реализации\n\n" +
                     "2. Метод Жордана-Гаусса:\n" +
                     "   • Модификация метода Гаусса с полным исключением переменных\n" +
                     "   • Приводит матрицу к единичной форме за один проход\n" +
                     "   • Удобен для ручных вычислений\n" +
                     "   • Обеспечивает непосредственное получение решения\n\n" +
                     "3. Метод Крамера:\n" +
                     "   • Использует формулы Крамера с определителями матрицы\n" +
                     "   • Требует невырожденности матрицы (det(A) ≠ 0)\n" +
                     "   • Вычислительно затратен для систем высокого порядка\n" +
                     "   • Предоставляет теоретически точное решение\n\n" +
                     "Возможности программы:\n" +
                     "• Импорт данных из Excel\n" +
                     "• Экспорт результатов в Excel\n" +
                     "• Генерация случайных данных для тестирования\n" +
                     "• Визуализация результатов в таблицах\n" +
                     "• Асинхронное выполнение вычислений с отображением времени\n\n" +
                     "Ограничения:\n" +
                     "• Размер матрицы: от 2×2 до 50×50\n" +
                     "• Все методы требуют невырожденности матрицы коэффициентов\n" +
                     "• Для метода Крамера система должна иметь единственное решение\n\n" +
                     "Рекомендации:\n" +
                     "• Для небольших систем (до 10×10) можно использовать любой метод\n" +
                     "• Для больших систем рекомендуется метод Гаусса как наиболее эффективный\n" +
                     "• Метод Крамера для проверки решений и учебных целей";

            MessageBox.Show(message, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public static class ExcelHelper
    {
        public static void ExportToExcel(double[,] A, double[] B, double[] X)
        {
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Решение СЛАУ");

                int n = B.Length;

                // Заголовки
                ws.Cells[1, 1].Value = "Матрица A";
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        ws.Cells[i + 2, j + 1].Value = A[i, j];
                    }
                }

                ws.Cells[1, n + 2].Value = "Вектор B";
                for (int i = 0; i < n; i++)
                {
                    ws.Cells[i + 2, n + 2].Value = B[i];
                }

                ws.Cells[1, n + 3].Value = "Вектор X";
                for (int i = 0; i < n; i++)
                {
                    ws.Cells[i + 2, n + 3].Value = X[i];
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel файлы (*.xlsx)|*.xlsx",
                    FileName = $"SLAU_Решение_{DateTime.Now:yyyy-MM-dd_HH-mm}.xlsx"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    package.SaveAs(new FileInfo(sfd.FileName));
                }
            }
        }

        public static (double[,]?, double[]?) ImportFromExcel()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Excel файлы (*.xlsx;*.xls)|*.xlsx;*.xls"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return (null, null);

            using (var package = new ExcelPackage(new FileInfo(ofd.FileName)))
            {
                var ws = package.Workbook.Worksheets[0];

                // Определяем размер матрицы
                int n = 0;
                while (ws.Cells[2 + n, 1].Value != null)
                    n++;

                if (n == 0)
                    throw new Exception("Файл не содержит данных");

                double[,] A = new double[n, n];
                double[] B = new double[n];

                // Чтение матрицы A
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (ws.Cells[2 + i, 1 + j].Value != null)
                            A[i, j] = Convert.ToDouble(ws.Cells[2 + i, 1 + j].Value);
                    }
                }

                // Чтение вектора B
                for (int i = 0; i < n; i++)
                {
                    if (ws.Cells[2 + i, n + 2].Value != null)
                        B[i] = Convert.ToDouble(ws.Cells[2 + i, n + 2].Value);
                }

                return (A, B);
            }
        }
    }
}