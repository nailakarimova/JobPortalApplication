using System;
using System.Collections.Generic;

namespace JobApplicationPortal.Models;

public partial class TAnswer
{
    public int AId { get; set; }

    /// <summary>
    /// question id from questions table
    /// </summary>
    public int AQId { get; set; }

    public string AText { get; set; } = null!;

    public bool AIscorrect { get; set; }

    public bool? AStatus { get; set; }

    public DateTime? ACreateDate { get; set; }

    public DateTime? AUpdateDate { get; set; }

    public virtual TQuestion AQ { get; set; } = null!;

    public virtual ICollection<TCandidateAnswer> TCandidateAnswers { get; set; } = new List<TCandidateAnswer>();
}
