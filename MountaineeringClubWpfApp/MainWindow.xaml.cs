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
using Microsoft.EntityFrameworkCore;
using MountaineeringClubWpfApp.Models;
namespace MountaineeringClubWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Initialize();

                LoadDb();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    var z = db.MountaineeringClubs.Where(x=>x.Mountain.NameMountain == "Еверест");
                    string nameMountain = "";
                    int countMountaineer = 0;
                    foreach (var i in z) { 
                       // i.Mountain.NameMountain
                       countMountaineer++;
                    }
                    foreach (var i in z)
                    {
                        MessageBox.Show($"{i.Mountain.NameMountain} - {countMountaineer}");
                        break;
                    }
                    var c = db.MountaineeringClubs.Include(x => x.Mountain).Include(x => x.Group.Mountaineers).Count(x => x.IdMountaineeringClub == 5);
                    MessageBox.Show(c.ToString());
                    //dataGrid.ItemsSource = с;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Initialize()
        {
            using (db = new ApplicationContext())
            {

                if (!db.MountaineeringClubs.Any())
                {

                    var group1 = new Group
                    {
                        NameGroup = "Група 1"
                        //MountaineerId = mountaineer.IdMountaineer
                    };
                    var group2 = new Group
                    {
                        NameGroup = "Група 2"
                        //MountaineerId = mountaineer.IdMountaineer
                    };
                    db.Groups.AddRange(group1, group2);
                    db.SaveChanges();
                    var mountaineer = new Mountaineer
                    {
                        FullNameMountaineer = "Вася",
                        GroupId = group1.IdGroup
                    };
                    var mountaineer1 = new Mountaineer
                    {
                        FullNameMountaineer = "Олег",
                        GroupId = group2.IdGroup
                    };
                    db.Mountaineers.AddRange(mountaineer, mountaineer1);
                    db.SaveChanges();
                    var mountain = new Mountain
                    {
                        NameMountain = "Еверест",
                        HeightMountain = "8848",
                        Country = "Непал",
                        Region = "Сауз Кол"
                    };
                    db.Mountains.Add(mountain);
                    db.SaveChanges();
                    var mountaineeringClub = new MountaineeringClub
                    {
                        GroupId = group1.IdGroup,
                        MountainId = mountain.IdMountain,
                        DateBeginningAscent = DateTime.Now,
                        DateCompletionAscent = DateTime.Now,
                        IsCarriedOutAscent = true
                    };
                    db.MountaineeringClubs.Add(mountaineeringClub);
                    db.SaveChanges();
                }
            }
        }
        private void btn_UpdateClient_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_DeleteClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnListGroup_Click(object sender, RoutedEventArgs e)
        {
            ListGroupWindow lgw = new ListGroupWindow();
            lgw.Show();
        }

        private void btnShowListClimbers_Click(object sender, RoutedEventArgs e)
        {
            ListClimbersWindow lcw = new ListClimbersWindow();
            lcw.Show();
        }
    }
}
