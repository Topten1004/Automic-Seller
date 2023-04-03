using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.AtomicSeller.Config
{
    /// <summary>
    /// Main Config Interface contains all appsettings Sections.
    /// </summary>
    public interface IRootConfig
    {
        /// <summary>
        /// Identity Data Config Section.
        /// </summary>
        IdentityDataConfig IdentityDataConfig { get; }
        /// <summary>
        /// Store Data Config Section.
        /// </summary>
        StoreDataConfig StoreDataConfig { get; }

        LangConfig LangConfig { get; }

        /// <summary>
        /// Stripe Config Section.
        /// </summary>
        StripeConfig StripeConfig { get; }
        /// <summary>
        /// Email Cred Config Section.
        /// </summary>
        EmailCredConfig EmailCredConfig { get; }
        /// <summary>
        /// SMTP Config Section.
        /// </summary>
        SMTPConfig SMTPConfig { get; }
        /// <summary>
        /// Google Auth Config Section.
        /// </summary>
        GoogleAuthConfig GoogleAuthConfig { get; }
        /// <summary>
        /// Facebook Auth Config Section.
        /// </summary>
        FacebookAuthConfig FacebookAuthConfig { get; }
        /// <summary>
        /// Microsoft Auth Config Section.
        /// </summary>
        MicrosoftAuthConfig MicrosoftAuthConfig { get; }
        /// <summary>
        /// Twitter Auth Config Section.
        /// </summary>
        TwitterAuthConfig TwitterAuthConfig { get; }
        /// <summary>
        /// Invoice Config Section.
        /// </summary>
        InvoiceConfig InvoiceConfig { get; }
    }

}
