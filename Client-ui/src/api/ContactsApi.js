import {
    get,
    post,
    put,
} from './ApiBase.js'

const controllerName =  'Contacts'

function getContacts() {
    return get(controllerName);
}
function getContactGroups() {
    return get(`${controllerName}/groups`);
}
function putContact() {
    console.error('Not Implimented Exception');
    return;
}
function postContact(data) {
    return post(controllerName, data);
}
function postContactGroup(data) {
    return post(`${controllerName}/group`, data);
}

function linkContact(recordedContact, contact) {
    return put(controllerName, {'recordedContact': recordedContact, 'contact': contact})
}

export {
    getContacts,
    putContact,
    postContact,
    linkContact,
    getContactGroups,
    postContactGroup,
}