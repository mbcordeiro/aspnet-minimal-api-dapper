using System.ComponentModel.DataAnnotations.Schema;

namespace TaskApi.Data
{
    [Table("Task")]
    public record Task(int Id, string Activity, string Status);
}
