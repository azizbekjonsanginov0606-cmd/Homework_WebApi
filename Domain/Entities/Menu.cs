namespace Domain.Entities;

public class Menu
{
    public int Id { get; set; }

    public DateOnly MenuDate { get; set; }

    public Boolean IsActive { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }
}
