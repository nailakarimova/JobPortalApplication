using System;
using System.Collections.Generic;

namespace JobApplicationPortal.Models;

public partial class TCandidate
{
    public int CId { get; set; }

    public string CFirstname { get; set; } = null!;

    public string CLastname { get; set; } = null!;

    public string CEmail { get; set; } = null!;

    public string? CPhone { get; set; }

    public DateTime? CCreateDate { get; set; }

    public string? CResumePath { get; set; }

    public virtual ICollection<TCandidateAnswer> TCandidateAnswers { get; set; } = new List<TCandidateAnswer>();

    public virtual ICollection<TTestResult> TTestResults { get; set; } = new List<TTestResult>();
}
