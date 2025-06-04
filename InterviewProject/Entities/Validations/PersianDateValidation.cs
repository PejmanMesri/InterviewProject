//using System.ComponentModel.DataAnnotations;
//using System.Globalization;

//namespace InterviewProject.Entities.Validations
//{
//    public class PersianDateValidation : ValidationAttribute
//    {
//        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//        {
//            // Explicit null check for the input value
//            if (value == null)
//            {
//                return new ValidationResult("مقدار تاریخ نمی‌تواند خالی باشد");
//            }

//            // If it's already a valid DateTime, return success
//            if (value is DateTime)
//            {
//                return ValidationResult.Success;
//            }

//            // Get the Persian date property info
//            var persianDateProperty = validationContext.ObjectType.GetProperty("PersianRequiredDate");
//            if (persianDateProperty == null)
//            {
//                return new ValidationResult("ویژگی تاریخ فارسی (PersianRequiredDate) یافت نشد");
//            }

//            // Get the Persian date string value
//            var persianDateValue = persianDateProperty.GetValue(validationContext.ObjectInstance) as string;
//            if (string.IsNullOrWhiteSpace(persianDateValue))
//            {
//                return new ValidationResult("تاریخ فارسی وارد نشده است");
//            }

//            try
//            {
//                // Parse the Persian date components
//                var parts = persianDateValue.Split('/');
//                if (parts.Length != 3)
//                {
//                    return new ValidationResult("فرمت تاریخ باید به صورت YYYY/MM/DD باشد");
//                }

//                // Parse year, month, day with additional validation
//                if (!int.TryParse(parts[0], out int year) || year < 1300 || year > 1500)
//                {
//                    return new ValidationResult("سال باید بین ۱۳۰۰ تا ۱۵۰۰ باشد");
//                }

//                if (!int.TryParse(parts[1], out int month) || month < 1 || month > 12)
//                {
//                    return new ValidationResult("ماه باید بین ۱ تا ۱۲ باشد");
//                }

//                if (!int.TryParse(parts[2], out int day) || day < 1 || day > 31)
//                {
//                    return new ValidationResult("روز باید بین ۱ تا ۳۱ باشد");
//                }

//                // Create Persian calendar and validate date
//                PersianCalendar pc = new PersianCalendar();
//                DateTime utcDate;

//                try
//                {
//                    utcDate = new DateTime(year, month, day, pc);
//                }
//                catch (ArgumentOutOfRangeException)
//                {
//                    return new ValidationResult("تاریخ وارد شده معتبر نیست (روز برای ماه انتخابی صحیح نیست)");
//                }

//                // Specify as UTC
//                utcDate = DateTime.SpecifyKind(utcDate, DateTimeKind.Utc);

//                // Set the actual DateTime property
//                var dateProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);
//                if (dateProperty == null)
//                {
//                    return new ValidationResult("ویژگی تاریخ مقصد یافت نشد");
//                }

//                dateProperty.SetValue(validationContext.ObjectInstance, utcDate);

//                return ValidationResult.Success;
//            }
//            catch (Exception ex)
//            {
//                // More specific error message for debugging
//                return new ValidationResult($"خطا در پردازش تاریخ: {ex.Message}");
//            }
//        }
//    }
//}