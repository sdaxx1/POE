using POE_ST10152183.Models;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace POE_ST10152183.data
{
    public class DbLayer
    {
        private IConfiguration _config;
        private string con;

        public DbLayer(IConfiguration configuration)
        {
            _config = configuration;
            con = _config.GetConnectionString("dbConnect");
        }
        public List<Employees> AllEmployees()
        {
            int Eid;
            string EUserName = "", EPassword = "", EFirstName = "",ELastName="",EPhoneNumber="" ,EEmailAddress="";
           
            List<Employees> emList = new List<Employees>();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlDataAdapter cmdSelect = new SqlDataAdapter($"SELECT * FROM Employees", Con);
                DataTable dt = new DataTable();
                DataRow dr;

                Con.Open();
                cmdSelect.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        Eid = (int)dr[0];
                        EUserName = (string)dr[1];
                        EPassword = (string)dr[2];
                        EFirstName = (string)dr[3];
                        ELastName = (string)dr[4];
                        EPhoneNumber = (string)dr[5];
                        EEmailAddress = (string)dr[6];
                        Employees em = new Employees(Eid, EUserName, EPassword, EFirstName, ELastName, EPhoneNumber, EEmailAddress);
 emList.Add(em);
                    }
                }
            }
            return emList;
        }
        public int Addemployee(Employees employee)
        {
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdInsert = new SqlCommand($"insert into Employees( EUserName, EPassword, EFirstName, ELastName, EPhoneNumber, EEmailAddress)values" +
                    $"(@EU,@EP,@EF,@EL,@EPN,@EE)", Con);
                cmdInsert.Parameters.AddWithValue("@EU",employee.EUserName);
                cmdInsert.Parameters.AddWithValue("@EP",employee.EPassword);
                cmdInsert.Parameters.AddWithValue("@EF",employee.EFirstName);
                cmdInsert.Parameters.AddWithValue("@EL",employee.ELastName);
                cmdInsert.Parameters.AddWithValue("@EPN",employee.EPhoneNumber);
                cmdInsert.Parameters.AddWithValue("@EE",employee.EEmailAddress);
                SqlTransaction transaction;

                Con.Open();
                transaction = Con.BeginTransaction();
                cmdInsert.Transaction = transaction;
                int x = 0;
                try
                {
                    x = cmdInsert.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
                finally
                {
                    Con.Close();
                }

                return x;
            }
        }
        

        public Employees GetEmployee(int id)
        {

            Employees em = new Employees();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM Employees WHERE EmployeeId = {id}", Con);
               
                Con.Open();

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        em = new Employees((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5], (string)reader[6]);
                        
                    }
                }
            }
            return em;
        }



        public int DeleteEmployee(int id)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmdDelete = new SqlCommand($"DELETE FROM Employees WHERE EmployeeId ='{id}'", Con);
            SqlTransaction transaction;

            Con.Open();
            transaction = Con.BeginTransaction();
            cmdDelete.Transaction = transaction;
            int x = 0;
            try
            {
                x = cmdDelete.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
            }
            finally
            {
                Con.Close();
            }

            return x;
        }
        public int UpdateEmployee(int id, Employees employee)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmdUpdate = new SqlCommand($"Update Employees SET EUserName=@EU,EPassword=@EP,EFirstName=@EF,ELastName=@EL,EPhoneNumber=@EPN,EEmailAddress=@EE Where EmployeeId={id} ", Con);
         
            cmdUpdate.Parameters.AddWithValue("@EU", employee.EUserName);
            cmdUpdate.Parameters.AddWithValue("@EP", employee.EPassword);
            cmdUpdate.Parameters.AddWithValue("@EF", employee.EFirstName);
            cmdUpdate.Parameters.AddWithValue("@EL", employee.ELastName);
            cmdUpdate.Parameters.AddWithValue("@EPN", employee.EPhoneNumber);
            cmdUpdate.Parameters.AddWithValue("@EE", employee.EEmailAddress);
            SqlTransaction transaction;

            Con.Open();
            transaction = Con.BeginTransaction();
            cmdUpdate.Transaction = transaction;
            int x=0;
            try
            {
                x = cmdUpdate.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
            }
            finally
            {
                Con.Close();
            }

            return x;

        }
        public List<Farmers> AllFarmers()
        {
            int Fid;
            string FUserName = "", FPassword = "", FFirstName = "", FLastName = "", FPhoneNumber = "", FEmailAddress = "";

            List<Farmers> faList = new List<Farmers>();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlDataAdapter cmdSelect = new SqlDataAdapter($"SELECT * FROM Farmers", Con);
                DataTable dt = new DataTable();
                DataRow dr;

                Con.Open();
                cmdSelect.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        Fid = (int)dr[0];
                        FUserName = (string)dr[1];
                        FPassword = (string)dr[2];
                        FFirstName = (string)dr[3];
                        FLastName = (string)dr[4];
                        FPhoneNumber = (string)dr[5];
                        FEmailAddress = (string)dr[6];
                        Farmers fa = new Farmers(Fid, FUserName, FPassword, FFirstName, FLastName, FPhoneNumber, FEmailAddress);
                        faList.Add(fa);
                    }
                }
            }
            return faList;
        }
        public int AddFarmers(Farmers farmer)
        {
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdInsert = new SqlCommand($"insert into Farmers(FUserName, FPassword, FFirstName, FLastName, FPhoneNumber, FEmailAddress)values" +
                    $"(@FU,@FP,@FF,@FL,@FPN,@FE)", Con);
                cmdInsert.Parameters.AddWithValue("@FU", farmer.FUserName);
                cmdInsert.Parameters.AddWithValue("@FP", farmer.FPassword);
                cmdInsert.Parameters.AddWithValue("@FF", farmer.FFirstName);
                cmdInsert.Parameters.AddWithValue("@FL", farmer.FLastName);
                cmdInsert.Parameters.AddWithValue("@FPN", farmer.FPhoneNumber);
                cmdInsert.Parameters.AddWithValue("@FE", farmer.FEmailAddress);
                SqlTransaction transaction;

                Con.Open();
                transaction = Con.BeginTransaction();
                cmdInsert.Transaction = transaction;
                int x = 0;
                try
                {
                    x = cmdInsert.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
                finally
                {
                    Con.Close();
                }

                return x;
            }
        }


        public Farmers GetFarmer(int id)
        {

            Farmers fa = new Farmers();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM Farmers WHERE FarmerId = {id}", Con);

                Con.Open();

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        fa = new Farmers((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5], (string)reader[6]);

                    }
                }
            }
            return fa;
        }
        public int DeleteFarmer(int id)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmdDelete = new SqlCommand($"DELETE FROM Farmers WHERE FarmerId ='{id}'", Con);
            SqlTransaction transaction;

            Con.Open();
            transaction = Con.BeginTransaction();
            cmdDelete.Transaction = transaction;
            int x = 0;
            try
            {
                x = cmdDelete.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
            }
            finally
            {
                Con.Close();
            }

            return x;
        }
        public int UpdateFarmer(int id, Farmers farmer)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmdUpdate = new SqlCommand($"Update Farmers SET FUserName=@FU,FPassword=@FP,FFirstName=@FF,FLastName=@FL,FPhoneNumber=@FPN,FEmailAddress=@FE Where FarmerId={id} ", Con);
            cmdUpdate.Parameters.AddWithValue("@FU", farmer.FUserName);
            cmdUpdate.Parameters.AddWithValue("@FP", farmer.FPassword);
            cmdUpdate.Parameters.AddWithValue("@FF", farmer.FFirstName);
            cmdUpdate.Parameters.AddWithValue("@FL", farmer.FLastName);
            cmdUpdate.Parameters.AddWithValue("@FPN", farmer.FPhoneNumber);
            cmdUpdate.Parameters.AddWithValue("@FE", farmer.FEmailAddress);
            SqlTransaction transaction;

            Con.Open();
            transaction = Con.BeginTransaction();
            cmdUpdate.Transaction = transaction;
            int x = 0;
            try
            {
                x = cmdUpdate.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
            }
            finally
            {
                Con.Close();
            }

            return x;

        }
        public List<Products> FProducts (int FarmId)
        {
            List<Products> prList = new List<Products>();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlDataAdapter cmdSelect = new SqlDataAdapter($"SELECT P.ProductId, P.ProductName,P.ProductType,P.Price,P.UploadDate " +
                    $"FROM Products P INNER JOIN Farmers F ON F.FarmerId = P.FarmerId " +
                    $"WHERE F.FarmerId = '{FarmId}'", Con);
                DataTable dt = new DataTable();
                DataRow dr;

                Con.Open();
                cmdSelect.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];

                        Products pt = new Products((int)dr[0], (string)dr[1], (string)dr[2], (int)dr[3], FarmId, (DateTime)dr[4]);
                        prList.Add(pt);
                    }
                }
            }
            return prList;
        }
        public List<Products> AllProducts()
        {
            List<Products> prList = new List<Products>();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlDataAdapter cmdSelect = new SqlDataAdapter($"SELECT * From Products ", Con);
                DataTable dt = new DataTable();
                DataRow dr;

                Con.Open();
                cmdSelect.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];

                        Products pt = new Products((int)dr[0], (string)dr[1], (string)dr[2], (int)dr[3], (int)dr[4],(DateTime)dr[5]);
                        prList.Add(pt);
                    }
                }
            }

            return prList;
        }
        public int AddProduct(Products product,int farmerId)
        {
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO Products(ProductName,ProductType,Price,FarmerId,UploadDate)values" +
                    $"(@PN,@PT,@PR,{farmerId},@UD)", Con);
                cmdInsert.Parameters.AddWithValue("@PN", product.ProductName);
                cmdInsert.Parameters.AddWithValue("@PT", product.ProductType);
                cmdInsert.Parameters.AddWithValue("@PR", product.Price);
                cmdInsert.Parameters.AddWithValue("@UD", DateTime.Now);

                SqlTransaction transaction;

                Con.Open();
                transaction = Con.BeginTransaction();
                cmdInsert.Transaction = transaction;
                int x = 0;
                try
                {
                    x = cmdInsert.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
                finally
                {
                    Con.Close();
                }

                return x;
            }
        }


        public Products GetProduct(int id)
        {

            Products pr = new Products();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM Products WHERE ProductId = {id}", Con);

                Con.Open();

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        pr = new Products((int)reader[0], (string)reader[1], (string)reader[2], (int)reader[3], (int)reader[4], (DateTime)reader[5]);

                    }
                }
            }
            return pr;
        }

        public int DeleteProduct(int id)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmdDelete = new SqlCommand($"DELETE FROM Products WHERE ProductId ='{id}'", Con);
            SqlTransaction transaction;

            Con.Open();
            transaction = Con.BeginTransaction();
            cmdDelete.Transaction = transaction;
            int x = 0;
            try
            {
                x = cmdDelete.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
            }
            finally
            {
                Con.Close();
            }

            return x;
        }
        public int UpdateProduct (int id, Products product,int farmerId)
        {
            SqlConnection Con = new SqlConnection(con);
            SqlCommand cmdUpdate = new SqlCommand($"Update Products SET ProductName=@PN,ProductType=@PT,Price=@Pr,FarmerId={farmerId},UploadDate='{DateTime.Now}' Where ProductId={id} ", Con);
           
            cmdUpdate.Parameters.AddWithValue("@PN", product.ProductName);
            cmdUpdate.Parameters.AddWithValue("@PT", product.ProductType);
            cmdUpdate.Parameters.AddWithValue("@PR", product.Price);
            
            SqlTransaction transaction;

            Con.Open();
            transaction = Con.BeginTransaction();
            cmdUpdate.Transaction = transaction;
            int x = 0;
            try
            {
                x = cmdUpdate.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {

                transaction.Rollback();
            }
            finally
            {
                Con.Close();
            }

            return x;

        }
        public List<int> GetEmployeeId()
        {
            List<int> id = new List<int>();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdGet = new SqlCommand($"Select EmployeeId from Employees", Con);
                Con.Open();
                using (SqlDataReader reader = cmdGet.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id.Add(Convert.ToInt32(reader[0]));
                    }
                }
            }
            return id;
        }
        public List<int> GetAdminId()
        {
            List<int> id = new List<int>();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdGet = new SqlCommand($"Select Id from admin", Con);
                Con.Open();
                using (SqlDataReader reader = cmdGet.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id.Add(Convert.ToInt32(reader[0]));
                    }
                }
            }
            return id;
        }
        public List<int> GetFarmerId()
        {
            List<int> id = new List<int>();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdGet = new SqlCommand($"Select FarmerId from Farmers", Con);
                Con.Open();
                using (SqlDataReader reader = cmdGet.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id.Add(Convert.ToInt32(reader[0]));
                    }
                }
            }
            return id;
        }
        public AdminAccounts GetAdmin(int id)
        {

            AdminAccounts aa = new AdminAccounts();
            using (SqlConnection Con = new SqlConnection(con))
            {
                SqlCommand cmdSelect = new SqlCommand($"SELECT * FROM admin WHERE Id = {id}", Con);

                Con.Open();

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        aa= new AdminAccounts((int)reader[0], (string)reader[1], (string)reader[2]);

                    }
                }
            }
            return aa;
        }
    }
}

