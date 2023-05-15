using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Domain.ViewModel
{
    public class ProjectViewModel
    {
        [Required(ErrorMessage ="Дайте проекту название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Напишите описание проекта")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Загрузите изображения проекта")]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        [Required(ErrorMessage = "Загрузите обложку проекта")]
        public IFormFile Image { get; set; }
        public bool GraphicalDesign { get; set; }
        public bool ProductDesign { get; set; }
        public bool InteractiveDesign { get; set; }
        public bool ClothDesign { get; set; }
        public bool WebDesign { get; set; }
        public bool Photo { get; set; }
        public bool Art { get; set; }
        public bool Illustration { get; set; }
        public bool AdPhoto { get; set; }
        public bool DigitalArt { get; set; }



        public int AuthorId { get; set; }
    }
}
