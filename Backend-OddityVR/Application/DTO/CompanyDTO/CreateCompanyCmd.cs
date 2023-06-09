﻿using Backend_OddityVR.Domain.Model;

namespace Backend_OddityVR.Application.DTO.CompanyDTO
{
    public class CreateCompanyCmd : ICmdAndDTO
    {
        // properties
        public string Name { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }


        // constructor
        public CreateCompanyCmd() { }


        // methods
        public IModel ToModel()
        {
            return new Company
            {
                Name = Name,
                Number = Number,
                Street = Street,
                City = City,
                PostalCode = PostalCode,
                Country = Country,
            };
        }
    }
}
