using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Core.Domain
{
    /// <summary>
    /// User class contains profile information and relationships
    /// with other users (contacts and conversations).
    /// </summary>
    public class User(string name)
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = name;

        public Status Status { get; set; } = Status.Offline();

        List<Contact> Contacts { get; set; } = [];

        List<Conversation> Conversations { get; set; } = [];

        internal User() : this(string.Empty)
        {
        }

        /// <summary>
        /// Get a user's profile including name, status, and profile picture...
        /// </summary>
        /// <returns></returns>
        public Profile GetProfile() => new(Name, Status);

        /// <summary>
        /// Functionality to add a new contact by making a request.
        /// </summary>
        /// <param name="user"></param>
        public void AddContact(User user)
        {
            Contacts.Add(new Contact(this, user));
            // In a real application, we would add request, pending status, etc.
        }

        /// <summary>
        /// Functionality to remove a contact.
        /// </summary>
        /// <param name="contact"></param>
        public void RemoveContact(Contact contact)
        {
            if (!Contacts.Contains(contact))
            {
                Contacts.Remove(contact);
            }
        }

        /// <summary>
        /// Update the current status of the user.
        /// </summary>
        /// <param name="newStatus"></param>
        public void UpdateStatus(Status newStatus)
        {
            Status = newStatus;
        }

        /// <summary>
        /// Send a message to a specific conversation.
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="content"></param>
        public void SendMessageToConversation(Guid conversationId, string content)
        {
            var conversation = Conversations.FirstOrDefault(c => c.Id == conversationId);
            conversation?.AddMessage(new Message(this, content));
        }

        /// <summary>
        /// Get conversation by identifier.
        /// </summary>
        /// <param name="conversationId"></param>
        public Conversation? GetConversation(Guid conversationId)
        {
            return Conversations.FirstOrDefault(c => c.Id == conversationId);
        }

        /// <summary>
        /// Join a conversation.
        /// </summary>
        /// <param name="conversation"></param>
        public void JoinConversation(Conversation conversation)
        {
            if (!Conversations.Contains(conversation))
            {
                Conversations.Add(conversation);
            }
        }

        /// <summary>
        /// Part a conversation.
        /// </summary>
        /// <param name="conversation"></param>
        public void PartConversation(Conversation conversation)
        {
            if (!Conversations.Contains(conversation))
            {
                Conversations.Remove(conversation);
            }
        }
    }
}
