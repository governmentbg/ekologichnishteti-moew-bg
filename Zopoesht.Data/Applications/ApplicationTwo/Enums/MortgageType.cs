using System.ComponentModel;

namespace Zopoesht.Data.Applications.ApplicationTwo.Enums
{
    public enum MortgageType
    {
        [Description("Върху неджим/и имот/и")]
        RealEstate = 1,

        [Description("Върху вещно/и права върху недвижими имоти")]
        ItemizationRightsOfRealEstate = 2
    }
}