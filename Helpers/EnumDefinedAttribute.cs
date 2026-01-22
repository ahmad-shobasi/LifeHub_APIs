using System.ComponentModel.DataAnnotations;

namespace LifeHub_APIs.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDefinedAttribute: ValidationAttribute
    {
        private readonly Type _enumType;
        public EnumDefinedAttribute(Type enumType)
        {
            if(!enumType.IsEnum)
                throw new ArgumentException("Type must be an enum.", nameof(enumType));
            _enumType = enumType;
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
                return true;
            var underLying = Enum.GetUnderlyingType(_enumType);

            try
            {
                var converted = Convert.ChangeType(value, underLying);
                return Enum.IsDefined(_enumType, converted);
            }
            catch { return false; }

        }
        public override string FormatErrorMessage(string name) =>
            string.IsNullOrEmpty(ErrorMessage)
                ? $"{name} must be a valid {_enumType.Name} value."
                : ErrorMessage!;
        
    }
}
