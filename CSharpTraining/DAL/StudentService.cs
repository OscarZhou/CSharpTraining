﻿using DAL.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class StudentService
    {
        public List<Student> GetListByClassId(string className)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("select * from [dbo].[Students] ");
            sqlBuilder.Append(
                "inner join [dbo].[StudentClass] on [dbo].[Students].ClassId = [dbo].[StudentClass].ClassId ");
            sqlBuilder.Append("where [dbo].[StudentClass].ClassName like '%{0}%'");


            string sql = string.Format(sqlBuilder.ToString(), className);

            try
            {
                List<Student> objStudents = new List<Student>();
                SqlDataReader objReaders = SQLHelper.GetDataReader(sql);
                while (objReaders.Read())
                {
                    objStudents.Add(new Student()
                    {
                        StudentId = Convert.ToInt32(objReaders["StudentId"]),
                        StudentName = objReaders["StudentName"].ToString(),
                        Gender = objReaders["Gender"].ToString(),
                        Birthday = Convert.ToDateTime(objReaders["Birthday"]),
                        StudentIdNo = objReaders["StudentIdNo"].ToString(),
                        CardNo = objReaders["CardNo"].ToString(),
                        PhoneNumber = objReaders["PhoneNumber"].ToString(),
                        StudentAddress = objReaders["StudentAddress"].ToString(),
                        ClassId = Convert.ToInt32(objReaders["ClassId"])
                    });
                }
                objReaders.Close();
                return objStudents;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int InsertStudent(Student objStudent)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(
                "insert into [dbo].[Students] (StudentName, Gender, Birthday, StudentIdNo, CardNo, PhoneNumber, StudentAddress, ClassId) ");
            sqlBuilder.Append("values('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7})");
            string sql = string.Format(sqlBuilder.ToString(), objStudent.StudentName, objStudent.Gender,
                objStudent.Birthday, objStudent.StudentIdNo, objStudent.CardNo, objStudent.PhoneNumber,
                objStudent.StudentAddress, objStudent.ClassId);

            try
            {
                return SQLHelper.Update(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
