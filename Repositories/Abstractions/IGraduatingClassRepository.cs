using AdminMNS.WebApp.Data.Entities;

namespace AdminMNS.WebApp.Repositories.Abstractions
{
    public interface IGraduatingClassRepository
    {
        public Task<List<GraduatingClass>> GetGraduatingClasses();

        public Task<GraduatingClass?> GetGraduatingClassById(int id);
    }
}
