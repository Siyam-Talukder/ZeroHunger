using System;
using System.Collections.Generic;
using System.Text;
using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repo
{
    public class ResturantRepo
    {
        ZeroHungerDbContext db;
        public ResturantRepo(ZeroHungerDbContext db)
        {
            this.db = db;
        }

        public Resturant? GetById(int id)
        {
            return db.Resturants.Find(id);
        }

        public List<Resturant> All()
        {
            var data = db.Resturants.ToList();
            return data;
        }

        public bool Create(Resturant r)
        {
            db.Resturants.Add(r);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var r = GetById(id);
            if (r == null) return false;

            db.Resturants.Remove(r);
            return db.SaveChanges() > 0;
        }

        public bool Update(int id, Resturant res)
        {
            var old = GetById(id);
            old.Name = res.Name;
            old.Address = res.Address;
            old.Phone = res.Phone;
            return db.SaveChanges() > 0;
        }
    }
}
