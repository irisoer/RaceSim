using Controller;
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
using System.Windows.Threading;
using WpfRaceSim;

namespace WpfRaceSim
{
   /// <summary>
   /// Interaction logic for CurrentRaceScreen.xaml
   /// </summary>
   public partial class CurrentRaceScreen : Window
   {
      public DataContext dataContext = new DataContext();
      public CurrentRaceScreen()
      {
         Data.Initialize();
         Data.CurrentRace.DriversChanged += OnDriversChangedEventHandlerMethod;
         Data.CurrentRace.RaceChanged += OnRaceChangedEventHandlerMethod;
         InitializeComponent(); 
      }

      public void OnDriversChangedEventHandlerMethod(object? sender, DriversChangedEventArgs e)
      {
         RaceStatsLV.Dispatcher.BeginInvoke(
         DispatcherPriority.Render,
         new Action(() =>
         {
            RaceStatsLV.ItemsSource = null;
            RaceStatsLV.ItemsSource = dataContext.RoundCount;
         }));

         EquipmentStatusLV.Dispatcher.BeginInvoke(
         DispatcherPriority.Render,
         new Action(() =>
         {
            EquipmentStatusLV.ItemsSource = null;
            EquipmentStatusLV.ItemsSource = dataContext.EquipmentStatus;
         }));


      }

      public void OnRaceChangedEventHandlerMethod(Race previous, Race next)
      {
         previous.DriversChanged -= OnDriversChangedEventHandlerMethod;
         next.DriversChanged += OnDriversChangedEventHandlerMethod;
         previous.RaceChanged -= OnRaceChangedEventHandlerMethod;
         next.RaceChanged += OnRaceChangedEventHandlerMethod;
      }

      //private void CurrentRace_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      //{
      //   e.Cancel = true;
      //   this.Hide();
      //}
   }
}
