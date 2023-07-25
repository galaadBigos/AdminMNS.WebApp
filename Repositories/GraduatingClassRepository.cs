using AdminMNS.WebApp.Data;
using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AdminMNS.WebApp.Repositories
{
    public class GraduatingClassRepository : IGraduatingClassRepository
    {
        private readonly AppDbContext _context;

        public GraduatingClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GraduatingClass>> GetGraduatingClasses()
        {
            return await _context.GraduatingClass.ToListAsync();
        }

        public async Task<GraduatingClass?> GetGraduatingClassById(int id)
        {
            return await _context.GraduatingClass.Where(gc => gc.Id == id).FirstOrDefaultAsync();
        }
    }
}
