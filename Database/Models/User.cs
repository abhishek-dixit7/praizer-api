﻿using System;
using System.Collections.Generic;

namespace praizer_api.Database.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int PointBalance { get; set; }

    public int PointToAward { get; set; }

    public DateOnly DateOfJoining { get; set; }

    public DateTime? CreateOn { get; set; }

    public DateTime? ModifedOn { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public int? ManagerId { get; set; }

    public string Uid { get; set; } = null!;

    public string? PhotoUrl { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<User> InverseManager { get; set; } = new List<User>();

    public virtual User? Manager { get; set; }

    public virtual ICollection<Praise> PraisePraizers { get; set; } = new List<Praise>();

    public virtual ICollection<Praise> PraiseUserPraiseds { get; set; } = new List<Praise>();
}
