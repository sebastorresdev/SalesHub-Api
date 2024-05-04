namespace ApplicationLayer;

public interface IDashboardService
{
    Task<DashboardDto> Summary();
}
