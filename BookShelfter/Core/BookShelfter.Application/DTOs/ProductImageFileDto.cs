using BookShelfter.Domain.Entities;

namespace BookShelfter.Application.DTOs;

public class ProductImageFileDto
{
    public string ImageUrl { get; set; }
    public string FileName { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; } 
    
}