using Event.Models;

namespace Event.Services
{
    public interface IThemeService
    {
        IEnumerable<Theme> GetAllThemes();
        IEnumerable<Theme> GetThemesByEventType(EventType eventType);
        Theme? GetThemeByKey(string key);
    }

    public class ThemeService : IThemeService
    {
        private readonly List<Theme> _themes;

        public ThemeService()
        {
            _themes = InitializeThemes();
        }

        private List<Theme> InitializeThemes()
        {
            return new List<Theme>
            {
                // Theme 1: Ethereal Bloom - Modern floating petals with dreamy atmosphere
                new Theme
                {
                    Key = "ethereal_bloom",
                    Name = "Ethereal Bloom",
                    NameArabic = "زهرة الأحلام",
                    EventType = EventType.Wedding,
                    Background = "wedding-ethereal.svg",
                    BackgroundColor = "#FFF0F5",
                    Font = "Amiri",
                    PrimaryColor = "#FFB5C5",
                    SecondaryColor = "#FFF5F8",
                    TextColor = "#4A3F4F",
                    AccentColor = "#FF69B4",
                    Animation = "floating-petals",
                    Layout = "centered",
                    PreviewImage = "preview-ethereal.svg",
                    Description = "تصميم حالم مع بتلات وردية عائمة وتأثيرات ضوئية ناعمة",
                    HasPattern = true,
                    PatternStyle = "floating-petals"
                },
                
                // Theme 2: Midnight Garden - Luxurious dark with animated gold stars
                new Theme
                {
                    Key = "midnight_garden",
                    Name = "Midnight Garden",
                    NameArabic = "حديقة منتصف الليل",
                    EventType = EventType.Wedding,
                    Background = "wedding-midnight.svg",
                    BackgroundColor = "#1a1a2e",
                    Font = "Cairo",
                    PrimaryColor = "#D4AF37",
                    SecondaryColor = "#16213e",
                    TextColor = "#f0e6d2",
                    AccentColor = "#e8c87c",
                    Animation = "starry-shimmer",
                    Layout = "centered",
                    PreviewImage = "preview-midnight.svg",
                    Description = "فخامة داكنة مع نجوم ذهبية متلألئة وتأثيرات وهج ساحرة",
                    HasPattern = true,
                    PatternStyle = "starry-shimmer"
                },
                
                // Theme 3: Aurora Dreams - Magical celestial waves
                new Theme
                {
                    Key = "aurora_dreams",
                    Name = "Aurora Dreams",
                    NameArabic = "أحلام الشفق",
                    EventType = EventType.Wedding,
                    Background = "wedding-aurora.svg",
                    BackgroundColor = "#1E1E3F",
                    Font = "Tajawal",
                    PrimaryColor = "#B19CD9",
                    SecondaryColor = "#2A2A4F",
                    TextColor = "#ffffff",
                    AccentColor = "#7EC8E3",
                    Animation = "aurora-wave",
                    Layout = "centered",
                    PreviewImage = "preview-aurora.svg",
                    Description = "سحر سماوي مستوحى من أضواء الشفق القطبي مع موجات متدفقة",
                    HasPattern = true,
                    PatternStyle = "aurora-gradient"
                },
                
                // Theme 4: Golden Hour - Warm radiant sunset
                new Theme
                {
                    Key = "golden_hour",
                    Name = "Golden Hour",
                    NameArabic = "الساعة الذهبية",
                    EventType = EventType.Wedding,
                    Background = "wedding-golden.svg",
                    BackgroundColor = "#FFF8E7",
                    Font = "Amiri",
                    PrimaryColor = "#FFD700",
                    SecondaryColor = "#FFFAED",
                    TextColor = "#5D4E37",
                    AccentColor = "#FF8C42",
                    Animation = "sunlight-rays",
                    Layout = "centered",
                    PreviewImage = "preview-golden.svg",
                    Description = "دفء الغروب مع أشعة ضوء ذهبية وجزيئات متوهجة",
                    HasPattern = true,
                    PatternStyle = "sunlight-rays"
                },
                
                // Theme 5: Rose Whisper - Soft pink with gentle flowing motion
                new Theme
                {
                    Key = "rose_whisper",
                    Name = "Rose Whisper",
                    NameArabic = "همسة الورد",
                    EventType = EventType.Wedding,
                    Background = "wedding-rose.svg",
                    BackgroundColor = "#FFE4E9",
                    Font = "Cairo",
                    PrimaryColor = "#FF6B9D",
                    SecondaryColor = "#FFF0F3",
                    TextColor = "#5F3853",
                    AccentColor = "#FFC2D1",
                    Animation = "gentle-flow",
                    Layout = "centered",
                    PreviewImage = "preview-rose.svg",
                    Description = "نعومة الورد مع حركة انسيابية هادئة وتأثيرات ضبابية",
                    HasPattern = true,
                    PatternStyle = "gentle-flow"
                },
                
                // Theme 6: Ocean Dreams - Flowing water with serene waves
                new Theme
                {
                    Key = "ocean_dreams",
                    Name = "Ocean Dreams",
                    NameArabic = "أحلام المحيط",
                    EventType = EventType.Wedding,
                    Background = "wedding-ocean.svg",
                    BackgroundColor = "#E8F4F8",
                    Font = "Tajawal",
                    PrimaryColor = "#74b9ff",
                    SecondaryColor = "#EBF5FB",
                    TextColor = "#2C3E50",
                    AccentColor = "#a29bfe",
                    Animation = "wave-flow",
                    Layout = "centered",
                    PreviewImage = "preview-ocean.svg",
                    Description = "هدوء المحيط مع أمواج متدفقة وفقاعات ضوئية عائمة",
                    HasPattern = true,
                    PatternStyle = "wave-flow"
                },
                
                // Theme 7: Crystal Elegance - Modern glassmorphism
                new Theme
                {
                    Key = "crystal_elegance",
                    Name = "Crystal Elegance",
                    NameArabic = "أناقة الكريستال",
                    EventType = EventType.Wedding,
                    Background = "wedding-crystal.svg",
                    BackgroundColor = "#f8f9fa",
                    Font = "Amiri",
                    PrimaryColor = "#c0c0c0",
                    SecondaryColor = "#ffffff",
                    TextColor = "#2d3436",
                    AccentColor = "#dfe6e9",
                    Animation = "glass-shimmer",
                    Layout = "centered",
                    PreviewImage = "preview-crystal.svg",
                    Description = "أناقة زجاجية حديثة مع تأثيرات بلورية وبريق متلألئ",
                    HasPattern = true,
                    PatternStyle = "glass-morph"
                },
                
                // Theme 8: Lavender Mist - Purple gradient with floating particles
                new Theme
                {
                    Key = "lavender_mist",
                    Name = "Lavender Mist",
                    NameArabic = "ضباب اللافندر",
                    EventType = EventType.Wedding,
                    Background = "wedding-lavender.svg",
                    BackgroundColor = "#E8D5F2",
                    Font = "Cairo",
                    PrimaryColor = "#9b59b6",
                    SecondaryColor = "#F4ECF7",
                    TextColor = "#4a235a",
                    AccentColor = "#d7bde2",
                    Animation = "particle-drift",
                    Layout = "centered",
                    PreviewImage = "preview-lavender.svg",
                    Description = "سحر اللافندر مع جزيئات ضبابية وفراشات متطايرة برقة",
                    HasPattern = true,
                    PatternStyle = "particle-drift"
                },
                
                // Theme 9: Enchanted Garden - Living green with firefly glow
                new Theme
                {
                    Key = "enchanted_garden",
                    Name = "Enchanted Garden",
                    NameArabic = "الحديقة المسحورة",
                    EventType = EventType.Wedding,
                    Background = "wedding-forest.svg",
                    BackgroundColor = "#1e3a20",
                    Font = "Tajawal",
                    PrimaryColor = "#7bed9f",
                    SecondaryColor = "#2C4A2E",
                    TextColor = "#ecf0f1",
                    AccentColor = "#a8e6cf",
                    Animation = "firefly-dance",
                    Layout = "centered",
                    PreviewImage = "preview-forest.svg",
                    Description = "سحر الغابة مع يراعات راقصة وأوراق متمايلة بنعومة",
                    HasPattern = true,
                    PatternStyle = "firefly-dance"
                },
                
                // Theme 10: Peach Sunset - Warm gradient with floating hearts
                new Theme
                {
                    Key = "peach_sunset",
                    Name = "Peach Sunset",
                    NameArabic = "غروب الخوخ",
                    EventType = EventType.Wedding,
                    Background = "wedding-peach.svg",
                    BackgroundColor = "#FFEEE8",
                    Font = "Amiri",
                    PrimaryColor = "#FFAB91",
                    SecondaryColor = "#FFF5F2",
                    TextColor = "#6D4C41",
                    AccentColor = "#FFE0B2",
                    Animation = "heart-float",
                    Layout = "centered",
                    PreviewImage = "preview-peach.svg",
                    Description = "دفء الغروب الخوخي مع قلوب عائمة وتدرجات ناعمة",
                    HasPattern = true,
                    PatternStyle = "heart-float"
                },
                
                // Theme 11: Midnight Bloom - Dark floral with glowing accents
                new Theme
                {
                    Key = "midnight_bloom",
                    Name = "Midnight Bloom",
                    NameArabic = "زهرة منتصف الليل",
                    EventType = EventType.Wedding,
                    Background = "wedding-midnight-bloom.svg",
                    BackgroundColor = "#2C1B3D",
                    Font = "Cairo",
                    PrimaryColor = "#E8B4F8",
                    SecondaryColor = "#3D2952",
                    TextColor = "#F5E6FF",
                    AccentColor = "#C084FC",
                    Animation = "bloom-glow",
                    Layout = "centered",
                    PreviewImage = "preview-midnight-bloom.svg",
                    Description = "أزهار ليلية متوهجة مع بريق أرجواني ساحر",
                    HasPattern = true,
                    PatternStyle = "bloom-glow"
                },
                
                // Theme 12: Champagne Sparkle - Luxe with bubbles and shimmer
                new Theme
                {
                    Key = "champagne_sparkle",
                    Name = "Champagne Sparkle",
                    NameArabic = "تألق الشمبانيا",
                    EventType = EventType.Wedding,
                    Background = "wedding-champagne.svg",
                    BackgroundColor = "#F5F0E8",
                    Font = "Tajawal",
                    PrimaryColor = "#D4C5A9",
                    SecondaryColor = "#FAF7F2",
                    TextColor = "#5D5346",
                    AccentColor = "#E8DCC8",
                    Animation = "bubble-rise",
                    Layout = "centered",
                    PreviewImage = "preview-champagne.svg",
                    Description = "فخامة الشمبانيا مع فقاعات صاعدة وبريق متلألئ",
                    HasPattern = true,
                    PatternStyle = "bubble-rise"
                }
            };
        }

        public IEnumerable<Theme> GetAllThemes()
        {
            return _themes;
        }

        public IEnumerable<Theme> GetThemesByEventType(EventType eventType)
        {
            return _themes.Where(t => t.EventType == eventType);
        }

        public Theme? GetThemeByKey(string key)
        {
            return _themes.FirstOrDefault(t => t.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
    }
}