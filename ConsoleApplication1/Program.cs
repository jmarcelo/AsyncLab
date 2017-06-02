using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
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
                // Chamada assíncrona
                Task task = prog.ExecutaTarefasAsync();
                task.Wait();

                // Chamada síncrona
                //prog.ExecutaTarefas();

                Console.WriteLine("Tempo de execução: {0}", DateTime.Now - inicio);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        void ExecutaTarefas()
        {
            Console.WriteLine("Criando as tasks...");
            var tasks = new List<Task>();
            for (int i = 1; i <= NUM_TASKS; i++)
            {
                tasks.Add(ProcessamentoAsync(i));
            }

            Console.WriteLine("Vai aguardar...");
            foreach(var task in tasks)
            {
                task.Wait();
            }

        }

        async Task ExecutaTarefasAsync()
        {
            Console.WriteLine("Criando as tasks...");
            var tasks = new List<Task>();
            for (int i = 1; i <= NUM_TASKS; i++)
            {
                tasks.Add(ProcessamentoAsync(i));
            }

            Console.WriteLine("Vai aguardar...");
            foreach (var task in tasks)
            {
                await task;
            }
        }

        async Task ProcessamentoAsync(int id)
        {
            int delay = rnd.Next(MAX_SECONDS) + 1;
            await Task.Delay(delay * 1000);
            Console.WriteLine("Concluída task {0} em {1}s.", id, delay);
        }

    }
}
