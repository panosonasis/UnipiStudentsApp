using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipiStudents.Helpers;

namespace UnipiStudents.Models
{
    public class Course : BindableBase
    {
        private long _courseId;
        public long CourseId
        {
            get { return _courseId; }
            set { SetProperty(ref _courseId, value); }
        }

        private string _courseName;
        public string CourseName
        {
            get { return _courseName; }
            set { SetProperty(ref _courseName, value); }
        }

        private string _semester;
        public string Semester
        {
            get { return _semester; }
            set { SetProperty(ref _semester, value); }
        }

        private string _courseDesc;
        public string CourseDesc
        {
            get { return _courseDesc; }
            set { SetProperty(ref _courseDesc, value); }
        }

        private string _professorName;
        public string ProfessorName
        {
            get { return _professorName; }
            set { SetProperty(ref _professorName, value); }
        }

        private ObservableCollection<Lab> _labs;
        public ObservableCollection<Lab> Labs
        {
            get { return _labs; }
            set { SetProperty(ref _labs, value); }
        }
    }
}
