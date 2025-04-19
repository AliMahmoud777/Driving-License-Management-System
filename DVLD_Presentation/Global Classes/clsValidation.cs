using System;
using System.Text.RegularExpressions;

namespace DVLD_Presentation
{
    public static class clsValidation
    {
        public static bool ValidateEmail(string Email)
        {
            string Pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            Regex regex = new Regex(Pattern);

            return regex.IsMatch(Email);
        }

        public static bool ValidateInteger(string Number)
        {
            string Pattern = @"^[0-9]*$";

            Regex regex = new Regex(Pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            string Pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            Regex regex = new Regex(Pattern);

            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }
    }
}