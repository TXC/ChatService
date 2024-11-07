using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Core.Domain
{
    /// <summary>
    /// Base class for conversation, can be extended by private chat or group chat.
    /// </summary>
    public abstract class Conversation(IEnumerable<User> participants)
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<User> Participants { get; set; } = [.. participants];

        public List<Message> Messages { get; set; } = [];

        protected static IEnumerable<User> ValidateParticipants(IEnumerable<User> participants)
        {
            IEnumerable<User> values = [.. participants.Distinct()];

            if (values.Count() != participants.Count())
            {
                throw new ArgumentException("Non-unique participants");
            }
            if (values.Count() < 2)
            {
                throw new ArgumentException("A conversation needs atleast 2 participants");
            }

            return values;
        }

        public virtual bool IsParticipating(User user)
        {
            return Participants.Contains(user);
        }

        public virtual void AddMessage(Message message)
        {
            if (IsParticipating(message.Sender))
            {
                Messages.Add(message);
            }
        }
    }

    /// <summary>
    /// Private Chat class, inherits from Conversation.
    /// <para>This is a one-to-one conversation.</para>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="recipient"></param>
    public class PrivateConversation(User sender, User recipient)
        : Conversation(ValidateParticipants([sender, recipient]))
    {
    }

    /// <summary>
    /// Group Chat class, inherits from Conversation.
    /// <para>This involves multiple users.</para>
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="participants"></param>
    public class GroupConversation(string groupName, List<User> participants)
        : Conversation(ValidateParticipants(participants))
    {
        public string GroupName { get; private set; } = groupName;
    }
}
