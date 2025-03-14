import type { TransactionUploadResponse } from '@/types/responses/transaction-upload-response.js'
import {
    get,
    put,
    postFile,
} from './ApiBase.js'

const controllerName =  'Transactions'

function getTransactions() {
    return get(controllerName)
}

function getTransaction(id: number) {
    return get(`${controllerName}/${id}`)
}

function putTransaction(id: number, data: any) {
    return put(`${controllerName}/${id}`, data)
}

function postCsv(file: any): Promise<TransactionUploadResponse> {
    return postFile('Upload', file)
}

export {
    getTransaction,
    getTransactions,
    putTransaction,
    postCsv,
}