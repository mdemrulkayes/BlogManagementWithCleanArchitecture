using CleanArchitecture.BlogManagement.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.BlogManagement.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
}
