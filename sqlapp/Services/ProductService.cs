using Microsoft.AspNetCore.Identity;
using sqlapp.Models;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace sqlapp.Services
{

    // This service will interact with our Product data in the SQL database
    public class ProductService
    {
        private static string db_source = "mydemodbnamemanu.database.windows.net";
        private static string db_user = "ManuMP";
        private static string db_password = "Staple333#";
        private static string db_database = "MyDemoDB";

        private SqlConnection GetConnection()
        {

            //var _builder = new SqlConnectionStringBuilder();
            //_builder.DataSource = db_source;
            //_builder.UserID = db_user;
            //_builder.Password = db_password;
            //_builder.InitialCatalog = db_database;
            //return new SqlConnection(_builder.ConnectionString);

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;

            var connectionString = builder.ConnectionString;

            return new SqlConnection(builder.ConnectionString);
        }
        public List<Product> GetProducts()
        {
            List<Product> _product_lst = new List<Product>();
            string _statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection _connection = GetConnection();

            _connection.Open();

            SqlCommand sqlCommand = new SqlCommand(_statement, _connection);
            using(SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while(reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _product_lst.Add(_product);
                }
            }

            _connection.Close();
            return _product_lst;
        }

    }
}

