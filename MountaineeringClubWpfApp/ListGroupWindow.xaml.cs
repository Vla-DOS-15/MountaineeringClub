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
    /// Логика взаимодействия для ListGroupWindow.xaml
    /// </summary>
    public partial class ListGroupWindow : Window
    {
        ApplicationContext db;

        public ListGroupWindow()
        {
            InitializeComponent();
            datePickergStart.SelectedDate = DateTime.Parse("2001-01-01");
            datePickergEnd.SelectedDate = DateTime.Now;
            checkBoxCheck(checkBox);
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

        private void btn_UpdateClient_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowListGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowListGroup_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    db.Groups.Load();
                    db.Mountains.Load();
                    db.Mountaineers.Load();
                    db.MountaineeringClubs.Load();
                    if (checkBox.IsChecked == true)
                    { 
                        dataGrid.ItemsSource = db.MountaineeringClubs.Include(x => x.Group).Where(x => x.IsCarriedOutAscent == true && x.DateBeginningAscent > DateTime.Parse(datePickergStart.Text) && x.DateBeginningAscent < DateTime.Parse(datePickergEnd.Text)).OrderBy(x => x.DateBeginningAscent).ToList();
                    }
                    else
                    {
                        dataGrid.ItemsSource = db.MountaineeringClubs.Include(x => x.Group).Where(x => x.IsCarriedOutAscent == false && x.DateBeginningAscent > DateTime.Parse(datePickergStart.Text)).OrderBy(x => x.DateBeginningAscent).ToList();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void checkBoxCheck(CheckBox checkBox)
        {
            if (checkBox.IsChecked == false)
            {
                datePickergEnd.IsEnabled = false;
            }

            else
            {
                datePickergEnd.IsEnabled = true;
            }
        }
        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            checkBoxCheck(checkBox);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
