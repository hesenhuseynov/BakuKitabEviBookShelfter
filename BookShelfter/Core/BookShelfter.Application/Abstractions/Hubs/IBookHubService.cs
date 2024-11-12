namespace BookShelfter.Application.Abstractions.Hubs;

public interface IBookHubService
{
    Task BookAddedMessageAsync(string message);
    
    
    
    
    
    
}