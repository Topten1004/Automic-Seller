namespace Sales.AtomicSeller.Config
{
    /// <summary>
    /// SMTP Server config.
    /// </summary>
    public class SMTPConfig
    {
        /// <summary>
        /// Host address.
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// SMTP Port.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Enalble SSL.
        /// </summary>
        public bool EnableSsl { get; set; }
        /// <summary>
        /// Use defaurlt credentials.
        /// </summary>
        public bool UseDefaultCredentials { get; set; }
    }
}
