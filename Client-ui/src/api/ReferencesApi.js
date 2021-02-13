import {
    get,
    post,
} from './ApiBase.js'

const controllerName =  'References'

function getReferences() {
    return get(controllerName);
}

function postReference(data) {
    return post(controllerName, data);
}

export {
    getReferences,
    postReference,
}