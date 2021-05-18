import Vue from "vue";
import Vuex from "vuex";
Vue.use(Vuex);

import { index_page } from "./modules/external/index_page.js"
import { external_header } from "./modules/external/external_header.js"

export const store = new Vuex.Store({
    modules: {
        index_page,
        external_header,
    },
});