using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipiStudents.Helpers;

namespace UnipiStudents.Models
{
    public class Lab : BindableBase
    {
        private long _labId;
        public long LabId
        {
            get { return _labId; }
            set { SetProperty(ref _labId, value); }
        }

        private string _labName;
        public string LabName
        {
            get { return _labName; }
            set { SetProperty(ref _labName, value); }
        }

        private string _labDesc;
        public string LabDesc
        {
            get { return _labDesc; }
            set { SetProperty(ref _labDesc, value); }
        }

        private long _maximumGrade;
        public long MaximumGrade
        {
            get { return _maximumGrade; }
            set { SetProperty(ref _maximumGrade, value); }
        }

        private long _maximumMembersOfTeam;
        public long MaximumMembersOfTeam
        {
            get { return _maximumMembersOfTeam; }
            set { SetProperty(ref _maximumMembersOfTeam, value); }
        }

        private ObservableCollection<StudentGroup> _studentGroups;
        public ObservableCollection<StudentGroup> StudentGroups
        {
            get { return _studentGroups; }
            set { SetProperty(ref _studentGroups, value); }
        }
    }
}
