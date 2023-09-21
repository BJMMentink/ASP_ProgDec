using BJM.ProgDec.BL.Models;
using BJM.ProgDec.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BJM.ProgDec.BL
{
    public static class StudenManager
    {
        // by refrence is used by calling "ref" or "out"
        public static int Insert(string firstName, string lastName, string studentId, ref int id, bool roleback = false)
        {
            try
            {
                Student student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    StudentId = studentId
                };
                int results = Insert(student, roleback);
                id = student.Id;
                return results;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public static int Insert(Student student, bool roleback = false)
        {
            try
            {
                int results = 0;
                using(ProgDecEntities dc = new ProgDecEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (roleback) transaction = dc.Database.BeginTransaction();
                    tblStudent entity = new tblStudent();
                    // if ? option 1 : option 2
                    entity.Id = dc.tblStudents.Any() ? dc.tblStudents.Max(s => s.Id) + 1 : 1;
                    entity.FirstName = student.FirstName;
                    entity.LastName = student.LastName;
                    entity.StudentId = student.StudentId;

                    // IMPORTANT - Back Fill ID
                    student.Id = entity.Id;

                    dc.tblStudents.Add(entity);
                    results = dc.SaveChanges();
                    if (roleback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
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
        public static Student LoadById(int id)
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
        public static List<Student> Load()
        {
            try
            {
                List<Student> list = new List<Student>();

                using(ProgDecEntities dc = new ProgDecEntities())
                {
                    (from s in dc.tblStudents
                     select new
                     {
                         s.Id,
                         s.FirstName,
                         s.LastName,
                         s.StudentId
                     })
                     .ToList()
                     .ForEach(student => list.Add(new Student
                     {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        StudentId = student.StudentId
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
