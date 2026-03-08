namespace Domain.Entities;

public class Subscription
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string PlanType { get; set; } = string.Empty;

    public int MealsPerDay { get; set; }

    public decimal Price { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
