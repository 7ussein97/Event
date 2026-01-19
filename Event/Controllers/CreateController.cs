using Event.Models;
using Event.Services;
using Event.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Event.Controllers
{
    public class CreateController : Controller
    {
        private readonly IThemeService _themeService;
        private readonly EventDbContext _context;

        public CreateController(IThemeService themeService, EventDbContext context)
        {
            _themeService = themeService;
            _context = context;
        }

        // Redirect /create directly to wedding theme selection
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Theme");
        }

        // Theme selection for weddings
        [HttpGet]
        public IActionResult Theme()
        {
            var themes = _themeService.GetThemesByEventType(EventType.Wedding);
            ViewBag.EventType = EventType.Wedding;
            return View(themes);
        }

        // Event Details Form
        [HttpGet]
        public IActionResult Details(string themeKey)
        {
            var theme = _themeService.GetThemeByKey(themeKey);
            if (theme == null)
            {
                return RedirectToAction("Theme");
            }

            var model = new EventDetails
            {
                ThemeKey = themeKey,
                EventType = EventType.Wedding,
                Date = DateTime.Today.AddDays(30),
                Time = new TimeSpan(18, 0, 0)
            };

            ViewBag.Theme = theme;
            return View(model);
        }

        // Process form submission and generate invitation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(EventDetails model)
        {
            var theme = _themeService.GetThemeByKey(model.ThemeKey);
            if (theme == null)
            {
                return RedirectToAction("Theme");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Theme = theme;
                return View(model);
            }

            // Generate unique slug
            model.Slug = model.GenerateSlug();

            // Save to database
            var eventData = EventData.FromEventDetails(model);
            _context.Events.Add(eventData);
            await _context.SaveChangesAsync();

            // Redirect to success page with edit token
            return RedirectToAction("Success", "Manage", new { slug = eventData.Slug, token = eventData.EditToken });
        }

        // API endpoint for live preview
        [HttpPost]
        public IActionResult Preview([FromBody] EventDetails model)
        {
            var theme = _themeService.GetThemeByKey(model.ThemeKey);
            if (theme == null)
            {
                return BadRequest("Theme not found");
            }

            return Json(new
            {
                success = true,
                data = new
                {
                    title = model.Title,
                    hostName = model.HostName,
                    guestName = model.GuestName,
                    formattedDate = model.FormattedDateArabic,
                    formattedTime = model.FormattedTime,
                    location = model.Location,
                    message = model.Message,
                    googleMapsUrl = model.GoogleMapsUrl
                }
            });
        }
    }
}
