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
        public int ID;
        //rights into bite system.
        int Rights;
        public string Name;
        public string Phone;
        byte[] PasswordHash;

        /// <summary>
        /// Set a new password to customer
        /// </summary>
        /// <param name="Pass">new password</param>
        public void SetPassword(string Pass)
        {
            PasswordHash = MainForm.ComputeHash(Pass);
        }

        /// <summary>
        /// Set parametres to sql command
        /// </summary>
        /// <param name="ret">Sql command before executing</param>
        /// <returns>sql command with parametres</returns>
        SqlCommand SetParams(SqlCommand ret)
        {
            ret.Parameters.Add(new SqlParameter("Id", ID));
            ret.Parameters.Add(new SqlParameter("Name", Name));
            ret.Parameters.Add(new SqlParameter("Phone", Phone));
            ret.Parameters.Add(new SqlParameter("Rights", Rights));
            ret.Parameters.Add(new SqlParameter("PassHash", PasswordHash));

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
            ID = Convert.ToInt32(result[0]);
            Name = Convert.ToString(result[1]);
            Phone = Convert.ToString(result[2]);
            Rights = Convert.ToInt32(result[3]);
            PasswordHash = result[4] as byte[];
            Name = SQLWorker.FixString(Name);
            Phone = SQLWorker.FixString(Phone);
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

            if (PasswordHash == null || passHash.Length != PasswordHash.Length)
                return false;

            for (int i = 0; i < passHash.Length; i++)
                answer &= (passHash[i] == PasswordHash[i]);

            return answer;
        }

        /// <summary>
        /// Convert right from bite system to string
        /// </summary>
        public string GetStringRights()
        {
            string result = "";
            result += (Rights & 1) != 0 ? "I" : "";
            result += (Rights & 2) != 0 ? "D" : "";
            result += (Rights & 4) != 0 ? "P" : "";
            result += (Rights & 8) != 0 ? "C" : "";
            result += (Rights & 16) != 0 ? "L" : "";
            return result;
        }

        /// <summary>
        /// Convert right from string to byte system
        /// </summary>
        public void SetStringRights(string rights)
        {
            Rights = 0;
            foreach (char c in rights)
            {
                switch (c)
                {
                    case 'I':
                        Rights += 1;
                        break;
                    case 'D':
                        Rights += 2;
                        break;
                    case 'P':
                        Rights += 4;
                        break;
                    case 'C':
                        Rights += 8;
                        break;
                    case 'L':
                        Rights += 16;
                        break;
                    case ' ': break;
                    default:
                        throw new Exception("unexpected char " + c + " founded");
                }
            }
        }
    }
}
