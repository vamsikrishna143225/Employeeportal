﻿namespace EmployeePortal.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Salary { get; set; }


        public bool Subscribed { get; set; }
    }
}
