
import {
    get,
    put,
    post,
    postFile,
} from './ApiBase.js'

function getTransactions() {
    return get('Transactions')
}

function getTransaction(id) {
    return get(`Transactions/${id}`)
}

function putTransaction(id, data) {
    return put(`Transactions/${id}`, data)
}

function getContacts() {
    return get('Contacts');
}

function postContact(data) {
    return post('Contacts', data);
}

function getReferences() {
    return get('References');
}

function postReference(data) {
    return post('References', data);
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