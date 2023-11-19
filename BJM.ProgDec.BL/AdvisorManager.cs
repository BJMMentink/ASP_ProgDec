using BJM.ProgDec.BL.Models;
using BJM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;


namespace BJM.ProgDec.BL
{
    public static class AdvisorManager
    {
        // by refrence is used by calling "ref" or "out"
        public static int Insert(string name, ref int id, bool roleback = false)
        {
            try
            {
                Advisor advisor = new Advisor
                {
                    Name = name
                };
                int results = Insert(advisor, roleback);
                id = advisor.Id;
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Insert(Advisor advisor, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblAdvisor entity = new tblAdvisor();
                    // if ? option 1 : option 2
                    entity.Id = dc.tblAdvisors.Any() ? dc.tblAdvisors.Max(s => s.Id) + 1 : 1;
                    entity.Name = advisor.Name;

                    // IMPORTANT - Back Fill ID
                    advisor.Id = entity.Id;

                    dc.tblAdvisors.Add(entity);
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
        public static int Update(Advisor advisor, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    // get the row we are trying to update
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == advisor.Id);
                    if (entity != null)
                    {
                        entity.Name = advisor.Name;
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
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        dc.tblAdvisors.Remove(entity);
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
        public static Advisor LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    tblAdvisor entity = dc.tblAdvisors.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        return new Advisor
                        {
                            Id = entity.Id,
                            Name = entity.Name,
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
        public static List<Advisor> Load(int studentId)
        {
            try
            {
                List<Advisor> list = new List<Advisor>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from a in dc.tblAdvisors
                     join sa in dc.tblStudentAdvisors on a.Id equals sa.AdvisorId
                     where sa.StudentId == studentId
                     select new
                     {
                         a.Id,
                         a.Name,
                     })
                     .ToList()
                     .ForEach(advisor => list.Add(new Advisor
                     {
                         Id = advisor.Id,
                         Name = advisor.Name
                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<Advisor> Load()
        {
            try
            {
                List<Advisor> list = new List<Advisor>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblAdvisors
                     select new
                     {
                         s.Id,
                         s.Name
                     })
                     .ToList()
                     .ForEach(advisor => list.Add(new Advisor
                     {
                         Id = advisor.Id,
                         Name = advisor.Name,
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
