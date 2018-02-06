using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product.Models
{
    public class Product
    {
        private string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }


        public bool IsProductCodeExit(string productCode)
        {
            SqlConnection connection = new SqlConnection(conString);
            string query = "SELECT Count(*) FROM Products Where ProductCode='" + productCode + "' ";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAff = (int)command.ExecuteScalar();
            connection.Close();
            if (rowAff > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string SaveOrProduct(Product product)
        {
            string msg = " ";


            if (product.ProductCode.Length != 3)
            {
                msg = "Product code must be 3 character long";
            }
            else if (IsProductCodeExit(product.ProductCode))
            {
                msg = UpdateProduct(product);
            }
            else
            {
                msg = SaveProduct(product);
            }
            return msg;
        }

        private string SaveProduct(Product product)
        {
            string msg = " ";
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand command = new SqlCommand("spInsertProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            connection.Open();
            int rowAff = command.ExecuteNonQuery();
            connection.Close();
            if (rowAff > 0)
            {
                msg = "Successfully";
            }
            else
            {
                msg = "Failed";
            }

            return msg;
        }

        public string UpdateProduct(Product product)
        {
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand command = new SqlCommand("spUpdateProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            connection.Open();
            int rowAff = command.ExecuteNonQuery();
            connection.Close();
            string msg = " ";
            if (rowAff > 0)
            {
                msg = "Successfully Updated";
            }
            else
            {
                msg = "Failed";
            }
            return msg;
        }

        public List<Product> GetProductList()
        {
            SqlConnection connection = new SqlConnection(conString);
            string query = "SELECT *FROM Products";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product();
                product.Id = (int)reader["Id"];
                product.ProductCode = reader["ProductCode"].ToString();
                product.Description = reader["Description"].ToString();
                product.Quantity = (int)reader["Quantity"];
                products.Add(product);
            }
            connection.Close();
            return products;
        }
        
    }
}








