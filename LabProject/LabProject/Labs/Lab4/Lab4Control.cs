using LabProject.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LabProject.Labs
{
    public partial class Lab4Control : UserControl, ILabModule
    {
        private const int DEFAULT_SIZE = 20;
        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 100;

        private List<int> array = null!;
        private Random random = new Random();
        private Dictionary<string, bool> sortingAlgorithms = new Dictionary<string, bool>();
        private bool isAscending = true;
        private bool isRunning = false;

        public int LabNumber => 4;
        public string LabTitle => "Лабораторная работа №4: Олимпиадные сортировки";

        public Lab4Control()
        {
            InitializeComponent();
            InitializeUI();
            this.Dock = DockStyle.Fill;
            GenerateRandomData();
        }

        private void InitializeUI()
        {
            // Настройка элементов управления
            numericUpDownSize.Value = DEFAULT_SIZE;
            checkBoxAscending.Checked = true;
            checkBoxDescending.Checked = false;

            // Настройка графика
            InitializeChart();

            // Подключение обработчиков событий
            calculateMenuItem.Click += CalculateMenuItem_Click;
            clearMenuItem.Click += ClearMenuItem_Click;
            generateDataMenuItem.Click += GenerateDataMenuItem_Click;
            aboutMenuItem4.Click += AboutMenuItem_Click;

            // Инициализация словаря алгоритмов
            sortingAlgorithms.Add("Пузырьковая", true);
            sortingAlgorithms.Add("Вставками", true);
            sortingAlgorithms.Add("Шейкерная", true);
            sortingAlgorithms.Add("Быстрая", true);
            sortingAlgorithms.Add("BOGO", true);

            // Привязка чекбоксов к словарю
            InitializeAlgorithmCheckboxes();
        }

        private void InitializeAlgorithmCheckboxes()
        {
            // Устанавливаем начальные значения чекбоксов
            checkBoxBubble.Checked = sortingAlgorithms["Пузырьковая"];
            checkBoxInsertion.Checked = sortingAlgorithms["Вставками"];
            checkBoxShaker.Checked = sortingAlgorithms["Шейкерная"];
            checkBoxQuick.Checked = sortingAlgorithms["Быстрая"];
            checkBoxBogo.Checked = sortingAlgorithms["BOGO"];

            // Привязываем обработчики событий
            checkBoxBubble.CheckedChanged += (s, e) => sortingAlgorithms["Пузырьковая"] = checkBoxBubble.Checked;
            checkBoxInsertion.CheckedChanged += (s, e) => sortingAlgorithms["Вставками"] = checkBoxInsertion.Checked;
            checkBoxShaker.CheckedChanged += (s, e) => sortingAlgorithms["Шейкерная"] = checkBoxShaker.Checked;
            checkBoxQuick.CheckedChanged += (s, e) => sortingAlgorithms["Быстрая"] = checkBoxQuick.Checked;
            checkBoxBogo.CheckedChanged += (s, e) => sortingAlgorithms["BOGO"] = checkBoxBogo.Checked;
        }

        private void InitializeChart()
        {
            chartSorting.Series.Clear();
            chartSorting.ChartAreas[0].AxisX.Title = "Индекс";
            chartSorting.ChartAreas[0].AxisY.Title = "Значение";
            chartSorting.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chartSorting.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chartSorting.Legends[0].Enabled = true;
            chartSorting.ChartAreas[0].AxisX.LabelStyle.Enabled = false; // Отключаем подписи к осям для лучшей производительности
        }

        private void GenerateRandomData()
        {
            int size = (int)numericUpDownSize.Value;
            array = new List<int>();

            for (int i = 0; i < size; i++)
            {
                array.Add(random.Next(1, 100));
            }

            DrawArray();
        }

        private void DrawArray()
        {
            chartSorting.Series.Clear();

            // Оригинальный массив
            var originalSeries = new Series("Исходный массив");
            originalSeries.ChartType = SeriesChartType.Column;
            originalSeries.Color = Color.Blue;
            originalSeries.BorderWidth = 1;

            for (int i = 0; i < array.Count; i++)
            {
                originalSeries.Points.AddXY(i, array[i]);
            }

            chartSorting.Series.Add(originalSeries);
        }

        private void CalculateMenuItem_Click(object? sender, EventArgs e)
        {
            if (isRunning)
                return;

            isRunning = true;
            UpdateStatus("Сортировка запущена...");

            // Проверка выбора алгоритмов
            var selectedAlgorithms = sortingAlgorithms
                .Where(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToList();

            if (selectedAlgorithms.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один алгоритм сортировки",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                isRunning = false;
                return;
            }

            // Очистка предыдущих результатов
            txtResults.Clear();
            chartSorting.Series.Clear();

            // Подготовка данных
            var originalArray = new List<int>(array);
            var results = new List<SortResult>();

            // Запуск выбранных алгоритмов
            foreach (var algorithm in selectedAlgorithms)
            {
                var sortedArray = new List<int>(originalArray);
                long elapsed = 0;
                int steps = 0;

                switch (algorithm)
                {
                    case "Пузырьковая":
                        elapsed = BubbleSort(sortedArray, ref steps);
                        break;
                    case "Вставками":
                        elapsed = InsertionSort(sortedArray, ref steps);
                        break;
                    case "Шейкерная":
                        elapsed = ShakerSort(sortedArray, ref steps);
                        break;
                    case "Быстрая":
                        elapsed = QuickSort(sortedArray, 0, sortedArray.Count - 1, ref steps);
                        break;
                    case "BOGO":
                        elapsed = BogoSort(sortedArray, ref steps);
                        break;
                }

                // Визуализация результата
                DrawSortedArray(sortedArray, algorithm, elapsed);
                results.Add(new SortResult
                {
                    Algorithm = algorithm,
                    Time = elapsed,
                    Steps = steps,
                    Size = sortedArray.Count
                });
            }

            // Отображение результатов
            DisplayResults(results);

            isRunning = false;
            UpdateStatus($"Сортировка завершена. Всего: {selectedAlgorithms.Count} алгоритма(ов)");

            // Определение самого быстрого алгоритма
            if (results.Count > 1)
            {
                var fastest = results.OrderBy(r => r.Time).First();
                txtResults.AppendText($"Самый быстрый алгоритм: {fastest.Algorithm}\n");
                txtResults.AppendText($"Время: {fastest.Time} мс\n");
            }
        }

        // Алгоритмы сортировки с учетом подсчета шагов
        private long BubbleSort(List<int> arr, ref int steps)
        {
            var startTime = DateTime.Now;
            int n = arr.Count;
            bool swapped;
            steps = 0;

            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    steps++;
                    if (isAscending ? arr[j] > arr[j + 1] : arr[j] < arr[j + 1])
                    {
                        // Обмен элементов
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }

            return (long)(DateTime.Now - startTime).TotalMilliseconds;
        }

        private long InsertionSort(List<int> arr, ref int steps)
        {
            var startTime = DateTime.Now;
            int n = arr.Count;
            steps = 0;

            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;

                if (isAscending)
                {
                    while (j >= 0 && arr[j] > key)
                    {
                        steps++;
                        arr[j + 1] = arr[j];
                        j--;
                    }
                }
                else
                {
                    while (j >= 0 && arr[j] < key)
                    {
                        steps++;
                        arr[j + 1] = arr[j];
                        j--;
                    }
                }

                arr[j + 1] = key;
                steps++;
            }

            return (long)(DateTime.Now - startTime).TotalMilliseconds;
        }

        private long ShakerSort(List<int> arr, ref int steps)
        {
            var startTime = DateTime.Now;
            int left = 0;
            int right = arr.Count - 1;
            bool swapped = true;
            steps = 0;

            while (left < right && swapped)
            {
                swapped = false;

                // Слева направо
                for (int i = left; i < right; i++)
                {
                    steps++;
                    if (isAscending ? arr[i] > arr[i + 1] : arr[i] < arr[i + 1])
                    {
                        // Обмен элементов
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                right--;
                swapped = false;

                // Справа налево
                for (int i = right; i > left; i--)
                {
                    steps++;
                    if (isAscending ? arr[i] < arr[i - 1] : arr[i] > arr[i - 1])
                    {
                        // Обмен элементов
                        int temp = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = temp;
                        swapped = true;
                    }
                }

                left++;
            }

            return (long)(DateTime.Now - startTime).TotalMilliseconds;
        }

        private long QuickSort(List<int> arr, int low, int high, ref int steps)
        {
            var startTime = DateTime.Now;
            steps = 0;
            QuickSortRecursive(arr, low, high, ref steps);
            return (long)(DateTime.Now - startTime).TotalMilliseconds;
        }

        private void QuickSortRecursive(List<int> arr, int low, int high, ref int steps)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high, ref steps);
                QuickSortRecursive(arr, low, pi - 1, ref steps);
                QuickSortRecursive(arr, pi + 1, high, ref steps);
            }
        }

        private int Partition(List<int> arr, int low, int high, ref int steps)
        {
            steps++;
            int pivot = arr[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                steps++;
                if (isAscending ? arr[j] <= pivot : arr[j] >= pivot)
                {
                    i++;
                    // Обмен элементов
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // Обмен элементов
            int temp2 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp2;
            steps++;

            return i + 1;
        }

        private long BogoSort(List<int> arr, ref int steps)
        {
            var startTime = DateTime.Now;
            int iterations = 0;
            steps = 0;

            while (!IsSorted(arr))
            {
                Shuffle(arr);
                iterations++;
                steps++;

                if (iterations > 100000) // Ограничение для безопасности
                    break;
            }

            return (long)(DateTime.Now - startTime).TotalMilliseconds;
        }

        private bool IsSorted(List<int> arr)
        {
            for (int i = 0; i < arr.Count - 1; i++)
            {
                if (isAscending ? arr[i] > arr[i + 1] : arr[i] < arr[i + 1])
                    return false;
            }
            return true;
        }

        private void Shuffle(List<int> arr)
        {
            int n = arr.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                // Обмен элементов
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        private void DrawSortedArray(List<int> sortedArray, string algorithm, long elapsed)
        {
            var series = new Series(algorithm);
            series.ChartType = SeriesChartType.Column;
            series.Color = GetRandomColor();
            series.BorderWidth = 2;

            for (int i = 0; i < sortedArray.Count; i++)
            {
                series.Points.AddXY(i, sortedArray[i]);
            }

            chartSorting.Series.Add(series);
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(
                random.Next(200, 256), // Более яркие цвета
                random.Next(200, 256),
                random.Next(200, 256)
            );
        }

        private void DisplayResults(List<SortResult> results)
        {
            txtResults.AppendText("=== РЕЗУЛЬТАТЫ СОРТИРОВКИ ===\n\n");

            foreach (var result in results)
            {
                txtResults.AppendText($"Алгоритм: {result.Algorithm}\n");
                txtResults.AppendText($"Время: {result.Time} мс\n");
                txtResults.AppendText($"Шагов: {result.Steps}\n");
                txtResults.AppendText($"Размер массива: {result.Size}\n");
                txtResults.AppendText($"Направление: {(isAscending ? "По возрастанию" : "По убыванию")}\n");
                txtResults.AppendText("------------------------------------------------\n");
            }
        }

        private void ClearMenuItem_Click(object? sender, EventArgs e)
        {
            array = new List<int>();
            chartSorting.Series.Clear();
            txtResults.Clear();
            GenerateRandomData();
            UpdateStatus("Данные очищены. Готово к работе");
        }

        private void GenerateDataMenuItem_Click(object? sender, EventArgs e)
        {
            GenerateRandomData();
            UpdateStatus($"Сгенерировано {array.Count} случайных элементов");
        }

        private void AboutMenuItem_Click(object? sender, EventArgs e)
        {
            string message = "Олимпиадные сортировки\n\n" +
                             "В программе реализованы следующие алгоритмы сортировки:\n\n" +

                             "1. Пузырьковая сортировка:\n" +
                             "   • Сравнивает пары соседних элементов\n" +
                             "   • Обменивает местами элементы, если они расположены неправильно\n" +
                             "   • Повторяет процесс до полной сортировки\n" +
                             "   • Сложность: O(n²) в худшем случае\n" +
                             "   • Оптимизирована для раннего завершения при отсутствии обменов\n\n" +

                             "2. Сортировка вставками:\n" +
                             "   • Строит отсортированную последовательность по одному элементу\n" +
                             "   • На каждом шаге вставляет очередной элемент в правильную позицию\n" +
                             "   • Эффективна для небольших или частично отсортированных массивов\n" +
                             "   • Сложность: O(n²) в худшем случае\n\n" +

                             "3. Шейкерная сортировка (смешанная):\n" +
                             "   • Модификация пузырьковой сортировки\n" +
                             "   • Перемещается в обоих направлениях по массиву\n" +
                             "   • Ускоряет процесс сортировки за счет уменьшения диапазона поиска\n" +
                             "   • Сложность: O(n²), но на практике быстрее пузырьковой\n\n" +

                             "4. Быстрая сортировка:\n" +
                             "   • Использует метод «разделяй и властвуй»\n" +
                             "   • Выбирает опорный элемент и разделяет массив на части\n" +
                             "   • Рекурсивно сортирует части\n" +
                             "   • Сложность: O(n log n) в среднем случае\n" +
                             "   • Один из самых быстрых алгоритмов в реальных приложениях\n\n" +

                             "5. BOGO-сортировка:\n" +
                             "   • Случайный алгоритм, основанный на методе Монте-Карло\n" +
                             "   • Случайно перемешивает массив до получения отсортированной последовательности\n" +
                             "   • Теоретически имеет сложность O(n·n!)\n" +
                             "   • Используется в учебных целях как пример неэффективного алгоритма\n\n" +

                             "Особенности реализации:\n" +
                             "• Возможность одновременного запуска нескольких алгоритмов\n" +
                             "• Визуализация процесса сортировки для каждого алгоритма\n" +
                             "• Отображение времени выполнения и количества операций\n" +
                             "• Выбор направления сортировки (по возрастанию/убыванию)\n" +
                             "• Генерация случайных данных для тестирования\n" +
                             "• Автоматическое определение самого быстрого алгоритма";

            MessageBox.Show(message, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateStatus(string message)
        {
            if (Parent?.Parent is Shell.MainForm mainForm)
            {
                mainForm.SetStatus(message);
            }
        }

        public void OnShow()
        {
            UpdateStatus("Лабораторная работа №4 активна");
        }

        public void OnHide()
        {
            // Сохранение состояния при необходимости
        }

        private void checkBoxAscending_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBoxAscending.Checked)
            {
                isAscending = true;
                checkBoxDescending.Checked = false;
            }
            else if (!checkBoxDescending.Checked)
            {
                // Если оба чекбокса выключены, включаем "по возрастанию"
                checkBoxAscending.Checked = true;
                isAscending = true;
            }
        }

        private void checkBoxDescending_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBoxDescending.Checked)
            {
                isAscending = false;
                checkBoxAscending.Checked = false;
            }
            else if (!checkBoxAscending.Checked)
            {
                // Если оба чекбокса выключены, включаем "по возрастанию"
                checkBoxAscending.Checked = true;
                isAscending = true;
            }
        }

        private void numericUpDownSize_ValueChanged(object? sender, EventArgs e)
        {
            // Ограничение размера
            if (numericUpDownSize.Value < MIN_SIZE)
                numericUpDownSize.Value = MIN_SIZE;
            else if (numericUpDownSize.Value > MAX_SIZE)
                numericUpDownSize.Value = MAX_SIZE;

            GenerateRandomData();
            UpdateStatus($"Размер массива изменен на {numericUpDownSize.Value}");
        }

        // Вспомогательные классы для результатов
        private class SortResult
        {
            public string? Algorithm { get; set; }
            public long Time { get; set; }
            public int Steps { get; set; }
            public int Size { get; set; }
        }
    }
}