window._ = require('lodash');
window.Vue = require('vue')

import Vue from 'vue'
import router from './router';
import Vuetify from 'vuetify';

//vuetify
Vue.use(Vuetify)

//vue components
Vue.component('ExternalLayout', require('./layouts/ExternalLayout.vue').default);

new Vue({
    router,
    el: '#app',
})