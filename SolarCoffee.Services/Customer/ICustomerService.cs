﻿using System;
using System.Collections.Generic;
using System.Text;
using SolarCoffee.Services.product;

namespace SolarCoffee.Services.Customer
{
    public interface ICustomerService
    {
        List<Data.Models.Customer> GetAllCustomers();
        ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        Data.Models.Customer GetCustomerById(int id);
    }
}
