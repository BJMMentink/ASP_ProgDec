using BJM.ProgDec.BL.Models;
using BJM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;


namespace BJM.ProgDec.BL
{
    public static class DegreeTypeManager
    {
        // by refrence is used by calling "ref" or "out"
        public static int Insert(string description, ref int id, bool roleback = false)
        {
            try
            {
                DegreeType degreeType = new DegreeType
                {
                    Description = description
                };
                int results = Insert(degreeType, roleback);
                id = degreeType.Id;
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Insert(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblDegreeType entity = new tblDegreeType();
                    // if ? option 1 : option 2
                    entity.Id = dc.tblDegreeTypes.Any() ? dc.tblDegreeTypes.Max(s => s.Id) + 1 : 1;
                    entity.Description = degreeType.Description;

                    foreach(Program program in degreeType.Programs)
                    {
                        // set orderid on tbl orderItems
                        results += ProgramManager.Insert(program, rollback);
                    }

                    // IMPORTANT - Back Fill ID
                    degreeType.Id = entity.Id;

                    dc.tblDegreeTypes.Add(entity);
                    results += dc.SaveChanges();
                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    // get the row we are trying to update
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == degreeType.Id);
                    if (entity != null)
                    {
                        entity.Description = degreeType.Description;
                        results = dc.SaveChanges();
                    }
                    if (rollback) transaction.Rollback();
                    else throw new Exception("Row does not exist");

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    // get the row we are trying to update
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        dc.tblDegreeTypes.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    if (rollback) transaction.Rollback();
                    else throw new Exception("Row does not exist");

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static DegreeType LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblDegreeType entity = dc.tblDegreeTypes.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        return new DegreeType
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            Programs = ProgramManager.Load(entity.Id)
                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

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
                    (from s in dc.tblDegreeTypes
                     select new
                     {
                         s.Id,
                         s.Description
                     })
                     .ToList()
                     .ForEach(degreeType => list.Add(new DegreeType
                     {
                         Id = degreeType.Id,
                         Description = degreeType.Description,
                         Programs = ProgramManager.Load(degreeType.Id)
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
