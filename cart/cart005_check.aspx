<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cart005_check.aspx.vb" EnableViewState="true"
    EnableEventValidation="false" Inherits="tw.page_cart_cart005_check" %>

<%@ Register TagPrefix="triphoowebHeader" TagName="triphoowebHeader" Src="../common/header.ascx" %>
<%@ Register TagPrefix="triphoowebFooter" TagName="triphoowebFooter" Src="../common/footer.ascx" %>
<%@ Register TagPrefix="TelMe" TagName="TelMe" Src="../common/TelMe.ascx" %>
<%@ Register TagPrefix="Payment" TagName="Payment" Src="../common/Payment.ascx" %>
<%@ Register TagPrefix="Waiting" TagName="Waiting" Src="../common/Waiting.ascx" %>

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
    <script src="../../scripts/custom/commonCss.js?ver=1.02" type="text/javascript"></script>
    <script src="../../scripts/custom/common.js?ver=1.01" type="text/javascript"></script>
    <script type="text/javascript" src="../../scripts/jquery.flexslider-min.js"></script>
    <script type="text/javascript" src="../../scripts/holiday.js"></script>
    <script src="../../scripts/custom/affiliate.js" type="text/javascript"></script>
    <style>
        #Payment_VISA, #Payment_AMEX, #Payment_DINERS, #Payment_JCB, #Payment_MASTER {
            width: 40px !important;
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
        <asp:HiddenField ID="ControlId" runat="server" Value="Payment_" />
        <asp:HiddenField ID="GOODS_CD" runat="server" />
        <asp:HiddenField ID="DEP_TIME" runat="server" />
        <asp:HiddenField ID="JOIN_CD" runat="server" />
        <asp:Literal ID="AFFILIATE_SCRIPTLiteral" runat="server"></asp:Literal>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="masthead">
            <triphoowebHeader:triphoowebHeader ID="triphoowebHeader" runat="Server"></triphoowebHeader:triphoowebHeader>
        </div>
        <%--▼パンくず--%>

        <%--        <div class="step pdl5 pdr5">
            <ul>
                <li class="step-main">
                    <asp:Label ID="LABEL_0001" runat="server"></asp:Label>
                </li>
                <li class="d-none d-lg-block d-md-none">
                    <asp:Label ID="LABEL_0002" runat="server"></asp:Label>
                </li>
                <li class="d-none d-lg-block d-md-none">
                    <asp:Label ID="LABEL_0003" runat="server"></asp:Label>
                </li>
                <li class="selected">
                    <asp:Label ID="LABEL_0004" runat="server"></asp:Label>
                </li>
                <li id="PaymentComplete" runat="server" visible="false">
                    <asp:Label ID="LABEL_0007" runat="server"></asp:Label>
                </li>
            </ul>
        </div>--%>

        <%--▲パンくず--%>
        <div class="breadcrumb-bg">
            <div class="container">
                <%--▼パンくず--%>
                <div class="row">
                    <div class="col-lg-12">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb mb-0">
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_0001" runat="server"></asp:Label>
                                </li>
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_0002" runat="server"></asp:Label>
                                </li>
                                <li class="breadcrumb-item" aria-current="page">
                                    <asp:Label ID="LABEL_0003" runat="server"></asp:Label>
                                </li>
                                <li class="breadcrumb-item selected" style="border-bottom: none;">
                                    <asp:Label ID="LABEL_0004" runat="server"></asp:Label>
                                </li>
                                <li id="PaymentComplete" runat="server" visible="false">
                                    <asp:Label ID="LABEL_0007" runat="server"></asp:Label>
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
                    <asp:Panel ID="ResInfoPanel" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Literal ID="PageHeader" runat="server"></asp:Literal>
                                <h3 style="color: Red;">
                                    <asp:Label ID="LABEL_0005" runat="server"></asp:Label>&nbsp;&nbsp;［<asp:Label ID="RES_NO" runat="server"></asp:Label>］
                                </h3>
                                <asp:Literal ID="Pagefooter" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ResConfirmPanel" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-4 offset-lg-4">
                                <a id="ResConfirmLinkButton" runat="server" class="btn btn-block btn-danger" target="_blank">予約確認書
                                </a>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PaymentPanel" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Literal ID="PageHeader2" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="alert alert-danger">
                                    <b>
                                        <asp:Label ID="LABEL_0014" runat="server"></asp:Label></b><br />
                                    <asp:Label ID="LABEL_0015" runat="server"></asp:Label>
                                    <asp:Label ID="LABEL_0016" runat="server"></asp:Label>
                                    <asp:Label ID="LABEL_0017" runat="server"></asp:Label>
                                    <asp:Label ID="LABEL_0018" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row pdt10">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="tab-ttl">
                                        <h2>
                                            <asp:Label ID="LABEL_0008" runat="server"></asp:Label></h2>
                                    </div>
                                    <div class="card-body">
                                        <Payment:Payment ID="Payment" runat="Server"></Payment:Payment>
                                    </div>
                                </div>
                                <asp:Panel ID="RES_BANKPanel" runat="server">
                                    <div class="card">
                                        <div class="tab-ttl">
                                            <h2>
                                                <asp:Label ID="LABEL_0010" runat="server"></asp:Label></h2>
                                        </div>
                                        <div class="card-body pdt5">
                                            <div class="row">
                                                <div class="col-lg-12 pdb10">
                                                    <asp:Label ID="LABEL_0011" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4 offset-lg-4">
                                                    <asp:LinkButton ID="RES_BANKLinkButton" runat="server" class="btn btn-block btn-danger" OnClientClick="BookWait();">
                                                        <asp:Label ID="LABEL_0013" runat="server"></asp:Label>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Literal ID="Pagefooter2" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </asp:Panel>
                    <%--保険申込画面へ進む--%>
                    <%--<asp:Panel ID="ResInsurancePanel" runat="server" Visible="false">
                        <div class="row pdt10">
                            <div class="col-lg-4 offset-lg-4">
                                <a id="InsuranceLinkButton" runat="server" class="btn btn-block btn-danger" target="_blank">
                                </a>
                            </div>
                        </div>
                    </asp:Panel>--%>
                    <%--キャンセル保険申込画面へ進む--%>
                    <%--<asp:Panel ID="ResInsuranceCXLPanel" runat="server" Visible="false">
                        <div class="row pdt10">
                            <div class="col-lg-4 offset-lg-4">
                                <a id="InsuranceCXLLinkButton" runat="server" class="btn btn-block btn-danger" target="_blank">
                                </a>
                            </div>
                        </div>
                    </asp:Panel>--%>
                    <asp:Panel ID="INSURANCE_SUBSCRIBERPanel" runat="server" Visible="false">
                        <div class="tab-ttl my-3">
                            <h2>
                                <asp:Label ID="LABEL_0030" runat="server">保険申込者</asp:Label>
                                <asp:Label ID="LABEL_0029" CssClass="text-danger small" runat="server">※保険に加入される場合契約者はツアーの申込者となります</asp:Label>
                            </h2>
                        </div>
                        <div class="card shadow mb-4">
                            <div class="card-body">
                                <table class="cp_table mb-0">
                                    <tbody>
                                        <tr>
                                            <th scope="row" class="tbhead-all">
                                                <asp:Label ID="LABEL_0032" runat="server">お名前(Name)</asp:Label>
                                            </th>
                                            <td class="tbcont">
                                                <asp:Label ID="INSURANCE_LABEL_NAME_ROMAN" runat="server"></asp:Label>
                                                <asp:Panel ID="INSURANCE_INPUT_NAME_ROMANPanel" runat="server" class="form-row">
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="INSURANCE_INPUT_SURNAME_ROMAN" runat="server" CssClass="form-control form-control-sm" MaxLength="50" placeholder="例：YAMADA" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="INSURANCE_INPUT_NAME_ROMAN" runat="server" CssClass="form-control form-control-sm" MaxLength="50" placeholder="例：TARO" Style="text-transform: uppercase;"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row" class="tbhead-all">
                                                <asp:Label ID="LABEL_0034" runat="server">お名前(漢字)</asp:Label>
                                            </th>
                                            <td class="tbcont">
                                                <asp:Label ID="INSURANCE_LABEL_NAME_KANJI" runat="server"></asp:Label>
                                                <asp:Panel ID="INSURANCE_INPUT_NAME_KANJIPanel" runat="server" class="form-row">
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="INSURANCE_INPUT_SURNAME_KANJI" runat="server" CssClass="form-control form-control-sm" MaxLength="50" placeholder="例：山田"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="INSURANCE_INPUT_NAME_KANJI" runat="server" CssClass="form-control form-control-sm" MaxLength="50" placeholder="例：太郎"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row" class="tbhead-all">
                                                <asp:Label ID="LABEL_0033" runat="server">生年月日</asp:Label>
                                            </th>
                                            <td class="tbcont">
                                                <asp:Label ID="INSURANCE_BIRTHLabel" runat="server"></asp:Label>
                                                <asp:Panel ID="INSURANCE_BIRTHPanel" runat="server" class="form-row">
                                                    <div class="col-lg-2 col-4 p-1 text-center">
                                                        <asp:TextBox ID="INSURANCE_BIRTH_YYYY" runat="server" CssClass="form-control form-control-sm" MaxLength="4" Style="ime-mode: disabled" placeholder="例：1990"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-1 col-2 p-1 text-center">
                                                        <asp:DropDownList ID="INSURANCE_BIRTH_MM" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-1 col-2 p-1 text-center">
                                                        <asp:DropDownList ID="INSURANCE_BIRTH_DD" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr id="INSURANCEPanel" runat="server">
                                            <th scope="row" class="tbhead-all">
                                                <asp:Label ID="LABEL_0035" runat="server">加入申請</asp:Label>
                                            </th>
                                            <td class="tbcont">
                                                <div class="form-row">
                                                    <asp:Panel ID="INSURANCELinkButtonPanel" runat="server" CssClass="col-lg-5 p-1 text-center">
                                                        <a id="INSURANCELinkButton" class="btn btn-block btn-danger">障害保険</a>
                                                    </asp:Panel>
                                                    <asp:Panel ID="INSURANCECXLLinkButtonPanel" runat="server" CssClass="col-lg-5 p-1 text-center">
                                                        <a id="INSURANCECXLLinkButton" class="btn btn-block btn-danger">キャンセル保険</a>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <div class="masthead">
            <triphoowebFooter:triphoowebFooter ID="triphoowebFooter" runat="Server"></triphoowebFooter:triphoowebFooter>
        </div>
        <script src="../../scripts/custom/Insurance.js?ver=1.00" type="text/javascript"></script>
        <Waiting:Waiting ID="Waiting" runat="Server"></Waiting:Waiting>
        <asp:HiddenField ID="aff_rt_cd" runat="server" />
        <asp:HiddenField ID="aff_site_cd" runat="server" />
        <asp:HiddenField ID="aff_res_no" runat="server" />
        <asp:HiddenField ID="aff_total_price" runat="server" />
        <asp:HiddenField ID="aff_sale_price" runat="server" />
        <asp:HiddenField ID="aff_conv_price" runat="server" />
        <asp:HiddenField ID="aff_member_cd" runat="server" />
        <asp:HiddenField ID="aff_rmks" runat="server" />
        <asp:HiddenField ID="aff_res_sts" runat="server" />
    </form>
    <div id="SCRIPT_FOOTER"></div>
    <div id="jQuery_FOOTER"></div>
    <asp:Literal ID="BODY_BOTTOM_PC" runat="server"></asp:Literal>
    <asp:Literal ID="BODY_BOTTOM_SP" runat="server"></asp:Literal>
</body>
</html>
