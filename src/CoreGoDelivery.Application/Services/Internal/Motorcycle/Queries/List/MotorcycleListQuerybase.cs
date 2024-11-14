using CoreGoDelivery.Domain.Consts.Pagination;
using CoreGoDelivery.Domain.Enums.Pagination;

namespace CoreGoDelivery.Application.Services.Internal.Motorcycle.Queries.List;

public class MotorcycleListQuerybase
{
    public OrderByEnum OrderBy { get; set; } = OrderByEnum.desc;
    public MotorcycleListFindBy? FindyBy { get; set; }
    public string? ValueToFind { get; set; }
    public int Limit { get; set; } = LimitPageConst.DEFAULT_LIMIT;
    public int Page { get; set; } = 1;
}