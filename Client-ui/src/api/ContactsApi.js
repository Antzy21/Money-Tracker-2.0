import {
    get,
    post,
    put,
} from './ApiBase.js'

const controllerName =  'Contacts'

function getContacts() {
    return get(controllerName);
}
function putContact() {
    console.error('Not Implimented Exception');
    return;
}
function postContact(data) {
    return post(controllerName, data);
}

function linkContact(recordedContact, contact) {
    return put(controllerName, {'recordedContact': recordedContact, 'contact': contact})
}

export {
    getContacts,
    putContact,
    postContact,
    linkContact,
}