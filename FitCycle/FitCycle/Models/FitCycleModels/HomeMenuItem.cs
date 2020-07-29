namespace FitCycle.Models
{
    public enum MenuItemType
    {
        Dashboard,
        Zyklus,
        GewichtsCalc,
        StrengthCalc,
        TopSets,
        WarmUps,
        Übungen,
        Maximalkraftwerte,
        Ziele,
        Chat
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }
    }
}
