﻿'use strict';

angular.module('trang.controllers', []).
    controller('LoginCtrl', function ($scope, $rootScope) {
        $rootScope.isLoginPage = true;
        $rootScope.isLightLoginPage = false;
        $rootScope.isLockscreenPage = false;
        $rootScope.isMainPage = false;

    }).
	controller('LoginLightCtrl', function ($scope, $rootScope, $state, $sessionStorage, BaseServices) {
	    $rootScope.isLoginPage = true;
	    $rootScope.isLightLoginPage = true;
	    $rootScope.isLockscreenPage = false;
	    $rootScope.isMainPage = false;

	    $scope.login = function () {
	        if ($('#login').valid()) {
	            var acc = "grant_type=password&username=" + $scope.username + "&password=" + $scope.password;
	            BaseServices.check('/Token', acc).then(function (response) {
	                sessionStorage['token'] = response.data.access_token;
	                var dataTemp = response.data.fullName;
	                var name = dataTemp.split(" ");
	                $sessionStorage["user"] = {
	                    UserName: response.data.userName,
	                    FullName: name[1] + " " + name[name.length - 1],
	                    Roles: response.data.roles,
	                    Avatar: response.data.avatar
	                }
	                $state.go('app.dashboard-variant-4');
	            }).catch(function (error) {
	                $scope.loginerror = "Tên người dùng hoặc mật khẩu không đúng.";
	            });
	        };
	    };
	}).
    controller('LockscreenCtrl', function ($scope, $rootScope, $sessionStorage, $state, BaseServices) {
        $rootScope.isLoginPage = false;
        $rootScope.isLightLoginPage = false;
        $rootScope.isLockscreenPage = true;
        $rootScope.isMainPage = false;
        $scope.user = $sessionStorage["user"];
        $scope.login = function () {
            if ($('#lockscreen').valid()) {
                var acc = "grant_type=password&username=" + $scope.user.UserName + "&password=" + $scope.password;
                BaseServices.check('/Token', acc).then(function (response) {
                    sessionStorage['token'] = response.data.access_token;
                    var dataTemp = response.data.fullName;
                    var name = dataTemp.split(" ");
                    $sessionStorage["user"] = {
                        UserName: response.data.userName,
                        FullName: name[1] + " " + name[name.length - 1],
                        Roles: response.data.roles,
                        Avatar: response.data.avatar
                    }
                    $state.go('app.dashboard-variant-4');
                }).catch(function (error) {
                    $scope.loginerror = "Tên người dùng hoặc mật khẩu không đúng.";
                });
            };
        };
    }).
	controller('MainCtrl', function ($scope, $rootScope, $location, $layout, $layoutToggles, $pageLoadingBar, Fullscreen) {
	    $rootScope.isLoginPage = false;
	    $rootScope.isLightLoginPage = false;
	    $rootScope.isLockscreenPage = false;
	    $rootScope.isMainPage = true;

	    $rootScope.layoutOptions = {
	        horizontalMenu: {
	            isVisible: false,
	            isFixed: true,
	            minimal: false,
	            clickToExpand: false,

	            isMenuOpenMobile: false
	        },
	        sidebar: {
	            isVisible: true,
	            isCollapsed: false,
	            toggleOthers: true,
	            isFixed: true,
	            isRight: false,

	            isMenuOpenMobile: false,

	            // Added in v1.3
	            userProfile: true
	        },
	        chat: {
	            isOpen: false,
	        },
	        settingsPane: {
	            isOpen: false,
	            useAnimation: true
	        },
	        container: {
	            isBoxed: false
	        },
	        skins: {
	            sidebarMenu: '',
	            horizontalMenu: '',
	            userInfoNavbar: ''
	        },
	        pageTitles: true,
	        userInfoNavVisible: false
	    };

	    $layout.loadOptionsFromCookies(); // remove this line if you don't want to support cookies that remember layout changes


	    $scope.updatePsScrollbars = function () {
	        var $scrollbars = jQuery(".ps-scrollbar:visible");

	        $scrollbars.each(function (i, el) {
	            if (typeof jQuery(el).data('perfectScrollbar') == 'undefined') {
	                jQuery(el).perfectScrollbar();
	            }
	            else {
	                jQuery(el).perfectScrollbar('update');
	            }
	        })
	    };


	    // Define Public Vars
	    public_vars.$body = jQuery("body");


	    // Init Layout Toggles
	    $layoutToggles.initToggles();


	    // Other methods
	    $scope.setFocusOnSearchField = function () {
	        public_vars.$body.find('.search-form input[name="s"]').focus();

	        setTimeout(function () { public_vars.$body.find('.search-form input[name="s"]').focus() }, 100);
	    };


	    // Watch changes to replace checkboxes
	    $scope.$watch(function () {
	        cbr_replace();
	    });

	    // Watch sidebar status to remove the psScrollbar
	    $rootScope.$watch('layoutOptions.sidebar.isCollapsed', function (newValue, oldValue) {
	        if (newValue != oldValue) {
	            if (newValue == true) {
	                public_vars.$sidebarMenu.find('.sidebar-menu-inner').perfectScrollbar('destroy')
	            }
	            else {
	                public_vars.$sidebarMenu.find('.sidebar-menu-inner').perfectScrollbar({ wheelPropagation: public_vars.wheelPropagation });
	            }
	        }
	    });


	    // Page Loading Progress (remove/comment this line to disable it)
	    $pageLoadingBar.init();

	    $scope.showLoadingBar = showLoadingBar;
	    $scope.hideLoadingBar = hideLoadingBar;


	    // Set Scroll to 0 When page is changed
	    $rootScope.$on('$stateChangeStart', function () {
	        var obj = { pos: jQuery(window).scrollTop() };

	        TweenLite.to(obj, .25, {
	            pos: 0, ease: Power4.easeOut, onUpdate: function () {
	                $(window).scrollTop(obj.pos);
	            }
	        });
	    });


	    // Full screen feature added in v1.3
	    $scope.isFullscreenSupported = Fullscreen.isSupported();
	    $scope.isFullscreen = Fullscreen.isEnabled() ? true : false;

	    $scope.goFullscreen = function () {
	        if (Fullscreen.isEnabled())
	            Fullscreen.cancel();
	        else
	            Fullscreen.all();

	        $scope.isFullscreen = Fullscreen.isEnabled() ? true : false;
	    }

	}).
    controller('SidebarMenuCtrl', function ($scope, $rootScope, $menuItems, $timeout, $location, $state, $layout, $sessionStorage) {
        $scope.user = $sessionStorage["user"];

        // Trigger menu setup
        public_vars.$sidebarMenu = public_vars.$body.find('.sidebar-menu');
        $timeout(setup_sidebar_menu, 1);

        ps_init(); // perfect scrollbar for sidebar
    }).
	controller('HorizontalMenuCtrl', function ($scope, $rootScope, $menuItems, $timeout, $location, $state) {
	    var $horizontalMenuItems = $menuItems.instantiate();

	    $scope.menuItems = $horizontalMenuItems.prepareHorizontalMenu().getAll();

	    // Set Active Menu Item
	    $horizontalMenuItems.setActive($location.path());

	    $rootScope.$on('$stateChangeSuccess', function () {
	        $horizontalMenuItems.setActive($state.current.name);

	        $(".navbar.horizontal-menu .navbar-nav .hover").removeClass('hover'); // Close Submenus when item is selected
	    });

	    // Trigger menu setup
	    $timeout(setup_horizontal_menu, 1);
	}).
    controller('SettingsPaneCtrl', function ($rootScope, $scope, $sessionStorage) {
        console.log('ok');
        $scope.user = $sessionStorage["user"];
        // Define Settings Pane Public Variable
        public_vars.$settingsPane = public_vars.$body.find('.settings-pane');
        public_vars.$settingsPaneIn = public_vars.$settingsPane.find('.settings-pane-inner');
    }).
	controller('ChatCtrl', function ($scope, $element) {
	    var $chat = jQuery($element),
			$chat_conv = $chat.find('.chat-conversation');

	    $chat.find('.chat-inner').perfectScrollbar(); // perfect scrollbar for chat container


	    // Chat Conversation Window (sample)
	    $chat.on('click', '.chat-group a', function (ev) {
	        ev.preventDefault();

	        $chat_conv.toggleClass('is-open');

	        if ($chat_conv.is(':visible')) {
	            $chat.find('.chat-inner').perfectScrollbar('update');
	            $chat_conv.find('textarea').autosize();
	        }
	    });

	    $chat_conv.on('click', '.conversation-close', function (ev) {
	        ev.preventDefault();

	        $chat_conv.removeClass('is-open');
	    });
	}).
	controller('UIModalsCtrl', function ($scope, $rootScope, $modal, $sce) {
	    // Open Simple Modal
	    $scope.openModal = function (modal_id, modal_size, modal_backdrop) {
	        $rootScope.currentModal = $modal.open({
	            templateUrl: modal_id,
	            size: modal_size,
	            backdrop: typeof modal_backdrop == 'undefined' ? true : modal_backdrop
	        });
	    };

	    // Loading AJAX Content
	    $scope.openAjaxModal = function (modal_id, url_location) {
	        $rootScope.currentModal = $modal.open({
	            templateUrl: modal_id,
	            resolve: {
	                ajaxContent: function ($http) {
	                    return $http.get(url_location).then(function (response) {
	                        $rootScope.modalContent = $sce.trustAsHtml(response.data);
	                    }, function (response) {
	                        $rootScope.modalContent = $sce.trustAsHtml('<div class="label label-danger">Cannot load ajax content! Please check the given url.</div>');
	                    });
	                }
	            }
	        });

	        $rootScope.modalContent = $sce.trustAsHtml('Modal content is loading...');
	    }
	}).
	controller('PaginationDemoCtrl', function ($scope) {
	    $scope.totalItems = 64;
	    $scope.currentPage = 4;

	    $scope.setPage = function (pageNo) {
	        $scope.currentPage = pageNo;
	    };

	    $scope.pageChanged = function () {
	        console.log('Page changed to: ' + $scope.currentPage);
	    };

	    $scope.maxSize = 5;
	    $scope.bigTotalItems = 175;
	    $scope.bigCurrentPage = 1;
	}).
	controller('LayoutVariantsCtrl', function ($scope, $layout, $cookies) {
	    $scope.opts = {
	        sidebarType: null,
	        fixedSidebar: null,
	        sidebarToggleOthers: null,
	        sidebarVisible: null,
	        sidebarPosition: null,

	        horizontalVisible: null,
	        fixedHorizontalMenu: null,
	        horizontalOpenOnClick: null,
	        minimalHorizontalMenu: null,

	        sidebarProfile: null
	    };

	    $scope.sidebarTypes = [
			{ value: ['sidebar.isCollapsed', false], text: 'Expanded', selected: $layout.is('sidebar.isCollapsed', false) },
			{ value: ['sidebar.isCollapsed', true], text: 'Collapsed', selected: $layout.is('sidebar.isCollapsed', true) },
	    ];

	    $scope.fixedSidebar = [
			{ value: ['sidebar.isFixed', true], text: 'Fixed', selected: $layout.is('sidebar.isFixed', true) },
			{ value: ['sidebar.isFixed', false], text: 'Static', selected: $layout.is('sidebar.isFixed', false) },
	    ];

	    $scope.sidebarToggleOthers = [
			{ value: ['sidebar.toggleOthers', true], text: 'Yes', selected: $layout.is('sidebar.toggleOthers', true) },
			{ value: ['sidebar.toggleOthers', false], text: 'No', selected: $layout.is('sidebar.toggleOthers', false) },
	    ];

	    $scope.sidebarVisible = [
			{ value: ['sidebar.isVisible', true], text: 'Visible', selected: $layout.is('sidebar.isVisible', true) },
			{ value: ['sidebar.isVisible', false], text: 'Hidden', selected: $layout.is('sidebar.isVisible', false) },
	    ];

	    $scope.sidebarPosition = [
			{ value: ['sidebar.isRight', false], text: 'Left', selected: $layout.is('sidebar.isRight', false) },
			{ value: ['sidebar.isRight', true], text: 'Right', selected: $layout.is('sidebar.isRight', true) },
	    ];

	    $scope.horizontalVisible = [
			{ value: ['horizontalMenu.isVisible', true], text: 'Visible', selected: $layout.is('horizontalMenu.isVisible', true) },
			{ value: ['horizontalMenu.isVisible', false], text: 'Hidden', selected: $layout.is('horizontalMenu.isVisible', false) },
	    ];

	    $scope.fixedHorizontalMenu = [
			{ value: ['horizontalMenu.isFixed', true], text: 'Fixed', selected: $layout.is('horizontalMenu.isFixed', true) },
			{ value: ['horizontalMenu.isFixed', false], text: 'Static', selected: $layout.is('horizontalMenu.isFixed', false) },
	    ];

	    $scope.horizontalOpenOnClick = [
			{ value: ['horizontalMenu.clickToExpand', false], text: 'No', selected: $layout.is('horizontalMenu.clickToExpand', false) },
			{ value: ['horizontalMenu.clickToExpand', true], text: 'Yes', selected: $layout.is('horizontalMenu.clickToExpand', true) },
	    ];

	    $scope.minimalHorizontalMenu = [
			{ value: ['horizontalMenu.minimal', false], text: 'No', selected: $layout.is('horizontalMenu.minimal', false) },
			{ value: ['horizontalMenu.minimal', true], text: 'Yes', selected: $layout.is('horizontalMenu.minimal', true) },
	    ];

	    $scope.chatVisibility = [
			{ value: ['chat.isOpen', false], text: 'No', selected: $layout.is('chat.isOpen', false) },
			{ value: ['chat.isOpen', true], text: 'Yes', selected: $layout.is('chat.isOpen', true) },
	    ];

	    $scope.boxedContainer = [
			{ value: ['container.isBoxed', false], text: 'No', selected: $layout.is('container.isBoxed', false) },
			{ value: ['container.isBoxed', true], text: 'Yes', selected: $layout.is('container.isBoxed', true) },
	    ];

	    $scope.sidebarProfile = [
			{ value: ['sidebar.userProfile', false], text: 'No', selected: $layout.is('sidebar.userProfile', false) },
			{ value: ['sidebar.userProfile', true], text: 'Yes', selected: $layout.is('sidebar.userProfile', true) },
	    ];

	    $scope.resetOptions = function () {
	        $layout.resetCookies();
	        window.location.reload();
	    };

	    var setValue = function (val) {
	        if (val != null) {
	            val = eval(val);
	            $layout.setOptions(val[0], val[1]);
	        }
	    };

	    $scope.$watch('opts.sidebarType', setValue);
	    $scope.$watch('opts.fixedSidebar', setValue);
	    $scope.$watch('opts.sidebarToggleOthers', setValue);
	    $scope.$watch('opts.sidebarVisible', setValue);
	    $scope.$watch('opts.sidebarPosition', setValue);

	    $scope.$watch('opts.horizontalVisible', setValue);
	    $scope.$watch('opts.fixedHorizontalMenu', setValue);
	    $scope.$watch('opts.horizontalOpenOnClick', setValue);
	    $scope.$watch('opts.minimalHorizontalMenu', setValue);

	    $scope.$watch('opts.chatVisibility', setValue);

	    $scope.$watch('opts.boxedContainer', setValue);

	    $scope.$watch('opts.sidebarProfile', setValue);
	}).
	controller('ThemeSkinsCtrl', function ($scope, $layout) {
	    var $body = jQuery("body");

	    $scope.opts = {
	        sidebarSkin: $layout.get('skins.sidebarMenu'),
	        horizontalMenuSkin: $layout.get('skins.horizontalMenu'),
	        userInfoNavbarSkin: $layout.get('skins.userInfoNavbar')
	    };

	    $scope.skins = [
			{ value: '', name: 'Default', palette: ['#2c2e2f', '#EEEEEE', '#FFFFFF', '#68b828', '#27292a', '#323435'] },
			{ value: 'aero', name: 'Aero', palette: ['#558C89', '#ECECEA', '#FFFFFF', '#5F9A97', '#558C89', '#255E5b'] },
			{ value: 'navy', name: 'Navy', palette: ['#2c3e50', '#a7bfd6', '#FFFFFF', '#34495e', '#2c3e50', '#ff4e50'] },
			{ value: 'facebook', name: 'Facebook', palette: ['#3b5998', '#8b9dc3', '#FFFFFF', '#4160a0', '#3b5998', '#8b9dc3'] },
			{ value: 'turquoise', name: 'Truquoise', palette: ['#16a085', '#96ead9', '#FFFFFF', '#1daf92', '#16a085', '#0f7e68'] },
			{ value: 'lime', name: 'Lime', palette: ['#8cc657', '#ffffff', '#FFFFFF', '#95cd62', '#8cc657', '#70a93c'] },
			{ value: 'green', name: 'Green', palette: ['#27ae60', '#a2f9c7', '#FFFFFF', '#2fbd6b', '#27ae60', '#1c954f'] },
			{ value: 'purple', name: 'Purple', palette: ['#795b95', '#c2afd4', '#FFFFFF', '#795b95', '#27ae60', '#5f3d7e'] },
			{ value: 'white', name: 'White', palette: ['#FFFFFF', '#666666', '#95cd62', '#EEEEEE', '#95cd62', '#555555'] },
			{ value: 'concrete', name: 'Concrete', palette: ['#a8aba2', '#666666', '#a40f37', '#b8bbb3', '#a40f37', '#323232'] },
			{ value: 'watermelon', name: 'Watermelon', palette: ['#b63131', '#f7b2b2', '#FFFFFF', '#c03737', '#b63131', '#32932e'] },
			{ value: 'lemonade', name: 'Lemonade', palette: ['#f5c150', '#ffeec9', '#FFFFFF', '#ffcf67', '#f5c150', '#d9a940'] },
	    ];

	    $scope.$watch('opts.sidebarSkin', function (val) {
	        if (val != null) {
	            $layout.setOptions('skins.sidebarMenu', val);

	            $body.attr('class', $body.attr('class').replace(/\sskin-[a-z]+/)).addClass('skin-' + val);
	        }
	    });

	    $scope.$watch('opts.horizontalMenuSkin', function (val) {
	        if (val != null) {
	            $layout.setOptions('skins.horizontalMenu', val);

	            $body.attr('class', $body.attr('class').replace(/\shorizontal-menu-skin-[a-z]+/)).addClass('horizontal-menu-skin-' + val);
	        }
	    });

	    $scope.$watch('opts.userInfoNavbarSkin', function (val) {
	        if (val != null) {
	            $layout.setOptions('skins.userInfoNavbar', val);

	            $body.attr('class', $body.attr('class').replace(/\suser-info-navbar-skin-[a-z]+/)).addClass('user-info-navbar-skin-' + val);
	        }
	    });
	}).
	// Added in v1.3
	controller('FooterChatCtrl', function ($scope, $element) {
	    $scope.isConversationVisible = false;

	    $scope.toggleChatConversation = function () {
	        $scope.isConversationVisible = !$scope.isConversationVisible;

	        if ($scope.isConversationVisible) {
	            setTimeout(function () {
	                var $el = $element.find('.ps-scrollbar');

	                if ($el.hasClass('ps-scroll-down')) {
	                    $el.scrollTop($el.prop('scrollHeight'));
	                }

	                $el.perfectScrollbar({
	                    wheelPropagation: false
	                });

	                $element.find('.form-control').focus();

	            }, 300);
	        }
	    }
	}).
    controller('BookController', function ($scope, ASSETS, DTOptionsBuilder, BaseServices) {
        $scope.listbook = {};
        //load init data
        BaseServices.AuthencationKeyGet('api/Book', sessionStorage['token'])
                .then(function (response) {
                    console.log(response.data);
                    $scope.listbook = response.data;
                }).catch(function (error) {
                    console.log(error);
                });
        //option for datatable
        $scope.dtOptions = DTOptionsBuilder.newOptions()
       .withDisplayLength(10)
       .withOption('bLengthChange', true)
       .withOption('responsive', true)
        //refresh event
        $scope.refresh = function () {
            BaseServices.AuthencationKeyGet('api/Book', sessionStorage['token'])
                .then(function (response) {
                    console.log(response.data);
                    $scope.listbook = response.data;
                }).catch(function (error) {
                    console.log(error);
                });
        };

        $scope.editRow = function (book) {

        }

        $scope.deleteRow = function (book) {

        }
    }).
    controller('EditBookController', function ($scope, $stateParams, BaseServices, $sessionStorage) {
        $scope.checkhavebook = 0;
        $scope.data = {};
        $scope.data.createby = $sessionStorage['user'].FullName;

        BaseServices.AuthencationKeyGet("api/Values", sessionStorage['token']).
            then(function (response) {
                if (response.data.length != 0) {
                    $scope.kind = response.data[0];
                    $scope.language = response.data[1];
                    $scope.company = response.data[2];
                    $scope.category = response.data[3];
                }
            }).catch(function (error) {
                console.log(error);
            })

        if ($stateParams.id != undefined) {
            BaseServices.AuthencationKeyGet('api/Book/' + $stateParams.id, sessionStorage['token'])
                 .then(function (response) {
                     if (response.data.length != 0) {
                         $scope.checkhavebook = 1;
                         binding_data(response);
                     }
                 }).catch(function (error) {
                 })
        }

        $scope.tabbedout = function (bookCode) {
            BaseServices.AuthencationKeyGet('api/Book/' + bookCode, sessionStorage['token'])
                .then(function (response) {
                    if (response.data.length != 0) {
                        $scope.checkhavebook = 1;
                        binding_data(response);
                    }
                }).catch(function (error) {
                })
        };

        function binding_data(response) {
            $scope.bookCode = $stateParams.id;
            $scope.data.bookname = response.data[0].BookName;
            $scope.data.author = response.data[0].Author;
            $scope.data.company = response.data[0].CompanyName;
            $scope.data.price = response.data[0].Price;
            $('#sample_wysiwyg').data("wysihtml5").editor.setValue(response.data[0].Content);
            $scope.data.kind = response.data[0].Kind1;
            $scope.data.language = response.data[0].Language1;
            $scope.data.category = response.data[0].CategoryName;
            $scope.data.yeah = moment(response.data[0].YearPublish).format('ddd, ll');
            $scope.data.keyword = response.data[0].Keyword;
            $scope.data.createby = response.data[0].FullName;
            $('#sample_wysiwyg').data("wysihtml5").editor.focus();
            $scope.data.image = response.data[1];
        }

        $scope.setPreviewImage = function (photo) {
            if ($scope.previewImage != '/BookImage/' + photo.Image1) {
                $scope.previewImage = '/BookImage/' + photo.Image1;
            }
            else {
                $scope.previewImage = null;
            }
        }

        $scope.save = function () {
            var objBook = {
                BookCode: $scope.bookCode,
                CompanyPublishName: $scope.data.company,
                Kind: $scope.data.kind,
                BookName: $scope.data.bookname,
                Language: $scope.data.language,
                Author: $scope.data.author,
                Category: $scope.data.category,
                Price: $scope.data.price,
                YearPublish: moment($scope.data.yea).format('L'),
                Keyword: $scope.data.keyword,
                Content: $('#sample_wysiwyg').data("wysihtml5").editor.getValue()
            }
            if ($scope.checkhavebook == 0) {
                BaseServices.AuthencationKey('api/Book', objBook, sessionStorage['token']).then(function (response) {
                    console.log(response);
                }).catch(function (error) {
                    console.log(error);
                });
            }
            else {
                BaseServices.AuthencationKeyPut('api/Book/' + $scope.bookCode, objBook, sessionStorage['token']).then(function (response) {
                    console.log(response);
                }).catch(function (error) {
                    console.log(error);
                });
            }
        };

        $scope.clearform = function () {
            $scope.data = {};
            $scope.bookCode = "";
            $scope.checkhavebook = 0;
            $('#sample_wysiwyg').data("wysihtml5").editor.clear();
            $scope.data.image = {};
        }
    })
    .controller('BorrowController', function ($scope, $state, BaseServices, $sessionStorage, ASSETS, DTOptionsBuilder) {
        //load init data
        BaseServices.AuthencationKeyGet('api/Borrow', sessionStorage['token'])
                .then(function (response) {
                    console.log(response.data);
                    $scope.listborrow = response.data;
                }).catch(function (error) {
                    console.log(error);
                });
        //option for datatable
        $scope.dtOptions = DTOptionsBuilder.newOptions()
       .withDisplayLength(10)
       .withOption('bLengthChange', true)
       .withOption('responsive', true)
        //refresh event
        $scope.refresh = function () {
            BaseServices.AuthencationKeyGet('api/Borrow', sessionStorage['token'])
                .then(function (response) {
                    console.log(response.data);
                    $scope.listbook = response.data;
                }).catch(function (error) {
                    console.log(error);
                });
        };

        //click td element
        $scope.handling = function (item) {
            sessionStorage["CardReaderID"] = item.CardReaderID;
            $state.go('app.major.handing', { "id": item.BorrowCode });
        };
    })

    .controller('HandingController', function ($scope, $state, $stateParams, BaseServices, $sessionStorage, $rootScope, $modal) {
        $scope.param = $stateParams.id;
        $scope.CardReaderID = sessionStorage["CardReaderID"];
        $scope.usermanage = $sessionStorage['user'].FullName;
        var date = new Date();
        $scope.date = date.getDate();
        $scope.month = date.getMonth() + 1;
        $scope.year = date.getFullYear();
        $scope.BookCodeSelected = null;
        //load init data
        BaseServices.AuthencationKeyGet('api/Borrow/' + $scope.param, sessionStorage['token'])
                .then(function (response) {
                    if (response.data != 2 && response.data != 4) {
                        BaseServices.AuthencationKeyPut('api/Borrow/' + $scope.param, 2, sessionStorage['token'])
                        .then(function (response1) {
                        }).catch(function (error) {
                            console.log(error);
                        });
                        $scope.listborrow = response.data;
                        $scope.BookCodeSelected = $scope.listborrow[0].BookCode;
                        $scope.getbook($scope.listborrow[0].BookCode);
                        $scope.timetoget = moment($scope.listborrow[0].TimeToGet).format('hh:mm A');
                        $scope.dateborrow = moment($scope.listborrow[0].DateBorrow).format('DD/MM/YYYY');
                        $scope.dateexpried = moment($scope.listborrow[0].DateExpried).format('DD/MM/YYYY');
                    }
                    else {
                        debugger;
                        toastr.info('Đã có thủ thư khác xử lý yêu cầu này');
                        $scope.listborrow = [{ PendingStatusName: 'Từ chối' }];
                    }

                }).catch(function (error) {
                    console.log(error);
                });
        BaseServices.AuthencationKeyGet('api/Reader/' + $scope.CardReaderID, sessionStorage['token'])
                .then(function (response) {
                    $scope.infomation = response.data[0];
                }).catch(function (error) {
                    console.log(error);
                });
        // //option for datatable
        // $scope.dtOptions = DTOptionsBuilder.newOptions()
        //.withDisplayLength(10)
        //.withOption('bLengthChange', true)
        //.withOption('responsive', true)

        //getbookinfo
        $scope.getbook = function (bookcode) {
            $scope.BookCodeSelected = bookcode;
            BaseServices.AuthencationKeyGet('api/Book/' + bookcode, sessionStorage['token'])
                .then(function (response) {
                    $scope.bookinfo = response.data[0];
                }).catch(function (error) {
                    console.log(error);
                });
        }

        //Open modal reson
        $scope.openModal = function (modal_size, modal_backdrop) {
            $rootScope.currentModal = $modal.open({
                templateUrl: 'modal-confirm',
                size: modal_size,
                backdrop: typeof modal_backdrop == 'undefined' ? true : modal_backdrop,
            });
        }

        //denied borrow
        $rootScope.denided = function () {
            var PendingStatus = 6;
            BaseServices.AuthencationKeyPut('api/Borrow/' + $scope.param, PendingStatus, sessionStorage['token'])
            .then(function (response) {
                console.log(response);
            }).catch(function (error) {
                console.log(error);
            });
        }

        //accept borrow
        $scope.accept = function () {
            var opts = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-bottom-right",
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "8000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
            toastr.success("Bạn đã xác nhận yêu cầu mượn " + $scope.param + " .", "Xác nhận mượn thành công", opts);
            BaseServices.AuthencationKeyPut('api/Borrow/' + $scope.param, 3, sessionStorage['token'])
            .then(function (response) {
            }).catch(function (error) {
                console.log(error);
            });
        }
    })
;