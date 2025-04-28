<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cart004.aspx.vb" EnableViewState="true"
    EnableEventValidation="false" Inherits="tw.page_cart_cart004" %>

<%@ Register TagPrefix="triphoowebHeader" TagName="triphoowebHeader" Src="../common/header.ascx" %>
<%@ Register TagPrefix="triphoowebFooter" TagName="triphoowebFooter" Src="../common/footer.ascx" %>
<%@ Register TagPrefix="TelMe" TagName="TelMe" Src="../common/TelMe.ascx" %>

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
        <asp:HiddenField ID="GOODS_CD" runat="server" />
        <asp:Literal ID="AFFILIATE_SCRIPTLiteral" runat="server"></asp:Literal>
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
                                    <asp:Label ID="LABEL_0001" runat="server">カート</asp:Label>
                                </li>
                                <li class="breadcrumb-item">
                                    <asp:Label ID="LABEL_0002" runat="server">お客様情報入力</asp:Label>
                                </li>
                                <li class="breadcrumb-item" aria-current="page">
                                    <asp:Label ID="LABEL_0003" runat="server">最終確認画面</asp:Label>
                                </li>
                                <li class="breadcrumb-item active">
                                    <asp:Label ID="LABEL_0004" runat="server">予約完了</asp:Label>
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
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Literal ID="PageHeader" runat="server"></asp:Literal>
                            <h3 style="color: Red;">
                                <asp:Label ID="LABEL_0005" runat="server"></asp:Label>&nbsp;&nbsp;［<asp:Label ID="RES_NO" runat="server"></asp:Label>］
                            </h3>
                            <asp:Literal ID="Pagefooter" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <%--▲ページフッター--%>
                </div>
            </div>
        </div>
        <br />
        <div class="masthead">
            <triphoowebFooter:triphoowebFooter ID="triphoowebFooter" runat="Server"></triphoowebFooter:triphoowebFooter>
        </div>
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
    <div ID="SCRIPT_FOOTER"></div>
    <div id="jQuery_FOOTER"></div>
    <asp:Literal ID="BODY_BOTTOM_PC" runat="server"></asp:Literal>
    <asp:Literal ID="BODY_BOTTOM_SP" runat="server"></asp:Literal>
</body>
</html>
