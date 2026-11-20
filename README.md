DAMAS BARBERSHOP — ASP.NET Core Site
=====================================

STACK
- ASP.NET Core 8 Razor Pages
- EF Core + SQLite (bookings persisted to damas.db, auto-created on first run)
- No JS framework — plain JS for the 4-step booking flow (wwwroot/js/booking.js)

RUN IT
1. Install the .NET 8 SDK: https://dotnet.microsoft.com/download
2. From this folder: `dotnet run`
3. Open the URL printed in the console (e.g. https://localhost:5001)

PAGES
- /            Home
- /Services    Services & Pricing
- /Barbers     Barbers / Team
- /Gallery     Gallery
- /BookNow     4-step booking flow (Service → Barber → Date/Time → Confirm)
- /Admin/Bookings   Plain list of all submitted bookings (no auth yet — see below)

EDIT CONTENT
- Shop info (phone, address, hours, socials): appsettings.json -> "ShopInfo"
- Services & pricing: Pages/Services.cshtml (menu list) and Pages/BookNow.cshtml.cs (Services array — keep both in sync)
- Barbers: Pages/Barbers.cshtml and Pages/BookNow.cshtml.cs (Barbers array)
- Homepage hero/copy: Pages/Index.cshtml
- Real photos: drop files into wwwroot/img/ and reference with /img/filename.jpg (your uploaded shop photo is already at wwwroot/img/shop-interior.jpg)

HOW BOOKING WORKS
BookNow.cshtml.cs's OnPost saves each submission as a Booking row via EF Core/SQLite — no external plugin needed. View them at /Admin/Bookings.

STILL TO WIRE UP BEFORE GOING LIVE
- Admin auth: /Admin/Bookings is currently open to anyone who knows the URL. Add ASP.NET Core Identity (or restrict by IP/basic auth) before deploying.
- Email notifications: OnPost has a comment marking where to call an email provider (SendGrid, SMTP via MailKit, etc.) to notify the shop + customer on each booking.
- Real-time availability: like the original, this stores requests — it doesn't block double-booked slots. Add a query against existing Bookings for the same barber/date/time if you want that check.

DEPLOY
Any standard ASP.NET Core host works: Azure App Service, IIS, or a Linux VM behind nginx (`dotnet publish` then run the published DLL with `dotnet DamasBarbershop.dll`).


COPYRIGHT
---------
Copyright (c) 2026 Abdullah Sharba. All rights reserved.
See LICENSE. No use, copying, or distribution without written permission.
