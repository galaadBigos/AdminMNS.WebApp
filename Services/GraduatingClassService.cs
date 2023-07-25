using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Repositories.Abstractions;
using AdminMNS.WebApp.Services.Abstractions;

namespace AdminMNS.WebApp.Services
{
    public class GraduatingClassService : IGraduatingClassService
    {
        private readonly IGraduatingClassRepository _repository;

        public GraduatingClassService(IGraduatingClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GraduatingClass>> GetGraduatingClasses()
        {
            return await _repository.GetGraduatingClasses();
        }

        public async Task<GraduatingClass?> GetGraduatingClassById(int id)
        {
            return await _repository.GetGraduatingClassById(id);
        }
    }
}
