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
    }
}
