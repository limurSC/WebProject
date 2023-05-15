using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPortfolio.Domain.Entity
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string Images { get; set; }
        public string Image { get; set; }
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
    }
}
