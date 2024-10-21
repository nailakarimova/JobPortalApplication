using System;
using System.Collections.Generic;

namespace JobApplicationPortal.Models;

public partial class TCandidateAnswer
{
    public int CaId { get; set; }

    public int CaCId { get; set; }

    /// <summary>
    /// result id 
    /// </summary>
    public int CaRId { get; set; }

    /// <summary>
    /// question id
    /// </summary>
    public int CaQId { get; set; }

    /// <summary>
    /// answer id
    /// </summary>
    public int CaAId { get; set; }

    public virtual TAnswer CaA { get; set; } = null!;

    public virtual TCandidate CaC { get; set; } = null!;

    public virtual TQuestion CaQ { get; set; } = null!;

    public virtual TTestResult CaR { get; set; } = null!;
}
