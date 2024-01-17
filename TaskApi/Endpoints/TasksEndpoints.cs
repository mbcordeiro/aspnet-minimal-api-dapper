using Dapper.Contrib.Extensions;
using static TaskApi.Data.TaskContext;

namespace TaskApi.Endpoints
{
    public static class TasksEndpoints
    {
        public static void MapTasksEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => $"Welcome Tasks API {DateTime.Now}");

            app.MapGet("/tasks", async (GetConnection connectionGetter) =>
            {
                using var con = await connectionGetter();
                var tasks = con.GetAll<Task>().ToList();
                if (tasks is null)
                    return Results.NotFound();
                return Results.Ok(tasks);
            });

            app.MapGet("/tasks/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                return con.Get<Task>(id) is Task task ? Results.Ok(task) : Results.NotFound();
            });

            app.MapPost("/tasks", async (GetConnection connectionGetter, Task task) =>
            {
                using var con = await connectionGetter();
                var id = con.Insert(task);
                return Results.Created($"/task/{id}", task);
            });

            app.MapPut("/tasks", async (GetConnection connectionGetter, Task task) =>
            {
                using var con = await connectionGetter();
                var id = con.Update(task);
                return Results.Ok();
            });

            app.MapDelete("/tasks/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                var deleted = con.Get<Task>(id);
                if (deleted is null)
                    return Results.NotFound();
                con.Delete(deleted);
                return Results.Ok(deleted);
            });
        }
    }
}
