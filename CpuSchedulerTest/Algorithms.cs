using System;
using System.Collections.Generic;

namespace CpuSchedulingConsole
{
    public static class Algorithms
    {
        public static void FCFSAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double twt = 0.0, awt;

            Console.WriteLine("First Come First Serve Scheduling (Y/N)?");
            string response = Console.ReadLine()?.Trim().ToUpper();

            if (response == "Y")
            {
                for (int num = 0; num < np; num++)
                {
                    Console.Write($"Enter Burst time for P{num + 1}: ");
                    bp[num] = Convert.ToDouble(Console.ReadLine());
                }

                for (int num = 0; num < np; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        Console.WriteLine($"Waiting time for P{num + 1} = {wtp[num]}");
                    }
                }

                for (int num = 0; num < np; num++)
                {
                    twt += wtp[num];
                }
                
                awt = twt / np;
                Console.WriteLine($"Average waiting time for {np} processes = {awt} sec(s)");
            }
        }

        public static void SJFAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt;

            Console.WriteLine("Shortest Job First Scheduling (Y/N)?");
            string response = Console.ReadLine()?.Trim().ToUpper();

            if (response == "Y")
            {
                for (int num = 0; num < np; num++)
                {
                    Console.Write($"Enter Burst time for P{num + 1}: ");
                    bp[num] = Convert.ToDouble(Console.ReadLine());
                    p[num] = bp[num];
                }

                Array.Sort(p);

                bool found = false;
                for (int num = 0; num < np; num++)
                {
                    if (num == 0)
                    {
                        for (int x = 0; x < np; x++)
                        {
                            if (p[num] == bp[x] && !found)
                            {
                                wtp[num] = 0;
                                Console.WriteLine($"Waiting time for P{x + 1} = {wtp[num]}");
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (int x = 0; x < np; x++)
                        {
                            if (p[num] == bp[x] && !found)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                Console.WriteLine($"Waiting time for P{x + 1} = {wtp[num]}");
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }

                foreach (var t in wtp)
                {
                    twt += t;
                }
                
                awt = twt / np;
                Console.WriteLine($"Average waiting time for {np} processes = {awt} sec(s)");
            }
        }

        public static void PriorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            int[] p = new int[np];
            int[] sp = new int[np];
            double twt = 0.0, awt;

            Console.WriteLine("Priority Scheduling (Y/N)?");
            string response = Console.ReadLine()?.Trim().ToUpper();

            if (response == "Y")
            {
                for (int num = 0; num < np; num++)
                {
                    Console.Write($"Enter Burst time for P{num + 1}: ");
                    bp[num] = Convert.ToDouble(Console.ReadLine());
                    Console.Write($"Enter Priority for P{num + 1}: ");
                    p[num] = Convert.ToInt32(Console.ReadLine());
                    sp[num] = p[num];
                }

                Array.Sort(sp);
                
                bool found = false;
                int tempIndex = 0;
                for (int num = 0; num < np; num++)
                {
                    if (num == 0)
                    {
                        for (int x = 0; x < np; x++)
                        {
                            if (sp[num] == p[x] && !found)
                            {
                                wtp[num] = 0;
                                Console.WriteLine($"Waiting time for P{x + 1} = {wtp[num]}");
                                tempIndex = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (int x = 0; x < np; x++)
                        {
                            if (sp[num] == p[x] && !found)
                            {
                                wtp[num] = wtp[num - 1] + bp[tempIndex];
                                Console.WriteLine($"Waiting time for P{x + 1} = {wtp[num]}");
                                tempIndex = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }

                foreach (var t in wtp)
                {
                    twt += t;
                }
                
                awt = twt / np;
                Console.WriteLine($"Average waiting time for {np} processes = {awt} sec(s)");
            }
        }

        public static void RoundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrivalTime = new double[10];
            double[] burstTime = new double[10];
            double[] temp = new double[10];
            double timeQuantum;
            double total = 0.0;
            double waitTime = 0, turnaroundTime = 0;

            Console.WriteLine("Round Robin Scheduling (Y/N)?");
            string response = Console.ReadLine()?.Trim().ToUpper();

            if (response == "Y")
            {
                for (int i = 0; i < np; i++)
                {
                    Console.Write($"Enter arrival time for P{i + 1}: ");
                    arrivalTime[i] = Convert.ToDouble(Console.ReadLine());
                    Console.Write($"Enter burst time for P{i + 1}: ");
                    burstTime[i] = Convert.ToDouble(Console.ReadLine());
                    temp[i] = burstTime[i];
                }

                Console.Write("Enter time quantum: ");
                timeQuantum = Convert.ToDouble(Console.ReadLine());

                int x = np;
                for (int i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total += temp[i];
                        temp[i] = 0;
                        x--;
                        Console.WriteLine($"Turnaround time for Process {i + 1}: {total - arrivalTime[i]}");
                        Console.WriteLine($"Wait time for Process {i + 1}: {total - arrivalTime[i] - burstTime[i]}");
                        turnaroundTime += total - arrivalTime[i];
                        waitTime += total - arrivalTime[i] - burstTime[i];
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] -= timeQuantum;
                        total += timeQuantum;
                    }

                    i = (i == np - 1) ? 0 : i + 1;
                }

                double averageWaitTime = waitTime / np;
                double averageTurnaroundTime = turnaroundTime / np;
                Console.WriteLine($"Average wait time for {np} processes: {averageWaitTime} sec(s)");
                Console.WriteLine($"Average turnaround time for {np} processes: {averageTurnaroundTime} sec(s)");
            }
        }
    }
}