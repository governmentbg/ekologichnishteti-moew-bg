using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Emails.Enums;

namespace Zopoesht.Data.Emails
{
    public class EmailAddressee : IEntity
    {
        public int Id { get; set; }

        public int EmailId { get; set; }

        public Email Email { get; set; }

        public EmailAddresseeType AddresseeType { get; set; }

        public string Address { get; set; }

        public DateTime SentDate { get; set; }

        public EmailStatus Status { get; set; }

        public int AttemptsCounter { get; set; }

        private EmailAddressee()
        {

        }

        public EmailAddressee(EmailAddresseeType type, string address)
        {
            this.AddresseeType = type;
            this.Address = address;
            this.SentDate = DateTime.UtcNow;
            this.Status = EmailStatus.Pending;
        }
    }
}
