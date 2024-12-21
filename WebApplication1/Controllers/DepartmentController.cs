using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.services;

namespace WebApplication1.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentServices departmentServices;

		public DepartmentController(IDepartmentServices DepartmentServices)
		{
			departmentServices = DepartmentServices;
		}
        [Authorize]
        public async Task<IActionResult> GetAll()
		{
			var departments = await departmentServices.GetAll();
			return View(departments);
		}

        [Authorize(Roles ="Admin")]
        public IActionResult Add()
        
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Department model)
        {
            if (ModelState.IsValid)
            {
                // Create a new department object from the model
                var department = new Department
                {
                    Name = model.Name,
                    budget = model.budget  // Correct property capitalization
                };

                try
                {
                    // Call the service to add the department
                    await departmentServices.Add(department);

                    // Save the department in the database
                    await departmentServices.Save();

                    // Return a success response
                    return Json(new { success = true, message = "Department added successfully!" });
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
            var department =await departmentServices.GetById(id);
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Department model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Id = model.Id,
                    Name = model.Name,
                    budget = model.budget  
                };

                try
                {
                    departmentServices.Update(department);

                    await departmentServices.Save();

                    return Json(new { success = true, message = "Department updated successfully!" });
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
            await departmentServices.Delete(id);
            await departmentServices.Save();
            return RedirectToAction("GetAll");
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var department = await departmentServices.GetById(id);
            return View(department);
        }

    }
}
