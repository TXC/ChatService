namespace Chat.Core.Domain
{
    /// <summary>
    /// Contact class represents a relationship between two users.
    /// </summary>
    public class Contact(User owner, User user, bool isConfirmed = false)
    {
        private string _nickname = string.Empty;

        public User Owner { get; set; } = owner;

        public User User { get; set; } = user;

        public bool IsConfirmed { get; set; } = isConfirmed;

        public string NickName
        {
            get => _nickname != string.Empty ? _nickname : User.Name;
        }

        /// <summary>
        /// Confirmation logic should be handled in a service layer, but for simplicity:
        /// </summary>
        public void ConfirmContact()
        {
            IsConfirmed = true;
        }

        /// <summary>
        /// Set a personalized name on a contact
        /// </summary>
        /// <param name="newName"></param>
        public void ChangeNickName(string newName)
        {
            _nickname = newName.Trim();
        }
    }
}
