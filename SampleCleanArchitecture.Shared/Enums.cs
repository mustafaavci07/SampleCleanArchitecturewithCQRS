using Ardalis.SmartEnum;

namespace SampleCleanArchitecture.Shared
{
    public class Gender:SmartEnum<Gender>
    {
        private Gender(string name, int value) : base(name, value)
        {
        }

        public static readonly Gender Male = new Gender("Male", 1);
        public static readonly Gender Female = new Gender("Female", 2);
    }

    public class JourneyState : SmartEnum<JourneyState>
    {
        private JourneyState(string name, int value) : base(name, value)
        {
        }

        public static readonly JourneyState Done = new JourneyState("Done", 1);
        public static readonly JourneyState Canceled = new JourneyState("Canceled", 2);
        public static readonly JourneyState Waiting = new JourneyState("Waiting", 3);
    }

    public class PaymentState : SmartEnum<PaymentState>
    {
        private PaymentState(string name, int value) : base(name, value)
        {
        }

        public static readonly PaymentState Waiting = new PaymentState("Waiting", 1);
        public static readonly PaymentState Approved = new PaymentState("Approved", 2);
        public static readonly PaymentState Rejected = new PaymentState("Rejected", 3);
        public static readonly PaymentState Canceled = new PaymentState("Canceled", 3);
    }
    
}
