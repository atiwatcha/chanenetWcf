using chananet.DataContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chananet.DataAccess
{
    public class AccessSqlServer
    {
        public SqlConnection connetion()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection("workstation id=xxxx;packet size=4096;user id=xxx;pwd=xxxx;data source=DBMSSql.mssql.somee.com;persist security info=False;initial catalog=DBMSSql");
                return sqlConnection;
            }
            catch (Exception ex)
            {
                return (SqlConnection)null;
            }
        }

        public void getlineProgram(out LineProgram p, int? id)
        {
            p = (LineProgram)null;
            try
            {
                p = new LineProgram();
                SqlConnection sqlConnection = new SqlConnection();
                SqlConnection connection = this.connetion();
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("select id,AccessToken,ClientSecret,redirect_uri from dbo.LineProgram", connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    p.id = (int)sqlDataReader.GetValue(0);
                    p.AccessToken = sqlDataReader.GetValue(1).ToString();
                    p.ClientSecret = sqlDataReader.GetValue(2).ToString();
                    p.redirect_uri = sqlDataReader.GetValue(3).ToString();
                }
                sqlDataReader.Close();
                sqlCommand.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void getlineUser(out List<LineUser> LineUsers, int? id, string Username, string Password)
        {
            LineUsers = (List<LineUser>)null;
            try
            {
                LineUsers = new List<LineUser>();
                SqlConnection sqlConnection = new SqlConnection();
                SqlConnection connection = this.connetion();
                connection.Open();
                string cmdText = "select id,LineProgram_id,AccessToken,Username,null as Password from chananet.lineUser ";
                if (Username != null && Password != null)
                    cmdText = cmdText + "Where  Username='" + Username + "' and Password = '" + Password + "'";
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    LineUsers.Add(new LineUser()
                    {
                        id = (int)sqlDataReader.GetValue(0),
                        LineProgram_id = (int)sqlDataReader.GetValue(1),
                        AccessToken = sqlDataReader.GetValue(2).ToString(),
                        Username = sqlDataReader.GetValue(3).ToString(),
                        Password = sqlDataReader.GetValue(4).ToString()
                    });
                sqlDataReader.Close();
                sqlCommand.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void UpdateLineUser(LineUser LineUser)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection();
                SqlConnection connection = this.connetion();
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("UPDATE chananet.LineUser SET AccessToken=@AccessToken WHERE id=@id", connection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", (object)LineUser.id);
                    sqlCommand.Parameters.AddWithValue("@AccessToken", (object)LineUser.AccessToken);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
            }
        }
    }
}
