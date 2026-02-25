
using AutoMapper;
using MediatR;
using TaskEngine.Application.DTOs;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;

namespace TaskEngine.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateTaskHandler(ITaskRepository taskRepository, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscamos la tarea EXISTENTE (EF ahora la está "vigilando")
        var taskExisting = await _taskRepository.GetByIdAsync(request.Id);

        if (taskExisting == null)
            throw new Exception("La tarea no existe.");

        // 2. Validamos la categoría
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            throw new Exception("La categoría especificada no existe.");

        // 3. ¡LA CLAVE!: Mapeamos el 'request' SOBRE 'taskExisting'
        // Fíjate que no hay un "var algo = ...". Estamos volcando datos en la variable vieja.
        _mapper.Map(request, taskExisting);

        // 4. Actualizamos y guardamos
        await _taskRepository.UpdateAsync(taskExisting);

        // Si no usas Unit of Work, asegúrate de que tu repo o el handler llamen a SaveChanges
        // await _context.SaveChangesAsync(); 

        return _mapper.Map<TaskDto>(taskExisting);


    }
}
