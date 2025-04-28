#Region "Imports"
Imports System.Data
Imports twClient
Imports twClient.TriphooB2CAPI
Imports tw.src.util
#End Region

Public Class cart006
    Inherits System.Web.UI.Page

#Region "クラス宣言"

    'src.util
    Dim CommonUtil As CommonUtil
    Dim CommonWebAuthentication As New CommonWebAuthentication
    Dim CreateWebServiceManager As New CreateWebServiceManager
    Dim ParameterUtil As New ParameterUtil

    Dim B2CAPIClient As TriphooB2CAPI.Service
    Dim dsB2CUser As DataSet

    'TriphooRRUtil.src.util
    Dim SetRRValue As New TriphooRRUtil.src.util.SetValue

    'DataSet
    Dim dsRTUser As UserDataSet
    Dim dsUser As New DataSet
    Dim lang As String = "1"
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


            '●リンク先設定
            Dim CART001URL As String = "cart001?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value

            '●セッション 更新
            Dim dsItinerary As New TriphooRR097DataSet

            Dim SessionCheck As Boolean = True
            SessionCheck = CommonWebAuthentication.SessionCheck(Me.RT_CD.Value, Me.S_CD.Value, "Itinerary", dsItinerary)

            If Not SessionCheck Or dsItinerary.PAGE_03.Rows.Count = 0 Then
                If Not IsPostBack Then
                    Response.Redirect(CART001URL, True)
                Else
                    '●アクション群
                    Dim actionEve As String = Request.Item("__EVENTTARGET")

                End If
            Else

                '●言語対応
                setlang(lang)

                dsUser = Session("user" & Me._RT_CD.Value & Me.S_CD.Value)

                '●スクリーン設定
                ScreenSet(dsItinerary)

                If Not IsPostBack Then
                    '●ページ設定
                    iniPage(dsItinerary)
                End If

            End If

        Catch ex As Exception
            '●エラー処理
            If Not (ex.Message.StartsWith("スレッドを中止") Or ex.Message.StartsWith("Thread was being aborted")) Then
                Dim ConcreteException As New src.common.ConcreteException
                ConcreteException.Exception(Request.Item("RT_CD"), Request.Item("S_CD"), "cart006", ex)
                Response.Redirect("../err/err002?RT_CD=" & Request.Item("RT_CD") & "&S_CD=" & Request.Item("S_CD"))
            End If
        End Try
    End Sub
#End Region

#Region "スクリーン設定"
    Private Sub ScreenSet(dsItinerary As TriphooRR097DataSet)

        '/* 初期設定 * /
        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/base/lang.xml")

        Dim rXml() As DataRow = dsXml.Tables("TITLE").Select("LANGUAGE_KBN='" & lang & "'")

        Dim PAGE_TITLE As String = ""

        If rXml.Length > 0 Then
            PAGE_TITLE = rXml(0)("cart006")
            PAGE_TITLE = PAGE_TITLE.Replace("@@SITE_TITLE@@", dsRTUser.M137_RT_SITE_CD.Rows(0)("SITE_TITLE"))
            Me.Title = PAGE_TITLE
        End If

        '/* 初期設定 * /
        Dim _SETTLE_KBN As String = dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN")

        Dim dsWebScrnRes As New TriphooCMSAPI.ScreenSettingDataSet

        If _SETTLE_KBN.Equals("01") Then
            dsWebScrnRes = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart006", lang)
        Else
            dsWebScrnRes = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart006_2", lang)
        End If

        If dsWebScrnRes.DETAIL_RES.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim PAGE_HEADER As String = ""
        Dim CSS As String = dsWebScrnRes.DETAIL_RES.Rows(0)("CSS")
        Dim SCRIPT As String = dsWebScrnRes.DETAIL_RES.Rows(0)("SCRIPT")

        PAGE_HEADER += dsWebScrnRes.DETAIL_RES.Rows(0)("HEADER")

        Me.PageHeader.Text = PAGE_HEADER
        Me.Pagefooter.Text = dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER")

        If Not dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE").Equals("") Then
            Me.Title = dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE")
        End If

        Me.MetaDescription = dsWebScrnRes.DETAIL_RES.Rows(0)("DESCRIPTION")
        Me.MetaKeywords = dsWebScrnRes.DETAIL_RES.Rows(0)("KEYWORD")
        Me.SCRIPTLiteral.Text = CSS & SCRIPT

    End Sub
#End Region

#Region "ページ設定"

#Region "iniPage"
    Private Sub iniPage(dsItinerary As TriphooRR097DataSet)

        Me.RES_NO.Text = dsItinerary.PAGE_03.Rows(0)("RES_NO")

        Session.Remove("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value)
        Session.Remove("dsAir" & Me.RT_CD.Value & Me.S_CD.Value)
        Session.Remove("dsHote" & Me.RT_CD.Value & Me.S_CD.Value)
        Session.Remove("dsTour" & Me.RT_CD.Value & Me.S_CD.Value)
        Session.Remove("dsOption" & Me.RT_CD.Value & Me.S_CD.Value)
        Session.Remove("MEDIA" & Me.RT_CD.Value & Me.S_CD.Value)

        'セッションデータ削除
        If Not dsUser Is Nothing Then

            'パスワードが空白のユーザーだった場合
            If dsUser.Tables("CLIENT_RES").Rows(0)("PASSWORD").Equals("") Then
                Session.Remove("user" & Me.RT_CD.Value & Me.S_CD.Value)
            End If

        End If

    End Sub
#End Region

#End Region

#Region "言語対応"
    Private Sub setlang(lang As String)

        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/cart/" & lang & "/cart006.xml")
        Dim rXml As DataRow = dsXml.Tables("LABEL").Rows(0)

        Dim MAIL_DOMAIN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MAIL_DOMAIN")

        Me.LABEL_0001.Text = rXml("LABEL_0001")
        Me.LABEL_0002.Text = rXml("LABEL_0002")
        Me.LABEL_0003.Text = rXml("LABEL_0003")
        Me.LABEL_0004.Text = rXml("LABEL_0004")
        Me.LABEL_0005.Text = rXml("LABEL_0005")
        Me.LABEL_0006.Text = rXml("LABEL_0006")
        Me.LABEL_0007.Text = rXml("LABEL_0007")
        Me.LABEL_0008.Text = Replace(rXml("LABEL_0008"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0009.Text = Replace(rXml("LABEL_0009"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0010.Text = Replace(rXml("LABEL_0010"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0011.Text = Replace(rXml("LABEL_0011"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0012.Text = Replace(rXml("LABEL_0012"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0013.Text = Replace(rXml("LABEL_0013"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0014.Text = Replace(rXml("LABEL_0014"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0015.Text = Replace(rXml("LABEL_0015"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0016.Text = Replace(rXml("LABEL_0016"), "@@MAIL_DOMAIN@@", MAIL_DOMAIN)

        'Me.ImportantMsg.InnerHtml = Me.ImportantMsg.InnerHtml.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)

    End Sub
#End Region

End Class