using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication
{
    public class User
    {
        public int id;
        //rights into bite system.
        int rights;
        public string name;
        public string phone;
        byte[] passwordHash;

        /// <summary>
        /// Set a new password to customer
        /// </summary>
        /// <param name="Pass">new password</param>
        public void SetPassword(string Pass)
        {
            passwordHash = MainForm.ComputeHash(Pass);
        }

        /// <summary>
        /// Set parametres to sql command
        /// </summary>
        /// <param name="ret">Sql command before executing</param>
        /// <returns>sql command with parametres</returns>
        SqlCommand SetParams(SqlCommand ret)
        {
            ret.Parameters.Add(new SqlParameter("Id", id));
            ret.Parameters.Add(new SqlParameter("Name", name));
            ret.Parameters.Add(new SqlParameter("Phone", phone));
            ret.Parameters.Add(new SqlParameter("Rights", rights));
            ret.Parameters.Add(new SqlParameter("PassHash", passwordHash));

            return ret;
        }

        /// <summary>
        /// for making a sql command for inserting a new user
        /// </summary>
        /// <returns>new sql command</returns>
        public SqlCommand InsertNewUser()
        {
            SqlCommand ret = new SqlCommand("insert into tblUsers values(@Id, @Name, @Phone, @Rights, @PassHash)");
            ret = SetParams(ret);

            return ret;
        }

        /// <summary>
        /// for making a sql command for updating details of a user
        /// </summary>
        /// <returns>new sql command</returns>
        public SqlCommand UpdateSqlCommand()
        {
            SqlCommand ret = new SqlCommand("update tblUsers set Name=@Name, Phone=@Phone, Rights=@Rights, PassHash=@PassHash where(Id=@Id)");
            ret = SetParams(ret);

            return ret;
        }

        /// <summary>
        /// Setting parametres of a current users from dataset.
        /// Command should be executed before
        /// </summary>
        /// <param name="result">Result of executing sql command</param>
        public void ReadSqlResult(SqlDataReader result)
        {
            id = Convert.ToInt32(result[0]);
            name = Convert.ToString(result[1]);
            phone = Convert.ToString(result[2]);
            rights = Convert.ToInt32(result[3]);
            passwordHash = result[4] as byte[];
        }

        /// <summary>
        /// compare customer password with another one
        /// </summary>
        /// <param name="Pass">another password</param>
        /// <returns>comparison of passwords</returns>
        public bool ComparePasswords(string Pass)
        {
            byte[] passHash = MainForm.ComputeHash(Pass);
            bool answer = true;

            if (passwordHash == null || passHash.Length != passwordHash.Length)
                return false;

            for (int i = 0; i < passHash.Length; i++)
                answer &= (passHash[i] == passwordHash[i]);

            return answer;
        }

        /// <summary>
        /// Convert right from bite system to string
        /// </summary>
        public string GetStringRights()
        {
            string result = "";
            result += (rights & 1) != 0 ? "I" : "";
            result += (rights & 2) != 0 ? "D" : "";
            result += (rights & 4) != 0 ? "P" : "";
            result += (rights & 8) != 0 ? "U" : "";
            result += (rights & 16) != 0 ? "L" : "";
            return result;
        }

        /// <summary>
        /// Convert right from string to byte system
        /// </summary>
        public void SetStringRights(string rght)
        {
            rights = 0;
            foreach (char c in rght)
            {
                switch (c)
                {
                    case 'I':
                        rights += 1;
                        break;
                    case 'D':
                        rights += 2;
                        break;
                    case 'P':
                        rights += 4;
                        break;
                    case 'U':
                        rights += 8;
                        break;
                    case 'L':
                        rights += 16;
                        break;
                    case ' ': break;
                    default:
                        throw new Exception("unexpected char " + c + " founded");
                }
            }
        }
    }
}
