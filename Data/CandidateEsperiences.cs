﻿using System;
using System.Collections.Generic;

namespace Candidates.Data;

public partial class CandidateEsperiences
{
    public int IdCandidateExperience { get; set; }

    public int IdCandidate { get; set; }

    public string Company { get; set; } = null!;

    public string Job { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateTime BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime InsertDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual Candidate IdCandidateNavigation { get; set; } = null!;
}
