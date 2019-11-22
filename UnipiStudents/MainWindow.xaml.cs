using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnipiStudents.Models;
using UnipiStudents.ViewModels;
using UnipiStudents.Views;

namespace UnipiStudents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CoursesTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var thisViewModel = (MainWindowViewModel)DataContext;
            if (thisViewModel != null)
            {
                thisViewModel.SelectedTreeItem = e.NewValue as object;
                if (thisViewModel.SelectedTreeItem.GetType() == typeof(Course))
                {
                    MainContent.ContentTemplate = (DataTemplate)Resources["CourseViewTemplateData"];
                }
                else if (thisViewModel.SelectedTreeItem.GetType() == typeof(Lab))
                {
                    MainContent.ContentTemplate = (DataTemplate)Resources["LabViewTemplateData"];

                }
                else if (thisViewModel.SelectedTreeItem.GetType() == typeof(StudentGroup))
                {
                    MainContent.ContentTemplate = (DataTemplate)Resources["StudentGroupViewTemplateData"];

                }
                else if (thisViewModel.SelectedTreeItem.GetType() == typeof(Student))
                {
                    MainContent.ContentTemplate = (DataTemplate)Resources["StudentViewTemplateData"];
                }
            }
        }

        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            MainContent.ContentTemplate = (DataTemplate)Resources["GroupViewTemplate"];
        }

        private void AddLab_Click(object sender, RoutedEventArgs e)
        {
            MainContent.ContentTemplate = (DataTemplate)Resources["LabViewTemplate"];
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            MainContent.ContentTemplate = (DataTemplate)Resources["CourseViewTemplate"];
        }

        private void StudentList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listview = sender as ListView;
            var selected = listview.SelectedItem;
            var test = FindVisualChildren<StudentView>(this);
            if (selected != null)
            {
                MainContent.ContentTemplate = (DataTemplate)Resources["StudentViewTemplate"];
            }
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
    }
}
