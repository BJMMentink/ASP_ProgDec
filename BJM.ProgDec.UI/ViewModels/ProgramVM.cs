﻿namespace BJM.ProgDec.UI.ViewModels
{
    public class ProgramVM
    {
        public BL.Models.Program Program { get; set; }
        public List<DegreeType> DegreeTypes { get; set; }
        public IFormFile File { get; set; }
    }
}
