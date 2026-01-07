using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Labs.Lab2
{
    public static class Solver
    {
        public static double[] Gauss(double[,] A, double[] B)
        {
            int n = B.Length;
            double[,] a = (double[,])A.Clone();
            double[] b = (double[])B.Clone();
            double[] x = new double[n];

            // Прямой ход
            for (int k = 0; k < n; k++)
            {
                // Поиск главного элемента
                int maxRow = k;
                double maxVal = Math.Abs(a[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(a[i, k]) > maxVal)
                    {
                        maxVal = Math.Abs(a[i, k]);
                        maxRow = i;
                    }
                }

                // Перестановка строк
                if (maxRow != k)
                {
                    for (int j = k; j < n; j++)
                    {
                        (a[k, j], a[maxRow, j]) = (a[maxRow, j], a[k, j]);
                    }
                    (b[k], b[maxRow]) = (b[maxRow], b[k]);
                }

                // Проверка на вырожденность
                if (Math.Abs(a[k, k]) < 1e-12)
                    throw new Exception("Матрица вырожденная или плохо обусловлена");

                // Обнуление элементов ниже главной диагонали
                for (int i = k + 1; i < n; i++)
                {
                    double factor = a[i, k] / a[k, k];
                    for (int j = k; j < n; j++)
                    {
                        a[i, j] -= factor * a[k, j];
                    }
                    b[i] -= factor * b[k];
                }
            }

            // Обратный ход
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += a[i, j] * x[j];
                }
                x[i] = (b[i] - sum) / a[i, i];
            }

            return x;
        }

        public static double[] JordanGauss(double[,] A, double[] B)
        {
            int n = B.Length;
            double[,] a = (double[,])A.Clone();
            double[] b = (double[])B.Clone();

            for (int k = 0; k < n; k++)
            {
                // Нормализация строки
                double div = a[k, k];
                if (Math.Abs(div) < 1e-12)
                    throw new Exception("Матрица вырожденная");

                for (int j = k; j < n; j++)
                    a[k, j] /= div;
                b[k] /= div;

                // Обнуление столбца
                for (int i = 0; i < n; i++)
                {
                    if (i != k)
                    {
                        double factor = a[i, k];
                        for (int j = k; j < n; j++)
                            a[i, j] -= factor * a[k, j];
                        b[i] -= factor * b[k];
                    }
                }
            }

            return b;
        }

        public static double[] Cramer(double[,] A, double[] B)
        {
            int n = B.Length;
            double[] x = new double[n];

            double detA = Determinant(A);

            if (Math.Abs(detA) < 1e-12)
                throw new Exception("Определитель матрицы A равен нулю");

            for (int i = 0; i < n; i++)
            {
                double[,] modified = (double[,])A.Clone();
                for (int j = 0; j < n; j++)
                    modified[j, i] = B[j];
                x[i] = Determinant(modified) / detA;
            }

            return x;
        }

        public static double Determinant(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            if (n == 1) return matrix[0, 0];
            if (n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            double det = 0;
            for (int k = 0; k < n; k++)
            {
                double[,] minor = new double[n - 1, n - 1];
                for (int i = 1; i < n; i++)
                {
                    int col = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == k) continue;
                        minor[i - 1, col] = matrix[i, j];
                        col++;
                    }
                }
                det += matrix[0, k] * Math.Pow(-1, k) * Determinant(minor);
            }
            return det;
        }
    }
}