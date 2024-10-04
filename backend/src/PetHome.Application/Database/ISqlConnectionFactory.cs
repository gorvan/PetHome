using System.Data;

namespace PetHome.Application.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();
    }
}
