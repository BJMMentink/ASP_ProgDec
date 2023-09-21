using BJM.ProgDec.BL.Models;
using BJM.ProgDec.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJM.ProgDec.BL
{
    public class DegreeTypeManager
    {
        public static int Insert()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }
        public static int Update()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            return 0;
        }
        public static int Delete()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }
        public static DegreeType LoadById(int id)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<DegreeType> Load()
        {
            try
            {
                List<DegreeType> list = new List<DegreeType>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from dt in dc.tblDegreeTypes
                     select new
                     {
                         dt.Id,
                         dt.Description
                     })
                     .ToList()
                     .ForEach(degreeType => list.Add(new DegreeType
                     {
                         Id = degreeType.Id,
                         Description = degreeType.Description
                     }));
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
