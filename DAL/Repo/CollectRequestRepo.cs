using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repo
{
    public class CollectRequestRepo
    {
        ZeroHungerDbContext db;
        public CollectRequestRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public CollectRequest? GetById(int id)
        {
            return db.CollectRequests.Find(id);
        }

        public List<CollectRequest> All()
        {
            var data = db.CollectRequests.ToList();
            return data;
        }

        public bool Create(CollectRequest r)
        {
            db.CollectRequests.Add(r);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var r = GetById(id);
            if (r == null) return false;

            db.CollectRequests.Remove(r);
            return db.SaveChanges() > 0;
        }

        public bool Update(int id, CollectRequest res)
        {
            var old = GetById(id);
            old.FoodDescription = res.FoodDescription;
            old.Quantity = res.Quantity;
            old.MaxPreservationTime = res.MaxPreservationTime;
            old.Status = res.Status;
            old.CreatedAt = res.CreatedAt;
            old.RestaurantId = res.RestaurantId;
            old.EmployeeId = res.EmployeeId;
            old.CompletedAt = res.CompletedAt;
            return db.SaveChanges() > 0;
        }

        public List<CollectRequest> GetAllTasksForEmployee(int employeeId)
        {
            var data = (from req in db.CollectRequests
                        where req.EmployeeId == employeeId
                        select req).ToList();

            return data;
        }
    }
}
