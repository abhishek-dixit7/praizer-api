using System;
using System.Collections.Generic;

namespace praizer_api.Database.Models;

public partial class Praise
{
    public int Id { get; set; }

    public int? UserPraisedId { get; set; }

    public int? PraizerId { get; set; }

    public DateTime? CreateOn { get; set; }

    public string PraizeText { get; set; } = null!;

    public int? RewardPoints { get; set; }

    public string? RecognitionType { get; set; }

    public virtual User? Praizer { get; set; }

    public virtual User? UserPraised { get; set; }
}
