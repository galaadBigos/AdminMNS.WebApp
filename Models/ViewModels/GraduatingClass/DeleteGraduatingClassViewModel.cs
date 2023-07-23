using System.ComponentModel.DataAnnotations;

namespace AdminMNS.WebApp.Models.ViewModel.GraduatingClass
{
    public class DeleteGraduatingClassViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        public string? Name { get; set; }

        [Display(Name = "Date de début"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Date de fin"), DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public DeleteGraduatingClassViewModel()
        {
            
        }

        public DeleteGraduatingClassViewModel(Data.Entities.GraduatingClass graduatingClass)
        {
            Id = graduatingClass.Id;
            Name = graduatingClass.Name;
            StartDate = graduatingClass.StartDate;
            EndDate = graduatingClass.EndDate;
        }
    }
}
