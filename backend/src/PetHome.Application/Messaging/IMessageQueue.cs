namespace PetHome.Application.Messaging
{
    public interface IMessageQueue<TMessage>
    {
        Task WriteAsync(IEnumerable<TMessage> paths, CancellationToken token);
        Task<IEnumerable<TMessage>> ReadAsync(CancellationToken token);
    }
}
