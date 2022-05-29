using hospital.Controller;
using hospital.Model;
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
    /// Interaction logic for HospitalReviewWindow.xaml
    /// </summary>
    public partial class HospitalReviewWindow : Window
    {
        private PollController pollController;
        public HospitalReviewWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pollController = app.pollBlueprintController;
            categoryComboBox.ItemsSource = pollController.GetHospitalPollBlueprint().Categories;
            FillFinalGrade();
        }

        private void FillFinalGrade()
        {
            finalAverage.Content = pollController.CalculateHospitalFinalGrade();
        }
        private void FillCategories(int index)
        {
            category1.Content = pollController.GetHospitalPollBlueprint().Categories[index].Name +"  "+ pollController.CalculateHospitalCategoryGrade(pollController.GetDoctorPollBlueprint().Categories[index].Id);
            //category2.Content = pollController.GetHospitalPollBlueprint().Categories[1].Name + pollController.CalculateHospitalCategoryGrade(pollController.GetDoctorPollBlueprint().Categories[1].Id); ;
            //category3.Content = pollController.GetHospitalPollBlueprint().Categories[2].Name + pollController.CalculateHospitalCategoryGrade(pollController.GetDoctorPollBlueprint().Categories[2].Id); ;
        }

        private void FillFirstCategoryQuestion()
        {
            PollCategory category = pollController.GetHospitalPollBlueprint().Categories[0];
            category1Q1.Text = pollController.GetHospitalPollBlueprint().Categories[0].PollQuestions[0].Question;
            grades1.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetHospitalPollBlueprint().Categories[0].PollQuestions[1].Question;
            grades2.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetHospitalPollBlueprint().Categories[0].PollQuestions[2].Question;
            grades3.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0);
            FillGrades(category, 1);
            FillGrades(category, 2);
        }
        private void FillSecondCategoryQuestion()
        {
            PollCategory category = pollController.GetHospitalPollBlueprint().Categories[1];

            category1Q1.Text = pollController.GetHospitalPollBlueprint().Categories[1].PollQuestions[0].Question;
            grades1.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetHospitalPollBlueprint().Categories[1].PollQuestions[1].Question;
            grades2.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetHospitalPollBlueprint().Categories[1].PollQuestions[2].Question;
            grades3.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0);
            FillGrades(category, 1);
            FillGrades(category, 2);
        }
        private void FillThirdCategoryQuestion()
        {
            PollCategory category = pollController.GetHospitalPollBlueprint().Categories[2];

            category1Q1.Text = pollController.GetHospitalPollBlueprint().Categories[2].PollQuestions[0].Question;
            grades1.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetHospitalPollBlueprint().Categories[2].PollQuestions[1].Question;
            grades2.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetHospitalPollBlueprint().Categories[2].PollQuestions[2].Question;
            grades3.Text = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0);
            FillGrades(category, 1);
            FillGrades(category, 2);
        }

        private void FillGrades(PollCategory category, int question)
        {
            int[] gradesCount = pollController.CountEachHospitalGrade(category.Id, category.PollQuestions[question].Id);
            if (question == 0)
            {
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
        }


        private void Category_Change(object sender, SelectionChangedEventArgs e)
        {
            if (categoryComboBox.SelectedIndex == 0)
            {
                FillFirstCategoryQuestion();
                FillCategories(0);
            }
            else if (categoryComboBox.SelectedIndex == 1) {
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
