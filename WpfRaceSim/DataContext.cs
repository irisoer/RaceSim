using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WpfRaceSim
{
   public class DataContext : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler? PropertyChanged;
      public string TrackName => Data.CurrentRace.Track.Name;

      public DataContext()
      {
         Data.CurrentRace.DriversChanged += DriversChangedEventMethod;
         Data.CurrentRace.RaceChanged += RaceChangedEventMethod;
      }

      public void DriversChangedEventMethod(object? s, DriversChangedEventArgs e)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
      }

      public void RaceChangedEventMethod(Race previousRace, Race nextRace)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
         System.Diagnostics.Debug.WriteLine(TrackName);
      }
   }
}
