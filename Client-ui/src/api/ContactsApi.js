import {
    get,
    post,
} from './ApiBase.js'

const controllerName =  'Contacts'

function getContacts() {
    return get(controllerName);
}

function postContact(data) {
    return post(controllerName, data);
}

export {
    getContacts,
    postContact,
}