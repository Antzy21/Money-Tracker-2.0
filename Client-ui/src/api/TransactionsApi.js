import {
    get,
    put,
    postFile,
} from './ApiBase.js'

const controllerName =  'Transactions'

function getTransactions() {
    return get(controllerName)
}

function getTransaction(id) {
    return get(`${controllerName}/${id}`)
}

function putTransaction(id, data) {
    return put(`${controllerName}/${id}`, data)
}

function postCsv(file) {
    return postFile('Upload', file)
}

export {
    getTransaction,
    getTransactions,
    putTransaction,
    postCsv,
}