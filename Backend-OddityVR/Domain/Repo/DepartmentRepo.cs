﻿using System.Data.SqlClient;

namespace Backend_OddityVR.Domain.Repo
{
    public class DepartmentRepo : AbstractRepo
    {
        // create
        public Department CreateNewDepartment(Department department)
        {
            string query =
                "INSERT INTO Department " +
                "(Name, Id_Company) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@Name, @CompanyId)";
            SqlCommand command = new(query, _database.GetDbConnection());
            AddParameters(command, department);

            // Starting connection with DB and executing
            _database.GetDbConnection().Open();

            int departmentId = (int) command.ExecuteScalar();

            _database.GetDbConnection().Close();
            command.Connection.Close();

            return GetDepartmentById(departmentId);
        }


        // get all
        public List<Department> GetAllDepartments()
        {
            string query =
                "SELECT * " +
                "FROM Department";
            SqlCommand command = new(query, _database.GetDbConnection());

            // Starting connection with DB and executing
            _database.GetDbConnection().Open();

            SqlDataReader sqlReader = command.ExecuteReader();
            List<Department> departments = ToModel(sqlReader);

            _database.GetDbConnection().Close();

            sqlReader.Close();
            command.Connection.Close();

            return departments;
        }


        // get id
        public Department GetDepartmentById(int id)
        {
            string query =
                "SELECT * " +
                "FROM Department " +
                "WHERE ID = @Id";
            SqlCommand command = new(query, _database.GetDbConnection());
            command.Parameters.AddWithValue("@Id", id);

            // Starting connection with DB and executing
            _database.GetDbConnection().Open();

            SqlDataReader sqlReader = command.ExecuteReader();
            Department department = ToModel(sqlReader).First();

            _database.GetDbConnection().Close();

            sqlReader.Close();
            command.Connection.Close();

            return department;
        }


        // get by company id
        public List<Department> GetDepartmentByCompanyId(int id)
        {
            string query =
                "SELECT * FROM Department " +
                "INNER JOIN Company " +
                "ON Department.Id_Company = Company.Id " +
                "WHERE Id_Company = @Id";
            SqlCommand command = new(query, _database.GetDbConnection());
            command.Parameters.AddWithValue("@Id", id);

            // Starting connection with DB and executing
            _database.GetDbConnection().Open();

            SqlDataReader sqlReader = command.ExecuteReader();
            List<Department> departments = ToModel(sqlReader);

            _database.GetDbConnection().Close();

            sqlReader.Close();
            command.Connection.Close();

            return departments;
        }


        // update
        public void UpdateDepartment(Department Department)
        {
            string query =
                "UPDATE Department " +
                "SET Name = @Name, Id_Company = @CompanyId " +
                "WHERE Id = @Id";
            SqlCommand command = new(query, _database.GetDbConnection());
            AddParameters(command, Department);

            // Starting connection with DB and executing
            _database.GetDbConnection().Open();

            SqlDataReader sqlReader = command.ExecuteReader();

            _database.GetDbConnection().Close();
            sqlReader.Close();
            command.Connection.Close();
        }


        // delete
        public void DeleteDepartment(int id)
        {
            string query =
                "DELETE FROM Department " +
                "WHERE Id = @Id";
            SqlCommand command = new(query, _database.GetDbConnection());
            command.Parameters.AddWithValue("@Id", id);

            // Starting connection with DB and executing
            _database.GetDbConnection().Open();

            SqlDataReader sqlReader = command.ExecuteReader();

            _database.GetDbConnection().Close();

            sqlReader.Close();
            command.Connection.Close();
        }

        // methods
        public List<Department> ToModel(SqlDataReader reader)
        {
            List<Department> listDepartments = new();
            while (reader.Read())
            {
                listDepartments.Add(new Department()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    CompanyId = int.Parse(reader["Id_Company"].ToString())
                });
            }
            return listDepartments;
        }

        public SqlCommand AddParameters(SqlCommand command, Department department)
        {
            command.Parameters.AddWithValue("@Name", department.Name);
            command.Parameters.AddWithValue("@CompanyId", department.CompanyId);
            if (department.Id != null)
                command.Parameters.AddWithValue("@Id", department.Id);

            return command;
        }
    }
}
