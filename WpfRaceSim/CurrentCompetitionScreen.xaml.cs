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

namespace WpfRaceSim
{
   /// <summary>
   /// Interaction logic for CurrentCompetitionScreen.xaml
   /// </summary>
   public partial class CurrentCompetitionScreen : Window
   {
      public DataContext dataContext = new DataContext();
      public CurrentCompetitionScreen()
      {
         Data.Initialize();
         Data.CurrentRace.DriversChanged += OnDriversChangedEventHandlerMethod;
         Data.CurrentRace.RaceChanged += OnRaceChangedEventHandlerMethod;
         InitializeComponent();
      }

      public void OnDriversChangedEventHandlerMethod(object? sender, DriversChangedEventArgs e)
      {
         CompetitionStatsLV.Dispatcher.BeginInvoke(
         DispatcherPriority.Render,
         new Action(() =>
         {
            CompetitionStatsLV.ItemsSource = null;
            CompetitionStatsLV.ItemsSource = dataContext.CompetitionStats;
         }));




      }

      public void OnRaceChangedEventHandlerMethod(Race previous, Race next)
      {
         previous.DriversChanged -= OnDriversChangedEventHandlerMethod;
         next.DriversChanged += OnDriversChangedEventHandlerMethod;
         previous.RaceChanged -= OnRaceChangedEventHandlerMethod;
         next.RaceChanged += OnRaceChangedEventHandlerMethod;
      }


   }
}
