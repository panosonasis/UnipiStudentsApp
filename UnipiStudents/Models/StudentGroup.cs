using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipiStudents.Helpers;

namespace UnipiStudents.Models
{
    public class StudentGroup : BindableBase
    {
        private long _groupId;
        public long GroupId
        {
            get { return _groupId; }
            set { SetProperty(ref _groupId, value); }
        }

        private string _groupName;
        public string GroupName
        {
            get { return _groupName; }
            set { SetProperty(ref _groupName, value); }
        }

        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }
    }
}
