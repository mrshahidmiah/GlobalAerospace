using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace GlobalAeroTechnicalTest.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Airport : ObservableObject
    {
        public string icao { get; set; }
        public string iata { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string elevation_ft { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string timezone { get; set; }
        
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}