namespace CoreGoDelivery.Domain.Consts;

public static class RentalServiceConst
{
    public const double PENALTY_VALUE_PER_DAY = 50.0;
    public const int MINIMAL_DAYS_PLAN = 7;
    public const double MINIMAL_FEE_PERCENTAGE = 20.0;
    public const double DEFAULT_FEE_PERCENTAGE = 40.0;
    public const string MESSAGE_RETURNED_TO_BASE_SUCCESS = "return to base date accepted with success";
    public const string MESSAGE_RETURNED_BEFORE = "Returned before expected";
    public const string MESSAGE_RETURNED_AFTER = "Returned after expected";
}
