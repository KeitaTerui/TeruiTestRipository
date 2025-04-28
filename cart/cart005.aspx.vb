#Region "Imports"
Imports System.Data
Imports twClient
Imports twClient.TriphooB2CAPI
Imports tw.src.util
#End Region

Partial Class page_cart_cart005
    Inherits System.Web.UI.Page

#Region "クラス宣言"

    'src.util
    Dim CommonUtil As CommonUtil
    Dim CommonWebAuthentication As New CommonWebAuthentication
    Dim CreateWebServiceManager As New CreateWebServiceManager
    Dim ParameterUtil As New ParameterUtil
    Dim WebSessionUtil As WebSessionUtil

    Dim B2CAPIClient As TriphooB2CAPI.Service
    Dim dsB2CUser As DataSet

    ' NSSOL負荷性能検証 2023/02/20
    'Dim logger As NSTSTLogger

    'TriphooRRUtil.src.util
    Dim SetRRValue As New TriphooRRUtil.src.util.SetValue
    Dim SetValue As New SetValue

    'DataSet
    Dim dsRTUser As UserDataSet
    Dim dsUser As New DataSet
    Dim lang As String = "1"

    Dim LABEL_0006 As String = ""
    Dim MEMBER_ADD_KBN As String = ""
    Dim BACK_OFFICE_KBN As String = ""
    Dim AF As String = ""

#End Region

#Region "初期処理"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' ロガー初期化 NSSOL負荷性能検証 2023/02/20
        'logger = New NSTSTLogger("cart005", "D:\", Request.Item("RT_CD"))

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "Page_Load()", 47, "開始", HttpContext.Current.Session.SessionID)

        Try
            Me.RT_CD.Value = SetRRValue.setNothingValueWeb(Request.Item("RT_CD"))
            Me.S_CD.Value = SetRRValue.setNothingValueWeb(Request.Item("S_CD"))
            Me.SESSION_NO.Value = SetRRValue.setNothingValueWeb(Request.Item("skey"))
            AF = SetRRValue.setNothingValueWeb(Request.Item("AF"))

            '●リテーラー認証 処理
            Dim RedirectUrl As String = ""
            Dim CommonCheck As Boolean = CommonWebAuthentication.UserCheck(Me._RT_CD.Value, Me.S_CD.Value, dsRTUser, Request, RedirectUrl)

            If Not CommonCheck Then
                If RedirectUrl.Equals("") Then
                    Throw New Exception("リテーラー認証エラー : RT_CD=" & Me._RT_CD.Value & " , SITE_CD=" & Me.S_CD.Value)
                Else
                    Response.Redirect(RedirectUrl, True)
                End If
            End If

            Select Case Me.RT_CD.Value
                Case "WTB", "WOW"
                Case Else
                    If Request.Url.Scheme.Equals("http") And Not ParameterUtil.DEBUG_FLG Then
                        If Not Request.Url.AbsoluteUri.Contains("test") Then
                            Dim url As String = Request.Url.AbsoluteUri.Replace("http://", "https://")
                            Response.Redirect(url, True)
                        End If
                    End If
            End Select


            If Not SetRRValue.setNothingValueWeb(Request.Item("lang")).Equals("") Then
                Session("lang") = Request.Item("lang")
            End If
            If Not Session("lang") Is Nothing Then
                lang = Session("lang")
            End If
            Me.HD_LANG.Value = lang

            '●インスタンス化
            CommonUtil = New CommonUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)
            B2CAPIClient = CreateWebServiceManager.CreateTriphooB2CAPIClient(Me.RT_CD.Value, Me.S_CD.Value)
            dsB2CUser = CreateWebServiceManager.CreateTriphooB2CAPIUser(Me.RT_CD.Value, Me.S_CD.Value, Request)
            WebSessionUtil = New WebSessionUtil(Me.RT_CD.Value, Me.S_CD.Value)

            MEMBER_ADD_KBN = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")
            BACK_OFFICE_KBN = dsRTUser.M137_RT_SITE_CD.Rows(0)("BACK_OFFICE_KBN")

            '●リンク先設定
            Dim CART001URL As String = ""
            Select Case Me.RT_CD.Value
                Case "ASX"
                    CART001URL = "../login/login009?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value
                Case Else
                    CART001URL = "cart001?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value
            End Select


            '●セッション 更新
            Dim dsItinerary As New TriphooRR097DataSet

            'Dim SessionCheck As Boolean = True
            'SessionCheck = CommonWebAuthentication.SessionCheck(Me.RT_CD.Value, Me.S_CD.Value, "Itinerary", dsItinerary)
            dsItinerary = WebSessionUtil.GetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsB2CUser)

            If dsItinerary Is Nothing Or dsItinerary.PAGE_03.Rows.Count = 0 Then
                If Not IsPostBack Then
                    Response.Redirect(CART001URL, True)
                Else
                    '●アクション群
                    Dim actionEve As String = Request.Item("__EVENTTARGET")

                End If
            Else

                '    '●言語対応
                setlang(lang)

                dsUser = Session("user" & Me._RT_CD.Value & Me.S_CD.Value)

                '●スクリーン設定
                ScreenSet(dsItinerary)

                If Not IsPostBack Then
                    '●ページ設定
                    iniPage(dsItinerary)
                Else
                    Dim actionEve As String = Request.Item("__EVENTTARGET")
                    If actionEve.Contains("RES_BANKLinkButton") Then

                        'DB Update
                        Dim CartUtil As New CartUtil(Me.RT_CD.Value, Me.S_CD.Value, Request, lang)
                        CartUtil.PayUpdate(dsItinerary, Me.RT_CD.Value, Me.S_CD.Value, "02", "01", lang, dsUser)

                        Dim url As String = "../cart/cart006"
                        url += "?RT_CD=" & Me.RT_CD.Value
                        url += "&S_CD=" & Me.S_CD.Value
                        Response.Redirect(url, True)

                    End If
                End If

            End If

        Catch ex As Exception
            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart005", "Page_Load()", 144, "終了（エラーorリダイレクト）", HttpContext.Current.Session.SessionID)

            ' ログファイルへの書き込み NSSOL負荷性能検証 2023/02/09
            'logger.WriteLogToFile()

            '●エラー処理
            If Not (ex.Message.StartsWith("スレッドを中止") Or ex.Message.StartsWith("Thread was being aborted")) Then
                Dim ConcreteException As New src.common.ConcreteException
                ConcreteException.Exception(Request.Item("RT_CD"), Request.Item("S_CD"), "cart005", ex)
                Response.Redirect("../err/err002?RT_CD=" & Request.Item("RT_CD") & "&S_CD=" & Request.Item("S_CD"))
            End If
        End Try

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "Page_Load()", 158, "終了", HttpContext.Current.Session.SessionID)

        ' ログファイルへの書き込み NSSOL負荷性能検証 2023/02/09
        'logger.WriteLogToFile()

    End Sub
#End Region

#Region "スクリーン設定"
    Private Sub ScreenSet(dsItinerary As TriphooRR097DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "ScreenSet()", 170, "開始", HttpContext.Current.Session.SessionID)

        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")
        Dim RES_STATUS_KBN As String = dsItinerary.PAGE_03.Rows(0)("RES_STATUS_KBN")

        '/* 初期設定 * /
        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/base/lang.xml")

        Dim rXml() As DataRow = dsXml.Tables("TITLE").Select("LANGUAGE_KBN='" & lang & "'")

        Dim PAGE_TITLE As String = ""

        If rXml.Length > 0 Then
            PAGE_TITLE = rXml(0)("cart005")
            PAGE_TITLE = PAGE_TITLE.Replace("@@SITE_TITLE@@", dsRTUser.M137_RT_SITE_CD.Rows(0)("SITE_TITLE"))
            Me.Title = PAGE_TITLE
        End If
        '/* 初期設定 * /

        Dim _SETTLE_KBN As String = dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN")
        Dim PAYMENT_PAGE_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("PAYMENT_PAGE_KBN")

        Dim dsWebScrnRes As New TriphooCMSAPI.ScreenSettingDataSet

        Select Case RES_STATUS_KBN
            Case "WT"
                dsWebScrnRes = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart005_3", lang)
            Case Else

                If Not PAYMENT_PAGE_KBN.Equals("1") And _SETTLE_KBN.Equals("01") Then
                    dsWebScrnRes = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart005_2", lang)
                Else
                    dsWebScrnRes = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart005", lang)
                End If

        End Select

        If dsWebScrnRes.DETAIL_RES.Rows.Count = 0 Then
            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart005", "ScreenSet()", 214, "終了", HttpContext.Current.Session.SessionID)

            Exit Sub
        End If

        Dim CmsUtil As CmsUtil = New CmsUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)

        Dim PAGE_HEADER As String = ""
        Dim CSS As String = dsWebScrnRes.DETAIL_RES.Rows(0)("CSS")
        Dim SCRIPT As String = dsWebScrnRes.DETAIL_RES.Rows(0)("SCRIPT")
        Dim META As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "META")
        Dim BODY_TOP_PC As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_TOP_PC")
        Dim BODY_TOP_SP As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_TOP_SP")
        Dim BODY_BOTTOM_PC As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_BOTTOM_PC")
        Dim BODY_BOTTOM_SP As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_BOTTOM_SP")

        CmsUtil.isPcOrSpAndReservationChange(BODY_TOP_PC, BODY_TOP_SP, BODY_BOTTOM_PC, BODY_BOTTOM_SP, Request, dsItinerary)

        META = CmsUtil.bookingComplete(META, dsItinerary)
        BODY_TOP_PC = CmsUtil.bookingComplete(BODY_TOP_PC, dsItinerary)
        BODY_TOP_SP = CmsUtil.bookingComplete(BODY_TOP_SP, dsItinerary)
        BODY_BOTTOM_PC = CmsUtil.bookingComplete(BODY_BOTTOM_PC, dsItinerary)
        BODY_BOTTOM_SP = CmsUtil.bookingComplete(BODY_BOTTOM_SP, dsItinerary)

        PAGE_HEADER += dsWebScrnRes.DETAIL_RES.Rows(0)("HEADER")
        PAGE_HEADER = setHeaderFooter(PAGE_HEADER, dsItinerary)

        Dim _car_cid As String = SetRRValue.setNothingValueWeb(Request.Item("_car-cid"))
        Dim _car_af As String = SetRRValue.setNothingValueWeb(Request.Item("_car-af"))
        If Not _car_af.Equals("") And Not _car_cid.Equals("") Then
            PAGE_HEADER += CommonUtil.SetMarkCodeLineApp(_car_cid, Request.Url.Host)

            If TICKET_FLG Then
                PAGE_HEADER += CommonUtil.SetConvCodeLineAppAir(dsItinerary, Me.RT_CD.Value, _car_cid, _car_af)
            End If

            If PACKAGE_FLG Then
                PAGE_HEADER += CommonUtil.SetConvCodeLineAppTour(dsItinerary, Me.RT_CD.Value, _car_cid, _car_af)
            End If

        End If

        Me.PageHeader.Text = PAGE_HEADER
        Me.Pagefooter.Text = setHeaderFooter(dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER"), dsItinerary)

        Me.PageHeader2.Text = PAGE_HEADER
        Me.Pagefooter2.Text = setHeaderFooter(dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER"), dsItinerary)

        If Not dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE").Equals("") Then
            Me.Title = dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE")
        End If

        Me.MetaDescription = dsWebScrnRes.DETAIL_RES.Rows(0)("DESCRIPTION")
        Me.MetaKeywords = dsWebScrnRes.DETAIL_RES.Rows(0)("KEYWORD")
        'Me.SCRIPTLiteral.Text = setHeaderFooter(CSS & SCRIPT, dsItinerary)

        Me.PAGE_CSS.Text = CSS
        Me.PAGE_SCRIPT.Text = SCRIPT
        Me.PAGE_META.Text = META

        Me.BODY_BOTTOM_PC.Text = BODY_BOTTOM_PC
        Me.BODY_BOTTOM_SP.Text = BODY_BOTTOM_SP

        If Not BODY_TOP_PC.Equals("") Then
            Dim BODY_TOP_PCLiteral As New Literal()
            BODY_TOP_PCLiteral.Text = BODY_TOP_PC
            Me.Page.Header.Controls.Add(BODY_TOP_PCLiteral)
        End If

        If Not BODY_TOP_SP.Equals("") Then
            Dim BODY_TOP_SPLiteral As New Literal()
            BODY_TOP_SPLiteral.Text = BODY_TOP_SP
            Me.Page.Header.Controls.Add(BODY_TOP_SPLiteral)
        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "ScreenSet()", 256, "終了", HttpContext.Current.Session.SessionID)

        ' パンくず表示／非表示設定
        Select Case Me.RT_CD.Value
            Case "ASX"
                Me.BREADCRUMBPanel.Visible = False
        End Select

    End Sub
#End Region

#Region "ページ設定"

#Region "iniPage"
    Private Sub iniPage(dsItinerary As TriphooRR097DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "iniPage()", 267, "開始", HttpContext.Current.Session.SessionID)

        Dim _RES_STATUS_KBN As String = dsItinerary.PAGE_03.Rows(0)("RES_STATUS_KBN")
        Dim _SETTLE_KBN As String = dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN")
        Dim DEP_TIME As String = dsItinerary.PAGE_03.Rows(0)("DEP_TIME")
        Dim PAYMENT_PAGE_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("PAYMENT_PAGE_KBN")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")
        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
        Dim KARTE_KBN As String = dsItinerary.PAGE_03.Rows(0)("KARTE_KBN")

        Dim isPackage As Boolean = False

        If PACKAGE_FLG Then
            isPackage = True
        End If

        If TICKET_FLG And HOTEL_FLG Then

            Select Case KARTE_KBN
                Case "06" : isPackage = True
            End Select

        End If

        If Not Session("Affiliate" & Me.RT_CD.Value & Me.S_CD.Value & AF) Is Nothing Then

            Dim COMMON_ACCOUNT_TYPE_CD_CLIENT As String = "7500"
            Dim ACCOUNT_TYPE_CD_CLIENT As String = "7500"

            Dim COMMON_ACCOUNT_TYPE_CD_AFFILIATE As String = "7600"
            Dim ACCOUNT_TYPE_CD_AFFILIATE As String = "7600"

            Dim dsM035_COMMON_ACCOUNT_TYPE As TriphooB2CAPI.M035_COMMON_ACCOUNT_TYPE_DataSet = B2CAPIClient.SelectM035_COMMON_ACCOUNT_TYPEGateway(Me.RT_CD.Value, "", "", dsB2CUser)
            Try
                ACCOUNT_TYPE_CD_CLIENT = dsM035_COMMON_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Select("COMMON_ACCOUNT_TYPE_CD = '" & COMMON_ACCOUNT_TYPE_CD_CLIENT & "'")(0)("ACCOUNT_TYPE_CD")
                ACCOUNT_TYPE_CD_AFFILIATE = dsM035_COMMON_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Select("COMMON_ACCOUNT_TYPE_CD = '" & COMMON_ACCOUNT_TYPE_CD_AFFILIATE & "'")(0)("ACCOUNT_TYPE_CD")
            Catch ex As Exception
            End Try

            Dim SALES_PRICE As Decimal = 0
            Dim CONVERTION_PRICE As Decimal = 0
            Dim TOTAL_PRICE As Decimal = 0

            For Each row In dsItinerary.RES_ORDER_DATA.Rows
                Dim SALES_SUB_TOTAL As Decimal = row("SALES_SUB_TOTAL")
                Dim ACCOUNT_TYPE_CD As String = row("ACCOUNT_TYPE_CD")

                If ACCOUNT_TYPE_CD_CLIENT.Equals(ACCOUNT_TYPE_CD) Then
                    SALES_PRICE += SALES_SUB_TOTAL
                ElseIf ACCOUNT_TYPE_CD_AFFILIATE.Equals(ACCOUNT_TYPE_CD) Then
                    CONVERTION_PRICE += SALES_SUB_TOTAL
                Else
                    TOTAL_PRICE += SALES_SUB_TOTAL
                End If
            Next

            Dim dsAffiliate As TriphooB2CAPI.AffiliateDetailRes = Session("Affiliate" & Me.RT_CD.Value & Me.S_CD.Value & AF)
            Dim RES_STATUS_KBN As String = dsItinerary.PAGE_03.Rows(0)("RES_STATUS_KBN")
            If dsAffiliate.M279_AFFILIATE.Rows(0)("AFFILIATE_KBN").Equals("01") Then
                RES_STATUS_KBN = "OK"
            End If

            Me.aff_rt_cd.Value = Me.RT_CD.Value
            Me.aff_site_cd.Value = Me.S_CD.Value
            Me.aff_res_no.Value = dsItinerary.PAGE_03.Rows(0)("RES_NO")
            Me.aff_total_price.Value = TOTAL_PRICE
            Me.aff_sale_price.Value = SALES_PRICE
            Me.aff_conv_price.Value = Replace(CONVERTION_PRICE, "-", "")
            Me.aff_member_cd.Value = ""
            Me.aff_rmks.Value = ""
            Me.aff_res_sts.Value = RES_STATUS_KBN

            If dsItinerary.WEB_TRANSACTION.Rows.Count > 0 Then
                Me.aff_member_cd.Value = dsItinerary.WEB_TRANSACTION.Rows(0)("COUPON_CD")
            End If

            Dim affiliateUtil As New AffiliateUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)
            Dim AffiliateScript As String = affiliateUtil.AffiliateParamNewCheck(dsItinerary, dsAffiliate, "CV")

            Me.AFFILIATE_SCRIPTLiteral.Text = AffiliateScript

            dsItinerary.RES_VENDER_LOG.Clear()
            Dim rRES_VENDER_LOG As TriphooRR097DataSet.RES_VENDER_LOGRow = dsItinerary.RES_VENDER_LOG.NewRES_VENDER_LOGRow
            rRES_VENDER_LOG.SEQ = 0
            rRES_VENDER_LOG.TYPE = "Affiliate"
            rRES_VENDER_LOG.GDS_KBN = ""
            rRES_VENDER_LOG.ACTION = "CV"
            rRES_VENDER_LOG.REQUEST = ""
            rRES_VENDER_LOG.REQUEST_TIME = "1900/01/01"
            rRES_VENDER_LOG.RESPONSE = AffiliateScript
            rRES_VENDER_LOG.RESPONSE_TIME = Now
            rRES_VENDER_LOG.EDIT_TIME = Now
            rRES_VENDER_LOG.EDIT_EMP_CODE = "tw"
            dsItinerary.RES_VENDER_LOG.AddRES_VENDER_LOGRow(rRES_VENDER_LOG)

            B2CAPIClient.TriphooRR097s038act001Mapper(dsItinerary, dsB2CUser)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "AffiliateConv();", True)
        End If

        If 0 < dsItinerary.PAGE_20.Rows.Count Then
            Me.GOODS_CD.Value = dsItinerary.PAGE_20.Rows(0)("GOODS_CD")
        End If

        If Not PAYMENT_PAGE_KBN.Equals("1") And _SETTLE_KBN.Equals("01") Then
            Me.PaymentPanel.Visible = True
            Me.PaymentComplete.Visible = True
            Me.ResInfoPanel.Visible = False
            Me.LABEL_0004.Text = LABEL_0006
            Me.Title = LABEL_0006
            If 0 < dsItinerary.WEB_TRANSACTION.Rows.Count Then
                Dim GOODS_SETTLE_KBN As String = dsItinerary.WEB_TRANSACTION.Rows(0)("GOODS_SETTLE_KBN")

                If GOODS_SETTLE_KBN.Equals("02") Then
                    Me.RES_BANKPanel.Visible = False
                End If
            End If
        Else
            Me.ResInfoPanel.Visible = True
            Me.RES_NO.Text = dsItinerary.PAGE_03.Rows(0)("RES_NO")

            Dim OVERSEAS_DOMESTIC_KBN As String = dsItinerary.PAGE_03.Rows(0)("OVERSEAS_DOMESTIC_KBN")

            Select Case MEMBER_ADD_KBN
                Case "3"
                    If BACK_OFFICE_KBN.Equals("4") Then

                        If OVERSEAS_DOMESTIC_KBN.Equals("01") Then
                            '海外

                            If 0 < dsItinerary.PAGE_05.Rows.Count Then
                                dsItinerary.PAGE_05.Rows(0)("SUP_FINLAL_LIMIT_DATE") = "1900/01/01"
                            End If

                            Session.Add("MyPage" & Me.RT_CD.Value & Me.S_CD.Value & "-" & dsItinerary.PAGE_03.Rows(0)("RES_NO"), dsItinerary)

                            Dim url As String = "../b2b/confirm"
                            url += "?RT_CD=" & Me.RT_CD.Value
                            url += "&S_CD=" & Me.S_CD.Value
                            url += "&RES_NO=" & dsItinerary.PAGE_03.Rows(0)("RES_NO")
                            url += "&LANGUAGE_KBN=" & "1"

                            url = "WindowOpen('" & url & "'); return false;"

                            Me.ResConfirmLinkButton.Attributes.Add("OnClick", url)
                            Me.ResConfirmPanel.Visible = True

                        Else

                            Session.Add("MyPage" & Me.RT_CD.Value & Me.S_CD.Value & "-" & dsItinerary.PAGE_03.Rows(0)("RES_NO"), dsItinerary)

                            Dim url As String = "/page/itinerary/itinerary002"
                            url += "?RT_CD=" & Me.RT_CD.Value
                            url += "&S_CD=" & Me.S_CD.Value
                            url += "&RES_NO=" & dsItinerary.PAGE_03.Rows(0)("RES_NO")
                            url += "&LANGUAGE_KBN=" & "1"

                            Me.ResConfirmLinkButton.HRef = url
                            Me.ResConfirmPanel.Visible = True
                            Me.ResConfirmLinkButton.InnerText = "国内線マイページ"
                        End If
                    End If
            End Select


            '保険の設定
            '保険加入申請ボタンのURLを設定
            'Me.InsuranceLinkButton.HRef = CommonUtil.setInsuranceUrl(dsItinerary, dsRTUser, "01", Me.ResInsurancePanel, Me.RT_CD.Value, Me.S_CD.Value)
            'キャンセル保険加入申請ボタンのURLを設定
            'Me.InsuranceCXLLinkButton.HRef = CommonUtil.setInsuranceUrl(dsItinerary, dsRTUser, "02", Me.ResInsuranceCXLPanel, Me.RT_CD.Value, Me.S_CD.Value)

            '保険の設定
            'Dim INSURANCE_KBN_OVERSEAS As String = SetRRValue.SetRowValue(dsRTUser.M137_RT_SITE_CD.Rows(0), "INSURANCE_KBN_OVERSEAS")
            'Dim INSURANCE_KBN_DOMESTIC As String = SetRRValue.SetRowValue(dsRTUser.M137_RT_SITE_CD.Rows(0), "INSURANCE_KBN_DOMESTIC")
            'Dim INSURANCE_CXL_KBN_OVERSEAS As String = SetRRValue.SetRowValue(dsRTUser.M137_RT_SITE_CD.Rows(0), "INSURANCE_CXL_KBN_OVERSEAS")
            'Dim INSURANCE_CXL_KBN_DOMESTIC As String = SetRRValue.SetRowValue(dsRTUser.M137_RT_SITE_CD.Rows(0), "INSURANCE_CXL_KBN_DOMESTIC")
            Dim INSURANCE_KBN_OVERSEAS As String = ""
            Dim INSURANCE_KBN_DOMESTIC As String = ""
            Dim INSURANCE_CXL_KBN_OVERSEAS As String = ""
            Dim INSURANCE_CXL_KBN_DOMESTIC As String = ""

            '保険申込パネル制御
            If INSURANCE_KBN_OVERSEAS.Equals("") And INSURANCE_KBN_DOMESTIC.Equals("") And INSURANCE_CXL_KBN_OVERSEAS.Equals("") And INSURANCE_CXL_KBN_DOMESTIC.Equals("") Then
                Me.INSURANCE_SUBSCRIBERPanel.Visible = False
            Else
                Me.INSURANCE_SUBSCRIBERPanel.Visible = True

                '障害保険
                Me.INSURANCELinkButtonPanel.Visible = CommonUtil.setInsuranceVisible(dsItinerary, "01", INSURANCE_KBN_OVERSEAS, INSURANCE_KBN_DOMESTIC, INSURANCE_CXL_KBN_OVERSEAS, INSURANCE_CXL_KBN_DOMESTIC)

                'キャンセル保険
                Me.INSURANCECXLLinkButtonPanel.Visible = CommonUtil.setInsuranceVisible(dsItinerary, "02", INSURANCE_KBN_OVERSEAS, INSURANCE_KBN_DOMESTIC, INSURANCE_CXL_KBN_OVERSEAS, INSURANCE_CXL_KBN_DOMESTIC)

                'カートはボタンが表示されなければ保険加入非表示
                If Not Me.INSURANCELinkButtonPanel.Visible And Not Me.INSURANCECXLLinkButtonPanel.Visible Then
                    Me.INSURANCEPanel.Visible = False
                    Me.INSURANCE_SUBSCRIBERPanel.Visible = False
                Else
                    Dim JOIN_CD As String = dsItinerary.PAGE_03.Rows(0)("RES_NO") & "_" & dsItinerary.PAGE_03.Rows(0)("CLIENT_CD")
                    Dim secureUtil As New TriphooRRUtil.src.util.SecureUtil
                    Me.JOIN_CD.Value = HttpUtility.UrlEncode(secureUtil.EncryptString(JOIN_CD, secureUtil.PASSCODE_KEY))
                    Me.DEP_TIME.Value = CDate(dsItinerary.PAGE_03.Rows(0)("DEP_TIME")).ToString("yyyyMMdd")

                    '名前(ローマ字)
                    Dim SURNAME_ROMAN As String = dsItinerary.M019_CLIENT.Rows(0)("SURNAME_ROMAN")
                    Dim NAME_ROMAN As String = dsItinerary.M019_CLIENT.Rows(0)("NAME_ROMAN")

                    If SURNAME_ROMAN.Equals("") Or NAME_ROMAN.Equals("") Then
                        Me.INSURANCE_LABEL_NAME_ROMAN.Visible = False
                        Me.INSURANCE_INPUT_NAME_ROMANPanel.Visible = True
                    Else
                        Me.INSURANCE_LABEL_NAME_ROMAN.Visible = True
                        Me.INSURANCE_LABEL_NAME_ROMAN.Text = SURNAME_ROMAN & " " & NAME_ROMAN
                        Me.INSURANCE_INPUT_NAME_ROMANPanel.Visible = False
                    End If

                    '名前(漢字)
                    Dim M019_SURNAME_KANJI As String = dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI")
                    Dim M019_NAME_KANJI As String = dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI")

                    If M019_SURNAME_KANJI.Equals("") Or M019_NAME_KANJI.Equals("") Then
                        Me.INSURANCE_LABEL_NAME_KANJI.Visible = False
                        Me.INSURANCE_INPUT_NAME_KANJIPanel.Visible = True
                    Else
                        Me.INSURANCE_LABEL_NAME_KANJI.Visible = True
                        Me.INSURANCE_LABEL_NAME_KANJI.Text = M019_SURNAME_KANJI & " " & M019_NAME_KANJI
                        Me.INSURANCE_INPUT_NAME_KANJIPanel.Visible = False
                    End If

                    '生年月日
                    Dim BIRTH As String = dsItinerary.M019_CLIENT.Rows(0)("BIRTH")

                    If BIRTH.Equals("") Then
                        Me.INSURANCE_BIRTHLabel.Visible = False
                        Me.INSURANCE_BIRTHPanel.Visible = True

                        Me.INSURANCE_BIRTH_MM.DataSource = SetValue.setBIRTH_MM(True)
                        Me.INSURANCE_BIRTH_MM.DataTextField = "TEXT"
                        Me.INSURANCE_BIRTH_MM.DataValueField = "VALUE"
                        Me.INSURANCE_BIRTH_MM.DataBind()

                        Me.INSURANCE_BIRTH_DD.DataSource = SetValue.setBIRTH_DD(True)
                        Me.INSURANCE_BIRTH_DD.DataTextField = "TEXT"
                        Me.INSURANCE_BIRTH_DD.DataValueField = "VALUE"
                        Me.INSURANCE_BIRTH_DD.DataBind()

                    Else
                        Me.INSURANCE_BIRTHLabel.Visible = True
                        Me.INSURANCE_BIRTHLabel.Text = BIRTH
                        Me.INSURANCE_BIRTHPanel.Visible = False
                    End If
                End If
            End If

            'Session.Remove("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value)
            WebSessionUtil.RemoveSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)
            Session.Remove("dsAir" & Me.RT_CD.Value & Me.S_CD.Value)
            Session.Remove("dsAirDome" & Me.RT_CD.Value & Me.S_CD.Value)
            Session.Remove("dsHote" & Me.RT_CD.Value & Me.S_CD.Value)
            'Session.Remove("dsTour" & Me.RT_CD.Value & Me.S_CD.Value)
            WebSessionUtil.RemoveSession(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "dsTour")
            Session.Remove("dsOption" & Me.RT_CD.Value & Me.S_CD.Value)
            Session.Remove("MEDIA" & Me.RT_CD.Value & Me.S_CD.Value)
            Session.Remove("_car-cid" & Me.RT_CD.Value & Me.S_CD.Value)
            Session.Remove("_car-af" & Me.RT_CD.Value & Me.S_CD.Value)

            Select Case Me.RT_CD.Value & Me.S_CD.Value
                Case "ASX"
                    ' 何もしない
                Case Else
                    Session.Remove("skey" & Me.RT_CD.Value & Me.S_CD.Value)
            End Select

            'セッションデータ削除

            Select Case Me.RT_CD.Value & Me.S_CD.Value
                Case "NTABTM02", "TWC03"
                Case Else

                    Select Case MEMBER_ADD_KBN
                        Case "3"
                        Case Else
                            If Not dsUser Is Nothing Then
                                'パスワードが空白のユーザーだった場合
                                If dsUser.Tables("CLIENT_RES").Rows(0)("PASSWORD").Equals("") Then
                                    Session.Remove("user" & Me.RT_CD.Value & Me.S_CD.Value)
                                End If
                            End If
                    End Select

            End Select

            'Select Case OVERSEAS_DOMESTIC_KBN
            '    Case "02"

            '        Dim isDisp As Boolean = True

            '        If Not isPackage Or INSURANCE_KBN_DOMESTIC.Equals("") Then
            '            isDisp = False
            '        End If

            '        Dim CALC_DEP_TIME As Date = CDate(DEP_TIME).AddDays(-1).ToString("yyyy/MM/dd")
            '        Dim _today As Date = Today.ToString("yyyy/MM/dd")

            '        Dim _dateDiff As Integer = DateDiff(DateInterval.Day, _today, CALC_DEP_TIME)

            '        If _dateDiff < 10 Then
            '            isDisp = False
            '        End If

            '        Dim rPAGE_04() As DataRow = dsItinerary.PAGE_04.Select("STATUS_KBN='01' AND INSURANCE_JOIN_KBN IN('','01') AND AGE <= 80")

            '        Select Case _RES_STATUS_KBN
            '            Case "RC", "CX", "NG", "WT"
            '                isDisp = False
            '            Case Else
            '                If rPAGE_04.Length = 0 Then
            '                    isDisp = False
            '                End If
            '        End Select

            '        If isDisp Then

            '            Dim JOIN_CD As String = dsItinerary.PAGE_03.Rows(0)("RES_NO") & "_" & dsItinerary.PAGE_03.Rows(0)("CLIENT_CD")
            '            Dim secureUtil As New TriphooRRUtil.src.util.SecureUtil
            '            JOIN_CD = secureUtil.EncryptString(JOIN_CD, secureUtil.PASSCODE_KEY)

            '            Dim InsuranceUrl As String = "/page/insurance/?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&JOIN_CD=" & HttpUtility.UrlEncode(JOIN_CD)

            '            Me.InsuranceLinkButton.HRef = InsuranceUrl & "&INSURANCE_TYPE=01"
            '            Me.ResInsurancePanel.Visible = True

            '        End If


            'End Select



        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "iniPage()", 497, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#End Region

#Region "util"

#Region "setHeaderFooter"
    Private Function setHeaderFooter(str As String, dsItinerary As TriphooRR097DataSet) As String

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "setHeaderFooter()", 510, "開始", HttpContext.Current.Session.SessionID)

        Dim temp As String = str

        If 0 < dsItinerary.M019_CLIENT.Rows.Count Then
            str = str.Replace("@@E_MAIL@@", dsItinerary.M019_CLIENT.Rows(0)("E_MAIL"))
        Else
            str = str.Replace("@@E_MAIL@@", "")
        End If
        str = str.Replace("@@RES_NO@@", dsItinerary.PAGE_03.Rows(0)("RES_NO"))
        str = str.Replace("@@NUM_ADULT@@", dsItinerary.PAGE_03.Rows(0)("NUM_ADULT"))
        str = str.Replace("@@NUM_CHILD@@", dsItinerary.PAGE_03.Rows(0)("NUM_CHILD"))
        str = str.Replace("@@NUM_INFANT@@", dsItinerary.PAGE_03.Rows(0)("NUM_INFANT"))

        Dim PRICE As Decimal = 0.0
        Dim ITEM_1 As String = "" ' 商品のID/個数/単価/
        Dim ITEM_2 As String = ""
        Dim ITEM_3 As String = ""
        Dim ITEM_4 As String = ""
        Dim ITEM_5 As String = ""
        Dim ITEMS As String = "" ' 商品コード羅列

        Dim i As Integer = 0
        For Each row In dsItinerary.RES_OPTION.Rows

            Dim OPTION_SEQ As Integer = row("OPTION_SEQ")

            Dim sale_price As Decimal = 0.0
            Dim rRES_ORDER_DATA() As DataRow = dsItinerary.RES_ORDER_DATA.Select("GOODS_SEQ = " & OPTION_SEQ)
            For Each r In rRES_ORDER_DATA
                sale_price += r("SALES_UNIT") * r("AMOUNT")
            Next

            PRICE += sale_price

            Select Case i
                Case 0
                    ITEM_1 = row("GOODS_CD") & "/1/" & sale_price & "/"
                Case 1
                    ITEM_1 = row("GOODS_CD") & "/1/" & sale_price & "/"
                Case 2
                    ITEM_1 = row("GOODS_CD") & "/1/" & sale_price & "/"
                Case 3
                    ITEM_1 = row("GOODS_CD") & "/1/" & sale_price & "/"
                Case 4
                    ITEM_1 = row("GOODS_CD") & "/1/" & sale_price & "/"
            End Select

            ITEMS += row("GOODS_CD") & ","

            i += 1
        Next

        If ITEMS.EndsWith(",") Then
            ITEMS = ITEMS.TrimEnd(",")
        End If

        str = str.Replace("@@PRICE@@", PRICE)
        str = str.Replace("@@ITEM_1@@", ITEM_1)
        str = str.Replace("@@ITEM_2@@", ITEM_2)
        str = str.Replace("@@ITEM_3@@", ITEM_3)
        str = str.Replace("@@ITEM_4@@", ITEM_4)
        str = str.Replace("@@ITEM_5@@", ITEM_5)
        str = str.Replace("@@ITEMS@@", ITEMS)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "setHeaderFooter()", 576, "終了", HttpContext.Current.Session.SessionID)

        Return str
    End Function
#End Region

#End Region

#Region "言語対応"
    Private Sub setlang(lang As String)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "setlang()", 588, "開始", HttpContext.Current.Session.SessionID)

        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/cart/" & lang & "/cart005.xml")
        Dim rXml As DataRow = dsXml.Tables("LABEL").Rows(0)

        Me.LABEL_0001.Text = rXml("LABEL_0001")
        Me.LABEL_0002.Text = rXml("LABEL_0002")
        Me.LABEL_0003.Text = rXml("LABEL_0003")
        Me.LABEL_0004.Text = rXml("LABEL_0004")
        Me.LABEL_0005.Text = rXml("LABEL_0005")
        LABEL_0006 = rXml("LABEL_0006")
        Me.LABEL_0007.Text = rXml("LABEL_0007")
        Me.LABEL_0008.Text = rXml("LABEL_0008")
        Me.LABEL_0010.Text = rXml("LABEL_0010")
        Me.LABEL_0011.Text = rXml("LABEL_0011")
        Me.LABEL_0013.Text = rXml("LABEL_0013")
        Me.LABEL_0014.Text = rXml("LABEL_0014")
        Me.LABEL_0015.Text = rXml("LABEL_0015")
        Me.LABEL_0016.Text = rXml("LABEL_0016")
        Me.LABEL_0017.Text = rXml("LABEL_0017")
        Me.LABEL_0018.Text = rXml("LABEL_0018")
        'Me.InsuranceLinkButton.InnerText = rXml("LABEL_0019")
        'Me.InsuranceCXLLinkButton.InnerText = rXml("LABEL_0020")

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart005", "setlang()", 588, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

End Class
