using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
      private CurrentCompetitionScreen _currentCompetitionScreen;
      private CurrentRaceScreen _currentRaceScreen;
      public string CompetitionWinner => $"{Data.Competition.Winner}";
      public MainWindow()
      {
         Data.Initialize();        
         _currentRaceScreen = new CurrentRaceScreen();
         _currentCompetitionScreen = new CurrentCompetitionScreen();

         Data.CurrentRace.DriversChanged += OnDriversChangedEventHandlerMethod;
         Data.CurrentRace.RaceChanged += RaceChangedDelegateMethod;
         Data.CurrentRace.Start();

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
         ImagesGenerator.ClearImageDictionary();
         previousRace.Cleanup();
         nextRace.DriversChanged += OnDriversChangedEventHandlerMethod;
         nextRace.RaceChanged += RaceChangedDelegateMethod;
         if(Data.RacesOver == true)
         {
            MessageBox.Show(CompetitionWinner, "Winner");
         }
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

      private void Main_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         e.Cancel = true;
         Application.Current.Shutdown();
      }
   }
}
