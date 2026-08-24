using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class CollectRequest
{
    public int Id { get; set; }

    public string FoodDescription { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime MaxPreservationTime { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int RestaurantId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Resturant Restaurant { get; set; } = null!;
}
