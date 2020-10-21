using System.Security.Claims;
using System.Security.Principal;

namespace APPAPI.Extensions {
    public static class IdentityExtensions {
        public static int GetUserId (this IIdentity identity) {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst (CustomClaimTypes.UserId);

            if (claim == null)
                return 0;

            return int.Parse (claim.Value);
        }

        public static string GetUserName (this IIdentity identity) {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst (CustomClaimTypes.Username);

            if (claim == null)
                return string.Empty;

            return claim.Value;
        }

        public static string GetName (this IIdentity identity) {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst (ClaimTypes.Name);

            return claim?.Value ?? string.Empty;
        }

        public static int GetRoleId (this IIdentity identity) {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst (ClaimTypes.Role);

            if (claim == null)
                return 0;

            return int.Parse (claim.Value);
        }
    }

    public static class CustomClaimTypes {
        public const string UserId = "id";
        public const string Username = "username";
        public const string Name = "name";

    }
}