﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using ShoeShopManagement.Models;

namespace ShoeShopManagement.DAL
{
    class AccountDAL : DataProvider
    {
        private static AccountDAL instance;

        public static AccountDAL Instance
        {
            get { if (instance == null) instance = new AccountDAL(); return AccountDAL.instance; }
            private set { AccountDAL.instance = value; }
        }
        private AccountDAL()
        {

        }
        public List<Account> ConvertDBToList()
        {
            DataTable dt;
            List<Account> accounts = new List<Account>();
            try
            {
                dt = LoadData("Account");
            }
            catch
            {
                CloseConnection();
                dt = LoadData("Account");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Account acc = new Account(int.Parse(dt.Rows[i].ItemArray[0].ToString()), int.Parse(dt.Rows[i].ItemArray[1].ToString()), dt.Rows[i].ItemArray[2].ToString(), dt.Rows[i].ItemArray[3].ToString(), int.Parse(dt.Rows[i].ItemArray[4].ToString()));
                accounts.Add(acc);
            }
            return accounts;
        }
        public void AddIntoDB(Account account)
        {
            OpenConnection();
            string query = "INSERT INTO Account(idAccount, idEmployee, userName, passWord, typeAccount) VALUES(@idAccount, @idEmployee, @username, @password, @type)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idAccount", account.IdAccount.ToString());
            cmd.Parameters.AddWithValue("@idEmployee", account.IdEmployee.ToString());
            cmd.Parameters.AddWithValue("@username", account.UserName);
            cmd.Parameters.AddWithValue("@password", account.PassWord);
            cmd.Parameters.AddWithValue("@type", account.TypeAcount.ToString());
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public bool DeleteAccount(string idAccount)
        {
            try
            {
                OpenConnection();
                string query = "delete from Account where IdAccount = " + idAccount;
                SqlCommand command = new SqlCommand(query, conn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool UpdatePassword(string idAccount, string passWord)
        {
            try
            {
                OpenConnection();
                string query = "update Account set password=@password where idAccount = @idAccount";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idAccount", idAccount);
                command.Parameters.AddWithValue("@password", passWord);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool UpdatePasswordByUsername(string userName, string passWord)
        {
            try
            {
                OpenConnection();
                string query = "update Account set password=@password where username = @username";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", userName);
                command.Parameters.AddWithValue("@password", passWord);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool UpdateType(Account account)
        {
            try
            {
                OpenConnection();
                string query = "update Account set type=@type where IdAccount = " + account.IdAccount;
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@type  ", account.TypeAcount);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool IsExistUserName(string username)
        {
            try
            {
                OpenConnection();
                string queryString = "select * from Account where username = '" + username + "'";
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int SetNewID()
        {
            try
            {
                OpenConnection();
                string queryString = "select max(idAccount) from Account";
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return int.Parse(dataTable.Rows[0].ItemArray[0].ToString()) + 1;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool CheckLogin(string username, string password)
        {
            try
            {
                OpenConnection();
                string queryString = string.Format("select * from TAIKHOAN where TenTK = '{0}' And MK = '{1}'",username,password);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
