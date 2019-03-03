using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Owleet.Models;

namespace Owleet.Filters
{
    public class ValidateTestAuthorAttribute : IActionFilter
    {
        private readonly ApplicationDbContext _context;
        private string userId;

        public ValidateTestAuthorAttribute(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = (Guid)context.ActionArguments["id"];
            var entity = _context.Tests.SingleOrDefault(x => x.Id.Equals(id));
            
            if (entity?.UserId != userId)
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
