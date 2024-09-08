namespace DefaultNamespace;

public abstract class BookingStatus:Enumeration
{
    public static readonly BookingStatus Pending = new BookingStatusEnum(1, "Pending");
    public static readonly BookingStatus Confirmed = new BookingStatusEnum(2, "Confirmed");
    public static readonly BookingStatus Cancelled = new BookingStatusEnum(3, "Cancelled");
    public static readonly BookingStatus Completed = new BookingStatusEnum(4, "Completed");

    protected BookingStatus(int id, string name) : base(id, name) { }

    private class BookingStatusEnum : BookingStatus
    {
        public BookingStatusEnum(int id, string name) : base(id, name) { }
    }
}