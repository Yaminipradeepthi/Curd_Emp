using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ILP2025.EmployeeCRUD.Repositores;
using Microsoft.ILP2025.EmployeeCRUD.Servcies;
using Microsoft.ILP2025.EmployeeCRUD.Entities;



namespace Microsoft.ILP2025.EmployeeCRUD.Web.Controllers
{
    public class EmployeeController : Controller
    {
         private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await employeeService.GetAllEmployees();
        return View(employees);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await employeeService.GetEmployee(id);
        return View(employee);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeEntity employee)
    {
        if (ModelState.IsValid)
        {
            await employeeService.CreateEmployee(employee);
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await employeeService.GetEmployee(id);
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeEntity employee)
    {
        if (ModelState.IsValid)
        {
            await employeeService.UpdateEmployee(employee);
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employee = await employeeService.GetEmployee(id);
        if(employee==null){
            return NotFound();
        }
        return View(employee);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await employeeService.DeleteEmployee(id);
        return RedirectToAction(nameof(Index));
    }
        
        
        
        
        
        
       
    }
}
