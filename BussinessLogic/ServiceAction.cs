using chananet.DataAccess;
using chananet.DataContract;
using System;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;

namespace chananet.BussinessLogic
{
    public class ServiceAction
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select<string, char>((Func<string, char>)(s => s[ServiceAction.random.Next(s.Length)])).ToArray<char>());
        }

        public void getlineProgram(out LineProgram lineProgram)
        {
            AccessSqlServer accessSqlServer = new AccessSqlServer();
            lineProgram = (LineProgram)null;
            accessSqlServer.getlineProgram(out lineProgram, new int?());
        }

        public void LineRegisterUser(int User_id)
        {
            try
            {
                AccessSqlServer accessSqlServer = new AccessSqlServer();
                LineProgram p = new LineProgram();
                accessSqlServer.getlineProgram(out p, new int?());
                if (p == null)
                    return;
                HttpResponseMessage result = new HttpClient().GetAsync("https://notify-bot.line.me/oauth/authorize?response_type=code&client_id=" + p.AccessToken + "&redirect_uri=" + p.redirect_uri + "&scope=notify&state=" + ServiceAction.RandomString(10)).Result;
                if (!result.IsSuccessStatusCode)
                    throw new Exception(result.IsSuccessStatusCode.ToString() + " ไม่สามารถส่งข้อความได้");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void sentNotify(string Message)
        {
            try
            {
                AccessSqlServer accessSqlServer = new AccessSqlServer();
                List<LineUser> LineUsers = new List<LineUser>();
                accessSqlServer.getlineUser(out LineUsers, new int?(), (string)null, (string)null);
                if (LineUsers == null)
                    return;
                string requestUri = "https://notify-api.line.me/api/notify?message=" + Message;
                StringContent stringContent = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpClient httpClient = new HttpClient();
                foreach (LineUser lineUser in LineUsers)
                {
                    HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "?message=" + Message);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Authorization", lineUser.AccessToken);
                    HttpResponseMessage result = httpClient.PostAsync(requestUri, (HttpContent)stringContent).Result;
                    if (!result.IsSuccessStatusCode)
                        throw new Exception(result.IsSuccessStatusCode.ToString() + " ไม่สามารถส่งข้อความได้");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LineUser> getlineUser(int? id, string UserName, string Password)
        {
            try
            {
                AccessSqlServer accessSqlServer = new AccessSqlServer();
                List<LineUser> LineUsers = new List<LineUser>();
                accessSqlServer.getlineUser(out LineUsers, new int?(), (string)null, (string)null);
                return LineUsers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void getAccesTokenUser(string code, int? id)
        {
            AccessSqlServer accessSqlServer = new AccessSqlServer();
            List<LineUser> LineUsers = new List<LineUser>();
            accessSqlServer.getlineUser(out LineUsers, id, (string)null, (string)null);
            LineProgram p = new LineProgram();
            accessSqlServer.getlineProgram(out p, new int?());
            if (LineUsers == null || !LineUsers.Any<LineUser>() || p == null)
                return;
            HttpResponseMessage result1 = new HttpClient().PostAsync("https://notify-bot.line.me/oauth/token?grant_type=authorization_code&code=" + code + "&redirect_uri=" + p.redirect_uri + "&client_id=" + p.AccessToken + "&client_secret=" + p.ClientSecret, (HttpContent)new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
            if (!result1.IsSuccessStatusCode)
                throw new Exception(result1.IsSuccessStatusCode.ToString() + " ไม่สามารถส่งข้อความได้");
            string result2 = result1.Content.ReadAsStringAsync().Result;
            using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(result2)))
            {
                oauth oauth = (oauth)new DataContractJsonSerializer(typeof(oauth)).ReadObject((Stream)memoryStream);
                if (!(oauth.status == "200"))
                    throw new Exception(oauth.status + ":" + oauth.message);
                LineUser lineUser = new LineUser();
                LineUser LineUser = LineUsers.FirstOrDefault<LineUser>();
                LineUser.AccessToken = "Bearer " + oauth.access_token;
                accessSqlServer.UpdateLineUser(LineUser);
            }
        }
    }
}
