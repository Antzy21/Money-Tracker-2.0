using System.Collections.Generic;
using System.Linq;

namespace MoneyTracker.Models.ViewModels
{
    public class ContactGroupView
    {
        public ContactGroupView(ContactGroup contactGroup)
        {
            Id = contactGroup.Id;
            Name = contactGroup.Name;
            Colour = contactGroup.Colour;
            Contacts = contactGroup.Contacts.Select(c => new ContactView(c)).ToList();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public List<ContactView> Contacts { get; set; }
    }
}
