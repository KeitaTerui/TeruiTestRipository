<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cart003.aspx.vb" EnableViewState="true"
    EnableEventValidation="false" Inherits="tw.page_cart_cart003" %>

<%@ Register TagPrefix="triphoowebHeader" TagName="triphoowebHeader" Src="../common/header.ascx" %>
<%@ Register TagPrefix="triphoowebFooter" TagName="triphoowebFooter" Src="../common/footer.ascx" %>
<%@ Register TagPrefix="triphoowebReserveDetail" TagName="triphoowebReserveDetail" Src="../common/bookDetail.ascx" %>
<%@ Register TagPrefix="triphoowebReserveDetailCheck" TagName="triphoowebReserveDetailCheck" Src="../common/bookDetailCheck.ascx" %>
<%@ Register TagPrefix="TelMe" TagName="TelMe" Src="../common/TelMe.ascx" %>
<%@ Register TagPrefix="CreditSecurityCode" TagName="CreditSecurityCode" Src="../common/creditSecurityCode.ascx" %>
<%@ Register TagPrefix="GmoTokenPay" TagName="GmoTokenPay" Src="../common/GmoTokenPay.ascx" %>
<%@ Register TagPrefix="GmoTokenPaySmbc" TagName="GmoTokenPaySmbc" Src="../common/GmoTokenPaySmbc.ascx" %>
<%@ Register TagPrefix="GmoTokenPaySmbcStation" TagName="GmoTokenPaySmbcStation" Src="../common/GmoTokenPaySmbcStation.ascx" %>
<%@ Register TagPrefix="EconTokenPay" TagName="EconTokenPay" Src="../common/EconTokenPay.ascx" %>
<%@ Register TagPrefix="VeriTrans4GTokenPay" TagName="VeriTrans4GTokenPay" Src="../common/VeriTrans4GTokenPay.ascx" %>
<%@ Register TagPrefix="Waiting" TagName="Waiting" Src="../common/WaitingBook.ascx" %>

<!DOCTYPE html>
<html lang="ja">
<head runat="server">
    <meta http-equiv="Content-Style-Type" content="text/css" />
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <title></title>
    <asp:Literal ID="SCRIPTLiteral" runat="server"></asp:Literal>
    <script src="../../scripts/custom/commontw.js?ver=1.02" type="text/javascript"></script>
    <script src="../../scripts/custom/common.js?ver=1.01" type="text/javascript"></script>
    <script type="text/javascript" src="../../scripts/jquery.flexslider-min.js"></script>
    <script type="text/javascript" src="../../scripts/holiday.js"></script>
    <script type="text/javascript">
        function WindowOpen(url) {
            window.open(url, 'chdwin', 'width=650px,height=650px,status=yes, location=no, toolbar=no, scrollbars=yes , resizable=yes');
            return false;
        }
        function ModalSecurity() {
            $('#securityModal').modal();
        }
        function _AirModal() {
            $('#AirModal').modal();
        }
        function _HotelModal() {
            $('#HotelModal').modal();
        }
        function _OptionalModal() {
            $('#OptionalModal').modal();
        }
        function _TourModal() {
            $('#TourModal').modal();
        }
        function _DpModal() {
            $('#DpModal').modal();
        }
        function BookWait() {
            $('#staticModal').modal();
        }
    </script>

    <script type="text/javascript">
        $(function () {
            var ua = window.navigator.userAgent.toLowerCase();
            var isPad = ua.indexOf('ipad') > -1 || ua.indexOf('macintosh') > -1 && 'ontouchend' in document;
            var isSP = ua.indexOf('blackberry') > -1 ||
                ua.indexOf('android') > -1 ||
                ua.indexOf('mobile') > -1 ||
                ua.indexOf('phone') > -1 ||
                ua.indexOf('ipad') > -1 ||
                ua.indexOf('macintosh') > -1 && 'ontouchend' in document;
            var isPc = false;

            if (!isPad && !isSP) {
                isPc = true;
            }

            if (isPc) {
                $('#RES_DEVICE_KBN')[0].value = "1";
            }
            if (isSP) {
                $('#RES_DEVICE_KBN')[0].value = "2";
            }
            if (isPad) {
                $('#RES_DEVICE_KBN')[0].value = "3";
            }

        });
        $(function () {
            $('.event-dropdown').on('click', '.click-dropdown', function () {
                $(this).closest('.event-dropdown').toggleClass('close-dropdown').find('.toggle-dropdown').slideToggle();
            });
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="RT_CD" runat="server" />
        <asp:HiddenField ID="S_CD" runat="server" />
        <asp:HiddenField ID="HD_LANG" runat="server" />
        <asp:HiddenField ID="EDIT_TIME" runat="server" />
        <asp:HiddenField ID="ControlId" runat="server" Value="" />
        <asp:HiddenField ID="token" runat="server" />
        <asp:HiddenField ID="shopid" runat="server" />
        <asp:HiddenField ID="syuno_co_cd" runat="server" />
        <asp:HiddenField ID="api_key" runat="server" />
        <asp:HiddenField ID="requestID" runat="server" />
        <asp:HiddenField ID="orderID" runat="server" />
        <asp:HiddenField ID="RES_DEVICE_KBN" runat="server" />
        <asp:HiddenField ID="SESSION_NO" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="masthead">
            <triphoowebHeader:triphoowebHeader ID="triphoowebHeader" runat="Server"></triphoowebHeader:triphoowebHeader>
        </div>
        <div class="breadcrumb-bg">
            <div class="container">
                <%--▼パンくず--%>
                <div class="row">
                    <div class="col-lg-12">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb mb-0">
                                <li class="breadcrumb-item">
                                    <asp:LinkButton ID="PANKUZU001LinkButton" runat="server">
                                        <asp:Label ID="LABEL_0001" runat="server"></asp:Label>
                                    </asp:LinkButton>
                                </li>
                                <li class="breadcrumb-item">
                                    <asp:LinkButton ID="PANKUZU002LinkButton" runat="server">
                                        <asp:Label ID="LABEL_0002" runat="server"></asp:Label>
                                    </asp:LinkButton>

                                </li>
                                <li class="breadcrumb-item active" aria-current="page">
                                    <asp:Label ID="LABEL_0003" runat="server"></asp:Label>
                                </li>
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_0004" runat="server"></asp:Label>
                                </li>
                            </ol>
                        </nav>
                    </div>
                </div>
                <%--▲パンくず--%>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div id="triphoomain" class="col-lg-12">
                    <%--▼ページヘッダー--%>
                    <div class="row pd5">
                        <div class="col-lg-12">
                            <asp:Literal ID="PageHeader" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <%--▲ページヘッダー--%>
                    <div class="row">
                        <div class="col-lg-3">
                            <triphoowebReserveDetail:triphoowebReserveDetail ID="RES_DETAIL" runat="Server"></triphoowebReserveDetail:triphoowebReserveDetail>
                        </div>
                        <div class="col-lg-9">
                            <triphoowebReserveDetailCheck:triphoowebReserveDetailCheck ID="TriphoowebReserveDetailCheck" runat="Server"></triphoowebReserveDetailCheck:triphoowebReserveDetailCheck>
                            <%--▼お申込金--%>
                            <asp:Panel ID="REPORT_MONEYPanel" runat="server">
                                <div class="tab-ttl mb-4">
                                    <h2>申込金情報</h2>
                                </div>
                                <div class="card mb-4 shadow">
                                    <table class="cp_table">
                                        <tbody>
                                            <tr>
                                                <th label="申込金">申込金
                                                </th>
                                                <td class="text-right">
                                                    <asp:Label ID="REPORT_MONEY" runat="server"></asp:Label></td>
                                                <th label="お支払い期限">お支払い期限
                                                </th>
                                                <td class="text-right">
                                                    <asp:Label ID="TEMP_RECEIPT_LIMIT_DATE" runat="server"></asp:Label></td>

                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </asp:Panel>
                            <%--▲お申込金--%>
                            <%--▼お支払方法--%>
                            <asp:Panel ID="PayPanel" runat="server">
                                <div class="tab-ttl mb-4">
                                    <h2>
                                        <asp:Label ID="LABEL_0005" runat="server"></asp:Label></h2>
                                </div>

                                <%-- <div id="travel-paymet-info-wrapper">
                                            <ul id="travel-payment-list">--%>
                                <li id="RES_BANKPanel" runat="server">
                                    <div class="card mb-4 shadow">
                                        <div class="card-header">
                                            <asp:RadioButton ID="RES_BANKRadioButton" GroupName="payMethod" runat="server" />
                                        </div>

                                        <asp:Repeater ID="M074_RT_DEPARTMENT_ACCOUNTRepeater" runat="server">
                                            <ItemTemplate>
                                                <div class="card-body">
                                                    <%# Eval("BANK_NM") %> <%# Eval("BRANCH_NM") %>&nbsp;
                                                                      <%# Eval("ACCOUNT_TYPE_NM") %>：<%# Eval("ACCOUNT_NO") %>&nbsp;
                                                                        口座名：<%# Eval("ACCOUNT_NAME") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="card-body">
                                            <asp:Label ID="LABEL_0092" runat="server"></asp:Label><br>
                                            <asp:Label ID="RECEIPT_LIMIT_DATE" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </li>
                                <li id="RES_CREDIT_REQUESTPanel" runat="server">
                                    <div class="card mb-4 shadow">
                                        <div class="card-header">
                                            <asp:RadioButton ID="RES_CREDIT_REQUESTRadioButton" GroupName="payMethod" runat="server" />
                                        </div>
                                        <div class="card-body">
                                            <asp:Label ID="LABEL_0093" runat="server">※Eメールにて決済手続用URLをお送りします</asp:Label>
                                        </div>

                                    </div>
                                </li>
                                <li id="RES_CREDITPanel" runat="server">
                                    <div class="card mb-4 shadow">
                                        <div class="card-header">
                                            <asp:RadioButton ID="RES_CREDITRadioButton" GroupName="payMethod" runat="server" />
                                        </div>

                                        <div class="card-body">
                                            <asp:Label ID="LABEL_0094" runat="server"></asp:Label><br>
                                            <asp:Label ID="LABEL_0073" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><br />
                                            <asp:Label ID="LABEL_0074" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="card-body">
                                            <asp:Panel ID="CreditDetailPanel" runat="server">
                                                <div class="row" style="display: none">
                                                    <div class="col-lg-12">
                                                        <div id="ErrMsg"></div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <p>
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0006" runat="server"></asp:Label></span>
                                                            <asp:Label ID="LABEL_0007" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <p class="form-group">
                                                            <asp:DropDownList ID="CARD_KIND" runat="server" CssClass="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="form-inline">
                                                            <div class="form-group content-font-padding-9">
                                                                <img id="VISA" runat="server" width="40" src="../../images/common/VISA.jpg" alt="" />
                                                                <img id="AMEX" runat="server" width="40" height="25" src="../../images/common/AMEX.jpg" alt="" />
                                                                <img id="DINERS" runat="server" width="40" height="25" src="../../images/common/DINERS.jpg"
                                                                    alt="" />
                                                                <img id="JCB" runat="server" width="40" height="25" src="../../images/common/JCB.jpg" alt="" />
                                                                <img id="MASTER" runat="server" width="40" height="25" src="../../images/common/MASTER.jpg"
                                                                    alt="" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <p>
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0008" runat="server"></asp:Label></span>
                                                            <asp:Label ID="LABEL_0009" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-7">
                                                        <p class="form-group">
                                                            <asp:TextBox ID="CARD_NO" runat="server" class=" form-control form-control-lg" MaxLength="16" AutoComplete="Off"></asp:TextBox>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <p>
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0010" runat="server"></asp:Label></span>
                                                            <asp:Label ID="LABEL_0011" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-7 mb-3">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="CARD_EXP_YEAR" runat="server" CssClass="form-control form-control-lg">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>/
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="CARD_EXP_MONTH" runat="server" CssClass="form-control form-control-lg">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <p>
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0012" runat="server"></asp:Label></span>
                                                            <asp:Label ID="LABEL_0013" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-7">
                                                        <p class="form-group">
                                                            <asp:TextBox ID="CARD_HOLDER_SURNAME" runat="server" class="form-control form-control-lg"
                                                                MaxLength="50"></asp:TextBox>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <p>
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0014" runat="server"></asp:Label></span>
                                                            <asp:Label ID="LABEL_0015" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-7">
                                                        <p class="form-group">
                                                            <asp:TextBox ID="CARD_HOLDER_NAME" runat="server" class="form-control form-control-lg" MaxLength="50"></asp:TextBox>
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <p>
                                                            <span class="label label-red" style="margin-left: 5px; margin-right: 5px;">
                                                                <asp:Label ID="LABEL_0016" runat="server"></asp:Label></span>
                                                            <asp:Label ID="LABEL_0017" runat="server"></asp:Label>&nbsp;&nbsp; 
                                                               
                                                        </p>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="CARD_SECURITY_CODE" runat="server" class="form-control form-control-lg"
                                                                AutoComplete="Off" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <a onclick="ModalSecurity();" style="cursor: pointer; text-decoration: underline"><font style="font-size: 7pt">
                                                            <asp:Label ID="LABEL_0018" runat="server"></asp:Label>
                                                        </font>
                                                        </a>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="BankCautionPanel" runat="server" Visible="false">
                                                    <p style="color: Red; font-weight: bold;">
                                                        <asp:Label ID="LABEL_0089" runat="server">※銀行振込をご希望のお客様はお手数ですが別途お電話にてご連絡ください</asp:Label>
                                                    </p>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </li>
                                <%--</ul>
                                        </div>--%>
                            </asp:Panel>
                            <%--▲お支払方法--%>
                            <%--▼PEX約款・説明--%>
                            <asp:Panel ID="FARE_BASISPanel" runat="server">
                                <div class="tab-ttl mb-4">
                                    <h2>PEX航空券の名称・運賃種別</h2>
                                </div>
                                <div class="card mb-4 shadow">
                                    <div class="row">
                                        <div class="ml-2 col-lg-12 divPadding" style="padding: 1.25rem;">
                                            <asp:Label ID="FARE_BASIS" runat="server"></asp:Label><br />
                                            (※このコースではPEX運賃を使用しています。旅行契約成立の当日より所定の取り消し料金が発生いたします。)
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PEX_CONDITIONS_Panel" runat="server">
                                <div class="tab-ttl mb-4">
                                    <h2>
                                        <asp:Label ID="LABEL_0090" runat="server"></asp:Label></h2>
                                </div>
                                <div class="card mb-4 shadow">
                                    <div class="row">
                                        <div class="ml-2 col-lg-12 divPadding" style="padding: 1.25rem;">
                                            <asp:Label ID="PEX_CONDITIONS" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%--▲PEX約款・説明--%>
                            <%--▼重要事項--%>
                            <asp:Panel ID="ImportantPanel" runat="server">
                                <div class="tab-ttl mb-4">
                                    <h2>
                                        <asp:Label ID="LABEL_0081" runat="server"></asp:Label></h2>
                                </div>
                                <asp:Repeater ID="ImportantRepeater" runat="server">
                                    <ItemTemplate>
                                        <div class="card mb-4 shadow">
                                            <div class="card-body">
                                                <%# Eval("CONTENTS") %>
                                                <%# IIf(Not Eval("URL") = "", "<a href='" + Eval("URL") + "' target='_blank'>" + Eval("URL") + "</a>", "") %>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <%--▲重要事項--%>
                            <%--▼航空券（手配旅行）規定--%>
                            <asp:Panel ID="TICKET_REGULATIONPanel" runat="server">
                                <asp:Panel ID="TICKET_REGULATION_TEXTPanel" runat="server">
                                    <p class="pdt15 pdl10">
                                        <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0019" runat="server"></asp:Label>
                                    </p>

                                    <div class="card mb-4 shadow">
                                        <div class="card-body" id="TICKET_REGULATION" runat="server" style="overflow-y: scroll; height: 300px; background-color: #FFFFFF"></div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="TICKET_REGULATION_DOWNLOADPanel" runat="server">
                                    <div class="col-lg-12 pdt20">
                                        <div class="text-center alert alert-danger">
                                            <asp:Label ID="LABEL_0023_TK" runat="server"></asp:Label>
                                            <br />
                                            <a id="TICKET_REGULATION_DOWNLOADLinkButton" runat="server" style="font-size: 20px;" target="_blank">
                                                <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0066_TK" runat="server"></asp:Label>
                                            </a>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                            <%--▲航空券（手配旅行）規定--%>
                            <%--▼ホテル（手配旅行）規定--%>
                            <asp:Panel ID="HOTEL_REGULATIONPanel" runat="server">
                                <asp:Panel ID="HOTEL_REGULATION_TEXTPanel" runat="server">
                                    <p class="pdt15 pdl10">
                                        <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0020" runat="server"></asp:Label>
                                    </p>

                                    <div class="card mb-4 shadow">
                                        <div class="card-body" id="HOTEL_REGULATION" runat="server" style="overflow-y: scroll; height: 300px; background-color: #FFFFFF">
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="HOTEL_REGULATION_DOWNLOADPanel" runat="server">
                                    <div class="col-lg-12 pdt20">
                                        <div class="text-center alert alert-danger">
                                            <asp:Label ID="LABEL_0023_HO" runat="server"></asp:Label>
                                            <br />
                                            <a id="HOTEL_REGULATION_DOWNLOADLinkButton" runat="server" style="font-size: 20px;" target="_blank">
                                                <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0066_HO" runat="server"></asp:Label>
                                            </a>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                            <%--▲ホテル（手配旅行）規定--%>
                            <%--▼現地ツアー（手配旅行）規定--%>
                            <asp:Panel ID="OPTION_REGULATIONPanel" runat="server">
                                <asp:Panel ID="OPTION_REGULATION_TEXTPanel" runat="server">
                                    <p class="pdt15 pdl10">
                                        <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0021" runat="server"></asp:Label>
                                    </p>
                                    <div class="card mb-4 shadowr">
                                        <div class="card-body" id="OPTION_REGULATION" runat="server" style="overflow-y: scroll; height: 300px; background-color: #FFFFFF">
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="OPTION_REGULATION_DOWNLOADPanel" runat="server">
                                    <div class="col-lg-12 pdt20">
                                        <div class="text-center alert alert-danger">
                                            <asp:Label ID="LABEL_0023_OP" runat="server"></asp:Label>
                                            <br />
                                            <a id="OPTION_REGULATION_DOWNLOADLinkButton" runat="server" style="font-size: 20px;" target="_blank">
                                                <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0066_OP" runat="server"></asp:Label>
                                            </a>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                            <%--▲現地ツアー（手配旅行）規定--%>
                            <%--▼DP（募集型企画旅行）規定--%>
                            <asp:Panel ID="DP_REGULATIONPanel" runat="server">
                                <asp:Panel ID="DP_REGULATION_TEXTPanel" runat="server">
                                    <p class="pdt15 pdl10">
                                        <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0024" runat="server"></asp:Label>
                                    </p>

                                    <div class="card mb-4 shadow">
                                        <div class="card-body" id="DP_REGULATION" runat="server" style="overflow-y: scroll; height: 300px; background-color: #FFFFFF">
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="DP_REGULATION_DOWNLOADPanel" runat="server">
                                    <div class="col-lg-12 pdt20">
                                        <div class="text-center alert alert-danger">
                                            <asp:Label ID="LABEL_0023_DP" runat="server"></asp:Label>
                                            <br />
                                            <a id="DP_REGULATION_DOWNLOADLinkButton" runat="server" style="font-size: 20px;" target="_blank">
                                                <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0066_DP" runat="server"></asp:Label>
                                            </a>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                            <%--▲DP（募集型企画旅行）規定--%>
                            <%--▼ツアー（募集型企画旅行）規定--%>
                            <asp:Panel ID="TOUR_REGULATIONPanel" runat="server">
                                <p class="pdt15 pdl10">
                                    <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0022" runat="server"></asp:Label>
                                </p>
                                <div class="card mb-4 shadow">
                                    <div class="card-body" id="TOUR_REGULATION" runat="server" style="overflow-y: scroll; height: 300px; background-color: #FFFFFF">
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="ConsentFormPanel" runat="server" Visible="false">
                                <div class="col-lg-12">
                                    <div class="alert alert-danger">
                                        <asp:Label ID="LABEL1" runat="server">
                                            旅行契約時（お申込時）に以下の場合は、親権者（法定代理人）の同意書を必要とします。<br />
                                            <br />
                                            未成年者のみの参加の場合、同行する保護者が親権者（法定代理人）以外の場合、15歳未満の方は保護者の同行が必要です。<br />
                                            保護者とは20歳以上の成人を指します（親権者も含まれます）。<br />
                                            親権者とは原則として父母のことを指します。
                                        </asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%--▲ツアー（募集型企画旅行）規定--%>
                            <asp:Panel ID="TOUR_REGULATION_DOWNLOADPanel" runat="server" CssClass="text-center">

                                <div class="alert alert-danger mt-4">
                                    <asp:Label ID="LABEL_0023" runat="server"></asp:Label>
                                    <div style="color: red;">
                                        <asp:Label ID="LABEL_0120" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <a id="TOUR_REGULATION_DOWNLOADLinkButton" runat="server" style="font-size: 20px;" target="_blank">
                                    <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0066" runat="server"></asp:Label>
                                </a>

                            </asp:Panel>
                            <asp:Panel ID="TOUR_REGULATION_B2BPanel" runat="server" Visible="false">
                                <div class="col-lg-12">
                                    <asp:Literal ID="TOUR_REGULATION_B2B" runat="server">
                                    </asp:Literal>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="TERMS_USEPanel" runat="server" Visible="false" CssClass="text-center">
                                <a id="TERMS_USE_LinkButton" runat="server" href="#" style="font-size: 20px;" target="_blank">
                                    <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0097" runat="server"></asp:Label>
                                </a>
                                <a id="TERMS_USE_ModalButton" runat="server" style="font-size: 20px;" href="#" onclick="$('#TermsUseModal').modal();">
                                    <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0098" runat="server"></asp:Label>
                                </a>
                            </asp:Panel>
                            <asp:Panel ID="CANCELPOLICYPanel" runat="server" Visible="false" CssClass="text-center">

                                <a id="TOUR_CANCELPOLICY_LinkButton" runat="server" href="#" style="font-size: 20px;" target="_blank">
                                    <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0069" runat="server"></asp:Label>
                                </a>
                                <a id="TOUR_CANCELPOLICY_ModalButton" runat="server" style="font-size: 20px;" href="##" onclick="$('#CancelModal').modal();">
                                    <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0072" runat="server"></asp:Label>
                                </a>
                            </asp:Panel>
                            <div class="d-flex justify-content-center">
                                <div class="text-left d-flex flex-column">

                                    <asp:Panel ID="Agree1Panel" runat="server" CssClass="mt-3 text-nowrap">
                                        <asp:CheckBox ID="Agree1CheckBox" runat="server" CssClass="align-top" />
                                        <label class="mb-0 text-wrap d-inline" for="Agree1CheckBox">
                                            <asp:Label ID="LABEL_0084" runat="server"></asp:Label></label>
                                    </asp:Panel>
                                    <asp:Panel ID="Agree2Panel" runat="server" CssClass="mt-3 text-nowrap">
                                        <asp:CheckBox ID="Agree2CheckBox" runat="server" CssClass="align-top" />
                                        <label class="mb-0 text-wrap d-inline" for="Agree2CheckBox">
                                            <asp:Label ID="LABEL_0085" runat="server">.text</asp:Label></label>
                                    </asp:Panel>
                                    <asp:Panel ID="AgreeImportantPanel" runat="server" CssClass="mt-3 text-nowrap">
                                        <asp:CheckBox ID="AgreeImportantCheckBox" runat="server" CssClass="align-top" />
                                        <label class="mb-0 text-wrap d-inline" for="AgreeImportantCheckBox">
                                            <asp:Label ID="LABEL_0088" runat="server"></asp:Label></label>
                                    </asp:Panel>
                                    <asp:Panel ID="AgreeCheckPanel" runat="server" CssClass="mt-3 text-nowrap">
                                        <asp:CheckBox ID="AgreeCheckBox" runat="server" CssClass="align-top" />
                                        <label class="mb-0 text-wrap d-inline" for="AgreeCheckBox">
                                            <asp:Label ID="LABEL_0103" runat="server"></asp:Label></label>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:Panel ID="AgreePanel" runat="server" CssClass="col-lg-12">
                                <br />
                                <p class="text-center">
                                    <font style="color: red;"><b>
                                        <asp:Label ID="LABEL_0025" runat="server"></asp:Label></b></font>
                                </p>
                            </asp:Panel>
                            <%--<div class="col-lg-12">    
                                
                            </div>--%>

                            <%--<asp:Panel ID="Panel1" runat="server">
                                <div class="col-lg-12 pdt20 text-center">
                                    <asp:CheckBox class="text-center" ID="CheckBox1" runat="server" Text="取引条件説明書面（旅行条件書）を電磁的方法で交付することを承諾する" />
                                </div>
                            </asp:Panel>--%>
                            <%-- <asp:Panel ID="Panel2" runat="server">
                                <div class="col-lg-12 pdt20 text-center">
                                    <asp:CheckBox class="text-center" ID="CheckBox2" runat="server" Text="取引条件説明書面（旅行条件書）については、このページの内容（重要事項）及び<br/>ご旅行条件書（共通事項）を確認の上、印刷又はファイル保存した。" />
                                </div>
                            </asp:Panel>--%>
                            <%--<asp:Panel ID="AgreeImportantPanel" runat="server">
                                <div class="col-lg-12 pdt20 text-center">
                                    <asp:CheckBox class="text-center" ID="AgreeImportantCheckBox" runat="server" />
                                </div>
                            </asp:Panel>--%>
                            <%--<asp:Panel ID="AgreeCheckPanel" runat="server">

                                <div class="col-lg-12 pdt20 text-center">
                                    <asp:CheckBox class="text-center" ID="AgreeCheckBox" runat="server" />
                                </div>
                            </asp:Panel--%>

                            <%--▼ページフッター--%>
                            <div class="row pd5">
                                <div class="col-lg-12">
                                    <asp:Literal ID="Pagefooter" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <%--▲ページフッター--%>
                            <div class="row mb-4">
                                <div class="col-lg-6 col-6">
                                    <asp:LinkButton ID="BackLinkButton" runat="server" class="btn btn-block btn-lg-tw btn-primary"></asp:LinkButton>
                                </div>
                                <div class="col-lg-6 col-6">
                                    <asp:Panel ID="RES_COMMIT_PANEL" runat="server" Visible="false">
                                        <asp:LinkButton ID="RES_COMMITLinkButton" runat="server" class="btn btn-block btn-lg-tw btn-danger"
                                            OnClientClick="BookWait();">
                                        </asp:LinkButton>
                                    </asp:Panel>
                                    <asp:Panel ID="RES_COMMIT_TOKEN_PANEL" runat="server" Visible="false">
                                        <%--表示されるボタン--%>
                                        <a id="commit" runat="server" class="btn btn-block btn-lg-tw btn-danger" onclick="return doPurchase();"></a>
                                        <%--アクション用ボタン--%>
                                        <asp:LinkButton ID="RES_COMMIT_TOKENLinkButton" runat="server" OnClientClick="BookWait();">
                                        </asp:LinkButton>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="masthead">
            <triphoowebFooter:triphoowebFooter ID="triphoowebFooter" runat="Server"></triphoowebFooter:triphoowebFooter>
        </div>
        <CreditSecurityCode:CreditSecurityCode ID="CreditSecurityCode" runat="Server"></CreditSecurityCode:CreditSecurityCode>
        <EconTokenPay:EconTokenPay ID="EconTokenPay" runat="Server"></EconTokenPay:EconTokenPay>
        <GmoTokenPay:GmoTokenPay ID="GmoTokenPay" runat="Server"></GmoTokenPay:GmoTokenPay>
        <GmoTokenPaySmbc:GmoTokenPaySmbc ID="GmoTokenPaySmbc" runat="Server"></GmoTokenPaySmbc:GmoTokenPaySmbc>
        <GmoTokenPaySmbcStation:GmoTokenPaySmbcStation ID="GmoTokenPaySmbcStation" runat="Server"></GmoTokenPaySmbcStation:GmoTokenPaySmbcStation>
        <VeriTrans4GTokenPay:VeriTrans4GTokenPay ID="VeriTrans4GTokenPay" runat="Server"></VeriTrans4GTokenPay:VeriTrans4GTokenPay>
        <%-- modal --%>
        <Waiting:Waiting ID="Waiting" runat="Server"></Waiting:Waiting>
        <%--<div class="modal" id="BookModal" tabindex="-1" role="dialog" aria-labelledby="staticModalLabel"
            aria-hidden="true" data-show="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="display:block">
                        <div class="row">
                            <div class="col-lg-2 text-center d-none d-lg-block">
                                <img src='../../images/common/loading.gif' />
                            </div>
                            <div class="col-lg-10">
                                <asp:Label ID="LABEL_0026" runat="server"></asp:Label>
                                <p class="recipient">
                                    <asp:Label ID="LABEL_0027" runat="server"></asp:Label>
                                </p>
                                <p class="recipient">
                                    <asp:Label ID="LABEL_0028" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body d-none d-lg-block">
                        <p class="recipient" id="RT_CONTENTS" runat="server">
                        </p>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="modal fade" id="CancelModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="LABEL_0070" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="CancelPolicyLabel" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 pd20">
                                <a id="A1" class="btn btn-block btn-primary" data-dismiss="modal">
                                    <asp:Label ID="LABEL_0071" runat="server"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="CancelModalAir" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="LABEL11" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label12" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 pd20">
                                <a id="A1" class="btn btn-block btn-primary" data-dismiss="modal">
                                    <asp:Label ID="LABEL13" runat="server"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="CancelModalHotel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="LABEL14" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label15" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 pd20">
                                <a id="A1" class="btn btn-block btn-primary" data-dismiss="modal">
                                    <asp:Label ID="LABEL16" runat="server"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="CancelModalOption" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="LABEL17" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label18" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 pd20">
                                <a id="A1" class="btn btn-block btn-primary" data-dismiss="modal">
                                    <asp:Label ID="LABEL19" runat="server"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="CancelModalDp" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="LABEL20" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label21" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 pd20">
                                <a id="A1" class="btn btn-block btn-primary" data-dismiss="modal">
                                    <asp:Label ID="LABEL22" runat="server"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="TermsUseModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="LABEL_0099" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="TermsUseLabel" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 pd20">
                                <a id="A1" class="btn btn-block btn-primary" data-dismiss="modal">
                                    <asp:Label ID="LABEL_0100" runat="server"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
    <script type="text/javascript" src="../../scripts/jquery.lazyload.min.js?v=1.9.1"></script>
    <script type="text/javascript">
        $(function () {
            $("img.lazy").lazyload({
                effect: "fadeIn"
            });

        });
        $(function () {
            $('#RES_BANKRadioButton').on('click', function () {
                if ($('#RES_CREDITPanel')) {
                    $('#RES_CREDITPanel .card-body').hide();
                    $('#RES_BANKPanel .card-body').show();
                }
            });
            $('#RES_CREDITRadioButton').on('click', function () {
                if ($('#RES_BANKPanel')) {
                    $('#RES_CREDITPanel .card-body').show();
                    $('#RES_BANKPanel .card-body').hide();
                }
            });
        });
    </script>
</body>
</html>
