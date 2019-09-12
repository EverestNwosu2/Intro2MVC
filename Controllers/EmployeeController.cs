using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Intro2MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HostingEnvironment _hostingEnvironment;
        public EmployeeController(HostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        //Get: Employee/Index
        public IActionResult Index()
        {
            return View();
        }
        
        //Get: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Save(string firstName, string lastName, int age,
                                    string nationalInsuranceNo, string gender,
                                    string address, double monthlySalary)
        {
            //Declare a filestream reference
            FileStream fileStreamEmployee = null;
            //Serrialize employee's data to the stream
            BinaryFormatter binaryFormatterEmp = new BinaryFormatter();
            HashSet<Models.Employee> employeeCollection = new HashSet<Models.Employee>();
            var employeeRecordDir = @"EmployeeFile/employee.emp";
            var webroot = _hostingEnvironment.WebRootPath;
            var path = Path.Combine(webroot, employeeRecordDir);
            if(!string.IsNullOrEmpty(firstName) &&
                !string.IsNullOrEmpty(lastName) &&
                !string.IsNullOrEmpty(nationalInsuranceNo))
                {
                    if(System.IO.File.Exists(path))
                    {
                        using(fileStreamEmployee = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            employeeCollection = (HashSet<Models.Employee>)binaryFormatterEmp.Deserialize(fileStreamEmployee);
                        
                        }
                    }
                    //Create the employee object using the data from the view
                    Models.Employee objEmployee = new Models.Employee()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Age = Convert.ToInt32(age),
                        NationalInsuranceNumber = nationalInsuranceNo,
                        Gender = gender,
                        Address = address,
                        MonthlySalary = Convert.ToDouble(monthlySalary)
                    };
                    //Add Employee obj to list of employee
                    employeeCollection.Add(objEmployee);

                    //Serialize and save to file
                    using(fileStreamEmployee = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        binaryFormatterEmp.Serialize(fileStreamEmployee, employeeCollection);
                        //fileStreamEmployee.Close();
                    }
                    //if employee list has been serialized and saved, return to the create view/form.
                    return RedirectToAction("Create");
                }
                return View();
        }
    }
}