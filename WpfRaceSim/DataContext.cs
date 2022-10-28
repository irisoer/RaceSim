using Model;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;

namespace WpfRaceSim
{
   public class DataContext : INotifyCollectionChanged, INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler? PropertyChanged;
      public event NotifyCollectionChangedEventHandler? CollectionChanged;
      public string TrackName => $"Current track {Data.CurrentRace.Track.Name}";
      public string NextTrackName => $"The next track in this competition is {Data.PeekInTrackQueue()}";
      public int TrackRounds => Data.CurrentRace.Track.Rounds;
      public int TracksToGo => Data.Competition.Tracks.Count;
      public string CompetitionWinner = Data.Competition.Winner;
      public string TracksToGoText => $"There are {TracksToGo} tracks left in this competition";
      public List<string> CompetitionStats => Data.Competition.Participants.Select(x => $"{x.Name} (team {x.TeamColor.ToString()}) » Points: {x.Points}").ToList();
      public List<string> RoundCount => Data.CurrentRace.Participants.Select(x => $"{x.Name} (team {x.TeamColor.ToString()}) has finished {x.Rounds} of {TrackRounds} rounds").ToList();
      public List<string> EquipmentStatus => Data.CurrentRace.Participants.Select(x => $"{x.Name} » Equipment: {x.Equipment.EquipmentStatus}").ToList();


      public DataContext()
      {
         Data.CurrentRace.DriversChanged += DriversChangedEventMethod;
         Data.CurrentRace.RaceChanged += RaceChangedEventMethod;
      }
      public void DriversChangedEventMethod(object? s, DriversChangedEventArgs e)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
         CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Reset));
      }

      public void RaceChangedEventMethod(Race previousRace, Race nextRace)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
         CollectionChanged?.Invoke(this,new(NotifyCollectionChangedAction.Reset));
      }

      public string DriversFinishedConverter()
      {
         int driversFinishedNow = 0;
         foreach(var participant in Data.CurrentRace.Participants)
         {
            if(participant.IsFinished == true)
            {
               driversFinishedNow++;
            }
         }
         return driversFinishedNow.ToString();
      }
   }
}
