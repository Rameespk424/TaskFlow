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
        // Arrange
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

        // Act + Assert
       var ex =  await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTaskAsync(request));
        Assert.Contains("does not exist", ex.Message);

        taskRepo.Verify(r => r.AddAsync(It.IsAny<TaskItem>()),Times.Never);

    }

    [Fact]
    public async Task CreateTaskAsync_ProjectExists_SavesAndReturnsId()
    {
        // Arrange
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

        // Act
        var newId = await service.CreateTaskAsync(request);

        // Assert
        Assert.Equal(42, newId);
        taskRepo.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);

    }

    [Fact]
    public async Task MoveToProjectAsync_TaskDoesNotExist_ReturnsFalseAndChecksNothingElse()
    {
        // Arrange
        var taskRepo = new Mock<ITaskRepository>();
        var projectRepo = new Mock<IProjectRepository>();

        taskRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((TaskItem?)null);

        var service = new TaskService(taskRepo.Object, projectRepo.Object);
        var request = new MoveTaskRequest { ProjectId = 1 };

        // Act
        var result = await service.MoveToProjectAsync(99, request);

        // Assert
        Assert.False(result);
        projectRepo.Verify(r => r.ExistsAsync(It.IsAny<int>()), Times.Never);
        taskRepo.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>()), Times.Never);
    }

    [Fact]
    public async Task MoveToProjectAsync_ProjectDoesNotExist_ThrowsAndDoesNotSave()
    {

        //Arrange
        var taskRepo = new Mock<ITaskRepository>();
        var projectRepo = new Mock<IProjectRepository>();

        var service = new TaskService(taskRepo.Object, projectRepo.Object);

        var existingTask = new TaskItem("title", 5);

        taskRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync(existingTask);

        projectRepo.Setup(r => r.ExistsAsync(6)).ReturnsAsync(false);

        var request = new MoveTaskRequest { ProjectId = 6};

        //Act

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.MoveToProjectAsync(99, request));
        Assert.Contains("does not exist", ex.Message);
        
        //Assert
        taskRepo.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>()), Times.Never);
        
    }

}

