using System;
using System.Collections.Generic;

namespace JobApplicationPortal.Models;

public partial class TQuestion
{
    public int QId { get; set; }

    /// <summary>
    /// test id from tests table
    /// </summary>
    public int QTId { get; set; }

    public string QText { get; set; } = null!;

    public DateTime? QCreateDate { get; set; }

    public bool? QStatus { get; set; }

    public DateTime? QUpdateDate { get; set; }

    public virtual TTest QT { get; set; } = null!;

    public virtual ICollection<TAnswer> TAnswers { get; set; } = new List<TAnswer>();

    public virtual ICollection<TCandidateAnswer> TCandidateAnswers { get; set; } = new List<TCandidateAnswer>();
}
