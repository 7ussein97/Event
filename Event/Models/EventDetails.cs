using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Event.Models
{
    public class EventDetails
    {
        [Required(ErrorMessage = "عنوان الحدث مطلوب")]
        [StringLength(100, ErrorMessage = "العنوان يجب أن لا يتجاوز 100 حرف")]
        [Display(Name = "عنوان الحدث")]
        public string Title { get; set; } = string.Empty;

        // Bride and Groom names
        [Required(ErrorMessage = "اسم العروس مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم يجب أن لا يتجاوز 50 حرف")]
        [Display(Name = "اسم العروس")]
        public string BrideName { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم العريس مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم يجب أن لا يتجاوز 50 حرف")]
        [Display(Name = "اسم العريس")]
        public string GroomName { get; set; } = string.Empty;

        // The person sending the invitation (e.g., أم العروس)
        [Required(ErrorMessage = "اسم صاحب الدعوة مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم يجب أن لا يتجاوز 100 حرف")]
        [Display(Name = "صاحب الدعوة")]
        public string InviterName { get; set; } = string.Empty;

        // Legacy field - maps to InviterName
        public string HostName 
        { 
            get => InviterName; 
            set => InviterName = value; 
        }

        [StringLength(50, ErrorMessage = "الاسم يجب أن لا يتجاوز 50 حرف")]
        [Display(Name = "اسم المدعو")]
        public string? GuestName { get; set; }

        // Opening prayer/blessing
        [StringLength(200, ErrorMessage = "الدعاء يجب أن لا يتجاوز 200 حرف")]
        [Display(Name = "الدعاء")]
        public string? Prayer { get; set; } = "اللهم بارك لهما وبارك عليهما واجمع بينهما في خير";

        [Required(ErrorMessage = "تاريخ الحدث مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today.AddDays(30);

        [Required(ErrorMessage = "وقت الحدث مطلوب")]
        [Display(Name = "الوقت")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; } = new TimeSpan(18, 0, 0);

        [Required(ErrorMessage = "مكان الحدث مطلوب")]
        [StringLength(200, ErrorMessage = "العنوان يجب أن لا يتجاوز 200 حرف")]
        [Display(Name = "المكان")]
        public string Location { get; set; } = string.Empty;

        [Url(ErrorMessage = "الرجاء إدخال رابط صحيح")]
        [Display(Name = "رابط خرائط جوجل")]
        public string? GoogleMapsUrl { get; set; }

        // Custom message in the middle
        [StringLength(500, ErrorMessage = "الرسالة يجب أن لا تتجاوز 500 حرف")]
        [Display(Name = "رسالة إضافية")]
        public string? Message { get; set; }

        // Closing message
        [StringLength(200, ErrorMessage = "الخاتمة يجب أن لا تتجاوز 200 حرف")]
        [Display(Name = "رسالة الختام")]
        public string? ClosingMessage { get; set; } = "نتشرف بحضوركم ومشاركتنا فرحتنا";

        [Display(Name = "رابط الموسيقى (يوتيوب)")]
        public string? YouTubeUrl { get; set; }

        // Theme and event type tracking
        public string ThemeKey { get; set; } = string.Empty;
        public EventType EventType { get; set; }

        // Generated slug for the invitation URL
        public string Slug { get; set; } = string.Empty;

        // Computed properties
        public DateTime EventDateTime => Date.Add(Time);
        
        public string FormattedDate => Date.ToString("yyyy/MM/dd");
        
        public string FormattedTime => DateTime.Today.Add(Time).ToString("hh:mm tt");
        
        public string FormattedDateArabic
        {
            get
            {
                var arabicMonths = new[] { "يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو", 
                    "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر" };
                var arabicDays = new[] { "الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت" };
                
                return $"{arabicDays[(int)Date.DayOfWeek]}، {Date.Day} {arabicMonths[Date.Month - 1]} {Date.Year}";
            }
        }

        // Extract YouTube Video ID from URL
        public string? YouTubeVideoId
        {
            get
            {
                if (string.IsNullOrEmpty(YouTubeUrl))
                    return null;

                var patterns = new[]
                {
                    @"(?:youtube\.com\/watch\?v=|youtu\.be\/|youtube\.com\/embed\/|youtube\.com\/v\/)([a-zA-Z0-9_-]{11})",
                    @"^([a-zA-Z0-9_-]{11})$"
                };

                foreach (var pattern in patterns)
                {
                    var match = Regex.Match(YouTubeUrl, pattern);
                    if (match.Success)
                        return match.Groups[1].Value;
                }

                return null;
            }
        }

        public string GenerateSlug()
        {
            var baseSlug = $"{BrideName}-{GroomName}".Replace(" ", "-").ToLowerInvariant();
            var timestamp = DateTime.Now.Ticks.ToString().Substring(10, 6);
            return $"{baseSlug}-{timestamp}";
        }
    }
}
