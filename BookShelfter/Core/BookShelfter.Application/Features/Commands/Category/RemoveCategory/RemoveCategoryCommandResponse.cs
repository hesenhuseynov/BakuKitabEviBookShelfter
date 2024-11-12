namespace BookShelfter.Application.Features.Commands.Category.RemoveCategory;

public class RemoveCategoryCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

}