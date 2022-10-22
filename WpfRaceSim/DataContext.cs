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
      public List<string> RaceStats => Data.CurrentRace.RaceStats.Select(x => $"{x.Key.Name} » Points: {x.Value}").ToList();

      public List<string> RoundCount => Data.CurrentRace.Participants.Select(x => $"{x.Name} round {x.Rounds}/{Data.CurrentRace.Track.Rounds}").ToList();

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
      }
   }
}
