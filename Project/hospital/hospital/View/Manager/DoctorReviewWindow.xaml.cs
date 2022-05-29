using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
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
using System.Windows.Shapes;

namespace hospital.View.Manager
{
    /// <summary>
    /// Interaction logic for DoctorReviewWindow.xaml
    /// </summary>
    public partial class DoctorReviewWindow : Window
    {
        private DoctorController doctorController;
        private PollController pollController;
        public DoctorReviewWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pollController = app.pollBlueprintController;
            doctorController = app.doctorController;
            doctorsComboBox.ItemsSource = doctorController.GetDoctors();
            categoryComboBox.ItemsSource = pollController.GetDoctorPollBlueprint().Categories;
        }

        private void Doctor_Changed(object sender, SelectionChangedEventArgs e)
        {
            FillFinalGrade();
            FillCategoryInfo();
            //FillFirstCategoryQuestion();
            //FillSecondCategoryQuestion();
            //FillThirdCategoryQuestion();
        }

        private void FillFinalGrade() {
            finalAverage.Content = pollController.CalculateDoctorFinalGrade(((Doctor)doctorsComboBox.SelectedItem).Username);
        }
        private void FillCategories(int index) {
            category1.Content = pollController.GetDoctorPollBlueprint().Categories[index].Name + "  " + pollController.CalculateCategoryGrade(((Doctor)doctorsComboBox.SelectedItem).Username, pollController.GetDoctorPollBlueprint().Categories[index].Id);
            //category2.Content = pollController.GetDoctorPollBlueprint().Categories[1].Name + pollController.CalculateCategoryGrade(((Doctor)doctorsComboBox.SelectedItem).Username, pollController.GetDoctorPollBlueprint().Categories[1].Id); ;
            //category3.Content = pollController.GetDoctorPollBlueprint().Categories[2].Name + pollController.CalculateCategoryGrade(((Doctor)doctorsComboBox.SelectedItem).Username, pollController.GetDoctorPollBlueprint().Categories[2].Id); ;
        }

        private void FillFirstCategoryQuestion() {
            PollCategory category = pollController.GetDoctorPollBlueprint().Categories[0];
            category1Q1.Text = pollController.GetDoctorPollBlueprint().Categories[0].PollQuestions[0].Question;
            grades1.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetDoctorPollBlueprint().Categories[0].PollQuestions[1].Question;
            grades2.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetDoctorPollBlueprint().Categories[0].PollQuestions[2].Question;
            grades3.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0);
            FillGrades(category, 1);
            FillGrades(category, 2);
        }
        private void FillSecondCategoryQuestion()
        {
            PollCategory category = pollController.GetDoctorPollBlueprint().Categories[1];

            category1Q1.Text = pollController.GetDoctorPollBlueprint().Categories[1].PollQuestions[0].Question;
            grades1.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetDoctorPollBlueprint().Categories[1].PollQuestions[1].Question;
            grades2.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetDoctorPollBlueprint().Categories[1].PollQuestions[2].Question;
            grades3.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0);
            FillGrades(category, 1);
            FillGrades(category, 2);
        }
        private void FillThirdCategoryQuestion()
        {
            PollCategory category = pollController.GetDoctorPollBlueprint().Categories[2];

            category1Q1.Text = pollController.GetDoctorPollBlueprint().Categories[2].PollQuestions[0].Question;
            grades1.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetDoctorPollBlueprint().Categories[2].PollQuestions[1].Question;
            grades2.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetDoctorPollBlueprint().Categories[2].PollQuestions[2].Question;
            grades3.Text = pollController.CalculateDoctorQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0);
            FillGrades(category, 1);
            FillGrades(category, 2);
        }

        private void FillGrades(PollCategory category, int question)
        {
            int[] gradesCount = pollController.CountEachGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[question].Id);
            if (question == 0) {
                g11.Text = gradesCount[0].ToString();
                g12.Text = gradesCount[1].ToString();
                g13.Text = gradesCount[2].ToString();
                g14.Text = gradesCount[3].ToString();
                g15.Text = gradesCount[4].ToString();
            }
            else if (question == 1)
            {
                g21.Text = gradesCount[0].ToString();
                g22.Text = gradesCount[1].ToString();
                g23.Text = gradesCount[2].ToString();
                g24.Text = gradesCount[3].ToString();
                g25.Text = gradesCount[4].ToString();
            }
            else if (question == 2)
            {
                g31.Text = gradesCount[0].ToString();
                g32.Text = gradesCount[1].ToString();
                g33.Text = gradesCount[2].ToString();
                g34.Text = gradesCount[3].ToString();
                g35.Text = gradesCount[4].ToString();
            }
            Console.WriteLine("okida");
        }

        private void Category_Changed(object sender, SelectionChangedEventArgs e)
        {
            FillCategoryInfo();
        }


        private void FillCategoryInfo()
        {
            if (categoryComboBox.SelectedIndex == 0)
            {
                FillFirstCategoryQuestion();
                FillCategories(0);

            }
            else if (categoryComboBox.SelectedIndex == 1)
            {
                FillSecondCategoryQuestion();
                FillCategories(1);

            }
            else if (categoryComboBox.SelectedIndex == 2)
            {
                FillThirdCategoryQuestion();
                FillCategories(2);
            }
        }
    }
}
