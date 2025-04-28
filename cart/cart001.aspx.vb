#Region "Imports"
Imports System.Data
Imports twClient
Imports twClient.TriphooB2CAPI
Imports tw.src.util
Imports TriphooConfig.Src.ApplicationContext
Imports TriphooConfig.Src.ApplicationContext2
Imports TriphooConfig.Src.ApplicationContext3
#End Region

Partial Class page_cart_cart001
    Inherits System.Web.UI.Page

#Region "クラス宣言"

    'src.util
    Dim CartUtil As CartUtil
    Dim CommonUtil As CommonUtil
    Dim WebSessionUtil As WebSessionUtil
    Dim CommonWebAuthentication As New CommonWebAuthentication
    Dim CreateWebServiceManager As New CreateWebServiceManager
    Dim ParameterUtil As New ParameterUtil

    ' NSSOL負荷性能検証 2023/02/20
    'Dim logger As NSTSTLogger

    'TriphooRRUtil.src.util
    Dim SetRRValue As New TriphooRRUtil.src.util.SetValue
    Dim SecureRRUtil As New TriphooRRUtil.src.util.SecureUtil

    'WebService
    Dim TriphooRMClient As TriphooRMWebService.Service
    Dim B2CAPIClient As TriphooB2CAPI.Service

    'DataSet
    Dim dsRMUser As DataSet
    Dim dsB2CUser As DataSet
    Dim dsRTUser As UserDataSet
    Dim dsUser As New DataSet
    Dim dsStaffUser As New DataSet

    'Str
    Dim CART002URL As String
    Dim lang As String = "1"
    Dim CURR_CD As String = "JPY"
    Dim isEstimate As Boolean = False
    Dim isBooking002 As Boolean = False

    Public LABEL_0012 As String = ""
    Public LABEL_0013 As String = ""
    Public LABEL_0014 As String = ""
    Public LABEL_0015 As String = ""
    Public LABEL_0016 As String = ""
    Public LABEL_0017 As String = ""
    Public LABEL_0018 As String = ""
    Public LABEL_0019 As String = ""
    Public LABEL_0020 As String = ""
    Public LABEL_0021 As String = ""

    Dim UNIQUE_COOKIE_VALUE As String = ""
    Dim REAL_COOKIE_VALUE As String = ""
    Dim PERSONAL_COOKIE_VALUE As String = ""
    Public MEMBER_ADD_KBN As String = ""
#End Region

#Region "初期処理"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' ロガー初期化 NSSOL負荷性能検証 2023/02/20
        'logger = New NSTSTLogger("cart001", "D:\", Request.Item("RT_CD"))

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "Page_Load()", 65, "開始", HttpContext.Current.Session.SessionID)

        Try
            Me.RT_CD.Value = SetRRValue.setNothingValueWeb(Request.Item("RT_CD"))
            Me.S_CD.Value = SetRRValue.setNothingValueWeb(Request.Item("S_CD"))
            Me.SESSION_NO.Value = SetRRValue.setNothingValueWeb(Request.Item("skey"))

            'Dim b As TriphooB2CAPI.TriphooRR097DataSet = HttpContext.Current.Session("Itinerary" & Request.Item("RT_CD") & Request.Item("S_CD"))
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

            'lang
            If Not SetRRValue.setNothingValueWeb(Request.Item("lang")).Equals("") Then
                Session("lang") = Request.Item("lang")
            End If
            If Not Session("lang") Is Nothing Then
                lang = Session("lang")
            End If

            Me.HD_LANG.Value = lang

            'CURR_CD
            If Not SetRRValue.setNothingValueWeb(Request.Item("CURR_CD")).Equals("") Then
                Session("CURR_CD") = Request.Item("CURR_CD")
            End If
            If Not Session("CURR_CD") Is Nothing Then
                CURR_CD = Session("CURR_CD")
            End If

            '●インスタンス化
            TriphooRMClient = CreateWebServiceManager.CreateTriphooRMClient(Me.RT_CD.Value, Me.S_CD.Value)
            dsRMUser = CreateWebServiceManager.CreateTriphooRMUser(Me.RT_CD.Value, Me.S_CD.Value)
            CommonUtil = New CommonUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)
            WebSessionUtil = New WebSessionUtil(Me.RT_CD.Value, Me.S_CD.Value)

            B2CAPIClient = CreateWebServiceManager.CreateTriphooB2CAPIClient(Me.RT_CD.Value, Me.S_CD.Value)
            dsB2CUser = CreateWebServiceManager.CreateTriphooB2CAPIUser(Me.RT_CD.Value, Me.S_CD.Value, Request)

            '●セッション 更新
            Dim dsItinerary As New TriphooRR097DataSet

            Dim ESTIMATE_NO As String = SetRRValue.setNothingValueWeb(Request.Item("ESTIMATE_NO"))

            If Not ESTIMATE_NO.Equals("") Then
                ESTIMATE_NO = SecureRRUtil.DecryptString(ESTIMATE_NO, SecureRRUtil.PASSCODE_KEY)

                Dim dsT001_ESTIMATE_DATA As TriphooB2CAPI.T001_ESTIMATE_DATA_DataSet = B2CAPIClient.SelectT001_ESTIMATE_DATAGateway(Me.RT_CD.Value, ESTIMATE_NO, dsB2CUser)

                If 0 < dsT001_ESTIMATE_DATA.T001_ESTIMATE_DATA.Rows.Count Then
                    Dim CLIENT_CD As String = dsT001_ESTIMATE_DATA.T001_ESTIMATE_DATA.Rows(0)("CLIENT_CD")
                    Dim ESTIMATE_XML As String = dsT001_ESTIMATE_DATA.T001_ESTIMATE_DATA.Rows(0)("ESTIMATE_XML")

                    Dim strRes As System.IO.TextReader = New System.IO.StringReader(ESTIMATE_XML)
                    dsItinerary.ReadXml(strRes)

                    If 0 < dsItinerary.PAGE_03.Rows.Count Then
                        dsItinerary.PAGE_03.Rows(0)("QUOTATION_CD") = ESTIMATE_NO
                    End If

                    If 0 < dsItinerary.PAGE_03.Rows.Count AndAlso Not dsItinerary.PAGE_03.Rows(0)("RES_NO").Equals("") Then

                        Dim dsBookDetailReq As New BookDetailReq
                        Dim rT009_RES_DATA As BookDetailReq.T009_RES_DATARow = dsBookDetailReq.T009_RES_DATA.NewT009_RES_DATARow
                        rT009_RES_DATA.RT_CD = Me.RT_CD.Value
                        rT009_RES_DATA.SITE_CD = Me.S_CD.Value
                        rT009_RES_DATA.RES_NO = dsItinerary.PAGE_03.Rows(0)("RES_NO")
                        dsBookDetailReq.T009_RES_DATA.AddT009_RES_DATARow(rT009_RES_DATA)

                        Dim ItineraryUtil As ItineraryUtil
                        ItineraryUtil = New ItineraryUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)

                        Dim dsTriphooRR097 As New TriphooRR097DataSet
                        ItineraryUtil.Detail(dsBookDetailReq, dsTriphooRR097, dsUser)

                        If 0 < dsTriphooRR097.PAGE_03.Rows.Count AndAlso Not dsTriphooRR097.PAGE_03.Rows(0)("RES_STATUS_KBN").Equals("ES") Then
                            Throw New Exception("お見積り ES以外")
                        End If

                    End If

                    For Each row In dsItinerary.RES_ORDER_DATA.Rows

                        If IsDBNull(row("SALES_UNIT")) Then
                            Continue For
                        End If

                        Dim SALES_UNIT As String = row("SALES_UNIT")
                        Dim SUP_UNIT As String = row("SUP_UNIT")
                        Dim SALES_SUB_TOTAL As String = row("SALES_SUB_TOTAL")
                        Dim SUP_SUB_TOTAL As String = row("SUP_SUB_TOTAL")

                        SALES_UNIT = SALES_UNIT.Replace("\", "").Replace(",", "")
                        SUP_UNIT = SUP_UNIT.Replace("\", "").Replace(",", "")
                        SALES_SUB_TOTAL = SALES_SUB_TOTAL.Replace("\", "").Replace(",", "")
                        SUP_SUB_TOTAL = SUP_SUB_TOTAL.Replace("\", "").Replace(",", "")

                        row("SALES_UNIT") = SetRRValue.setFare(SALES_UNIT)
                        row("SUP_UNIT") = SetRRValue.setFare(SUP_UNIT)
                        row("SALES_SUB_TOTAL") = SetRRValue.setFare(SALES_SUB_TOTAL)
                        row("SUP_SUB_TOTAL") = SetRRValue.setFare(SUP_SUB_TOTAL)
                    Next

                    For Each row In dsItinerary.PAGE_03.Rows
                        Dim sCity() As String = row("RECORD_CITY_CD").Split(",")
                        Dim CITY_NM As String = ""
                        For Each s As String In sCity

                            Dim __CITY_NM As String = ""

                            Dim dsM004_CITY As New DataSet
                            dsM004_CITY.Merge(TriphooRMClient.SelectM004_CITYGateway(s, dsRMUser))
                            __CITY_NM = dsM004_CITY.Tables(0).Rows(0)("CITY_NM_JP")

                            If __CITY_NM.Equals("") Then
                                Continue For
                            End If

                            CITY_NM += __CITY_NM & "/"

                        Next
                        CITY_NM = CITY_NM.TrimEnd("/")
                        dsItinerary.PAGE_03.Rows(0)("RECORD_CITY_NM") = CITY_NM
                    Next

                    Dim BB_NUM As Integer = 0
                    Dim LL_NUM As Integer = 0
                    Dim DD_NUM As Integer = 0
                    For Each row In dsItinerary.PAGE_08.Rows
                        If row("BB_FLG") Then
                            BB_NUM += 1
                        End If

                        If row("LL_FLG") Then
                            LL_NUM += 1
                        End If

                        If row("DD_FLG") Then
                            DD_NUM += 1
                        End If
                    Next

                    For Each row In dsItinerary.PAGE_05.Rows
                        row("CT_FLG") = SetRRValue.setBoolean(row("CT_FLG"))
                        row("CARRIER_MASK_FLG") = SetRRValue.setBoolean(row("CARRIER_MASK_FLG"))
                        row("BFM_FLG") = SetRRValue.setBoolean(row("BFM_FLG"))
                        row("XML_DATA") = SetRRValue.setDBNullValue(row("XML_DATA"))
                    Next

                    For Each row In dsItinerary.PAGE_07.Rows
                        row("DEP_DATE_NM") = SetRRValue.setDBNullValue(row("DEP_TIME"))
                        row("ARR_DATE_NM") = SetRRValue.setDBNullValue(row("ARR_TIME"))
                        row("DEP_TIME_NM") = CDate(row("DEP_TIME")).ToString("HH:mm")
                        row("ARR_TIME_NM") = CDate(row("ARR_TIME")).ToString("HH:mm")
                    Next

                    For Each row In dsItinerary.RES_HOTEL.Rows
                        Dim CITY_CD As String = SetRRValue.setDBNullValue(row("CITY_CD"))

                        If Not CITY_CD.Equals("") Then
                            Dim dsM004_CITY As New DataSet
                            dsM004_CITY.Merge(TriphooRMClient.SelectM004_CITYGateway(CITY_CD, dsRMUser))
                            row("CITY_NM") = dsM004_CITY.Tables(0).Rows(0)("CITY_NM_JP")
                        End If
                    Next

                    For Each row In dsItinerary.PAGE_08.Rows
                        row("TRANSFER_ARRANGE_FLG") = False
                        row("SIGHTSEEING_ARRANGE_FLG") = False
                    Next

                    For Each row In dsItinerary.PAGE_20.Rows
                        row("INQUIRY_CD") = SetRRValue.setDBNullValue(row("GOODS_CD"))
                        row("GOODS_CLASS") = SetRRValue.setDBNullValue(row("GOODS_CLASS"))
                        row("CARRIER_NM") = SetRRValue.setDBNullValue(row("CARRIER_NM"))
                        row("STAY_HOTEL_NM") = SetRRValue.setDBNullValue(row("STAY_HOTEL_NM"))
                        row("IMAGE_01") = BB_NUM
                        row("IMAGE_02") = LL_NUM
                        row("IMAGE_03") = DD_NUM
                        row("IMAGE_04") = ""
                        row("OIL_TAX_NM") = ""

                        If row("CARRIER_NM").Equals("") Then
                            If 0 < dsItinerary.PAGE_05.Rows.Count Then
                                If Not dsItinerary.PAGE_05.Rows(0)("AIR_COMPANY_CD").Equals("") Then
                                    CommonUtil.TourCarrName(Me.RT_CD.Value, Me.S_CD.Value, dsItinerary.PAGE_05.Rows(0)("AIR_COMPANY_CD"), row("CARRIER_NM"))
                                End If
                            End If
                        End If
                        If row("STAY_HOTEL_NM").Equals("") Then
                            Dim STAY_HOTEL_NM As String = ""
                            For Each rhotel In dsItinerary.RES_HOTEL.Rows
                                STAY_HOTEL_NM += rhotel("GOODS_NM") & ","
                            Next
                            row("STAY_HOTEL_NM") = STAY_HOTEL_NM.TrimEnd(",")
                        End If
                    Next

                    For Each row In dsItinerary.PAGE_04.Rows
                        Select Case row("AGE_KBN")
                            Case "ADT"
                                row("AGE_KBN_NM") = LABEL_0017
                            Case "CHD"
                                row("AGE_KBN_NM") = LABEL_0018
                            Case "INF"
                                row("AGE_KBN_NM") = LABEL_0019
                        End Select
                    Next

                    ' ●予約備考(履歴) ： PAGE_09 → T015_RES_REMARKS
                    dsItinerary.PAGE_09.Clear()
                    Dim rPAGE_09 As TriphooRR097DataSet.PAGE_09Row = dsItinerary.PAGE_09.NewPAGE_09Row
                    ' *** 新規追加時
                    rPAGE_09.REMARKS_NO = 1
                    rPAGE_09.MAIL_KBN = ""
                    rPAGE_09.REMARKS = LABEL_0020
                    rPAGE_09.EDIT_TIME = Now
                    rPAGE_09.EDIT_EMP_CD = "WEB"
                    dsItinerary.PAGE_09.AddPAGE_09Row(rPAGE_09)

                    dsItinerary.M019_CLIENT.Clear()
                    dsItinerary.M021_CLIENT_ADDRESS.Clear()
                    dsItinerary.M023_CLIENT_TEL.Clear()

                    'Session.Add("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value, dsItinerary)
                    WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)
                    isEstimate = True

                End If
            Else

                'Dim SessionCheck As Boolean = True
                'SessionCheck = CommonWebAuthentication.SessionCheck(Me.RT_CD.Value, Me.S_CD.Value, "Itinerary", dsItinerary)

                'If Not SessionCheck Then
                '    Throw New Exception("セッション(Itinerary)エラー : RT_CD=" & Me._RT_CD.Value & " , SITE_CD=" & Me.S_CD.Value)
                'End If

                dsItinerary = WebSessionUtil.GetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsB2CUser)

                If dsItinerary Is Nothing Then
                    Throw New Exception("セッション(Itinerary)エラー : RT_CD=" & Me._RT_CD.Value & " , SITE_CD=" & Me.S_CD.Value)
                End If

            End If

            '●言語対応
            setlang(lang)

            dsUser = Session("user" & Me.RT_CD.Value & Me.S_CD.Value)

            ' 22.5.22 不明なためコメントアウト Mitsuta
            'Dim _twtk As String = SetRRValue.setNothingValueWeb(Request.Item("_twtk"))

            'If Not _twtk.Equals("") Then
            '    dsStaffUser = Session("StaffUser" & Me.RT_CD.Value & Me.S_CD.Value & _twtk)
            'End If

            MEMBER_ADD_KBN = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")

            '●自動ログイン
            AutoLogIn()

            '●スクリーン設定
            ScreenSet(dsItinerary)

            '●Url設定
            If Not dsItinerary Is Nothing AndAlso dsItinerary.PAGE_03.Rows.Count > 0 Then
                Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")

                If PACKAGE_FLG Then
                    ' 接続先切替対応
                    If ServiceVendor.IsNSSOL Then
                        isBooking002 = True
                    Else
                        Select Case Me.RT_CD.Value
                            Case "ASX", "ADV", "A0509", "TSJ", "RT11", "RT01", "A0057", "A0508", "TWC", "WTB"
                                isBooking002 = True
                        End Select
                    End If
                End If
            End If

            If isBooking002 Then
                CART002URL = "./../booking/booking002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
            Else
                CART002URL = "./cart002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
            End If

            'Select Case Me.RT_CD.Value
            '    Case "ASX", "ADV", "A0509", "TSJ", "RT11", "RT01"
            '        CART002URL = "./../booking/booking002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
            '    Case Else
            '        CART002URL = "./cart002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
            'End Select

            Dim SITE_DOMAIN_HTTPS As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("SITE_DOMAIN_HTTPS")
            If SITE_DOMAIN_HTTPS.Equals("") Then

                If isBooking002 Then
                    CART002URL = "https://" & Request.Url.Host & "/page/booking/booking002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
                Else
                    CART002URL = "https://" & Request.Url.Host & ParameterUtil.CartRootPath & "/page/cart/cart002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
                End If
                'Select Case Me.RT_CD.Value
                '    Case "ASX", "ADV", "A0509", "TSJ", "RT11" ', "RT01"
                '        CART002URL = "https://" & Request.Url.Host & "/page/booking/booking002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
                '    Case Else
                '        CART002URL = "https://" & Request.Url.Host & ParameterUtil.CartRootPath & "/page/cart/cart002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
                'End Select

            Else
                If 0 < dsItinerary.PAGE_20.Rows.Count Then
                    Dim SEASONALITY As String = dsItinerary.PAGE_20.Rows(0)("SEASONALITY")
                    Dim SEASONALITY_KBN As String = dsItinerary.PAGE_20.Rows(0)("SEASONALITY_KBN")
                    Dim GOODS_CD As String = dsItinerary.PAGE_20.Rows(0)("GOODS_CD")
                    Dim INQUIRY_REPORT_KBN As String = SetRRValue.setNothingValue(dsItinerary.PAGE_20.Rows(0)("INQUIRY_REPORT_KBN"))
                    If INQUIRY_REPORT_KBN.Equals("1") Then
                        CART002URL = "http://" & Request.Url.Host & ParameterUtil.CartRootPath & "/page/estimate/estimate001?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value &
                                     "&GOODS_CD=" & SEASONALITY & SEASONALITY_KBN & GOODS_CD & "&mt=pkg"
                        Me.BookLinkButton.Text = LABEL_0021
                    End If
                End If
            End If

            If Not ESTIMATE_NO.Equals("") Then
                CART002URL += "&ESTIMATE_NO=" & HttpUtility.UrlEncode(Request.Item("ESTIMATE_NO"))
            End If

            If Not IsPostBack Then
                '●ページ設定
                iniPage(dsItinerary, dsUser)
            Else
                '●アクション群
                Dim actionEve As String = Request.Item("__EVENTTARGET")

                If actionEve.Contains("BookLinkButton") Then
                    BookLinkButton_Click(dsItinerary, dsUser)            '予約する
                ElseIf actionEve.Contains("TO_LOGINLinkButton") Then
                    TO_LOGINLinkButton_Click(dsItinerary, dsUser)        'ログイン
                ElseIf actionEve.Contains("CLIENT_ADDLinkButton") Then

                    CLIENT_ADDLinkButton_Click(dsItinerary, dsUser)      '新規会員登録 
                ElseIf actionEve.Contains("NOT_LOGINLinkButton") Then
                    NOT_LOGINLinkButton_Click(dsItinerary, dsUser) '      会員登録せずに予約する
                ElseIf actionEve.Contains("FORGET_PASSWORDLinkButton") Then
                    FORGET_PASSWORDLinkButton_Click(dsItinerary, dsUser) 'パスワードを忘れた方はこちら
                ElseIf actionEve.Contains("HotelLinkButton") Then
                ElseIf actionEve.Contains("EstimateSubmitButton") Then
                    EstimateLinkButton_Click(dsItinerary, dsUser)
                ElseIf actionEve.Contains("InquiryLinkButton") Then
                    Response.Redirect("/page/inquiry/inquiry001?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value, True)
                ElseIf actionEve.Contains("HotelRoomSelectButton") Then

                    Dim ADT_NUM As Integer = dsItinerary.PAGE_03.Rows(0)("NUM_ADULT")
                    Dim CHD_NUM As Integer = dsItinerary.PAGE_03.Rows(0)("NUM_CHILD")
                    Dim INF_NUM As Integer = dsItinerary.PAGE_03.Rows(0)("NUM_INFANT")

                    Dim ROOM_NUM As Integer = CType(Me.hotelSearchModal.FindControl("ROOM"), DropDownList).SelectedValue

                    Dim ADT_NUM1 As Integer = CType(Me.hotelSearchModal.FindControl("ADT_NUM1"), DropDownList).SelectedValue
                    Dim ADT_NUM2 As Integer = CType(Me.hotelSearchModal.FindControl("ADT_NUM2"), DropDownList).SelectedValue
                    Dim ADT_NUM3 As Integer = CType(Me.hotelSearchModal.FindControl("ADT_NUM3"), DropDownList).SelectedValue
                    Dim ADT_NUM4 As Integer = CType(Me.hotelSearchModal.FindControl("ADT_NUM4"), DropDownList).SelectedValue
                    Dim ADT_NUM5 As Integer = CType(Me.hotelSearchModal.FindControl("ADT_NUM5"), DropDownList).SelectedValue

                    Dim CHD_NUM1 As Integer = CType(Me.hotelSearchModal.FindControl("CHD_NUM1"), DropDownList).SelectedValue
                    Dim CHD_NUM2 As Integer = CType(Me.hotelSearchModal.FindControl("CHD_NUM2"), DropDownList).SelectedValue
                    Dim CHD_NUM3 As Integer = CType(Me.hotelSearchModal.FindControl("CHD_NUM3"), DropDownList).SelectedValue
                    Dim CHD_NUM4 As Integer = CType(Me.hotelSearchModal.FindControl("CHD_NUM4"), DropDownList).SelectedValue
                    Dim CHD_NUM5 As Integer = CType(Me.hotelSearchModal.FindControl("CHD_NUM5"), DropDownList).SelectedValue

                    Dim AGE1_1 As Integer = CType(Me.hotelSearchModal.FindControl("AGE1_1"), DropDownList).SelectedValue
                    Dim AGE1_2 As Integer = CType(Me.hotelSearchModal.FindControl("AGE1_2"), DropDownList).SelectedValue
                    Dim AGE2_1 As Integer = CType(Me.hotelSearchModal.FindControl("AGE2_1"), DropDownList).SelectedValue
                    Dim AGE2_2 As Integer = CType(Me.hotelSearchModal.FindControl("AGE2_2"), DropDownList).SelectedValue
                    Dim AGE3_1 As Integer = CType(Me.hotelSearchModal.FindControl("AGE3_1"), DropDownList).SelectedValue
                    Dim AGE3_2 As Integer = CType(Me.hotelSearchModal.FindControl("AGE3_2"), DropDownList).SelectedValue
                    Dim AGE4_1 As Integer = CType(Me.hotelSearchModal.FindControl("AGE4_1"), DropDownList).SelectedValue
                    Dim AGE4_2 As Integer = CType(Me.hotelSearchModal.FindControl("AGE4_2"), DropDownList).SelectedValue
                    Dim AGE5_1 As Integer = CType(Me.hotelSearchModal.FindControl("AGE5_1"), DropDownList).SelectedValue
                    Dim AGE5_2 As Integer = CType(Me.hotelSearchModal.FindControl("AGE5_2"), DropDownList).SelectedValue

                    Select Case ROOM_NUM
                        Case 1
                            ADT_NUM2 = 0
                            ADT_NUM3 = 0
                            ADT_NUM4 = 0
                            ADT_NUM5 = 0
                            CHD_NUM2 = 0
                            CHD_NUM3 = 0
                            CHD_NUM4 = 0
                            CHD_NUM5 = 0
                        Case 2
                            ADT_NUM3 = 0
                            ADT_NUM4 = 0
                            ADT_NUM5 = 0
                            CHD_NUM3 = 0
                            CHD_NUM4 = 0
                            CHD_NUM5 = 0
                        Case 3
                            ADT_NUM4 = 0
                            ADT_NUM5 = 0
                            CHD_NUM4 = 0
                            CHD_NUM5 = 0
                        Case 4
                            ADT_NUM5 = 0
                            CHD_NUM5 = 0
                    End Select

                    Dim TOTAL_ADT_NUM As Integer = ADT_NUM1 + ADT_NUM2 + ADT_NUM3 + ADT_NUM4 + ADT_NUM5
                    Dim TOTAL_CHD_NUM As Integer = CHD_NUM1 + CHD_NUM2 + CHD_NUM3 + CHD_NUM4 + CHD_NUM5

                    Dim ErrMsg As String = ""

                    If TOTAL_ADT_NUM = ADT_NUM Then
                    Else
                        ErrMsg += LABEL_0012 & "\n"
                    End If

                    If TOTAL_CHD_NUM = CHD_NUM + INF_NUM Then
                    Else
                        ErrMsg += LABEL_0013 & "\n"
                    End If

                    If ErrMsg.Equals("") Then
                        Dim url As String = CommonUtil.ConvertHotelDetailUrl1(Nothing, dsItinerary, Me.RT_CD.Value, Me.S_CD.Value, "", False)
                        url += "&ROOM=" & ROOM_NUM
                        url += "&ADT_NUM1=" & ADT_NUM1
                        url += "&ADT_NUM2=" & ADT_NUM2
                        url += "&ADT_NUM3=" & ADT_NUM3
                        url += "&ADT_NUM4=" & ADT_NUM4
                        url += "&ADT_NUM5=" & ADT_NUM5
                        url += "&CHD_NUM1=" & CHD_NUM1
                        url += "&CHD_NUM2=" & CHD_NUM2
                        url += "&CHD_NUM3=" & CHD_NUM3
                        url += "&CHD_NUM4=" & CHD_NUM4
                        url += "&CHD_NUM5=" & CHD_NUM5
                        url += "&AGE1_1=" & AGE1_1
                        url += "&AGE1_2=" & AGE1_2
                        url += "&AGE2_1=" & AGE2_1
                        url += "&AGE2_2=" & AGE2_2
                        url += "&AGE3_1=" & AGE3_1
                        url += "&AGE3_2=" & AGE3_2
                        url += "&AGE4_1=" & AGE4_1
                        url += "&AGE4_2=" & AGE4_2
                        url += "&AGE5_1=" & AGE5_1
                        url += "&AGE5_2=" & AGE5_2
                        url = "../hotel/hotelListLoading" & url

                        Response.Redirect(url, True)

                    Else
                        ScriptManager.RegisterStartupScript(
                        Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)
                    End If
                End If

            End If

        Catch ex As Exception
            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart001", "Page_Load()", 497, "終了（エラーorリダイレクト）", HttpContext.Current.Session.SessionID)

            ' ログファイルへの書き込み NSSOL負荷性能検証 2023/02/09
            'logger.WriteLogToFile()

            '●エラー処理
            If Not (ex.Message.StartsWith("スレッドを中止") Or ex.Message.StartsWith("Thread was being aborted")) Then
                Dim ConcreteException As New src.common.ConcreteException
                ConcreteException.Exception(Request.Item("RT_CD"), Request.Item("S_CD"), "cart001", ex)
                Response.Redirect("../err/err002?RT_CD=" & Request.Item("RT_CD") & "&S_CD=" & Request.Item("S_CD"))
            End If
        End Try

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "Page_Load()", 511, "終了", HttpContext.Current.Session.SessionID)

        ' ログファイルへの書き込み NSSOL負荷性能検証 2023/02/09
        'logger.WriteLogToFile()

    End Sub
#End Region

#Region "スクリーン設定"
    Private Sub ScreenSet(dsItinerary As TriphooRR097DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "ScreenSet()", 523, "開始", HttpContext.Current.Session.SessionID)

        '/* 初期設定 * /
        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/base/lang.xml")

        Dim rXml() As DataRow = dsXml.Tables("TITLE").Select("LANGUAGE_KBN='" & lang & "'")

        Dim PAGE_TITLE As String = ""

        If rXml.Length > 0 Then
            PAGE_TITLE = rXml(0)("cart001")
            PAGE_TITLE = PAGE_TITLE.Replace("@@SITE_TITLE@@", dsRTUser.M137_RT_SITE_CD.Rows(0)("SITE_TITLE"))
            Me.Title = PAGE_TITLE
        End If
        '/* 初期設定 * /

        Dim dsWebScrnRes As TriphooCMSAPI.ScreenSettingDataSet = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart001", lang)

        If dsWebScrnRes.DETAIL_RES.Rows.Count = 0 Then
            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart001", "ScreenSet()", 545, "終了", HttpContext.Current.Session.SessionID)

            Exit Sub
        End If


        If Not dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE").Equals("") Then
            Me.Title = dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE")
        End If

        Dim CmsUtil As CmsUtil = New CmsUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)

        Me.MetaDescription = dsWebScrnRes.DETAIL_RES.Rows(0)("DESCRIPTION")
        Me.MetaKeywords = dsWebScrnRes.DETAIL_RES.Rows(0)("KEYWORD")
        Dim CSS As String = dsWebScrnRes.DETAIL_RES.Rows(0)("CSS")
        Dim SCRIPT As String = dsWebScrnRes.DETAIL_RES.Rows(0)("SCRIPT")
        Dim PAGE_HEADER As String = setHeaderFooter(dsWebScrnRes.DETAIL_RES.Rows(0)("HEADER"), dsItinerary)
        Dim META As String = CmsUtil.TourSearch(SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "META"))
        Dim BODY_TOP_PC As String = CmsUtil.TourSearch(SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_TOP_PC"))
        Dim BODY_TOP_SP As String = CmsUtil.TourSearch(SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_TOP_SP"))
        Dim BODY_BOTTOM_PC As String = CmsUtil.TourSearch(SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_BOTTOM_PC"))
        Dim BODY_BOTTOM_SP As String = CmsUtil.TourSearch(SetRRValue.SetRowValue(dsWebScrnRes.DETAIL_RES.Rows(0), "BODY_BOTTOM_SP"))

        CmsUtil.isPcOrSpAndReservationChange(BODY_TOP_PC, BODY_TOP_SP, BODY_BOTTOM_PC, BODY_BOTTOM_SP, Request, dsItinerary)

        Me.PageHeader.Text = PAGE_HEADER
        'Me.PageSide.Text = setHeaderFooter(dsWebScrnRes.DETAIL_RES.Rows(0)("SIDE"), dsItinerary)
        Me.Pagefooter.Text = setHeaderFooter(dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER"), dsItinerary)
        'Me.SCRIPTLiteral.Text = CSS & SCRIPT

        Me.PAGE_CSS.Text = CSS
        Me.PAGE_SCRIPT.Text = SCRIPT
        Me.PAGE_META.Text = META

        'Me.PAGE_CSS.Text = "<style>.PAGE_CSS{}</style>"
        'Me.PAGE_SCRIPT.Text = "<style>.PAGE_SCRIPT{}</style>"
        'Me.PAGE_META.Text = "<style>.PAGE_META{}</style>"

        Me.BODY_BOTTOM_PC.Text = BODY_BOTTOM_PC
        Me.BODY_BOTTOM_SP.Text = BODY_BOTTOM_SP

        'Dim BODY_TOP_PC As String = "BODY_TOP_PC"
        'Dim BODY_TOP_SP As String = "BODY_TOP_SP"
        'Dim BODY_BOTTOM_PC As String = "BODY_BOTTOM_PC"
        'Dim BODY_BOTTOM_SP As String = "BODY_BOTTOM_SP"

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

        ' /* ログインコメント */
        dsWebScrnRes = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "login001", lang)
        If dsWebScrnRes.DETAIL_RES.Rows.Count > 0 Then
            Dim _FOOTER As String = dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER")
            If Not _FOOTER.Equals("") Then
                login001Literal.Text = _FOOTER
            End If
        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "ScreenSet()", 577, "終了", HttpContext.Current.Session.SessionID)

        ' パンくず表示／非表示設定
        Select Case Me.RT_CD.Value
            Case "ASX"
                Me.BREADCRUMBPanel.Visible = False
        End Select

    End Sub
#End Region

#Region "言語対応"
    Private Sub setlang(lang As String)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "setlang()", 586, "開始", HttpContext.Current.Session.SessionID)

        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/cart/" & lang & "/cart001.xml")
        Dim rXml As DataRow = dsXml.Tables("LABEL").Rows(0)

        Me.LABEL_0001.Text = rXml("LABEL_0001")
        Me.LABEL_1002.Text = rXml("LABEL_0002")
        Me.LABEL_0003.Text = rXml("LABEL_0003")
        Me.LABEL_1004.Text = rXml("LABEL_0004")
        Me.BookLinkButton.Text = rXml("BookLinkButton")
        Me.LABEL_0005.Text = rXml("LABEL_0005")
        Me.LABEL_0006.Text = rXml("LABEL_0006")
        Me.LABEL_0007.Text = rXml("LABEL_0007")
        Me.LABEL_0008.Text = rXml("LABEL_0008")
        'Me.E_MAIL.Attributes("placeholder") = rXml("E_MAIL_PLACEHOLDER")
        Me.LABEL_0009.Text = rXml("LABEL_0009")
        Me.LABEL_0010.Text = rXml("LABEL_0010")
        Me.USER_CASH.Text = rXml("USER_CASH")
        Me.TO_LOGINLinkButton.Text = rXml("TO_LOGINLinkButton")
        Me.CLIENT_ADDLinkButton.Text = rXml("CLIENT_ADDLinkButton")
        Me.NOT_LOGINLinkButton.Text = rXml("NOT_LOGINLinkButton")
        Me.FORGET_PASSWORDLinkButton.Text = rXml("FORGET_PASSWORDLinkButton")
        Me.LABEL_0011.Text = rXml("LABEL_0011")
        Me.LABEL_0012 = rXml("LABEL_0012_COM")
        Me.LABEL_0013 = rXml("LABEL_0013_COM")
        Me.LABEL_0014 = rXml("LABEL_0014_COM")
        Me.LABEL_0015 = rXml("LABEL_0015_COM")
        Me.LABEL_0016 = rXml("LABEL_0016_COM")
        Me.LABEL_0017 = rXml("LABEL_0017_COM")
        Me.LABEL_0018 = rXml("LABEL_0018_COM")
        Me.LABEL_0019 = rXml("LABEL_0019_COM")
        Me.LABEL_0020 = rXml("LABEL_0020_COM")
        Me.LABEL_0021 = rXml("LABEL_0021_COM")
        Me.LABEL_0023.Text = rXml("LABEL_0023_COM")
        Me.LABEL_0024.Text = rXml("LABEL_0024_COM")

        'WOWOW独自対応
        If Me.RT_CD.Value.Equals("WOW") Then
            Me.LABEL_0008.Text = "WEB会員ID"
            Me.LABEL_0015 = "WEB会員IDを入力してください。"
        End If


        'Select Case lang

        '    Case "2" ' 英語
        '        Me.label001.Text = "RES Cart"
        '        Me.label002.Text = "Detail"
        '        Me.label004.Text = "最終確認画面"
        '        Me.label005.Text = "予約完了"
        '        Me.loginLabel.Text = "Login"
        '        Me.emailLabel.Text = "E-mail Address"
        '        Me.passwordLabel.Text = "Password"
        '        Me.E_MAIL.Attributes("placeholder") = "Sample：sample@sample.com"
        '        Me.USER_CASH.Text = "Keep sign in"
        '        Me.TO_LOGINLinkButton.Text = "Login"
        '        Me.CLIENT_ADDLinkButton.Text = "Member Register"
        '        Me.NOT_LOGINLinkButton.Text = "Reserve Without Register"
        '        Me.FORGET_PASSWORDLinkButton.Text = "Forgot Password"

        '        Me.BookLinkButton.Text = "ご予約申込"
        '        Me.HotelLabel.Text = "ホテルを同時購入"

        '    Case "3" ' 中国語
        '        Me.label001.Text = "予約カート"
        '        Me.label002.Text = "旅客情報入力"
        '        Me.label004.Text = "最終確認画面"
        '        Me.label005.Text = "予約完了"
        '        Me.loginLabel.Text = "ログイン"
        '        Me.emailLabel.Text = "Eメールアドレス"
        '        Me.passwordLabel.Text = "パスワード"
        '        Me.E_MAIL.Attributes("placeholder") = "例：sample@sample.com"
        '        Me.USER_CASH.Text = "サインインしたままにする"
        '        Me.TO_LOGINLinkButton.Text = "ログイン"
        '        Me.CLIENT_ADDLinkButton.Text = "新規会員登録"
        '        Me.NOT_LOGINLinkButton.Text = "会員登録せずに予約する"
        '        Me.FORGET_PASSWORDLinkButton.Text = "パスワードを忘れた方はこちら"

        '        Me.BookLinkButton.Text = "ご予約申込"
        '        Me.HotelLabel.Text = "ホテルを同時購入"

        '    Case "4" ' ベトナム語
        '        Me.label001.Text = "Giỏ hàng"
        '        Me.label002.Text = "Chi tiết"
        '        Me.loginLabel.Text = "Login"
        '        Me.emailLabel.Text = "E-mail Address"
        '        Me.passwordLabel.Text = "Password"
        '        Me.E_MAIL.Attributes("placeholder") = "例：sample@sample.com"
        '        Me.USER_CASH.Text = "サインインしたままにする"
        '        Me.TO_LOGINLinkButton.Text = "ログイン"
        '        Me.CLIENT_ADDLinkButton.Text = "新規会員登録"
        '        Me.NOT_LOGINLinkButton.Text = "会員登録せずに予約する"
        '        Me.FORGET_PASSWORDLinkButton.Text = "パスワードを忘れた方はこちら"

        '        Me.BookLinkButton.Text = "ご予約申込"
        '        Me.HotelLabel.Text = "ホテルを同時購入"

        '    Case Else '日本語、その他例外

        '        Me.label001.Text = "予約カート"
        '        Me.label002.Text = "旅客情報入力"
        '        Me.label004.Text = "最終確認画面"
        '        Me.label005.Text = "予約完了"
        '        Me.loginLabel.Text = "ログイン"
        '        Me.emailLabel.Text = "Eメールアドレス"
        '        Me.passwordLabel.Text = "パスワード"
        '        Me.E_MAIL.Attributes("placeholder") = "例：sample@sample.com"
        '        Me.USER_CASH.Text = "サインインしたままにする"
        '        Me.TO_LOGINLinkButton.Text = "ログイン"
        '        Me.CLIENT_ADDLinkButton.Text = "新規会員登録"
        '        Me.NOT_LOGINLinkButton.Text = "会員登録せずに予約する"
        '        Me.FORGET_PASSWORDLinkButton.Text = "パスワードを忘れた方はこちら"

        '        Me.BookLinkButton.Text = "ご予約申込"
        '        Me.HotelLabel.Text = "ホテルを同時購入"

        'End Select

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "setlang()", 707, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#Region "ページ設定"

#Region "iniPage"
    Private Sub iniPage(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "iniPage()", 718, "開始", HttpContext.Current.Session.SessionID)

        'RT・サイトコード設定
        '顧客登録区分
        ':通常(両方使用できる)
        '1:使用しない(ログインしない流れ)
        '2:必ず会員登録(必ずログインさせる流れ)
        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")
        Dim OFFLINE_NOTICE_FLG As Boolean = dsRTUser.M137_RT_SITE_CD.Rows(0)("OFFLINE_NOTICE_FLG")

        If dsItinerary Is Nothing Then
            Me.LogInPanel.Visible = False
            Me.PickUpHotelPanel.Visible = False
            'Me.PickUpOptionPanel.Visible = False
            Me.BOOKPanel.Visible = False

            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart001", "iniPage()", 735, "終了", HttpContext.Current.Session.SessionID)

            Exit Sub
        Else

            '全ての商材テーブルにレコードがなかった場合
            If dsItinerary.PAGE_05.Rows.Count = 0 And dsItinerary.RES_HOTEL.Rows.Count = 0 And
               dsItinerary.RES_OPTION.Rows.Count = 0 And dsItinerary.PAGE_20.Rows.Count = 0 Then
                Me.LogInPanel.Visible = False
                Me.PickUpHotelPanel.Visible = False
                'Me.PickUpOptionPanel.Visible = False
                Me.BOOKPanel.Visible = False

                ' ログ生成 NSSOL負荷性能検証 2023/02/09
                'logger.CreateLog("page_cart_cart001", "iniPage()", 749, "終了", HttpContext.Current.Session.SessionID)

                Exit Sub
            Else
                '航空券
                If dsItinerary.PAGE_05.Rows.Count > 0 Then
                    '往復以外
                    Dim rPAGE_07() As DataRow = dsItinerary.PAGE_07.Select("GOING_RETURN_KBN='02'")
                    If rPAGE_07.Length = 0 Then
                        Me.PickUpHotelPanel.Visible = False
                    End If
                    'Me.PickUpOptionPanel.Visible = False
                End If

                'ホテル
                If dsItinerary.RES_HOTEL.Rows.Count > 0 Then
                    Me.PickUpHotelPanel.Visible = False
                    'Me.PickUpOptionPanel.Visible = False
                End If

                'オプション
                If dsItinerary.RES_OPTION.Rows.Count > 0 Then
                    Me.PickUpHotelPanel.Visible = False
                End If

                'ツアー
                If dsItinerary.PAGE_20.Rows.Count > 0 Then
                    Me.PickUpHotelPanel.Visible = False
                    'Me.PickUpOptionPanel.Visible = False
                End If

            End If

        End If

        'RT設定
        'ホテル販売フラグ
        If dsRTUser.M137_RT_SITE_CD.Rows(0)("HOTEL_FLG") Then
        Else
            Me.PickUpHotelPanel.Visible = False
        End If

        'オプション販売フラグ
        If dsRTUser.M137_RT_SITE_CD.Rows(0)("OPTION_FLG") Then
        Else
            'Me.PickUpOptionPanel.Visible = False
        End If

        '●ホテルを追加購入
        If Me.PickUpHotelPanel.Visible Then
            Dim rPAGE_07() As DataRow = dsItinerary.PAGE_07.Select("GOING_RETURN_KBN='01'")
            Dim CITY_CD As String = rPAGE_07(rPAGE_07.Length - 1)("ARR_CD")

            Select Case CITY_CD
                Case "NRT", "HND"
                    CITY_CD = "TYO"
                Case "KIX", "ITM"
                    CITY_CD = "OSA"
            End Select

            Dim dsM085_DOME_CITY As TriphooRMWebService.M085_DOME_CITY_DataSet =
            TriphooRMClient.SelectM085_DOME_CITY_Gateway("", "", CITY_CD, dsRMUser)

            If dsM085_DOME_CITY.M085_DOME_CITY.Rows.Count = 0 Then
                Me.HotelLinkButton.Visible = True
            Else
                Me.HotelLinkButton.Visible = False
            End If
        End If


        'ログイン・メッセージ
        Dim dsRegulation As DataSet = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "mypage002", lang)

        If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
            Me.RT_MYPAGE_MESSAGELabel.Text = CStr(dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
            Me.RT_MYPAGE_MESSAGEPanel.Visible = True
        End If


        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")
        Dim RES_METHOD_KBN As String = dsItinerary.PAGE_03.Rows(0)("RES_METHOD_KBN")
        Dim OVERSEAS_DOMESTIC_KBN As String = dsItinerary.PAGE_03.Rows(0)("OVERSEAS_DOMESTIC_KBN")

        If dsUser Is Nothing Then
            Me.LogInPanel.Visible = True
            Me.BOOKPanel.Visible = False
        Else
            Me.LogInPanel.Visible = False
            Me.BOOKPanel.Visible = True
        End If

        Select Case MEMBER_ADD_KBN
            Case ""
                'do nothing
            Case "1", "5"
                Me.LogInPanel.Visible = False
                Me.BOOKPanel.Visible = True
            Case "2"
                Me.NOT_LOGINLinkButton.Visible = False
        End Select

        Select Case Me.RT_CD.Value
            Case "WOW"
                Me.CLIENT_ADDLinkButton.Visible = False
                Me.FORGET_PASSWORDLinkButton.Visible = False
                Me.CLIENT_ADD_WOWLinkButton.Visible = True
                Me.FORGET_PASSWORD_WOWLinkButton.Visible = True
                Me.USER_CASH.Visible = False
            Case "ASX"
                Me.FORGET_PASSWORDLinkButton.Visible = False
                Me.FORGET_PASSWORD_ASXLinkButton.Visible = True
        End Select


        If isEstimate Then
            If dsUser Is Nothing Then
                Me.LogInPanel.Visible = True
                Me.BOOKPanel.Visible = False
            End If

            Me.NOT_LOGINLinkButton.Visible = False
            Me.PickUpHotelPanel.Visible = False
            'Me.PickUpOptionPanel.Visible = False

        End If

        If PACKAGE_FLG Then
            'オフライン通知メッセージ
            If RES_METHOD_KBN.Equals("02") Then
                Dim TOUR_GOODS_CLASS As String = dsItinerary.PAGE_20.Rows(0)("GOODS_CLASS")
                Select Case TOUR_GOODS_CLASS
                    Case "01", "05", "11", "13", "14", "21", "22", "23", "24"
                        Dim dsnotice As DataSet = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "notice009", lang)
                        If 0 < dsnotice.Tables("DETAIL_RES").Rows.Count Then
                            Me.OfflineNoticeMessage.Text = CStr(dsnotice.Tables("DETAIL_RES").Rows(0)("CONTENTS")).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
                            Me.OfflineNoticePanel.Visible = True
                        End If
                End Select
            End If
        End If

        Dim ESTIMATE_GOODS_NM As String = ""

        ' AIR, HOTEL, OPTIONAL, TOUR
        If TICKET_FLG Then

            If dsItinerary.PAGE_05.Rows.Count > 0 Then
                ESTIMATE_GOODS_NM = dsItinerary.PAGE_05.Rows(0)("GOODS_NM")
            End If

        ElseIf HOTEL_FLG Then

            If dsItinerary.RES_HOTEL.Rows.Count > 0 Then
                ESTIMATE_GOODS_NM = dsItinerary.RES_HOTEL.Rows(0)("GOODS_NM")
            End If

        ElseIf OPTION_FLG Then

            If dsItinerary.RES_OPTION.Rows.Count > 0 Then
                ESTIMATE_GOODS_NM = dsItinerary.RES_OPTION.Rows(0)("GOODS_NM")
            End If

        ElseIf PACKAGE_FLG Then

            If dsItinerary.PAGE_20.Rows.Count > 0 Then
                ESTIMATE_GOODS_NM = dsItinerary.PAGE_20.Rows(0)("GOODS_NM")
            End If

        End If

        ' DP
        If TICKET_FLG And HOTEL_FLG Then

            Dim dsAirHotel As AirHotelDataSet = Session("dsAirHotel" & Me.RT_CD.Value & Me.S_CD.Value)

            If Not dsAirHotel Is Nothing AndAlso 0 < dsAirHotel.TOUR_DETAIL_DP.Rows.Count Then
                ESTIMATE_GOODS_NM = dsAirHotel.TOUR_DETAIL_DP.Rows(0)("GOODS_NM")
            End If

        End If


        If Not dsUser Is Nothing Then
            Dim COMPANY_KBN As String = dsUser.Tables("CLIENT_RES").Rows(0)("COMPANY_KBN")
            Dim SURNAME_KANJI As String = dsUser.Tables("CLIENT_RES").Rows(0)("SURNAME_KANJI")
            Dim NAME_KANJI As String = dsUser.Tables("CLIENT_RES").Rows(0)("NAME_KANJI")
            Me.ESTIMATE_TITLE.Text = ESTIMATE_GOODS_NM
            Me.ESTIMATE_ADDRESS.Text = SURNAME_KANJI & " " & NAME_KANJI
        End If


        ' コメントアウト Mitsuta
        'If Not Me.LogInPanel.Visible Then

        If TICKET_FLG And HOTEL_FLG And OVERSEAS_DOMESTIC_KBN.Equals("02") Then

            Me.BOOKPanel.Visible = False
            Me.BOOKPanel2.Visible = True

            Dim HOTEL_GOODS_CD As String = dsItinerary.RES_HOTEL.Rows(0)("SEASONALITY") & dsItinerary.RES_HOTEL.Rows(0)("SEASONALITY_KBN") & dsItinerary.RES_HOTEL.Rows(0)("GOODS_CD")

            Dim dsAirHotelDome As DataSet = HttpContext.Current.Session("dsAirHotelDome" & Me.RT_CD.Value & Me.S_CD.Value & HOTEL_GOODS_CD)

            Dim rDetailUrl() As DataRow = dsAirHotelDome.Tables("REQUEST_RESPONSE").Select("GET_TYPE='DETAIL_URL'")

            If 0 < rDetailUrl.Length Then
                Me.BackLinkButton.PostBackUrl = rDetailUrl(0)("REQUEST")
            End If

        End If

        'End If

        '予約受付制御
        If dsItinerary.WEB_TRANSACTION.Rows.Count > 0 Then
            Dim BOOKING_FLG As Boolean = SetRRValue.SetRowValue(dsItinerary.WEB_TRANSACTION.Rows(0), "BOOKING_FLG", True)
            If Not BOOKING_FLG Then
                Me.LogInPanel.Visible = False
                Me.PickUpHotelPanel.Visible = False
                Me.BOOKPanel.Visible = False
                Me.BOOKING_FLG_REMARKSPanel.Visible = True
                Me.BOOKING_FLG_REMARKS.Text = SetRRValue.SetRowValue(dsItinerary.WEB_TRANSACTION.Rows(0), "BOOKING_FLG_REMARKS")
            End If
        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "iniPage()", 976, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#End Region

#Region "アクション"

#Region "予約する"
    Protected Sub BookLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "BookLinkButton_Click()", 989, "開始", HttpContext.Current.Session.SessionID)

        dsItinerary.M019_CLIENT.Clear()
        dsItinerary.M021_CLIENT_ADDRESS.Clear()
        dsItinerary.M023_CLIENT_TEL.Clear()

        Select Case Me.RT_CD.Value
            Case "TWC"
                Dim isPartner As Boolean = False

                Dim SiteType As String = SetRRValue.setNothingValue(HttpContext.Current.Session("SiteType" & Me.RT_CD.Value & Me.S_CD.Value))
                If Not SiteType.Equals("") Then
                    isPartner = True
                End If

                Select Case Me.S_CD.Value
                    Case "02" 'B2B
                        isPartner = True
                End Select

                ' B2Bのセッションが残ってしまう場合があるため 22.10.17 Mitsuta
                If Not IsNothing(dsUser) AndAlso dsUser.Tables.Contains("CLIENT_RES") AndAlso dsUser.Tables("CLIENT_RES").Rows.Count > 0 Then
                    ' OK
                Else
                    isPartner = False
                End If

                If isPartner Then

                    Dim dsPartner As New DataSet
                    Dim dtPartner As New DataTable("REQ_DATA")
                    dtPartner.Columns.Add("s_Agent_Co_CD", GetType(String))
                    dtPartner.Columns.Add("s_Agent_Br_CD", GetType(String))
                    dtPartner.Columns.Add("s_Agent_Br_K", GetType(String))
                    dtPartner.Columns.Add("s_Zip_CD", GetType(String))
                    dtPartner.Columns.Add("s_State", GetType(String))
                    dtPartner.Columns.Add("s_Address_1", GetType(String))
                    dtPartner.Columns.Add("s_Address_2", GetType(String))
                    dtPartner.Columns.Add("s_Tel_Main", GetType(String))
                    dtPartner.Columns.Add("s_Fax_Main", GetType(String))
                    dtPartner.Columns.Add("SKEY", GetType(String))

                    Dim B2B_CLIENT_CD As String = dsUser.Tables("CLIENT_RES").Rows(0)("CLIENT_CD")
                    Dim ss3() As String = B2B_CLIENT_CD.Split("@")
                    Dim s_AGT_Co As String = ss3(0)
                    Dim s_AGT_Br As String = ss3(1)

                    Dim s_Zip_CD As String = ""
                    Dim s_Address_1 As String = ""
                    Dim s_Address_2 As String = ""
                    Dim s_Tel_Main As String = ""
                    Dim s_Fax_Main As String = ""

                    Dim rCLIENT_TEL_RES() As DataRow = dsUser.Tables("CLIENT_ADDRESS_RES").Select("ADDRESS_KBN='01'")

                    If 0 < rCLIENT_TEL_RES.Length Then
                        s_Zip_CD = rCLIENT_TEL_RES(0)("ZIPCODE")
                        s_Address_1 = rCLIENT_TEL_RES(0)("ADDRESS1")
                        s_Address_2 = rCLIENT_TEL_RES(0)("ADDRESS2")
                        s_Tel_Main = rCLIENT_TEL_RES(0)("TEL_NO")
                        s_Fax_Main = rCLIENT_TEL_RES(0)("FAX_NO")
                    End If

                    Dim rPartner As DataRow = dtPartner.NewRow
                    rPartner("s_Agent_Co_CD") = s_AGT_Co
                    rPartner("s_Agent_Br_CD") = s_AGT_Br
                    rPartner("s_Agent_Br_K") = dsUser.Tables("CLIENT_RES").Rows(0)("COMPANY_SECTION_NM_KANA")
                    rPartner("s_Zip_CD") = s_Zip_CD
                    rPartner("s_State") = ""
                    rPartner("s_Address_1") = s_Address_1
                    rPartner("s_Address_2") = s_Address_2
                    rPartner("s_Tel_Main") = s_Tel_Main
                    rPartner("s_Fax_Main") = s_Fax_Main
                    rPartner("SKEY") = Me.SESSION_NO.Value
                    dtPartner.Rows.Add(rPartner)
                    dsPartner.Merge(dtPartner)

                    Dim reserveLoad As New src.action.partner.ReserveLoad
                    reserveLoad.execute(Me.RT_CD.Value, Me.S_CD.Value, "Reserve", "Load", dsPartner)

                    Select Case Me.S_CD.Value
                        Case "03" 'B2B2Cの場合だけuserセッション削除
                            dsUser = Nothing
                            HttpContext.Current.Session.Remove("user" & Me.RT_CD.Value & Me.S_CD.Value)
                    End Select

                    If isBooking002 Then
                        CART002URL = "/page/booking/booking002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
                    Else
                        CART002URL = "./cart002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
                    End If
                    'Select Case Me.RT_CD.Value
                    '    Case "ASX", "ADV", "A0509", "TSJ", "RT11"
                    '        CART002URL = "/page/booking/booking002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value
                    '    Case Else
                    '        CART002URL = "./cart002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value
                    'End Select

                Else
                    CART002URL = "/partner/partnerlist?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
                End If
        End Select

        If Not dsUser Is Nothing Then
            Select Case MEMBER_ADD_KBN
                Case "5"
                    'ウエディング
                    If Not SetRRValue.setNothingValue(dsUser.Tables("CLIENT_RES").Rows(0)("DEPARTMENT_CD")).Equals("") Then
                        dsItinerary.PAGE_03.Rows(0)("MANAGE_CD") = SetRRValue.setNothingValue(dsUser.Tables("CLIENT_RES").Rows(0)("DEPARTMENT_CD"))
                    End If
                Case Else
                    If dsUser.Tables("CLIENT_RES").Rows.Count > 0 Then
                        dsItinerary.M019_CLIENT.ImportRow(dsUser.Tables("CLIENT_RES").Rows(0))
                    End If
                    If dsUser.Tables.Contains("CLIENT_ADDRESS_RES") Then
                        If dsUser.Tables("CLIENT_ADDRESS_RES").Rows.Count > 0 Then
                            dsItinerary.M021_CLIENT_ADDRESS.Merge(dsUser.Tables("CLIENT_ADDRESS_RES"))
                        End If
                    Else
                        Dim dsClientDetailRes As New TriphooB2CAPI.ClientDetailRes
                        dsUser.Merge(dsClientDetailRes.CLIENT_ADDRESS_RES)
                    End If
                    If dsUser.Tables.Contains("CLIENT_TEL_RES") Then
                        If dsUser.Tables("CLIENT_TEL_RES").Rows.Count > 0 Then
                            dsItinerary.M023_CLIENT_TEL.Merge(dsUser.Tables("CLIENT_TEL_RES"))
                        End If
                    Else
                        Dim dsClientDetailRes As New TriphooB2CAPI.ClientDetailRes
                        dsUser.Merge(dsClientDetailRes.CLIENT_TEL_RES)
                    End If

                    'Session.Add("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value, dsItinerary)
                    WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)
            End Select
        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "BookLinkButton_Click()", 1113, "終了", HttpContext.Current.Session.SessionID)

        Response.Redirect(CART002URL, True)

    End Sub
#End Region

#Region "ログイン"
    Protected Sub TO_LOGINLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "TO_LOGINLinkButton_Click()", 1124, "開始", HttpContext.Current.Session.SessionID)

        Dim E_MAIL As String = StrConv(Me.E_MAIL.Text, VbStrConv.Narrow)
        Dim PASSWORD As String = Me.PASSWORD.Text
        Dim USER_CASH As Boolean = Me.USER_CASH.Checked

        'データチェック
        Dim ErrMsg As String = DataCheck()

        If ErrMsg.Equals("") Then

            If Me.RT_CD.Value.Equals("ASX") Then
                ErrMsg = CommonUtil.USER_LOGIN(Me.RT_CD.Value, Me.S_CD.Value, "", E_MAIL, PASSWORD, "", Session.SessionID, REAL_COOKIE_VALUE, PERSONAL_COOKIE_VALUE)
            Else
                ErrMsg = CommonUtil.USER_LOGIN(Me.RT_CD.Value, Me.S_CD.Value, E_MAIL, "", PASSWORD)
            End If

            If Not ErrMsg.Equals("") And Not lang.Equals("1") Then
                ErrMsg = ErrMsg.Replace("入力されたメールアドレスが登録されていません。\n未登録の場合は新規会員登録（無料）より登録をお願いします。", "You entered Email address has not been registered. \nplease register from ""Member Register"".")
                ErrMsg = ErrMsg.Replace("入力されたメールアドレスもしくはパスワードに誤りがあります。\nご確認のうえ、もう一度入力しなおしてください。", "Email address or password you entered is incorrect. \nPlease check and re-enter.")
                ErrMsg = ErrMsg.Replace("入力されたメールアドレスは、５回以上パスワードを間違えた為、現在ご利用頂けません。\n「パスワードをお忘れの方はこちら」を押下してパスワードの再発行を行い、ログインし直してください。", "It was locked because you entered wrong password more than 5 times. \nPlease reissue password from ""Forgot Password"" and login again.")
            End If


            If ErrMsg.Equals("") Then

                If Me.RT_CD.Value.Equals("ASX") Then

                    'Cookieを設定
                    Dim isSuccessSetCookie As Boolean = SetCookieValue()

                    ' Cookie情報存在しない場合、エラー画面に遷移
                    If Not isSuccessSetCookie Then
                        Response.Redirect("../err/err002?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value, True)
                    End If

                End If

                'ユーザーキャッシュ
                If USER_CASH Then
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("USER_ID") = E_MAIL    '           ユーザーID
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("PASSWORD") =
                    EncryptString(PASSWORD, "TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value) '                            パスワード
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("USER_CASH") = USER_CASH
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("LOGIN_STS") = True
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value).Expires = DateTime.Now.AddDays(10) 'Cookie 有効期間（１０日）
                    Response.Cookies("TriphooWeb" & RT_CD.Value & Me.S_CD.Value).HttpOnly = True
                Else
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("USER_ID") = E_MAIL    '           ユーザーID
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("PASSWORD") =
                    EncryptString(PASSWORD, "TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value) '                            パスワード
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("USER_CASH") = USER_CASH
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("LOGIN_STS") = False
                    Response.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value).Expires = DateTime.Now.AddDays(10) 'Cookie 有効期間（１０日）
                    Response.Cookies("TriphooWeb" & RT_CD.Value & Me.S_CD.Value).HttpOnly = True
                End If

                dsItinerary.M019_CLIENT.Clear()
                dsItinerary.M021_CLIENT_ADDRESS.Clear()
                dsItinerary.M023_CLIENT_TEL.Clear()

                dsUser = Session("user" & Me.RT_CD.Value & Me.S_CD.Value)

                If dsUser.Tables("CLIENT_RES").Rows.Count > 0 Then
                    dsItinerary.PAGE_03.Rows(0)("CLIENT_CD") = dsUser.Tables("CLIENT_RES").Rows(0)("CLIENT_CD")
                    dsItinerary.M019_CLIENT.ImportRow(dsUser.Tables("CLIENT_RES").Rows(0))
                End If
                dsItinerary.M021_CLIENT_ADDRESS.Merge(dsUser.Tables("CLIENT_ADDRESS_RES"))
                dsItinerary.M023_CLIENT_TEL.Merge(dsUser.Tables("CLIENT_TEL_RES"))

                'Session.Add("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value, dsItinerary)
                WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)

                'BIN認証
                Select Case Me.RT_CD.Value & Me.S_CD.Value
                    Case "JGA03", "JGA04"
                        If SetRRValue.setNothingValueWeb(Session("Bin" & Me.RT_CD.Value & Me.S_CD.Value)).Equals("") Then
                            Dim url As String = "../login/login005?RT_CD=" & RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&URL=" & HttpUtility.UrlEncode(CART002URL)
                            Response.Redirect(url, True)
                        End If
                End Select
                Response.Redirect(CART002URL, True)
            Else
                ScriptManager.RegisterStartupScript(
            Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(
            Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)
        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "TO_LOGINLinkButton_Click()", 1200, "終了", HttpContext.Current.Session.SessionID)

    End Sub

    Protected Sub TO_LOGINLinkButton_Click1(sender As Object, e As System.EventArgs) Handles TO_LOGINLinkButton.Click

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "TO_LOGINLinkButton_Click1()", 1207, "開始", HttpContext.Current.Session.SessionID)

        Dim dsItinerary As New TriphooRR097DataSet

        Dim SessionCheck As Boolean = True
        SessionCheck = CommonWebAuthentication.SessionCheck(Me.RT_CD.Value, Me.S_CD.Value, "Itinerary", dsItinerary)

        TO_LOGINLinkButton_Click(dsItinerary, dsUser)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "TO_LOGINLinkButton_Click1()", 1217, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#Region "新規会員登録"
    Protected Sub CLIENT_ADDLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "CLIENT_ADDLinkButton_Click()", 1226, "開始", HttpContext.Current.Session.SessionID)

        Dim url As String = "../join/join001"
        url += "?RT_CD=" & Me.RT_CD.Value
        url += "&S_CD=" & Me.S_CD.Value
        url += "&skey=" & Me.SESSION_NO.Value

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "CLIENT_ADDLinkButton_Click()", 1233, "終了", HttpContext.Current.Session.SessionID)

        Response.Redirect(url, True)

    End Sub
#End Region

#Region "会員登録せずに予約する"
    Protected Sub NOT_LOGINLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "NOT_LOGINLinkButton_Click()", 1244, "開始", HttpContext.Current.Session.SessionID)

        'ユーザーセッション削除
        dsItinerary.M019_CLIENT.Clear()
        dsItinerary.M021_CLIENT_ADDRESS.Clear()
        dsItinerary.M023_CLIENT_TEL.Clear()
        'Session.Add("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value, dsItinerary)
        WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)

        Session.Remove("user" & Me.RT_CD.Value & Me.S_CD.Value)

        Dim SITE_DOMAIN_HTTPS As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("SITE_DOMAIN_HTTPS")

        Dim url As String = CART002URL

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "NOT_LOGINLinkButton_Click()", 1259, "終了", HttpContext.Current.Session.SessionID)

        Response.Redirect(url, True)

    End Sub
#End Region

#Region "パスワードを忘れた方はこちら"
    Protected Sub FORGET_PASSWORDLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        Dim url As String = "../forget/forget001"
        url += "?RT_CD=" & Me.RT_CD.Value
        url += "&S_CD=" & Me.S_CD.Value
        Response.Redirect(url, True)

    End Sub
#End Region

#Region "見積もり作成"
    Protected Sub EstimateLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "EstimateLinkButton_Click()", 1281, "開始", HttpContext.Current.Session.SessionID)

        Dim dsWork As New TriphooRR097DataSet
        dsWork.Merge(dsItinerary)

        dsWork.M019_CLIENT.Clear()
        dsWork.M021_CLIENT_ADDRESS.Clear()
        dsWork.M023_CLIENT_TEL.Clear()

        Dim RECORD_CITY_CD As String = dsWork.PAGE_03.Rows(0)("RECORD_CITY_CD")
        Dim TICKET_FLG As Boolean = dsWork.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsWork.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsWork.PAGE_03.Rows(0)("OPTION_FLG")
        Dim PACKAGE_FLG As Boolean = dsWork.PAGE_03.Rows(0)("PACKAGE_FLG")

        If 3 < RECORD_CITY_CD.Length Then
            RECORD_CITY_CD = RECORD_CITY_CD.Substring(0, 3)
        End If

        If Not dsUser Is Nothing Then
            dsWork.M019_CLIENT.Clear()
            dsWork.M021_CLIENT_ADDRESS.Clear()
            dsWork.M023_CLIENT_TEL.Clear()

            If dsUser.Tables("CLIENT_RES").Rows.Count > 0 Then

                Select Case MEMBER_ADD_KBN
                    Case "5"
                        dsWork.PAGE_03.Rows(0)("B2B_CLIENT_CD") = dsUser.Tables("CLIENT_RES").Rows(0)("CLIENT_CD")
                    Case Else
                        dsWork.PAGE_03.Rows(0)("CLIENT_CD") = dsUser.Tables("CLIENT_RES").Rows(0)("CLIENT_CD")
                End Select

                If Not dsUser.Tables("CLIENT_RES").Rows(0)("DEPARTMENT_CD").Equals("") Then
                    dsWork.PAGE_03.Rows(0)("MANAGE_CD") = dsUser.Tables("CLIENT_RES").Rows(0)("DEPARTMENT_CD")
                End If

            End If
        End If


        ' AIR, HOTEL, OPTIONAL, TOUR
        Dim DEP_PLACE As String = ""
        If TICKET_FLG Then

            If dsWork.PAGE_07.Rows.Count > 0 Then
                DEP_PLACE = dsWork.PAGE_07.Rows(0)("DEP_CD")
            End If

        ElseIf HOTEL_FLG Then

        ElseIf OPTION_FLG Then

        ElseIf PACKAGE_FLG Then

            If dsWork.PAGE_20.Rows.Count > 0 Then
                DEP_PLACE = dsWork.PAGE_20.Rows(0)("DEP_PLACE")
            End If

        End If

        Dim dsT001_ESTIMATE_DATA As New T001_ESTIMATE_DATA_DataSet
        Dim rT001_ESTIMATE_DATA As T001_ESTIMATE_DATA_DataSet.T001_ESTIMATE_DATARow = dsT001_ESTIMATE_DATA.T001_ESTIMATE_DATA.NewT001_ESTIMATE_DATARow
        rT001_ESTIMATE_DATA.RT_CD = Me.RT_CD.Value
        rT001_ESTIMATE_DATA.ESTIMATE_NO = "INSERT"
        rT001_ESTIMATE_DATA.OVERSEAS_DOMESTIC_KBN = dsWork.PAGE_03.Rows(0)("OVERSEAS_DOMESTIC_KBN")
        rT001_ESTIMATE_DATA.DEP_DATE = dsWork.PAGE_03.Rows(0)("DEP_TIME")
        rT001_ESTIMATE_DATA.DEP_PLACE = DEP_PLACE
        rT001_ESTIMATE_DATA.ARR_PLACE = RECORD_CITY_CD
        rT001_ESTIMATE_DATA.ESTIMATE_XML = dsWork.GetXml.ToString
        rT001_ESTIMATE_DATA.CLIENT_CD = SetRRValue.setNothingValue(dsWork.PAGE_03.Rows(0)("CLIENT_CD"))
        rT001_ESTIMATE_DATA.EDIT_TIME = Now
        rT001_ESTIMATE_DATA.EDIT_RT_CD = Me.RT_CD.Value
        rT001_ESTIMATE_DATA.EDIT_EMP_CD = "WEB"
        rT001_ESTIMATE_DATA.RES_NO = ""
        rT001_ESTIMATE_DATA.ESTIMATE_TITLE = Me.ESTIMATE_TITLE.Text
        rT001_ESTIMATE_DATA.ADDRESS = Me.ESTIMATE_ADDRESS.Text
        rT001_ESTIMATE_DATA.CREATE_TIME = Now
        rT001_ESTIMATE_DATA.ESTIMATE_LIMIT = Today.AddDays(7).ToString("yyyy/MM/dd")
        rT001_ESTIMATE_DATA.REMARKS = ""
        rT001_ESTIMATE_DATA.STS = "2"
        rT001_ESTIMATE_DATA.B2B_CLIENT_CD = SetRRValue.setNothingValue(dsWork.PAGE_03.Rows(0)("B2B_CLIENT_CD"))
        rT001_ESTIMATE_DATA.CHARGE_NAME = Me.EMP_NM.Text
        dsT001_ESTIMATE_DATA.T001_ESTIMATE_DATA.AddT001_ESTIMATE_DATARow(rT001_ESTIMATE_DATA)

        Dim dtKbn As New DataTable("PAGE_KBN")
        dtKbn.Columns.Add("PAGE_ID", GetType(String))

        dtKbn.Rows.Add("cart001")

        dsT001_ESTIMATE_DATA.Merge(dtKbn)

        Dim CartUtil As New CartUtil(Me.RT_CD.Value, Me.S_CD.Value, Request, lang)
        Dim dsMapper As DataSet = CartUtil.inquiry(dsT001_ESTIMATE_DATA, Me.RT_CD.Value, Me.S_CD.Value)

        If 0 < dsMapper.Tables.Count Then

            Dim SecureUtil As New TriphooRRUtil.src.util.SecureUtil

            Dim URL As String = ParameterUtil.TriphooRRReportUrl &
            "?REPORT_ID=Report002&RT_CD=" & Me.RT_CD.Value & "&EMP_CD=WEB&PASSWORD=" & SecureUtil.EncryptString("kero3pi", SecureUtil.PASSCODE_KEY) & "&ESTIMATE_NO=" & dsMapper.Tables(0).Rows(0)("ESTIMATE_NO") & "&SITE_CD=" & Me.S_CD.Value & "&lang=" & lang

            Me.ESTIMATE_NO.Text = dsMapper.Tables(0).Rows(0)("ESTIMATE_NO")
            Me.EstimateDownloadLinkButton.HRef = URL

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "$('#EstimateDownloadModal').modal('show');", True)

        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "EstimateLinkButton_Click()", 1391, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

    ' Cookie情報の設定
    Private Function SetCookieValue() As Boolean
        Dim userSession As DataSet = HttpContext.Current.Session("user" & Me.RT_CD.Value & Me.S_CD.Value)

        If userSession.Tables.Contains("CLIENT_COOKIE") AndAlso userSession.Tables("CLIENT_COOKIE").Rows.Count > 0 Then
            ' realCookieを設定
            Dim realCookieValue As String = userSession.Tables("CLIENT_COOKIE").Rows(0)("REAL_COOKIE_VALUE")
            If String.IsNullOrEmpty(realCookieValue) Then
                Return False
            Else
                Response.Cookies("real").Value = realCookieValue
            End If

            ' personalCookieを設定
            Dim personalCookieValue As String = userSession.Tables("CLIENT_COOKIE").Rows(0)("PERSONAL_COOKIE_VALUE")
            If String.IsNullOrEmpty(personalCookieValue) Then
                Return False
            Else
                Response.Cookies("personal").Value = personalCookieValue
            End If

            ' uniqueCookieを設定
            ' 会員番号取得
            Dim clientCd As String = userSession.Tables("CLIENT_RES").Rows(0)("CLIENT_CD")
            If String.IsNullOrEmpty(clientCd) Then
                Return False
            End If

            ' 性別取得
            Dim sexKbn As String = userSession.Tables("CLIENT_RES").Rows(0)("SEX_KBN")
            Dim sexCd As String = ""
            Select Case sexKbn
                Case "01" '男
                    sexCd = "9"
                Case "02" '女
                    sexCd = "8"
                Case "" 'OTHER
                    sexCd = "0"
                Case Else
                    Return False
            End Select

            ' 生年月日取得し、年代を計算する
            Dim birth As String = userSession.Tables("CLIENT_RES").Rows(0)("BIRTH")
            Dim eraCd As String = ""
            If String.IsNullOrEmpty(birth) Then
                Return False
            Else
                Dim nowDateTime As DateTime = Now
                Dim birthDateTime As DateTime = DateTime.Parse(birth)

                ' 生年と現行年の差分から年齢を算出
                Dim age As Integer = nowDateTime.Year - birthDateTime.Year

                ' 生年月日と現行日付を比較し、誕生日を迎えていない場合の年齢を調整する
                If birthDateTime > nowDateTime.AddYears(-age) Then
                    age -= 1
                End If

                ' 0歳以下はありえないので、エラー
                If age < 0 Then
                    Return False
                End If

                ' 算出年齢に該当する年代のコードを設定
                Select Case age
                    Case 0 To 9
                        eraCd = "A"
                    Case 10 To 14
                        eraCd = "B"
                    Case 15 To 19
                        eraCd = "C"
                    Case 20 To 24
                        eraCd = "D"
                    Case 25 To 29
                        eraCd = "E"
                    Case 30 To 34
                        eraCd = "F"
                    Case 35 To 39
                        eraCd = "G"
                    Case 40 To 44
                        eraCd = "H"
                    Case 45 To 49
                        eraCd = "I"
                    Case 50 To 54
                        eraCd = "J"
                    Case 55 To 59
                        eraCd = "K"
                    Case 60 To 64
                        eraCd = "L"
                    Case 65 To 69
                        eraCd = "M"
                    Case 70 To 74
                        eraCd = "N"
                    Case 75 To 79
                        eraCd = "O"
                    Case Else '80歳～
                        eraCd = "P"
                End Select
            End If

            Dim frequentStatus As String = userSession.Tables("CLIENT_COOKIE").Rows(0)("FREQUENT_STATUS")
            Dim tabinoTatsujinJoinFlg As String = userSession.Tables("CLIENT_COOKIE").Rows(0)("TABINO_TATSUJIN_JOIN_FLG")
            If String.IsNullOrEmpty(frequentStatus) OrElse String.IsNullOrEmpty(tabinoTatsujinJoinFlg) Then
                Return False
            Else
                Dim unique() As String = UNIQUE_COOKIE_VALUE.Split(":")
                ' userId_会員番号:年代:性別:
                Dim uniqueCookieValue As String = "userId_" & clientCd & ":" & eraCd & ":" & sexCd & ":" & frequentStatus & ":" & tabinoTatsujinJoinFlg
                Response.Cookies("unique").Value = uniqueCookieValue
                Return True
            End If
        Else
            Return False
        End If
    End Function

#End Region

#Region "util"

#Region "自動ログイン"
    Private Sub AutoLogIn()

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "AutoLogIn()", 1404, "開始", HttpContext.Current.Session.SessionID)

        '自動ログイン
        If Not Request.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value) Is Nothing Then

            If Not SetRRValue.setBoolean(Server.HtmlEncode(Request.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("USER_CASH"))) Then

                ' ログ生成 NSSOL負荷性能検証 2023/02/09
                'logger.CreateLog("page_cart_cart001", "AutoLogIn()", 1412, "終了", HttpContext.Current.Session.SessionID)

                Exit Sub
            End If

            If Not SetRRValue.setBoolean(Server.HtmlEncode(Request.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("LOGIN_STS"))) Then

                ' ログ生成 NSSOL負荷性能検証 2023/02/09
                'logger.CreateLog("page_cart_cart001", "AutoLogIn()", 1420, "終了", HttpContext.Current.Session.SessionID)

                Exit Sub
            End If

            Dim USER_ID As String = Server.HtmlEncode(Request.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("USER_ID"))
            Dim PASSWORD As String = DecryptString(Server.HtmlEncode(Request.Cookies("TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)("PASSWORD")), "TriphooWeb" & Me.RT_CD.Value & Me.S_CD.Value)

            'ユーザー情報取得
            CommonUtil.USER_LOGIN(Me.RT_CD.Value, Me.S_CD.Value, USER_ID, "", PASSWORD)

        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart001", "AutoLogIn()", 1434, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#Region "Cookie"
    ''' <summary>
    ''' 文字列を暗号化する
    ''' </summary>
    ''' <param name="sourceString">暗号化する文字列</param>
    ''' <param name="password">暗号化に使用するパスワード</param>
    ''' <returns>暗号化された文字列</returns>
    Public Shared Function EncryptString(ByVal sourceString As String,
                                         ByVal password As String) As String
        'RijndaelManagedオブジェクトを作成
        Dim rijndael As New System.Security.Cryptography.RijndaelManaged()

        'パスワードから共有キーと初期化ベクタを作成
        Dim key As Byte(), iv As Byte()
        GenerateKeyFromPassword(password, rijndael.KeySize, key, rijndael.BlockSize, iv)
        rijndael.Key = key
        rijndael.IV = iv

        '文字列をバイト型配列に変換する
        Dim strBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(sourceString)

        '対称暗号化オブジェクトの作成
        Dim encryptor As System.Security.Cryptography.ICryptoTransform =
            rijndael.CreateEncryptor()
        'バイト型配列を暗号化する
        Dim encBytes As Byte() = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length)
        '閉じる
        encryptor.Dispose()

        'バイト型配列を文字列に変換して返す
        Return System.Convert.ToBase64String(encBytes)
    End Function

    ''' <summary>
    ''' 暗号化された文字列を復号化する
    ''' </summary>
    ''' <param name="sourceString">暗号化された文字列</param>
    ''' <param name="password">暗号化に使用したパスワード</param>
    ''' <returns>復号化された文字列</returns>
    Public Shared Function DecryptString(ByVal sourceString As String,
                                         ByVal password As String) As String
        'RijndaelManagedオブジェクトを作成
        Dim rijndael As New System.Security.Cryptography.RijndaelManaged()

        'パスワードから共有キーと初期化ベクタを作成
        Dim key As Byte(), iv As Byte()
        GenerateKeyFromPassword(password, rijndael.KeySize, key, rijndael.BlockSize, iv)
        rijndael.Key = key
        rijndael.IV = iv

        '文字列をバイト型配列に戻す
        Dim strBytes As Byte() = System.Convert.FromBase64String(sourceString)

        '対称暗号化オブジェクトの作成
        Dim decryptor As System.Security.Cryptography.ICryptoTransform =
            rijndael.CreateDecryptor()
        'バイト型配列を復号化する
        '復号化に失敗すると例外CryptographicExceptionが発生
        Dim decBytes As Byte() = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length)
        '閉じる
        decryptor.Dispose()

        'バイト型配列を文字列に戻して返す
        Return System.Text.Encoding.UTF8.GetString(decBytes)
    End Function

    ''' <summary>
    ''' パスワードから共有キーと初期化ベクタを生成する
    ''' </summary>
    ''' <param name="password">基になるパスワード</param>
    ''' <param name="keySize">共有キーのサイズ（ビット）</param>
    ''' <param name="key">作成された共有キー</param>
    ''' <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
    ''' <param name="iv">作成された初期化ベクタ</param>
    Private Shared Sub GenerateKeyFromPassword(ByVal password As String,
                                               ByVal keySize As Integer,
                                               ByRef key As Byte(),
                                               ByVal blockSize As Integer,
                                               ByRef iv As Byte())
        'パスワードから共有キーと初期化ベクタを作成する
        'saltを決める
        Dim salt As Byte() = System.Text.Encoding.UTF8.GetBytes("saltは必ず8バイト以上")
        'Rfc2898DeriveBytesオブジェクトを作成する
        Dim deriveBytes As New System.Security.Cryptography.Rfc2898DeriveBytes(
            password, salt)
        '.NET Framework 1.1以下の時は、PasswordDeriveBytesを使用する
        'Dim deriveBytes As New System.Security.Cryptography.PasswordDeriveBytes( _
        '    password, salt)

        '反復処理回数を指定する デフォルトで1000回
        deriveBytes.IterationCount = 1000

        '共有キーと初期化ベクタを生成する
        key = deriveBytes.GetBytes(keySize \ 8)
        iv = deriveBytes.GetBytes(blockSize \ 8)
    End Sub
#End Region

#Region "DataCheck"
    Private Function DataCheck() As String

        Dim ErrMsg As String = ""

        '背景色リセット
        Me.E_MAIL.BackColor = Drawing.Color.White
        Me.PASSWORD.BackColor = Drawing.Color.White

        If Not Session("user" & Me.RT_CD.Value & Me.S_CD.Value) Is Nothing Then
            ErrMsg = "ログアウトしてからログインをしてください"
        End If

        If ErrMsg.Equals("") Then

            If Me.E_MAIL.Text.Equals("") Then
                If Me.RT_CD.Value.Equals("ASX") Then
                    ErrMsg += "お客様番号を入力してください。" & "\n"
                Else
                    ErrMsg += LABEL_0015 & "\n"
                End If
                Me.E_MAIL.BackColor = Drawing.Color.Pink
            End If

            If Me.PASSWORD.Text.Equals("") Then
                ErrMsg += LABEL_0016 & "\n"
                Me.PASSWORD.BackColor = Drawing.Color.Pink
            End If

        End If

        Return ErrMsg
    End Function
#End Region

#Region "getBedType"
    Public Function getBedType(ADT_NUM As Integer) As String

        Dim BED_TYPE As String = ""

        Select Case ADT_NUM
            Case 0
            Case 1
            Case 2 : BED_TYPE = "TWN"
        End Select

        Return BED_TYPE
    End Function
#End Region

#Region "setHeaderFooter"
    Private Function setHeaderFooter(str As String, dsItinerary As TriphooRR097DataSet) As String

        If Not str.Contains("@@") OrElse dsItinerary Is Nothing Then
            Return str
        End If

        Dim temp As String = str

        Try
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

        Catch ex As Exception
            Return temp
        End Try

        Return str
    End Function
#End Region

#End Region

End Class
