using Lab3.Data.Models.Link;

namespace Lab3.Data.Models.Student;

public class StudentResponseModel
{
    public int? Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? Phone { get; set; } = null!;
    public List<LinkResponseModel> Links { get; set; } = [];
}
