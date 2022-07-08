using ClientApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndProject.Models
{
    public class Event_Participans
    {
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
