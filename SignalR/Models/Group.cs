using System.Collections;

namespace SignalR.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public ICollection<GroupMessage> Messages { get; set; }
    }
}
