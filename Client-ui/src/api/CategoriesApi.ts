import {
    get,
    post,
    put,
} from './ApiBase'

const controllerName =  'Categories'

function getCategories() {
    return get(controllerName);
}
function putCategory() {
    console.error('Not Implimented Exception');
    return;
}
function postCategory(data: any) {
    return post(controllerName, data);
}

function linkCategory(Category: any, category: any) {
    return put(controllerName, {'Category': Category, 'category': category})
}

export {
    getCategories,
    putCategory,
    postCategory,
    linkCategory,
}