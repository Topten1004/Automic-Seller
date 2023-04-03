using System.Collections.Generic;

namespace Sales.AtomicSeller.Config
{
    /// <summary>
    /// Stripe Keys.
    /// </summary>
    public class StripeConfig
    {
        /// <summary>
        /// Secret key.
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// Publishable Key.
        /// </summary>
        public string PublishableKey { get; set; }
    }
}
