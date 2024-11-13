import {
    get,
    post,
} from './ApiBase.js'

function healthcheck() {
    return get('healthcheck');
}

function posthealthcheck(file: any) {
    return post('healthcheck', file);
}

export {
    healthcheck,
    posthealthcheck,
}