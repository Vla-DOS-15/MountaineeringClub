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
                    var c = db.MountaineeringClubs.Include(x => x.Mountain).Include(x => x.Group.Mountaineer).Count(x => x.IdMountaineeringClub == 5);
                    MessageBox.Show(c.ToString());
                    dataGrid.ItemsSource = db.MountaineeringClubs.Local.ToBindingList();
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
                    };
                    var group2 = new Group
                    {
                        NameGroup = "Група 2"
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

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    var group1 = new Group
                    {
                        NameGroup = tb_NameGroup.Text
                    };
                    db.Groups.Add(group1);
                    db.SaveChanges();
                    var mountaineer = new Mountaineer
                    {
                        FullNameMountaineer = tb_FullNameMountaineer.Text,
                        GroupId = group1.IdGroup
                    };
                    db.Mountaineers.Add(mountaineer);
                    db.SaveChanges();
                    var mountain = new Mountain
                    {
                        NameMountain = tb_NameMountain.Text,
                        HeightMountain = tb_HeightMountain.Text,
                        Country = tb_Country.Text,
                        Region = tb_Region.Text
                    };
                    db.Mountains.Add(mountain);
                    db.SaveChanges();
                    var mountaineeringClub = new MountaineeringClub
                    {
                        GroupId = group1.IdGroup,
                        MountainId = mountain.IdMountain,
                        DateBeginningAscent = DateTime.Parse(datePickerDateBeginningAscent.Text),
                        DateCompletionAscent = DateTime.Parse("0001-01-01"),
                        IsCarriedOutAscent = false
                    };
                    db.MountaineeringClubs.Add(mountaineeringClub);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    var del = new MountaineeringClub() { IdMountaineeringClub = int.Parse(tb_Del.Text) };
                    db.MountaineeringClubs.Attach(del);
                    db.MountaineeringClubs.Remove(del);
                    db.SaveChanges();
                    LoadDb();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (db = new ApplicationContext())
                {
                    int IdMountaineeringClub = (dataGrid.SelectedItem as MountaineeringClub).IdMountaineeringClub;
                    int IdGroup = (dataGrid.SelectedItem as MountaineeringClub).Group.IdGroup;
                    int IdMountain = (dataGrid.SelectedItem as MountaineeringClub).Mountain.IdMountain;
                    int IdMountaineer = (dataGrid.SelectedItem as MountaineeringClub).Group.Mountaineer.IdMountaineer;

                    MountaineeringClub updateMountaineeringClub = (from d in db.MountaineeringClubs
                                             where d.IdMountaineeringClub == IdMountaineeringClub
                                                     select d).Single();

                    Group updateGroup = (from d in db.Groups
                                                 where d.IdGroup == IdGroup
                                             select d).Single();

                    Mountain updateMountain = (from d in db.Mountains
                                             where d.IdMountain == IdMountain
                                               select d).Single();

                    Mountaineer updateMountaineer = (from d in db.Mountaineers
                                                     where d.IdMountaineer == IdMountaineer
                                                     select d).Single();

                    updateMountaineeringClub.DateBeginningAscent = (dataGrid.SelectedItem as MountaineeringClub).DateBeginningAscent;
                    updateMountaineeringClub.DateCompletionAscent = (dataGrid.SelectedItem as MountaineeringClub).DateCompletionAscent;
                    updateMountaineeringClub.IsCarriedOutAscent = (dataGrid.SelectedItem as MountaineeringClub).IsCarriedOutAscent;

                    updateGroup.NameGroup = (dataGrid.SelectedItem as MountaineeringClub).Group.NameGroup;

                    updateMountain.NameMountain = (dataGrid.SelectedItem as MountaineeringClub).Mountain.NameMountain;
                    updateMountain.HeightMountain = (dataGrid.SelectedItem as MountaineeringClub).Mountain.HeightMountain;
                    updateMountain.Country = (dataGrid.SelectedItem as MountaineeringClub).Mountain.Country;
                    updateMountain.Region = (dataGrid.SelectedItem as MountaineeringClub).Mountain.Region;

                    updateMountaineer.FullNameMountaineer = (dataGrid.SelectedItem as MountaineeringClub).Group.Mountaineer.FullNameMountaineer;
                    db.SaveChanges();
                    LoadDb();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
