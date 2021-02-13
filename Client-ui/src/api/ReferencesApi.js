import {
    get,
    post,
    put,
} from './ApiBase.js'

const controllerName =  'References'

function getReferences() {
    return get(controllerName);
}
function putReference() {
    console.error('Not Implimented Exception');
    return;
}
function postReference(data) {
    return post(controllerName, data);
}

export {
    getReferences,
    putReference,
    postReference,
}