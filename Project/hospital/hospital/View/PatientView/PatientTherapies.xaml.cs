using Controller;
using hospital.DTO;
using hospital.View.PatientView.ViewModel;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientTherapies.xaml
    /// </summary>
    public partial class PatientTherapies : Page
    {
        public PatientTherapies()
        {
            InitializeComponent();
            DataContext = new TherapyViewModel();
        }
    }
}
