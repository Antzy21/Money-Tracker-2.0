import { createStore } from 'vuex'

import {
  healthcheck,
  //posthealthcheck,
} from '../api/HealthCheckApi.js'

import {
  getCategories,
  postCategory,
  putCategory,
  linkCategory,
} from '../api/CategoriesApi.js'
import {
  getTransactions,
  putTransaction,
  postCsv,
} from '../api/TransactionsApi.js'

export default createStore({
  state: {
    transactions: [],
    categories: [],
    categoryGroups: [],
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
    setCategories(state, data) {
      state.categories = data;
    },
    setCategory(state, data) {
      const category = state.categories.find(c => c.id === data.id)
      if (category) {
        Object.assign(category, data);
      } else {
        state.categories.push(data);
      }
    },
    setCategoryGroups(state, data) {
      state.categoryGroups = data;
    },
    setCategoryGroup(state, data) {
      const categoryGroup = state.categoryGroups.find(c => c.id === data.id)
      if (categoryGroup) {
        Object.assign(categoryGroup, data);
      } else {
        state.categoryGroups.push(data);
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
          getCategories().then(data => store.commit('setCategories', data));
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
    updateCategory(store, category) {
      putCategory(category.id, category).then(data => {
        store.commit('setCategory', data);
      }).catch((error) => {
        console.log('caught error')
        return error;
      });
    },
    addCategory(store, newCategory) {
      postCategory(newCategory).then(data => {
        debugger;
        store.commit('setCategory', data);
      }).catch(error => {
        console.log('caught error')
        return error;
      });
    },
    linkCategoryToTransaction(store, { Category, category }) {
        linkCategory(Category, category).then((updatedTransactions)=>{
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