﻿using EmployeePortal.Data;
using EmployeePortal.Models.Entities;
using EmployeePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace EmployeePortal.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbcontext dbContext;

        public EmployeesController(ApplicationDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            var employee = new Employee
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Salary = viewModel.Salary,
                Subscribed = viewModel.Subscribed
            };

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            return View(employee);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(Employee viewModel)
        {
            var employee = await dbContext.Employees.FindAsync(viewModel.Id);
            
            if (employee is not null)
            {
                employee.Name = viewModel.Name;
                employee.Email = viewModel.Email;
                employee.Phone = viewModel.Phone;
                employee.Salary = viewModel.Salary;
                employee.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Employees"); 
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Employee viewModel)
        {
            var employee = await dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id ==viewModel.Id);

            if (employee is not null)
            {
                dbContext.Employees.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Employees");
        }

    }
}
