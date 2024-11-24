namespace SampleCleanArchitecture.Shared.DTO.Passengers
{
    public record PassengerDTO(Ulid Id,string NationalIdentityNumber,string Name,string Surname, Gender Gender,
        string MailAddress, string PhoneNumber, string Address,List<PassengerJourneyDTO> Journeys,List<PaymentDTO> Payments
        )
    {
    }
}
