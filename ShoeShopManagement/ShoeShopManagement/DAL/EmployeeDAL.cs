﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using FootballFieldManagement.DAL;
using System.IO;
using ShoeShopManagement.Models;

namespace FootballFieldManagement.DAL
{
    class EmployeeDAL : DataProvider
    {

        private static EmployeeDAL instance;

        public static EmployeeDAL Instance
        {
            get { if (instance == null) instance = new EmployeeDAL(); return EmployeeDAL.instance; }
            private set { EmployeeDAL.instance = value; }
        }

        private EmployeeDAL()
        {

        }
        //public int GetMaxIdEmployee()
        //{
        //    int res = 0;
        //    try
        //    {
        //        OpenConnection();
        //        string queryString = "select max(idEmployee) from Employee ";

        //        SqlCommand command = new SqlCommand(queryString, conn);
        //        command.ExecuteNonQuery();
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);

        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);
        //        res = int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //    return res;
        //}
        public List<Employee> ConvertDBToList()
        {
            DataTable dt = new DataTable();
            List<Employee> employees = new List<Employee>();
            try
            {
                OpenConnection();
                string queryString = @"Select * from Employee";

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                CloseConnection();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Employee employee = new Employee(int.Parse(dt.Rows[i].ItemArray[0].ToString()),
                    dt.Rows[i].ItemArray[1].ToString(),
                    DateTime.Parse(dt.Rows[i].ItemArray[2].ToString()),
                    dt.Rows[i].ItemArray[3].ToString(),
                    DateTime.Parse(dt.Rows[i].ItemArray[4].ToString()),
                    dt.Rows[i].ItemArray[5].ToString(),
                    dt.Rows[i].ItemArray[6].ToString(),
                    dt.Rows[i].ItemArray[7].ToString(),
                    dt.Rows[i].ItemArray[8].ToString());
                employees.Add(employee);
            }
            return employees;
        }
        //public bool UpdateIdAccount(Employee employee)
        //{
        //    try
        //    {
        //        OpenConnection();
        //        string query;
        //        if (employee.IdAccount != -1)
        //            query = "update Employee set idAccount = " + employee.IdAccount + " where idEmployee = @idEmployee";
        //        else
        //            query = "update Employee set idAccount = NULL where idEmployee = @idEmployee";

        //        SqlCommand command = new SqlCommand(query, conn);
        //        command.Parameters.AddWithValue("@idEmployee", employee.IdEmployee);

        //        int rs = command.ExecuteNonQuery();
        //        if (rs != 1)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}
        //public bool AddIntoDB(Employee employee)
        //{
        //    try
        //    {
        //        OpenConnection();
        //        string query = "insert into Employee( idEmployee,name,gender,phonenumber,address,dateofBirth,position,startingdate,imageFile,isDeleted) values(@idEmployee,@name,@gender,@phonenumber,@address,@dateofBirth,@position,@startingdate,@imageFile,@isDeleted)";
        //        SqlCommand command = new SqlCommand(query, conn);
        //        command.Parameters.AddWithValue("@idEmployee", employee.IdEmployee);
        //        command.Parameters.AddWithValue("@name", employee.Name);
        //        command.Parameters.AddWithValue("@gender", employee.Gender);
        //        command.Parameters.AddWithValue("@phonenumber", employee.Phonenumber);
        //        command.Parameters.AddWithValue("@address", employee.Address);
        //        command.Parameters.AddWithValue("@dateofBirth", employee.DateOfBirth);
        //        command.Parameters.AddWithValue("@position", employee.Position);
        //        command.Parameters.AddWithValue("@startingdate", employee.Startingdate);
        //        command.Parameters.AddWithValue("@imageFile", Convert.ToBase64String(employee.ImageFile));
        //        command.Parameters.AddWithValue("@isDeleted", employee.IsDeleted);
        //        int rs = command.ExecuteNonQuery();
        //        if (rs != 1)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}
        //public bool UpdateOnDB(Employee employee)
        //{
        //    try
        //    {
        //        OpenConnection();
        //        string query = "update Employee  set name=@name,gender=@gender,phonenumber=@phonenumber,address=@address,dateofBirth=@dateofBirth,position=@position,startingdate=@startingdate,imageFile=@imageFile,isDeleted=@isDeleted where idEmployee=" + employee.IdEmployee;
        //        SqlCommand command = new SqlCommand(query, conn);
        //        command.Parameters.AddWithValue("@name", employee.Name);
        //        command.Parameters.AddWithValue("@gender", employee.Gender);
        //        command.Parameters.AddWithValue("@phonenumber", employee.Phonenumber);
        //        command.Parameters.AddWithValue("@address", employee.Address);
        //        command.Parameters.AddWithValue("@dateofBirth", employee.DateOfBirth);
        //        command.Parameters.AddWithValue("@position", employee.Position);
        //        command.Parameters.AddWithValue("@startingdate", employee.Startingdate);
        //        command.Parameters.AddWithValue("@imageFile", Convert.ToBase64String(employee.ImageFile));
        //        command.Parameters.AddWithValue("@isDeleted", employee.IsDeleted);
        //        int rs = command.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}
        //public bool DeleteEmployee(Employee employee)
        //{
        //    try
        //    {
        //        OpenConnection();
        //        string query = @"Update Employee" +
        //                        "Set isDeleted=1" +
        //                        " where idEmployee = " + employee.IdEmployee.ToString();
        //        SqlCommand command = new SqlCommand(query, conn);
        //        if (command.ExecuteNonQuery() > 0)
        //            return true;
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        CustomMessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}
        //public void AddEmployee(Employee employee)
        //{
        //    if (ConvertDBToList().Count == 0 || employee.IdEmployee > GetMaxIdEmployee())
        //    {
        //        if (AddIntoDB(employee))
        //            CustomMessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        //        else
        //            CustomMessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    else
        //    {
        //        if (UpdateOnDB(employee))
        //            CustomMessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        //        else
        //            CustomMessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        //public Employee GetEmployee(string idEmployee) // Bao gồm cả nhân viên đã xóa hoặc chưa xóa
        //{
        //    Employee res = new Employee();
        //    try
        //    {
        //        OpenConnection();
        //        string queryString = "select * from Employee where idEmployee = " + idEmployee;

        //        SqlCommand command = new SqlCommand(queryString, conn);
        //        command.ExecuteNonQuery();
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);

        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);
        //        int idAccount = -1;
        //        if (dataTable.Rows[0].ItemArray[8].ToString() != "")
        //        {
        //            idAccount = int.Parse(dataTable.Rows[0].ItemArray[8].ToString());
        //        }
        //        res = new Employee(int.Parse(dataTable.Rows[0].ItemArray[0].ToString()),
        //             dataTable.Rows[0].ItemArray[1].ToString(), dataTable.Rows[0].ItemArray[2].ToString(),
        //             dataTable.Rows[0].ItemArray[3].ToString(), dataTable.Rows[0].ItemArray[4].ToString(),
        //             DateTime.Parse(dataTable.Rows[0].ItemArray[5].ToString()),
        //             dataTable.Rows[0].ItemArray[6].ToString(), DateTime.Parse(dataTable.Rows[0].ItemArray[7].ToString()),
        //             idAccount, Convert.FromBase64String(dataTable.Rows[0].ItemArray[9].ToString()), int.Parse(dataTable.Rows[0].ItemArray[10].ToString()));
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //    return res;
        //}
        //public Employee GetEmployeeByIdEmployee(string idEmployee) // Lấy thông tin khi biết id nhân viên - Không lấy nhân viên đã xóa
        //{
        //    Employee res = new Employee();
        //    try
        //    {
        //        OpenConnection();
        //        string queryString = "select * from Employee where isDeleted=0 and idEmployee = " + idEmployee;

        //        SqlCommand command = new SqlCommand(queryString, conn);
        //        command.ExecuteNonQuery();
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);

        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);
        //        int idAccount = -1;
        //        if (dataTable.Rows[0].ItemArray[8].ToString() != "")
        //        {
        //            idAccount = int.Parse(dataTable.Rows[0].ItemArray[8].ToString());
        //        }
        //        res = new Employee(int.Parse(dataTable.Rows[0].ItemArray[0].ToString()),
        //             dataTable.Rows[0].ItemArray[1].ToString(), dataTable.Rows[0].ItemArray[2].ToString(),
        //             dataTable.Rows[0].ItemArray[3].ToString(), dataTable.Rows[0].ItemArray[4].ToString(),
        //             DateTime.Parse(dataTable.Rows[0].ItemArray[5].ToString()),
        //             dataTable.Rows[0].ItemArray[6].ToString(), DateTime.Parse(dataTable.Rows[0].ItemArray[7].ToString()),
        //             idAccount, Convert.FromBase64String(dataTable.Rows[0].ItemArray[9].ToString()), int.Parse(dataTable.Rows[0].ItemArray[10].ToString()));
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //    return res;
        //}
        //public Employee GetEmployeeByIdAccount(string idAccount)
        //{
        //    Employee res = new Employee();
        //    try
        //    {
        //        OpenConnection();
        //        string queryString = "select * from Employee where idAccount = " + idAccount;

        //        SqlCommand command = new SqlCommand(queryString, conn);
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);

        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);

        //        res = new Employee(int.Parse(dataTable.Rows[0].ItemArray[0].ToString()),
        //             dataTable.Rows[0].ItemArray[1].ToString(), dataTable.Rows[0].ItemArray[2].ToString(),
        //             dataTable.Rows[0].ItemArray[3].ToString(), dataTable.Rows[0].ItemArray[4].ToString(),
        //             DateTime.Parse(dataTable.Rows[0].ItemArray[5].ToString()),
        //             dataTable.Rows[0].ItemArray[6].ToString(), DateTime.Parse(dataTable.Rows[0].ItemArray[7].ToString()),
        //             int.Parse(idAccount), Convert.FromBase64String(dataTable.Rows[0].ItemArray[9].ToString()), int.Parse(dataTable.Rows[0].ItemArray[10].ToString()));
        //    }
        //    catch
        //    {
        //        res.Name = "Chủ sân";
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //    return res;
        //}
        //public List<Employee> GetEmployeesByType(string typeEmployee)
        //{
        //    List<Employee> employees = new List<Employee>();
        //    try
        //    {
        //        OpenConnection();
        //        string queryString = "select * from Employee where isDeleted=0 and position = " + typeEmployee;

        //        SqlCommand command = new SqlCommand(queryString, conn);
        //        command.ExecuteNonQuery();
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);

        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);
        //        for (int i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            int idAccount = -1;
        //            if (dataTable.Rows[i].ItemArray[8].ToString() != "")
        //            {
        //                idAccount = int.Parse(dataTable.Rows[i].ItemArray[8].ToString());
        //            }
        //            Employee employee = new Employee(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
        //                dataTable.Rows[i].ItemArray[1].ToString(), dataTable.Rows[i].ItemArray[2].ToString(),
        //                dataTable.Rows[i].ItemArray[3].ToString(), dataTable.Rows[i].ItemArray[4].ToString(),
        //                DateTime.Parse(dataTable.Rows[i].ItemArray[5].ToString()),
        //                dataTable.Rows[i].ItemArray[6].ToString(), DateTime.Parse(dataTable.Rows[i].ItemArray[7].ToString()),
        //                idAccount, Convert.FromBase64String(dataTable.Rows[i].ItemArray[9].ToString()), int.Parse(dataTable.Rows[i].ItemArray[10].ToString()));
        //            employees.Add(employee);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //    return employees;
        //}

        //public string GetPosition(string id) // Lấy chức vụ khi biết id
        //{
        //    try
        //    {
        //        OpenConnection();
        //        string query = "select position from Employee where idEmployee = " + id;
        //        SqlCommand command = new SqlCommand(query, conn);
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);
        //        adapter.Fill(dt);
        //        return dt.Rows[0].ItemArray[0].ToString();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}
        //public List<string> GetAllPosition()
        //{
        //    try
        //    {
        //        OpenConnection();
        //        List<string> newList = new List<string>();
        //        string query = "select distinct(position) from Employee";
        //        SqlCommand command = new SqlCommand(query, conn);
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);
        //        adapter.Fill(dt);
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            newList.Add(dt.Rows[i].ItemArray[0].ToString());
        //        }
        //        return newList;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}
    }
}
