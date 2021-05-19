import Vue from 'vue'

// layouts -> external
Vue.component('ExternalHeader', require('./layouts/external/header/ExternalHeader.vue').default);
Vue.component('ExternalFooter', require('./layouts/external/footer/ExternalFooter.vue').default);

// pages -> external
Vue.component('IndexPage', require('./pages/external/IndexPage.vue').default);