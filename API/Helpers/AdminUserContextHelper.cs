﻿using System.Security.Claims;

namespace API.Helpers
{
    public class AdminUserContextHelper
    {
        public static int GetAdminId(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("Admin ID bulunamadı."));
        }

        public static string GetAdminNumber(ClaimsPrincipal user)
        {
            return user.FindFirst("UserNumber")?.Value ?? throw new UnauthorizedAccessException("Admin numarası bulunamadı.");
        }

        public static Guid GetAdminGuid(ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst("UserGuid")?.Value ?? throw new UnauthorizedAccessException("Admin GUID bulunamadı."));
        }

        public static bool IsAdminActive(ClaimsPrincipal user)
        {
            return bool.Parse(user.FindFirst("IsActive")?.Value ?? throw new UnauthorizedAccessException("Admin aktiflik durumu bulunamadı."));
        }
    }
}
