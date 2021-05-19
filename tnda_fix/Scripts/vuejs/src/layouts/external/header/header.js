export default {
    name: "external-header",
    data() {
      return {
        status: false,
        person: {},
      };
    },
    async created() {
      await this.checkAuthUser();
    },
    methods: {
      async checkAuthUser() {
        await this.$store
          .dispatch("external_header/getAuthStatus")
          .then((result) => {
            if (result.status == 200) {
              this.status = result.data;
            }
          })
          .catch((err) => {
            alert(err.message);
          });
        if (this.status) {
          await this.$store
            .dispatch("external_header/getPersonFromSession")
            .then((result) => {
              if (result.status == 200) {
                this.person = result.data;
                console.log(this.person);
              }
            })
            .catch((err) => {
              alert(err.message);
            });
        }
      },
    },
  };