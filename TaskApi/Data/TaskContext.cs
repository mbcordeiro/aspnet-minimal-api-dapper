using System.Data;

namespace TaskApi.Data
{
    public class TaskContext
    {
        public delegate Task<IDbConnection> GetConnection();
    }
}
