using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace taskCancellationToken
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task myTask = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }

                token.ThrowIfCancellationRequested();

            }, token);
            try
            {

                Console.WriteLine("Press enter to stop the process:");
                Console.ReadLine();
                cancellationTokenSource.Cancel();
                myTask.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0].Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine("other expection: " + ex.Message);
            }

            Console.WriteLine("Press enter to end the program");
            Console.ReadLine();
        }
    }
}