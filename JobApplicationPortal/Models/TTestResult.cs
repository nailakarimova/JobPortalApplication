using System;
using System.Collections.Generic;

namespace JobApplicationPortal.Models;

public partial class TTestResult
{
    public int TrId { get; set; }

    public int TrCId { get; set; }

    /// <summary>
    /// Test id
    /// </summary>
    public int TrTId { get; set; }

    public decimal? TrScore { get; set; }

    public DateTime? TrTakenDate { get; set; }

    public string? TrStatus { get; set; }

    public DateTime? TrCreateDate { get; set; }

    public virtual ICollection<TCandidateAnswer> TCandidateAnswers { get; set; } = new List<TCandidateAnswer>();

    public virtual TCandidate TrC { get; set; } = null!;

    public virtual TTest TrT { get; set; } = null!;
}
