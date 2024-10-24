using PetHome.Shared.Core.Messaging;
using System.Threading.Channels;

namespace PetHome.Shared.Framework.MessageQueues
{
    public class MemoryCleanerQueue<TValue> : IMessageQueue<TValue>
    {
        private readonly Channel<IEnumerable<TValue>> _channel =
            Channel.CreateUnbounded<IEnumerable<TValue>>();

        public async Task WriteAsync(IEnumerable<TValue> paths, CancellationToken token)
        {
            await _channel.Writer.WriteAsync(paths, token);
        }

        public async Task<IEnumerable<TValue>> ReadAsync(CancellationToken token)
        {
            return await _channel.Reader.ReadAsync(token);
        }
    }
}
