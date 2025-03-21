﻿using BJM.ProgDec.BL.Models;
using BJM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace BJM.ProgDec.BL
{
    public static class ProgramManager
    {
        // by refrence is used by calling "ref" or "out"
        public static int Insert(string description, int degreeTypeId, ref int id, string imagePath, bool roleback = false)
        {
            try
            {
                Program program = new Program
                {
                    Description = description,
                    DegreeTypeId = degreeTypeId,
                    ImagePath = imagePath
                };
                int results = Insert(program, roleback);
                id = program.Id;
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static int Insert(Program program, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblProgram entity = new tblProgram();
                    // if ? option 1 : option 2
                    entity.Id = dc.tblPrograms.Any() ? dc.tblPrograms.Max(s => s.Id) + 1 : 1;
                    entity.Description = program.Description;
                    entity.DegreeTypeId = program.DegreeTypeId;
                    entity.ImagePath = program.ImagePath;

                    // IMPORTANT - Back Fill ID
                    program.Id = entity.Id;

                    dc.tblPrograms.Add(entity);
                    results = dc.SaveChanges();
                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Program program, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    // get the row we are trying to update
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(s => s.Id == program.Id);
                    if (entity != null)
                    {
                        entity.Description = program.Description;
                        entity.DegreeTypeId = program.DegreeTypeId;
                        entity.ImagePath = program.ImagePath;
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
                    tblProgram entity = dc.tblPrograms.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        dc.tblPrograms.Remove(entity);
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
        public static Program LoadById(int id)
        {
            try
            {
                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    var entity = (from s in dc.tblPrograms
                                  join dt in dc.tblDegreeTypes on s.DegreeTypeId equals dt.Id
                                  where s.Id == id
                                  select new
                                  {
                                      s.Id,
                                      s.Description,
                                      s.DegreeTypeId,
                                      DegreeTypeName = dt.Description,
                                      s.ImagePath
                                  })
                                .FirstOrDefault();

                    if (entity != null)
                    {
                        return new Program
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            DegreeTypeId = entity.DegreeTypeId,
                            DegreeTypeName = entity.DegreeTypeName,
                            ImagePath = entity.ImagePath
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
        public static List<Program> Load(int? DegreeTypeId = null)
        {
            try
            {
                List<Program> list = new List<Program>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblPrograms
                     join dt in dc.tblDegreeTypes on s.DegreeTypeId equals dt.Id
                     where s.DegreeTypeId == DegreeTypeId || DegreeTypeId == null
                     select new
                     {
                         s.Id,
                         s.Description,
                         s.DegreeTypeId,
                         DegreeTypeName = dt.Description,
                         s.ImagePath
                     })
                     .ToList()
                     .ForEach(program => list.Add(new Program
                     {
                         Id = program.Id,
                         Description = program.Description,
                         DegreeTypeId = program.DegreeTypeId,
                         DegreeTypeName = program.DegreeTypeName,
                         ImagePath = program.ImagePath
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
