const controllerName =  'Admin'

function deleteAllTransactions() {
    return fetch(`${API_URL}/${controllerName}/deleteAllTransactions`, {
        method: "delete",
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
        },
    })
}

function deleteAllCategories() {
    return fetch(`${API_URL}/${controllerName}/deleteAllCategories`, {
        method: "delete",
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
        },
    })
}

export {
    deleteAllTransactions,
    deleteAllCategories,
}