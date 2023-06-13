using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Core
{
    public static class EnumHelper
    {
        public static int ToOrder<T>(this Enum e)
        {
            var field = typeof(T).GetField(e.ToString());
            var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
#pragma warning disable CS8604 // Possible null reference argument.
            return attributes.First().Order;
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static string ToDescription<T>(this Enum e)
        {
            var field = typeof(T).GetField(e.ToString());
            var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
            return attributes.First().Description;
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static string ToShortName<T>(this Enum e)
        {
            var field = typeof(T).GetField(e.ToString());
            var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
            return attributes.First().ShortName;
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static string? ToName<T>(this Enum e)
        {
            var field = typeof(T).GetField(e.ToString());

            if (field == null)
            {
                return string.Empty;
            }

            var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
#pragma warning disable CS8604 // Possible null reference argument.
            return attributes.Any() ? attributes.First().Name : e.ToString();
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}