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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfRaceSim
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private CurrentCompetitionScreen _currentCompetitionScreen = new CurrentCompetitionScreen();
      private CurrentRaceScreen _currentRaceScreen = new CurrentRaceScreen();
      public MainWindow()
      {
         MainWindowInitialize();
      }

      public void MainWindowInitialize()
      {
         Data.Initialize();
         Data.CurrentRace.RaceChanged += RaceChangedDelegateMethod;
         Data.CurrentRace.DriversChanged += OnDriversChangedEventHandlerMethod;
        
         InitializeComponent();
      }

      public void OnDriversChangedEventHandlerMethod(object? sender, DriversChangedEventArgs e)
      {
         TrackImage.Dispatcher.BeginInvoke(
         DispatcherPriority.Render,
         new Action(() =>
         {
            TrackImage.Source = null;
            TrackImage.Source = WPFVisualisation.DrawTrack(e.Track);
         }));
      }

      public void RaceChangedDelegateMethod(Race previousRace, Race nextRace)
      {
         Images.ClearImageDictionary();
         previousRace.DriversChanged -= OnDriversChangedEventHandlerMethod;
         previousRace.RaceChanged -= RaceChangedDelegateMethod;
         nextRace.DriversChanged += OnDriversChangedEventHandlerMethod;
         nextRace.RaceChanged += RaceChangedDelegateMethod;
      }

      private void MenuItem_Open_CurrentRaceScreen(object sender, RoutedEventArgs e)
      {
         _currentRaceScreen.Show();
      }

      private void MenuItem_Open_CurrentCompetitionScreen(object sender, RoutedEventArgs e)
      {
         _currentCompetitionScreen.Show();
      }

      private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      private void Menu_Click(object sender, RoutedEventArgs e)
      {

      }
   }
}
