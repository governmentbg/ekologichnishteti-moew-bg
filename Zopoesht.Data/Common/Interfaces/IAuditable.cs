namespace Zopoesht.Data.Common.Interfaces
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }
        int CreatorUserId { get; set; }
    }
}