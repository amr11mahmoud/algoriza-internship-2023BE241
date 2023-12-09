namespace Vezeeta.Service.Helpers
{
    public static class UserHelper
    {
        public static int CalculateUserAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
