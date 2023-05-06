﻿using System.ComponentModel.DataAnnotations;

namespace Backend_OddityVR.Department.DTO
{
    public class CreateDepartmentCmd
    {
        // properties
        [Required(ErrorMessage = "Department name is mandatory")]
        public string Name { get; set; }
        public int CompanyId { get; set; }


        // constructor
        public CreateDepartmentCmd() { }


        // methods
        public Department ToModel(int id = 0)
        {
            return new Department
            {
                Id = id,
                Name = this.Name,
                CompanyId = this.CompanyId
            };
        }
    }
}
