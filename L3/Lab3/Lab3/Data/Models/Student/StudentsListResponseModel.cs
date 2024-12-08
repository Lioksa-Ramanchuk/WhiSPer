using Lab3.Data.Models.Link;

namespace Lab3.Data.Models.Student;

public class StudentsListResponseModel
{
    public List<StudentResponseModel> Students { get; set; } = [];
    public List<LinkResponseModel> Links { get; set; } = [];
}
