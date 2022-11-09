using ProjectControlAPI.DataAccess;
using ProjectControlAPI.DataAccess.Entities;

namespace ProjectControlAPI.TestBusinessLogic
{
    internal static class AddTestDataExtension
    {
        public async static void AddTestData(this DataContext context)
        {
            var worker1 = new Worker()
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                Mail = "ivanov@gmail.com"
            };

            var worker2 = new Worker()
            {
                Id = 2,
                FirstName = "Пётр",
                LastName = "Петров",
                Patronymic = "Петрович",
                Mail = "petrov@mail.ru"
            };

            var worker3 = new Worker()
            {
                Id = 3,
                FirstName = "Александр",
                LastName = "Толстухин",
                Patronymic = "Викторович",
                Mail = "tolstuhin@yandex.ru"
            };

            await context.Workers.AddRangeAsync(worker1, worker2, worker3);

            var project1 = new Project()
            {
                Id = 1,
                Name = "Проект 1",
                CustomerCompanyName = "Компания 1",
                ExecutorCompanyName = "Компания 2",
                Priority = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(32)
            };

            var project2 = new Project()
            {
                Id = 2,
                Name = "Проект2",
                CustomerCompanyName = "Компания 3",
                ExecutorCompanyName = "Компания 2",
                Priority = 1,
                StartDate = DateTime.Now.AddMonths(-14),
                EndDate = DateTime.Now.AddMonths(10)
            };

            await context.Projects.AddRangeAsync(project1, project2);
            await context.WorkerProject.AddRangeAsync(
                new WorkerProject
                {
                    ProjectId = 1,
                    WorkerId = 3,
                    Position = Position.ProjectManager
                },
                new WorkerProject
                {
                    ProjectId = 1,
                    WorkerId = 1,
                    Position = Position.Employee
                },
                new WorkerProject
                {
                    ProjectId = 2,
                    WorkerId = 2,
                    Position = Position.Employee,
                },
                new WorkerProject
                {
                    ProjectId = 2,
                    WorkerId = 3,
                    Position = Position.ProjectManager,
                });

            var task1 = new TaskProject()
            {
                Id = 1,
                Name = "Фича1",
                Comment = "Добавить ....",
                Priority = 2,
                AuthorId = 3,
                ProjectId = 1,
                WorkerId = 1
            };

            var task2 = new TaskProject()
            {
                Id = 2,
                Name = "Пофиксить баг1",
                Comment = "Исправить ....",
                Priority = 1,
                AuthorId = 3,
                ProjectId = 2,
                WorkerId = 2
            };

            await context.TaskProject.AddRangeAsync(task1, task2);
            await context.SaveChangesAsync();
        }
    }
}
