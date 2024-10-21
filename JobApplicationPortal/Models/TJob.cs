using System;
using System.Collections.Generic;

namespace JobApplicationPortal.Models;

public partial class TJob
{
    public int JId { get; set; }

    public string? JTitle { get; set; }

    public string? JRequirements { get; set; }

    public bool? JStatus { get; set; }

    public DateTime? JCreateDate { get; set; }

    public DateTime? JUpdateDate { get; set; }

    /// <summary>
    /// test id from test table 
    /// </summary>
    public int? JTId { get; set; }

    public int? JQuestionQty { get; set; }

    public virtual TTest? JT { get; set; }
}
