using AutoMapper;
using TaskEngine.Application.Categories.Commands.CreateCategory;
using TaskEngine.Application.Categories.Commands.UpdateCategory;
using TaskEngine.Application.DTOs;
using TaskEngine.Application.Tasks.Commands.CreateTask;
using TaskEngine.Application.Tasks.Commands.UpdateTask;
using TaskEngine.Application.Users.Commands.CreateUser;
using TaskEngine.Application.Users.Commands.UpdateUser;
using TaskEngine.Domain.Entities;

namespace TaskEngine.Application.Mappings;

// 1. Heredar de Profile le dice a AutoMapper: "Oye, léeme al iniciar la app"
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 2. CreateMap establece el origen (Source) y el destino (Destination)
        CreateMap<TaskItem, TaskDto>()

            // 3. ForMember se usa cuando los nombres NO coinciden 
            // o cuando queremos sacar información de un objeto anidado.
            .ForMember(
                dest => dest.CategoryName, // Destino: La propiedad en tu TaskDto
                opt => opt.MapFrom(src =>
                    src.Category != null ? src.Category.Name : "Sin Categoría") // Origen: Lógica para obtener el dato
            ).ReverseMap();

        CreateMap<CreateTaskCommand, TaskItem>();

        CreateMap<TaskUpdateDto, TaskItem>();

        CreateMap<UpdateTaskCommand, TaskItem>();

        //Category

        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<CreateCategoryCommand, Category>();

        CreateMap<UpdateCategoryCommand, Category>();

        // Para User

        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<CreateUserCommand, User>();

        CreateMap<UpdateUserCommand, User>();


    }
}