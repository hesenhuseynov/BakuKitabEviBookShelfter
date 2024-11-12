namespace BookShelfter.Application.Features.Commands.Category;

public class CreateCategoryCommandResponse
{
    public bool  Success { get; set; }
    public string CategoryId { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    

}