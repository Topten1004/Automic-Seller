namespace Sales.AtomicSeller.Providers
{
    public interface IIdentityProvider
    {
        public string Username { get; }
        public string UserId { get; }
        //public bool IsAdministrator { get;  }
    }
}
