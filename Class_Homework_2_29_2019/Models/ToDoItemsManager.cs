using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Class_Homework_2_29_2019.Models
{
    public class ToDoItemsManager
    {
        private string _connectionstring;
        public ToDoItemsManager(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public IEnumerable<Categories> GetAllIncomplete()
        {
            SqlConnection connection = new SqlConnection(_connectionstring);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"select c.Name, T.Title, T.DueDate, T.CompletedDate,T.CategoryId, T.Id
                                From Categories c
                                join ToDoItem T
                                on c.Id = t.CategoryId
                                where t.CompletedDate is null";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Categories> categories = new List<Categories>();
            while (reader.Read())
            {
                int id = (int)reader["CategoryId"];
                Categories c = categories.FirstOrDefault(C => id == C.Id);
                if (c == null)
                {
                    c = new Categories
                    {
                        Id = id,
                        Name = (string)reader["Name"]
                    };
                    categories.Add(c);
                }
                object M = reader["CompletedDate"];
                DateTime? completed = null;
                if (M != DBNull.Value)
                {
                    completed = (DateTime?)M;
                }
                c.ToDoItems.Add(new ToDoItem
                {
                    Id = (int?)reader["Id"],
                    CategoryId = id,
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    CompletedDate = completed
                });
            }
            connection.Close();
            connection.Dispose();

            return categories;
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            SqlConnection connection = new SqlConnection(_connectionstring);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"select * From Categories";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Categories> categories = new List<Categories>();
            while (reader.Read())
            {
                categories.Add(new Categories
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"]
                });
            }
            connection.Close();
            connection.Dispose();

            return categories;
        }

        public void AddCompletedDate(int id, DateTime date)
        {
            SqlConnection conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE ToDoItem
                                SET CompletedDate = @Date
                                Where Id = @Id";
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

        public void AddCategory(string name)
        {
            SqlConnection conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into Categories
                                Values (@Name)";
            cmd.Parameters.AddWithValue("@Name", name);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

        public void AddToDoItem(ToDoItem T)
        {
            SqlConnection conn = new SqlConnection(_connectionstring);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into ToDoItem(CategoryId,Title,DueDate)
                                Values (@CategoryId,@Title,@DueDate)";
            cmd.Parameters.AddWithValue("@CategoryId", T.CategoryId);
            cmd.Parameters.AddWithValue("@Title", T.Title);
            cmd.Parameters.AddWithValue("@DueDate", T.DueDate);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

        public IEnumerable<Categories> GetAllComplete()
        {
            SqlConnection connection = new SqlConnection(_connectionstring);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"select c.Name, T.Title, T.DueDate, T.CompletedDate,T.CategoryId, T.Id
                                From Categories c
                                join ToDoItem T
                                on c.Id = t.CategoryId
                                where t.CompletedDate is not null";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Categories> categories = new List<Categories>();
            while (reader.Read())
            {
                int id = (int)reader["CategoryId"];
                Categories c = categories.FirstOrDefault(C => id == C.Id);
                if (c == null)
                {
                    c = new Categories
                    {
                        Id = id,
                        Name = (string)reader["Name"]
                    };
                    categories.Add(c);
                }
                object M = reader["CompletedDate"];
                DateTime? completed = null;
                if (M != DBNull.Value)
                {
                    completed = (DateTime?)M;
                }
                c.ToDoItems.Add(new ToDoItem
                {
                    Id = (int?)reader["Id"],
                    CategoryId = id,
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    CompletedDate = completed
                });
            }
            connection.Close();
            connection.Dispose();

            return categories;
        }
    }
}