using System.Data;


namespace TaskFlow.Infrastructure.Persistance
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();

    }
}
