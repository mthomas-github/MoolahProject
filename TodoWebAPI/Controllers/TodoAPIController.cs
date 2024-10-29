using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;
using TodoWebAPI.Services;

namespace TodoWebAPI.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoAPIController : ControllerBase
    {
        private readonly TodoProviderFactory _providerFactory;

        public TodoAPIController(TodoProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        private ITodoService GetProvider(string providerName) => _providerFactory.GetProvider(providerName);

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] string provider, [FromQuery] string search = "")
        {
            var todos = await GetProvider(provider).GetTodosAsync(search);
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromQuery] string provider, Todo todo)
        {
            if (todo.Id == Guid.Empty)
            {
                todo.Id = Guid.NewGuid();
            }

            var newTodo = await GetProvider(provider).AddTodoAsync(todo);
            return Ok(newTodo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromQuery] string provider, Todo todo)
        {
            var updatedTodo = await GetProvider(provider).UpdateTodoAsync(todo);
            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromQuery] string provider, int id)
        {
            var success = await GetProvider(provider).DeleteTodoAsync(id);
            return Ok(success);
        }
    }
}
