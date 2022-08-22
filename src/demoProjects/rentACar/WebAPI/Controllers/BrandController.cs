using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommend createBrandCommend)
        {
            CreatedBrandDto result = await Mediator.Send(createBrandCommend);
            return Created("", result);
        }
    }
}
