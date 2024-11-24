

namespace SampleCleanArchitecture.Shared.DTO.Payments
{
    public record PaymentDTO (Ulid Id,PassengerDTO passenger,JourneyDTO journey,string Bank,string CardNo,DateTime CardValidTill,DateTime PaymentDate, PaymentState PaymentState)
    {
    }
}

