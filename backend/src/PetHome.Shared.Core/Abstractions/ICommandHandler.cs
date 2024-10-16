
using PetHome.Shared.Core.Shared;

namespace PetHome.Shared.Core.Abstractions
{
    public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
    {
        public Task<Result<TResponse>> Execute(TCommand command, CancellationToken token);
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        public Task<Result> Execute(TCommand command, CancellationToken token);
    }
}
