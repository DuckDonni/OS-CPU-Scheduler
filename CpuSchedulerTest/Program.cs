using System;

namespace CpuSchedulingConsole
{
     class Program
    {
        static void Main()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("CPU Scheduling Algorithms");
                Console.WriteLine("1. FCFS");
                Console.WriteLine("2. SJF");
                Console.WriteLine("3. Priority");
                Console.WriteLine("4. Round Robin");
                Console.WriteLine("5. Exit");
                Console.Write("Select algorithm: ");

                var choice = Console.ReadLine();
                if(choice == "5") break;

                Console.Write("Enter number of processes: ");
                var input = Console.ReadLine();

                try 
                {
                    switch(choice)
                    {
                        case "1":
                            Algorithms.FCFSAlgorithm(input);
                            break;
                        case "2":
                            Algorithms.SJFAlgorithm(input);
                            break;
                        // Add other cases
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
