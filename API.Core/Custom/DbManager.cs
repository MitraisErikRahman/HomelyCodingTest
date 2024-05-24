using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using static API.Core.Models.Enums;

namespace API.Core.Custom
{
    public class DbManager : IDbManager
    {
        #region Variables Declaration
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        /// <summary>
        /// DBManager class constructor
        /// </summary>
        /// <param name="name">Database name</param>
        /// <param name="accessLevel">Database access level</param>
        /// <param name="configuration">Configuration interface</param>
        public DbManager(EnumDB name, DbAccessLevel accessLevel, IConfiguration configuration)
        {
            _configuration = configuration;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(name.ToString());

            if (accessLevel == DbAccessLevel.WRITE)
            {
                stringBuilder.Append("Write");
            }
            else
            {
                stringBuilder.Append("Read");
            }

            _connectionString = ConfigurationExtensions.GetConnectionString(configuration, stringBuilder.ToString()).ToString();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get database connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            return sqlConnection;
        }

        /// <summary>
        /// Open database connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetOpenConnection()
        {
            var sqlConnection = GetConnection();
            sqlConnection.Open();
            return sqlConnection;
        } 
        #endregion
    }
}
