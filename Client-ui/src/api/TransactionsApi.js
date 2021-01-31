const path = `http://localhost:${port}/api`

import {
    get,
    put,
    post,
    port
} from './ApiBase.js'

function getTransactions() {
    return get(`${path}/Transactions`)
}

function getTransaction(id) {
    return get(`${path}/Transactions/${id}`)
}

function putTransaction(id, data) {
    return put(`${path}/Transactions/${id}`, data)
}

function getContacts() {
    return get(`${path}/Contacts`);
}

function postContact(data) {
    return post(`${path}/Contacts`, data);
}

function getReferences() {
    return get(`${path}/References`);
}

function postReference(data) {
    return post(`${path}/References`, data);
}

export {
    getTransaction,
    getTransactions,
    putTransaction,
    getContacts,
    postContact,
    getReferences,
    postReference,
}