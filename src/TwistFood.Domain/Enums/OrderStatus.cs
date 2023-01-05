namespace TwistFood.Domain.Enums
{
    public enum OrderStatus
    {
        InQueue = 0,
    Confirmed = 1,
    OnProcess = 2,
    OnDelivery = 3,
    InPoint = 4,
    Successful = 5,
    Cancelled = -1,
    }
}
