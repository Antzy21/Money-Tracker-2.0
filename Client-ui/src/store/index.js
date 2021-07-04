import { createStore } from 'vuex'

import {
  healthcheck,
  //posthealthcheck,
} from '../api/HealthCheckApi.js'

import {
  getContacts,
  postContact,
  putContact,
  linkContact,
  getContactGroups,
  postContactGroup,
} from '../api/ContactsApi.js'
import {
  getReferences,
  postReference,
  putReference,
  linkReference,
} from '../api/ReferencesApi.js'
import {
  getTransactions,
  putTransaction,
  postCsv,
} from '../api/TransactionsApi.js'

export default createStore({
  state: {
    transactions: [],
    contacts: [],
    contactGroups: [],
    references: [],
  },
  mutations: {
    setTransactions(state, data) {
      state.transactions = data;
    },
    setTransaction(state, data) {
      const transaction = state.transactions.find(t => t.id === data.id)
      if (transaction) {
        Object.assign(transaction, data);
      } else {
        state.transactions.push(data);
      }
    },
    setContacts(state, data) {
      state.contacts = data;
    },
    setContact(state, data) {
      const contact = state.contacts.find(c => c.id === data.id)
      if (contact) {
        Object.assign(contact, data);
      } else {
        state.contacts.push(data);
      }
    },
    setContactGroups(state, data) {
      state.contactGroups = data;
    },
    setContactGroup(state, data) {
      const contactGroup = state.contactGroups.find(c => c.id === data.id)
      if (contactGroup) {
        Object.assign(contactGroup, data);
      } else {
        state.contactGroups.push(data);
      }
    },
    setReferences(state, data) {
      state.references = data;
    },
    setReference(state, data) {
      const reference = state.references.find(c => c.id === data.id)
      if (reference) {
        Object.assign(reference, data);
      } else {
        state.references.push(data);
      }
    },
  },
  actions: {
    initialise(store) {
      healthcheck().then((response) => {
        if (response == undefined) {
          console.error('Health Check Failed');
        }
        else {
          getTransactions().then(data => store.commit('setTransactions', data));
          getContacts().then(data => store.commit('setContacts', data));
          getContactGroups().then(data => store.commit('setContactGroups', data));
          getReferences().then(data => store.commit('setReferences', data));
        }
      });
    },
    updateTransaction(store, transaction) {
      putTransaction(transaction.id, transaction).then(data => {
        store.commit('setTransaction', data);
      }).catch((error) => {
        console.log('caught error')
        return error;
      });
    },
    updateContact(store, contact) {
      putContact(contact.id, contact).then(data => {
        store.commit('setContact', data);
      }).catch((error) => {
        console.log('caught error')
        return error;
      });
    },
    updateReference(store, reference) {
      putReference(reference.id, reference).then(data => {
        store.commit('setReference', data);
      }).catch((error) => {
        console.log('caught error')
        return error;
      });
    },
    addContact(store, newContact) {
      postContact(newContact).then(data => {
        store.commit('setContact', data);
      }).catch(error => {
        console.log('caught error')
        return error;
      });
    },
    addContactGroup(store, newContactGroup) {
      postContactGroup(newContactGroup).then(data => {
        store.commit('setContactGroup', data);
      }).catch(error => {
        console.log('caught error')
        return error;
      });
    },
    addReference(store, newReference) {
      postReference(newReference).then(data => {
        store.commit('setReference', data);
      }).catch(error => {
        console.log('caught error')
        return error;
      });      
    },
    linkReferenceToTransaction(store, { recordedReference, reference }) {
        linkReference(recordedReference, reference).then((updatedTransactions)=>{
        updatedTransactions.forEach(updatedTransactions => {
          store.commit('setTransaction', updatedTransactions);
        });
      }).catch(error => {
        console.log('caught error')
        return error;
      });
    },
    linkContactToTransaction(store, { recordedContact, contact }) {
        linkContact(recordedContact, contact).then((updatedTransactions)=>{
        updatedTransactions.forEach(updatedTransactions => {
          store.commit('setTransaction', updatedTransactions);
        });
      }).catch(error => {
        console.log('caught error')
        return error;
      });
    },
    uploadCsv(store, file) {
      return postCsv(file).then((newlyAddedTransactions) => {
        newlyAddedTransactions.forEach(newlyAddedTransaction => {
          store.commit('setTransaction', newlyAddedTransaction);
        });
        return newlyAddedTransactions.length;
      }).catch(error => {
        console.log('caught error')
        return error;
      });
    },
  },
  modules: {
  }
})