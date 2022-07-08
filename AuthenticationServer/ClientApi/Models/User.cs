using BackEndProject.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public List<Event_Participans> Event_Participans { get; set; }
    }
}
