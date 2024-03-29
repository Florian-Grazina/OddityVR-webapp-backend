﻿using Backend_OddityVR.Domain.Model;
using Backend_OddityVR.Domain.Service;
using Backend_OddityVR.Infrastructure.Repo.Interfaces;
using System.Data.SqlClient;

namespace Backend_OddityVR.Infrastructure.Repo
{
    public class DepartmentRepo : AbstractRepo
    {
        // constructor
        public DepartmentRepo(Database database) : base(database)
        {
        }


        // create
        public Department CreateNewDepartment(Department department)
        {
            string query =
                "INSERT INTO Department " +
                "(Name, Id_Company) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@Name, @CompanyId)";

            using SqlCommand command = new(query, GetDatabase().GetDbConnection());
            RepoHelper.AddParameters(command, department);

            int departmentId = (int)command.ExecuteScalar();

            return GetDepartmentById(departmentId);
        }


        // get all
        public List<Department> GetAllDepartments()
        {
            string query =
                "SELECT * " +
                "FROM Department";

            using SqlCommand command = new(query, GetDatabase().GetDbConnection());

            using SqlDataReader sqlReader = command.ExecuteReader();
            List<Department> departments = ToModel(sqlReader);

            return departments;
        }


        // get id
        public Department GetDepartmentById(int id)
        {
            string query =
                "SELECT * " +
                "FROM Department " +
                "WHERE ID = @Id";

            using SqlCommand command = new(query, GetDatabase().GetDbConnection());
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader sqlReader = command.ExecuteReader();
            Department department = ToModel(sqlReader).First();

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

            using SqlCommand command = new(query, GetDatabase().GetDbConnection());
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader sqlReader = command.ExecuteReader();
            List<Department> departments = ToModel(sqlReader);

            return departments;
        }


        // update
        public void UpdateDepartment(Department department)
        {
            string query =
                "UPDATE Department " +
                "SET Name = @Name, Id_Company = @CompanyId " +
                "WHERE Id = @Id";

            using SqlCommand command = new(query, GetDatabase().GetDbConnection());
            RepoHelper.AddParameters(command, department);

            command.ExecuteNonQuery();
        }


        // delete
        public void DeleteDepartment(int id)
        {
            string query =
                "DELETE FROM Department " +
                "WHERE Id = @Id";

            using SqlCommand command = new(query, GetDatabase().GetDbConnection());
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
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
    }
}
