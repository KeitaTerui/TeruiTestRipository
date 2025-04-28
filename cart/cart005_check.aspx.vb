Imports tw.src.util
Imports twClient
Imports twClient.TriphooB2CAPI

Partial Class page_cart_cart005_check
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

        Try
            Me.RT_CD.Value = SetRRValue.setNothingValueWeb(Request.Item("RT_CD"))
            Me.S_CD.Value = SetRRValue.setNothingValueWeb(Request.Item("S_CD"))

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

            Dim dsItinerary As New TriphooRR097DataSet
            dsItinerary = WebSessionUtil.GetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsB2CUser)

            setlang(lang)

            dsUser = Session("user" & Me._RT_CD.Value & Me.S_CD.Value)

            ScreenSet(dsItinerary)

            Me.RES_NO.Text = "S0001234"

        Catch ex As Exception
        End Try

    End Sub
#End Region

    ''' <summary>
    ''' スクリーン設定
    ''' </summary>
    Private Sub ScreenSet(dsItinerary As TriphooRR097DataSet)

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

        Dim dsWebScrnRes As New TriphooCMSAPI.ScreenSettingDataSet
        dsWebScrnRes = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart005", lang)

        Dim CmsUtil As CmsUtil = New CmsUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)

        Dim PAGE_HEADER As String = ""
        Dim CSS As String = dsWebScrnRes.DETAIL_RES.Rows(0)("CSS")
        Dim SCRIPT As String = dsWebScrnRes.DETAIL_RES.Rows(0)("SCRIPT")
        Dim META As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "META")
        Dim BODY_TOP_PC As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_TOP_PC")
        Dim BODY_TOP_SP As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_TOP_SP")
        Dim BODY_BOTTOM_PC As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_BOTTOM_PC")
        Dim BODY_BOTTOM_SP As String = SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_BOTTOM_SP")

        PAGE_HEADER += dsWebScrnRes.DETAIL_RES.Rows(0)("HEADER")
        PAGE_HEADER = SetHeaderFooter(PAGE_HEADER)

        Me.PageHeader.Text = PAGE_HEADER
        Me.Pagefooter.Text = SetHeaderFooter(dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER"))

        Me.PageHeader2.Text = PAGE_HEADER
        Me.Pagefooter2.Text = SetHeaderFooter(dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER"))

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

    End Sub

    ' SetHeaderFooter
    Private Function SetHeaderFooter(str As String) As String
        Return str
    End Function

    ''' <summary>
    ''' 言語対応
    ''' </summary>
    Private Sub SetLang(lang As String)

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

End Class
