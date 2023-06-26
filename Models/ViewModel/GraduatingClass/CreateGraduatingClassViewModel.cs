using System.ComponentModel.DataAnnotations;

namespace AdminMNS.WebApp.Models.ViewModel.GraduatingClass
{
    public class CreateGraduatingClassViewModel
	{
        [Required(ErrorMessage = "Le nom de la classe est obligatoire"), Display(Name = "Nom de la formation")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "La date de début de formation est obligatoire"), DataType(DataType.Date), Display(Name = "Date de début de formation")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La date de fin de formation est obligatoire"), DataType(DataType.Date), Display(Name = "Date de fin de formation")]
        public DateTime EndDate { get; set; }
	}
}
