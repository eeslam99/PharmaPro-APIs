using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectAPI.BL.Repos
{
    public class PharmacistRepo : IPharmacist
    {
        private readonly DataContext db;

        public PharmacistRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(Pharmacist Pharmacist)
        {
            this.db.Pharmacists.Add(Pharmacist);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = db.Pharmacists.Where(a => a.Id == id).Include(a => a.orderHistories).FirstOrDefault();

            if (data.orderHistories.Count() > 0)
            {
                throw new Exception("can not delete Pharmacist assigned To Order");

            }
            this.db.Pharmacists.Remove(db.Pharmacists.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Pharmacist> GetAll()
        {
            return db.Pharmacists.ToList();
        }

        public Pharmacist GetById(int id)
        {
            return db.Pharmacists.Find(id);
        }

        public void Update(Pharmacist Pharmacist)
        {
            db.Entry(Pharmacist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
