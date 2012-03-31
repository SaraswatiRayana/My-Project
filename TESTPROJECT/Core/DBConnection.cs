using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace MitraTech.Core
{
    public class DBConnectionObject
    {
        private string _connectionString = string.Empty; 
        private SqlConnection _connection;

        public DBConnectionObject()
        {
            _connectionString = "Server=localhost\\sqlexpress;Database=SEKOLAHMINGGU;User ID=Admin;Password=Admin;";
            _connection = new SqlConnection(_connectionString);
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public SqlDataReader ExecuteReader(QueryParameter queryParam)
        {
            this.Open();
            SqlCommand command = new SqlCommand(queryParam.commandText);
            command.Connection = _connection;
            command.Parameters.AddRange(queryParam.parameters.ToArray());
            SqlDataReader reader;
            
            try
            {
                reader = command.ExecuteReader();
            }
            catch (SqlException) {throw;}
            
            finally
            {
                command.Dispose();                
            }
            
            return reader;

        }

        public int ExecuteNonQuery(QueryParameter queryParam)
        {
            this.Open();
            SqlCommand command = new SqlCommand(queryParam.commandText);
            command.Connection = _connection;
            command.Parameters.AddRange(queryParam.parameters.ToArray());
            int rowCount=0;

            try
            {
                rowCount = command.ExecuteNonQuery();
            }
            catch (SqlException) { throw; }
            finally{
                command.Dispose();
                this.Close();
            }
                     
            return rowCount;
        }

        public void ExecuteNonQuery(List<QueryParameter> queryParams)
        {
            this.Open();
            SqlCommand command = _connection.CreateCommand();
            SqlTransaction transaction = _connection.BeginTransaction();
            int rowCount = 0;

            command.Connection = _connection;
            command.Transaction = transaction;
            try
            {
                foreach (QueryParameter queryParam in queryParams)
                {
                    command.CommandText = queryParam.commandText;
                    command.Parameters.AddRange(queryParam.parameters.ToArray());

                    rowCount =+ command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                command.Transaction.Commit();           
            }
            catch (SqlException) 
            {
                transaction.Rollback();
                throw; 
            }
            finally
            {
                command.Dispose();
                this.Close();
            }
            
        }
    }
    public class QueryParameter
    {
        public string commandText;
        public List<SqlParameter> parameters;
    }

}
