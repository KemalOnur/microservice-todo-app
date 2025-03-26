#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using CORE.APP.Features;
using APP.TODO.Features.Todos;

//Generated from Custom Template.
namespace API.TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ILogger<TodosController> _logger;
        private readonly IMediator _mediator;

        public TodosController(ILogger<TodosController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/Todos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _mediator.Send(new TodoQueryRequest());
                var list = await response.ToListAsync();
                if (list.Any())
                    return Ok(list);
                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError("TodosGet Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during TodosGet.")); 
            }
        }

        // GET: api/Todos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _mediator.Send(new TodoQueryRequest());
                var item = await response.SingleOrDefaultAsync(r => r.Id == id);
                if (item is not null)
                    return Ok(item);
                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError("TodosGetById Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during TodosGetById.")); 
            }
        }

        // POST: api/Todos
        [HttpPost]
        public async Task<IActionResult> Post(TodoCreateRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(request);
                    if (response.IsSuccessful)
                    {
                        //return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
                        return Ok(response);
                    }
                    ModelState.AddModelError("TodosPost", response.Message);
                }
                return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
            }
            catch (Exception exception)
            {
                _logger.LogError("TodosPost Exception: " + exception.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during TodosPost."));
            }
        }

        //      // PUT: api/Todos
        //      [HttpPut]
        //      public async Task<IActionResult> Put(TodoUpdateRequest request)
        //      {
        //          try
        //          {
        //              if (ModelState.IsValid)
        //              {
        //                  var response = await _mediator.Send(request);
        //                  if (response.IsSuccessful)
        //                  {
        //                      //return NoContent();
        //                      return Ok(response);
        //                  }
        //                  ModelState.AddModelError("TodosPut", response.Message);
        //              }
        //              return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
        //          }
        //          catch (Exception exception)
        //          {
        //              _logger.LogError("TodosPut Exception: " + exception.Message);
        //              return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during TodosPut.")); 
        //          }
        //      }

        //      // DELETE: api/Todos/5
        //      [HttpDelete("{id}")]
        //      public async Task<IActionResult> Delete(int id)
        //      {
        //          try
        //          {
        //              var response = await _mediator.Send(new TodoDeleteRequest() { Id = id });
        //              if (response.IsSuccessful)
        //              {
        //                  //return NoContent();
        //                  return Ok(response);
        //              }
        //              ModelState.AddModelError("TodosDelete", response.Message);
        //              return BadRequest(new CommandResponse(false, string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))));
        //          }
        //          catch (Exception exception)
        //          {
        //              _logger.LogError("TodosDelete Exception: " + exception.Message);
        //              return StatusCode(StatusCodes.Status500InternalServerError, new CommandResponse(false, "An exception occured during TodosDelete.")); 
        //          }
        //      }
    }
}
