const path = `https://localhost:${port}`


import {
    get,
    post,
    port
} from './ApiBase.js'

function healthcheck() {
    return get(`${path}/healthcheck`)
}

function posthealthcheck(file) {
    return post(`${path}/healthcheck`, file)
}

export {
    healthcheck,
    posthealthcheck,
}