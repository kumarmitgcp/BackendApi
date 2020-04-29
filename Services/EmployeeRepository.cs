using BackendApi.DBContexts;
using BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Services
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly CoreDbContext _context;

        public EmployeeRepository (CoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddEmployee(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException(nameof(emp));
            }
            _context.Employee.Add(emp);
        }

        public void DeleteEmployee(Employee emp)
        {
            _context.Employee.Remove(emp);
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Employee.Where(c => c.EmployeeId == employeeId).FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employee.OrderBy(c => c.EmployeeId).ToList();
        }


        public bool EmployeeExists(int employeeId)
        {
            return _context.Employee.Any(a => a.EmployeeId == employeeId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateEmployee(Employee course)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EmployeeRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        
        #endregion
    }
}
