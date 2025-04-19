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
                Console.WriteLine("5. SRTF (New)");
                Console.WriteLine("6. MLFQ (New)");
                Console.WriteLine("7. Exit");
                Console.Write("Select algorithm: ");

                var choice = Console.ReadLine();
                if(choice == "7") break;

                Console.Write("Enter number of processes: ");
                var userinput = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        Algorithms.FCFSAlgorithm(userinput);
                        break;
                    case "2":
                        Algorithms.SJFAlgorithm(userinput);
                        break;
                    case "3":
                        Algorithms.PriorityAlgorithm(userinput);
                        break;
                    case "4":
                        Algorithms.RoundRobinAlgorithm(userinput);
                        break;
                    case "5":
                        //Algorithms.SRTFAlgorithm(userinput);
                        break;
                    case "6":
                        //Algorithms.MLFQAlgorithm(userinput);
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}