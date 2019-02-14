﻿using Microsoft.Maps.MapControl.WPF;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using ProjectTemplate_v2.ViewModels;
using System.Linq;

namespace ProjectTemplate_v2.ViewModels
{
    public partial class PushpinModel : Pushpin
    {
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        
    }

    public class MapViewModel : BaseViewModel
    {
        public Map MapWithMarkers { get; set; }
        private PushpinModel selectedPushpin;
        private ObservableCollection<PushpinModel> pushpins;
        private string currentValue;

        public MapViewModel(ref Sensors sensors)
        {
            this.sensors = sensors;
            MapLayer dataLayer = new MapLayer();
            Pushpins = new ObservableCollection<PushpinModel>();

            InitMap();
        }

        private void PushPinClicked(object obj)
        {
            SelectedPushpin = (PushpinModel)obj;
            //tuk ima nqkvi neshta
        }

        private void InitMap()
        {
            foreach (var sensor in sensors.List)
            {
                PushpinModel pin = new PushpinModel
                {
                    //Location is the field of Pushpin class
                    Location = new Location(sensor.Latitude, sensor.Longitude),
                    //currentvalue??
                    Latitude = sensor.Latitude,
                    Longtitude = sensor.Longitude,
                    Title = sensor.Name.ToString(),
                    Type = sensor.GetType().Name,
                    Description = sensor.Description 
                };

                ToolTipService.SetToolTip(pin, new ToolTip()
                {
                    DataContext = pin,
                    Style = Application.Current.Resources["CustomInfoboxStyle"] as Style
                });

                Pushpins.Add(pin);
                //MapWithMarkers.Children.Add(pin);
            }
        }


        private void ChangeCurrentValue()
        {
            try
            {
                //tuk tarsq current value
            }
            catch
            {
                CurrentValue = "N/A";
            }
        }

        public ObservableCollection<PushpinModel> Pushpins
        {
            get { return pushpins; }
            set
            {
                if (pushpins != value)
                    pushpins = value;
                RaisePropertyChanged("Pushpins");
            }
        }

        public PushpinModel SelectedPushpin
        {
            get { return selectedPushpin; }
            set
            {
                if (selectedPushpin != value)
                {
                    selectedPushpin = value;
                    RaisePropertyChanged("SelectedPushpin");
                }
            }
        }

        public string CurrentValue
        {
            get
            {
                if (currentValue == "true (true/false)")
                    return "Open";
                else if (currentValue == "false (true/false)")
                    return "Closed";
                else
                    return currentValue;
            }
            set
            {
                if (currentValue != value)
                {
                    currentValue = value;
                    RaisePropertyChanged("CurrentValue");
                }
            }
        }
    }
}