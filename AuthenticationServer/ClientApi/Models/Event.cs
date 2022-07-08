using BackEndProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientApi.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public User Organiser { get; set; }


        //Relationship
        public List<Event_Participans> Event_Participans { get; set; }
        public int PlaygroundId { get; set; }
        [ForeignKey("PlaygroundId")]
        public Playground Playground { get; set; }
    }
}
