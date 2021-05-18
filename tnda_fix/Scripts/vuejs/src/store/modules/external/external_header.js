export const external_header = {
    namespaced: true,
    state: {

    },
    mutations: {

    },
    actions: {
        async getAuthStatus() {
            return await axios.get("/auth/getAuthStatus").then((result) => {
                console.log("getAuthStatus: ", result)
                return result
            }).catch((err) => {
                throw err
            })
        },
        async getPersonFromSession() {
            return await axios.get("/auth/getPersonFromSession").then((result) => {
                return result
            }).catch((err) => {
                throw err
            })
        },
    },
}