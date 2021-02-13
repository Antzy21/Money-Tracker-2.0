import {
    get,
    post,
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

export {
    getContacts,
    putContact,
    postContact,
}