using Event.Models;
using Event.Services;
using Event.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event.Controllers
{
    public class InviteController : Controller
    {
        private readonly IThemeService _themeService;
        private readonly EventDbContext _context;

        public InviteController(IThemeService themeService, EventDbContext context)
        {
            _themeService = themeService;
            _context = context;
        }

        [HttpGet("invite/{theme}/{slug}")]
        public async Task<IActionResult> View(string theme, string slug)
        {
            var themeData = _themeService.GetThemeByKey(theme);
            if (themeData == null)
            {
                return NotFound("Theme not found");
            }

            // Load from database
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug);

            if (eventData == null)
            {
                return NotFound("Invitation not found");
            }

            // Check if event is expired
            if (eventData.IsExpired)
            {
                ViewBag.Theme = themeData;
                ViewBag.EventData = eventData;
                return View("Expired");
            }

            var eventDetails = eventData.ToEventDetails();
            ViewBag.Theme = themeData;
            return View("Index", eventDetails);
        }

        // Generate calendar event file
        [HttpGet("invite/{theme}/{slug}/calendar")]
        public async Task<IActionResult> Calendar(string theme, string slug)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug);

            if (eventData == null)
            {
                return NotFound();
            }

            var icsContent = GenerateIcsFile(eventData);
            return File(System.Text.Encoding.UTF8.GetBytes(icsContent), "text/calendar", "event.ics");
        }

        private string GenerateIcsFile(EventData eventData)
        {
            var startDate = eventData.EventDateTime;
            var endDate = startDate.AddHours(3); // Default 3-hour duration

            return $@"BEGIN:VCALENDAR
VERSION:2.0
PRODID:-//Event Invitation//AR
BEGIN:VEVENT
UID:{Guid.NewGuid()}@event.local
DTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}
DTSTART:{startDate:yyyyMMddTHHmmss}
DTEND:{endDate:yyyyMMddTHHmmss}
SUMMARY:{eventData.Title}
DESCRIPTION:{eventData.Message ?? "You are cordially invited"}
LOCATION:{eventData.Location}
END:VEVENT
END:VCALENDAR";
        }
    }
}
