using Lab3.Data.Models.Link;

namespace Lab3.Data.Models.Error;

public class ErrorResponseModel
{
    public int Code { get; set; }
    public List<LinkResponseModel> Links { get; set; } = [];
}
