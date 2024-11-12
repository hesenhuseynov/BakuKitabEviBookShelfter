import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { AddReviewResponse, Review } from '../../../core/models/review';
import { ReviewService } from '../../../core/services/review.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { error } from 'node:console';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {
  @Input() bookId!: number;
  reviews: Review[] = [];
  newReview: Review = { bookId: 0, userId: '', rating: 0, comment: '', userName: '' };
  stars: number[] = [1, 2, 3, 4, 5];
  currentRating = 0;
  hoverRating = 0;
  isLoggedIn: boolean = false;
  showModal :boolean =false;
  errorMessage: string = '';


  constructor(private reviewService: ReviewService, 
    private authService:AuthService,private router:Router,
    private toastr:ToastrService,
    private changeDetectorRef:ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    console.log("Received bookId: ", this.bookId);

      this.isLoggedIn = !!this.authService.getToken();
      this.currentRating = 0; 
      this.hoverRating = 0;  
       this.loadReviews();
  }

  loadReviews(){
    this.reviewService.getReviews(this.bookId).subscribe({
      next: (data) => {
        if (data && data.reviews) {
          this.reviews = data.reviews;
        }
      },
      error: (err) => {
        if (err.status === 204) {
          console.log('Bu kitap için henüz yorum yapılmamış.');
          this.reviews = [];
        } else {
          this.toastr.error('Yorumlar yüklenirken bir hata oluştu.', 'Hata');
        }
      }
    });
  }

  
  onReviewSectionClick(): void {
    if (!this.isLoggedIn) {
      this.showModal = true;
    }
  }


  showLoginPrompt(): void {
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }
  
  redirectToLogin(): void {
    this.router.navigate(['/login']);
  }


  selectStar(rating: number): void {
    this.currentRating = rating;
    this.newReview.rating = rating;
  }

  hoverStar(rating: number): void {
    this.hoverRating = rating;

  }
  
  addReview(form: NgForm): void {
    if (!form.valid || this.currentRating === 0){
      this.toastr.warning('Xaiş edirik şərh qismini doldurun.', 'Uyarı');
      return;
    }
  
    this.newReview.bookId = this.bookId;
    this.newReview.userId = this.authService.getUserId()!;
    this.newReview.userName = this.authService.getUserName()!;
  
    this.reviewService.addReview(this.newReview).subscribe({
      next: (response: AddReviewResponse) => {
        if (response.success) {
          this.reviews.push(response.review!);
          form.resetForm();
          this.currentRating = 0;
          this.hoverRating = 0;


          this.loadReviews();
          this.toastr.success('Şərhiniz əlavə  edildi.', 'Uğurlu');
        } else {
          this.toastr.error(response.message, 'Xəta');
        }
      },
      error: (err) => {
        if (err.error && err.error.message) {
          this.toastr.error(err.error.message, 'Xəta');
        } else {
          this.toastr.error('Şərh əlavə edilərkən bir problem baş verdi.', 'Xəta');
        }
      }
    });
  }
  
  


}

