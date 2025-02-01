namespace InnoClinic.Profiles.Core.Models
{
    /// <summary>
    /// Represents an account in the system.
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the email address associated with the account.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number associated with the account.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier for the photo associated with the account.
        /// </summary>
        public Guid PhotoId { get; set; }
    }
}
