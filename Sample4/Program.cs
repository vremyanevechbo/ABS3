using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    // Класс для хранения задачи
    public class TaskItem
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TaskItem(string title, DateTime date)
        {
            Title = title;
            Date = date;
        }
        public override string ToString() => $"{Title} - {Date.ToShortDateString()}";
    }
    // Класс для управления задачами
    class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        // Метод для добавления задачи
        public void AddTask(string title, DateTime date)
        {
            tasks.Add(new TaskItem(title, date));
            Console.WriteLine($"Задача добавлена: {title} - {date.ToShortDateString()}");
        }
        // Метод для сортировки задач по дате
        public List<TaskItem> SortTasksByDate()
        {
            return tasks.OrderBy(task => task.Date).ToList();
        }
        // Метод для сортировки задач по заголовку
        public List<TaskItem> SortTasksByTitle()
        {
            return tasks.OrderBy(task => task.Title).ToList();
        }
        public void DisplayTasks(List<TaskItem> items)
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Нет задач для отображения.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
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
            Console.WriteLine("2. Сортировать задачи по дате");
            Console.WriteLine("3. Сортировать задачи по алфавиту");
            Console.WriteLine("4. Выход");
            Console.Write("Введите номер действия: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Введите описание задачи: ");
                    string taskDescription = Console.ReadLine();
                    Console.Write("Введите дату (ГГГГ-ММ-ДД): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime taskDate))
                    {
                        taskManager.AddTask(taskDescription, taskDate);
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат даты.");
                    }
                    break;
                case "2":
                    var sortedByDate = taskManager.SortTasksByDate();
                    taskManager.DisplayTasks(sortedByDate);
                    break;
                case "3":
                    var sortedByTitle = taskManager.SortTasksByTitle();
                    taskManager.DisplayTasks(sortedByTitle);
                    break;
                case "4":
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