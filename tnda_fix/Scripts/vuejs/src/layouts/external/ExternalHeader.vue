<template>
  <div>
    <div>
      <nav class="navbar navbar-light bg-light static-top">
        <!-- id="vue_app_layout" -->
        <div
          class="container d-lg-flex justify-content-lg-between d-flex justify-content-center flex-column flex-lg-row"
        >
          <a class="navbar-brand" href="/external/index"
            ><i class="fas fa-church mr-1"></i>Thiếu nhi Giáo xứ Dĩ An</a
          >
          <button
            id="login_btn"
            v-if="!status"
            data-toggle="modal"
            data-target="#login_modal"
            class="btn btn-primary font-weight-bold"
          >
            Đăng nhập GLV
          </button>
          <a
            :href="'/internal/index?id=' + person.id"
            id="glv_name"
            v-if="status"
            >Xin chào
            <b>{{ person.ch_name }} {{ person.fname }} {{ person.name }}</b></a
          >
        </div>
      </nav>
    </div>
    <div
      class="modal fade"
      id="login_modal"
      tabindex="-1"
      role="dialog"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Đăng nhập GLV</h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form action="/auth/login" method="post">
            <div class="modal-body">
              <div class="form-group">
                <label>Tài khoản</label>
                <input
                  type="text"
                  name="username"
                  class="form-control"
                  placeholder="username"
                />
              </div>
              <div class="form-group">
                <label>Mật khẩu</label>
                <input
                  type="password"
                  name="password"
                  class="form-control"
                  placeholder="password"
                />
              </div>
            </div>
            <div class="modal-footer">
              <button
                type="submit"
                class="w-100 btn btn-primary"
                onclick="loading('.modal-content','show')"
              >
                Đăng nhập
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
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
</script>