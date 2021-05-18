<template>
  <div>
    <nav class="navbar navbar-light bg-light static-top">
      <div
        id="vue_app_layout"
        class="container d-lg-flex justify-content-lg-between d-flex justify-content-center flex-column flex-lg-row"
      >
        <a class="navbar-brand" href="~/external/index"
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
        <!-- <a
          :href="'/internal/index?id=' + person.id"
          id="glv_name"
          style="display: none"
          v-if="status"
          >Xin chào
          <b>{{ person.ch_name }} {{ person.fname }} {{ person.name }}</b></a
        > -->
      </div>
    </nav>
    <section
      class="features-icons bg-info"
      style="padding-top: 0px; padding-bottom: 2%"
    >
      <index-page></index-page>
    </section>
    <!-- Footer -->
    <footer class="page-footer container font-small blue pt-4">
      <div class="container-fluid text-center text-md-left">
        <div class="row">
          <div class="col-md-6 mt-md-0 mt-3">
            <h5 class="text-uppercase">Nhà thờ Giáo xứ Dĩ An</h5>
            <p>368 Trần Hưng Đạo, Dĩ An, Bình Dương</p>
          </div>
          <hr class="clearfix w-100 d-md-none pb-3" />
          <div class="col-md-3 mb-md-0 mb-3"></div>
          <div class="col-md-3 mb-md-0 mb-3">
            <ul class="list-unstyled font-weight-bold">
              <li>
                <a
                  href="https://www.facebook.com/Thi%E1%BA%BFu-Nhi-Gi%C3%A1o-X%E1%BB%A9-D%C4%A9-An-102533327790422"
                >
                  <span
                    >Trang Facebook <i class="fab fa-facebook-square"></i
                  ></span>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </footer>
    <div
      class="footer-copyright text-center py-3 bg-secondary text-white font-weight-bold"
    >
      <i class="fas fa-laptop-code mr-1"></i> Phát triển bởi Long & Tuan -
      @BuildVersion.ver
    </div>
    <!-- Footer -->
  </div>
</template>

<script>
import  IndexPage  from "../pages/external/IndexPage.vue";
export default {
  components: {
    IndexPage,
  },
  data() {
    return {
      person: {},
      status: false,
    };
  },
};
</script>