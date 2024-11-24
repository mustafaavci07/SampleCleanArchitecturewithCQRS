
using SampleCleanArchitecture.Core.Domain.Payments;

namespace SampleCleanArchitecture.Core.Domain.Passengers
{
    public class Passenger : AuditableBaseEntity
    {
        public DateTime BirthDate { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }

        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public List<PassengerJourney> Journeys { get; set; }
        public List<Payment> Payments { get; set; }
    }

}
