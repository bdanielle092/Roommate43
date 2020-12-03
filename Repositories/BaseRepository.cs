using Microsoft.Data.SqlClient;

namespace Roommate43.Repositories
{
 
    //  A base class for every other Repository class to inherit from.
    //  This class is responsible for providing a database connection to each of the repository subclasses
    public class BaseRepository
    {
        ///  A "connection string" is the address of the database.
        /// this is a field
        private string _connectionString;

        ///  This constructor will be invoked by subclasses.
        ///  It will save the connection string for later use.
        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        ///  Represents a connection to the database.
        ///   This is a "tunnel" to connect the application to the database.
        ///   All communication between the application and database passes through this connection.
        protected SqlConnection Connection => new SqlConnection(_connectionString);
    }
}
