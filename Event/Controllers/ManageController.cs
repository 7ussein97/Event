using Event.Models;
using Event.Services;
using Event.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event.Controllers
{
    public class ManageController : Controller
    {
        private readonly IThemeService _themeService;
        private readonly EventDbContext _context;

        public ManageController(IThemeService themeService, EventDbContext context)
        {
            _themeService = themeService;
            _context = context;
        }

        // Success page after creating invitation - shows edit link
        [HttpGet]
        public async Task<IActionResult> Success(string slug, string token)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug && e.EditToken == token);

            if (eventData == null)
            {
                return NotFound();
            }

            var theme = _themeService.GetThemeByKey(eventData.ThemeKey);
            ViewBag.Theme = theme;
            ViewBag.EditToken = token;
            
            return View(eventData);
        }

        // Edit page - requires token
        [HttpGet]
        public async Task<IActionResult> Edit(string slug, string token)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug && e.EditToken == token);

            if (eventData == null)
            {
                return View("InvalidToken");
            }

            var theme = _themeService.GetThemeByKey(eventData.ThemeKey);
            if (theme == null)
            {
                return NotFound();
            }

            var eventDetails = eventData.ToEventDetails();
            ViewBag.Theme = theme;
            ViewBag.EditToken = token;
            ViewBag.Themes = _themeService.GetThemesByEventType(EventType.Wedding);
            
            return View(eventDetails);
        }

        // Process edit submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string slug, string token, EventDetails model)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug && e.EditToken == token);

            if (eventData == null)
            {
                return View("InvalidToken");
            }

            var theme = _themeService.GetThemeByKey(model.ThemeKey);
            if (theme == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Theme = theme;
                ViewBag.EditToken = token;
                ViewBag.Themes = _themeService.GetThemesByEventType(EventType.Wedding);
                return View(model);
            }

            // Update the event
            eventData.UpdateFromEventDetails(model);
            await _context.SaveChangesAsync();

            // Redirect to success page
            return RedirectToAction("Success", new { slug = eventData.Slug, token = token });
        }

        // Delete confirmation page
        [HttpGet]
        public async Task<IActionResult> Delete(string slug, string token)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug && e.EditToken == token);

            if (eventData == null)
            {
                return View("InvalidToken");
            }

            var theme = _themeService.GetThemeByKey(eventData.ThemeKey);
            ViewBag.Theme = theme;
            ViewBag.EditToken = token;
            
            return View(eventData);
        }

        // Process deletion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string slug, string token)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug && e.EditToken == token);

            if (eventData == null)
            {
                return View("InvalidToken");
            }

            _context.Events.Remove(eventData);
            await _context.SaveChangesAsync();

            return View("Deleted");
        }
    }
}
