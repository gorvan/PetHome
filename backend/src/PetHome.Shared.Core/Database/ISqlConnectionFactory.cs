using System.Data;

namespace PetHome.Shared.Core.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();
    }
}
