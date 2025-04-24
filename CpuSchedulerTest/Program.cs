using System;

namespace CpuSchedulingConsole
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("CPU Scheduling Algorithms");
                Console.WriteLine("1. FCFS");
                Console.WriteLine("2. SJF");
                Console.WriteLine("3. Priority");
                Console.WriteLine("4. Round Robin");
                Console.WriteLine("5. Longest Job First (LJF)");
                Console.WriteLine("6. Longest Remaining Job First (LRJF)");
                Console.WriteLine("7. Exit");
                Console.Write("Select algorithm: ");


                
                var choice = Console.ReadLine();

                if (choice == "7") 
                    break;
                

                Console.Write("Enter number of processes: ");
                var input = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Algorithms.FCFSAlgorithm(input);
                            break;
                        case "2":
                            Algorithms.SJFAlgorithm(input);
                            break;
                        case "3":
                            Algorithms.PriorityAlgorithm(input);
                            break;
                        case "4":
                            Algorithms.RoundRobinAlgorithm(input);
                            break;
                        case "5":
                            Algorithms.LJFAlgorithm(input);
                            break;
                        case "6":
                            Algorithms.LRJFAlgorithm(input);
                            break;
                        default:
                            Console.WriteLine("Invalid choice!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
