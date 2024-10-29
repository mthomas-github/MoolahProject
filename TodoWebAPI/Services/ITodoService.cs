using TodoWebAPI.Models;

namespace TodoWebAPI.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetTodosAsync(string search = "");
        Task<Todo> AddTodoAsync(Todo todo);
        Task<Todo> UpdateTodoAsync(Todo todo);
        Task<bool> DeleteTodoAsync(int id);

    }
}
