using Assignment2_EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Assignment2_EF.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int studentId)
        {
            return _context.Employees.Find(studentId);
        }
        /// <summary>
        /// Author:Sumit Singh
        /// Date:09-oct-2024
        /// Scope: Exception handling
        /// </summary>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>
        public int AddEmployee(Employee employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                try
                {
                    _context.Employees.Add(employeeEntity);
                    _context.SaveChanges();
                    result = employeeEntity.EmployeeId;
                }
                catch (DbUpdateException dbEx)
                {
                    // Log the database update exception
                    LogError(dbEx);
                    throw new ApplicationException("There was an issue adding the employee. Please try again later.");
                }
                catch (Exception ex)
                {
                    // Log the general exception
                    LogError(ex);
                    throw new ApplicationException("An unexpected error occurred. Please try again later.");
                }
            }

            return result;
        }

        private void LogError(Exception ex)
        {
            
            System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt"), ex.ToString());
        }
        /// <summary>
        /// Author:Sumit Singh
        /// Date: 09-oct-2014
        /// Scope: Update and Exception handling
        /// </summary>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>

        public int UpdateEmployee(Employee employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                try
                {
                    
                    _context.Entry(employeeEntity).State = EntityState.Modified;
                    _context.SaveChanges(); // Save changes to the database
                    result = employeeEntity.EmployeeId; // Assuming EmployeeId is valid
                }
                catch (DbUpdateConcurrencyException concurrencyEx)
                {
                    // Log the concurrency exception
                    LogError(concurrencyEx);
                    throw new ApplicationException("The employee record you attempted to update no longer exists. Please try again.");
                }
                catch (DbUpdateException dbEx)
                {
                    // Log the database update exception
                    LogError(dbEx);
                    throw new ApplicationException("There was an issue updating the employee. Please try again later.");
                }
                catch (Exception ex)
                {
                    // Log the general exception
                    LogError(ex);
                    throw new ApplicationException("An unexpected error occurred while updating the employee. Please try again later.");
                }
            }

            return result;
        }
        //Parametrrised query
        public List<Employee> GetEmployeeByName(string empName)
        {
            return _context.Employees
                .SqlQuery("SELECT * FROM Employees WHERE Name = @p0", empName)
                .ToList();
        }



        public void DeleteEmployee(int employeeId)
        {
            Employee employeeEntity = _context.Employees.Find(employeeId);
            _context.Employees.Remove(employeeEntity);
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}