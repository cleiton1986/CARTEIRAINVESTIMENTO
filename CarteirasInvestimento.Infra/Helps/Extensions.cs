using System.ComponentModel;
using System.Reflection;

namespace CarteirasInvestimento.Infra
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static bool ValidateString(this string value, string mensagem)
        {
            var validate = value.Equals("string") || string.IsNullOrWhiteSpace(value);
            if (validate)
            {
                Notification.NotifyList(mensagem);
            }
            return validate;
        }

        public static bool ValidateDecimal(this decimal value, string mensagem)
        {
            var validate = value <= 0;
            if (validate)
            {
                Notification.NotifyList(mensagem);
            }
            return validate;
        }

        public static bool ValidateInt(this int value, string mensagem)
        {
            var validate = value <= 0;
            if (validate)
            {
                Notification.NotifyList(mensagem);
            }
            return validate;
        }
        public static int GetNumero()
        {
            Random a = new Random();
   
            int c = a.Next(100, 999);
            return c;
        }
    }
}
