using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentCourse.Models;

namespace StudentCourse.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(
            ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors =
                    context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response =
                    new CommonResponse<List<string>>
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Validation Failed",
                        Data = errors
                    };

                context.Result =
                    new BadRequestObjectResult(response);
                
            }
        }

        public void OnActionExecuted(
            ActionExecutedContext context)
        {
        }
    }
}