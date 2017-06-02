using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncLab
{
    class Program
    {
        private Random rnd = new Random();
        private const int MAX_SECONDS = 10;
        private const int NUM_TASKS = 10;
        static void Main(string[] args)
        {
            var prog = new Program();

            while (true)
            {
                var inicio = DateTime.Now;
                
                // Asynchronous call
                //Task task = prog.ExecuteBigTaskAsync();
                //task.Wait();

                // Synchronous call
                prog.ExecuteBigTask();

                Console.WriteLine("Execution time: {0}", DateTime.Now - inicio);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        void ExecuteBigTask()
        {
            Console.WriteLine("Starting the tasks...");
            var tasks = new List<Task>();
            for (int i = 1; i <= NUM_TASKS; i++)
            {
                tasks.Add(ExecuteSmallTaskAsync(i));
            }

            Console.WriteLine("Waiting for the tasks to finish...");
            foreach(var task in tasks)
            {
                task.Wait();
            }

        }

        async Task ExecuteBigTaskAsync()
        {
            Console.WriteLine("Starting the tasks...");
            var tasks = new List<Task>();
            for (int i = 1; i <= NUM_TASKS; i++)
            {
                tasks.Add(ExecuteSmallTaskAsync(i));
            }

            Console.WriteLine("Waiting for the tasks to finish...");
            foreach (var task in tasks)
            {
                await task;
            }
        }

        async Task ExecuteSmallTaskAsync(int id)
        {
            int delay = rnd.Next(MAX_SECONDS) + 1;
            await Task.Delay(delay * 1000); // here it would do some real work...
            Console.WriteLine("Task {0} finnished in {1} seconds.", id, delay);
        }

    }
}
