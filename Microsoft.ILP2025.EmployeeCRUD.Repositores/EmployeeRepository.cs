using Microsoft.ILP2025.EmployeeCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
         private const string FilePath = "employees.json";

        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {
            return await Task.FromResult(GetEmployees());
        }

        public async Task<EmployeeEntity> GetEmployee(int id)
        {
            var employees = GetEmployees();
            return await Task.FromResult(employees.FirstOrDefault(e => e.Id == id));
        }

        public async Task CreateEmployee(EmployeeEntity employee)
        {
            var employees = GetEmployees();
            employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
            employees.Add(employee);
            await SaveEmployees(employees);
        }

        public async Task UpdateEmployee(EmployeeEntity updatedEmployee)
        {
            var employees = GetEmployees();
            var employee = employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.Salary = updatedEmployee.Salary;
                employee.Department = updatedEmployee.Department;
                employee.Location = updatedEmployee.Location;
                await SaveEmployees(employees);
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var employees = GetEmployees();
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                await SaveEmployees(employees);
            }
        }

        // 🔽 Add these two methods below 🔽

        private List<EmployeeEntity> GetEmployees()
        {
            if (!File.Exists(FilePath))
                return new List<EmployeeEntity>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<EmployeeEntity>>(json) ?? new List<EmployeeEntity>();
        }

        private async Task SaveEmployees(List<EmployeeEntity> employees)
        {
            var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }





        // public async Task<List<EmployeeEntity>> GetAllEmployees()
        // {
        //     return await Task.FromResult(this.GetEmployees());
        // }

        // public async Task<EmployeeEntity> GetEmployee(int id)
        // {
        //     var employees = this.GetEmployees();

        //     return await Task.FromResult(employees.FirstOrDefault(e => e.Id == id));
        // }

        // private List<EmployeeEntity> GetEmployees()
        // {
        //     var employees = new List<EmployeeEntity>();

        //     employees.Add(new EmployeeEntity { Id = 1, Name = "Pradip" });
        //     employees.Add(new EmployeeEntity { Id = 2, Name = "Shrikanth" });

        //     return employees;
        // }
    }
}
