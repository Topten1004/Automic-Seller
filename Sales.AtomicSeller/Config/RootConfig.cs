using Microsoft.Extensions.Options;

namespace Sales.AtomicSeller.Config
{
    /// <summary>
    /// Main Config Class.
    /// </summary>
    public class RootConfig : IRootConfig
    {
        /// <inheritdoc/>
        public IdentityDataConfig IdentityDataConfig { get; set; } = new IdentityDataConfig();
        /// <inheritdoc/>
        public StripeConfig StripeConfig { get; set; } = new StripeConfig();
        /// <inheritdoc/>
        public EmailCredConfig EmailCredConfig { get; set; } = new EmailCredConfig();
        /// <inheritdoc/>
        public SMTPConfig SMTPConfig { get; set; } = new SMTPConfig();
        /// <inheritdoc/>
        public StoreDataConfig StoreDataConfig { get; set; } = new StoreDataConfig();
        /// <inheritdoc/>
        public LangConfig LangConfig { get; set; } = new LangConfig();
        /// <inheritdoc/>
        public GoogleAuthConfig GoogleAuthConfig { get; set; } = new GoogleAuthConfig();
        /// <inheritdoc/>
        public FacebookAuthConfig FacebookAuthConfig { get; set; } = new FacebookAuthConfig();
        /// <inheritdoc/>
        public MicrosoftAuthConfig MicrosoftAuthConfig { get; set; } = new MicrosoftAuthConfig();
        /// <inheritdoc/>
        public TwitterAuthConfig TwitterAuthConfig { get; set; } = new TwitterAuthConfig();
        /// <inheritdoc/>
        public InvoiceConfig InvoiceConfig { get; set; } = new InvoiceConfig();
    }
}
