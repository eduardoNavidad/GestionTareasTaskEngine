using AutoMapper;
using Moq;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Tasks.Commands.UpdateTask;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;
using Xunit;

namespace TaskEngine.Tests.Tasks.Commands;

public class UpdateTaskHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepoMock;
    private readonly Mock<ICategoryRepository> _categoryRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateTaskHandler _handler;

    public UpdateTaskHandlerTests()
    {
        _taskRepoMock = new Mock<ITaskRepository>();
        _categoryRepoMock = new Mock<ICategoryRepository>();
        _mapperMock = new Mock<IMapper>();

        // Inyectamos los 3 mocks en el orden de tu constructor
        _handler = new UpdateTaskHandler(_taskRepoMock.Object, _mapperMock.Object, _categoryRepoMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Update_ShouldReturnTaskDto_WhenEverythingIsOk()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        var command = new UpdateTaskCommand(taskId, "Nuevo", "Desc", true, categoryId);

        var existingTask = new TaskItem { Id = taskId };
        var existingCategory = new Category { Id = categoryId };

        // Mocks de repositorios
        _taskRepoMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
        _categoryRepoMock.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(existingCategory);

        // Forzamos el retorno para evitar conflictos de nombres
        _taskRepoMock.Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
                     .Returns(System.Threading.Tasks.Task.FromResult(1));

        // Fix: Usamos el constructor del TaskDto con todos sus parámetros
        var taskDtoMock = new TaskDto(taskId, "Tarea Actualizada", "Desc", DateTime.Now, true, categoryId, "Categoría");

        _mapperMock.Setup(m => m.Map<TaskDto>(It.IsAny<object>()))
                   .Returns(taskDtoMock);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskId, result.Id);
    }
}