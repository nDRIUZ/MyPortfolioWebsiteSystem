using Microsoft.EntityFrameworkCore;
using MyPortfolioWeb.Contracts;
using MyPortfolioWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolioWeb.Repository
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly ApplicationDbContext _db;
        public TestimonialRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Testimonial entity)
        {
            await _db.Testimonials.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Testimonial entity)
        {
            _db.Testimonials.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Testimonial>> FindAll()
        {
            var ToList = await _db.Testimonials.ToListAsync();
            return ToList;
        }

        public async Task<Testimonial> FindById(int id)
        {
            var ToFind = await _db.Testimonials.FindAsync(id);
            return ToFind;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Testimonial entity)
        {
            _db.Testimonials.Update(entity);
            return await Save();
        }
    }
}
