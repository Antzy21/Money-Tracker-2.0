import Vue from 'vue'
import Vuex from 'vuex'

import {
  getContacts,
  getReferences,
  getTransactions,
  putTransaction,
  postContact,
  putContact,
  postReference,
  putReference,
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
      getTransactions().then(data => store.commit('setTransactions', data));
      getContacts().then(data => store.commit('setContacts', data));
      getReferences().then(data => store.commit('setReferences', data));
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
      });
    },
    addReference(store, newReference) {
      postReference(newReference).then(data => {
        store.commit('setReference', data);
      });      
    },
    linkReferenceToTransaction(store, reference, transaction) {
      reference.transactions.push(transaction);
      this.updateReference(reference).then(()=>{
        const updatedTransaction = { ...this.transaction, 'reference': reference };
        this.updateTransaction(updatedTransaction);
      });
    },
    uploadCsv(store, file) {
      postCsv(file).then(() => {
        console.log('posted csv')
      });
    }
  },
})

export default store;