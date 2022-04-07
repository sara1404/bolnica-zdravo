namespace Model
{
    public class Operation : Appointment
    {
        private readonly Room operationRoom;
        public Operation() : base(0, null, null, System.DateTime.Now)
        {

        }
    }
}