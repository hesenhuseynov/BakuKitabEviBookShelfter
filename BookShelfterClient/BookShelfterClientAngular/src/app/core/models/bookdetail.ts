export interface BookDetailDto {
    bookId: number;
    bookName: string;
    authorName: string;
    stock: number;
    price: number;
    description: string;
    categoryId: number | null;
    imageUrls: string[];
    averageRating: number;
    totalReviews: number;  
  }
  