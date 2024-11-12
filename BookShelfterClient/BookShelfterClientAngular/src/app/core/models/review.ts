export interface  Review{
    bookId: number;
    userId:string;
    rating:number;
    comment:string;
    userName: string;
  }


  export interface AddReviewResponse {
    success: boolean;
    message: string;
    review?: Review;
  }