using System;
using System.Collections.Generic;
class Program
{
    // Определение делегата для методов сортировки
    public delegate void SortDelegate(int[] array);
    static void Main(string[] args)
    {
        while (true)
        {
            List<int> numbersList = new List<int>();
            Console.Clear(); // Очистка консоли
            Console.WriteLine("Введите числа для сортировки (вводите только значение):");
            Console.WriteLine("Ввод завершится при вводе '---'.");
            while (true)
            {
                Console.Write("Введите число: ");
                string input = Console.ReadLine();
                if (input == "---")
                {
                    break;
                }
                // Попытка преобразовать введенное значение в число
                if (int.TryParse(input, out int value))
                {
                    numbersList.Add(value);
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите целое число.");
                }
            }
            if (numbersList.Count == 0) // Проверка на пустой массив
            {
                Console.WriteLine("Вы не ввели ни одного числа. Пожалуйста, попробуйте снова.");
                continue; // Возврат к началу цикла
            }
            int[] numbers = numbersList.ToArray();
            Console.WriteLine("Выберите метод сортировки:");
            Console.WriteLine("1. Сортировка пузырьком");
            Console.WriteLine("2. Быстрая сортировка");
            Console.WriteLine("3. Сортировка вставками");
            Console.WriteLine("4. Сортировка выбором");
            Console.Write("Введите номер метода (1-4): ");
            string choice = Console.ReadLine();
            SortDelegate sortMethod = choice switch
            {
                "1" => BubbleSort,
                "2" => QuickSort,
                "3" => InsertionSort,
                "4" => SelectionSort,
                _ => throw new InvalidOperationException("Неверный выбор метода сортировки.")
            };
            // Выполнение сортировки
            sortMethod(numbers);
            // Вывод отсортированного массива в формате a[N] = значение
            Console.WriteLine("Отсортированные числа:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine($"a[{i + 1}] = {numbers[i]}");
            }
            // Запрос на повторный ввод
            Console.WriteLine("\nХотите продолжить? (да/нет): ");
            string continueInput = Console.ReadLine();
            if (continueInput?.ToLower() != "да")
            {
                break; // Выход из основного цикла
            }
        }
    }
    // Метод сортировки пузырьком
    static void BubbleSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    // Меняем местами
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
    // Метод быстрой сортировки
    static void QuickSort(int[] array)
    {
        QuickSortHelper(array, 0, array.Length - 1);
    }
    // Вспомогательный метод для быстрой сортировки
    static void QuickSortHelper(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high);
            QuickSortHelper(array, low, pivotIndex - 1);
            QuickSortHelper(array, pivotIndex + 1, high);
        }
    }
    // Метод разделения для быстрой сортировки
    static int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                // Меняем местами
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
        // Меняем местами элемент с индексом i+1 и pivot
        int temp1 = array[i + 1];
        array[i + 1] = array[high];
        array[high] = temp1;
        return i + 1;
    }
    // Метод сортировки вставками
    static void InsertionSort(int[] array)
    {
        int n = array.Length;
        for (int i = 1; i < n; i++)
        {
            int key = array[i];
            int j = i - 1;
            // Перемещение элементов, которые больше ключа, на одну позицию вперед
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }
    // Метод сортировки выбором
    static void SelectionSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            // Находим минимальный элемент в неотсортированной части массива
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }
            // Меняем местами найденный минимальный элемент с первым элементом
            int temp = array[minIndex];
            array[minIndex] = array[i];
            array[i] = temp;
        }
    }
}
