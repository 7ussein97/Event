using System.ComponentModel.DataAnnotations;

namespace Event.Models
{
    public class EventData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string BrideName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string GroomName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string InviterName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? GuestName { get; set; }

        [StringLength(200)]
        public string? Prayer { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public TimeSpan EventTime { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [StringLength(500)]
        public string? GoogleMapsUrl { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }

        [StringLength(200)]
        public string? ClosingMessage { get; set; }

        [StringLength(500)]
        public string? YouTubeUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string ThemeKey { get; set; } = string.Empty;

        public EventType EventType { get; set; }

        [Required]
        [StringLength(100)]
        public string Slug { get; set; } = string.Empty;

        // Secret token for editing - given to creator only
        [Required]
        [StringLength(64)]
        public string EditToken { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }

        // Computed property to check if event is expired
        public bool IsExpired => EventDate.Add(EventTime) < DateTime.Now;

        public DateTime EventDateTime => EventDate.Add(EventTime);

        // Convert to EventDetails for view rendering
        public EventDetails ToEventDetails()
        {
            return new EventDetails
            {
                Title = Title,
                BrideName = BrideName,
                GroomName = GroomName,
                InviterName = InviterName,
                GuestName = GuestName,
                Prayer = Prayer,
                Date = EventDate,
                Time = EventTime,
                Location = Location,
                GoogleMapsUrl = GoogleMapsUrl,
                Message = Message,
                ClosingMessage = ClosingMessage,
                YouTubeUrl = YouTubeUrl,
                ThemeKey = ThemeKey,
                EventType = EventType,
                Slug = Slug
            };
        }

        // Create from EventDetails
        public static EventData FromEventDetails(EventDetails details)
        {
            return new EventData
            {
                Title = details.Title,
                BrideName = details.BrideName,
                GroomName = details.GroomName,
                InviterName = details.InviterName,
                GuestName = details.GuestName,
                Prayer = details.Prayer,
                EventDate = details.Date,
                EventTime = details.Time,
                Location = details.Location,
                GoogleMapsUrl = details.GoogleMapsUrl,
                Message = details.Message,
                ClosingMessage = details.ClosingMessage,
                YouTubeUrl = details.YouTubeUrl,
                ThemeKey = details.ThemeKey,
                EventType = details.EventType,
                Slug = details.Slug,
                EditToken = GenerateEditToken(),
                CreatedAt = DateTime.UtcNow
            };
        }

        // Update from EventDetails (for editing)
        public void UpdateFromEventDetails(EventDetails details)
        {
            Title = details.Title;
            BrideName = details.BrideName;
            GroomName = details.GroomName;
            InviterName = details.InviterName;
            GuestName = details.GuestName;
            Prayer = details.Prayer;
            EventDate = details.Date;
            EventTime = details.Time;
            Location = details.Location;
            GoogleMapsUrl = details.GoogleMapsUrl;
            Message = details.Message;
            ClosingMessage = details.ClosingMessage;
            YouTubeUrl = details.YouTubeUrl;
            ThemeKey = details.ThemeKey;
            UpdatedAt = DateTime.UtcNow;
        }

        // Generate a secure random token for editing
        private static string GenerateEditToken()
        {
            var bytes = new byte[32];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").TrimEnd('=');
        }
    }
}
