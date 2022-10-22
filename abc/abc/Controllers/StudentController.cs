using abc.core.Features.Student.Get;
using abc.core.Features.Student.Post;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace abc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public StudentController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("", Name ="GetStudent")]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator
                .CreateRequestClient<IGetStudentQuery>()
                .GetResponse<GetStudentResponse>(new
                {
                    Id = Guid.NewGuid(),
                });

            return Ok(response.Message.Id);
        }

        [HttpPost("", Name = "PostTaskUnit")]
        public async Task<IActionResult> Post([FromBody] PostStudentCommand student)
        {
            var response = await _mediator
                .CreateRequestClient<PostStudentCommand>()
                .GetResponse<IPostStudentResponse>(student);

            return Ok(response.Message.IsAdded);
        }
    }
}
