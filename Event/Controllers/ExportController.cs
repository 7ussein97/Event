using Event.Data;
using Event.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event.Controllers
{
    public class ExportController : Controller
    {
        private readonly EventDbContext _context;
        private readonly IVideoExportService _videoExportService;
        private readonly ILogger<ExportController> _logger;

        public ExportController(
            EventDbContext context,
            IVideoExportService videoExportService,
            ILogger<ExportController> logger)
        {
            _context = context;
            _videoExportService = videoExportService;
            _logger = logger;
        }

        /// <summary>
        /// Shows the export options page
        /// </summary>
        [HttpGet("/export/{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug);

            if (eventData == null)
            {
                return NotFound();
            }

            ViewBag.Slug = slug;
            ViewBag.BrideName = eventData.BrideName;
            ViewBag.GroomName = eventData.GroomName;
            ViewBag.ThemeKey = eventData.ThemeKey;
            
            return View();
        }

        /// <summary>
        /// Exports the invitation as a screenshot image
        /// </summary>
        [HttpGet("/export/{slug}/image")]
        public async Task<IActionResult> ExportImage(string slug)
        {
            try
            {
                var eventData = await _context.Events
                    .FirstOrDefaultAsync(e => e.Slug == slug);

                if (eventData == null)
                {
                    return NotFound();
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var invitationUrl = $"{baseUrl}/invite/{eventData.ThemeKey}/{slug}";

                _logger.LogInformation("Exporting image for invitation: {Slug}", slug);

                var imageData = await _videoExportService.ExportInvitationAsGifAsync(invitationUrl, 1);

                var fileName = $"invitation_{eventData.BrideName}_{eventData.GroomName}.png";
                
                return File(imageData, "image/png", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to export image for slug: {Slug}", slug);
                TempData["Error"] = "حدث خطأ أثناء تصدير الصورة. حاول مرة أخرى.";
                return RedirectToAction("Index", new { slug });
            }
        }

        /// <summary>
        /// Exports the invitation as video frames (screenshot sequence)
        /// </summary>
        [HttpGet("/export/{slug}/video")]
        public async Task<IActionResult> ExportVideo(string slug, int duration = 10)
        {
            try
            {
                var eventData = await _context.Events
                    .FirstOrDefaultAsync(e => e.Slug == slug);

                if (eventData == null)
                {
                    return NotFound();
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var invitationUrl = $"{baseUrl}/invite/{eventData.ThemeKey}/{slug}";

                _logger.LogInformation("Exporting video for invitation: {Slug}, Duration: {Duration}s", slug, duration);

                var videoData = await _videoExportService.ExportInvitationAsVideoAsync(invitationUrl, duration);

                var fileName = $"invitation_{eventData.BrideName}_{eventData.GroomName}.png";
                
                // For now returning as image until FFmpeg is integrated
                return File(videoData, "image/png", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to export video for slug: {Slug}", slug);
                TempData["Error"] = "حدث خطأ أثناء تصدير الفيديو. حاول مرة أخرى.";
                return RedirectToAction("Index", new { slug });
            }
        }

        /// <summary>
        /// API endpoint to check export status (for async exports)
        /// </summary>
        [HttpGet("/api/export/{slug}/status")]
        public async Task<IActionResult> GetExportStatus(string slug)
        {
            var eventData = await _context.Events
                .FirstOrDefaultAsync(e => e.Slug == slug);

            if (eventData == null)
            {
                return NotFound(new { error = "Invitation not found" });
            }

            return Ok(new
            {
                slug = slug,
                ready = true,
                message = "Export is ready"
            });
        }
    }
}
