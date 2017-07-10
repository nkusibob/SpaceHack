using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace app_HackSpace.DB
{
    public class DbAdo
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"Data Source=LAPTOP-MSI\SQLEXPRESS;Initial Catalog=db_HackSpace;Integrated Security=True";

            try
            {
                SqlConnection db = new SqlConnection();
                db.ConnectionString = ConnectionString;

                Console.WriteLine(string.Format("Etat : {0}", db.State.ToString()));
                db.Open();
                Console.WriteLine(string.Format("Etat : {0}", db.State.ToString()));
                db.Close();
                Console.WriteLine(string.Format("Etat : {0}", db.State.ToString()));
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}