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
            var timeline = new List<string>();
            for (int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());

                processes.Add(new Process
                {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst
                });
            }

            var sorted = processes.OrderBy(p => p.ArrivalTime).ToList();

            foreach (var p in sorted)
            {
                if (currentTime < p.ArrivalTime)
                    currentTime = p.ArrivalTime;

                timeline.Add($"{currentTime} P{p.Id}");
                p.WaitingTime = currentTime - p.ArrivalTime;
                currentTime += p.BurstTime;
                p.TurnaroundTime = currentTime - p.ArrivalTime;
            }

            PrintMetrics(sorted, timeline, currentTime);
        }

        public static void SJFAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            var processes = new List<Process>();
            double currentTime = 0;
            var timeline = new List<string>();

            for (int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());

                processes.Add(new Process
                {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst
                });
            }

            int completed = 0;
            while (completed < np)
            {
                var available = processes
                    .Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0)
                    .OrderBy(p => p.RemainingTime)
                    .ToList();

                if (available.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                var currentProcess = available.First();
                timeline.Add($"{currentTime} P{currentProcess.Id}");

                currentProcess.WaitingTime = currentTime - currentProcess.ArrivalTime;

                currentTime += currentProcess.RemainingTime;
                currentProcess.RemainingTime = 0;
                currentProcess.TurnaroundTime = currentTime - currentProcess.ArrivalTime;
                completed++;
            }

            PrintMetrics(processes, timeline, currentTime);
        }

        public static void PriorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            var processes = new List<Process>();
            double currentTime = 0;
            var timeline = new List<string>();

            for (int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter priority for P{i + 1} (lower number = higher priority): ");
                var priority = Convert.ToInt32(Console.ReadLine());

                processes.Add(new Process
                {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst,
                    Priority = priority
                });
            }

            int completed = 0;
            while (completed < np)
            {
                var available = processes
                    .Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0)
                    .OrderBy(p => p.Priority)
                    .ThenBy(p => p.ArrivalTime)
                    .ToList();

                if (available.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                var currentProcess = available.First();
                timeline.Add($"{currentTime} P{currentProcess.Id}");

                currentProcess.WaitingTime = currentTime - currentProcess.ArrivalTime;

                currentTime += currentProcess.RemainingTime;
                currentProcess.RemainingTime = 0;
                currentProcess.TurnaroundTime = currentTime - currentProcess.ArrivalTime;
                completed++;
            }

            PrintMetrics(processes, timeline, currentTime);
        }
        public static void RoundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            var processes = new List<Process>();
            double currentTime = 0;
            var timeline = new List<string>();

            for (int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());

                processes.Add(new Process
                {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst
                });
            }

            Console.Write("Enter time quantum: ");
            var timeQuantum = Convert.ToDouble(Console.ReadLine());

            var queue = new Queue<Process>();
            int completed = 0;
            int lastProcessId = -1;

            while (completed < np)
            {
                foreach (var p in processes.Where(p =>
                    p.ArrivalTime <= currentTime &&
                    p.RemainingTime > 0 &&
                    !queue.Contains(p)))
                {
                    queue.Enqueue(p);
                }

                if (queue.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                var currentProcess = queue.Dequeue();
                if (lastProcessId != currentProcess.Id)
                {
                    timeline.Add($"{currentTime} P{currentProcess.Id}");
                    lastProcessId = currentProcess.Id;
                }

                var executionTime = Math.Min(timeQuantum, currentProcess.RemainingTime);
                currentProcess.RemainingTime -= executionTime;
                currentTime += executionTime;

                foreach (var p in processes.Where(p =>
                    p != currentProcess &&
                    p.ArrivalTime <= currentTime &&
                    p.RemainingTime > 0))
                {
                    p.WaitingTime += executionTime;
                }

                if (currentProcess.RemainingTime <= 0)
                {
                    completed++;
                    currentProcess.TurnaroundTime = currentTime - currentProcess.ArrivalTime;
                }
                else
                {
                    queue.Enqueue(currentProcess);
                }
            }

            PrintMetrics(processes, timeline, currentTime);
        }
        public static void LJFAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            var processes = new List<Process>();
            double currentTime = 0;
            var timeline = new List<string>();

            // Input processes
            for (int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());

                processes.Add(new Process
                {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst
                });
            }

            // LJF scheduling
            int completed = 0;
            while (completed < np)
            {
                var available = processes
                    .Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0)
                    .OrderByDescending(p => p.BurstTime)
                    .ThenBy(p => p.ArrivalTime)
                    .ToList();

                if (available.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                var currentProcess = available.First();
                timeline.Add($"{currentTime} P{currentProcess.Id}");

                currentProcess.WaitingTime = currentTime - currentProcess.ArrivalTime;
                currentTime += currentProcess.RemainingTime;
                currentProcess.RemainingTime = 0;
                currentProcess.TurnaroundTime = currentTime - currentProcess.ArrivalTime;
                completed++;
            }

            PrintMetrics(processes, timeline, currentTime);
        }

        // Longest Remaining Job First (Preemptive)
        public static void LRJFAlgorithm(string userInput)
        {
            int np = Convert.ToInt32(userInput);
            var processes = new List<Process>();
            double currentTime = 0;
            var timeline = new List<string>();
            Process currentProcess = null;

            // Input processes
            for (int i = 0; i < np; i++)
            {
                Console.Write($"Enter arrival time for P{i + 1}: ");
                var arrival = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter burst time for P{i + 1}: ");
                var burst = Convert.ToDouble(Console.ReadLine());

                processes.Add(new Process
                {
                    Id = i + 1,
                    ArrivalTime = arrival,
                    BurstTime = burst,
                    RemainingTime = burst
                });
            }

            // LRJF scheduling
            int completed = 0;
            while (completed < np)
            {
                var candidates = processes
                    .Where(p => p.ArrivalTime <= currentTime && p.RemainingTime > 0)
                    .OrderByDescending(p => p.RemainingTime)
                    .ThenBy(p => p.ArrivalTime)
                    .ToList();

                if (candidates.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                var nextProcess = candidates.First();

                if (currentProcess != nextProcess)
                {
                    timeline.Add($"{currentTime} P{nextProcess.Id}");
                    currentProcess = nextProcess;
                }

                currentProcess.RemainingTime--;
                currentTime++;

                foreach (var p in processes.Where(p =>
                    p != currentProcess &&
                    p.ArrivalTime <= currentTime &&
                    p.RemainingTime > 0))
                {
                    p.WaitingTime++;
                }

                if (currentProcess.RemainingTime == 0)
                {
                    completed++;
                    currentProcess.TurnaroundTime = currentTime - currentProcess.ArrivalTime;
                }
            }

            PrintMetrics(processes, timeline, currentTime);
        }
        private static void PrintMetrics(List<Process> processes, List<string> timeline, double totalTime)
        {
            Console.WriteLine("\nExecution Timeline:");
            Console.WriteLine(string.Join(" ", timeline) + $" {totalTime}");

            Console.WriteLine("\nPerformance Metrics:");
            Console.WriteLine($"Average Waiting Time: {processes.Average(p => p.WaitingTime):F2}");
            Console.WriteLine($"Average Turnaround Time: {processes.Average(p => p.TurnaroundTime):F2}");
            Console.WriteLine($"CPU Utilization: {processes.Sum(p => p.BurstTime) / totalTime * 100:F2}%");
            Console.WriteLine($"Throughput: {processes.Count / totalTime:F2} processes/time unit\n");
        }

    }

    public class Process
    {
        public int Id { get; set; }
        public double ArrivalTime { get; set; }
        public double BurstTime { get; set; }
        public double RemainingTime { get; set; }
        public double WaitingTime { get; set; }
        public double TurnaroundTime { get; set; }
        public int Priority { get; set; }
    }
}