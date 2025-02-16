import {
    get,
    post,
    put,
    remove,
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

function deleteCategory(id: number) {
    return remove(controllerName, id);
}

function linkCategory(Category: any, category: any) {
    return put(controllerName, {'Category': Category, 'category': category})
}

function addCategoryRegex(regex: string, categoryId: number) {
    return post(controllerName + "/AddRegex", { regex, categoryId });
}

export {
    getCategories,
    putCategory,
    postCategory,
    deleteCategory,
    linkCategory,
    addCategoryRegex,
}