namespace MoneyTracker.Models
{
    public class ReferenceView
    {
        public ReferenceView(Reference reference)
        {
            Id = reference.Id;
            Name = reference.Name;
            Colour = reference.Colour;
            ReferenceGroupName = reference.ReferenceGroup?.Name ?? "None";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string ReferenceGroupName { get; set; }

    }
}