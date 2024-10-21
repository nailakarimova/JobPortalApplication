using System;
using System.Collections.Generic;
using JobApplicationPortal.Models;

namespace JobApplicationPortal.Models;

public partial class TTest
{
    public int TId { get; set; }

    public string TName { get; set; } = null!;

    public string? TDescription { get; set; }

    public DateTime? TCreateDate { get; set; }

    public bool? TStastus { get; set; }

    public DateTime? TUpdateDate { get; set; }

    public virtual ICollection<TJob> TJobs { get; set; } = new List<TJob>();

    public virtual ICollection<TQuestion> TQuestions { get; set; } = new List<TQuestion>();

    public virtual ICollection<TTestResult> TTestResults { get; set; } = new List<TTestResult>();
}
