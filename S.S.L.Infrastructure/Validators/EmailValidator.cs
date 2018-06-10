using S.S.L.Infrastructure.S.S.L.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace S.S.L.Infrastructure.Validators
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]


    public class EmailValidator : ValidationAttribute
    {
        //public override string FormatErrorMessage(string name)
        //{
        //    return base.FormatErrorMessage(name);
        //}
        public EmailValidator() : base(errorMessage: "This {0} is taken") => _context = new Entities();

        public Entities _context { get; }

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var email = (value as string)?.Trim().ToLower();
            return String.IsNullOrEmpty(email) || !_context.Users.Any(u => u.Email == email);
        }
    }
}
