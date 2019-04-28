import request from '@/utils/request'

export function StarTop (page, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/PoetryTopStar',
    params: { page: page, type: type }
  })
}

export function PoetryDetails (poetryId, pn, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/PoetryDetails',
    params: { poetryId: poetryId, pn: pn, type: type }
  })
}

export function SearchImage (key, perPage) {
  return request({
    method: 'get',
    url: 'https://pixabay.com/api/?key=11766290-a3982ff28b5e9fada8f9675e8&&q=' + key + '&&per_page=' + perPage + '&&lang=zh'
  })
}

export function SearchPoetImage (name, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/PoetDetails',
    params: { name: name, type: type }
  })
}

export function SearchPoetry (key, page, type, bol) {
  return request({
    method: 'get',
    url: '../api/Poetry/SearchPoetry',
    params: { key: key, page: page, type: type, bol: bol }
  })
}
export function SearchPoet (page, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/SearchPoet',
    params: { page: page, type: type }
  })
}

export function SearchRandomSentence (type) {
  return request({
    method: 'get',
    url: '../api/Poetry/SearchRandomSentence',
    params: { type: type }
  })
}

export function SearchPoetrySentence (page, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/SearchPoetrySentence',
    params: { page: page, type: type }
  })
}

export function getPoetryBaidu (word, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/getPoetryBaidu',
    params: { word: word, type: type }
  })
}

export function login (username, password) {
  return request({
    method: 'get',
    url: '../api/Poetry/Login',
    params: { username: username, password: password }
  })
}

export function register (username, password) {
  return request({
    method: 'get',
    url: '../api/Poetry/Register',
    params: { username: username, password: password }
  })
}

export function collectList (page, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/CollectList',
    params: { page: page, type: type }
  })
}

export function addCollect (data) {
  return request({
    method: 'post',
    url: '../api/Poetry/AddCollect',
    data: JSON.stringify(data)
  })
}

export function delCollect (data) {
  return request({
    method: 'post',
    url: '../api/Poetry/DelCollect',
    data: JSON.stringify(data)
  })
}

export function isCollect (poetryId) {
  return request({
    method: 'get',
    url: '../api/Poetry/IsCollect',
    params: { poetryId: poetryId }
  })
}

export function SearchPoetryFromTags (page, tags, dynasty, state, type) {
  return request({
    method: 'get',
    url: '../api/Poetry/SearchPoetryFromTags',
    params: { page: page, tags: tags, dynasty: dynasty, state: state, type: type }
  })
}

// export function SearchPoetryForPoet (poet, page, type) {
//   return request({
//     method: 'get',
//     url: '../api/Poetry/SearchPoetryForPoet',
//     params: { poet: poet, page: page, type: type }
//   })
// }
