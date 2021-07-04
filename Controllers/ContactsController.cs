using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Models;
using MoneyTracker.Models.ViewModels;
using MoneyTracker2.Data.DataAccessLayers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly TransactionRepository _transactionRepository;

        public ContactsController(BankContext context, TransactionRepository transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactView>>> GetContacts()
        {
            return await _context.Contacts.Include(c => c.Transactions)
                .Select(c => new ContactView(c)).ToListAsync();
        }

        // GET: api/Contacts
        [HttpGet("groups")]
        public async Task<ActionResult<IEnumerable<ContactGroupView>>> GetContactGroups()
        {
            return await _context.ContactGroups.Include(cg => cg.Contacts)
                .Select(c => new ContactGroupView(c)).ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactView>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return new ContactView(contact);
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> LinkContact(LinkContactObject linkContact)
        {
            if (linkContact.recordedContact == null || linkContact.contact == null)
            {
                return BadRequest();
            }

            var updatedTransactionIds = await _transactionRepository.LinkContactAsync(linkContact.recordedContact, linkContact.contact);

            var updatedTransactions = await _transactionRepository.GetTransactions(updatedTransactionIds);

            return new JsonResult(updatedTransactions.ToList());
        }
        public class LinkContactObject
        {
            public string recordedContact { get; set; }
            public ContactView contact { get; set; }
        }

        // POST: api/Contacts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
