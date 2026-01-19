/**
 * Event Invitation Application
 * Main JavaScript file for interactive features
 */

(function () {
    'use strict';

    // ==========================================================================
    // Utility Functions
    // ==========================================================================

    /**
     * Debounce function to limit execution rate
     */
    function debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }

    /**
     * Format number with leading zero
     */
    function padZero(num) {
        return num.toString().padStart(2, '0');
    }

    /**
     * Arabic month names
     */
    const arabicMonths = [
        'يناير', 'فبراير', 'مارس', 'أبريل', 'مايو', 'يونيو',
        'يوليو', 'أغسطس', 'سبتمبر', 'أكتوبر', 'نوفمبر', 'ديسمبر'
    ];

    /**
     * Arabic day names
     */
    const arabicDays = [
        'الأحد', 'الإثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت'
    ];

    /**
     * Format date to Arabic
     */
    function formatArabicDate(dateStr) {
        if (!dateStr) return 'التاريخ';
        const date = new Date(dateStr);
        if (isNaN(date)) return 'التاريخ';
        return `${arabicDays[date.getDay()]}، ${date.getDate()} ${arabicMonths[date.getMonth()]} ${date.getFullYear()}`;
    }

    /**
     * Format time to Arabic AM/PM
     */
    function formatTime(timeStr) {
        if (!timeStr) return 'الوقت';
        const [hours, minutes] = timeStr.split(':');
        const hour = parseInt(hours);
        const ampm = hour >= 12 ? 'مساءً' : 'صباحاً';
        const hour12 = hour % 12 || 12;
        return `${hour12}:${minutes} ${ampm}`;
    }

    // ==========================================================================
    // Page Initialization
    // ==========================================================================

    document.addEventListener('DOMContentLoaded', function () {
        initAnimations();
        initEventTypeCards();
        initThemeCards();
    });

    // ==========================================================================
    // Animation Observer
    // ==========================================================================

    function initAnimations() {
        const animatedElements = document.querySelectorAll('.fade-in, .fade-up, .slide-up, .bounce-in');

        if ('IntersectionObserver' in window) {
            const observer = new IntersectionObserver((entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('animated');
                        observer.unobserve(entry.target);
                    }
                });
            }, {
                threshold: 0.1,
                rootMargin: '0px 0px -50px 0px'
            });

            animatedElements.forEach(el => observer.observe(el));
        } else {
            // Fallback for older browsers
            animatedElements.forEach(el => el.classList.add('animated'));
        }
    }

    // ==========================================================================
    // Event Type Cards Hover Effects
    // ==========================================================================

    function initEventTypeCards() {
        const cards = document.querySelectorAll('.event-type-card');

        cards.forEach(card => {
            card.addEventListener('mouseenter', function () {
                this.querySelector('.card-icon')?.classList.add('pulse');
            });

            card.addEventListener('mouseleave', function () {
                this.querySelector('.card-icon')?.classList.remove('pulse');
            });
        });
    }

    // ==========================================================================
    // Theme Cards Preview
    // ==========================================================================

    function initThemeCards() {
        const themeCards = document.querySelectorAll('.theme-card');

        themeCards.forEach(card => {
            card.addEventListener('mouseenter', function () {
                const preview = this.querySelector('.theme-preview');
                if (preview) {
                    preview.style.transform = 'scale(1.02)';
                }
            });

            card.addEventListener('mouseleave', function () {
                const preview = this.querySelector('.theme-preview');
                if (preview) {
                    preview.style.transform = 'scale(1)';
                }
            });
        });
    }

    // ==========================================================================
    // Countdown Timer Class
    // ==========================================================================

    class CountdownTimer {
        constructor(targetDate, elements) {
            this.targetDate = targetDate;
            this.elements = elements;
            this.intervalId = null;
        }

        start() {
            this.update();
            this.intervalId = setInterval(() => this.update(), 1000);
        }

        stop() {
            if (this.intervalId) {
                clearInterval(this.intervalId);
                this.intervalId = null;
            }
        }

        update() {
            const now = new Date();
            const diff = this.targetDate - now;

            if (diff <= 0) {
                this.elements.days.textContent = '00';
                this.elements.hours.textContent = '00';
                this.elements.minutes.textContent = '00';
                this.elements.seconds.textContent = '00';
                this.stop();
                return;
            }

            const days = Math.floor(diff / (1000 * 60 * 60 * 24));
            const hours = Math.floor((diff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
            const seconds = Math.floor((diff % (1000 * 60)) / 1000);

            this.elements.days.textContent = padZero(days);
            this.elements.hours.textContent = padZero(hours);
            this.elements.minutes.textContent = padZero(minutes);
            this.elements.seconds.textContent = padZero(seconds);
        }
    }

    // ==========================================================================
    // RSVP Handler Class
    // ==========================================================================

    class RSVPHandler {
        constructor(options) {
            this.slug = options.slug;
            this.buttons = options.buttons;
            this.feedback = options.feedback;
            this.endpoint = options.endpoint || '/Invite/Rsvp';

            this.messages = {
                'attending': 'شكراً لتأكيد حضورك! نراك قريباً ✨',
                'maybe': 'شكراً! سنكون سعداء برؤيتك إذا استطعت الحضور 🌟',
                'not-attending': 'نأسف لعدم تمكنك من الحضور. نتمنى لك كل التوفيق 💫'
            };

            this.init();
        }

        init() {
            this.buttons.forEach(btn => {
                btn.addEventListener('click', () => this.handleResponse(btn.dataset.response, btn));
            });
        }

        handleResponse(response, button) {
            // Update UI
            this.buttons.forEach(b => b.classList.remove('active'));
            button.classList.add('active');

            // Show feedback
            if (this.feedback) {
                this.feedback.textContent = this.messages[response] || 'شكراً لردك';
                this.feedback.classList.add('show');
            }

            // Log to console
            console.log('RSVP Response:', {
                slug: this.slug,
                response: response,
                timestamp: new Date().toISOString()
            });

            // Send to server (non-blocking)
            this.sendToServer(response);
        }

        async sendToServer(response) {
            try {
                await fetch(this.endpoint, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        slug: this.slug,
                        response: response
                    })
                });
            } catch (error) {
                console.log('RSVP logged locally (server unavailable)');
            }
        }
    }

    // ==========================================================================
    // Calendar Integration
    // ==========================================================================

    class CalendarIntegration {
        constructor(eventData) {
            this.eventData = eventData;
        }

        generateGoogleCalendarUrl() {
            const { title, location, description, startDate, startTime, endTime } = this.eventData;
            const encodedTitle = encodeURIComponent(title);
            const encodedLocation = encodeURIComponent(location);
            const encodedDetails = encodeURIComponent(description || '');

            return `https://calendar.google.com/calendar/render?action=TEMPLATE&text=${encodedTitle}&dates=${startDate}T${startTime}/${startDate}T${endTime}&location=${encodedLocation}&details=${encodedDetails}`;
        }

        generateICSContent() {
            const { title, location, description, startDate, startTime, endTime } = this.eventData;
            const uid = `${Date.now()}@event.local`;
            const now = new Date().toISOString().replace(/[-:]/g, '').split('.')[0] + 'Z';

            return `BEGIN:VCALENDAR
VERSION:2.0
PRODID:-//Event Invitation//AR
BEGIN:VEVENT
UID:${uid}
DTSTAMP:${now}
DTSTART:${startDate}T${startTime}
DTEND:${startDate}T${endTime}
SUMMARY:${title}
DESCRIPTION:${description || 'You are cordially invited'}
LOCATION:${location}
END:VEVENT
END:VCALENDAR`;
        }

        downloadICS() {
            const content = this.generateICSContent();
            const blob = new Blob([content], { type: 'text/calendar;charset=utf-8' });
            const url = URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = url;
            link.download = 'event.ics';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            URL.revokeObjectURL(url);
        }

        openGoogleCalendar() {
            window.open(this.generateGoogleCalendarUrl(), '_blank');
        }
    }

    // ==========================================================================
    // Share Functionality
    // ==========================================================================

    class ShareManager {
        constructor(options) {
            this.title = options.title;
            this.text = options.text;
            this.url = options.url || window.location.href;
        }

        async share() {
            if (navigator.share) {
                try {
                    await navigator.share({
                        title: this.title,
                        text: this.text,
                        url: this.url
                    });
                    return true;
                } catch (error) {
                    if (error.name !== 'AbortError') {
                        console.error('Error sharing:', error);
                    }
                    return false;
                }
            } else {
                // Fallback: copy to clipboard
                return this.copyToClipboard();
            }
        }

        async copyToClipboard() {
            try {
                await navigator.clipboard.writeText(this.url);
                return true;
            } catch (error) {
                console.error('Error copying to clipboard:', error);
                return false;
            }
        }

        shareViaWhatsApp() {
            const url = `https://wa.me/?text=${encodeURIComponent(this.text + '\n' + this.url)}`;
            window.open(url, '_blank');
        }

        shareViaTelegram() {
            const url = `https://t.me/share/url?url=${encodeURIComponent(this.url)}&text=${encodeURIComponent(this.text)}`;
            window.open(url, '_blank');
        }

        shareViaTwitter() {
            const url = `https://twitter.com/intent/tweet?text=${encodeURIComponent(this.text)}&url=${encodeURIComponent(this.url)}`;
            window.open(url, '_blank');
        }
    }

    // ==========================================================================
    // Expose to Global Scope
    // ==========================================================================

    window.EventApp = {
        CountdownTimer,
        RSVPHandler,
        CalendarIntegration,
        ShareManager,
        utils: {
            debounce,
            padZero,
            formatArabicDate,
            formatTime,
            arabicMonths,
            arabicDays
        }
    };

})();
