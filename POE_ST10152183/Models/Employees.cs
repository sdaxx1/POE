using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace POE_ST10152183.Models
{
    public class Employees
    {
        public Employees()
        {
        }

        public Employees(int employeeId, string eUserName, string ePassword, string eFirstName, string eLastName, string ePhoneNumber, string eEmailAddress)
        {
            EmployeeId = employeeId;
            EUserName = eUserName;
            EPassword = ePassword;
            EFirstName = eFirstName;
            ELastName = eLastName;
            EPhoneNumber = ePhoneNumber;
            EEmailAddress = eEmailAddress;
        }
        public Employees(int employeeId,string eUserName,string ePassword)
        {
            EmployeeId = employeeId;
            EUserName = eUserName;
            EPassword = ePassword;
        }

        public int EmployeeId{ get; set; }

        
        public string EUserName { get; set; }

        public string EPassword { get; set; }

      
        public string EFirstName { get; set; }

      
        public string ELastName { get; set; }

      
        public string EPhoneNumber { get; set; }

       
        public string EEmailAddress { get; set; }
    }
}
