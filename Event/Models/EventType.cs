namespace Event.Models
{
    public enum EventType
    {
        Wedding
    }

    public static class EventTypeExtensions
    {
        public static string GetDisplayName(this EventType eventType)
        {
            return eventType switch
            {
                EventType.Wedding => "زفاف",
                _ => eventType.ToString()
            };
        }

        public static string GetIcon(this EventType eventType)
        {
            return eventType switch
            {
                EventType.Wedding => "💒",
                _ => "📅"
            };
        }

        public static string GetDescription(this EventType eventType)
        {
            return eventType switch
            {
                EventType.Wedding => "دعوات زفاف أنيقة وراقية",
                _ => ""
            };
        }
    }
}
