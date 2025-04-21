public class Process
{
    public int Id { get; set; }
    public double ArrivalTime { get; set; }
    public double BurstTime { get; set; }
    public double RemainingTime { get; set; }
    public double WaitingTime { get; set; }
    public double LastPreempted { get; set; }
    public double TurnaroundTime => BurstTime + WaitingTime;
}