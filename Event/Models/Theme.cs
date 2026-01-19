namespace Event.Models
{
    public class Theme
    {
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NameArabic { get; set; } = string.Empty;
        public EventType EventType { get; set; }
        public string Background { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = "#ffffff";
        public string Font { get; set; } = "Amiri";
        public string PrimaryColor { get; set; } = "#C8A45D";
        public string SecondaryColor { get; set; } = "#2C3E50";
        public string TextColor { get; set; } = "#333333";
        public string AccentColor { get; set; } = "#E8D5B7";
        public string Animation { get; set; } = "fade-in";
        public string Layout { get; set; } = "centered";
        public string PreviewImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool HasPattern { get; set; } = false;
        public string PatternStyle { get; set; } = string.Empty;
    }
}

