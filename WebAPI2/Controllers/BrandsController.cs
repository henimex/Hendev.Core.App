using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetListWithDeleted;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        {
            CreatedBrandResponse response = await Mediator.Send(createBrandCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListBrandQuery getListBrandQuery = new() {PageRequest = pageRequest};
            GetListResponse<GetListBrandListItemDto> response = await Mediator.Send(getListBrandQuery);
            return Ok(response);
        }

        [HttpGet("With Deleted")]
        public async Task<IActionResult> GetListWithDeleted([FromQuery] PageRequest pageRequest)
        {
            GetListBrandAllQuery getListBrandAllQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListBrandAllItemDto> response = await Mediator.Send(getListBrandAllQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdBrandQuery getByIdBrandQuery = new() {Id = id};
            GetByIdBrandResponse response = await Mediator.Send(getByIdBrandQuery);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateCommand)
        {
            UpdatedBrandResponse response = await Mediator.Send(updateCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeletedBrandResponse response = await Mediator.Send(new DeleteBrandCommand {Id = id});
            return Ok(response);
        }
    }
}