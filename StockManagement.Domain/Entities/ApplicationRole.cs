using Microsoft.AspNetCore.Identity;

namespace StockManagement.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
} 