using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Features.Commands.Review;
using BookShelfter.Application.Repositories.Review;
using FluentValidation;

namespace BookShelfter.Application.Validators.Reviews
{
    public  class AddReviewValidator:AbstractValidator<AddReviewCommandRequest>
    {
        private readonly IReviewReadRepository _reviewRepository;

        public AddReviewValidator(IReviewReadRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Şərh  boş olmamalıdır")
                .MaximumLength(1000).WithMessage("Şərh maksimum 1000 hərf ola bilər");


        }

      
    }
}
