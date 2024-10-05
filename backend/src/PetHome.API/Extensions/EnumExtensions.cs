using PetHome.Domain.Shared;
using System.ComponentModel;
using System.Reflection;

namespace PetHome.API.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum value)
        {
            var member = typeof(TEnum).GetMember($"{value}")
                .FirstOrDefault(typeof(TEnum));

            if (member != null
                && member.GetCustomAttribute(typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return $"{value}";
        }

        public static HelpStatus GetHelpStatusByDescription(this string stringValue)
        {
            var valueMambers = typeof(HelpStatus).GetMembers(
            BindingFlags.Public
            | BindingFlags.Static
            | BindingFlags.Default);

            var enumList = Enum.GetValues(typeof(HelpStatus)).Cast<HelpStatus>().ToList();

            if (enumList == null)
            {
                return default!;
            }

            foreach (var member in valueMambers)
            {               
                var description = (member.GetCustomAttribute(typeof(DescriptionAttribute)) 
                        as DescriptionAttribute)?.Description;

                if (!string.IsNullOrWhiteSpace(description) && description == stringValue)
                {
                    return enumList.FirstOrDefault(e => e.ToString() == member.Name);
                }
            }

            return default!;
        }
    }
}
