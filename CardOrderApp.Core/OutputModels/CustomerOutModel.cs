﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.Core.OutputModels
{
    public class CustomerOutModel
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string PassportSeries { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }


        public CustomerOutModel(int Id, string fullName, string passportNumber, string passportSeries, string phoneNumber, string email)
        {
            CustomerId = Id;
            FullName = fullName;
            PassportNumber = passportNumber;
            PassportSeries = passportSeries;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
