using Microsoft.EntityFrameworkCore;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly ApplicationDbContext _db;
        public ExperienceRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Experience entity)
        {
            await _db.Experiences.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Experience entity)
        {
            _db.Experiences.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Experience>> FindAll()
        {
            var ToSave = await _db.Experiences.ToListAsync();
            return ToSave;
        }

        public async Task<Experience> FindById(int id)
        {
            var ToSave = await _db.Experiences.FindAsync(id);
            return ToSave;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Experience entity)
        {
            _db.Experiences.Update(entity);
            return await Save();
        }
    }
}
