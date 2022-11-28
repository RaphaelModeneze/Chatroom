using System.ComponentModel.DataAnnotations;

namespace Repository.Entities
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public ChatRoom Chatroom { get; set; }
    }
}
