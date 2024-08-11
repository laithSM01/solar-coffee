import Vue from "vue";
import Vuex from "vuex";
import pathify from "vuex-pathify";
import globalStore from './global-store';

pathify.options.mapping = 'simple';
pathify.options.deep = 2;

Vue.use(Vuex);

export default new Vuex.Store({
    ...globalStore,// we could have had global on modules too
    modules: {},
    plugins: [pathify.plugin]
});