﻿using Microsoft.AspNetCore.Mvc;

namespace BJM.ProgDec.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(ProgramManager.Load().OrderBy(p => p.Description));
        }
    }
}
