namespace Chat.Core.Domain
{
    public class Profile(string name, Status status)
    {
        public string Name { get; set; } = name;

        public Status CurrentStatus { get; set; } = status;
    }
}
