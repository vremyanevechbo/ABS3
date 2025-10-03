using System;
using System.Collections.Generic;
class Program
{
    // Определение делегата
    public delegate void TaskDelegate(string message);
    // Класс для управления задачами
    class TaskManager
    {
        private List<string> tasks = new List<string>();
        private List<string> journal = new List<string>(); // Список для хранения записей в журнале
        // Метод для добавления задачи
        public void AddTask(string task)
        {
            tasks.Add(task);
            Console.WriteLine($"Задача добавлена: {task}");
        }
        // Метод для выполнения всех задач с выбранным делегатом
        public void ExecuteTasks(TaskDelegate taskDelegate)
        {
            foreach (var task in tasks)
            {
                taskDelegate(task);
                if (taskDelegate.Method.Name == nameof(LogTask))
                {
                    journal.Add(task); // Добавляем в журнал, если вызван метод LogTask
                }
            }
        }
        // Метод для вывода содержимого журнала
        public void DisplayJournal()
        {
            if (journal.Count == 0)
            {
                Console.WriteLine("Журнал пуст.");
            }
            else
            {
                Console.WriteLine("Содержимое журнала:");
                foreach (var entry in journal)
                {
                    Console.WriteLine($"- {entry}");
                }
            }
        }
    }
    // Метод для отправки уведомления
    static void SendNotification(string task)
    {
        Console.WriteLine($"Уведомление: Задача \"{task}\" выполнена.");
    }
    // Метод для записи в журнал
    static void LogTask(string task)
    {
        Console.WriteLine($"Журнал: Задача \"{task}\" записана в журнал.");
    }
    // Метод для выполнения базовых математических операций
    static void PerformMathOperation(string operation)
    {
        Console.Write("Введите первое число: ");
        double num1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите второе число: ");
        double num2 = Convert.ToDouble(Console.ReadLine());
        double result = operation switch
        {
            "сложение" => num1 + num2,
            "вычитание" => num1 - num2,
            "умножение" => num1 * num2,
            "деление" => num1 / num2,
            _ => double.NaN
        };
        if (double.IsNaN(result))
        {
            Console.WriteLine("Неверная операция!");
        }
        else
        {
            Console.WriteLine($"Результат {operation}: {result}");
        }
    }
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Выполнить задачи с уведомлением");
            Console.WriteLine("3. Выполнить задачи с записью в журнал");
            Console.WriteLine("4. Просмотреть журнал");
            Console.WriteLine("5. Выполнить математическую операцию");
            Console.WriteLine("6. Выход");
            Console.Write("Введите номер действия: ");
            string choice = Console.ReadLine();
            Console.Clear(); // Очищаем консоль после выбора
            switch (choice)
            {
                case "1":
                    Console.Write("Введите описание задачи: ");
                    string taskDescription = Console.ReadLine();
                    taskManager.AddTask(taskDescription);
                    break;
                case "2":
                    taskManager.ExecuteTasks(SendNotification);
                    break;
                case "3":
                    taskManager.ExecuteTasks(LogTask);
                    break;
                case "4":
                    taskManager.DisplayJournal();
                    break;
                case "5":
                    Console.WriteLine("Выберите математическую операцию: сложение, вычитание, умножение, деление");
                    string operation = Console.ReadLine();
                    PerformMathOperation(operation);
                    break;
                case "6":
                    Console.WriteLine("Выход из программы.");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                    break;
            }
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}