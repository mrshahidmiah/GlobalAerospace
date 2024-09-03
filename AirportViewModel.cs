using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using GlobalAeroTechnicalTest.Model;
using GlobalAeroTechnicalTest.Data;

namespace GlobalAeroTechnicalTest.ViewModel
{
    public partial class AirportViewModel : ObservableObject
    {
        public ObservableCollection<Airport> Airports { get; } = [];
        public ObservableCollection<Airport> SelectedAirports { get; } = [];

        private AirportDatabase _airportDatabase;

        private ObservableCollection<Airport> _allAirports = [];

        [ObservableProperty]
        private Airport filteredAirport;

        [ObservableProperty]
        private Airport selectedAirport;

        [ObservableProperty]
        private string filterText;


        public AirportViewModel(AirportDatabase airportDatabase)
        {
            _airportDatabase = airportDatabase;

            // get list of airports
            IList<Airport> airports = _airportDatabase.GetByCountry("GB");

            _allAirports.Clear();
            Airports.Clear();

            if (airports.Count > 0)
            {
                foreach (var airport in airports)
                {
                    airport.PropertyChanged += Airport_PropertyChanged;
                    _allAirports.Add(airport);
                    Airports.Add(airport);
                }
            }
        }

        partial void OnFilterTextChanged(string value)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            // consider refactoring??

            Airports.Clear();

            // no filter, so show all airports
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                foreach (var airport in _allAirports)
                {
                    Airports.Add(airport);
                }
            }
            else
            {
                // apply filter
                var filterAirports = _allAirports
                    .Where(a => a.name.Contains(FilterText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var airport in filterAirports)
                {
                    Airports.Add(airport);
                }
            }
        }

        private void Airport_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Airport.IsSelected))
            {
                var airport = sender as Airport;
                if (airport.IsSelected && !SelectedAirports.Contains(airport))
                {
                    SelectedAirports.Add(airport);
                }
                else if (!airport.IsSelected && SelectedAirports.Contains(airport))
                {
                    SelectedAirports.Remove(airport);
                }
            }
        }
    }
}