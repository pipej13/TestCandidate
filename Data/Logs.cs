using System;
using System.Collections.Generic;

namespace Candidates.Data;

public partial class Logs
{
    public int IdLog { get; set; }

    public string Error { get; set; } = null!;

    public string Modulo { get; set; } = null!;
}
