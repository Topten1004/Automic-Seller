using System.Security.Claims;

namespace Sales.AtomicSeller.Providers
{
    public class IdentityProvider : IIdentityProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public string Username => httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        public string UserId => httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public IdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
