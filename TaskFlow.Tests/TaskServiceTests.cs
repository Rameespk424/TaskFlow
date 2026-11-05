using Moq;
using TaskFlow.Application.Services;
using TaskFlow.Domain.Repositories;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain;

namespace TaskFlow.Tests;

public class TaskServiceTests
{
    [Fact]
    public async Task CreateTaskAsync_ProjectDoesNotExist_ThrowsAndDoesNotSave()
    {
        var projectRepo = new Mock<IProjectRepository>();
        var taskRepo = new Mock<ITaskRepository>();

        projectRepo.Setup(r => r.ExistsAsync(9999)).ReturnsAsync(false);

        var service = new TaskService(taskRepo.Object, projectRepo.Object);

        var request = new CreateTaskRequest { Title = "Test Task",
            Description = "Description",
            ProjectId = 9999,
            Priority = 1,
            AssigneeId = 1
            };

       var ex =  await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTaskAsync(request));
        Assert.Contains("does not exist", ex.Message);

        taskRepo.Verify(r => r.AddAsync(It.IsAny<TaskItem>()),Times.Never);

    }

    [Fact]
    public async Task CreateTaskAsync_ProjectExists_SavesAndReturnsId()
    {
        var projectRepo = new Mock<IProjectRepository>();
        var taskRepo = new Mock<ITaskRepository>();

        projectRepo.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
        taskRepo.Setup(r => r.AddAsync(It.IsAny<TaskItem>())).ReturnsAsync(42);

        var service = new TaskService(taskRepo.Object, projectRepo.Object);

        var request = new CreateTaskRequest
        {
            Title = "Test Task",
            Description = "Description",
            ProjectId = 1,
            Priority = 1,
            AssigneeId = 1
        };

        var newId = await service.CreateTaskAsync(request);

        Assert.Equal(42, newId);

        taskRepo.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);

    }
}

