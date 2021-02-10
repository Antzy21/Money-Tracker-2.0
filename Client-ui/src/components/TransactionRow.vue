<template>
  <tr>
      <th>{{transaction.id}}</th>
      <th v-if="!showContactChoices"
        @click="selectContact"
        :class="{ uncategorised: transaction.contact==null }">
        {{contactName}}
      </th>
      <th v-else>
        <select :name="'Contact'" @change="onChangeContact($event)">
          <option value="">{{transaction.recordedContact}}</option>
          <option v-for="contact in contacts"
            :key="contact.id"
            :value="contact.id">
            {{contact.name}}
          </option>
        </select>
      </th>
      <th v-if="!showReferenceChoices"
        @click="selectReference"
        :class="{ uncategorised: transaction.reference==null }">
        {{ReferenceName}}
      </th>
      <th v-else>
        <select :name="'Reference'" @change="onChangeReference($event)">
          <option value="">{{transaction.recordedReference}}</option>
          <option v-for="reference in references"
            :key="reference.id"
            :value="reference.id">
            {{reference.name}}
          </option>
        </select>
      </th>
      <th>{{transaction.amount}}</th>
  </tr>
</template>

<script>
import { mapActions, mapState } from 'vuex'

export default {
  name: 'TransactionRow',
  props: {
    transaction: Object,
  },
  computed: {
    ...mapState(['contacts', 'references']),
    contactName() {
      if (this.transaction.contact) {
        return this.transaction.contact.name;
      }
      return this.transaction.recordedContact;
    },
    ReferenceName() {
      if (this.transaction.reference) {
        return this.transaction.reference.name;
      }
      return this.transaction.recordedReference;
    },
  },
  data() {
    return {
      showContactChoices: false,
      showReferenceChoices: false,
    }
  },
  methods: {
    ...mapActions(['linkReferenceToTransaction']),
    selectContact() {
      this.showContactChoices = !this.showContactChoices && this.transaction.contact == null
    },
    onChangeContact(event) {
      const value = event.target.value;
      console.log(value)
      if (value) {
        const contact = this.contacts.find(o => o.id == value);
        const updatedTransaction = { ...this.transaction, 'contact': contact };
        updatedTransaction.Amount = 10001;
        this.updateTransaction(updatedTransaction);
      }
      this.selectContact();
    },
    selectReference() {
      this.showReferenceChoices = !this.showReferenceChoices && this.transaction.reference == null
    },
    onChangeReference(event) {
      const value = event.target.value;
      console.log(value)
      if (value) {
        const reference = this.references.find(o => o.id == value);
        reference.transactions.push(this.transaction)
        this.linkReferenceToTransaction(reference);
      }
      this.selectReference();
    },
  },
}
</script>

<style scoped>
  .uncategorised {
    color: red;
  }
</style>