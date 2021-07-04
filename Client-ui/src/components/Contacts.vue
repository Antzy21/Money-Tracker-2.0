<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <h1>Contacts</h1>
        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Group</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="contact in contacts" :key="contact.id">
              <td>
                {{contact.id}}
              </td>
              <td>
                {{contact.name}}
              </td>
              <td>
                {{contact.contactGroup}}
              </td>
            </tr>
            <tr>
              <td>
                <button @click="onAddContact">
                  +
                </button>
              </td>
              <td>
                <input v-model="newContact.name">
              </td>
              <td>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <ContactGroupsTable />
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import ContactGroupsTable from './contactReferences/ContactGroupsTable.vue'

export default {
  name: 'Contacts',
  components: {
    ContactGroupsTable
  },
  computed: {
    ...mapState(['contacts', 'contactGroups']),
  },
  data() {
    return {
      newContact: {
        name: ''
      }
    }
  },
  methods: {
    ...mapActions(['addContact']),
    onAddContact() {
      this.addContact(this.newContact);
      this.resetNewContact();
    },
    resetNewContact() {
      this.newContact = {
        name: ''
      }
    },
  }
}
</script>