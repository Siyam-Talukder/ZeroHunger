using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repo
{
    public class EmployeeRepo
    {
        ZeroHungerDbContext db;
        public EmployeeRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public Employee? GetById(int id)
        {
            return db.Employees.Find(id);
        }

        public List<Employee> All()
        {
            var data = db.Employees.ToList();
            return data;
        }

        public bool Create(Employee r)
        {
            db.Employees.Add(r);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var r = GetById(id);
            if (r == null) return false;

            db.Employees.Remove(r);
            return db.SaveChanges() > 0;
        }

        public bool Update(int id, Employee res)
        {
            var old = GetById(id);
            old.Name = res.Name;
            old.Phone = res.Phone;
            return db.SaveChanges() > 0;
        }
    }
}
