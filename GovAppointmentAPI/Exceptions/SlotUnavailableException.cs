namespace GovAppointmentAPI.Exceptions
{
    public class SlotUnavailableException : Exception
    {
        public SlotUnavailableException(string message) : base(message) { }
    }
}
