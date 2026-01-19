# 💒 Wedding Invitation App

A modern, elegant Arabic-first wedding invitation web application built with ASP.NET Core MVC. Create beautiful digital wedding invitations with custom themes, background music, and shareable links.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

## ✨ Features

- 🎨 **Beautiful Themes** - Multiple elegant wedding themes with gold accents and modern aesthetics
- 🌙 **Arabic-First Design** - RTL layout with premium Arabic typography (Aref Ruqaa Ink, Noto Naskh Arabic)
- 🎵 **Background Music** - YouTube audio integration with autoplay and toggle controls
- 📱 **Fully Responsive** - Looks stunning on all devices
- ⏰ **Live Countdown** - Dynamic countdown timer to the wedding date
- 🔗 **Shareable Links** - One-click copy invitation URL
- 🔐 **Passwordless Management** - Edit/delete invitations using secure token links
- ✨ **Modern Animations** - Smooth CSS animations and transitions
- 📍 **Google Maps Integration** - Direct link to venue location

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core 10.0 MVC
- **Database:** SQL Server LocalDB with Entity Framework Core
- **Frontend:** Razor Views, Modern CSS3, Vanilla JavaScript
- **Fonts:** Google Fonts (Aref Ruqaa Ink, Noto Naskh Arabic, Amiri)

## 📋 Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (included with Visual Studio)

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone git@github.com:7ussein97/Event.git
cd Event
```

### 2. Navigate to project folder

```bash
cd Event
```

### 3. Apply database migrations

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

### 5. Open in browser

Navigate to `http://localhost:5287`

## 📁 Project Structure

```
Event/
├── Controllers/
│   ├── HomeController.cs      # Landing page
│   ├── CreateController.cs    # Invitation creation flow
│   ├── InviteController.cs    # Public invitation display
│   ├── ManageController.cs    # Edit/delete with token auth
│   └── ExportController.cs    # Export functionality
├── Models/
│   ├── EventData.cs           # Database entity
│   ├── EventDetails.cs        # View model
│   ├── EventType.cs           # Event type enum
│   └── Theme.cs               # Theme model
├── Services/
│   ├── ThemeService.cs        # Theme management
│   └── VideoExportService.cs  # Export service
├── Views/
│   ├── Home/                  # Landing page views
│   ├── Create/                # Creation wizard views
│   ├── Invite/                # Invitation display views
│   └── Manage/                # Management views
├── Data/
│   └── EventDbContext.cs      # EF Core context
└── wwwroot/
    ├── css/                   # Stylesheets
    └── js/                    # JavaScript
```

## 🎨 Available Themes

| Theme | Description |
|-------|-------------|
| 🌹 Royal Rose | Elegant rose gold with floral accents |
| 🌙 Midnight Gold | Deep navy with golden shimmer |
| 🌸 Blush Garden | Soft pink with botanical elements |
| ✨ Classic Ivory | Timeless cream and gold elegance |
| 🌊 Ocean Breeze | Serene turquoise with pearl accents |

## 📝 Invitation Template Structure

1. **Opening Prayer** - بسم الله الرحمن الرحيم
2. **Blessing** - اللهم بارك لهما...
3. **Invitation Text** - Personal invitation from the host
4. **Couple Names** - Bride & Groom names with elegant styling
5. **Event Details** - Date, time, and venue with map link
6. **Personal Message** - Optional custom message
7. **Countdown Timer** - Live countdown to the event
8. **Closing Message** - Heartfelt closing

## 🔐 Passwordless Authentication

When you create an invitation, you receive:
- **Public Link** - Share with guests
- **Private Edit Link** - Manage your invitation (edit/delete)

Keep your edit link safe - it's the only way to modify your invitation!

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License.

## 👨‍💻 Author

**Hussein Al Ghafri**

---

<p align="center">Made with ❤️ for beautiful weddings</p>
