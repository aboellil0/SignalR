using System.ComponentModel.DataAnnotations;

namespace SignalR.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public string SinderName { get; set; }
        public string Message { get; set; }
        [Required]
        public Group group { get; set; }
        public int groupId { get; set; }
    }
}
