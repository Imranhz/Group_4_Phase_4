using Group4Flight.Models.ExtensionMethods;
using Group4Flight.Models.ViewModels;

namespace Group4Flight.Models.Utilities
{
    public class FlightSession
    {
        private readonly ISession _session;
        private const string FilterKey = "FlightFilter";
        private const string SelectionsKey = "FlightSelections";

        public FlightSession(ISession session) => _session = session;

        public FlightViewModel? GetFilter()
            => _session.GetObject<FlightViewModel>(FilterKey);

        public void SetFilter(FlightViewModel vm)
            => _session.SetObject(FilterKey, vm);

        public void ClearFilter()
            => _session.Remove(FilterKey);

        public List<int> GetSelections()
            => _session.GetObject<List<int>>(SelectionsKey) ?? new List<int>();

        public void AddSelection(int flightId)
        {
            var list = GetSelections();
            if (!list.Contains(flightId)) list.Add(flightId);
            _session.SetObject(SelectionsKey, list);
        }

        public void RemoveSelection(int flightId)
        {
            var list = GetSelections();
            list.Remove(flightId);
            _session.SetObject(SelectionsKey, list);
        }

        public void ClearSelections()
            => _session.Remove(SelectionsKey);
    }
}
