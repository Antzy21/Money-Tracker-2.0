import {
    get,
    post,
    put,
} from './ApiBase.js'

const controllerName =  'Categories'

function getCategories() {
    return get(controllerName);
}
function putCategory() {
    console.error('Not Implimented Exception');
    return;
}
function postCategory(data) {
    return post(controllerName, data);
}

function linkCategory(Category, category) {
    return put(controllerName, {'Category': Category, 'category': category})
}

export {
    getCategories,
    putCategory,
    postCategory,
    linkCategory,
}