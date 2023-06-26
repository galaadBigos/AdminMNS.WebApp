
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminMNS.WebApp.Data.Entities
{
	public class GraduatingClass
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
