function apifetch(url, method) {
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

function apifetchWithBody(url, method, data) {
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

function apifetchWithFile(url, method, file) {
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

function get(url) {
    return apifetch(`${path}/${url}`, 'get')
}

function post(url, data) {
    console.log('post data')
    console.log(data)
    return apifetchWithBody(`${path}/${url}`, 'post', data)
}

function postFile(url, file) {
    console.log('post file')
    console.log(file)
    return apifetchWithFile(`${path}/${url}`, 'post', file)
}

function put(url, data) {
    console.log('put data')
    console.log(data)
    return apifetchWithBody(`${path}/${url}`, 'put', data)
}

const port = '5001'
const path = `https://localhost:${port}`

export {
    get,
    post,
    postFile,
    put,
}