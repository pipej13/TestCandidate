using System;
using System.Collections.Generic;

namespace Candidates.Data;

public partial class Candidate
{
    public int IdCandidate { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public string Email { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual ICollection<CandidateEsperiences> CandidateEsperiences { get; set; } = new List<CandidateEsperiences>();
}
