using Chat.Core.Domain;
using System;
using System.Collections.Generic;

namespace Chat.Core
{
    public class ChatService
    {
        readonly List<User> _users = [];

        public User RegisterUser(string username)
        {
            var user = new User(username);
            _users.Add(user);
            return user;
        }

        public void SendMessageToConversation(User sender, Guid conversationId, string message)
        {
            _users.ForEach(user =>
            {
                user.GetConversation(conversationId)?
                    .AddMessage(new Message(sender, message));

            });
        }

        public Conversation CreatePrivateConversation(User user1, User user2)
        {
            PrivateConversation conversation = new(user1, user2);
            user1.JoinConversation(conversation);
            user2.JoinConversation(conversation);
            return conversation;
        }

        public Conversation CreateGroupConversation(string groupName, List<User> participants)
        {
            var conversation = new GroupConversation(groupName, participants);
            foreach (var participant in participants)
            {
                participant.JoinConversation(conversation);
            }
            return conversation;
        }

        public void AddContactRequest(User user, User contact)
        {
            user.AddContact(contact);
        }
    }
}
