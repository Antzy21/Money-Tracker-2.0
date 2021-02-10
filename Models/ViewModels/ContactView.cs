namespace MoneyTracker.Models
{
    public class ContactView
    {
        public ContactView(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            Colour = contact.Colour;
            ContactGroupName = contact.ContactGroup?.Name ?? "None";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string ContactGroupName { get; set; }
    }
}