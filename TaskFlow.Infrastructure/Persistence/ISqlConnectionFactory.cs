using System.Data;


namespace TaskFlow.Infrastructure.Persistence
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();

    }
}
