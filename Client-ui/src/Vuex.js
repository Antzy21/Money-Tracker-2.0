import Vue from 'vue'
import Vuex from 'vuex'

import {
  healthcheck,
  //posthealthcheck,
} from './api/HealthCheckApi.js'

import {
  getContacts,
  postContact,
  putContact,
} from './api/ContactsApi.js'
import {
  getReferences,
  postReference,
  putReference,
} from './api/ReferencesApi.js'
import {
  getTransactions,
  putTransaction,
  postCsv,
} from './api/TransactionsApi.js'

Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    transactions: [],
    contacts: [],
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
    initialise() {
      healthcheck().then((response) => {
        if (response == undefined) {
          console.error('Health Check Failed');
        }
        else {
          getTransactions().then(data => store.commit('setTransactions', data));
          getContacts().then(data => store.commit('setContacts', data));
          getReferences().then(data => store.commit('setReferences', data));
        }
      });
    },
    updateTransaction(store, transaction) {
      putTransaction(transaction.id, transaction).then(data => {
        console.log('returned value', data);
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
    addReference(store, newReference) {
      postReference(newReference).then(data => {
        store.commit('setReference', data);
      }).catch(error => {
        console.log('caught error')
        return error;
      });      
    },
    linkReferenceToTransaction(store, reference, transaction) {
      reference.transactions.push(transaction);
      this.updateReference(reference).then(()=>{
        const updatedTransaction = { ...this.transaction, 'reference': reference };
        this.updateTransaction(updatedTransaction);
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
    }
  },
})

export default store;