using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoView.Domain.Images.Queries;

namespace PhotoView.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ImagesController(IMediator mediator) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> Get(int? page, int limit)
	{
		var result = await mediator.Send(new GetImages(page, limit));
		return Ok(result);
	}
}