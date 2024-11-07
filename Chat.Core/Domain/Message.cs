using System;

namespace Chat.Core.Domain
{
    /// <summary>
    /// Message class represents the communication
    /// between users within a conversation.
    /// </summary>
    public class Message(User sender, string content)
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public User Sender { get; set; } = sender;

        public string Content { get; set; } = content;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
