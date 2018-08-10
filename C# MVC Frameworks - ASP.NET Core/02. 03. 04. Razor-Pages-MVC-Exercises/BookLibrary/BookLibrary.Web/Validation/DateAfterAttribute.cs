namespace BookLibrary.Web.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateAfterAttribute : ValidationAttribute
    {
        private readonly string previousDatePropertyName;

        public DateAfterAttribute(string previousDatePropertyName)
            : base()
        {
            this.previousDatePropertyName = previousDatePropertyName;
        }
        public DateAfterAttribute(string previousDatePropertyName, string errorMessage)
            : base(errorMessage)
        {
            this.previousDatePropertyName = previousDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.ErrorMessage = this.ErrorMessageString;
            var currentValue = (DateTime?)value;

            if (!currentValue.HasValue)
            {
                return ValidationResult.Success;
            }

            var otherProperty = validationContext.ObjectType.GetProperty(previousDatePropertyName);

            if (otherProperty == null)
            {
                throw new ArgumentException("Property with this name not found");

            }
            var comparisonValue = (DateTime)otherProperty.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
            {
                return new ValidationResult(this.ErrorMessageString);
            }

            return ValidationResult.Success;
        }
    }
}