namespace Chat.Core.Domain
{
    public class Status(bool isOnline = false, string customMessage = "")
    {
        public bool IsOnline { get; set; } = isOnline;

        public string CustomMessage { get; set; } = customMessage;

        public static Status Online() => new(true, "Online");

        public static Status Offline() => new(false, "Offline");

        public static Status Custom(string message) => new(false, message);

        public override string ToString() => CustomMessage;
    }
}
