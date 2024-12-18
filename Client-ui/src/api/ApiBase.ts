function apifetch(url: RequestInfo | URL, method: string) {
    return fetch(url, {
        method: method,
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
        },
    })
    .then(response => response.json())
    .then(data => {
        return data;
    })
    .catch(response => {
        console.warn(response);
    });
}

function apifetchWithBody(url: RequestInfo | URL, method: string, data: any) {
    const body = JSON.stringify(data);
    return fetch(url, {
        method: method,
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
        },
        body: body
    })
    .then(response => response.json())
    .then(data => {
        return data;
    })
    .catch(response => {
        console.warn(response);
    });
}

function apifetchWithFile(url: RequestInfo | URL, method: string, file: string | Blob) {
    const formData = new FormData();
    formData.append('file', file);
    return fetch(url, {
        method: method,
        mode: 'cors',
        headers: {
        },
        body: formData,
    })
    .then(response => response.json())
    .then(data => {
        return data;
    })
    .catch(response => {
        console.warn(response);
    });
}

function get(url: string) {
    return apifetch(`${path}/${url}`, 'get')
}

function post(url: string, data: any) {
    console.log('post data', data)
    return apifetchWithBody(`${path}/${url}`, 'post', data)
}

function postFile(url: string, file: any) {
    console.log('post file', file)
    return apifetchWithFile(`${path}/${url}`, 'post', file)
}

function put(url: string, data: any) {
    console.log('put data', data)
    return apifetchWithBody(`${path}/${url}`, 'put', data)
}

function remove(url: string, id: number) {
    return apifetch(`${path}/${url}/${id}`, "delete")
}

const port = '5283'
const path = `http://localhost:${port}`

export {
    get,
    post,
    postFile,
    put,
    remove,
}