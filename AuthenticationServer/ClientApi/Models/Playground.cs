using System.ComponentModel.DataAnnotations;

namespace BackEndProject.Models
{
    public class Playground
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; } 
        public string CordinatesX { get; set; }
        public string CordinatesY { get; set; }
        public int Capacity { get; set; }


    }
}
