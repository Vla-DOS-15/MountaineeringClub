using Microsoft.EntityFrameworkCore;
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

namespace MountaineeringClubWpfApp
{
    /// <summary>
    /// Логика взаимодействия для ListClimbersWindow.xaml
    /// </summary>
    public partial class ListClimbersWindow : Window
    {
        ApplicationContext db;
        public ListClimbersWindow()
        {
            InitializeComponent();
            datePickerStart.SelectedDate = DateTime.Parse("2001-01-01");
            datePickerEnd.SelectedDate = DateTime.Now;
            LoadDb();
        }
        void LoadDb()
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    db.Groups.Load();
                    db.Mountains.Load();
                    db.Mountaineers.Load();
                    db.MountaineeringClubs.Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    db.Groups.Load();
                    db.Mountains.Load();
                    db.Mountaineers.Load();
                    db.MountaineeringClubs.Load();
                    dataGrid.ItemsSource = db.MountaineeringClubs.Include(x => x.Group).Include(x => x.Group.Mountaineers).Where(x => x.IsCarriedOutAscent == true && x.DateBeginningAscent > DateTime.Parse(datePickerStart.Text) && x.DateCompletionAscent < DateTime.Parse(datePickerEnd.Text)).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
