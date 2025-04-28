<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cart002Back.aspx.vb" EnableViewState="true" ValidateRequest="false"
    EnableEventValidation="false" Inherits="tw.page_cart_cart002Back" %>

<%@ Register TagPrefix="triphoowebHeader" TagName="triphoowebHeader" Src="../common/header.ascx" %>
<%@ Register TagPrefix="triphoowebFooter" TagName="triphoowebFooter" Src="../common/footer.ascx" %>
<%@ Register TagPrefix="TelMe" TagName="TelMe" Src="../common/TelMe.ascx" %>
<%@ Register TagPrefix="cart" TagName="cart" Src="../common/cart.ascx" %>
<%@ Register Src="~/page/common/bookDetail.ascx" TagPrefix="bookDetail" TagName="bookDetail" %>
<%@ Register TagPrefix="Waiting" TagName="Waiting" Src="../common/Waiting.ascx" %>

<!DOCTYPE html>
<html lang="ja">
<head runat="server">
    <meta http-equiv="Content-Style-Type" content="text/css" />
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no,maximum-scale=1" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <title></title>
    <asp:Literal ID="SCRIPTLiteral" runat="server"></asp:Literal>
    <script src="../../scripts/custom/commontw.js?ver=1.02" type="text/javascript"></script>
    <script src="../../scripts/custom/common.js?ver=1.01" type="text/javascript"></script>
    <script type="text/javascript" src="../../scripts/jquery.flexslider-min.js"></script>
    <script type="text/javascript" src="../../scripts/holiday.js"></script>
    <script type="text/javascript" src="/scripts/custom/fromKanaToRomenConvertUtil.js" charset="UTF-8"></script>
    <style type="text/css">
        .ui-datepicker-calendar .ui-datepicker-unselectable span {
            background: #e9e9e9;
            color: #333;
        }

            .ui-datepicker-calendar .ui-datepicker-unselectable span:hover {
                background: #fff;
                color: #333;
            }

        .ui-datepicker-calendar .ui-state-active {
            background: #cff;
        }

        .ui-datepicker-calendar .date-saturday a {
            color: #00f;
        }

        .ui-datepicker-calendar .date-sunday a {
            color: #f00;
        }

        .date-holiday0 .ui-state-default {
            color: #F00;
        }

        /*input:disabled, select:disabled,input:read-only {
            border: none !important;
            box-shadow: none !important;
            background-color: #fff !important;
            -webkit-appearance: none !important;
            -moz-appearance: none !important;
            appearance: none !important;
            color: inherit !important;
            background: none !important;
            opacity: 1.0;
            font-size:inherit;
        }*/
    </style>
    <%--カレンダー--%>
    <script type="text/javascript">
        //カレンダー
        function pageLoad() {
            $('#DEP_DATE').datepicker({
                dateFormat: "yy/mm/dd",
                minDate: '0d',
                maxDate: document.getElementById("MIN_DATE").value,
                beforeShowDay: function (day) {
                    var result;
                    var holidayname = amaitortedays.isNationalHoliday(day);
                    // 祝日・非営業日定義に存在するか？
                    if (holidayname) {
                        result = [true, "date-holiday" + 0, holidayname];
                    } else {
                        switch (day.getDay()) {
                            case 0: // 日曜日か？
                                result = [true, "date-sunday"];
                                break;
                            case 6: // 土曜日か？
                                result = [true, "date-saturday"];
                                break;
                            default:
                                result = [true, ""];
                                break;
                        }
                    }
                    return result;
                }
            });
            $('#RET_DATE').datepicker({
                dateFormat: "yy/mm/dd",
                minDate: document.getElementById("MAX_DATE").value,
                beforeShowDay: function (day) {
                    var result;
                    var holidayname = amaitortedays.isNationalHoliday(day);
                    // 祝日・非営業日定義に存在するか？
                    if (holidayname) {
                        result = [true, "date-holiday" + 0, holidayname];
                    } else {
                        switch (day.getDay()) {
                            case 0: // 日曜日か？
                                result = [true, "date-sunday"];
                                break;
                            case 6: // 土曜日か？
                                result = [true, "date-saturday"];
                                break;
                            default:
                                result = [true, ""];
                                break;
                        }
                    }
                    return result;
                }
            });
            $('#LOCAL_DEP_DATE').datepicker({
                dateFormat: "yy/mm/dd",
                minDate: document.getElementById("MAX_DATE").value,
                beforeShowDay: function (day) {
                    var result;
                    var holidayname = amaitortedays.isNationalHoliday(day);
                    // 祝日・非営業日定義に存在するか？
                    if (holidayname) {
                        result = [true, "date-holiday" + 0, holidayname];
                    } else {
                        switch (day.getDay()) {
                            case 0: // 日曜日か？
                                result = [true, "date-sunday"];
                                break;
                            case 6: // 土曜日か？
                                result = [true, "date-saturday"];
                                break;
                            default:
                                result = [true, ""];
                                break;
                        }
                    }
                    return result;
                }
            });
            $('#LOCAL_ARR_DATE').datepicker({
                dateFormat: "yy/mm/dd",
                minDate: '0d',
                maxDate: document.getElementById("MIN_DATE").value,
                beforeShowDay: function (day) {
                    var result;
                    var holidayname = amaitortedays.isNationalHoliday(day);
                    // 祝日・非営業日定義に存在するか？
                    if (holidayname) {
                        result = [true, "date-holiday" + 0, holidayname];
                    } else {
                        switch (day.getDay()) {
                            case 0: // 日曜日か？
                                result = [true, "date-sunday"];
                                break;
                            case 6: // 土曜日か？
                                result = [true, "date-saturday"];
                                break;
                            default:
                                result = [true, ""];
                                break;
                        }
                    }
                    return result;
                }
            });
        }

        var ZIPCODE1 = function (Json) {
            document.getElementById("INPUT1_MAIN_PREFECTURE").value = Json[0].VALUE;
            document.getElementById("INPUT1_MAIN_ADDRESS").value = Json[0].TEXT;
        }
        function createTag1() {
            var zip = document.getElementById("INPUT1_MAIN_ZIPCODE").value;
            var nzip = "";

            if (document.getElementById("INPUT1_MAIN_ADDRESS").value != '') { return }

            for (var i = 0; i < zip.length; i++) {
                var chr = zip.charCodeAt(i);
                if (chr < 48) { continue };
                if (chr > 57) { continue };
                nzip += zip.charAt(i);
            }
            if (nzip.length < 7) { return };

            var script = document.createElement('script');
            script.src = 'https://www.triphoo.jp/TriphooRMWebService/service/getList.aspx?CALLBACK=ZIPCODE1&ZIPCODE=' + nzip;
            script.type = 'text/javascript';
            document.getElementsByTagName('head')[0].appendChild(script);
        }

        var ZIPCODE = function (Json) {
            document.getElementById("INPUT_MAIN_PREFECTURE").value = Json[0].VALUE;
            document.getElementById("INPUT_MAIN_ADDRESS").value = Json[0].TEXT;
        }

        function createTag() {
            var zip = document.getElementById("INPUT_MAIN_ZIPCODE").value;
            var nzip = "";

            for (var i = 0; i < zip.length; i++) {
                var chr = zip.charCodeAt(i);
                if (chr < 48) { continue };
                if (chr > 57) { continue };
                nzip += zip.charAt(i);
            }
            if (nzip.length < 7) { return };

            var script = document.createElement('script');
            script.src = 'https://www.triphoo.jp/TriphooRMWebService/service/getList.aspx?CALLBACK=ZIPCODE&ZIPCODE=' + nzip;
            script.type = 'text/javascript';
            document.getElementsByTagName('head')[0].appendChild(script);
        }
        function _RegModal() {
            $('#RegModal').modal();
        }

        function doConfirm() {
            var commit = confirm("現在入力されている内容はクリアされます。よろしいですか？");
            if (commit == true) {
                $('#staticModal').modal();
            } else {
                return false;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="RT_CD" runat="server" />
        <asp:HiddenField ID="S_CD" runat="server" />
        <asp:HiddenField ID="HD_LANG" runat="server" />
        <asp:HiddenField ID="MIN_DATE" runat="server" />
        <asp:HiddenField ID="MAX_DATE" runat="server" />
        <asp:Literal ID="DATA_SCRIPT" runat="server"></asp:Literal>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="masthead">
            <triphoowebHeader:triphoowebHeader ID="triphoowebHeader" runat="Server"></triphoowebHeader:triphoowebHeader>
        </div>
        <div class="breadcrumb-bg">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb mb-0">
                                <li class="breadcrumb-item">
                                    <asp:LinkButton ID="PANKUZU001LinkButton" runat="server">
                                        <asp:Label ID="LABEL_0001" runat="server"></asp:Label>
                                    </asp:LinkButton>
                                </li>
                                <li class="breadcrumb-item active" aria-current="page">

                                    <asp:Label ID="LABEL_0002" runat="server"></asp:Label>

                                </li>
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_0003" runat="server"></asp:Label>
                                </li>
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_0004" runat="server"></asp:Label>
                                </li>
                            </ol>
                        </nav>
                    </div>

                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">

                <div class="col-lg-3">
                    <bookDetail:bookDetail ID="bookDetail" runat="server" />
                </div>
                <div id="triphoomain" class="col-lg-9">
                    <%--▼ページヘッダー--%>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Literal ID="PageHeader" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <%--▲ページヘッダー--%>

                    <div class="row">
                        <div class="col-lg-12" style="padding: 0px 5px 10px 5px;">
                            <div class="row">
                                <asp:Label ID="LABEL_0005" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <%--提携先情報--%>
                    <asp:Panel ID="PARTNERSPanel" runat="server" Visible="false">
                        <div class="tab-ttl">
                            <h2>提携先情報</h2>
                        </div>
                        <div class="card mb-3 shadow">
                            <div class="card-header">

                                <span class="label label-red mr-2">必須</span>E-mailアドレス
                            
                            </div>
                            <div class="card-body">

                                <asp:TextBox ID="PARTNERS_E_MAIL" runat="server" class="form-control form-control-lg" MaxLength="200"></asp:TextBox>

                            </div>

                            <div class="card-header">

                                <span class="label label-red mr-2">必須</span>ご担当者様名
                                
                            </div>
                            <div class="card-body">
                                <asp:TextBox ID="PARTNERS_EMP_NM" runat="server" class="form-control form-control-lg" MaxLength="200"></asp:TextBox>
                            </div>

                            <div class="card-header">
                                宴席番号
                         
                            </div>
                            <div class="card-body">
                                <p class="input-group">
                                    <asp:TextBox ID="PARTNERS_AGENT_RES_NO" runat="server" class="form-control form-control-lg" MaxLength="7"></asp:TextBox>
                                    <asp:LinkButton ID="GET_CLIENT_INFO1LinkButton" runat="server" CssClass="btn btn-block btn-primary" Width="120px" OnClientClick="return doConfirm();" Visible="false">顧客取込</asp:LinkButton>
                                </p>
                            </div>

                            <div class="card-header">
                                顧客コード
                            
                            </div>
                            <div class="card-body">
                                <p class="input-group">
                                    <asp:TextBox ID="PARTNERS_CLIENT_CD" runat="server" class="form-control form-control-lg" MaxLength="8"></asp:TextBox>
                                    <asp:LinkButton ID="GET_CLIENT_INFO3LinkButton" runat="server" CssClass="btn btn-block btn-primary" Width="120px" OnClientClick="return doConfirm();" Visible="false">顧客取込</asp:LinkButton>
                                </p>
                            </div>

                            <div class="card-header">
                                電話番号
                           
                            </div>
                            <div class="card-body">
                                <p class="input-group">
                                    <asp:TextBox ID="PARTNERS_TEL_NO" runat="server" class="form-control form-control-lg" MaxLength="200"></asp:TextBox>
                                </p>
                            </div>

                        </div>
                    </asp:Panel>
                    <%--代理店情報（TWC）--%>
                    <asp:Panel ID="AGENCYPanel" runat="server" Visible="false">
                        <div class="tab-ttl">
                            <h2>代理店情報</h2>
                        </div>
                        <div class="card mb-3 shadow">
                            <div class="card-header">
                                代理店名
                               
                            <div class="card-body">

                                <asp:Label ID="AGENCY_NAME" runat="server"></asp:Label>

                            </div>

                                <div class="card-header">
                                    住所
                              
                                </div>
                                <div class="card-body">

                                    <asp:Label ID="AGENCY_ADDRESS" runat="server"></asp:Label>

                                </div>

                                <div class="card-header">
                                    電話番号
                                
                                </div>
                                <div class="card-body">

                                    <asp:Label ID="AGENCY_TEL_NO" runat="server"></asp:Label>

                                </div>

                                <div class="card-header">
                                    FAX
                              
                                </div>
                                <div class="card-body">

                                    <asp:Label ID="AGENCY_FAX_NO" runat="server"></asp:Label>
                                  
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <%--販売店様情報--%>
                    <div class="tab-ttl mb-4">
                        <h2>
                            <asp:Label ID="LABEL_0006" runat="server"></asp:Label>
                        </h2>
                    </div>
                    <p>
                        <asp:Label ID="LABEL_0008" runat="server"></asp:Label></p>
                    <%--旅行者情報--%>
                    <div class="card mb-3 shadow">
                        <asp:Panel ID="MAIN_PERSON_INPUTPanel" CssClass="listLine" runat="server">

                            <div class="card-header">
                                <span class="label label-red mr-2">
                                    <asp:Label ID="LABEL_0009" runat="server"></asp:Label></span>
                                <asp:Label ID="LABEL_0010" runat="server"></asp:Label>
                            </div>
                            <div class="card-body">
                                <asp:Label ID="INPUT_MAIN_E_MAILLabel" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="INPUT_MAIN_E_MAIL" runat="server" class="form-control form-control-lg" MaxLength="200"></asp:TextBox>

                            </div>
                            <asp:Panel ID="INPUT_MAIN_E_MAIL_CONFPanel" runat="server">
                                <div class="card-header">
                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0179" runat="server"></asp:Label></span><asp:Label ID="LABEL_0178" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="INPUT_MAIN_E_MAIL_CONF" runat="server" class="form-control form-control-lg" MaxLength="200"></asp:TextBox>
                                    <div class="alert alert-danger mt-3" role="alert">
                                        <b>
                                            <asp:Label ID="LABEL_0181" runat="server"></asp:Label>
                                        </b>
                                        <br />
                                        <br />
                                        <b>
                                            <asp:Label ID="LABEL_0182" runat="server"></asp:Label>
                                        </b>
                                        <br />
                                        <asp:Label ID="LABEL_0183" runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="LABEL_0184" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <b>
                                            <asp:Label ID="LABEL_0185" runat="server"></asp:Label></b>
                                        <br />
                                        <asp:Label ID="LABEL_0186" runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="LABEL_0187" runat="server"></asp:Label>
                                        <br />
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="card-header">
                                <span class="label label-red mr-2">
                                    <asp:Label ID="LABEL_0011" runat="server"></asp:Label></span><asp:Label ID="LABEL_0012" runat="server"></asp:Label>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <asp:Panel ID="INPUT_MAIN_TEL_NOPanel" runat="server" CssClass="col-lg-5 col-9">
                                        <asp:TextBox ID="INPUT_MAIN_TEL_NO" runat="server" class="form-control form-control-lg" MaxLength="200"></asp:TextBox>
                                    </asp:Panel>
                                    <asp:Panel ID="INPUT_MAIN_TEL_NO_SEPARATEPanel" runat="server" CssClass="col-lg-6 col-12" Visible="false">
                                        <div class="input-group" style="margin-bottom: 0px;">
                                            <asp:TextBox ID="INPUT_MAIN_TEL_NO_1" runat="server" class="form-control form-control-lg" MaxLength="5" Style="ime-mode: disabled">
                                            </asp:TextBox>
                                            <span class="pt-2">&nbsp;ー&nbsp;</span>
                                            <asp:TextBox ID="INPUT_MAIN_TEL_NO_2" runat="server" class="form-control form-control-lg" MaxLength="5" Style="ime-mode: disabled">
                                            </asp:TextBox>
                                            <span class="pt-2">&nbsp;ー&nbsp;</span>
                                            <asp:TextBox ID="INPUT_MAIN_TEL_NO_3" runat="server" class="form-control form-control-lg" MaxLength="5" Style="ime-mode: disabled">
                                            </asp:TextBox>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:Panel ID="INPUT_MAIN_ADDRESSPanel" runat="server" Visible="false">
                                <div class="card-header">

                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0013" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0014" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-5 col-12">
                                            <asp:TextBox ID="INPUT_MAIN_ZIPCODE" runat="server" class="form-control form-control-lg"
                                                MaxLength="7" onkeyup="createTag();"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-7 col-12">
                                            <asp:Panel ID="INPUT_MAIN_ZIPCODE_MESSAGE" runat="server">
                                                <span>
                                                    <asp:Label ID="LABEL_0015" runat="server"></asp:Label></span>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                                <div class="card-header">

                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0016" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0017" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:DropDownList ID="INPUT_MAIN_PREFECTURE" runat="server" class="form-control form-control-lg">
                                    </asp:DropDownList>
                                </div>

                                <div class="card-header">

                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0018" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0019" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="INPUT_MAIN_ADDRESS" runat="server" class="form-control form-control-lg"
                                        MaxLength="200"></asp:TextBox>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="INPUT_MAIN_SEXPanel" runat="server">
                                <div class="card-header">

                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0020" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0021" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:RadioButton GroupName="INPUT_MAIN_SEX_FLG" ID="INPUT_MAIN_MAN" runat="server" />
                                    <asp:RadioButton GroupName="INPUT_MAIN_SEX_FLG" ID="INPUT_MAIN_WOMAN" runat="server" />
                                    <asp:Label ID="INPUT_MAIN_SEX_FLG_LABEL" runat="server" Visible="false" CssClass="pd10"></asp:Label>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="INPUT_MAIN_BIRTHPanel" runat="server">
                                <div class="card-header">

                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0022" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0023" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="input-group" style="width: 300px; margin-bottom: 0px;">
                                        <asp:TextBox ID="INPUT_MAIN_BIRTH_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4" Style="width: 140px; ime-mode: disabled">
                                        </asp:TextBox>
                                        <asp:DropDownList ID="INPUT_MAIN_BIRTH_MM" Style="width: 80px; ime-mode: disabled" runat="server" class="form-control form-control-lg">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="INPUT_MAIN_BIRTH_DD" Style="width: 80px; ime-mode: disabled" runat="server" class="form-control form-control-lg">
                                        </asp:DropDownList>
                                        <asp:Label ID="INPUT_MAIN_BIRTH_LABEL" runat="server" Visible="false" CssClass="pd10"></asp:Label>
                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="INPUT_MAIN_SURNAME_KANJIPanel" runat="server" Visible="false">
                                <div class="card-header">

                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0024" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0025" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">
                                                        <asp:Label ID="LABEL_0026" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="INPUT_MAIN_SURNAME_KANJI" runat="server" class="form-control form-control-lg"
                                                    MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">
                                                        <asp:Label ID="LABEL_0027" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="INPUT_MAIN_NAME_KANJI" runat="server" class="form-control form-control-lg"
                                                    MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="INPUT_MAIN_NAME_KANAPanel" runat="server" Visible="false">
                                <div class="card-header">

                                    <span class="label label-red mr-2">必須</span>
                                    姓名:カナ (全角カナ)
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">セイ
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="INPUT_MAIN_SURNAME_KANA" runat="server" class="form-control form-control-lg" MaxLength="100" placeholder="例：ヤマダ" onblur="$('.mainSurNameKana').val(convertKanaToRome(this.value));"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">メイ
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="INPUT_MAIN_NAME_KANA" runat="server" class="form-control form-control-lg" MaxLength="100" placeholder="例：タロウ" onblur="$('.mainNameKana').val(convertKanaToRome(this.value));"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </asp:Panel>
                            <asp:Panel ID="INPUT_MAIN_NAME_ROMANPanel" runat="server" Visible="false">
                                <div class="card-header">

                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0028" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0029" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">
                                                        <asp:Label ID="LABEL_0030" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="INPUT_MAIN_SURNAME_ROMAN" runat="server" class="form-control form-control-lg mainSurNameKana"
                                                    Style="text-transform: uppercase; ime-mode: disabled" MaxLength="100"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">
                                                        <asp:Label ID="LABEL_0031" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="INPUT_MAIN_NAME_ROMAN" runat="server" class="form-control form-control-lg mainNameKana"
                                                    Style="text-transform: uppercase; ime-mode: disabled" MaxLength="100"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </asp:Panel>
                            <asp:Panel ID="COUPON_CDPanel" runat="server" Visible="false">

                                <div class="card-header">
                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0032" runat="server"></asp:Label>
                                    </span>
                                    <asp:Label ID="LABEL_0033" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="COUPON_CD" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled;" MaxLength="100"></asp:TextBox>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="MAIL_MAGAZINE_FLGPanel" runat="server" CssClass="row panelUsael content-panel-24" Style="margin-left: 0px; margin-right: 0px" Visible="false">
                                <div class="card-header">
                                    <span class="label label-red mr-2">必須</span>
                                    <asp:Label ID="LABEL2" runat="server">メールマガジン配信</asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:RadioButton ID="MAIL_MAGAZINE_FLG_01" runat="server" GroupName="MAIL_MAGAZINE_FLG" Text="希望します" Checked="true" />
                                    <asp:RadioButton ID="MAIL_MAGAZINE_FLG_02" runat="server" GroupName="MAIL_MAGAZINE_FLG" Text="希望しません" />
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="MAIN_PERSON_LABELPanel" CssClass="listLine" runat="server">

                            <div class="card-header">

                                <asp:Label ID="LABEL_0034" runat="server"></asp:Label>

                            </div>
                            <div class="card-body">
                                <asp:Label ID="MAIN_E_MAIL" runat="server"></asp:Label>
                                <asp:TextBox ID="INPUT_MAIN_E_MAIL_B2B" runat="server" class="form-control form-control-lg" MaxLength="200" Visible="false"></asp:TextBox>
                            </div>

                            <asp:Panel ID="PORTAL_RES_NOPanel" runat="server" Visible="false">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0035" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="PORTAL_RES_NOTextBox" runat="server" class="form-control form-control-lg" MaxLength="20"></asp:TextBox>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="RES_CONFIRMATION_TOPanel" runat="server" Visible="false">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0036" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="RES_CONFIRMATION_TO" runat="server" class="form-control form-control-lg" MaxLength="50"></asp:TextBox>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="MAIN_TEL_NOPanel" runat="server">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0037" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_TEL_NO" runat="server"></asp:Label>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LABEL_MAIN_SURNAME_KANJIPanel" runat="server">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0038" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_SURNAME_KANJI" runat="server"></asp:Label>&nbsp;<asp:Label ID="MAIN_NAME_KANJI"
                                        runat="server"></asp:Label>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LABEL_MAIN_SURNAME_KANAPanel" runat="server" Visible="false">

                                <div class="card-header">
                                    姓名:カナ
                                  
                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_SURNAME_KANA" runat="server"></asp:Label>&nbsp;<asp:Label ID="MAIN_NAME_KANA"
                                        runat="server"></asp:Label>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LABEL_MAIN_SURNAME_ROMANPanel" runat="server" Visible="false">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0039" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_SURNAME_ROMAN" runat="server"></asp:Label>&nbsp;<asp:Label ID="MAIN_NAME_ROMAN" runat="server"></asp:Label>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LABEL_MAIN_SEXPanel" runat="server">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0040" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_SEX_FLG" runat="server"></asp:Label>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LABEL_MAIN_BIRTHPanel" runat="server">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0041" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_BIRTH" runat="server"></asp:Label>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="INPUT1_MAIN_ADDRESSPanel" runat="server" Visible="false">

                                <div class="card-header">

                                    <asp:Label ID="LABEL_0042" runat="server"></asp:Label>

                                </div>
                                <div class="card-body">
                                    <div class="col-lg-5 col-9">
                                        <asp:TextBox ID="INPUT1_MAIN_ZIPCODE" runat="server" class="form-control form-control-lg"
                                            MaxLength="7" onkeyup="createTag1();"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-7">
                                        <span style="color: red; font-size: 10px; padding-left: 5px; line-height: 34px;">
                                            <asp:Label ID="LABEL_0043" runat="server"></asp:Label>
                                        </span>
                                    </div>
                                </div>


                                <div class="card-header">
                                    <asp:Label ID="LABEL_0044" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="col-lg-5 col-9">
                                        <asp:DropDownList ID="INPUT1_MAIN_PREFECTURE" runat="server" class="form-control form-control-lg">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="card-header">
                                    <asp:Label ID="LABEL_0045" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="INPUT1_MAIN_ADDRESS" runat="server" class="form-control form-control-lg"
                                        MaxLength="200"></asp:TextBox>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LABEL_MAIN_ADDRESSPanel" runat="server" Visible="false">
                                <div class="card-header">

                                    <asp:Label ID="LABEL_0046" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_ZIPCODE" runat="server"></asp:Label>
                                </div>

                                <div class="card-header">
                                    <asp:Label ID="LABEL_0047" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_PREFECTURE" runat="server"></asp:Label>
                                </div>

                                <div class="card-header">
                                    <asp:Label ID="LABEL_0048" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:Label ID="MAIN_ADDRESS" runat="server"></asp:Label>
                                </div>

                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="MAIN_PERSON_TOUR_SERVICEPanel" runat="server" CssClass="mgr5 mgl5" Visible="false">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Repeater ID="MAIN_PERSON_TOUR_SERVICERepeater" runat="server" Visible="false">
                                        <ItemTemplate>
                                            <div class="card-header">
                                                <asp:Label ID="SERVICE_CD" runat="server" Text='<%# Eval("SERVICE_CD")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="SELECT_WAY_KBN" runat="server" Text='<%# Eval("SELECT_WAY_KBN")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="SELECT_KBN" runat="server" Text='<%# Eval("SELECT_KBN")%>' Visible="false"></asp:Label>

                                                <%# IIf(Eval("SELECT_KBN") = "1", "<span class='label label-red' style='margin-left: 5px; margin-right: 5px;'>必須</span>", "<span class='label label-green' style='margin-left: 5px; margin-right: 5px;'>任意</span>") %>
                                                <asp:Label ID="SERVICE_NM" runat="server" Text='<%# Eval("SERVICE_NM")%>'></asp:Label>
                                                <%# IIf(Eval("SELECT_WAY_KBN") = "2", "<br/>(複数選択可)", "")%>
                                                <%# IIf(Not Eval("SERVICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("SERVICE_DISP_REMARKS").ToString & "</p>", "") %>
                                            </div>
                                            <div class="card-body">
                                                <asp:Panel ID="RES_SERVICE_TOUR_CHOICEPanel" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-6 pd10">
                                                            <asp:DropDownList ID="SERVICE_PRICE_CD_DROPDOWNLIST" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Repeater ID="RES_SERVICE_TOUR_CHECKLISTRepeater" runat="server" Visible="false">
                                                    <ItemTemplate>
                                                        <div class="row" style="padding: 5px 0px 5px 0px;">
                                                            <div class="col-lg-12 pdt5">
                                                                <asp:Label ID="SERVICE_PRICE_CD" runat="server" Text='<%# Eval("SERVICE_PRICE_CD")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="SERVICE_PRICE_NM" runat="server" Text='<%# Eval("SERVICE_PRICE_NM")%>' Visible="false"></asp:Label>
                                                                <asp:CheckBox ID="SERVICE_PRICE_CD_CHECKBOX" runat="server" />
                                                                <asp:Label ID="SERVICE_PRICE_NM_DISP" runat="server"></asp:Label>
                                                                <%# IIf(Not Eval("PRICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("PRICE_DISP_REMARKS").ToString & "</p>", "") %>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Panel ID="RES_SERVICE_TOUR_INPUTPanel" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-6 pd10">
                                                            <asp:TextBox ID="RES_SERVICE_TOUR_INPUT" runat="server" CssClass='form-control form-control-lg'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="MAIN_PERSON_LABEL2Panel" runat="server">
                            <div class="row mt-2">
                                <div class="col-7 col-lg-7">
                                    &nbsp;
                                </div>
                                <div class="col-5 col-lg-5">
                                    <asp:LinkButton ID="MAIN_PERSON_CHANGELinkButton" runat="server" CssClass="btn btn-block btn-primary"></asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <asp:Panel ID="PASSENGERPanel" CssClass="pdt20" runat="server">
                        <div class="tab-ttl mb-4">
                            <h2>
                                <asp:Label ID="LABEL_0049" runat="server"></asp:Label></h2>
                        </div>
                        <div class="mb-4">
                            <span class="label label-red mr-2">
                                <asp:Label ID="LABEL_0050" runat="server"></asp:Label></span>
                            <asp:Label ID="LABEL_0051" runat="server"></asp:Label>
                        </div>
                        <asp:Panel ID="PASSPORT_MESSAGEPanel" runat="server">
                            <div class="alert alert-danger" role="alert">
                                <asp:Label ID="LABEL_0052" runat="server"></asp:Label>
                            </div>
                        </asp:Panel>
                        <%--ホテル以外用--%>
                        <asp:Panel ID="PASSENGER_OTHERPanel" runat="server" Visible="false">
                             <div class="card mb-3 shadow">
                            <asp:Repeater ID="PASSENGER_OTHERRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="card-header">
                                        <span class="label label-red mr-2"><%= LABEL_0053 %></span>

                                        <asp:Label ID="AGE_KBN" runat="server" Text='<%# Eval("AGE_KBN") %>' Visible="false"></asp:Label>
                                        <span class="label label-info">
                                            <asp:Label ID="NAME_NO" runat="server" Text='<%# Eval("NAME_NO")%>' Visible="false"></asp:Label><%# Eval("NAME_NO_NM") %>&nbsp;(<%# Eval("AGE_KBN_NM")%>)</span>
                                    </div>
                                    <div class="card-body">

                                        <asp:Panel ID="NAME_KANJIPanel" runat="server" CssClass="row">
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;">姓
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="FAMILY_NAME_KANJI" runat="server" class="form-control form-control-lg"
                                                        MaxLength="50" Text='<%# Eval("FAMILY_NAME_KANJI")%>'></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;">名
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="FIRST_NAME_KANJI" runat="server" class="form-control form-control-lg"
                                                        MaxLength="50" Text='<%# Eval("FIRST_NAME_KANJI")%>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="NAME_KANAPanel" runat="server" CssClass="row">
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;"><%= LABEL_0196 %>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="FAMILY_NAME_KANA" runat="server" class="form-control form-control-lg" onblur="RepeaterItemConvert(this);"
                                                        MaxLength="50" Text='<%# Eval("FAMILY_NAME_KANA")%>' placeholder="例：ヤマダ"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;"><%= LABEL_0197 %>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="FIRST_NAME_KANA" runat="server" class="form-control form-control-lg" onblur="RepeaterItemConvert(this);"
                                                        MaxLength="50" Text='<%# Eval("FIRST_NAME_KANA")%>' placeholder="例：タロウ"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="NAME_ROMANPanel" runat="server" CssClass="row flex-lg-nowrap">
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;"><%= LABEL_0055 %>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="SURNAME_ROMAN" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                        MaxLength="50" Text='<%# Eval("FAMILY_NAME")%>'></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;"><%= LABEL_0057 %>
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="NAME_ROMAN" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                        MaxLength="50" Text='<%# Eval("FIRST_NAME")%>'></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:Panel ID="MIDDLE_NMPanel" runat="server" CssClass="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;"><%= LABEL_0195 %></span>
                                                    </div>
                                                    <asp:TextBox ID="MIDDLE_NM" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                        MaxLength="50" Text='<%# SetRRValue.setNothingValueWeb(Eval("MIDDLE_NM"))%>'></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </asp:Panel>
                                        <div class="row mb-3">
                                            <asp:Panel ID="SEX_FLGPanel" runat="server" CssClass="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;"><%= LABEL_0061 %></span>
                                                    </div>
                                                    <div class="form-control form-control-lg fs16">
                                                        <asp:RadioButton GroupName="SEX_FLG" ID="MAN" runat="server" />
                                                        <%=LABEL_0062%>
                                                        <asp:RadioButton GroupName="SEX_FLG" ID="WOMAN" runat="server" />
                                                        <%=LABEL_0063%>
                                                        <asp:RadioButton GroupName="SEX_FLG" ID="NOANSWER" runat="server" Text='回答しない' Visible="false" />
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <asp:Panel ID="BIRTHPanel" runat="server" CssClass="col-lg-6">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;">
                                                            <asp:Label ID="BIRTHLabel" runat="server"></asp:Label></span>
                                                    </div>
                                                    <asp:TextBox ID="BIRTH_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4" Style="ime-mode: disabled">
                                                    </asp:TextBox>
                                                    <asp:DropDownList ID="BIRTH_MM" runat="server" class="form-control form-control-lg">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="BIRTH_MM_TEXT" runat="server" class="form-control form-control-lg" MaxLength="4" ReadOnly="true" Visible="false">
                                                    </asp:TextBox>
                                                    <asp:DropDownList ID="BIRTH_DD" runat="server" class="form-control form-control-lg">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="BIRTH_DD_TEXT" runat="server" class="form-control form-control-lg" MaxLength="4" ReadOnly="true" Visible="false">
                                                    </asp:TextBox>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                        <asp:Panel ID="NATIONALITYPanel" runat="server">
                                            <div class="input-group col-lg-6">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;"><%=LABEL_0064%></span>
                                                </div>
                                                <asp:DropDownList ID="NATIONALITY" runat="server" class="form-control form-control-lg">
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PASSPORT_NOPanel" runat="server" Visible="false">
                                            <div class="alert alert-secondary" role="alert">
                                                <div class="row">
                                                    <div class="col-lg-12 pb-2">
                                                        <%=LABEL_0065%>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">
                                                                    <%=LABEL_0066%></span>
                                                            </div>

                                                            <asp:TextBox ID="PASSPORT_NO" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                                MaxLength="15" Text='<%# Eval("PASSPORT_NO")%>'></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="PASSPORT_DATEPanel" runat="server" CssClass="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;"><%=LABEL_0067%></span>
                                                            </div>
                                                            <asp:TextBox ID="PASSPORT_DATE_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4">
                                                            </asp:TextBox>
                                                            <asp:DropDownList ID="PASSPORT_DATE_MM" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="PASSPORT_DATE_DD" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="PASSPORT_LIMITPanel" runat="server" CssClass="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;"><%=LABEL_0068%></span>
                                                            </div>
                                                            <asp:TextBox ID="PASSPORT_LIMIT_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4">
                                                            </asp:TextBox>
                                                            <asp:DropDownList ID="PASSPORT_LIMIT_MM" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="PASSPORT_LIMIT_DD" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="PASSPORT_ISSUE_COUNTRYPanel" runat="server" CssClass="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;"><%=LABEL_0069%></span>
                                                            </div>
                                                            <asp:DropDownList ID="PASSPORT_ISSUE_COUNTRY" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="VISAPanel" runat="server" Visible="false">
                                            <div class="alert alert-secondary" role="alert">
                                                <div class="row">
                                                    <div class="col-lg-12 pb-2">
                                                        ＶＩＳＡ(任意）
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">番号</span>
                                                            </div>
                                                            <asp:TextBox ID="VISA_NO" runat="server" class="form-control form-control-lg" MaxLength="30">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">有効期間</span>
                                                            </div>
                                                            <asp:TextBox ID="VISA_VALID_DATE_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4">
                                                            </asp:TextBox>
                                                            <asp:DropDownList ID="VISA_VALID_DATE_MM" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="VISA_VALID_DATE_DD" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">発行国</span>
                                                            </div>

                                                            <asp:DropDownList ID="VISA_COUNTRY_CD" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">参加国</span>
                                                            </div>
                                                            <asp:DropDownList ID="VISA_JOIN_COUNTRY_CD" runat="server" class="form-control form-control-lg">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="MILEAGEPanel" runat="server" Visible="false">
                                            <div class="alert alert-secondary" role="alert">
                                                <div class="row">
                                                    <div class="col-lg-12 pb-2">
                                                        マイレージ〔任意）
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">航空会社</span>
                                                            </div>
                                                            <asp:DropDownList ID="MILEAGE_CARRIER" runat="server" class="form-control form-control-lg">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                <asp:ListItem Value="A3">エーゲ航空</asp:ListItem>
                                                                <asp:ListItem Value="AC">エアーカナダ</asp:ListItem>
                                                                <asp:ListItem Value="CA">中国国際航空</asp:ListItem>
                                                                <asp:ListItem Value="AI">エアーインディア</asp:ListItem>
                                                                <asp:ListItem Value="NZ">ニュージーランド航空</asp:ListItem>
                                                                <asp:ListItem Value="NH">ANA</asp:ListItem>
                                                                <asp:ListItem Value="OZ">アシアナ航空</asp:ListItem>
                                                                <asp:ListItem Value="OS">オーストリア航空</asp:ListItem>
                                                                <asp:ListItem Value="AV">アビアンカ航空</asp:ListItem>
                                                                <asp:ListItem Value="SN">ブリュッセル航空</asp:ListItem>
                                                                <asp:ListItem Value="CM">コパ航空</asp:ListItem>
                                                                <asp:ListItem Value="OU">クロアチア航空</asp:ListItem>
                                                                <asp:ListItem Value="MS">エジプト航空</asp:ListItem>
                                                                <asp:ListItem Value="ET">エチオピア航空</asp:ListItem>
                                                                <asp:ListItem Value="BR">エバー航空</asp:ListItem>
                                                                <asp:ListItem Value="LO">LOTポーランド航空</asp:ListItem>
                                                                <asp:ListItem Value="LH">ルフトハンザ航空</asp:ListItem>
                                                                <asp:ListItem Value="SK">スカンジナビア航空</asp:ListItem>
                                                                <asp:ListItem Value="ZH">深圳航空</asp:ListItem>
                                                                <asp:ListItem Value="SQ">シンガポール航空</asp:ListItem>
                                                                <asp:ListItem Value="SA">南アフリカ航空</asp:ListItem>
                                                                <asp:ListItem Value="LX">スイスエアラインズ</asp:ListItem>
                                                                <asp:ListItem Value="TP">TAP ポルトガル航空</asp:ListItem>
                                                                <asp:ListItem Value="TH">タイ国際航空</asp:ListItem>
                                                                <asp:ListItem Value="TK">ターキッシュエアラインズ</asp:ListItem>
                                                                <asp:ListItem Value="UA">ユナイテッド航空</asp:ListItem>
                                                                <asp:ListItem Value="SU">アエロフロートロシア航空</asp:ListItem>
                                                                <asp:ListItem Value="AR">アルゼンチン航空</asp:ListItem>
                                                                <asp:ListItem Value="AM">アエロメヒコ航空</asp:ListItem>
                                                                <asp:ListItem Value="UX">エア・ヨーロッパ</asp:ListItem>
                                                                <asp:ListItem Value="AF">エールフランス</asp:ListItem>
                                                                <asp:ListItem Value="AZ">アリタリア航空</asp:ListItem>
                                                                <asp:ListItem Value="CI">チャイナエアライン</asp:ListItem>
                                                                <asp:ListItem Value="MU">中国東方航空</asp:ListItem>
                                                                <asp:ListItem Value="OK">チェコ航空</asp:ListItem>
                                                                <asp:ListItem Value="DL">デルタ航空</asp:ListItem>
                                                                <asp:ListItem Value="GA">ガルーダ・インドネシア航空</asp:ListItem>
                                                                <asp:ListItem Value="KQ">ケニア航空</asp:ListItem>
                                                                <asp:ListItem Value="KL">KLMオランダ航空</asp:ListItem>
                                                                <asp:ListItem Value="KE">大韓航空</asp:ListItem>
                                                                <asp:ListItem Value="ME">ミドル・イースト航空</asp:ListItem>
                                                                <asp:ListItem Value="SV">サウディア</asp:ListItem>
                                                                <asp:ListItem Value="RO">タロム航空</asp:ListItem>
                                                                <asp:ListItem Value="VN">ベトナム航空</asp:ListItem>
                                                                <asp:ListItem Value="MF">厦門航空</asp:ListItem>
                                                                <asp:ListItem Value="AA">アメリカン航空</asp:ListItem>
                                                                <asp:ListItem Value="BA">ブリティシュエアウェイズ</asp:ListItem>
                                                                <asp:ListItem Value="CX">キャセイパシフィック航空</asp:ListItem>
                                                                <asp:ListItem Value="AY">フィンエアー</asp:ListItem>
                                                                <asp:ListItem Value="IB">イベリア航空</asp:ListItem>
                                                                <asp:ListItem Value="JL">日本航空</asp:ListItem>
                                                                <asp:ListItem Value="MH">マレーシア航空</asp:ListItem>
                                                                <asp:ListItem Value="QF">カンタス航空</asp:ListItem>
                                                                <asp:ListItem Value="QR">カタール航空</asp:ListItem>
                                                                <asp:ListItem Value="AT">ロイヤル・エア・モロッコ</asp:ListItem>
                                                                <asp:ListItem Value="RJ">ロイヤル・ヨルダン航空</asp:ListItem>
                                                                <asp:ListItem Value="S7">S7航空</asp:ListItem>
                                                                <asp:ListItem Value="UL">スリランカ航空</asp:ListItem>
                                                                <asp:ListItem Value="FJ">フィジー・エアウェイズ</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">番号</span>
                                                            </div>
                                                            <asp:TextBox ID="MILEAGE_NO" runat="server" class="form-control form-control-lg" MaxLength="50">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="SEAT_REQUESTPanel" runat="server" Visible="false">
                                            <div class="alert alert-secondary" role="alert">
                                                <div class="row">
                                                    <div class="col-lg-12 pb-2">
                                                        シート希望（任意）
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="input-group">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text" style="width: 100px;">希望場所</span>
                                                            </div>
                                                            <asp:DropDownList ID="SEAT_REQUEST" runat="server" class="form-control form-control-lg">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Value="窓際">窓際</asp:ListItem>
                                                                <asp:ListItem Value="通路側">通路側</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <asp:Panel ID="RES_SERVICEPanel" runat="server" CssClass="col-lg-12" Visible="false">
                                        <div class="card-header">
                                            付帯サービス(任意)
                                        </div>
                                        <div class="card-body">
                                            <asp:Repeater ID="RES_SERVICERepeater" runat="server">
                                                <ItemTemplate>
                                                    <div class="row pdb10">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="SEGMENT_SEQ" runat="server" Text='<%# Eval("SEGMENT_SEQ")%>' Visible="false"></asp:Label>
                                                            <div class="alert alert-secondary ng-binding" role="alert" style="padding: 5px 0px 5px 12px; letter-spacing: 0">
                                                                <div class="row pd5">
                                                                    <div class="col-lg-4 col-12">
                                                                        <b>
                                                                            <%# Eval("AIR_COMPANY_CD")%>&nbsp;(<%# Eval("AIR_COMPANY_NM")%>)<%# Eval("FLIGHT_NO") %><%=LABEL_0074%>
                                                                        </b>
                                                                    </div>
                                                                    <div class="col-lg-8 col-12">
                                                                        <b><%# Eval("DEP_NM")%></b><%=LABEL_0075%>&nbsp;<i class="fa fa-arrow-right" style="padding-bottom: 5px;"></i>&nbsp;
                                                                                    <b><%# Eval("ARR_NM")%></b><%=LABEL_0076%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding: 5px 0px 5px 0px;">
                                                                <asp:Repeater ID="RES_SERVICE_SUBRepeater" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="col-lg-12 pdt5">
                                                                            <div class="input-group">
                                                                                <div class="input-group-prepend">
                                                                                    <%--1:受託手荷物、2:食事、3:座席、4:コンフォートキット、5:ビデオオンデマンド、6:機内持込手荷物、9:その他--%>
                                                                                    <span class="input-group-text" style="width: 170px;">
                                                                                        <%#IIf(Eval("SERVICE_KBN") = "1", "受託手荷物", "") %>
                                                                                        <%#IIf(Eval("SERVICE_KBN") = "2", "食事", "") %>
                                                                                        <%#IIf(Eval("SERVICE_KBN") = "3", "座席指定", "") %>
                                                                                        <%#IIf(Eval("SERVICE_KBN") = "4", "コンフォートキット", "") %>
                                                                                        <%#IIf(Eval("SERVICE_KBN") = "5", "ビデオオンデマンド", "") %>
                                                                                        <%#IIf(Eval("SERVICE_KBN") = "6", "機内持込手荷物", "") %>
                                                                                        <%#IIf(Eval("SERVICE_KBN") = "9", "その他", "") %>
                                                                                    </span>
                                                                                </div>
                                                                                <asp:DropDownList ID="SERVICE" runat="server" class="form-control form-control-lg">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>

                                    </asp:Panel>
                                    <asp:Panel ID="RES_SERVICE_TOURPanel" runat="server" CssClass="col-lg-12" Visible="false">
                                        <asp:Repeater ID="RES_SERVICE_TOURRepeater" runat="server" Visible="false">
                                            <ItemTemplate>
                                                <div class="card-header">
                                                    <asp:Label ID="SERVICE_CD" runat="server" Text='<%# Eval("SERVICE_CD")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="SELECT_WAY_KBN" runat="server" Text='<%# Eval("SELECT_WAY_KBN")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="SELECT_KBN" runat="server" Text='<%# Eval("SELECT_KBN")%>' Visible="false"></asp:Label>

                                                    <%# IIf(Eval("SELECT_KBN") = "1", "<span class='label label-red' style='margin-left: 5px; margin-right: 5px;'>必須</span>", "<span class='label label-green' style='margin-left: 5px; margin-right: 5px;'>任意</span>") %>
                                                    <asp:Label ID="SERVICE_NM" runat="server" Text='<%# Eval("SERVICE_NM")%>'></asp:Label>
                                                    <%# IIf(Eval("SELECT_WAY_KBN") = "2", "<br/>(複数選択可)", "")%>
                                                    <%# IIf(Not Eval("SERVICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("SERVICE_DISP_REMARKS").ToString & "</p>", "") %>
                                                </div>
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-lg-6 pd10">
                                                            <asp:Panel ID="RES_SERVICE_TOUR_CHOICEPanel" runat="server" Visible="false">
                                                                <asp:DropDownList ID="SERVICE_PRICE_CD_DROPDOWNLIST" runat="server" class="form-control form-control-lg">
                                                                </asp:DropDownList>
                                                            </asp:Panel>
                                                            <asp:Repeater ID="RES_SERVICE_TOUR_CHECKLISTRepeater" runat="server" Visible="false">
                                                                <ItemTemplate>
                                                                    <div class="row" style="padding: 5px 0px 5px 0px;">
                                                                        <div class="col-lg-12 pdt5">
                                                                            <asp:Label ID="SERVICE_PRICE_CD" runat="server" Text='<%# Eval("SERVICE_PRICE_CD")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="SERVICE_PRICE_NM" runat="server" Text='<%# Eval("SERVICE_PRICE_NM")%>' Visible="false"></asp:Label>
                                                                            <asp:CheckBox ID="SERVICE_PRICE_CD_CHECKBOX" runat="server" />
                                                                            <asp:Label ID="SERVICE_PRICE_NM_DISP" runat="server"></asp:Label>
                                                                            <%# IIf(Not Eval("PRICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("PRICE_DISP_REMARKS").ToString & "</p>", "") %>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <asp:Panel ID="RES_SERVICE_TOUR_INPUTPanel" runat="server" Visible="false">
                                                                <asp:TextBox ID="RES_SERVICE_TOUR_INPUT" runat="server" CssClass='form-control form-control-lg'></asp:TextBox>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                    
                                </ItemTemplate>
                            </asp:Repeater>
                                 </div>
                        </asp:Panel>
                        <%--ホテル以外用--%>
                        <%--ホテル用--%>
                        <asp:Panel ID="PASSENGER_HOTELPanel" runat="server" Visible="false">

                            <asp:Repeater ID="PASSENGER_HOTELRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="card mb-3 shadow">
                                        <div class="card-header  text-white bg-secondary">
                                            <%# Container.ItemIndex + 1%><%=LABEL_0077%><%# Eval("ROOM_TYPE_NM")%><%=LABEL_0078%><asp:Label ID="HOTEL_SEQ" runat="server" Text='<%# Eval("HOTEL_SEQ")%>' Visible="false"></asp:Label>
                                        </div>
                                        <asp:Repeater ID="PASSENGER_HOTEL_NAMERepeater" runat="server">
                                            <ItemTemplate>
                                                <div class="card-header">

                                                    <asp:Label ID="AGE_KBN" runat="server" Text='<%# Eval("AGE_KBN") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="NAME_NO" runat="server" Text='<%# Eval("NAME_NO") %>' Visible="false"></asp:Label>
                                                    <%# Eval("AGE_KBN_NM")%>
                                                    <%# Container.ItemIndex + 1%><%=LABEL_0079%>
                                                </div>
                                                <div class="card-body">
                                                    <asp:Panel ID="NAME_KANJIPanel" runat="server" CssClass="row">
                                                        <div class="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;">姓
                                                                    </span>
                                                                </div>
                                                                <asp:TextBox ID="FAMILY_NAME_KANJI" runat="server" class="form-control form-control-lg"
                                                                    MaxLength="50" Text='<%# Eval("FAMILY_NAME_KANJI")%>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;">名
                                                                    </span>
                                                                </div>
                                                                <asp:TextBox ID="FIRST_NAME_KANJI" runat="server" class="form-control form-control-lg"
                                                                    MaxLength="50" Text='<%# Eval("FIRST_NAME_KANJI")%>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="NAME_KANAPanel" runat="server" CssClass="row">
                                                        <div class="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;">セイ
                                                                    </span>
                                                                </div>
                                                                <asp:TextBox ID="FAMILY_NAME_KANA" runat="server" class="form-control form-control-lg" onblur="RepeaterItemConvert(this);"
                                                                    MaxLength="50" Text='<%# Eval("FAMILY_NAME_KANA")%>' placeholder="例：ヤマダ"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;">メイ
                                                                    </span>
                                                                </div>
                                                                <asp:TextBox ID="FIRST_NAME_KANA" runat="server" class="form-control form-control-lg" onblur="RepeaterItemConvert(this);"
                                                                    MaxLength="50" Text='<%# Eval("FIRST_NAME_KANA")%>' placeholder="例：タロウ"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="NAME_ROMANPanel" runat="server" CssClass="row">
                                                        <div class="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;"><%=LABEL_0080%></span>
                                                                </div>
                                                                <asp:TextBox ID="SURNAME_ROMAN" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                                    MaxLength="50" Text='<%#  SetRRValue.setNothingValueWeb(Eval("FAMILY_NAME"))%>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;"><%=LABEL_0081%></span>
                                                                </div>
                                                                <asp:TextBox ID="NAME_ROMAN" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                                    MaxLength="50" Text='<%#  SetRRValue.setNothingValueWeb(Eval("FIRST_NAME"))%>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <asp:Panel ID="MIDDLE_NMPanel" runat="server" CssClass="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;">MIDDLE</span>
                                                                </div>
                                                                <asp:TextBox ID="MIDDLE_NM" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                                    MaxLength="50" Text='<%#  SetRRValue.setNothingValueWeb(Eval("MIDDLE_NM"))%>'></asp:TextBox>
                                                            </div>
                                                        </asp:Panel>
                                                    </asp:Panel>
                                                    <div class="row">
                                                        <asp:Panel ID="SEX_FLGPanel" runat="server" CssClass="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;"><%= LABEL_0084%></span>
                                                                </div>
                                                                <div class="form-control form-control-lg fs16">
                                                                    <asp:RadioButton GroupName="SEX_FLG" ID="MAN" runat="server" Text="<%# LABEL_0085%>" />
                                                                    <asp:RadioButton GroupName="SEX_FLG" ID="WOMAN" runat="server" Text='<%# LABEL_0086%>' />
                                                                    <asp:RadioButton GroupName="SEX_FLG" ID="NOANSWER" runat="server" Text='回答しない' Visible="false" />
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="BIRTHPanel" runat="server" Visible="false" CssClass="col-lg-6">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;">
                                                                        <asp:Label ID="BIRTHLabel" runat="server"></asp:Label></span>
                                                                </div>
                                                                <asp:TextBox ID="BIRTH_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4" Style="ime-mode: disabled">
                                                                </asp:TextBox>
                                                                <asp:DropDownList ID="BIRTH_MM" runat="server" class="form-control form-control-lg">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="BIRTH_MM_TEXT" runat="server" class="form-control form-control-lg" MaxLength="4" ReadOnly="true" Visible="false">
                                                                </asp:TextBox>
                                                                <asp:DropDownList ID="BIRTH_DD" runat="server" class="form-control form-control-lg">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="BIRTH_DD_TEXT" runat="server" class="form-control form-control-lg" MaxLength="4" ReadOnly="true" Visible="false">
                                                                </asp:TextBox>
                                                            </div>
                                                        </asp:Panel>

                                                    </div>
                                                    <asp:Panel ID="NATIONALITYPanel" runat="server" Visible="false" CssClass="row">
                                                        <div class="col-lg-12">
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" style="width: 100px;"><%=LABEL_0087%></span>
                                                                </div>
                                                                <asp:DropDownList ID="NATIONALITY" runat="server" class="form-control form-control-lg">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="PASSPORT_NOPanel" runat="server" Visible="false" CssClass="row">
                                                        <div class="alert alert-secondary mt-2" role="alert">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <b>
                                                                        <%=LABEL_0088%></b>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <div class="input-group">
                                                                        <div class="input-group-prepend">
                                                                            <span class="input-group-text" style="width: 100px;"><%=LABEL_0089%></span>
                                                                        </div>

                                                                        <asp:TextBox ID="PASSPORT_NO" runat="server" class="form-control form-control-lg" Style="text-transform: uppercase; ime-mode: disabled"
                                                                            MaxLength="15" Text='<%# Eval("PASSPORT_NO")%>'></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Panel ID="PASSPORT_DATEPanel" runat="server">
                                                                        <div class="input-group">
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text" style="width: 100px;"><%=LABEL_0090%></span>
                                                                            </div>
                                                                            <asp:TextBox ID="PASSPORT_DATE_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4">
                                                                            </asp:TextBox>
                                                                            <asp:DropDownList ID="PASSPORT_DATE_MM" runat="server" class="form-control form-control-lg">
                                                                            </asp:DropDownList>
                                                                            <asp:DropDownList ID="PASSPORT_DATE_DD" runat="server" class="form-control form-control-lg">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    <asp:Panel ID="PASSPORT_LIMITPanel" runat="server">
                                                                        <div class="input-group">
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text" style="width: 100px;"><%=LABEL_0091%></span>
                                                                            </div>
                                                                            <asp:TextBox ID="PASSPORT_LIMIT_YYYY" runat="server" class="form-control form-control-lg" MaxLength="4">
                                                                            </asp:TextBox>
                                                                            <asp:DropDownList ID="PASSPORT_LIMIT_MM" runat="server" class="form-control form-control-lg">
                                                                            </asp:DropDownList>
                                                                            <asp:DropDownList ID="PASSPORT_LIMIT_DD" runat="server" class="form-control form-control-lg">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Panel ID="PASSPORT_ISSUE_COUNTRYPanel" runat="server">
                                                                        <div class="input-group">
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text" style="width: 100px;"><%=LABEL_0092%></span>
                                                                            </div>
                                                                            <asp:DropDownList ID="PASSPORT_ISSUE_COUNTRY" runat="server" class="form-control form-control-lg">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="RES_SERVICEPanel" runat="server" Visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <span class="badge badge-secondary p-2 mt-3 mb-3">付帯サービス(任意)</span>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:Repeater ID="RES_SERVICERepeater" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <asp:Label ID="SEGMENT_SEQ" runat="server" Text='<%# Eval("SEGMENT_SEQ")%>' Visible="false"></asp:Label>
                                                                                <div class="alert alert-secondary  ng-binding" role="alert">
                                                                                    <div class="row mb-3">
                                                                                        <div class="col-lg-12 col-12">
                                                                                            <%# Eval("DEP_NM")%><%=LABEL_0075%>&nbsp;<i class="fa fa-arrow-right"></i>&nbsp;
                                                                                  <%# Eval("ARR_NM")%><%=LABEL_0076%>
                                                                                        </div>
                                                                                        <div class="col-lg-12 col-12">

                                                                                            <%# Eval("AIR_COMPANY_CD")%>&nbsp;(<%# Eval("AIR_COMPANY_NM")%>)<%# Eval("FLIGHT_NO") %><%=LABEL_0074%>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <asp:Repeater ID="RES_SERVICE_SUBRepeater" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <div class="col-lg-12">
                                                                                                    <div class="input-group">
                                                                                                        <div class="input-group-prepend">
                                                                                                            <%--1:受託手荷物、2:食事、3:座席、4:コンフォートキット、5:ビデオオンデマンド、6:機内持込手荷物、9:その他--%>
                                                                                                            <span class="input-group-text" style="width: 170px;">
                                                                                                                <%#IIf(Eval("SERVICE_KBN") = "1", "受託手荷物", "") %>
                                                                                                                <%#IIf(Eval("SERVICE_KBN") = "2", "食事", "") %>
                                                                                                                <%#IIf(Eval("SERVICE_KBN") = "3", "座席指定", "") %>
                                                                                                                <%#IIf(Eval("SERVICE_KBN") = "4", "コンフォートキット", "") %>
                                                                                                                <%#IIf(Eval("SERVICE_KBN") = "5", "ビデオオンデマンド", "") %>
                                                                                                                <%#IIf(Eval("SERVICE_KBN") = "6", "機内持込手荷物", "") %>
                                                                                                                <%#IIf(Eval("SERVICE_KBN") = "9", "その他", "") %>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                        <asp:DropDownList ID="SERVICE" runat="server" class="form-control form-control-lg">
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="RES_SERVICE_TOURPanel" runat="server" CssClass="row" Visible="false">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:Repeater ID="RES_SERVICE_TOURRepeater" runat="server" Visible="false">
                                                                    <ItemTemplate>


                                                                        <div class="alert alert-secondary  ng-binding" role="alert">
                                                                            <div class="row">
                                                                                <div class="col-lg-12">
                                                                                    <asp:Label ID="SERVICE_CD" runat="server" Text='<%# Eval("SERVICE_CD")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="SELECT_WAY_KBN" runat="server" Text='<%# Eval("SELECT_WAY_KBN")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="SELECT_KBN" runat="server" Text='<%# Eval("SELECT_KBN")%>' Visible="false"></asp:Label>

                                                                                    <%# IIf(Eval("SELECT_KBN") = "1", "<span class='label label-red' style='margin-left: 5px; margin-right: 5px;'>必須</span>", "<span class='label label-green' style='margin-left: 5px; margin-right: 5px;'>任意</span>") %>
                                                                                    <asp:Label ID="SERVICE_NM" runat="server" Text='<%# Eval("SERVICE_NM")%>'></asp:Label>
                                                                                    <%# IIf(Eval("SELECT_WAY_KBN") = "2", "<br/>(複数選択可)", "")%>
                                                                                    <%# IIf(Not Eval("SERVICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("SERVICE_DISP_REMARKS").ToString & "</p>", "") %>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-lg-6 pd10">
                                                                                    <asp:Panel ID="RES_SERVICE_TOUR_CHOICEPanel" runat="server" Visible="false">
                                                                                        <asp:DropDownList ID="SERVICE_PRICE_CD_DROPDOWNLIST" runat="server" class="form-control form-control-lg">
                                                                                        </asp:DropDownList>
                                                                                    </asp:Panel>
                                                                                    <asp:Repeater ID="RES_SERVICE_TOUR_CHECKLISTRepeater" runat="server" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <div class="row" style="padding: 5px 0px 5px 0px;">
                                                                                                <div class="col-lg-12 pdt5">
                                                                                                    <asp:Label ID="SERVICE_PRICE_CD" runat="server" Text='<%# Eval("SERVICE_PRICE_CD")%>' Visible="false"></asp:Label>
                                                                                                    <asp:Label ID="SERVICE_PRICE_NM" runat="server" Text='<%# Eval("SERVICE_PRICE_NM")%>' Visible="false"></asp:Label>
                                                                                                    <asp:CheckBox ID="SERVICE_PRICE_CD_CHECKBOX" runat="server" />
                                                                                                    <asp:Label ID="SERVICE_PRICE_NM_DISP" runat="server"></asp:Label>
                                                                                                    <%# IIf(Not Eval("PRICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("PRICE_DISP_REMARKS").ToString & "</p>", "") %>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <asp:Panel ID="RES_SERVICE_TOUR_INPUTPanel" runat="server" Visible="false">
                                                                                        <asp:TextBox ID="RES_SERVICE_TOUR_INPUT" runat="server" class="form-control form-control-lg"></asp:TextBox>
                                                                                    </asp:Panel>
                                                                                    <%--<asp:Label ID="PRICE_DISP_REMARKSLabel" runat="server"></asp:Label>--%>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </asp:Panel>
                        <%--ホテル用--%>
                    </asp:Panel>


                    <%--備考--%>
                    <asp:Panel ID="RemarksPanel" runat="server">
                        <div class="tab-ttl mb-3">
                            <h2>
                                <asp:Label ID="LABEL_0093" runat="server"></asp:Label></h2>
                        </div>
                        <div class="card mb-3 shadow">
                            <asp:Panel ID="RES_SERVICE_TOURPanel" runat="server">
                                <asp:Repeater ID="RES_SERVICE_TOURRepeater" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="SERVICE_CD" runat="server" Text='<%# Eval("SERVICE_CD")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="SELECT_WAY_KBN" runat="server" Text='<%# Eval("SELECT_WAY_KBN")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="SELECT_KBN" runat="server" Text='<%# Eval("SELECT_KBN")%>' Visible="false"></asp:Label>
                                        <div class="card-header">
                                            <%# IIf(Eval("SELECT_KBN") = "1", "<span class='label label-red' style='margin-left: 5px; margin-right: 5px;'>必須</span>", "<span class='label label-green' style='margin-left: 5px; margin-right: 5px;'>任意</span>") %>
                                            <asp:Label ID="SERVICE_NM" runat="server" Text='<%# Eval("SERVICE_NM")%>'></asp:Label>
                                            <%# IIf(Eval("SELECT_WAY_KBN") = "2", "<br/>(複数選択可)", "")%>
                                            <%# IIf(Not Eval("SERVICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("SERVICE_DISP_REMARKS").ToString & "</p>", "") %>
                                        </div>
                                        <div class="card-body">
                                            <asp:Panel ID="RES_SERVICE_TOUR_CHOICEPanel" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <asp:DropDownList ID="SERVICE_PRICE_CD_DROPDOWNLIST" runat="server" class="form-control form-control-lg">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Repeater ID="RES_SERVICE_TOUR_CHECKLISTRepeater" runat="server" Visible="false">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="SERVICE_PRICE_CD" runat="server" Text='<%# Eval("SERVICE_PRICE_CD")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="SERVICE_PRICE_NM" runat="server" Text='<%# Eval("SERVICE_PRICE_NM")%>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="SERVICE_PRICE_CD_CHECKBOX" runat="server" />
                                                            <asp:Label ID="SERVICE_PRICE_NM_DISP" runat="server"></asp:Label>
                                                            <%# IIf(Not Eval("PRICE_DISP_REMARKS").Equals(""), "<p class='small mb-0'>" & Eval("PRICE_DISP_REMARKS").ToString & "</p>", "") %>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Panel ID="RES_SERVICE_TOUR_INPUTPanel" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="RES_SERVICE_TOUR_INPUT" runat="server" class="form-control form-control-lg"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <%--<asp:Label ID="PRICE_DISP_REMARKSLabel" runat="server"></asp:Label>--%>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <%--日本出発日--%>
                            <asp:Panel ID="DEP_DATEPanel" runat="server">

                                <div class="card-header">
                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0094" runat="server"></asp:Label></span><asp:Label ID="LABEL_0095"
                                            runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="DEP_DATE" BackColor="White" runat="server" CssClass="form-control form-control-lg"
                                        Style="cursor: pointer" ReadOnly="true"></asp:TextBox>
                                </div>

                            </asp:Panel>
                            <%--現地到着--%>
                            <asp:Panel ID="LOCAL_ARR_DATEPanel" runat="server" Visible="false">

                                <div class="card-header">
                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0172" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0173" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-5 col-12">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">到着日
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="LOCAL_ARR_DATE" BackColor="White" runat="server" CssClass="form-control form-control-lg"
                                                    Style="cursor: pointer" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-5 col-12">
                                            <asp:Panel ID="LOCAL_ARR_TIMEPanel" runat="server" Visible="false">
                                                <div class="input-group" style="margin-bottom: 0px;">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;">時間
                                                        </span>
                                                    </div>
                                                    <asp:DropDownList ID="LOCAL_ARR_TIME_HH" runat="server" class="form-control form-control-lg">
                                                        <asp:ListItem Value="">--</asp:ListItem>
                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                        <asp:ListItem Value="09">09</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="LOCAL_ARR_TIME_HH" BackColor="White" runat="server" CssClass="form-control form-control-lg" MaxLength="2"></asp:TextBox>--%>
                                                    <div class="input-group-prepend"><span class="input-group-text" style="width: 50px;">：</span></div>
                                                    <asp:DropDownList ID="LOCAL_ARR_TIME_MM" runat="server" class="form-control form-control-lg">
                                                        <asp:ListItem Value="">--</asp:ListItem>
                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                        <asp:ListItem Value="09">09</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="31">31</asp:ListItem>
                                                        <asp:ListItem Value="32">32</asp:ListItem>
                                                        <asp:ListItem Value="33">33</asp:ListItem>
                                                        <asp:ListItem Value="34">34</asp:ListItem>
                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                        <asp:ListItem Value="36">36</asp:ListItem>
                                                        <asp:ListItem Value="37">37</asp:ListItem>
                                                        <asp:ListItem Value="38">38</asp:ListItem>
                                                        <asp:ListItem Value="39">39</asp:ListItem>
                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                        <asp:ListItem Value="41">41</asp:ListItem>
                                                        <asp:ListItem Value="42">42</asp:ListItem>
                                                        <asp:ListItem Value="43">43</asp:ListItem>
                                                        <asp:ListItem Value="44">44</asp:ListItem>
                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                        <asp:ListItem Value="46">46</asp:ListItem>
                                                        <asp:ListItem Value="47">47</asp:ListItem>
                                                        <asp:ListItem Value="48">48</asp:ListItem>
                                                        <asp:ListItem Value="49">49</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="51">51</asp:ListItem>
                                                        <asp:ListItem Value="52">52</asp:ListItem>
                                                        <asp:ListItem Value="53">53</asp:ListItem>
                                                        <asp:ListItem Value="54">54</asp:ListItem>
                                                        <asp:ListItem Value="55">55</asp:ListItem>
                                                        <asp:ListItem Value="56">56</asp:ListItem>
                                                        <asp:ListItem Value="57">57</asp:ListItem>
                                                        <asp:ListItem Value="58">58</asp:ListItem>
                                                        <asp:ListItem Value="59">59</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="LOCAL_ARR_TIME_MM" BackColor="White" runat="server" CssClass="form-control form-control-lg" MaxLength="2"></asp:TextBox>--%>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <%--現地出発--%>
                            <asp:Panel ID="LOCAL_ARR_FLIGHTPanel" runat="server" Visible="false">

                                <div class="card-header">
                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0174" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0175" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-5 col-12">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">航空会社
                                                    </span>
                                                </div>
                                                <asp:DropDownList ID="LOCAL_ARR_CARRIER_CD" runat="server" class="form-control form-control-lg">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-12" style="background-color: White;">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">便名
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="LOCAL_ARR_FLIGHT_NO" BackColor="White" runat="server" CssClass="form-control form-control-lg" MaxLength="4" Width="100px" placeholder="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LOCAL_DEP_TIMEPanel" runat="server" Visible="false">

                                <div class="card-header">
                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0096" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0097" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-5 col-12">
                                            <asp:Panel ID="LOCAL_DEP_DATEPanel" runat="server" Visible="false">
                                                <div class="input-group" style="margin-bottom: 0px;">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" style="width: 100px;">現地出発日
                                                        </span>
                                                    </div>
                                                    <asp:TextBox ID="LOCAL_DEP_DATE" BackColor="White" runat="server" CssClass="form-control form-control-lg"
                                                        Style="cursor: pointer" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-lg-5 col-12">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">時間
                                                    </span>
                                                </div>
                                                <asp:DropDownList ID="LOCAL_DEP_TIME_HH" runat="server" class="form-control form-control-lg">
                                                    <asp:ListItem Value="">--</asp:ListItem>
                                                    <asp:ListItem Value="00">00</asp:ListItem>
                                                    <asp:ListItem Value="01">01</asp:ListItem>
                                                    <asp:ListItem Value="02">02</asp:ListItem>
                                                    <asp:ListItem Value="03">03</asp:ListItem>
                                                    <asp:ListItem Value="04">04</asp:ListItem>
                                                    <asp:ListItem Value="05">05</asp:ListItem>
                                                    <asp:ListItem Value="06">06</asp:ListItem>
                                                    <asp:ListItem Value="07">07</asp:ListItem>
                                                    <asp:ListItem Value="08">08</asp:ListItem>
                                                    <asp:ListItem Value="09">09</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="16">16</asp:ListItem>
                                                    <asp:ListItem Value="17">17</asp:ListItem>
                                                    <asp:ListItem Value="18">18</asp:ListItem>
                                                    <asp:ListItem Value="19">19</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="21">21</asp:ListItem>
                                                    <asp:ListItem Value="22">22</asp:ListItem>
                                                    <asp:ListItem Value="23">23</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:TextBox ID="LOCAL_DEP_TIME_HH" BackColor="White" runat="server" CssClass="form-control form-control-lg" MaxLength="2"></asp:TextBox>--%>
                                                <div class="input-group-prepend"><span class="input-group-text" style="width: 50px;">：</span></div>
                                                <asp:DropDownList ID="LOCAL_DEP_TIME_MM" runat="server" class="form-control form-control-lg">
                                                    <asp:ListItem Value="">--</asp:ListItem>
                                                    <asp:ListItem Value="01">01</asp:ListItem>
                                                    <asp:ListItem Value="02">02</asp:ListItem>
                                                    <asp:ListItem Value="03">03</asp:ListItem>
                                                    <asp:ListItem Value="04">04</asp:ListItem>
                                                    <asp:ListItem Value="05">05</asp:ListItem>
                                                    <asp:ListItem Value="06">06</asp:ListItem>
                                                    <asp:ListItem Value="07">07</asp:ListItem>
                                                    <asp:ListItem Value="08">08</asp:ListItem>
                                                    <asp:ListItem Value="09">09</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="16">16</asp:ListItem>
                                                    <asp:ListItem Value="17">17</asp:ListItem>
                                                    <asp:ListItem Value="18">18</asp:ListItem>
                                                    <asp:ListItem Value="19">19</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="21">21</asp:ListItem>
                                                    <asp:ListItem Value="22">22</asp:ListItem>
                                                    <asp:ListItem Value="23">23</asp:ListItem>
                                                    <asp:ListItem Value="24">24</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="26">26</asp:ListItem>
                                                    <asp:ListItem Value="27">27</asp:ListItem>
                                                    <asp:ListItem Value="28">28</asp:ListItem>
                                                    <asp:ListItem Value="29">29</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="31">31</asp:ListItem>
                                                    <asp:ListItem Value="32">32</asp:ListItem>
                                                    <asp:ListItem Value="33">33</asp:ListItem>
                                                    <asp:ListItem Value="34">34</asp:ListItem>
                                                    <asp:ListItem Value="35">35</asp:ListItem>
                                                    <asp:ListItem Value="36">36</asp:ListItem>
                                                    <asp:ListItem Value="37">37</asp:ListItem>
                                                    <asp:ListItem Value="38">38</asp:ListItem>
                                                    <asp:ListItem Value="39">39</asp:ListItem>
                                                    <asp:ListItem Value="40">40</asp:ListItem>
                                                    <asp:ListItem Value="41">41</asp:ListItem>
                                                    <asp:ListItem Value="42">42</asp:ListItem>
                                                    <asp:ListItem Value="43">43</asp:ListItem>
                                                    <asp:ListItem Value="44">44</asp:ListItem>
                                                    <asp:ListItem Value="45">45</asp:ListItem>
                                                    <asp:ListItem Value="46">46</asp:ListItem>
                                                    <asp:ListItem Value="47">47</asp:ListItem>
                                                    <asp:ListItem Value="48">48</asp:ListItem>
                                                    <asp:ListItem Value="49">49</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="51">51</asp:ListItem>
                                                    <asp:ListItem Value="52">52</asp:ListItem>
                                                    <asp:ListItem Value="53">53</asp:ListItem>
                                                    <asp:ListItem Value="54">54</asp:ListItem>
                                                    <asp:ListItem Value="55">55</asp:ListItem>
                                                    <asp:ListItem Value="56">56</asp:ListItem>
                                                    <asp:ListItem Value="57">57</asp:ListItem>
                                                    <asp:ListItem Value="58">58</asp:ListItem>
                                                    <asp:ListItem Value="59">59</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:TextBox ID="LOCAL_DEP_TIME_MM" BackColor="White" runat="server" CssClass="form-control form-control-lg" MaxLength="2"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="LOCAL_DEP_FLIGHTPanel" runat="server" Visible="false">

                                <div class="card-header">
                                    <span class="label label-red mr-2">
                                        <asp:Label ID="LABEL_0098" runat="server"></asp:Label></span>
                                    <asp:Label ID="LABEL_0099" runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-5 col-12">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">航空会社
                                                    </span>
                                                </div>
                                                <asp:DropDownList ID="LOCAL_DEP_CARRIER_CD" runat="server" class="form-control form-control-lg">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-12" style="background-color: White;">
                                            <div class="input-group" style="margin-bottom: 0px;">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text" style="width: 100px;">便名
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="LOCAL_DEP_FLIGHT_NO" BackColor="White" runat="server" CssClass="form-control form-control-lg" MaxLength="4" Width="100px" placeholder="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <%--日本到着--%>
                            <asp:Panel ID="RET_DATEPanel" runat="server">


                                <div class="card-header">
                                    <asp:Label ID="LABEL_0100" runat="server"></asp:Label></span><asp:Label ID="LABEL_0101"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="card-body">
                                    <asp:TextBox ID="RET_DATE" BackColor="White" runat="server" CssClass="form-control form-control-lg"
                                        Style="cursor: pointer" ReadOnly="true"></asp:TextBox>
                                </div>

                            </asp:Panel>


                            <div class="card-header"><asp:Label ID="LABEL_0107"
                                        runat="server"></asp:Label>(<asp:Label ID="LABEL_0106" runat="server"> </asp:Label>)
                            </div>
                            <div class="card-body">

                                <asp:TextBox ID="CLIENT_COMMENT" runat="server" class="form-control" Rows="10" TextMode="MultiLine"></asp:TextBox>

                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PASSPORT_AGREEPanel" CssClass="col-lg-12" runat="server" Visible="false">
                         <div class="tab-ttl mb-3">
                            <h2>個人情報の取扱いについて</h2>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-12 text-center">
                                <asp:CheckBox ID="Agree2CheckBox" runat="server" CssClass="text-center" />
                                <label for="Agree2CheckBox" style="font-weight: normal">
                                    &nbsp;<asp:Label ID="LABEL_0111" runat="server"></asp:Label></label>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="REGULATIONPanel" runat="server">
                    <asp:Panel ID="REGULATION_DEFAULTPanel" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-header"> <asp:LinkButton ID="REGULATIONLinkButton" runat="server" OnClientClick="_RegModal();return false;">
                                        <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;<asp:Label ID="LABEL_0108" runat="server"></asp:Label>
                                    </asp:LinkButton></div>
                                    <div class="card mb-3 shadow">
                                        <div class="card-body">
                                            <div id="regulation" runat="server" style="overflow-y: scroll; height: 300px; background-color: #FFFFFF; line-height: 20px" class="border pd10">
                                            </div>
                                        </div>
                                </div>
                            </div>
                            </div>
                    </asp:Panel>
                    <asp:Panel ID="REGULATION_LINKPanel" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <p style="text-align: center">
                                        <a id="REGULATION_LINKLinkButton" runat="server" target="_blank">
                                            <i class="fa fa-chevron-circle-right"></i>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="LABEL_0112_2" runat="server"></asp:Label>
                                        </a>
                                    </p>
                                </div>
                            </div>
                    </asp:Panel>
                <%--▼ページフッター--%>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Literal ID="AddRegulation" runat="server"></asp:Literal>
                    </div>
                </div>
                <%--▲ページフッター--%>
                <div class="row">
                    <div class="col-lg-12">
                        <p class="text-center">
                            <span style="color: red;">
                                <b>
                                    <asp:Label ID="LABEL_0109" runat="server"></asp:Label></b>
                                <br />
                                <asp:Label ID="LABEL_0177" runat="server"></asp:Label>
                            </span>
                        </p>
                    </div>
                </div>
                <asp:Panel ID="AgreeCheckPanel" runat="server">
                    <div class="row">
                        <asp:Panel ID="AgreeMessagePanel" runat="server" CssClass="col-lg-12">
                            <div class="text-center">
                                <span style="color: red;"><b>
                                    <asp:Label ID="LABEL_0110" runat="server"></asp:Label></b></span>
                            </div>
                        </asp:Panel>
                        <div class="col-lg-12 mt-3 text-center">
                            <asp:CheckBox class="text-center" ID="Agree1CheckBox" runat="server" />
                        </div>
                    </div>
                </asp:Panel>
                </asp:Panel>
                    <%--▼ページフッター--%>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Literal ID="Pagefooter" runat="server"></asp:Literal>
                    </div>
                </div>
                <%--▲ページフッター--%>

                <div class="row">
                    <div class="col-6 p-3">
                        <asp:LinkButton ID="BackLinkButton" runat="server" class="btn btn-block btn-lg-tw btn-primary"></asp:LinkButton>
                    </div>
                    <div class="col-6 p-3">
                        <asp:LinkButton ID="CONFIRMLinkButton" runat="server" class="btn btn-block btn-lg-tw btn-danger"></asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>
        </div>
        <div class="masthead">
            <triphoowebFooter:triphoowebFooter ID="triphoowebFooter" runat="Server"></triphoowebFooter:triphoowebFooter>
        </div>
        <Waiting:Waiting ID="Waiting" runat="Server"></Waiting:Waiting>
        <%-- 個人情報の取扱いについて --%>
        <div class="modal fade" id="RegModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label ID="LABEL_0112" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="REGULATIONLabel" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 pd20">
                                <a id="A1" class="btn btn-block btn-primary" data-dismiss="modal">
                                    <asp:Label ID="LABEL_0113" runat="server"></asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 個人情報の取扱いについて --%>
        <script type="text/javascript" src="/scripts/jquery.lazyload.min.js?v=1.9.1"></script>
        <script type="text/javascript">
            $(function () {
                $("img.lazy").lazyload({
                    effect: "fadeIn"
                });

            });

        </script>

    </form>
</body>
</html>
