using FluentValidation;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Validators
{
    public class ActorValidator: AbstractValidator<Actor>
    {

        public ActorValidator() {

            RuleFor(x => x.Name).NotEmpty().NotNull();

            RuleFor(x => x.LastName).NotEmpty().NotNull();

            RuleFor(x => x.Age).NotEmpty();

        }
    }
}
