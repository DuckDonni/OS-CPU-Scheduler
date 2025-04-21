using System;
using System.Collections.Generic;
using System.Linq;

namespace CpuSchedulingConsole
{
    public static class Algorithms
    {
        public static void FCFSAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            var processes = new List<Process>();
            double currentTime = 0;
            double totalWait = 0;
            double totalTurnaround = 0;
            var timeline = new List<string>();

            // Input processes
            for(int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());
                
                processes.Add(new Process {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst
                });
            }

            // Sort by arrival time
            var sorted = processes.OrderBy(p => p.ArrivalTime).ToList();

            // Build timeline and calculate metrics
            foreach(var p in sorted)
            {
                if(currentTime < p.ArrivalTime)
                    currentTime = p.ArrivalTime;

                timeline.Add($"{currentTime} P{p.Id}");
                p.WaitingTime = currentTime - p.ArrivalTime;
                totalWait += p.WaitingTime;
                
                currentTime += p.BurstTime;
                totalTurnaround += currentTime - p.ArrivalTime;
            }

            // Display results
            PrintMetrics(sorted, timeline, totalWait, totalTurnaround, currentTime);
        }

        public static void SJFAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            var processes = new List<Process>();
            double currentTime = 0;
            double totalWait = 0;
            double totalTurnaround = 0;
            var timeline = new List<string>();

            // Input processes
            for(int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());
                
                processes.Add(new Process {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst
                });
            }

            // SJF implementation
            var completed = 0;
            while(completed < np)
            {
                var available = processes
                    .Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0)
                    .OrderBy(p => p.RemainingTime)
                    .ToList();

                if(available.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                var currentProcess = available.First();
                timeline.Add($"{currentTime} P{currentProcess.Id}");
                
                currentProcess.WaitingTime += currentTime - currentProcess.LastPreempted;
                currentTime += currentProcess.RemainingTime;
                currentProcess.RemainingTime = 0;
                completed++;
                
                totalWait += currentProcess.WaitingTime;
                totalTurnaround += currentTime - currentProcess.ArrivalTime;
            }

            PrintMetrics(processes, timeline, totalWait, totalTurnaround, currentTime);
        }

        // Similar modifications for other algorithms...

        private static void PrintMetrics(List<Process> processes, List<string> timeline, 
            double totalWait, double totalTurnaround, double totalTime)
        {
            Console.WriteLine("\nExecution Timeline:");
            Console.WriteLine(string.Join(" ", timeline) + $" {totalTime}");

            Console.WriteLine("\nPerformance Metrics:");
            Console.WriteLine($"Average Waiting Time: {totalWait / processes.Count:F2}");
            Console.WriteLine($"Average Turnaround Time: {totalTurnaround / processes.Count:F2}");
            Console.WriteLine($"CPU Utilization: {(processes.Sum(p => p.BurstTime) / totalTime) * 100:F2}%");
            Console.WriteLine($"Throughput: {processes.Count / totalTime:F2} processes/time unit");
        }
    }

}