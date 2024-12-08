using AutoMapper;
using Lab3.Data.Models.Student;

namespace Lab3.Data;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<StudentAddReplaceRequestModel, Student>();
        CreateMap<Student, StudentResponseModel>();
    }
}
