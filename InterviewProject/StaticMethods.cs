using System.Globalization;

namespace InterviewProject
{
    public static class StaticMethods
    {
        public static DateTime ConvertPersianToGregorian(string persianDate)
        {
            PersianCalendar pc = new PersianCalendar();

            persianDate = persianDate.Trim();
            string[] parts = persianDate.Split('/');

            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid Persian date format. Expected format: YYYY/MM/DD");
            }

            int year = ConvertPersianNumberToInt(parts[0]);
            int month = ConvertPersianNumberToInt(parts[1]);
            int day = ConvertPersianNumberToInt(parts[2]);

            if (year < 1 || month < 1 || month > 12 || day < 1 || day > 31)
            {
                throw new ArgumentException("Invalid Persian date values");
            }

            DateTime gregorianDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);

            return gregorianDate;
        }

        private static int ConvertPersianNumberToInt(string persianNumber)
        {
            string westernNumber = persianNumber
                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9");

            if (!int.TryParse(westernNumber, out int result))
            {
                throw new ArgumentException($"Invalid Persian number: {persianNumber}");
            }

            return result;
        }
    }
}
