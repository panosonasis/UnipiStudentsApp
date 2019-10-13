using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipiStudents.Helpers;

namespace UnipiStudents.Models
{
    public class Student : BindableBase
    {
        private long _studentId;
        public long StudentId
        {
            get { return _studentId; }
            set { SetProperty(ref _studentId, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
    }
}
