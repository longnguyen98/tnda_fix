import Vue from 'vue'

// layouts -> external
Vue.component('ExternalHeader', require('./layouts/external/ExternalHeader.vue').default);
Vue.component('ExternalFooter', require('./layouts/external/ExternalFooter.vue').default);

// pages -> external
Vue.component('IndexPage', require('./pages/external/IndexPage.vue').default);