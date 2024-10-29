using System.ComponentModel.DataAnnotations.Schema;

namespace TodoWebAPI.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Source { get; set; } = string.Empty;

    }
}
