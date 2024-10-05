using System.ComponentModel;

namespace PetHome.Domain.Shared
{
    public enum HelpStatus
    {
        [Description("Нуждается в помщи")]
        NeedHelp,
        [Description("Нужен дом")]
        NeeedHome,
        [Description("Нашел дом")]
        FoundHome,
        [Description("На лечении")]
        OnTreatment
    }
}
