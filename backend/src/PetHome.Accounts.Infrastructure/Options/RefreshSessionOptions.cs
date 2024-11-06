namespace PetHome.Accounts.Infrastructure.Options
{
    public class RefreshSessionOptions
    {
        public const string SESSION_OPTIONS = "RefreshSession"; 
        public int ExpiredDaysTime {  get; set; }
    }
}
