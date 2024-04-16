using System;
using System.Collections.Generic;

namespace Samurai_V2_.Net_8.DbContexts.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Mobile { get; set; }

    public DateTime? DateofBirth { get; set; }

    public DateTime? ActionDate { get; set; }

    public bool? IsActive { get; set; }
}
