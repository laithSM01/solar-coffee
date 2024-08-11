import { make } from "vuex-pathify";

import { IInventoryTimeLine } from "./../types/InventoryGraph";
import { InventoryService } from "./../services/inventory-service";

class GlobalStore {
    snapshotTimeline: IInventoryTimeLine = {
        productInventorySnapshots: [],
        timeLine: []
    }

    isTimelineBuilt = false;
}

const state = new GlobalStore();

const mutations = make.mutations(state);

const actions = {
    async assignSnapshots({ commit }: any) {
        const inventoryService = new InventoryService();
        const res = await inventoryService.getSnapShotHistory();

        const timeline: IInventoryTimeLine = {
            productInventorySnapshots: res.productInventorySnapshots,
            timeLine: res.timeLine
        };

        commit('SET_SNAPSHOT_TIMELINE', timeline);
        commit('SET_IS_TIMELINE_BUILT', true);
    },
};

const getters = {};

export default {
    state,
    mutations,
    actions,
    getters
}