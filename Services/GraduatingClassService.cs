using AdminMNS.WebApp.Data;
using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AdminMNS.WebApp.Services
{
    public class GraduatingClassService : IGraduatingClassService
    {
        private readonly AppDbContext _context;

        public GraduatingClassService(AppDbContext context)
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
