using System;
using System.Collections.Generic;
using System.Linq;

namespace OrganizerApp
{
    class TaskItem
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{Title,-20} | До: {DueDate:dd.MM.yyyy} | {(IsCompleted ? "Виконано" : "Невиконано")}";
        }
    }

    class Program
    {
        static void Main()
        {
            List<TaskItem> tasks = new List<TaskItem>
            {
                new TaskItem { Title = "Підготувати звіт", DueDate = DateTime.Today.AddDays(2), IsCompleted = false },
                new TaskItem { Title = "Купити продукти", DueDate = DateTime.Today.AddDays(1), IsCompleted = true },
                new TaskItem { Title = "Прочитати книгу", DueDate = DateTime.Today.AddDays(5), IsCompleted = false },
                new TaskItem { Title = "Сходити у спортзал", DueDate = DateTime.Today.AddDays(3), IsCompleted = false },
                new TaskItem { Title = "Написати лабораторну", DueDate = DateTime.Today, IsCompleted = true }
            };

            Console.WriteLine("=== Усі завдання ===");
            tasks.ForEach(t => Console.WriteLine(t));

            Console.WriteLine("\n=== Завдання, які ще не виконані ===");
            var notCompleted = tasks
                .Where(t => !t.IsCompleted)         
                .Select(t => t.Title)               
                .Take(3);                           

            foreach (var title in notCompleted)
                Console.WriteLine($"- {title}");

            Console.WriteLine("\n=== Сортування за датою, а потім за назвою ===");
            var sortedTasks = tasks
                .OrderBy(t => t.DueDate)           
                .ThenBy(t => t.Title)              
                .Reverse();                         

            foreach (var t in sortedTasks)
                Console.WriteLine(t);

            Console.WriteLine("\n=== Перевірка виконаних завдань ===");
            var firstDone = tasks.FirstOrDefault(t => t.IsCompleted);
            Console.WriteLine($"Перше виконане завдання: {firstDone?.Title}");

            bool hasLateTasks = tasks.Any(t => t.DueDate < DateTime.Today && !t.IsCompleted);
            Console.WriteLine($"Є прострочені завдання: {hasLateTasks}");

            Console.WriteLine("\n=== Групування завдань за статусом ===");
            var grouped = tasks.GroupBy(t => t.IsCompleted);
            foreach (var group in grouped)
            {
                Console.WriteLine(group.Key ? "\nВиконані:" : "\nНевиконані:");
                foreach (var task in group)
                    Console.WriteLine($"- {task.Title}");
            }
        }
    }
}
