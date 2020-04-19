using Microsoft.EntityFrameworkCore;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _db;
        public SkillRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Skill entity)
        {
            await _db.Skills.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Skill entity)
        {
            _db.Skills.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Skill>> FindAll()
        {
            var Skill = await _db.Skills.ToListAsync();
            return Skill;
        }

        public async Task<Skill> FindById(int id)
        {
            var Skill = await _db.Skills.FindAsync(id);
            return Skill;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Skill entity)
        {
            _db.Skills.Update(entity);
            return await Save();
        }
    }
}
