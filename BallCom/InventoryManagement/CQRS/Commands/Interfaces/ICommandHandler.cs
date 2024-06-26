namespace InventoryManagement.CQRS.Commands.Interfaces
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand request);
    }
}
