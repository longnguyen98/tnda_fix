import Vue from 'vue'
import VueRouter from 'vue-router';

//vue router
Vue.use(VueRouter)
// Component Layout
import ExternalLayout from "./layouts/ExternalLayout.vue";

const routes = [
    {
        path: '/', component: ExternalLayout.default, redirect: "Index Page", children: [
            {
                path: "/index",
                name: "Index Page",
                component: () =>
                    import(/* webpackChunkName: "demo" */ "./pages/external/IndexPage.vue"),
            },
        ]
    },
];

const router = new VueRouter({
    mode: 'history', //removes # (hashtag) from url
    base: '/',
    fallback: true, //router should fallback to hash (#) mode when the browser does not support history.pushState
    routes // short for `routes: routes`
});

export default router;