namespace Tasklyne.Domain.Entities;

public class Enums
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        OnHold,
        Cancelled
    }
    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
    public enum ReviewStatus
    {
        NotSubmitted,
        Pending,
        Approved,
        Rejected
    }
}
