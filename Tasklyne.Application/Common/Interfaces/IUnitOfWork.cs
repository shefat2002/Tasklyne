namespace Tasklyne.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task SaveAsync();
}