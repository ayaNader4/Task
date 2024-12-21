using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployeeServices employeeServices;

        public IDepartmentServices DepartmentServices { get; }

        public EmployeeController(IEmployeeServices employeeServices,IDepartmentServices departmentServices)
		{
			this.employeeServices = employeeServices;
            DepartmentServices = departmentServices;
        }
        [Authorize]
        public async Task<IActionResult> GetAll()
		{
            ViewBag.Department = await DepartmentServices.GetAll();
            var employees =await employeeServices.GetAll();
			return View(employees);
		}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add() {
            ViewBag.Department= await DepartmentServices.GetAll();

        return View();
		}

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Employee model)
        {
            if (ModelState.IsValid)
            {
                // Create a new department object from the model
                var employee = new Employee
                {
                    Name = model.Name,
                    salary = model.salary,
                    DepartmentId = model.DepartmentId
                };

                try
                {
                    // Call the service to add the department
                    await employeeServices.Add(employee);

                    // Save the department in the database
                    await employeeServices.Save();

                    // Return a success response
                    return Json(new { success = true, message = "Employee added successfully!" });
                }
                catch (Exception ex)
                {
                    // Return a failure response in case of an error
                    return Json(new { success = false, message = $"Error occurred: {ex.Message}" });
                }
            }

            // If model state is not valid, return a failure response
            return Json(new { success = false, message = "Invalid data!" });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Department = await DepartmentServices.GetAll();
            var employee = await employeeServices.GetById(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Employee model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    Name = model.Name,
                    salary = model.salary,
                    DepartmentId = model.DepartmentId
                };

                try
                {
                    employeeServices.Update(employee);

                    await employeeServices.Save();

                    return Json(new { success = true, message = "Employee updated successfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Error occurred: {ex.Message}" });
                }
            }

            return Json(new { success = false, message = "Invalid data!" });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeeServices.Delete(id);
            await employeeServices.Save();
            return RedirectToAction("GetAll");
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await employeeServices.GetById(id);
            return View(employee);
        }


    }
}
