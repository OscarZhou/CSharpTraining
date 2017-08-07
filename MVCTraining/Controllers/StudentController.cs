﻿using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCTraining.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["query"] != null)
            {
                List<Student> objStudents = new StudentManage().GetListByClassId(System.Web.HttpContext.Current.Session["query"].ToString());
                ViewBag.stuList = objStudents;
            }
            return View("StudentManage");
        }

        public ActionResult GetList()
        {
            string queryClass = Request.Params["query"].ToString();

            List<Student> objStudents = new StudentManage().GetListByClassId(queryClass);
            ViewBag.className = queryClass;
            ViewBag.stuList = objStudents;

            return View("StudentManage");
        }

        public ActionResult NewStudent()
        {
            List<StudentClass> objStudentClasses = new StudentClassManage().GetAllClasses();
            ViewBag.clsList = objStudentClasses;
            return View("AddStudent");
        }

        public ActionResult AddStudent()
        {
            Student objStudent = new Student()
            {
                StudentName = Request.Params["studentName"],
                Gender = Request.Params["gender"],
                Birthday = Convert.ToDateTime( Request.Params["birthday"]),
                StudentIdNo = Request.Params["studentIdNo"],
                CardNo = Request.Params["cardNo"],
                PhoneNumber = Request.Params["phoneNumber"],
                StudentAddress = Request.Params["studentAddress"],
                ClassId = Convert.ToInt32(Request.Params["ClassId"]) 
            };

            int result = new StudentManage().InsertStudent(objStudent);

            return Content("<script>alert('Add Successfully'); document.location='"+Url.Action("Index", "Student")+"'</script>");

        }

        public ActionResult ViewStudent()
        {
            string studentId = Request.QueryString["stuId"];
            Student objStudent = new StudentManage().GetStudent(studentId);
            ViewData["student"] = objStudent;

            string className = new StudentClassManage().GetClassNameById(objStudent.ClassId.ToString());
            ViewData["className"] = className;

            return View("ViewStudent");

        }

        public ActionResult GetStudent()
        {
            List<StudentClass> objStudentClasses = new StudentClassManage().GetAllClasses();
            ViewBag.clsList = objStudentClasses;

            string studentId = Request.QueryString["stuId"];
            Student objStudent = new StudentManage().GetStudent(studentId);
            return View("EditStudent", objStudent);
        }

        public ActionResult EditStudent()
        {
            Student objStudent = new Student()
            {
                StudentId = Convert.ToInt32(Request.Params["studentId"]),
                StudentName = Request.Params["studentName"],
                Gender = Request.Params["gender"],
                Birthday = Convert.ToDateTime(Request.Params["birthday"]),
                StudentIdNo = Request.Params["studentIdNo"],
                CardNo = Request.Params["cardNo"],
                PhoneNumber = Request.Params["phoneNumber"],
                StudentAddress = Request.Params["studentAddress"],
                ClassId = Convert.ToInt32(Request.Params["classId"])
            };

            int result = new StudentManage().EditStudent(objStudent);

            return Content("<script>alert('Modify Successfully'); document.location='" + Url.Action("Index", "Student") + "'</script>");
        }

        public ActionResult DeleteStudent()
        {
            string studentId = Request.QueryString["stuId"];
            int result = new StudentManage().DeleteStudent(studentId);

            return Content("<script>alert('Delete Successfully'); document.location='" + Url.Action("Index", "Student") + "'</script>");
        }
    }
}