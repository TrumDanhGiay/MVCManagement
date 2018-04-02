app.directive('showListBook', function () {
    return {
        replace: true,
        template: `<div class="panel panel-flat">
        <div class="panel-heading">
            <h5 class="panel-title">Sách hiện có</h5>
            <div class="heading-elements">
                <ul class="icons-list">
                    <li><a data-action="collapse"></a></li>
                    <li><a data-action="reload" ng-click="reload()"></a></li>
                    <li><a data-action="close"></a></li>
                </ul>
            </div>
        </div>

        <div class="panel-body">
            Danh sách sách hiện có trong thư viện.
        </div>
        <table class="table table-bordered table-hover datatable-highlight">
            <thead>
                <tr>
                    <th>Mã sách</th>
                    <th>Tên sách</th>
                    <th>Tác giả</th>
                    <th>Nhà xuất bản</th>
                    <th>Năm xuất bản</th>
                    <th>Ngôn ngữ</th>
                    <th>Thể loại</th>
                    <th>Định dạng</th>
                    <th>Giá</th>
                    <th>Ngày thêm</th>
                    <th>Ảnh</th>
                    <th>Hành động</th>
                </tr>
            </thead>
            <tbody>
                <tr ng-repeat="item in listbook">
                    <td>{{item.BookCode}}</td>
                    <td>{{item.BookName}}</td>
                    <td>{{item.Author}}</td>
                    <td>{{item.CompanyName}}</td>
                    <td>{{item.YearPublish |date:'yyyy'}}</td>
                    <td>{{item.Language1}}</td>
                    <td>{{item.Kind1}}</td>
                    <td>{{item.BookFormat}}</td>
                    <td><span class="label label-success">{{item.Price}}</span></td>
                    <td>{{item.AddDate | date:'dd-MM-yyyy'}}</td>
                    <td><img ng-src="{{item.Image}}" /></td>
                    <td class="text-center">
                        <ul class="icons-list">
                            <li class="dropdown">
                                <a href="" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="icon-menu9"></i>
                                </a>
                                <ul class="dropdown-menu dropdown-menu-right">
                                    <li><a href=""><i class="icon-file-pdf"></i> Sửa</a></li>
                                    <li><a href=""><i class="icon-file-excel"></i> Xóa</a></li>
                                    <li><a href=""><i class="icon-file-word"></i> Xuất nội dung</a></li>
                                </ul>
                            </li>
                        </ul>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>`,
        link: function ($scope, $el) {
            var script = document.createElement('script');
            script.src = '/Style/assets/js/plugins/loaders/pace.min.js'
            $el.append(script);
            var script1 = document.createElement('script');
            script1.src = '/Style/assets/js/core/libraries/jquery.min.js'
            $el.append(script1);
            var script2 = document.createElement('script');
            script2.src = '/Style/assets/js/core/libraries/bootstrap.min.js'
            $el.append(script2);
            var script6 = document.createElement('script');
            script6.src = '/Style/assets/js/plugins/loaders/blockui.min.js'
            $el.append(script6);
            var script3 = document.createElement('script');
            script3.src = '/Style/assets/js/plugins/tables/datatables/datatables.min.js'
            $el.append(script3);
            var script4 = document.createElement('script');
            script4.src = '/Style/assets/js/plugins/forms/selects/select2.min.js'
            $el.append(script4);
            var script5 = document.createElement('script');
            script5.src = '/Style/assets/js/pages/datatables_advanced.js'
            $el.append(script5);
        }
    }
});