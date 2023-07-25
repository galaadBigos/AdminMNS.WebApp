using AdminMNS.WebApp.Data.Entities;

namespace AdminMNS.WebApp.Services.Abstractions
{
    public interface IGraduatingClassService
    {
        Task<List<GraduatingClass>> GetGraduatingClasses();
        Task<GraduatingClass?> GetGraduatingClassById(int id);

    }
}
