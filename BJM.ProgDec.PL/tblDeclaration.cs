using System;
using System.Collections.Generic;

namespace BJM.ProgDec.PL;

public partial class tblDeclaration
{
    public int Id { get; set; }

    public int ProgramId { get; set; }

    public int StudentId { get; set; }

    public DateTime ChangeDate { get; set; }
}
