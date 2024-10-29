using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Services
{
    public class SqlTodoService(SQLTodoDbContext context) : ITodoService
    {
        private readonly SQLTodoDbContext _context = context;

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Todo>> GetTodosAsync(string search = "")
        {
            if (string.IsNullOrEmpty(search))
            {
                // Return all todos if search is empty
                return await _context.Todos.ToListAsync();
            }
            else
            {
                // Filter todos based on the search term
                return await _context.Todos
                    .Where(t => t.Title.Contains(search))
                    .ToListAsync();
            }
        }

        public async Task<Todo> UpdateTodoAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}
