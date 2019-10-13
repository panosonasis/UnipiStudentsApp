using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnipiStudents.Helpers;
using UnipiStudents.Models;

namespace UnipiStudents.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Properties

        private ObservableCollection<Course> _coursesCollection;
        public ObservableCollection<Course> CoursesCollection
        {
            get { return _coursesCollection; }
            set { SetProperty(ref _coursesCollection, value); }
        }

        private ObservableCollection<Student> _studentCollection;
        public ObservableCollection<Student> StudentCollection
        {
            get { return _studentCollection; }
            set { SetProperty(ref _studentCollection, value); }
        }

        private object _selectedTreeItem;
        public object SelectedTreeItem
        {
            get { return _selectedTreeItem; }
            set { SetProperty(ref _selectedTreeItem, value); }
        }

        private TypeOfView _typeOfViewDisplayed;
        public TypeOfView TypeOfViewDisplayed
        {
            get { return _typeOfViewDisplayed; }
            set { SetProperty(ref _typeOfViewDisplayed, value); }
        }

        private CourseViewModel _courseViewModel;
        public CourseViewModel CourseViewModel
        {
            get { return _courseViewModel; }
            set { SetProperty(ref _courseViewModel, value); }
        }

        private LabViewModel _labViewModel;
        public LabViewModel LabViewModel
        {
            get { return _labViewModel; }
            set { SetProperty(ref _labViewModel, value); }
        }

        private GroupViewModel _groupViewModel;
        public GroupViewModel GroupViewModel
        {
            get { return _groupViewModel; }
            set { SetProperty(ref _groupViewModel, value); }
        }

        private StudentViewModel _studentViewModel;
        public StudentViewModel StudentViewModel
        {
            get { return _studentViewModel; }
            set { SetProperty(ref _studentViewModel, value); }
        }
        #endregion
        #region Commands
        public RelayCommand AddCourseCommand { get; set; }
        public RelayCommand AddLabCommand { get; set; }
        public RelayCommand AddGroupCommand { get; set; }

        #endregion

        public MainWindowViewModel()
        {
            CourseViewModel = new CourseViewModel();
            LabViewModel = new LabViewModel();
            GroupViewModel = new GroupViewModel();
            StudentViewModel = new StudentViewModel();
            AddCourseCommand = new RelayCommand(AddCourse);
            AddLabCommand = new RelayCommand(AddLab);
            AddGroupCommand = new RelayCommand(AddGroup);

            CoursesCollection = new ObservableCollection<Course>(MockData());

            TypeOfViewDisplayed = TypeOfView.LabView;
        }

        private void AddGroup()
        {
            TypeOfViewDisplayed = TypeOfView.GroupView;
        }

        private void AddLab()
        {
            TypeOfViewDisplayed = TypeOfView.LabView;
        }

        private void AddCourse()
        {
            TypeOfViewDisplayed = TypeOfView.CourseView;
        }

        //Mock Data for Tree
        private List<Course> MockData()
        {
            //Students
            Student student1 = new Student() { StudentId = 1, FirstName = "Panos", LastName = "Onasis" };
            Student student2 = new Student() { StudentId = 2, FirstName = "Nikos", LastName = "Mpenakis" };
            Student student3 = new Student() { StudentId = 3, FirstName = "Manos", LastName = "Geller" };
            Student student4 = new Student() { StudentId = 4, FirstName = "Kostas", LastName = "Pappadopoulos" };

            //Groups
            StudentGroup group1 = new StudentGroup() {GroupId = 1 ,GroupName = "Group 1" , Students = new ObservableCollection<Student>() { student1, student2 } };
            StudentGroup group2 = new StudentGroup() { GroupId = 2, GroupName = "Group 2", Students = new ObservableCollection<Student>() { student3, student4 } };

            //Labs
            Lab lab1 = new Lab() {LabId = 1,LabName = "Lab Lesson 1",LabDesc = "This is the first laboratory lesson",MaximumMembersOfTeam = 5, MaximumGrade = 2,StudentGroups = new ObservableCollection<StudentGroup>() { group1, group2 } };
            Lab lab2 = new Lab() { LabId = 2, LabName = "Lab Lesson 2", LabDesc = "This is the second laboratory lesson", MaximumMembersOfTeam = 3, MaximumGrade = 1, StudentGroups = new ObservableCollection<StudentGroup>() { group1 } };

            //Courses
            Course course1 = new Course() {CourseId = 1,CourseName = "Advanced Topics of Object - Oriented Programming (Java)",CourseDesc="",ProfessorName="Professor1",Semester ="A",Labs = new ObservableCollection<Lab>() { lab1, lab2 } };
            Course course2 = new Course() { CourseId = 1, CourseName = "Algorithmic Techniques and Applications", CourseDesc = "", ProfessorName = "Professor2", Semester = "A", Labs = new ObservableCollection<Lab>() { lab1} };

            List<Course> tempCourses = new List<Course>() {course1,course2 };
            StudentCollection = new ObservableCollection<Student>() {student1,student2,student3,student4 };

            return tempCourses;
        }
    }
}
