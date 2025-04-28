<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cart006.aspx.vb" Inherits="tw.cart006" EnableViewState="true"
    EnableEventValidation="false"  %>

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
    <script src="../../scripts/custom/commonCss.js?ver=1.02" type="text/javascript"></script>
    <script src="../../scripts/custom/common.js?ver=1.01" type="text/javascript"></script>
    <script type="text/javascript" src="../../scripts/jquery.flexslider-min.js"></script>
    <script type="text/javascript" src="../../scripts/holiday.js"></script>
    <script src="../../scripts/custom/affiliate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="RT_CD" runat="server" />
        <asp:HiddenField ID="S_CD" runat="server" />
        <asp:HiddenField ID="HD_LANG" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="masthead">
            <triphoowebHeader:triphoowebHeader ID="triphoowebHeader" runat="Server"></triphoowebHeader:triphoowebHeader>
        </div>
        <div class="container">
            <%--▼パンくず--%>

            <div class="step pdl5 pdr5">
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
                </ul>
            </div>
            <%--▲パンくず--%>
            <div class="row">
                <div id="triphoomain" class="col-lg-12">
                    <%--▼ページヘッダー--%>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Literal ID="PageHeader" runat="server"></asp:Literal>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <br />
                                    <font style="font-size: 40px; color: Green; font-weight: bold;">
                                        <asp:Label ID="LABEL_0006" runat="server"></asp:Label></font>
                                    <br />
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="LABEL_0007" runat="server"></asp:Label>
                                    <h3 style="color: Red;">
                                        <asp:Label ID="LABEL_0005" runat="server"></asp:Label>&nbsp;&nbsp;［<asp:Label ID="RES_NO" runat="server"></asp:Label>］
                                    </h3>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div id="ImportantMsg" runat="server" class="col-lg-12">
                                    <asp:Label ID="LABEL_0008" runat="server"></asp:Label><br />
                                    <asp:Label ID="LABEL_0009" runat="server"></asp:Label><br />
                                    <br />
                                    <asp:Label ID="LABEL_0010" runat="server"></asp:Label><br />
                                    <b><asp:Label ID="LABEL_0011" runat="server"></asp:Label></b><br />
                                    <asp:Label ID="LABEL_0012" runat="server"></asp:Label><br />
                                    <asp:Label ID="LABEL_0013" runat="server"></asp:Label><br />
                                    <br />
                                    <b><asp:Label ID="LABEL_0014" runat="server"></asp:Label></b><br />
                                    <asp:Label ID="LABEL_0015" runat="server"></asp:Label><br />
                                    <asp:Label ID="LABEL_0016" runat="server"></asp:Label><br />
                                </div>
                            </div>
                            <asp:Literal ID="Pagefooter" runat="server"></asp:Literal>
                        </div>
                    </div>
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
</body>
</html>