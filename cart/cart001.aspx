<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cart001.aspx.vb" EnableViewState="true"
    EnableEventValidation="false" Inherits="tw.page_cart_cart001" %>

<%@ Register TagPrefix="triphoowebHeader" TagName="triphoowebHeader" Src="../common/header.ascx" %>
<%@ Register TagPrefix="triphoowebFooter" TagName="triphoowebFooter" Src="../common/footer.ascx" %>
<%@ Register TagPrefix="triphoowebReserveDetail" TagName="triphoowebReserveDetail" Src="../common/cartDetail.ascx" %>
<%--<%@ Register TagPrefix="recommendHotelWidth" TagName="recommendHotelWidth" Src="../common/recommendHotelWidth.ascx" %>
<%@ Register TagPrefix="recommendOptionalWidth" TagName="recommendOptionalWidth" Src="../common/recommendOptionalWidth.ascx" %>--%>
<%@ Register TagPrefix="TelMe" TagName="TelMe" Src="../common/TelMe.ascx" %>
<%@ Register TagPrefix="Waiting" TagName="Waiting" Src="../common/Waiting.ascx" %>
<%@ Register TagPrefix="hotelSearchModal" TagName="hotelSearchModal" Src="../common/hotelSearchModal.ascx" %>
<%@ Register TagPrefix="breadcrumbsList" TagName="breadcrumbsList" Src="../common/breadcrumbsList.ascx" %>
<!DOCTYPE html>
<html lang="ja">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Style-Type" content="text/css" />
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no,maximum-scale=1" />
    <meta name="description" runat="server" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <title></title>
    <asp:Literal ID="SCRIPTLiteral" runat="server"></asp:Literal>
    <%--<script src="../../scripts/custom/commonCss.js?ver=1.02" type="text/javascript"></script>--%>
    <script src="../../scripts/custom/commontw.js?ver=1.02" type="text/javascript"></script>
    <script src="../../scripts/custom/common.js?ver=1.01" type="text/javascript"></script>
    <script type="text/javascript" src="../../scripts/jquery.flexslider-min.js"></script>
    <script type="text/javascript" src="../../scripts/holiday.js"></script>
    <script type="text/javascript">
        //-------------------------------------------------------------------------------------
        /* 戻るローディング画面対応 */
        //-------------------------------------------------------------------------------------
        $(window).on('pageshow', function (event) {
            $('#staticModal').modal('hide');
        });
        $(window).on('load', function () {
            DTL_ROOM_CHANGE(document.getElementById("hotelSearchModal_ROOM"));
            $('.carousel').flexslider({
                slideshow: false,
                animation: "slide",
                animationLoop: false,
                easing: 'easeOutExpo',
                itemWidth: 188,
                itemMargin: 10,
                minItems: 1,
                maxItems: 5,
                directionNav: false
            });

        });
        function ModalHotelCitySelect() {
            $('#HotelCitySelectModal').modal();
        }
        function ModalRoomSelect() {
            $('#RoomSelectModal').modal();
        }

        function deleteEnter(msg) {

            var commit = confirm(msg);
            if (commit == true) {
                WaitScreen()
            } else {
                return false;
            }


        }
    </script>
    <script type="text/javascript">
        document.onkeydown =
            function (e) {
                if (event.keyCode == 13) {
                    document.getElementById("TO_LOGINLinkButton").click();
                    return false;
                }
            }
    </script>
    <style>
        #RES_DETAIL_TourPanel .cp_table th {
            width: 200px
        }
    </style>
    <asp:Literal ID="PAGE_CSS" runat="server"></asp:Literal>
    <asp:Literal ID="PAGE_SCRIPT" runat="server"></asp:Literal>
    <asp:Literal ID="PAGE_META" runat="server"></asp:Literal>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="RT_CD" runat="server" />
        <asp:HiddenField ID="S_CD" runat="server" />
        <asp:HiddenField ID="SESSION_NO" runat="server" />
        <asp:HiddenField ID="HD_LANG" runat="server" />
        <asp:HiddenField ID="SCREEN_ID" runat="server" Value="Cart001" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="masthead">
            <triphoowebHeader:triphoowebHeader ID="triphoowebHeader" runat="Server"></triphoowebHeader:triphoowebHeader>
        </div>
        <asp:Panel ID="BREADCRUMBPanel" runat="Server" class="breadcrumb-bg">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb mb-0">
                                <li class="breadcrumb-item active">
                                    <asp:Label ID="LABEL_0001" runat="server"></asp:Label>
                                </li>
                                <li class="breadcrumb-item" aria-current="page">

                                    <asp:Label ID="LABEL_1002" runat="server"></asp:Label>

                                </li>
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_0003" runat="server"></asp:Label>
                                </li>
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_1004" runat="server"></asp:Label>
                                </li>
                            </ol>
                        </nav>
                    </div>

                </div>
            </div>
        </asp:Panel>
        <breadcrumbsList:breadcrumbsList ID="breadcrumbsList" runat="server"></breadcrumbsList:breadcrumbsList>
        <div class="container">
            <div class="row">
                <div id="triphoomain" class="col-lg-12 mt-4">
                    <%--▼ページヘッダー--%>
                    <div class="px-sm-0 px-2">
                        <asp:Literal ID="PageHeader" runat="server"></asp:Literal>
                    </div>
                    <%--▲ページヘッダー--%>
                    <%--▼OfflineNoticeメッセージ--%>
                    <asp:Panel ID="OfflineNoticePanel" CssClass="px-sm-0 px-2" runat="server" Visible="false">
                        <asp:Literal ID="OfflineNoticeMessage" runat="server"></asp:Literal>
                    </asp:Panel>
                    <asp:Panel ID="BOOKING_FLG_REMARKSPanel" CssClass="px-sm-0 px-2" runat="server" Visible="false">
                        <div class="alert alert-danger text-center">
                            <asp:Literal ID="BOOKING_FLG_REMARKS" runat="server"></asp:Literal>
                        </div>
                    </asp:Panel>
                    <%--▲OfflineNoticeメッセージ--%>
                    <div class="masthead">
                        <triphoowebReserveDetail:triphoowebReserveDetail ID="RES_DETAIL" runat="Server"></triphoowebReserveDetail:triphoowebReserveDetail>
                        <%--予約--%>
                        <asp:Panel ID="BOOKPanel" runat="server" CssClass="col-lg-12">
                            <div class="row">
                                <div class="col-lg-4 offset-lg-4 mb-3 mb-lg-0 pr-3 pl-3">
                                    <asp:LinkButton ID="BookLinkButton" runat="server" CssClass="btn btn-block btn-lg-tw btn-danger">
                                        ご予約申込
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="BOOKPanel2" runat="server" CssClass="col-lg-12" Visible="false">
                            <div class="row">
                                <div class="col-lg-4  mb-3 mb-lg-0 offset-lg-2 pr-3 pl-3">
                                    <asp:LinkButton ID="BackLinkButton" runat="server" CssClass="btn btn-block btn-lg-tw btn-primary">
                                        戻る
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-4 pr-3 pl-3">
                                    <asp:LinkButton ID="BookLinkButton2" runat="server" CssClass="btn btn-block btn-lg-tw btn-danger">
                                        ご予約申込
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--予約--%>
                        <%--ログイン--%>
                        <asp:Panel ID="LogInPanel" runat="server">
                            <div class="tab-ttl mb-4">
                                <h2>
                                    <asp:Label ID="LABEL_0005" runat="server"></asp:Label></h2>
                            </div>
                            <div class="masthead">
                                <div class="mb-3">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="card">
                                                <div class="card-header bg-primary text-white pt-3 pb-3 text-center">
                                                    <asp:Label ID="LABEL_0023" runat="server"></asp:Label>
                                                </div>
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-lg-12 pb-2">
                                                            <asp:Label ID="LABEL_0006" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-12 pb-2">
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0007" runat="server"></asp:Label></span><asp:Label ID="LABEL_0008" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-12 pb-2">
                                                            <asp:TextBox ID="E_MAIL" runat="server" class="form-control form-control-lg" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-12 pb-2">
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0009" runat="server"></asp:Label></span><asp:Label ID="LABEL_0010" runat="server" Width="120px"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-12 pb-2">
                                                            <asp:TextBox ID="PASSWORD" runat="server" class="form-control form-control-lg" MaxLength="200" TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="text-center">
                                                            <p class="checkbox">
                                                                <label>
                                                                    <asp:CheckBox class="text-center" ID="USER_CASH" runat="server" />
                                                                </label>
                                                            </p>
                                                        </div>
                                                        <div class="pb-2">

                                                            <asp:LinkButton ID="TO_LOGINLinkButton" runat="server" class="btn btn-danger btn-lg btn-block">
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="pb-2">
                                                            <asp:LinkButton ID="FORGET_PASSWORDLinkButton" runat="server" class="btn btn-block btn-lg btn-primary">
                                                            </asp:LinkButton>
                                                            <a id="FORGET_PASSWORD_WOWLinkButton" runat="server" visible="false" target="_blank" href="https://www.wowow.co.jp/inquiry/pwd_inquiry.php">※パスワードを忘れた方はこちら</a>
                                                            <a id="FORGET_PASSWORD_ASXLinkButton" runat="server" visible="false" class="btn btn-block btn-lg btn-primary" target="_blank" href="https://cam.ana.co.jp/reminder/ReminderInputServlet">パスワードを忘れた方はこちら</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="card">
                                                <div class="card-header bg-primary text-white pt-3 pb-3 text-center">
                                                    <asp:Label ID="LABEL_0024" runat="server"></asp:Label>
                                                </div>
                                                <div class="card-body">
                                                    <asp:Panel ID="RT_MYPAGE_MESSAGEPanel" runat="server" Visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-12 mb-3">
                                                                <asp:Label ID="RT_MYPAGE_MESSAGELabel" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:LinkButton ID="CLIENT_ADDLinkButton" runat="server" class="btn btn-block btn-lg btn-primary">
                                                    </asp:LinkButton>
                                                    <a id="CLIENT_ADD_WOWLinkButton" class="btn btn-block btn-lg btn-primary" runat="server" visible="false" target="_blank" href="https://www.wowow.co.jp/member/entry.php">新規会員登録</a>
                                                    <asp:LinkButton ID="NOT_LOGINLinkButton" runat="server" class="btn btn-block btn-lg btn-primary">
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-3">
                                        <asp:Literal ID="login001Literal" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--ログイン--%>
                </div>
                <%--▼ページフッター--%>
                <div class="row py-3">
                    <asp:Literal ID="Pagefooter" runat="server"></asp:Literal>
                </div>
                <%--▲ページフッター--%>
            </div>
        </div>
        <div class="container history_bg mt-3">
            <asp:Panel ID="PickUpHotelPanel" runat="server">
                <%--<recommendHotelWidth:recommendHotelWidth ID="recommendHotelWidth" runat="server" />--%>
                <div class="row">
                    <div class="col-lg-4 text-center offset-lg-4">
                        <asp:LinkButton ID="HotelLinkButton" runat="server" class="btn btn-lg btn-danger"
                            OnClientClick="ModalRoomSelect();return false;">
                            <asp:Label ID="LABEL_0011" runat="server"></asp:Label>
                        </asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
            <%--おすすめ・オプショナル--%>
            <%--<asp:Panel ID="PickUpOptionPanel" runat="server">
                <h4 class="mb-2" style="color: #666666">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal></h4>
                    <recommendOptionalWidth:recommendOptionalWidth ID="recommendOptionalWidth" runat="server"></recommendOptionalWidth:recommendOptionalWidth>
            </asp:Panel>--%>
            <%--おすすめ・オプショナル--%>
        </div>
        <div class="masthead">
            <triphoowebFooter:triphoowebFooter ID="triphoowebFooter" runat="Server"></triphoowebFooter:triphoowebFooter>
        </div>
        <hotelSearchModal:hotelSearchModal ID="hotelSearchModal" runat="Server"></hotelSearchModal:hotelSearchModal>
        <Waiting:Waiting ID="Waiting" runat="Server"></Waiting:Waiting>
        <script type="text/javascript" src="../../scripts/jquery.lazyload.min.js?v=1.9.1"></script>
        <script type="text/javascript">
            $(function () {
                $("img.lazy").lazyload({
                    effect: "fadeIn"
                });

            });
        </script>
        <div class="modal fade" id="EstimateModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        お見積り
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2">
                                <asp:Label ID="LABEL_0022" runat="server">宛名</asp:Label>
                            </div>
                            <div class="col-lg-10">
                                <asp:TextBox ID="ESTIMATE_ADDRESS" runat="server" CssClass="form-control" placeholder="お客様のお名前を入力して下さい"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-2 mb-3 mb-lg-0">
                                ご担当者様
                            </div>
                            <div class="col-lg-10">
                                <asp:TextBox ID="EMP_NM" runat="server" CssClass="form-control" placeholder="ご担当者様のお名前を入力して下さい"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2 mb-3 mb-lg-0">
                                タイトル
                            </div>
                            <div class="col-lg-10">
                                <asp:TextBox ID="ESTIMATE_TITLE" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-6 offset-lg-3">
                                <asp:LinkButton ID="EstimateSubmitButton" runat="server" CssClass="btn btn-block btn-danger">
                                    お見積り作成
                                </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="EstimateDownloadModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        お見積り書ダウンロード
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2 mb-3 mb-lg-0">
                                お見積り番号
                            </div>
                            <div class="col-lg-10">
                                <asp:Label ID="ESTIMATE_NO" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-6 offset-lg-3">
                                <a id="EstimateDownloadLinkButton" runat="server" class="btn btn-block btn-danger" target="_blank">ダウンロード
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div ID="SCRIPT_FOOTER"></div>
    <div id="jQuery_FOOTER"></div>
    <asp:Literal ID="BODY_BOTTOM_PC" runat="server"></asp:Literal>
    <asp:Literal ID="BODY_BOTTOM_SP" runat="server"></asp:Literal>
</body>
</html>
