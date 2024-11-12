namespace BookShelfter.Application.Features.Commands.Book.RemoveProduct;

public class RemoveProductCommandResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}