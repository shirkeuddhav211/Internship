using System.ComponentModel.DataAnnotations;

namespace GISApi.Models
{
    public class SpecialCharacterVerify
    {
        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
        public class ApplyRegularExpressionToPropertiesAttribute : ValidationAttribute
        {
            private readonly string pattern;

            public ApplyRegularExpressionToPropertiesAttribute(string pattern)
            {
                this.pattern = pattern;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value == null)
                {
                    return ValidationResult.Success; // Nothing to validate if the object is null
                }

                var objectType = value.GetType();
                var stringProperties = objectType.GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                foreach (var property in stringProperties)
                {
                    if (property.PropertyType == typeof(string))
                    {


                        var propertyValue = (string?)property.GetValue(value);
                        if (!string.IsNullOrEmpty(propertyValue) && !System.Text.RegularExpressions.Regex.IsMatch(propertyValue, pattern))
                        {
                            return new ValidationResult(ErrorMessage ?? $"{property.Name} contains invalid characters.");
                        }
                    }
                }

                return ValidationResult.Success;
            }
        }
    }
}
