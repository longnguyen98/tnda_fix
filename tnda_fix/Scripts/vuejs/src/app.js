window._ = require('lodash');
window.Vue = require('vue')

import Vue from 'vue'
// import router from './router';

// vuetify - Material
import Vuetify from 'vuetify';
Vue.use(Vuetify)

// Axios
import axios from 'axios'
import VueAxios from 'vue-axios'
Vue.use(VueAxios, axios)

// Vuex
import Vuex from 'vuex'
Vue.use(Vuex)

//Store
import { store } from "./store";

// Register components
import './register-components.js'

new Vue({
    // router,
    store,
    el: '#app',
})