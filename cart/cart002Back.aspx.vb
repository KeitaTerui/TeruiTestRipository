#Region "Imports"
Imports System.Data
Imports System.Xml

Imports UsaelFrameWork.src.datacheck

Imports twClient
Imports twClient.TriphooB2CAPI

Imports tw.src.util
#End Region

Partial Class page_cart_cart002Back
    Inherits System.Web.UI.Page

#Region "クラス宣言"

    'src.util
    Dim CartUtil As CartUtil
    Dim CommonUtil As CommonUtil
    Dim checker As New CommonDataCheck
    Dim CommonWebAuthentication As New CommonWebAuthentication
    Dim CreateWebServiceManager As New CreateWebServiceManager
    Dim JoinUtil As JoinUtil
    Dim ParameterUtil As New ParameterUtil
    Dim SetValue As New SetValue
    Dim MoneyComma As New MoneyComma

    'TriphooRRUtil.src.util
    Dim CalcAgeByBirth As New TriphooRRUtil.src.util.CalcAgeByBirth
    Public SetRRValue As New TriphooRRUtil.src.util.SetValue
    Dim SetRRKbn As New TriphooRRUtil.src.util.SetKbn

    'WebService
    Dim TriphooRMClient As TriphooRMWebService.Service
    Dim B2CAPIClient As TriphooB2CAPI.Service

    'DataSet
    Dim dsRTUser As UserDataSet
    Dim dsRMUser As DataSet
    Dim dsB2CUser As DataSet
    Dim dsUser As New DataSet
    Dim lang As String = "1"

    Public isEstimate As Boolean = False
    Public LABEL_0053 As String = ""
    Public LABEL_0054 As String = ""
    Public LABEL_0055 As String = ""
    Public LABEL_0056 As String = ""
    Public LABEL_0057 As String = ""
    Public LABEL_0058 As String = ""
    Public LABEL_0059 As String = ""
    Public LABEL_0060 As String = ""
    Public LABEL_0061 As String = ""
    Public LABEL_0062 As String = ""
    Public LABEL_0063 As String = ""
    Public LABEL_0064 As String = ""
    Public LABEL_0065 As String = ""
    Public LABEL_0066 As String = ""
    Public LABEL_0067 As String = ""
    Public LABEL_0068 As String = ""
    Public LABEL_0069 As String = ""
    Public LABEL_0070 As String = ""
    Public LABEL_0071 As String = ""
    Public LABEL_0072 As String = ""
    Public LABEL_0073 As String = ""
    Public LABEL_0074 As String = ""
    Public LABEL_0075 As String = ""
    Public LABEL_0076 As String = ""
    Public LABEL_0077 As String = ""
    Public LABEL_0078 As String = ""
    Public LABEL_0079 As String = ""
    Public LABEL_0080 As String = ""
    Public LABEL_0081 As String = ""
    Public LABEL_0082 As String = ""
    Public LABEL_0083 As String = ""
    Public LABEL_0084 As String = ""
    Public LABEL_0085 As String = ""
    Public LABEL_0086 As String = ""
    Public LABEL_0087 As String = ""
    Public LABEL_0088 As String = ""
    Public LABEL_0089 As String = ""
    Public LABEL_0090 As String = ""
    Public LABEL_0091 As String = ""
    Public LABEL_0092 As String = ""
    Public LABEL_0114 As String = ""
    Public LABEL_0115 As String = ""
    Public LABEL_0116 As String = ""
    Public LABEL_0117 As String = ""
    Public LABEL_0118 As String = ""
    Public LABEL_0119 As String = ""
    Public LABEL_0120 As String = ""
    Public LABEL_0121 As String = ""
    Public LABEL_0122 As String = ""
    Public LABEL_0123 As String = ""
    Public LABEL_0124 As String = ""
    Public LABEL_0125 As String = ""
    Public LABEL_0126 As String = ""
    Public LABEL_0127 As String = ""
    Public LABEL_0128 As String = ""
    Public LABEL_0129 As String = ""
    Public LABEL_0130 As String = ""
    Public LABEL_0131 As String = ""
    Public LABEL_0132 As String = ""
    Public LABEL_0133 As String = ""
    Public LABEL_0134 As String = ""
    Public LABEL_0135 As String = ""
    Public LABEL_0136 As String = ""
    Public LABEL_0137 As String = ""
    Public LABEL_0138 As String = ""
    Public LABEL_0139 As String = ""
    Public LABEL_0140 As String = ""
    Public LABEL_0141 As String = ""
    Public LABEL_0142 As String = ""
    Public LABEL_0143 As String = ""
    Public LABEL_0144 As String = ""
    Public LABEL_0145 As String = ""
    Public LABEL_0146 As String = ""
    Public LABEL_0147 As String = ""
    Public LABEL_0148 As String = ""
    Public LABEL_0149 As String = ""
    Public LABEL_0150 As String = ""
    Public LABEL_0151 As String = ""
    Public LABEL_0152 As String = ""
    Public LABEL_0153 As String = ""
    Public LABEL_0154 As String = ""
    Public LABEL_0155 As String = ""
    Public LABEL_0156 As String = ""
    Public LABEL_0157 As String = ""
    Public LABEL_0158 As String = ""
    Public LABEL_0159 As String = ""
    Public LABEL_0160 As String = ""
    Public LABEL_0161 As String = ""
    Public LABEL_0162 As String = ""
    Public LABEL_0163 As String = ""
    Public LABEL_0164 As String = ""
    Public LABEL_0165 As String = ""
    Public LABEL_0166 As String = ""
    Public LABEL_0167 As String = ""
    Public LABEL_0168 As String = ""
    Public LABEL_0169 As String = ""
    Public LABEL_0170 As String = ""
    Public LABEL_0171 As String = ""
    Public LABEL_0180 As String = ""
    Public LABEL_0188 As String = ""
    Public LABEL_0189 As String = ""
    Public LABEL_0190 As String = ""
    Public LABEL_0191 As String = ""
    Public LABEL_0192 As String = ""
    Public LABEL_0193 As String = ""
    Public LABEL_0194 As String = ""
    Public LABEL_0195 As String = ""
    Public LABEL_0196 As String = ""
    Public LABEL_0197 As String = ""
    Public LABEL_0198 As String = ""
    Public LABEL_0199 As String = ""
    Public LABEL_0200 As String = ""
    Public LABEL_0201 As String = ""
    Public LABEL_0202 As String = ""
    Public LABEL_0203 As String = ""
    Public LABEL_0204 As String = ""
    Public LABEL_0205 As String = ""

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
                Session("lang") = SetRRValue.setNothingValueWeb(Request.Item("lang"))
            End If
            If Not Session("lang") Is Nothing Then
                lang = Session("lang")
            End If
            Me.HD_LANG.Value = lang

            '●インスタンス化
            TriphooRMClient = CreateWebServiceManager.CreateTriphooRMClient(Me.RT_CD.Value, Me.S_CD.Value)
            dsRMUser = CreateWebServiceManager.CreateTriphooRMUser(Me.RT_CD.Value, Me.S_CD.Value)

            B2CAPIClient = CreateWebServiceManager.CreateTriphooB2CAPIClient(Me.RT_CD.Value, Me.S_CD.Value)
            dsB2CUser = CreateWebServiceManager.CreateTriphooB2CAPIUser(Me.RT_CD.Value, Me.S_CD.Value, Request)

            CartUtil = New CartUtil(Me.RT_CD.Value, Me.S_CD.Value, Request, lang)
            CommonUtil = New CommonUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)
            JoinUtil = New JoinUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)

            '●ぱんくずリンク先設定
            Dim PANKUZU001URL As String = "cart001?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value

            Dim ESTIMATE_NO As String = SetRRValue.setNothingValueWeb(Request.Item("ESTIMATE_NO"))
            If Not ESTIMATE_NO.Equals("") Then
                PANKUZU001URL += "&ESTIMATE_NO=" & ESTIMATE_NO
                isEstimate = True
            End If
            'PANKUZU001URL = SetRRValue.setNothingValueWeb(PANKUZU001URL)

            Me.PANKUZU001LinkButton.Attributes.Add("href", PANKUZU001URL)

            '●セッション 更新
            Dim dsItinerary As New TriphooRR097DataSet

            Dim SessionCheck As Boolean = True
            SessionCheck = CommonWebAuthentication.SessionCheck(Me.RT_CD.Value, Me.S_CD.Value, "Itinerary", dsItinerary)

            If Not SessionCheck Or dsItinerary.PAGE_03.Rows.Count = 0 Then
                Response.Redirect(PANKUZU001URL, True)
            End If

            dsUser = Session("user" & Me._RT_CD.Value & Me.S_CD.Value)

            Me.PANKUZU001LinkButton.PostBackUrl = PANKUZU001URL

            '●スクリーン設定
            ScreenSet(dsItinerary, dsUser)

            setlang(lang)

            If Not IsPostBack Then
                '●ページ設定
                iniPage(dsItinerary, dsUser)
            Else
                '●アクション群
                Dim actionEve As String = Request.Item("__EVENTTARGET")

                If actionEve.Contains("CONFIRMLinkButton") Then                '確認
                    CONFIRMLinkButton_Click(dsItinerary, dsUser)
                ElseIf actionEve.Contains("BackLinkButton") Then               '戻る
                    Response.Redirect(PANKUZU001URL, True)
                ElseIf actionEve.Contains("MAIN_PERSON_CHANGELinkButton") Then '代表者情報変更
                    Me.MAIN_PERSON_INPUTPanel.Visible = True
                    Me.MAIN_PERSON_LABELPanel.Visible = False
                    Me.MAIN_PERSON_LABEL2Panel.Visible = False
                    Me.INPUT_MAIN_E_MAILLabel.Visible = True
                    Me.INPUT_MAIN_E_MAIL.Visible = False
                ElseIf actionEve.Contains("FORGET_PASSWORDLinkButton") Then
                    FORGET_PASSWORDLinkButton_Click(dsItinerary, dsUser) 'パスワードを忘れた方はこちら
                ElseIf actionEve.Contains("GET_CLIENT_INFO1LinkButton") Or
                       actionEve.Contains("GET_CLIENT_INFO2LinkButton") Or
                       actionEve.Contains("GET_CLIENT_INFO3LinkButton") Then
                    GET_CLIENT_INFOLinkButton_Click(dsItinerary, dsUser, actionEve) '顧客取込
                End If
            End If

        Catch ex As Exception
            '●エラー処理
            If Not (ex.Message.StartsWith("スレッドを中止") Or ex.Message.StartsWith("Thread was being aborted")) Then
                Dim ConcreteException As New src.common.ConcreteException
                ConcreteException.Exception(Request.Item("RT_CD"), Request.Item("S_CD"), "cart002", ex)
                Response.Redirect("../err/err002?RT_CD=" & Request.Item("RT_CD") & "&S_CD=" & Request.Item("S_CD"))
            End If
        End Try
    End Sub
#End Region

#Region "スクリーン設定"
    Private Sub ScreenSet(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        '/* 初期設定 * /
        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/base/lang.xml")

        Dim rXml() As DataRow = dsXml.Tables("TITLE").Select("LANGUAGE_KBN='" & lang & "'")

        Dim PAGE_TITLE As String = ""

        If rXml.Length > 0 Then
            PAGE_TITLE = rXml(0)("cart002")
            PAGE_TITLE = PAGE_TITLE.Replace("@@SITE_TITLE@@", dsRTUser.M137_RT_SITE_CD.Rows(0)("SITE_TITLE"))
            Me.Title = PAGE_TITLE
        End If
        '/* 初期設定 * /

        Dim dsWebScrnRes As TriphooCMSAPI.ScreenSettingDataSet = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart002", lang)

        If dsWebScrnRes.DETAIL_RES.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim PAGE_HEADER As String = ""
        Dim CSS As String = dsWebScrnRes.DETAIL_RES.Rows(0)("CSS")
        Dim SCRIPT As String = dsWebScrnRes.DETAIL_RES.Rows(0)("SCRIPT")

        'If Not CSS.Equals("") Then
        '    PAGE_HEADER += CSS
        'End If

        'If Not SCRIPT.Equals("") Then
        '    PAGE_HEADER += SCRIPT
        'End If

        PAGE_HEADER += dsWebScrnRes.DETAIL_RES.Rows(0)("HEADER")

        Me.PageHeader.Text = CStr(PAGE_HEADER).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
        'Me.PageSide.Text = dsWebScrnRes.DETAIL_RES.Rows(0)("SIDE")
        Me.Pagefooter.Text = CStr(dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER")).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")

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
    Private Sub iniPage(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim LAND_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("LAND_FLG")
        Dim OPERATOR_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPERATOR_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")
        Dim RENTACAR_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("RENTACAR_FLG")
        Dim OVERSEAS_DOMESTIC_KBN As String = dsItinerary.PAGE_03.Rows(0)("OVERSEAS_DOMESTIC_KBN")

        '※オプションのみ予約の場合のみ適用
        Dim MAIN_PERSON_ONLY As Boolean = False '代表者のみ名称必須
        Dim BIRTH_REQUIRED As Boolean = True   '生年月日入力必須
        Dim FLIGHT_REQUIRED As Boolean = False   'フライト必須
        Dim GO_FLIGHT_REQUIRED As Boolean = False   'フライト必須(日本出発時間/便)
        Dim RE_FLIGHT_REQUIRED As Boolean = False   'フライト必須(現地出発時間/便)
        Dim PASSPORT_REQUIRED As Boolean = False   'パスポート必須

        If Not TICKET_FLG And Not PACKAGE_FLG And Not HOTEL_FLG And OPTION_FLG Then

            'オプション複数予約対応
            Dim dvRES_OPTION As DataView = dsItinerary.RES_OPTION.DefaultView
            '●名称必須フラグで重複行削除
            Dim dtRES_OPTION As DataTable = dvRES_OPTION.ToTable(True, "MAIN_PERSON_ONLY")

            If dtRES_OPTION.Rows.Count = 1 Then
                '重複行削除の結果,1レコードのみだった場合1レコード目の値を取得する(True/Falseどちらかのケース)
                MAIN_PERSON_ONLY = dtRES_OPTION.Rows(0)("MAIN_PERSON_ONLY")
            Else
                '重複行削除の結果,2レコードだった場合Falseを設定する(True/False混在ケース)
                MAIN_PERSON_ONLY = False
            End If

            '●生年月日入力必須で重複行削除
            dtRES_OPTION = dvRES_OPTION.ToTable(True, "BIRTH_REQUIRED")

            If dtRES_OPTION.Rows.Count = 1 Then
                '重複行削除の結果,1レコードのみだった場合1レコード目の値を取得する(True/Falseどちらかのケース)
                BIRTH_REQUIRED = dtRES_OPTION.Rows(0)("BIRTH_REQUIRED")
            Else
                '重複行削除の結果,2レコードだった場合Falseを設定する(True/False混在ケース)
                BIRTH_REQUIRED = False
            End If

            '●フライト必須で重複行削除
            dtRES_OPTION = dvRES_OPTION.ToTable(True, "FLIGHT_REQUIRED")

            If dtRES_OPTION.Rows.Count = 1 Then
                '重複行削除の結果,1レコードのみだった場合1レコード目の値を取得する(True/Falseどちらかのケース)
                GO_FLIGHT_REQUIRED = dtRES_OPTION.Rows(0)("FLIGHT_REQUIRED")
            Else
                '重複行削除の結果,2レコードだった場合Falseを設定する(True/False混在ケース)
                GO_FLIGHT_REQUIRED = False
            End If

            '●フライト必須で重複行削除
            dtRES_OPTION = dvRES_OPTION.ToTable(True, "FLIGHT_REQUIRED_RETURN")

            If dtRES_OPTION.Rows.Count = 1 Then
                '重複行削除の結果,1レコードのみだった場合1レコード目の値を取得する(True/Falseどちらかのケース)
                RE_FLIGHT_REQUIRED = dtRES_OPTION.Rows(0)("FLIGHT_REQUIRED_RETURN")
            Else
                '重複行削除の結果,2レコードだった場合Falseを設定する(True/False混在ケース)
                RE_FLIGHT_REQUIRED = False
            End If

            '●パスポート必須で重複行削除
            dtRES_OPTION = dvRES_OPTION.ToTable(True, "PASSPORT_REQUIRED")

            If dtRES_OPTION.Rows.Count = 1 Then
                '重複行削除の結果,1レコードのみだった場合1レコード目の値を取得する(True/Falseどちらかのケース)
                PASSPORT_REQUIRED = dtRES_OPTION.Rows(0)("PASSPORT_REQUIRED")
            Else
                '重複行削除の結果,2レコードだった場合Falseを設定する(True/False混在ケース)
                PASSPORT_REQUIRED = False
            End If

            '●パスポート必須かつ代表者のみ名称必須だった場合
            If PASSPORT_REQUIRED And MAIN_PERSON_ONLY Then
                MAIN_PERSON_ONLY = False
            End If

        End If

        Dim PASSENGER_NAME_DISP_KBN As String = ""
        Try
            '0:英字
            '1:カナ
            '10:ミドルネームあり
            '※カンマ区切りで複数可

            Select Case OVERSEAS_DOMESTIC_KBN
                Case "02" '国内
                    PASSENGER_NAME_DISP_KBN = dsRTUser.M137_RT_SITE_CD.Rows(0)("PASSENGER_NAME_DISP_KBN_DOMESTIC")
                Case Else
                    PASSENGER_NAME_DISP_KBN = dsRTUser.M137_RT_SITE_CD.Rows(0)("PASSENGER_NAME_DISP_KBN_OVERSEAS")
            End Select

        Catch ex As Exception
        End Try

        If PASSENGER_NAME_DISP_KBN.Equals("") Then
            PASSENGER_NAME_DISP_KBN = "0"
        End If

        Dim strPASSENGER_NAME_DISP_KBN() As String = PASSENGER_NAME_DISP_KBN.Split(",")

        Dim PASSENGER_NAME_ROMAN_DISP_FLG As Boolean = True
        Dim PASSENGER_NAME_KANA_DISP_FLG As Boolean = True
        Dim PASSENGER_NAME_KANJI_DISP_FLG As Boolean = True
        Dim PASSENGER_NAME_MIDDLE_DISP_FLG As Boolean = True

        If Array.IndexOf(strPASSENGER_NAME_DISP_KBN, "0") < 0 Then
            PASSENGER_NAME_ROMAN_DISP_FLG = False
        End If

        If Array.IndexOf(strPASSENGER_NAME_DISP_KBN, "1") < 0 Then
            PASSENGER_NAME_KANA_DISP_FLG = False
        End If

        If Array.IndexOf(strPASSENGER_NAME_DISP_KBN, "2") < 0 Then
            PASSENGER_NAME_KANJI_DISP_FLG = False
        End If

        If Array.IndexOf(strPASSENGER_NAME_DISP_KBN, "10") < 0 Then
            PASSENGER_NAME_MIDDLE_DISP_FLG = False
        End If

        Dim PKG_SEX_KBN As String = ""

        If 0 < dsItinerary.PAGE_20.Rows.Count Then
            PKG_SEX_KBN = SetRRValue.setNothingValue(dsItinerary.PAGE_20.Rows(0)("SEX_KBN"))
        End If

        If dsUser Is Nothing Then

            'ログインしないで予約する場合、性別と生年月日を非表示とする
            Me.INPUT_MAIN_SEXPanel.Visible = False
            Me.INPUT_MAIN_BIRTHPanel.Visible = False
            Me.INPUT_MAIN_E_MAILLabel.Visible = False
            Me.INPUT_MAIN_E_MAIL.Visible = True
            Me.LABEL_MAIN_SEXPanel.Visible = False
            Me.LABEL_MAIN_BIRTHPanel.Visible = False

            '代表者情報のみ 且つ 生年月日必須だった場合
            If MAIN_PERSON_ONLY And BIRTH_REQUIRED Then
                Me.INPUT_MAIN_BIRTHPanel.Visible = True
                Me.LABEL_MAIN_BIRTHPanel.Visible = True
            End If

        End If

        Dim PASSPORT_NO_DISP_FLG As Boolean = False
        Dim PASSPORT_LIMIT_DISP_FLG As Boolean = False
        Dim PASSPORT_DATE_DISP_FLG As Boolean = False
        Dim PASSPORT_ISSUE_COUNTRY_DISP_FLG As Boolean = False
        Dim NATIONALITY_DISP_FLG As Boolean = False
        Dim VISA_DISP_FLG As Boolean = False
        Dim MILEAGE_DISP_FLG As Boolean = False
        Dim SEAT_REQUEST_DISP_FLG As Boolean = False
        Dim SERVICE_DISP_FLG As Boolean = True

        'パスポート入力欄表示制御
        If isEstimate Then
            PASSPORT_NO_DISP_FLG = True
        End If

        If PASSPORT_REQUIRED Then
            PASSPORT_NO_DISP_FLG = True
            PASSPORT_DATE_DISP_FLG = True
        End If

        Select Case Me.RT_CD.Value & Me.S_CD.Value
            Case "NTABTM02"
                PASSPORT_NO_DISP_FLG = True
                PASSPORT_LIMIT_DISP_FLG = True
                VISA_DISP_FLG = True
                MILEAGE_DISP_FLG = True
                SEAT_REQUEST_DISP_FLG = True

                LABEL_0065 = "パスポート情報（任意）"

        End Select

        If PACKAGE_FLG AndAlso 0 < dsItinerary.PAGE_20.Rows.Count Then
            Try
                SERVICE_DISP_FLG = dsItinerary.PAGE_20.Rows(0)("SERVICE_DISP_FLG")
            Catch ex As Exception
            End Try
        Else
            SERVICE_DISP_FLG = False
        End If

        Dim dsM222_PACKAGE_SERVICE As New DataSet
        If PACKAGE_FLG AndAlso SERVICE_DISP_FLG Then
            dsM222_PACKAGE_SERVICE.Merge(B2CAPIClient.SelectM222_PACKAGE_SERVICE_FOR_WEB_Gateway(Me.RT_CD.Value,
                                                                                                 dsItinerary.PAGE_20.Rows(0)("SEASONALITY"),
                                                                                                 dsItinerary.PAGE_20.Rows(0)("SEASONALITY_KBN"),
                                                                                                 dsItinerary.PAGE_20.Rows(0)("GOODS_CD"),
                                                                                                 dsItinerary.PAGE_20.Rows(0)("DEP_DATE"),
                                                                                                 dsB2CUser))

        End If

        Dim dtMeal As New DataTable

        If dsItinerary.PAGE_05.Rows.Count > 0 Then
            Try
                Dim GDS_KBN As String = dsItinerary.PAGE_05.Rows(0)("GDS_KBN")

                If GDS_KBN.Equals("22") Then

                    dsItinerary.RES_SERVICE.Clear()

                    'XML_DATA解析
                    Dim XML_DATA As String = dsItinerary.PAGE_05.Rows(0)("XML_DATA")

                    Dim xmlDoc As XmlDocument = New XmlDocument
                    xmlDoc.LoadXml(XML_DATA)

                    Dim XLRequiredParameter As XmlNodeList = xmlDoc.GetElementsByTagName("RequiredParameter")
                    Dim CTRequiredParameter As Integer = 0

                    '/* RequiredParameter */
                    For CTRequiredParameter = 0 To XLRequiredParameter.Count - 1

                        Dim ERequiredParameter As XmlElement = XLRequiredParameter(CTRequiredParameter)

                        Dim Name As String = ""
                        Dim DisplayText As String = ""
                        Dim PerPassenger As Boolean = False
                        Dim IsOptional As Boolean = False

                        If ERequiredParameter.GetElementsByTagName("Name").Count > 0 Then
                            Name = ERequiredParameter.GetElementsByTagName("Name")(0).InnerText
                        End If

                        If ERequiredParameter.GetElementsByTagName("PerPassenger").Count > 0 Then
                            PerPassenger = ERequiredParameter.GetElementsByTagName("PerPassenger")(0).InnerText
                        End If

                        If ERequiredParameter.GetElementsByTagName("IsOptional").Count > 0 Then
                            '"true" : 当該パラメーター情報は任意項目、"false"　または　省略：当該パラメーター情報は必須項目
                            IsOptional = ERequiredParameter.GetElementsByTagName("IsOptional")(0).InnerText
                        End If

                        If ERequiredParameter.GetElementsByTagName("DisplayText").Count > 0 Then
                            DisplayText = ERequiredParameter.GetElementsByTagName("DisplayText")(0).InnerText
                        End If

                        Select Case Name
                            Case "PassportNumber"
                                If Not IsOptional Then
                                    PASSPORT_NO_DISP_FLG = True
                                    PASSPORT_DATE_DISP_FLG = True
                                End If
                            Case "PassportExpiryDate"
                                If Not IsOptional Then
                                    PASSPORT_LIMIT_DISP_FLG = True
                                End If
                            Case "Nationality"
                                If Not IsOptional Then
                                    NATIONALITY_DISP_FLG = True
                                End If
                            Case "PassportCountryOfIssue"
                                If Not IsOptional Then
                                    PASSPORT_ISSUE_COUNTRY_DISP_FLG = True
                                End If
                            'Case "PostCode"
                            '    If Not IsOptional Then
                            '        POST_CODE_DISP_FLG = True
                            '    End If
                            'Case "FrequentFlyerNumber"
                            '    If Not IsOptional Then
                            '        MILAGE_CARD_DISP_FLG = True
                            '    End If
                            Case "MealType"

                                dtMeal = GetMealType(dsItinerary, DisplayText)

                            Case "OutwardLuggageOptions"

                                GetBuggage(xmlDoc, DisplayText, dsItinerary, "01")

                            Case "ReturnLuggageOptions"

                                GetBuggage(xmlDoc, DisplayText, dsItinerary, "02")

                        End Select
                    Next
                    '/* RequiredParameter */

                End If
            Catch ex As Exception

            End Try

        End If

        Session.Add("Itinerary" & Me._RT_CD.Value & Me.S_CD.Value, dsItinerary)

        Dim dsM002_COUNTRY As TriphooRMWebService.M002_COUNTRY_DataSet = TriphooRMClient.SelectM002_COUNTRYGateway("", dsRMUser)
        Dim dvM002_COUNTRY As DataView = dsM002_COUNTRY.M002_COUNTRY.DefaultView
        dvM002_COUNTRY.Sort = "COUNTRY_NM_JP ASC"

        Dim dtM002_COUNTRY As New DataTable
        dtM002_COUNTRY.Merge(dvM002_COUNTRY.ToTable(True, "COUNTRY_CD", "COUNTRY_NM_JP"))

        Dim ROOM_PASSENGER_FLG As Boolean = False

        If TICKET_FLG Or OPTION_FLG Then
            ROOM_PASSENGER_FLG = False
        ElseIf PACKAGE_FLG Or HOTEL_FLG Then
            If 0 < dsItinerary.RES_HOTEL_NAME.Rows.Count Then
                ROOM_PASSENGER_FLG = True
            End If
        End If

        '表示設定
        If MAIN_PERSON_ONLY Then
            dsItinerary.PAGE_04.Clear()
        End If

        If Not dsItinerary.PAGE_04.Columns.Contains("NAME_NO_NM") Then
            dsItinerary.PAGE_04.Columns.Add("NAME_NO_NM", GetType(String))
        End If

        For iCount = 0 To dsItinerary.PAGE_04.Rows.Count - 1

            Dim NAME_NO_NM As String = Me.LABEL_0204

            If 0 < iCount Then
                NAME_NO_NM = Me.LABEL_0205 & iCount
            End If

            dsItinerary.PAGE_04.Rows(iCount)("NAME_NO_NM") = NAME_NO_NM
        Next

        Dim dvWork As DataView = dsItinerary.PAGE_04.DefaultView
        Select Case Me.RT_CD.Value
            Case "ATR"
                dvWork.RowFilter = "MAIN_PERSON_FLG=0"

                'ツアー付帯サービス
                If SERVICE_DISP_FLG Then
                    Dim dvM222_PACKAGE_SERVICE As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
                    dvM222_PACKAGE_SERVICE.RowFilter = "OF_KBN='2'" ' 1:全て 2:人ごと
                    Dim dtM222_PACKAGE_SERVICE As DataTable = dvM222_PACKAGE_SERVICE.ToTable(True, "SERVICE_CD", "SERVICE_NM", "SELECT_KBN", "SELECT_WAY_KBN", "SERVICE_DISP_REMARKS")

                    If 0 < dtM222_PACKAGE_SERVICE.Rows.Count Then

                        Me.MAIN_PERSON_TOUR_SERVICEPanel.Visible = True
                        Me.MAIN_PERSON_TOUR_SERVICERepeater.Visible = True

                        Me.MAIN_PERSON_TOUR_SERVICERepeater.DataSource = dtM222_PACKAGE_SERVICE
                        Me.MAIN_PERSON_TOUR_SERVICERepeater.DataBind()

                        For m = 0 To dtM222_PACKAGE_SERVICE.Rows.Count - 1

                            Dim SERVICE_CD As String = dtM222_PACKAGE_SERVICE.Rows(m)("SERVICE_CD")
                            Dim SELECT_WAY_KBN As String = dtM222_PACKAGE_SERVICE.Rows(m)("SELECT_WAY_KBN")

                            Dim dvM222_PACKAGE_SERVICE_SUB As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
                            dvM222_PACKAGE_SERVICE_SUB.RowFilter = "SERVICE_CD='" & SERVICE_CD & "'" ' 1:全て 2:人ごと
                            Dim dtM222_PACKAGE_SERVICE_SUB As DataTable = dvM222_PACKAGE_SERVICE_SUB.ToTable(True, "SERVICE_PRICE_CD", "SERVICE_PRICE_NM", "SALES_PRICE", "FROM_STOCK_COUNT", "STOCK_COUNT", "PRICE_DISP_REMARKS")

                            Select Case SELECT_WAY_KBN' 1:一つのみ 2:複数回答
                                Case "1"
                                    CreateDropDownListItems(dtM222_PACKAGE_SERVICE_SUB, CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList))
                                    CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHOICEPanel"), Panel).Visible = True
                                Case "2"
                                    CreateCheckBoxItems(dtM222_PACKAGE_SERVICE_SUB, CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater))
                                    CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater).Visible = True
                                Case "3"
                                    CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUTPanel"), Panel).Visible = True
                            End Select

                        Next

                    End If


                End If


        End Select

        Dim dtWork As DataTable = dvWork.ToTable(True)

        If Not ROOM_PASSENGER_FLG Then

            Me.PASSENGER_OTHERPanel.Visible = True

            '表示設定
            If dtWork.Rows.Count = 0 Then
                Me.PASSENGERPanel.Visible = False
            End If

            Me.PASSENGER_OTHERRepeater.DataSource = dtWork
            Me.PASSENGER_OTHERRepeater.DataBind()

            For i = 0 To dtWork.Rows.Count - 1

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SURNAME_ROMAN"), TextBox).Attributes("placeholder") = LABEL_0056
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMAN"), TextBox).Attributes("placeholder") = LABEL_0058
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTHLabel"), Label).Text = LABEL_0059
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).Attributes("placeholder") = LABEL_0060

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).DataSource = SetValue.setBIRTH_MM(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).DataBind()

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).DataSource = SetValue.setBIRTH_DD(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).DataBind()

                Dim AGE_KBN As String = dsItinerary.PAGE_04.Rows(i)("AGE_KBN")
                Dim BIRTH As String = dsItinerary.PAGE_04.Rows(i)("BIRTH")

                Select Case PKG_SEX_KBN' 01:男性限定 02:女性限定 03:回答しない 04:回答不要
                    Case "01" ' 01:男性限定
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MAN"), RadioButton).Visible = True
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("WOMAN"), RadioButton).Visible = False
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NOANSWER"), RadioButton).Visible = False
                    Case "02" ' 02:女性限定
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MAN"), RadioButton).Visible = False
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("WOMAN"), RadioButton).Visible = True
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NOANSWER"), RadioButton).Visible = False
                    Case "03" ' 03:回答しない
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MAN"), RadioButton).Visible = True
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("WOMAN"), RadioButton).Visible = True
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NOANSWER"), RadioButton).Visible = True
                    Case "04" ' 04:回答不要
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SEX_FLGPanel"), Panel).Visible = False
                End Select

                Dim SEX As String = dsItinerary.PAGE_04.Rows(i)("SEX")
                Select Case SEX
                    Case "01" : CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MAN"), RadioButton).Checked = True
                    Case "02" : CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("WOMAN"), RadioButton).Checked = True
                    Case "03" : CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NOANSWER"), RadioButton).Checked = True
                End Select

                If Not SetRRValue.setDispDate(BIRTH).Equals("") Then
                    CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).Text = CDate(BIRTH).ToString("yyyy")
                    CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).SelectedValue = CInt(CDate(BIRTH).ToString("MM"))
                    CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).SelectedValue = CInt(CDate(BIRTH).ToString("dd"))
                End If


                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataSource = SetValue.setPASSPORT_LIMIT_MM(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataBind()

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataSource = SetValue.setPASSPORT_LIMIT_DD(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataBind()

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_MM"), DropDownList).DataSource = SetValue.setBIRTH_MM(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_MM"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_MM"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_MM"), DropDownList).DataBind()

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_DD"), DropDownList).DataSource = SetValue.setBIRTH_DD(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_DD"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_DD"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_DD"), DropDownList).DataBind()

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).DataSource = SetValue.setBIRTH_MM(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).DataBind()

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).DataSource = SetValue.setBIRTH_DD(True)
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).DataTextField = "TEXT"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).DataValueField = "VALUE"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).DataBind()

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataSource = dtM002_COUNTRY
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataTextField = "COUNTRY_NM_JP"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataValueField = "COUNTRY_CD"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataBind()
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).SelectedValue = "JP"

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITY"), DropDownList).DataSource = dtM002_COUNTRY
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITY"), DropDownList).DataTextField = "COUNTRY_NM_JP"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITY"), DropDownList).DataValueField = "COUNTRY_CD"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITY"), DropDownList).DataBind()
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITY"), DropDownList).SelectedValue = "JP"

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).DataSource = dtM002_COUNTRY
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).DataTextField = "COUNTRY_NM_JP"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).DataValueField = "COUNTRY_CD"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).DataBind()
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).SelectedValue = "JP"

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).DataSource = dtM002_COUNTRY
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).DataTextField = "COUNTRY_NM_JP"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).DataValueField = "COUNTRY_CD"
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).DataBind()
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).SelectedValue = "JP"

                '表示設定
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTHPanel"), Panel).Visible = BIRTH_REQUIRED
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMANPanel"), Panel).Visible = PASSENGER_NAME_ROMAN_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MIDDLE_NMPanel"), Panel).Visible = PASSENGER_NAME_MIDDLE_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_KANAPanel"), Panel).Visible = PASSENGER_NAME_KANA_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_KANJIPanel"), Panel).Visible = PASSENGER_NAME_KANJI_DISP_FLG

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_NOPanel"), Panel).Visible = PASSPORT_NO_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATEPanel"), Panel).Visible = PASSPORT_DATE_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMITPanel"), Panel).Visible = PASSPORT_LIMIT_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRYPanel"), Panel).Visible = PASSPORT_ISSUE_COUNTRY_DISP_FLG

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITYPanel"), Panel).Visible = NATIONALITY_DISP_FLG

                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SEAT_REQUESTPanel"), Panel).Visible = SEAT_REQUEST_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISAPanel"), Panel).Visible = VISA_DISP_FLG
                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGEPanel"), Panel).Visible = MILEAGE_DISP_FLG

                '付帯サービス
                If 0 < dsItinerary.RES_SERVICE.Rows.Count Then
                    If Not AGE_KBN.Equals("INF") Then
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICEPanel"), Panel).Visible = True
                        'PASSENGER_OPTION_REQUESTRepeater
                        Dim ServiceRepeater As Repeater = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICERepeater"), Repeater)
                        ServiceRepeater.DataSource = dsItinerary.PAGE_07
                        ServiceRepeater.DataBind()

                        For k = 0 To dsItinerary.PAGE_07.Rows.Count - 1

                            Dim SEGMENT_SEQ As String = dsItinerary.PAGE_07.Rows(k)("SEGMENT_SEQ")

                            Dim dvRES_SERVICE As DataView = dsItinerary.RES_SERVICE.DefaultView
                            dvRES_SERVICE.RowFilter = "FLIGHT_SEGMENT_LINE_NO='" & SEGMENT_SEQ & "'"
                            dvRES_SERVICE.Sort = "SERVICE_KBN ASC"

                            Dim dtRES_SERVICE As DataTable = dvRES_SERVICE.ToTable(True, "SERVICE_KBN")

                            Dim SubRepeater As Repeater = CType(ServiceRepeater.Items(k).FindControl("RES_SERVICE_SUBRepeater"), Repeater)
                            SubRepeater.DataSource = dtRES_SERVICE
                            SubRepeater.DataBind()

                            For m = 0 To dtRES_SERVICE.Rows.Count - 1

                                Dim SERVICE_KBN As String = dtRES_SERVICE.Rows(m)("SERVICE_KBN")

                                Dim dvSERVICE_LIST As DataView = dsItinerary.RES_SERVICE.DefaultView
                                dvSERVICE_LIST.RowFilter = "FLIGHT_SEGMENT_LINE_NO='" & SEGMENT_SEQ & "' And SERVICE_KBN='" & SERVICE_KBN & "'"

                                Dim dtSERVICE_LIST As DataTable = dvSERVICE_LIST.ToTable(True, "SERVICE_KBN", "SERVICE_TYPE", "SERVICE_CD", "SERVICE_NM", "SALE_PRICE")

                                For Each sbrow In dtSERVICE_LIST.Rows

                                    Dim SERVICE_NM As String = sbrow("SERVICE_NM")
                                    Dim SALE_PRICE As String = MoneyComma.addYen2(CInt(sbrow("SALE_PRICE")), LABEL_0154)
                                    SERVICE_NM += " ( ＋" & SALE_PRICE & " )"
                                    sbrow("SERVICE_NM") = SERVICE_NM
                                Next

                                Dim dtSERVICE_LIST_CLONE As DataTable = dtSERVICE_LIST.Clone

                                ' 共通： 1:受託手荷物、2:食事、3:座席、4:コンフォートキット、5:ビデオオンデマンド、6:機内持込手荷物、9:その他

                                Select Case SERVICE_KBN
                                    Case "1"
                                        dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                    Case "2"
                                        dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                    Case "3"
                                        dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                    Case "4"
                                        dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                    Case "5"
                                        dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                    Case "6"
                                        dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "航空会社が定める無料範囲内　+0円", 0)
                                    Case "9"
                                        dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                End Select
                                dtSERVICE_LIST_CLONE.Merge(dtSERVICE_LIST)

                                CType(SubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataSource = dtSERVICE_LIST_CLONE
                                CType(SubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataTextField = "SERVICE_NM"
                                CType(SubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataValueField = "SERVICE_CD"
                                CType(SubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataBind()

                            Next


                        Next

                    End If
                End If

                'ツアー付帯サービス
                If SERVICE_DISP_FLG Then
                    Dim dvM222_PACKAGE_SERVICE As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
                    dvM222_PACKAGE_SERVICE.RowFilter = "OF_KBN='2'" ' 1:全て 2:人ごと
                    Dim dtM222_PACKAGE_SERVICE As DataTable = dvM222_PACKAGE_SERVICE.ToTable(True, "SERVICE_CD", "SERVICE_NM", "SELECT_KBN", "SELECT_WAY_KBN", "SERVICE_DISP_REMARKS")

                    If 0 < dtM222_PACKAGE_SERVICE.Rows.Count Then

                        CType(PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURPanel"), Panel).Visible = True
                        CType(PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURRepeater"), Repeater).Visible = True

                        CType(PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURRepeater"), Repeater).DataSource = dtM222_PACKAGE_SERVICE
                        CType(PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURRepeater"), Repeater).DataBind()

                        For m = 0 To dtM222_PACKAGE_SERVICE.Rows.Count - 1

                            Dim TourServiceRepeater As Repeater = CType(PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURRepeater"), Repeater)

                            Dim SERVICE_CD As String = dtM222_PACKAGE_SERVICE.Rows(m)("SERVICE_CD")
                            Dim SELECT_WAY_KBN As String = dtM222_PACKAGE_SERVICE.Rows(m)("SELECT_WAY_KBN")

                            Dim dvM222_PACKAGE_SERVICE_SUB As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
                            dvM222_PACKAGE_SERVICE_SUB.RowFilter = "SERVICE_CD='" & SERVICE_CD & "'" ' 1:全て 2:人ごと
                            Dim dtM222_PACKAGE_SERVICE_SUB As DataTable = dvM222_PACKAGE_SERVICE_SUB.ToTable(True, "SERVICE_PRICE_CD", "SERVICE_PRICE_NM", "SALES_PRICE", "FROM_STOCK_COUNT", "STOCK_COUNT", "PRICE_DISP_REMARKS")

                            'If Not dtM222_PACKAGE_SERVICE_SUB.Rows(0)("PRICE_DISP_REMARKS").Equals("") Then
                            '    CType(TourServiceRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Text = dtM222_PACKAGE_SERVICE_SUB.Rows(0)("PRICE_DISP_REMARKS")
                            'Else
                            '    CType(TourServiceRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Visible = False
                            'End If

                            Select Case SELECT_WAY_KBN' 1:一つのみ 2:複数回答
                                Case "1"
                                    CreateDropDownListItems(dtM222_PACKAGE_SERVICE_SUB, CType(TourServiceRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList))
                                    CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHOICEPanel"), Panel).Visible = True
                                Case "2"
                                    CreateCheckBoxItems(dtM222_PACKAGE_SERVICE_SUB, CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater))
                                    CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater).Visible = True
                                    'CType(TourServiceRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Visible = False

                                Case "3"
                                    CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUTPanel"), Panel).Visible = True
                            End Select

                        Next

                    End If


                End If


                If isEstimate Then
                    If Not dtWork.Rows(i)("FAMILY_NAME").Equals("") Then
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SURNAME_ROMAN"), TextBox).ReadOnly = True
                    End If

                    If Not dtWork.Rows(i)("FIRST_NAME").Equals("") Then
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMAN"), TextBox).ReadOnly = True
                    End If

                    If Not SetRRValue.setDispDate(BIRTH).Equals("") Then
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).ReadOnly = True
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).Visible = False
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).Visible = False
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM_TEXT"), TextBox).Visible = True
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD_TEXT"), TextBox).Visible = True
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM_TEXT"), TextBox).Text = CInt(CDate(BIRTH).ToString("MM"))
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD_TEXT"), TextBox).Text = CInt(CDate(BIRTH).ToString("dd"))
                    End If

                    If Not SEX.Equals("") Then
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MAN"), RadioButton).Enabled = False
                        CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("WOMAN"), RadioButton).Enabled = False
                    End If

                End If

            Next

        End If

        If ROOM_PASSENGER_FLG Then

            '表示設定
            If dtWork.Rows.Count = 0 Then
                Me.PASSENGERPanel.Visible = False
            End If

            Me.PASSENGER_HOTELPanel.Visible = True
            Me.PASSENGER_OTHERPanel.Visible = False

            Dim workView As DataView = dsItinerary.RES_HOTEL.DefaultView
            Dim workTable As DataTable
            If PACKAGE_FLG Then
                workView.RowFilter = String.Format("CHECKIN=#{0}# AND CHECKOUT=#{1}#", dsItinerary.RES_HOTEL.Rows(0)("CHECKIN"), dsItinerary.RES_HOTEL.Rows(0)("CHECKOUT"))
                workTable = workView.ToTable(True)
            Else
                workTable = workView.ToTable(True, "HOTEL_SEQ", "ROOM_TYPE_NM")
            End If

            Me.PASSENGER_HOTELRepeater.DataSource = workTable
            Me.PASSENGER_HOTELRepeater.DataBind()

            For i = 0 To workTable.Rows.Count - 1
                Dim HOTEL_SEQ As String = workTable.Rows(i)("HOTEL_SEQ")

                Dim dtPAGE_04 As DataTable = dtWork.Clone

                Dim rRES_HOTEL_NAME() As DataRow = dsItinerary.RES_HOTEL_NAME.Select("HOTEL_SEQ='" & HOTEL_SEQ & "'")
                For Each row As DataRow In rRES_HOTEL_NAME
                    Dim rPAGE_04() As DataRow = dtWork.Select("NAME_NO='" & row("NAME_NO") & "'")

                    If rPAGE_04.Length = 0 Then
                        Continue For
                    End If

                    dtPAGE_04.ImportRow(rPAGE_04(0))

                Next

                Dim SubRepeater As Repeater = CType(Me.PASSENGER_HOTELRepeater.Items(i).FindControl("PASSENGER_HOTEL_NAMERepeater"), Repeater)
                SubRepeater.DataSource = dtPAGE_04
                SubRepeater.DataBind()

                For k = 0 To dtPAGE_04.Rows.Count - 1

                    CType(SubRepeater.Items(k).FindControl("BIRTHLabel"), Label).Text = LABEL_0082
                    CType(SubRepeater.Items(k).FindControl("BIRTH_YYYY"), TextBox).Attributes("placeholder") = LABEL_0083
                    CType(SubRepeater.Items(k).FindControl("SURNAME_ROMAN"), TextBox).Attributes("placeholder") = LABEL_0056
                    CType(SubRepeater.Items(k).FindControl("NAME_ROMAN"), TextBox).Attributes("placeholder") = LABEL_0058

                    CType(SubRepeater.Items(k).FindControl("BIRTH_MM"), DropDownList).DataSource = SetValue.setBIRTH_MM(True)
                    CType(SubRepeater.Items(k).FindControl("BIRTH_MM"), DropDownList).DataTextField = "TEXT"
                    CType(SubRepeater.Items(k).FindControl("BIRTH_MM"), DropDownList).DataValueField = "VALUE"
                    CType(SubRepeater.Items(k).FindControl("BIRTH_MM"), DropDownList).DataBind()

                    CType(SubRepeater.Items(k).FindControl("BIRTH_DD"), DropDownList).DataSource = SetValue.setBIRTH_DD(True)
                    CType(SubRepeater.Items(k).FindControl("BIRTH_DD"), DropDownList).DataTextField = "TEXT"
                    CType(SubRepeater.Items(k).FindControl("BIRTH_DD"), DropDownList).DataValueField = "VALUE"
                    CType(SubRepeater.Items(k).FindControl("BIRTH_DD"), DropDownList).DataBind()

                    Dim BIRTH As String = dtPAGE_04.Rows(k)("BIRTH")
                    Dim SEX As String = dtPAGE_04.Rows(k)("SEX")
                    Dim AGE_KBN As String = dtPAGE_04.Rows(k)("AGE_KBN")

                    Select Case SEX
                        Case "01" : CType(SubRepeater.Items(k).FindControl("MAN"), RadioButton).Checked = True
                        Case "02" : CType(SubRepeater.Items(k).FindControl("WOMAN"), RadioButton).Checked = True
                        Case "03" : CType(SubRepeater.Items(k).FindControl("NOANSWER"), RadioButton).Checked = True
                    End Select

                    If Not SetRRValue.setDispDate(BIRTH).Equals("") Then
                        CType(SubRepeater.Items(k).FindControl("BIRTH_YYYY"), TextBox).Text = CDate(BIRTH).ToString("yyyy")
                        CType(SubRepeater.Items(k).FindControl("BIRTH_MM"), DropDownList).SelectedValue = CInt(CDate(BIRTH).ToString("MM"))
                        CType(SubRepeater.Items(k).FindControl("BIRTH_DD"), DropDownList).SelectedValue = CInt(CDate(BIRTH).ToString("dd"))
                    End If

                    CType(SubRepeater.Items(k).FindControl("NAME_ROMANPanel"), Panel).Visible = PASSENGER_NAME_ROMAN_DISP_FLG
                    CType(SubRepeater.Items(k).FindControl("MIDDLE_NMPanel"), Panel).Visible = PASSENGER_NAME_MIDDLE_DISP_FLG
                    CType(SubRepeater.Items(k).FindControl("NAME_KANAPanel"), Panel).Visible = PASSENGER_NAME_KANA_DISP_FLG
                    CType(SubRepeater.Items(k).FindControl("NAME_KANJIPanel"), Panel).Visible = PASSENGER_NAME_KANJI_DISP_FLG

                    Select Case PKG_SEX_KBN' 01:男性限定 02:女性限定 03:回答しない 04:回答不要
                        Case "01" ' 01:男性限定
                            CType(SubRepeater.Items(k).FindControl("MAN"), RadioButton).Visible = True
                            CType(SubRepeater.Items(k).FindControl("WOMAN"), RadioButton).Visible = False
                            CType(SubRepeater.Items(k).FindControl("NOANSWER"), RadioButton).Visible = False
                        Case "02" ' 02:女性限定
                            CType(SubRepeater.Items(k).FindControl("MAN"), RadioButton).Visible = False
                            CType(SubRepeater.Items(k).FindControl("WOMAN"), RadioButton).Visible = True
                            CType(SubRepeater.Items(k).FindControl("NOANSWER"), RadioButton).Visible = False
                        Case "03" ' 03:回答しない
                            CType(SubRepeater.Items(k).FindControl("MAN"), RadioButton).Visible = True
                            CType(SubRepeater.Items(k).FindControl("WOMAN"), RadioButton).Visible = True
                            CType(SubRepeater.Items(k).FindControl("NOANSWER"), RadioButton).Visible = True
                        Case "04" ' 04:回答不要
                            CType(SubRepeater.Items(k).FindControl("SEX_FLGPanel"), Panel).Visible = False
                    End Select

                    If Me.RT_CD.Value.Equals("CDT") Then
                        CType(SubRepeater.Items(k).FindControl("BIRTHPanel"), Panel).Visible = False
                    Else
                        CType(SubRepeater.Items(k).FindControl("BIRTHPanel"), Panel).Visible = True
                    End If

                    '付帯サービス
                    If 0 < dsItinerary.RES_SERVICE.Rows.Count Then

                        Dim PASSPORT_DATE As String = dtPAGE_04.Rows(k)("PASSPORT_DATE")
                        Dim PASSPORT_LIMIT As String = dtPAGE_04.Rows(k)("PASSPORT_LIMIT")
                        Dim PASSPORT_ISSUE_COUNTRY As String = dtPAGE_04.Rows(k)("PASSPORT_ISSUE_COUNTRY")
                        Dim NATIONALITY As String = dtPAGE_04.Rows(k)("NATIONALITY")

                        CType(SubRepeater.Items(k).FindControl("PASSPORT_NOPanel"), Panel).Visible = PASSPORT_NO_DISP_FLG
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATEPanel"), Panel).Visible = False
                        CType(SubRepeater.Items(k).FindControl("NATIONALITYPanel"), Panel).Visible = NATIONALITY_DISP_FLG
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMITPanel"), Panel).Visible = PASSPORT_LIMIT_DISP_FLG
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRYPanel"), Panel).Visible = PASSPORT_ISSUE_COUNTRY_DISP_FLG

                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_MM"), DropDownList).DataSource = SetValue.setBIRTH_MM(True)
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_MM"), DropDownList).DataTextField = "TEXT"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_MM"), DropDownList).DataValueField = "VALUE"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_MM"), DropDownList).DataBind()

                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_DD"), DropDownList).DataSource = SetValue.setBIRTH_DD(True)
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_DD"), DropDownList).DataTextField = "TEXT"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_DD"), DropDownList).DataValueField = "VALUE"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_DD"), DropDownList).DataBind()

                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataSource = SetValue.setBIRTH_MM(True)
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataTextField = "TEXT"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataValueField = "VALUE"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).DataBind()

                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataSource = SetValue.setBIRTH_DD(True)
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataTextField = "TEXT"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataValueField = "VALUE"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).DataBind()

                        CType(SubRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataSource = dtM002_COUNTRY
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataTextField = "COUNTRY_NM_JP"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataValueField = "COUNTRY_CD"
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).DataBind()
                        CType(SubRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).SelectedValue = "JP"

                        If Not SetRRValue.setDispDate(PASSPORT_DATE).Equals("") Then
                            CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_YYYY"), DropDownList).SelectedValue = CDate(PASSPORT_DATE).ToString("yyyy")
                            CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_MM"), DropDownList).SelectedValue = CInt(CDate(PASSPORT_DATE).ToString("MM"))
                            CType(SubRepeater.Items(k).FindControl("PASSPORT_DATE_DD"), DropDownList).SelectedValue = CInt(CDate(PASSPORT_DATE).ToString("dd"))
                        End If

                        If Not SetRRValue.setDispDate(PASSPORT_LIMIT).Equals("") Then
                            CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_YYYY"), DropDownList).SelectedValue = CDate(PASSPORT_LIMIT).ToString("yyyy")
                            CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).SelectedValue = CInt(CDate(PASSPORT_LIMIT).ToString("MM"))
                            CType(SubRepeater.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).SelectedValue = CInt(CDate(PASSPORT_LIMIT).ToString("dd"))
                        End If

                        If Not NATIONALITY.Equals("") Then
                            CType(SubRepeater.Items(k).FindControl("NATIONALITY"), DropDownList).SelectedValue = NATIONALITY
                        End If

                        If Not PASSPORT_ISSUE_COUNTRY.Equals("") Then
                            CType(SubRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).SelectedValue = PASSPORT_ISSUE_COUNTRY
                        End If



                        If Not AGE_KBN.Equals("INF") Then
                            CType(SubRepeater.Items(k).FindControl("RES_SERVICEPanel"), Panel).Visible = True
                            'PASSENGER_OPTION_REQUESTRepeater
                            Dim ServiceRepeater As Repeater = CType(SubRepeater.Items(k).FindControl("RES_SERVICERepeater"), Repeater)
                            ServiceRepeater.DataSource = dsItinerary.PAGE_07
                            ServiceRepeater.DataBind()

                            For n = 0 To dsItinerary.PAGE_07.Rows.Count - 1

                                Dim SEGMENT_SEQ As String = dsItinerary.PAGE_07.Rows(n)("SEGMENT_SEQ")

                                Dim dvRES_SERVICE As DataView = dsItinerary.RES_SERVICE.DefaultView
                                dvRES_SERVICE.RowFilter = "FLIGHT_SEGMENT_LINE_NO='" & SEGMENT_SEQ & "'"
                                dvRES_SERVICE.Sort = "SERVICE_KBN ASC"

                                Dim dtRES_SERVICE As DataTable = dvRES_SERVICE.ToTable(True, "SERVICE_KBN")

                                Dim ServiceSubRepeater As Repeater = CType(ServiceRepeater.Items(n).FindControl("RES_SERVICE_SUBRepeater"), Repeater)
                                ServiceSubRepeater.DataSource = dtRES_SERVICE
                                ServiceSubRepeater.DataBind()

                                For m = 0 To dtRES_SERVICE.Rows.Count - 1

                                    Dim SERVICE_KBN As String = dtRES_SERVICE.Rows(m)("SERVICE_KBN")

                                    Dim dvSERVICE_LIST As DataView = dsItinerary.RES_SERVICE.DefaultView
                                    dvSERVICE_LIST.RowFilter = "FLIGHT_SEGMENT_LINE_NO='" & SEGMENT_SEQ & "' And SERVICE_KBN='" & SERVICE_KBN & "'"

                                    Dim dtSERVICE_LIST As DataTable = dvSERVICE_LIST.ToTable(True, "SERVICE_KBN", "SERVICE_TYPE", "SERVICE_CD", "SERVICE_NM", "SALE_PRICE")

                                    For Each sbrow In dtSERVICE_LIST.Rows

                                        Dim SERVICE_NM As String = sbrow("SERVICE_NM")
                                        Dim SALE_PRICE As String = MoneyComma.addYen2(CInt(sbrow("SALE_PRICE")), LABEL_0154)
                                        SERVICE_NM += " ( ＋" & SALE_PRICE & " )"
                                        sbrow("SERVICE_NM") = SERVICE_NM
                                    Next

                                    Dim dtSERVICE_LIST_CLONE As DataTable = dtSERVICE_LIST.Clone
                                    ' 共通： 1:受託手荷物、2:食事、3:座席、4:コンフォートキット、5:ビデオオンデマンド、6:機内持込手荷物、9:その他

                                    Select Case SERVICE_KBN
                                        Case "1"
                                            dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                        Case "2"
                                            dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                        Case "3"
                                            dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                        Case "4"
                                            dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                        Case "5"
                                            dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                        Case "6"
                                            dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "航空会社が定める無料範囲内　+0円", 0)
                                        Case "9"
                                            dtSERVICE_LIST_CLONE.Rows.Add("", "", "", "なし　+0円", 0)
                                    End Select
                                    dtSERVICE_LIST_CLONE.Merge(dtSERVICE_LIST)

                                    CType(ServiceSubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataSource = dtSERVICE_LIST_CLONE
                                    CType(ServiceSubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataTextField = "SERVICE_NM"
                                    CType(ServiceSubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataValueField = "SERVICE_CD"
                                    CType(ServiceSubRepeater.Items(m).FindControl("SERVICE"), DropDownList).DataBind()

                                Next


                            Next

                        End If


                    End If

                    'ツアー付帯サービス
                    If SERVICE_DISP_FLG Then
                        Dim dvM222_PACKAGE_SERVICE As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
                        dvM222_PACKAGE_SERVICE.RowFilter = "OF_KBN='2'" ' 1:全て 2:人ごと
                        Dim dtM222_PACKAGE_SERVICE As DataTable = dvM222_PACKAGE_SERVICE.ToTable(True, "SERVICE_CD", "SERVICE_NM", "SELECT_KBN", "SELECT_WAY_KBN", "SERVICE_DISP_REMARKS")

                        If 0 < dtM222_PACKAGE_SERVICE.Rows.Count Then

                            CType(SubRepeater.Items(k).FindControl("RES_SERVICE_TOURPanel"), Panel).Visible = True
                            CType(SubRepeater.Items(k).FindControl("RES_SERVICE_TOURRepeater"), Repeater).Visible = True

                            CType(SubRepeater.Items(k).FindControl("RES_SERVICE_TOURRepeater"), Repeater).DataSource = dtM222_PACKAGE_SERVICE
                            CType(SubRepeater.Items(k).FindControl("RES_SERVICE_TOURRepeater"), Repeater).DataBind()

                            For m = 0 To dtM222_PACKAGE_SERVICE.Rows.Count - 1

                                Dim TourServiceRepeater As Repeater = CType(SubRepeater.Items(k).FindControl("RES_SERVICE_TOURRepeater"), Repeater)

                                Dim SERVICE_CD As String = dtM222_PACKAGE_SERVICE.Rows(m)("SERVICE_CD")
                                Dim SELECT_WAY_KBN As String = dtM222_PACKAGE_SERVICE.Rows(m)("SELECT_WAY_KBN")

                                Dim dvM222_PACKAGE_SERVICE_SUB As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
                                dvM222_PACKAGE_SERVICE_SUB.RowFilter = "SERVICE_CD='" & SERVICE_CD & "'" ' 1:全て 2:人ごと
                                Dim dtM222_PACKAGE_SERVICE_SUB As DataTable = dvM222_PACKAGE_SERVICE_SUB.ToTable(True, "SERVICE_PRICE_CD", "SERVICE_PRICE_NM", "SALES_PRICE", "FROM_STOCK_COUNT", "STOCK_COUNT", "PRICE_DISP_REMARKS")

                                'If Not dtM222_PACKAGE_SERVICE_SUB.Rows(0)("PRICE_DISP_REMARKS").Equals("") Then
                                '    CType(TourServiceRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Text = dtM222_PACKAGE_SERVICE_SUB.Rows(0)("PRICE_DISP_REMARKS")
                                'Else
                                '    CType(TourServiceRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Visible = False
                                'End If

                                Select Case SELECT_WAY_KBN' 1:一つのみ 2:複数回答
                                    Case "1"
                                        CreateDropDownListItems(dtM222_PACKAGE_SERVICE_SUB, CType(TourServiceRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList))
                                        CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHOICEPanel"), Panel).Visible = True
                                    Case "2"
                                        CreateCheckBoxItems(dtM222_PACKAGE_SERVICE_SUB, CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater))
                                        CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater).Visible = True
                                        'CType(TourServiceRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Visible = False
                                    Case "3"
                                        CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUTPanel"), Panel).Visible = True
                                End Select

                            Next

                        End If


                    End If

                    If isEstimate Then
                        Dim FAMILY_NAME As String = dtPAGE_04.Rows(k)("FAMILY_NAME")
                        Dim FIRST_NAME As String = dtPAGE_04.Rows(k)("FIRST_NAME")

                        If Not FAMILY_NAME.Equals("") Then
                            CType(SubRepeater.Items(k).FindControl("SURNAME_ROMAN"), TextBox).ReadOnly = True
                        End If

                        If Not FIRST_NAME.Equals("") Then
                            CType(SubRepeater.Items(k).FindControl("NAME_ROMAN"), TextBox).ReadOnly = True
                        End If

                        If Not SetRRValue.setDispDate(BIRTH).Equals("") Then
                            CType(SubRepeater.Items(k).FindControl("BIRTH_YYYY"), TextBox).ReadOnly = True
                            CType(SubRepeater.Items(k).FindControl("BIRTH_MM"), DropDownList).Visible = False
                            CType(SubRepeater.Items(k).FindControl("BIRTH_DD"), DropDownList).Visible = False
                            CType(SubRepeater.Items(k).FindControl("BIRTH_MM_TEXT"), TextBox).Visible = True
                            CType(SubRepeater.Items(k).FindControl("BIRTH_DD_TEXT"), TextBox).Visible = True
                            CType(SubRepeater.Items(k).FindControl("BIRTH_MM_TEXT"), TextBox).Text = CInt(CDate(BIRTH).ToString("MM"))
                            CType(SubRepeater.Items(k).FindControl("BIRTH_DD_TEXT"), TextBox).Text = CInt(CDate(BIRTH).ToString("dd"))
                        End If

                        Select Case SEX
                            Case "01", "02"
                                CType(SubRepeater.Items(k).FindControl("MAN"), RadioButton).Enabled = False
                                CType(SubRepeater.Items(k).FindControl("WOMAN"), RadioButton).Enabled = False
                        End Select
                    End If

                Next

                'i += 1
            Next

        End If

        If OVERSEAS_DOMESTIC_KBN.Equals("02") Then
            '国内ツアーの場合パスポートメッセージは非表示とする
            Me.PASSPORT_MESSAGEPanel.Visible = False
        End If

        BindData(dsItinerary, dsUser)

        getPage(dsItinerary, dsUser)

        If dsM222_PACKAGE_SERVICE.Tables.Contains("M222_PACKAGE_SERVICE") Then
            Dim dvM222_PACKAGE_SERVICE_GOODS As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
            dvM222_PACKAGE_SERVICE_GOODS.RowFilter = "OF_KBN='1'" ' 1:全て 2:人ごと
            Dim dtM222_PACKAGE_SERVICE_GOODS As DataTable = dvM222_PACKAGE_SERVICE_GOODS.ToTable(True, "SERVICE_CD", "SERVICE_NM", "SELECT_KBN", "SELECT_WAY_KBN", "SERVICE_DISP_REMARKS")

            If 0 < dtM222_PACKAGE_SERVICE_GOODS.Rows.Count Then

                Me.RES_SERVICE_TOURRepeater.DataSource = dtM222_PACKAGE_SERVICE_GOODS
                Me.RES_SERVICE_TOURRepeater.DataBind()

                For m = 0 To dtM222_PACKAGE_SERVICE_GOODS.Rows.Count - 1

                    Dim SERVICE_CD As String = dtM222_PACKAGE_SERVICE_GOODS.Rows(m)("SERVICE_CD")
                    Dim SELECT_WAY_KBN As String = dtM222_PACKAGE_SERVICE_GOODS.Rows(m)("SELECT_WAY_KBN")

                    Dim dvM222_PACKAGE_SERVICE_SUB As DataView = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").DefaultView
                    dvM222_PACKAGE_SERVICE_SUB.RowFilter = "SERVICE_CD='" & SERVICE_CD & "'" ' 1:全て 2:人ごと
                    Dim dtM222_PACKAGE_SERVICE_SUB As DataTable = dvM222_PACKAGE_SERVICE_SUB.ToTable(True, "SERVICE_PRICE_CD", "SERVICE_PRICE_NM", "SALES_PRICE", "FROM_STOCK_COUNT", "STOCK_COUNT", "PRICE_DISP_REMARKS")

                    'If Not dtM222_PACKAGE_SERVICE_SUB.Rows(0)("PRICE_DISP_REMARKS").Equals("") Then
                    '    CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Text = dtM222_PACKAGE_SERVICE_SUB.Rows(0)("PRICE_DISP_REMARKS")
                    'Else
                    '    CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Visible = False
                    'End If

                    Select Case SELECT_WAY_KBN' 1:一つのみ 2:複数回答
                        Case "1"
                            CreateDropDownListItems(dtM222_PACKAGE_SERVICE_SUB, CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList))
                            'CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).DataSource = dtM222_PACKAGE_SERVICE_SUB
                            'CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).DataTextField = "SERVICE_PRICE_NM_DISP"
                            'CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).DataValueField = "SERVICE_PRICE_CD"
                            'CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).DataBind()
                            CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHOICEPanel"), Panel).Visible = True
                        Case "2"
                            CreateCheckBoxItems(dtM222_PACKAGE_SERVICE_SUB, CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater))
                            CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater).Visible = True
                            'CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("PRICE_DISP_REMARKSLabel"), Label).Visible = False
                        Case "3"
                            CType(RES_SERVICE_TOURRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUTPanel"), Panel).Visible = True
                    End Select

                Next


            Else
                Me.RES_SERVICE_TOURPanel.Visible = False
            End If

        Else
            Me.RES_SERVICE_TOURPanel.Visible = False
        End If

        '出発日、帰国日表示設定

        If Not TICKET_FLG And Not PACKAGE_FLG And Not HOTEL_FLG And OPTION_FLG Then
            Me.DEP_DATEPanel.Visible = False
            Me.RET_DATEPanel.Visible = False

            If RE_FLIGHT_REQUIRED Then
                Me.LOCAL_DEP_DATEPanel.Visible = True
                Me.LOCAL_DEP_TIMEPanel.Visible = True
                Me.LOCAL_DEP_FLIGHTPanel.Visible = True
            Else
                Me.LOCAL_DEP_DATEPanel.Visible = False
                Me.LOCAL_DEP_TIMEPanel.Visible = False
                Me.LOCAL_DEP_FLIGHTPanel.Visible = False
            End If
            If GO_FLIGHT_REQUIRED Then
                Me.LOCAL_ARR_DATEPanel.Visible = True
                Me.LOCAL_ARR_TIMEPanel.Visible = True
                Me.LOCAL_ARR_FLIGHTPanel.Visible = True
            Else
                Me.LOCAL_ARR_DATEPanel.Visible = False
                Me.LOCAL_ARR_TIMEPanel.Visible = False
                Me.LOCAL_ARR_FLIGHTPanel.Visible = False
            End If

        End If

        If 0 < dsItinerary.RES_OPTION.Rows.Count Then
            Dim LOCAL_ARR_TIME As String = dsItinerary.RES_OPTION.Rows(0)("LOCAL_ARR_TIME")
            Dim LOCAL_DEP_TIME As String = dsItinerary.RES_OPTION.Rows(0)("LOCAL_DEP_TIME")
            Dim LOCAL_ARR_FLIGHT As String = dsItinerary.RES_OPTION.Rows(0)("LOCAL_ARR_FLIGHT")
            Dim LOCAL_DEP_FLIGHT As String = dsItinerary.RES_OPTION.Rows(0)("LOCAL_DEP_FLIGHT")

            If Not LOCAL_ARR_TIME.Equals("") AndAlso Not LOCAL_ARR_TIME.StartsWith("1900") Then
                Me.LOCAL_ARR_DATE.Text = CDate(LOCAL_ARR_TIME).ToString("yyyy/MM/dd")
                Me.LOCAL_ARR_TIME_HH.SelectedValue = CDate(LOCAL_ARR_TIME).ToString("HH")
                Me.LOCAL_ARR_TIME_MM.SelectedValue = CDate(LOCAL_ARR_TIME).ToString("mm")
            End If

            If Not LOCAL_DEP_TIME.Equals("") AndAlso Not LOCAL_DEP_TIME.StartsWith("1900") Then
                Me.LOCAL_DEP_DATE.Text = CDate(LOCAL_DEP_TIME).ToString("yyyy/MM/dd")
                Me.LOCAL_DEP_TIME_HH.SelectedValue = CDate(LOCAL_DEP_TIME).ToString("HH")
                Me.LOCAL_DEP_TIME_MM.SelectedValue = CDate(LOCAL_DEP_TIME).ToString("mm")
            End If

            If Not LOCAL_ARR_FLIGHT.Equals("") AndAlso 2 < LOCAL_ARR_FLIGHT.Length Then
                Me.LOCAL_ARR_CARRIER_CD.SelectedValue = LOCAL_ARR_FLIGHT.Substring(0, 2)
                Me.LOCAL_ARR_FLIGHT_NO.Text = LOCAL_ARR_FLIGHT.Substring(2, LOCAL_ARR_FLIGHT.Length - 2)
            End If

            If Not LOCAL_DEP_FLIGHT.Equals("") AndAlso 2 < LOCAL_DEP_FLIGHT.Length Then
                Me.LOCAL_DEP_CARRIER_CD.SelectedValue = LOCAL_DEP_FLIGHT.Substring(0, 2)
                Me.LOCAL_DEP_FLIGHT_NO.Text = LOCAL_DEP_FLIGHT.Substring(2, LOCAL_DEP_FLIGHT.Length - 2)
            End If
        End If

        If TICKET_FLG Then
            Me.DEP_DATEPanel.Visible = False
            Me.RET_DATEPanel.Visible = False
        End If

        If PACKAGE_FLG Then
            Me.DEP_DATEPanel.Visible = False
            Me.RET_DATEPanel.Visible = False
        End If

        Select Case OVERSEAS_DOMESTIC_KBN
            Case "02" '国内
                Me.DEP_DATEPanel.Visible = False
                Me.RET_DATEPanel.Visible = False
        End Select


        Dim _MIN_DATE As Date = "1900/01/01"
        Dim _MAX_DATE As Date = "1900/01/01"

        'ホテル
        If dsItinerary.RES_HOTEL.Rows.Count > 0 Then
            Dim CHECKIN As Date = dsItinerary.RES_HOTEL.Rows(0)("CHECKIN")
            Dim CHECKOUT As Date = dsItinerary.RES_HOTEL.Rows(0)("CHECKOUT")
            Dim VENDER_KBN As String = SetRRValue.setNothingValueWeb(dsItinerary.RES_HOTEL.Rows(0)("VENDER_KBN"))
            If SetRRValue.setDispDate(_MIN_DATE).Equals("") Or _MIN_DATE > CHECKIN Then
                _MIN_DATE = CHECKIN
            End If
            If SetRRValue.setDispDate(_MAX_DATE).Equals("") Or _MAX_DATE < CHECKOUT Then
                _MAX_DATE = CHECKOUT
            End If

            Select Case VENDER_KBN
                Case "54"
                    Me.INPUT_MAIN_SEXPanel.Visible = True
                    Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible = True
                    Me.INPUT_MAIN_NAME_KANAPanel.Visible = True
            End Select

        End If

        'オプション
        If dsItinerary.RES_OPTION.Rows.Count > 0 Then
            Dim SAIKO_DATE_MIN As Date = SetRRValue.setDate(dsItinerary.RES_OPTION.Compute("MIN(DATE)", ""))
            Dim SAIKO_DATE_MAX As Date = SetRRValue.setDate(dsItinerary.RES_OPTION.Compute("MAX(DATE)", ""))

            SAIKO_DATE_MIN = SAIKO_DATE_MIN.ToString("yyyy/MM/dd")
            SAIKO_DATE_MAX = SAIKO_DATE_MAX.ToString("yyyy/MM/dd")

            If SetRRValue.setDispDate(_MIN_DATE.ToString("yyyy/MM/dd")).Equals("") Or _MIN_DATE > SAIKO_DATE_MIN Then
                _MIN_DATE = SAIKO_DATE_MIN
            End If

            If SetRRValue.setDispDate(_MAX_DATE.ToString("yyyy/MM/dd")).Equals("") Or _MAX_DATE < SAIKO_DATE_MAX Then
                _MAX_DATE = SAIKO_DATE_MAX
            End If

        End If

        If _MIN_DATE.Year = "1900" Then
            Me.MIN_DATE.Value = "0d"
        Else
            Me.MIN_DATE.Value = _MIN_DATE.Subtract(Today).Days + 1 & "d"
        End If

        If _MAX_DATE.Year = "1900" Then
            Me.MAX_DATE.Value = ""
        Else
            Me.MAX_DATE.Value = _MAX_DATE.Subtract(Today).Days - 1 & "d"
        End If

        '個人情報取り扱いについて
        Dim dsRegulation As New DataSet
        dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "agreement007", lang)

        If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
            Dim URL As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("URL")
            Dim CONTENTS As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
            If Not URL.Equals("") Then
                Me.REGULATION_LINKLinkButton.HRef = URL
                Me.REGULATION_LINKLinkButton.Visible = True
                Me.REGULATION_DEFAULTPanel.Visible = False
                Me.REGULATIONPanel.Style.Add("background-color", "#f0f0f0")
            Else
                Me.REGULATION_LINKLinkButton.Visible = False
                Me.REGULATION_DEFAULTPanel.Visible = True
                Me.regulation.InnerHtml = CONTENTS
                Me.REGULATIONLabel.Text = CONTENTS
            End If
        End If

        dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "addregulation", lang)

        If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
            Dim CONTENTS As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
            Me.AddRegulation.Text = CStr(CONTENTS).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
        End If

        If dsUser Is Nothing Then
            Me.REGULATIONPanel.Visible = True
        Else
            If Me.RT_CD.Value.Equals("WOW") Then
                Me.AgreeCheckPanel.Visible = True
                Me.REGULATIONPanel.Visible = True
                Me.REGULATION_DEFAULTPanel.Visible = False
            Else
                Me.REGULATIONPanel.Visible = False
            End If

        End If
        If Me.RT_CD.Value.Equals("WOW") Then
            Me.AgreeMessagePanel.Visible = False
        End If

        Try
            If Not Session("Affiliate" & Me.RT_CD.Value & Me.S_CD.Value) Is Nothing Then
                Dim dsAffiliate As TriphooCMSAPI.AffiliateDataSet = Session("Affiliate" & Me.RT_CD.Value & Me.S_CD.Value)
                If dsAffiliate.DETAIL_RES.Rows.Count > 0 Then
                    Dim MEMBER_FLG As Boolean = dsAffiliate.DETAIL_RES.Rows(0)("MEMBER_FLG")
                    If MEMBER_FLG Then
                        Me.COUPON_CDPanel.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")
        Select Case MEMBER_ADD_KBN
            Case "3", "4"
                Me.PORTAL_RES_NOPanel.Visible = True
                Me.RES_CONFIRMATION_TOPanel.Visible = True

                Me.INPUT1_MAIN_ADDRESSPanel.Visible = False
                Me.LABEL_MAIN_SURNAME_KANJIPanel.Visible = False
                Me.LABEL_MAIN_SURNAME_ROMANPanel.Visible = False
                Me.LABEL_MAIN_SEXPanel.Visible = False
                Me.LABEL_MAIN_BIRTHPanel.Visible = False
                Me.LABEL_MAIN_ADDRESSPanel.Visible = False

                'Me.GET_CLIENT_INFOLinkButton.Visible = True
            Case "5"
                Me.INPUT_MAIN_SEXPanel.Visible = False
                Me.INPUT_MAIN_BIRTHPanel.Visible = False
                Me.LABEL_MAIN_BIRTHPanel.Visible = False
                Me.LABEL_MAIN_ADDRESSPanel.Visible = False
                Me.PARTNERSPanel.Visible = True

                'Dim COMPANY_CLIENT_CD As String = dsUser.Tables("CLIENT_RES").Rows(0)("COMPANY_CLIENT_CD")
                'If COMPANY_CLIENT_CD.Equals(Me.RT_CD.Value) Then
                '    Me.GET_CLIENT_INFO1LinkButton.Visible = True
                '    Me.GET_CLIENT_INFO3LinkButton.Visible = True
                'End If

        End Select

        Dim MAIL_MAGAZINE_FLG As Boolean = SetRRValue.setBoolean(dsRTUser.M137_RT_SITE_CD.Rows(0)("MAIL_MAGAZINE_FLG"))
        Me.MAIL_MAGAZINE_FLGPanel.Visible = MAIL_MAGAZINE_FLG

        Select Case Me.RT_CD.Value
            Case "RT40"
                Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
                Me.LABEL_MAIN_SURNAME_ROMANPanel.Visible = True
            Case "A0027", "WOW"
                Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible = True
                Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
                Me.INPUT_MAIN_NAME_KANAPanel.Visible = True
                Me.LABEL_MAIN_SURNAME_KANAPanel.Visible = True
            Case "TPI"
                Me.PORTAL_RES_NOPanel.Visible = False
            Case "KIC"
                Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible = True
                Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
                If TICKET_FLG And Not HOTEL_FLG And Not OPTION_FLG And Not PACKAGE_FLG Then
                    'KICかつAOの場合、備考は非表示
                    Me.RemarksPanel.Visible = False
                End If
            Case "UAS"
                If Me.S_CD.Value.Equals("01") Then
                    Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
                End If
            Case "A0114"
                If Me.S_CD.Value.Equals("04") Then
                    Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible = PASSENGER_NAME_KANJI_DISP_FLG
                    Me.INPUT_MAIN_NAME_ROMANPanel.Visible = PASSENGER_NAME_ROMAN_DISP_FLG
                    Me.INPUT_MAIN_NAME_KANAPanel.Visible = PASSENGER_NAME_KANA_DISP_FLG
                Else
                    Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible = True
                    Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
                End If
            Case "RT01", "ATR"
                Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible = PASSENGER_NAME_KANJI_DISP_FLG
                Me.INPUT_MAIN_NAME_ROMANPanel.Visible = PASSENGER_NAME_ROMAN_DISP_FLG
                Me.INPUT_MAIN_NAME_KANAPanel.Visible = PASSENGER_NAME_KANA_DISP_FLG

                Me.INPUT_MAIN_SEXPanel.Visible = True
                Me.INPUT_MAIN_BIRTHPanel.Visible = True
            Case Else
                Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible = True
                Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
        End Select

        If MAIN_PERSON_ONLY Then
            Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
            Me.LABEL_MAIN_SURNAME_ROMANPanel.Visible = True
        End If

        If dsItinerary.PAGE_05.Rows.Count > 0 Then
            Dim GDS_KBN As String = dsItinerary.PAGE_05.Rows(0)("GDS_KBN")
            If GDS_KBN.Equals("22") Then
                Me.INPUT_MAIN_NAME_ROMANPanel.Visible = True
            End If
        End If

        If Not lang.Equals("1") Then
            Me.INPUT_MAIN_NAME_ROMANPanel.Visible = False
            Me.LABEL_MAIN_SURNAME_ROMANPanel.Visible = False
            Me.INPUT_MAIN_NAME_KANAPanel.Visible = False
            Me.LABEL_MAIN_SURNAME_KANAPanel.Visible = False
        End If


        Select Case Me.RT_CD.Value & Me.S_CD.Value
            Case "TWC02", "TWC03"

                Dim dsAgency As DataSet = Session("Agency" & Me.RT_CD.Value & Me.S_CD.Value)

                If Not dsAgency Is Nothing AndAlso dsAgency.Tables.Contains("REQ_DATA") AndAlso 0 < dsAgency.Tables("REQ_DATA").Rows.Count Then
                    Me.AGENCY_NAME.Text = dsAgency.Tables("REQ_DATA").Rows(0)("s_Agent_Br_K")
                    Me.AGENCY_ADDRESS.Text = "〒" & dsAgency.Tables("REQ_DATA").Rows(0)("s_Zip_CD") & " " &
                                     dsAgency.Tables("REQ_DATA").Rows(0)("s_State") & " " &
                                     dsAgency.Tables("REQ_DATA").Rows(0)("s_Address_1") & " " &
                                     dsAgency.Tables("REQ_DATA").Rows(0)("s_Address_2")
                    Me.AGENCY_TEL_NO.Text = dsAgency.Tables("REQ_DATA").Rows(0)("s_Tel_Main")
                    Me.AGENCY_FAX_NO.Text = dsAgency.Tables("REQ_DATA").Rows(0)("s_Fax_Main")
                    Me.AGENCYPanel.Visible = True
                End If
        End Select


    End Sub
#End Region

#Region "BindData"
    Private Sub BindData(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        Me.INPUT_MAIN_BIRTH_MM.DataSource = SetValue.setBIRTH_MM(True)
        Me.INPUT_MAIN_BIRTH_MM.DataTextField = "TEXT"
        Me.INPUT_MAIN_BIRTH_MM.DataValueField = "VALUE"
        Me.INPUT_MAIN_BIRTH_MM.DataBind()

        Me.INPUT_MAIN_BIRTH_DD.DataSource = SetValue.setBIRTH_DD(True)
        Me.INPUT_MAIN_BIRTH_DD.DataTextField = "TEXT"
        Me.INPUT_MAIN_BIRTH_DD.DataValueField = "VALUE"
        Me.INPUT_MAIN_BIRTH_DD.DataBind()

        Me.INPUT_MAIN_PREFECTURE.DataSource = SetRRKbn.setPREFECTURE("", True, lang)
        Me.INPUT_MAIN_PREFECTURE.DataTextField = "TEXT"
        Me.INPUT_MAIN_PREFECTURE.DataValueField = "VALUE"
        Me.INPUT_MAIN_PREFECTURE.DataBind()

        Me.INPUT1_MAIN_PREFECTURE.DataSource = SetRRKbn.setPREFECTURE("", True, lang)
        Me.INPUT1_MAIN_PREFECTURE.DataTextField = "TEXT"
        Me.INPUT1_MAIN_PREFECTURE.DataValueField = "VALUE"
        Me.INPUT1_MAIN_PREFECTURE.DataBind()

        Dim dsM074_RT_WEB_TICKET_CARRIER As DataSet = B2CAPIClient.SelectM074_RT_WEB_TICKET_CARRIER_FOR_WEB_BY_SITE_CDGateway(Me.RT_CD.Value, Me.S_CD.Value, "", lang, dsB2CUser)

        Me.LOCAL_ARR_CARRIER_CD.DataSource = dsM074_RT_WEB_TICKET_CARRIER.Tables(0).Copy
        Me.LOCAL_ARR_CARRIER_CD.DataTextField = "GOING_CARRIER_NM"
        Me.LOCAL_ARR_CARRIER_CD.DataValueField = "GOING_CARRIER_CD"
        Me.LOCAL_ARR_CARRIER_CD.DataBind()

        Me.LOCAL_DEP_CARRIER_CD.DataSource = dsM074_RT_WEB_TICKET_CARRIER.Tables(0).Copy
        Me.LOCAL_DEP_CARRIER_CD.DataTextField = "GOING_CARRIER_NM"
        Me.LOCAL_DEP_CARRIER_CD.DataValueField = "GOING_CARRIER_CD"
        Me.LOCAL_DEP_CARRIER_CD.DataBind()



    End Sub
#End Region

#Region "getPage"
    Private Sub getPage(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        Dim CLIENT_ADDRESS_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("CLIENT_ADDRESS_KBN")
        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")
        Dim BACK_OFFICE_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("BACK_OFFICE_KBN")

        If dsItinerary.PAGE_03.Rows.Count > 0 Then
            Me.DEP_DATE.Text = SetRRValue.setDispDate(dsItinerary.PAGE_03.Rows(0)("DEP_TIME")) '            日本出発日
            Me.RET_DATE.Text = SetRRValue.setDispDate(dsItinerary.PAGE_03.Rows(0)("RET_TIME")) '            日本帰着日
            Me.CLIENT_COMMENT.Text = SetRRValue.setNothingValueWeb(dsItinerary.PAGE_03.Rows(0)("CLIENT_COMMENT")) '顧客コメント

            Me.PORTAL_RES_NOTextBox.Text = SetRRValue.setNothingValueWeb(dsItinerary.PAGE_03.Rows(0)("AGT_RES_NO")) 'AGENT_RES_NO
            Me.RES_CONFIRMATION_TO.Text = SetRRValue.setNothingValueWeb(dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_NAME")) 'RES_CONFIRMATION_TO

            Me.PARTNERS_AGENT_RES_NO.Text = SetRRValue.setNothingValueWeb(dsItinerary.PAGE_03.Rows(0)("AGT_RES_NO")) 'AGENT_RES_NO
            Me.PARTNERS_EMP_NM.Text = SetRRValue.setNothingValueWeb(dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_NAME")) 'RES_CONFIRMATION_TO
            Me.PARTNERS_TEL_NO.Text = SetRRValue.setNothingValueWeb(dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_TEL"))
        End If

        If SetRRValue.setBoolean(dsItinerary.WEB_TRANSACTION.Rows(0)("GOTO_FLG")) Then
            CLIENT_ADDRESS_KBN = "1"
        End If

        Select Case CLIENT_ADDRESS_KBN
            Case "1"
                Me.LABEL_MAIN_ADDRESSPanel.Visible = False
                Me.INPUT1_MAIN_ADDRESSPanel.Visible = True
                Me.INPUT_MAIN_ADDRESSPanel.Visible = True
        End Select

        If 0 < dsItinerary.PAGE_05.Rows.Count Then
            Dim GDS_KBN As String = dsItinerary.PAGE_05.Rows(0)("GDS_KBN")

            Select Case GDS_KBN
                Case "44"
                    Me.INPUT_MAIN_TEL_NOPanel.Visible = False
                    Me.INPUT_MAIN_TEL_NO_SEPARATEPanel.Visible = True
            End Select

        End If

        '代表者情報
        If Not dsUser Is Nothing AndAlso dsUser.Tables("CLIENT_RES").Rows.Count > 0 Then
            Select Case MEMBER_ADD_KBN
                Case "3", "4"
                    Me.MAIN_PERSON_LABELPanel.Visible = True
                    Me.MAIN_PERSON_LABEL2Panel.Visible = True
                    Me.MAIN_PERSON_INPUTPanel.Visible = False
                    Me.MAIN_E_MAIL.Visible = False
                    Me.INPUT_MAIN_E_MAIL_B2B.Visible = True
                    Me.LABEL_0038.Text = LABEL_0159
                    Me.LABEL_0039.Text = LABEL_0160
                    Me.LABEL_MAIN_SEXPanel.Visible = False
                    Me.LABEL_MAIN_BIRTHPanel.Visible = False
                    Me.MAIN_PERSON_CHANGELinkButton.Visible = False
                Case "5"
                    Me.PARTNERS_E_MAIL.Text = SetRRValue.setNothingValueWeb(dsUser.Tables("CLIENT_RES").Rows(0)("E_MAIL"))

                    Dim rr() As DataRow = dsUser.Tables("CLIENT_TEL_RES").Select("TEL_KBN='01'")
                    If rr.Length > 0 Then
                        Me.PARTNERS_TEL_NO.Text = SetRRValue.setNothingValueWeb(rr(0)("NO"))
                    End If

                    Me.MAIN_PERSON_LABELPanel.Visible = False
                    Me.MAIN_PERSON_LABEL2Panel.Visible = False
                    Me.MAIN_PERSON_INPUTPanel.Visible = False

                    If dsItinerary.M019_CLIENT.Rows.Count = 0 Then
                        Exit Sub
                    End If

                Case Else

                    Select Case Me.RT_CD.Value
                        Case "ATR"
                            Me.MAIN_PERSON_LABELPanel.Visible = False
                            Me.MAIN_PERSON_LABEL2Panel.Visible = False
                            Me.MAIN_PERSON_INPUTPanel.Visible = True

                            Me.INPUT_MAIN_E_MAILLabel.Visible = True
                            Me.INPUT_MAIN_E_MAIL.Visible = False
                            Me.INPUT_MAIN_E_MAIL_CONFPanel.Visible = False

                        Case Else
                            Me.MAIN_PERSON_LABELPanel.Visible = True
                            Me.MAIN_PERSON_LABEL2Panel.Visible = True
                            Me.MAIN_PERSON_INPUTPanel.Visible = False
                    End Select


            End Select
        Else
            Me.MAIN_PERSON_LABELPanel.Visible = False
            Me.MAIN_PERSON_LABEL2Panel.Visible = False
            Me.MAIN_PERSON_INPUTPanel.Visible = True
            If dsItinerary.M019_CLIENT.Rows.Count = 0 Then
                Exit Sub
            End If
        End If

        Select Case BACK_OFFICE_KBN
            Case "7"
                Me.MAIN_PERSON_LABELPanel.Visible = False
                Me.MAIN_PERSON_LABEL2Panel.Visible = False
                Me.MAIN_PERSON_INPUTPanel.Visible = True
        End Select

        Dim SEX_KBN As String = SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("SEX_KBN"))
        Dim TEL_NO As String = ""
        Dim ZIPCODE As String = ""
        Dim ZIPCODE_NM As String = ""
        Dim PREFECTURE As String = ""
        Dim PREFECTURE_NM As String = ""
        Dim ADDRESS As String = ""
        Dim BIRTH_YYYYMMDD As String = ""
        Dim BIRTH_YYYY As String = ""
        Dim BIRTH_MM As String = ""
        Dim BIRTH_DD As String = ""

        Try
            Dim BIRTH As DateTime = SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("BIRTH"))

            If BIRTH.Year.Equals("1900") Then
                Throw New Exception
            End If

            BIRTH_YYYY = BIRTH.Year
            BIRTH_MM = BIRTH.Month
            BIRTH_DD = BIRTH.Day
            BIRTH_YYYYMMDD = BIRTH.Year & "/" & BIRTH.Month & "/" & BIRTH.Day
        Catch ex As Exception

        End Try

        Dim r() As DataRow = dsItinerary.M023_CLIENT_TEL.Select("TEL_KBN='01'")
        If r.Length > 0 Then
            TEL_NO = SetRRValue.setNothingValueWeb(r(0)("NO"))
        End If

        r = dsItinerary.M021_CLIENT_ADDRESS.Select("ADDRESS_KBN='01'")
        If r.Length > 0 Then
            ZIPCODE = SetRRValue.setNothingValueWeb(r(0)("ZIPCODE"))
            PREFECTURE = SetRRValue.setNothingValueWeb(r(0)("PREFECTURE"))

            Dim dtPREFECTURE As DataTable = SetRRKbn.setPREFECTURE("", True, "1")
            Dim rPREFECTURE() As DataRow = dtPREFECTURE.Select("VALUE='" & PREFECTURE & "'")
            If rPREFECTURE.Length > 0 Then
                PREFECTURE_NM = rPREFECTURE(0)("TEXT")
            End If

            ADDRESS = SetRRValue.setNothingValueWeb(r(0)("ADDRESS1")) & SetRRValue.setNothingValueWeb(r(0)("ADDRESS2"))
            If Not (ZIPCODE.Equals("") AndAlso PREFECTURE.Equals("") AndAlso PREFECTURE_NM.Equals("") AndAlso ADDRESS.Equals("")) Then
                Me.INPUT1_MAIN_ADDRESSPanel.Visible = False
                Me.LABEL_MAIN_ADDRESSPanel.Visible = True
            End If

            If ZIPCODE.Length = 7 Then
                ZIPCODE_NM = ZIPCODE.Substring(0, 3) & "-" & ZIPCODE.Substring(3, 4)
            End If

        End If

        Me.INPUT_MAIN_E_MAILLabel.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("E_MAIL")))
        Me.INPUT_MAIN_E_MAIL.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("E_MAIL")))
        Me.INPUT_MAIN_E_MAIL_B2B.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("E_MAIL")))
        Me.INPUT_MAIN_SURNAME_KANJI.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI")))
        Me.INPUT_MAIN_NAME_KANJI.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI")))
        Me.INPUT_MAIN_SURNAME_KANA.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANA")))
        Me.INPUT_MAIN_NAME_KANA.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("NAME_KANA")))
        Me.INPUT_MAIN_SURNAME_ROMAN.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("SURNAME_ROMAN")))
        Me.INPUT_MAIN_NAME_ROMAN.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("NAME_ROMAN")))
        Me.INPUT_MAIN_BIRTH_YYYY.Text = CommonUtil.Xss(BIRTH_YYYY)
        Me.INPUT_MAIN_BIRTH_MM.SelectedValue = CommonUtil.Xss(BIRTH_MM)
        Me.INPUT_MAIN_BIRTH_DD.SelectedValue = CommonUtil.Xss(BIRTH_DD)
        Me.INPUT_MAIN_BIRTH_LABEL.Text = BIRTH_YYYYMMDD
        Me.INPUT_MAIN_TEL_NO.Text = CommonUtil.Xss(TEL_NO)
        Me.INPUT_MAIN_ZIPCODE.Text = CommonUtil.Xss(ZIPCODE)
        Me.INPUT_MAIN_PREFECTURE.SelectedValue = CommonUtil.Xss(PREFECTURE)
        Me.INPUT_MAIN_ADDRESS.Text = CommonUtil.Xss(ADDRESS)
        Me.MAIN_E_MAIL.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("E_MAIL")))
        Me.MAIN_SURNAME_KANJI.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI")))
        Me.MAIN_NAME_KANJI.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI")))
        Me.MAIN_SURNAME_ROMAN.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("SURNAME_ROMAN")))
        Me.MAIN_NAME_ROMAN.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("NAME_ROMAN")))
        Me.MAIN_SURNAME_KANA.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANA")))
        Me.MAIN_NAME_KANA.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("NAME_KANA")))
        Me.MAIN_BIRTH.Text = CommonUtil.Xss(BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD)
        Me.MAIN_ZIPCODE.Text = CommonUtil.Xss(ZIPCODE_NM)
        Me.MAIN_PREFECTURE.Text = CommonUtil.Xss(PREFECTURE_NM)
        Me.MAIN_ADDRESS.Text = CommonUtil.Xss(ADDRESS)
        Me.MAIN_TEL_NO.Text = CommonUtil.Xss(TEL_NO)

        Dim MAIL_MAGAZINE_FLG As Boolean = SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("MAIL_MAGAZINE_FLG"))

        If MAIL_MAGAZINE_FLG Then
            Me.MAIL_MAGAZINE_FLG_01.Checked = True
            Me.MAIL_MAGAZINE_FLG_02.Checked = False
        Else
            Me.MAIL_MAGAZINE_FLG_01.Checked = False
            Me.MAIL_MAGAZINE_FLG_02.Checked = True
        End If

        Select Case SEX_KBN
            Case "01"
                Me.INPUT_MAIN_MAN.Checked = True
                Me.MAIN_SEX_FLG.Text = LABEL_0117
            Case "02"
                Me.INPUT_MAIN_WOMAN.Checked = True
                Me.MAIN_SEX_FLG.Text = LABEL_0118
        End Select

        If MEMBER_ADD_KBN.Equals("5") Then
            Me.PARTNERS_CLIENT_CD.Text = CommonUtil.Xss(SetRRValue.setNothingValueWeb(dsItinerary.M019_CLIENT.Rows(0)("ACCOUNT_CD")))
        End If

        Select Case Me.RT_CD.Value
            Case "ATR"

                If Not Me.INPUT_MAIN_TEL_NO.Text.Equals("") Then
                    Me.INPUT_MAIN_TEL_NO.ReadOnly = True
                End If

                If Not Me.INPUT_MAIN_ZIPCODE.Text.Equals("") Then
                    Me.INPUT_MAIN_ZIPCODE.ReadOnly = True
                    Me.INPUT_MAIN_ZIPCODE_MESSAGE.Visible = False
                End If

                If Not Me.INPUT_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                    Me.INPUT_MAIN_PREFECTURE.Enabled = False
                End If

                If Not Me.INPUT_MAIN_ADDRESS.Text.Equals("") Then
                    Me.INPUT_MAIN_ADDRESS.ReadOnly = True
                End If

                If Me.INPUT_MAIN_MAN.Checked Or Me.INPUT_MAIN_WOMAN.Checked Then
                    Me.INPUT_MAIN_MAN.Visible = False
                    Me.INPUT_MAIN_WOMAN.Visible = False

                    Me.INPUT_MAIN_SEX_FLG_LABEL.Visible = True
                    Me.INPUT_MAIN_SEX_FLG_LABEL.Text = Me.MAIN_SEX_FLG.Text

                End If

                If Not BIRTH_YYYYMMDD.Equals("") AndAlso Not BIRTH_YYYYMMDD.StartsWith("1900") Then
                    Me.INPUT_MAIN_BIRTH_YYYY.Visible = False
                    Me.INPUT_MAIN_BIRTH_MM.Visible = False
                    Me.INPUT_MAIN_BIRTH_DD.Visible = False
                    Me.INPUT_MAIN_BIRTH_LABEL.Text = BIRTH_YYYYMMDD
                    Me.INPUT_MAIN_BIRTH_LABEL.Visible = True
                End If

                If Not Me.INPUT_MAIN_SURNAME_ROMAN.Text.Equals("") Then
                    Me.INPUT_MAIN_SURNAME_ROMAN.ReadOnly = True
                End If

                If Not Me.INPUT_MAIN_NAME_ROMAN.Text.Equals("") Then
                    Me.INPUT_MAIN_NAME_ROMAN.ReadOnly = True
                End If

                If Not Me.INPUT_MAIN_SURNAME_KANA.Text.Equals("") Then
                    Me.INPUT_MAIN_SURNAME_KANA.ReadOnly = True
                End If

                If Not Me.INPUT_MAIN_NAME_KANA.Text.Equals("") Then
                    Me.INPUT_MAIN_NAME_KANA.ReadOnly = True
                End If

                If Not Me.INPUT_MAIN_SURNAME_KANJI.Text.Equals("") Then
                    Me.INPUT_MAIN_SURNAME_KANJI.ReadOnly = True
                End If

                If Not Me.INPUT_MAIN_NAME_KANJI.Text.Equals("") Then
                    Me.INPUT_MAIN_NAME_KANJI.ReadOnly = True
                End If

        End Select

        If Me.INPUT_MAIN_TEL_NO_SEPARATEPanel.Visible Then

            Dim SEPARATE_TEL_NO As String = Me.INPUT_MAIN_TEL_NO.Text

            If Not SEPARATE_TEL_NO.Equals("") AndAlso Not SEPARATE_TEL_NO.Contains("-") Then
                Dim conv As New ConvertUtil()
                SEPARATE_TEL_NO = conv.FormatPhoneNumber(SEPARATE_TEL_NO)
            End If

            Dim ss() As String = SEPARATE_TEL_NO.Split("-")

            If ss.Length = 3 Then

                Me.INPUT_MAIN_TEL_NO_1.Text = ss(0)
                Me.INPUT_MAIN_TEL_NO_2.Text = ss(1)
                Me.INPUT_MAIN_TEL_NO_3.Text = ss(2)

                Me.MAIN_TEL_NO.Text = ss(0) & "-" & ss(1) & "-" & ss(2)
                Me.INPUT_MAIN_TEL_NO.Text = ss(0) & "-" & ss(1) & "-" & ss(2)

            End If

        End If

    End Sub
#End Region

#Region "setPage"
    Private Sub setPage(ByRef dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")

        Dim DEP_DATE As String = Request.Item("DEP_DATE")
        Dim RET_DATE As String = Request.Item("RET_DATE")

        Dim MAIL_MAGAZINE_FLG As Boolean = False

        Dim DEP_TIME As String = ""
        Dim RET_TIME As String = ""

        Dim LOCAL_ARR_DATE As String = ""
        Dim LOCAL_DEP_DATE As String = ""

        Dim LOCAL_ARR_FLIGHT As String = ""
        Dim LOCAL_DEP_FLIGHT As String = ""

        If Not Me.DEP_DATEPanel.Visible Then
            DEP_DATE = SetRRValue.setDispDate(dsItinerary.PAGE_03.Rows(0)("DEP_TIME"))
        End If

        If Not Me.RET_DATEPanel.Visible Then
            RET_DATE = SetRRValue.setDispDate(dsItinerary.PAGE_03.Rows(0)("RET_TIME"))
        End If

        If Me.LOCAL_ARR_DATEPanel.Visible Then
            LOCAL_ARR_DATE = Request.Item("LOCAL_ARR_DATE") & " " & Me.LOCAL_ARR_TIME_HH.SelectedValue.PadLeft(2, "0") & ":" & Me.LOCAL_ARR_TIME_MM.SelectedValue.PadLeft(2, "0")
        End If

        If Me.LOCAL_DEP_DATEPanel.Visible Then
            LOCAL_DEP_DATE = Request.Item("LOCAL_DEP_DATE") & " " & Me.LOCAL_DEP_TIME_HH.SelectedValue.PadLeft(2, "0") & ":" & Me.LOCAL_DEP_TIME_MM.SelectedValue.PadLeft(2, "0")
        End If

        If Me.LOCAL_ARR_FLIGHTPanel.Visible Then
            LOCAL_ARR_FLIGHT = Me.LOCAL_ARR_CARRIER_CD.SelectedValue & Me.LOCAL_ARR_FLIGHT_NO.Text
        End If

        If Me.LOCAL_DEP_FLIGHTPanel.Visible Then
            LOCAL_DEP_FLIGHT = Me.LOCAL_DEP_CARRIER_CD.SelectedValue & Me.LOCAL_DEP_FLIGHT_NO.Text
        End If

        If Me.MAIL_MAGAZINE_FLGPanel.Visible Then
            If Me.MAIL_MAGAZINE_FLG_01.Checked Then
                MAIL_MAGAZINE_FLG = True
            End If
        End If

        Dim MAIN_TEL_NO As String = Me.INPUT_MAIN_TEL_NO.Text

        If Me.INPUT_MAIN_TEL_NO_SEPARATEPanel.Visible Then
            MAIN_TEL_NO = Me.INPUT_MAIN_TEL_NO_1.Text & "-" & Me.INPUT_MAIN_TEL_NO_2.Text & "-" & Me.INPUT_MAIN_TEL_NO_3.Text
        End If

        '顧客データ　アップデート
        If Me.MAIN_PERSON_INPUTPanel.Visible Then
            'INPUT_MAIN_PERSON

            'M019_CLIENT
            If dsItinerary.M019_CLIENT.Rows.Count <> 0 Then
                dsItinerary.M019_CLIENT.Rows(0)("E_MAIL") = CommonUtil.Xss(StrConv(Me.INPUT_MAIN_E_MAIL.Text, VbStrConv.Narrow))
                dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI") = CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_KANJI.Text)
                dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI") = CommonUtil.Xss(Me.INPUT_MAIN_NAME_KANJI.Text)
                dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANA") = CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_KANA.Text)
                dsItinerary.M019_CLIENT.Rows(0)("NAME_KANA") = CommonUtil.Xss(Me.INPUT_MAIN_NAME_KANA.Text)
                dsItinerary.M019_CLIENT.Rows(0)("SURNAME_ROMAN") = CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_ROMAN.Text.ToUpper)
                dsItinerary.M019_CLIENT.Rows(0)("NAME_ROMAN") = CommonUtil.Xss(Me.INPUT_MAIN_NAME_ROMAN.Text.ToUpper)
                dsItinerary.M019_CLIENT.Rows(0)("MAIL_MAGAZINE_FLG") = MAIL_MAGAZINE_FLG

                If Me.INPUT_MAIN_MAN.Checked Then
                    dsItinerary.M019_CLIENT.Rows(0)("SEX_KBN") = "01"
                ElseIf Me.INPUT_MAIN_WOMAN.Checked Then
                    dsItinerary.M019_CLIENT.Rows(0)("SEX_KBN") = "02"
                End If

                Try
                    Dim BIRTH As DateTime = Me.INPUT_MAIN_BIRTH_YYYY.Text & "/" &
                                            Me.INPUT_MAIN_BIRTH_MM.SelectedValue & "/" &
                                            Me.INPUT_MAIN_BIRTH_DD.SelectedValue

                    If BIRTH.Year.Equals("1900") Then
                        Throw New Exception
                    End If

                    dsItinerary.M019_CLIENT.Rows(0)("BIRTH") = CommonUtil.Xss(BIRTH.ToString("yyyy/MM/dd"))

                Catch ex As Exception

                End Try

            End If

            'M023_CLIENT_TEL
            If dsItinerary.M023_CLIENT_TEL.Rows.Count <> 0 Then

                Dim r() As DataRow = dsItinerary.M023_CLIENT_TEL.Select("TEL_KBN='01'")
                If r.Length <> 0 Then
                    r(0)("NO") = CommonUtil.Xss(MAIN_TEL_NO)
                Else
                    Dim rM023_CLIENT_TEL As TriphooRR097DataSet.M023_CLIENT_TELRow
                    rM023_CLIENT_TEL = dsItinerary.M023_CLIENT_TEL.NewM023_CLIENT_TELRow
                    rM023_CLIENT_TEL.RT_CD = RT_CD.Value
                    rM023_CLIENT_TEL.CLIENT_CD = ""
                    rM023_CLIENT_TEL.TEL_KBN = "01"
                    rM023_CLIENT_TEL.NO = CommonUtil.Xss(MAIN_TEL_NO)
                    rM023_CLIENT_TEL.REMARKS = ""
                    dsItinerary.M023_CLIENT_TEL.AddM023_CLIENT_TELRow(rM023_CLIENT_TEL)
                End If
            Else
                Dim rM023_CLIENT_TEL As TriphooRR097DataSet.M023_CLIENT_TELRow
                rM023_CLIENT_TEL = dsItinerary.M023_CLIENT_TEL.NewM023_CLIENT_TELRow
                rM023_CLIENT_TEL.RT_CD = RT_CD.Value
                rM023_CLIENT_TEL.CLIENT_CD = ""
                rM023_CLIENT_TEL.TEL_KBN = "01"
                rM023_CLIENT_TEL.NO = CommonUtil.Xss(MAIN_TEL_NO)
                rM023_CLIENT_TEL.REMARKS = ""
                dsItinerary.M023_CLIENT_TEL.AddM023_CLIENT_TELRow(rM023_CLIENT_TEL)
            End If

            'M021_CLIENT_ADDRESS
            If dsItinerary.M021_CLIENT_ADDRESS.Rows.Count <> 0 Then
                Dim r() As DataRow = dsItinerary.M021_CLIENT_ADDRESS.Select("ADDRESS_KBN='01'")

                Dim ZIPCODE As String = ""
                Dim PREFECTURE As String = ""
                Dim PREFECTURE_NM As String = ""
                Dim ADDRESS1 As String = ""

                If Not Me.INPUT_MAIN_ZIPCODE.Text.Equals("") Then
                    ZIPCODE = CommonUtil.Xss(Me.INPUT_MAIN_ZIPCODE.Text)
                End If

                If Not Me.INPUT_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                    PREFECTURE = CommonUtil.Xss(Me.INPUT_MAIN_PREFECTURE.SelectedValue)
                End If

                If Not Me.INPUT_MAIN_PREFECTURE.SelectedItem.Text.Equals("") Then
                    PREFECTURE_NM = CommonUtil.Xss(Me.INPUT_MAIN_PREFECTURE.SelectedItem.Text)
                End If

                If Not Me.INPUT_MAIN_ADDRESS.Text.Equals("") Then
                    ADDRESS1 = CommonUtil.Xss(Me.INPUT_MAIN_ADDRESS.Text)
                End If

                If r.Length <> 0 Then
                    r(0)("ZIPCODE") = ZIPCODE
                    r(0)("PREFECTURE") = PREFECTURE
                    r(0)("PREFECTURE_NM") = PREFECTURE_NM
                    r(0)("ADDRESS1") = ADDRESS1
                Else
                    Dim rM021_CLIENT_ADDRESS As TriphooRR097DataSet.M021_CLIENT_ADDRESSRow
                    rM021_CLIENT_ADDRESS = dsItinerary.M021_CLIENT_ADDRESS.NewM021_CLIENT_ADDRESSRow
                    rM021_CLIENT_ADDRESS.RT_CD = RT_CD.Value
                    rM021_CLIENT_ADDRESS.CLIENT_CD = ""
                    rM021_CLIENT_ADDRESS.ADDRESS_KBN = "01"
                    rM021_CLIENT_ADDRESS.ZIPCODE = ZIPCODE
                    rM021_CLIENT_ADDRESS.PREFECTURE = PREFECTURE
                    rM021_CLIENT_ADDRESS.ADDRESS1 = ADDRESS1
                    rM021_CLIENT_ADDRESS.ADDRESS2 = ""
                    rM021_CLIENT_ADDRESS.TEL_NO = ""
                    rM021_CLIENT_ADDRESS.FAX_NO = ""
                    rM021_CLIENT_ADDRESS.EDIT_TIME = Now
                    rM021_CLIENT_ADDRESS.EDIT_RT_CD = RT_CD.Value
                    rM021_CLIENT_ADDRESS.EDIT_EMP_CD = "WEB"
                    rM021_CLIENT_ADDRESS.ADDRESS_KBN_NM = ""
                    rM021_CLIENT_ADDRESS.PREFECTURE_NM = PREFECTURE_NM
                    dsItinerary.M021_CLIENT_ADDRESS.AddM021_CLIENT_ADDRESSRow(rM021_CLIENT_ADDRESS)

                End If
            Else
                Dim ZIPCODE As String = ""
                Dim PREFECTURE As String = ""
                Dim PREFECTURE_NM As String = ""
                Dim ADDRESS1 As String = ""

                If Not Me.INPUT_MAIN_ZIPCODE.Text.Equals("") Then
                    ZIPCODE = CommonUtil.Xss(Me.INPUT_MAIN_ZIPCODE.Text)
                End If

                If Not Me.INPUT_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                    PREFECTURE = CommonUtil.Xss(Me.INPUT_MAIN_PREFECTURE.SelectedValue)
                End If

                If Not Me.INPUT_MAIN_PREFECTURE.SelectedItem.Text.Equals("") Then
                    PREFECTURE_NM = CommonUtil.Xss(Me.INPUT_MAIN_PREFECTURE.SelectedItem.Text)
                End If

                If Not Me.INPUT_MAIN_ADDRESS.Text.Equals("") Then
                    ADDRESS1 = CommonUtil.Xss(Me.INPUT_MAIN_ADDRESS.Text)
                End If
                Dim rM021_CLIENT_ADDRESS As TriphooRR097DataSet.M021_CLIENT_ADDRESSRow
                rM021_CLIENT_ADDRESS = dsItinerary.M021_CLIENT_ADDRESS.NewM021_CLIENT_ADDRESSRow
                rM021_CLIENT_ADDRESS.RT_CD = RT_CD.Value
                rM021_CLIENT_ADDRESS.CLIENT_CD = ""
                rM021_CLIENT_ADDRESS.ADDRESS_KBN = "01"
                rM021_CLIENT_ADDRESS.ZIPCODE = ZIPCODE
                rM021_CLIENT_ADDRESS.PREFECTURE = PREFECTURE
                rM021_CLIENT_ADDRESS.ADDRESS1 = ADDRESS1
                rM021_CLIENT_ADDRESS.ADDRESS2 = ""
                rM021_CLIENT_ADDRESS.TEL_NO = ""
                rM021_CLIENT_ADDRESS.FAX_NO = ""
                rM021_CLIENT_ADDRESS.EDIT_TIME = Now
                rM021_CLIENT_ADDRESS.EDIT_RT_CD = RT_CD.Value
                rM021_CLIENT_ADDRESS.EDIT_EMP_CD = "WEB"
                rM021_CLIENT_ADDRESS.ADDRESS_KBN_NM = ""
                rM021_CLIENT_ADDRESS.PREFECTURE_NM = PREFECTURE_NM
                dsItinerary.M021_CLIENT_ADDRESS.AddM021_CLIENT_ADDRESSRow(rM021_CLIENT_ADDRESS)
            End If
        Else
            'INPUT1_MAIN_PERSON

            'M023_CLIENT_TEL
            If dsItinerary.M023_CLIENT_TEL.Rows.Count <> 0 Then

                Dim r() As DataRow = dsItinerary.M023_CLIENT_TEL.Select("TEL_KBN='01'")
                If r.Length <> 0 Then
                    r(0)("NO") = CommonUtil.Xss(MAIN_TEL_NO)
                Else
                    Dim rM023_CLIENT_TEL As TriphooRR097DataSet.M023_CLIENT_TELRow
                    rM023_CLIENT_TEL = dsItinerary.M023_CLIENT_TEL.NewM023_CLIENT_TELRow
                    rM023_CLIENT_TEL.RT_CD = RT_CD.Value
                    rM023_CLIENT_TEL.CLIENT_CD = ""
                    rM023_CLIENT_TEL.TEL_KBN = "01"
                    rM023_CLIENT_TEL.NO = CommonUtil.Xss(MAIN_TEL_NO)
                    rM023_CLIENT_TEL.REMARKS = ""
                    dsItinerary.M023_CLIENT_TEL.AddM023_CLIENT_TELRow(rM023_CLIENT_TEL)
                End If
            Else
                Dim rM023_CLIENT_TEL As TriphooRR097DataSet.M023_CLIENT_TELRow
                rM023_CLIENT_TEL = dsItinerary.M023_CLIENT_TEL.NewM023_CLIENT_TELRow
                rM023_CLIENT_TEL.RT_CD = RT_CD.Value
                rM023_CLIENT_TEL.CLIENT_CD = ""
                rM023_CLIENT_TEL.TEL_KBN = "01"
                rM023_CLIENT_TEL.NO = CommonUtil.Xss(MAIN_TEL_NO)
                rM023_CLIENT_TEL.REMARKS = ""
                dsItinerary.M023_CLIENT_TEL.AddM023_CLIENT_TELRow(rM023_CLIENT_TEL)
            End If

            'M021_CLIENT_ADDRESS
            If dsItinerary.M021_CLIENT_ADDRESS.Rows.Count <> 0 Then
                Dim r() As DataRow = dsItinerary.M021_CLIENT_ADDRESS.Select("ADDRESS_KBN='01'")
                If r.Length <> 0 Then
                    Dim ZIPCODE As String = SetRRValue.setNothingValueWeb(r(0)("ZIPCODE"))
                    Dim PREFECTURE As String = SetRRValue.setNothingValueWeb(r(0)("PREFECTURE"))
                    Dim PREFECTURE_NM As String = SetRRValue.setNothingValueWeb(r(0)("PREFECTURE_NM"))
                    Dim ADDRESS1 As String = SetRRValue.setNothingValueWeb(r(0)("ADDRESS1"))

                    If Not Me.INPUT1_MAIN_ZIPCODE.Text.Equals("") Then
                        ZIPCODE = CommonUtil.Xss(Me.INPUT1_MAIN_ZIPCODE.Text)
                    End If

                    If Not Me.INPUT1_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                        PREFECTURE = CommonUtil.Xss(Me.INPUT1_MAIN_PREFECTURE.SelectedValue)
                    End If

                    If Not Me.INPUT1_MAIN_PREFECTURE.SelectedItem.Text.Equals("") Then
                        PREFECTURE_NM = CommonUtil.Xss(Me.INPUT1_MAIN_PREFECTURE.SelectedItem.Text)
                    End If

                    If Not Me.INPUT1_MAIN_ADDRESS.Text.Equals("") Then
                        ADDRESS1 = CommonUtil.Xss(Me.INPUT1_MAIN_ADDRESS.Text)
                    End If
                    r(0)("ZIPCODE") = ZIPCODE
                    r(0)("PREFECTURE") = PREFECTURE
                    r(0)("PREFECTURE_NM") = PREFECTURE_NM
                    r(0)("ADDRESS1") = ADDRESS1
                Else
                    Dim ZIPCODE As String = ""
                    Dim PREFECTURE As String = ""
                    Dim PREFECTURE_NM As String = ""
                    Dim ADDRESS1 As String = ""

                    If Not Me.INPUT1_MAIN_ZIPCODE.Text.Equals("") Then
                        ZIPCODE = CommonUtil.Xss(Me.INPUT1_MAIN_ZIPCODE.Text)
                    End If

                    If Not Me.INPUT1_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                        PREFECTURE = CommonUtil.Xss(Me.INPUT1_MAIN_PREFECTURE.SelectedValue)
                    End If

                    If Not Me.INPUT1_MAIN_PREFECTURE.SelectedItem.Text.Equals("") Then
                        PREFECTURE_NM = CommonUtil.Xss(Me.INPUT1_MAIN_PREFECTURE.SelectedItem.Text)
                    End If

                    If Not Me.INPUT1_MAIN_ADDRESS.Text.Equals("") Then
                        ADDRESS1 = CommonUtil.Xss(Me.INPUT1_MAIN_ADDRESS.Text)
                    End If
                    Dim rM021_CLIENT_ADDRESS As TriphooRR097DataSet.M021_CLIENT_ADDRESSRow
                    rM021_CLIENT_ADDRESS = dsItinerary.M021_CLIENT_ADDRESS.NewM021_CLIENT_ADDRESSRow
                    rM021_CLIENT_ADDRESS.RT_CD = RT_CD.Value
                    rM021_CLIENT_ADDRESS.CLIENT_CD = ""
                    rM021_CLIENT_ADDRESS.ADDRESS_KBN = "01"
                    rM021_CLIENT_ADDRESS.ZIPCODE = ZIPCODE
                    rM021_CLIENT_ADDRESS.PREFECTURE = PREFECTURE
                    rM021_CLIENT_ADDRESS.ADDRESS1 = ADDRESS1
                    rM021_CLIENT_ADDRESS.ADDRESS2 = ""
                    rM021_CLIENT_ADDRESS.TEL_NO = ""
                    rM021_CLIENT_ADDRESS.FAX_NO = ""
                    rM021_CLIENT_ADDRESS.EDIT_TIME = Now
                    rM021_CLIENT_ADDRESS.EDIT_RT_CD = RT_CD.Value
                    rM021_CLIENT_ADDRESS.EDIT_EMP_CD = "WEB"
                    rM021_CLIENT_ADDRESS.ADDRESS_KBN_NM = ""
                    rM021_CLIENT_ADDRESS.PREFECTURE_NM = PREFECTURE_NM
                    dsItinerary.M021_CLIENT_ADDRESS.AddM021_CLIENT_ADDRESSRow(rM021_CLIENT_ADDRESS)

                End If
            Else
                Dim ZIPCODE As String = ""
                Dim PREFECTURE As String = ""
                Dim PREFECTURE_NM As String = ""
                Dim ADDRESS1 As String = ""

                If Not Me.INPUT1_MAIN_ZIPCODE.Text.Equals("") Then
                    ZIPCODE = CommonUtil.Xss(Me.INPUT1_MAIN_ZIPCODE.Text)
                End If

                If Not Me.INPUT1_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                    PREFECTURE = CommonUtil.Xss(Me.INPUT1_MAIN_PREFECTURE.SelectedValue)
                End If

                If Not Me.INPUT1_MAIN_PREFECTURE.SelectedItem.Text.Equals("") Then
                    PREFECTURE_NM = CommonUtil.Xss(Me.INPUT1_MAIN_PREFECTURE.SelectedItem.Text)
                End If

                If Not Me.INPUT1_MAIN_ADDRESS.Text.Equals("") Then
                    ADDRESS1 = CommonUtil.Xss(Me.INPUT1_MAIN_ADDRESS.Text)
                End If
                Dim rM021_CLIENT_ADDRESS As TriphooRR097DataSet.M021_CLIENT_ADDRESSRow
                rM021_CLIENT_ADDRESS = dsItinerary.M021_CLIENT_ADDRESS.NewM021_CLIENT_ADDRESSRow
                rM021_CLIENT_ADDRESS.RT_CD = RT_CD.Value
                rM021_CLIENT_ADDRESS.CLIENT_CD = ""
                rM021_CLIENT_ADDRESS.ADDRESS_KBN = "01"
                rM021_CLIENT_ADDRESS.ZIPCODE = ZIPCODE
                rM021_CLIENT_ADDRESS.PREFECTURE = PREFECTURE
                rM021_CLIENT_ADDRESS.ADDRESS1 = ADDRESS1
                rM021_CLIENT_ADDRESS.ADDRESS2 = ""
                rM021_CLIENT_ADDRESS.TEL_NO = ""
                rM021_CLIENT_ADDRESS.FAX_NO = ""
                rM021_CLIENT_ADDRESS.EDIT_TIME = Now
                rM021_CLIENT_ADDRESS.EDIT_RT_CD = RT_CD.Value
                rM021_CLIENT_ADDRESS.EDIT_EMP_CD = "WEB"
                rM021_CLIENT_ADDRESS.ADDRESS_KBN_NM = ""
                rM021_CLIENT_ADDRESS.PREFECTURE_NM = PREFECTURE_NM
                dsItinerary.M021_CLIENT_ADDRESS.AddM021_CLIENT_ADDRESSRow(rM021_CLIENT_ADDRESS)
            End If
        End If

        'PAGE_03
        dsItinerary.PAGE_03.Rows(0)("CLIENT_CD") = dsItinerary.M019_CLIENT.Rows(0)("CLIENT_CD")
        dsItinerary.PAGE_03.Rows(0)("CLIENT_NM") = CommonUtil.Xss(dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI")) & " " &
                                                       CommonUtil.Xss(dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI"))
        dsItinerary.PAGE_03.Rows(0)("DEP_TIME") = SetRRValue.setDate(DEP_DATE) '            日本出発日
        dsItinerary.PAGE_03.Rows(0)("RET_TIME") = SetRRValue.setDate(RET_DATE) '            日本帰着日
        dsItinerary.PAGE_03.Rows(0)("CLIENT_COMMENT") = CommonUtil.Xss(Me.CLIENT_COMMENT.Text) '顧客コメント

        Select Case MEMBER_ADD_KBN
            Case "3", "4"
                ' エージェン情報を設定
                If dsItinerary.M019_CLIENT.Rows.Count <> 0 Then
                    dsItinerary.M019_CLIENT.Rows(0)("E_MAIL") = CommonUtil.Xss(Me.INPUT_MAIN_E_MAIL_B2B.Text)
                End If
                dsItinerary.PAGE_03.Rows(0)("RES_CONFIRMATION_TO") = CommonUtil.Xss(Me.RES_CONFIRMATION_TO.Text) '予約担当者
                dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_NAME") = CommonUtil.Xss(Me.RES_CONFIRMATION_TO.Text) '予約担当者
                dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_E_MAIL") = CommonUtil.Xss(Me.INPUT_MAIN_E_MAIL_B2B.Text) '予約担当者E-mail
                dsItinerary.PAGE_03.Rows(0)("AGT_RES_NO") = CommonUtil.Xss(Me.PORTAL_RES_NOTextBox.Text) '御社予約番号

            Case "5"
                ' 提携先情報を設定
                If dsItinerary.M019_CLIENT.Rows.Count <> 0 Then
                    dsItinerary.M019_CLIENT.Rows(0)("E_MAIL") = CommonUtil.Xss(Me.PARTNERS_E_MAIL.Text)
                End If
                dsItinerary.M019_CLIENT.Rows(0)("ACCOUNT_CD") = CommonUtil.Xss(Me.PARTNERS_CLIENT_CD.Text)
                dsItinerary.PAGE_03.Rows(0)("B2B_CLIENT_CD") = dsUser.Tables("CLIENT_RES").Rows(0)("CLIENT_CD")
                dsItinerary.PAGE_03.Rows(0)("PORTAL_RES_NO") = CommonUtil.Xss(Me.PARTNERS_AGENT_RES_NO.Text)    '提携先情報　宴席番号
                dsItinerary.PAGE_03.Rows(0)("AGT_RES_NO") = CommonUtil.Xss(Me.PARTNERS_AGENT_RES_NO.Text)    '提携先情報　宴席番号
                dsItinerary.PAGE_03.Rows(0)("RES_CONFIRMATION_TO") = CommonUtil.Xss(Me.PARTNERS_EMP_NM.Text)  '提携先情報　担当者
                dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_NAME") = CommonUtil.Xss(Me.PARTNERS_EMP_NM.Text)    '提携先情報　担当者
                dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_E_MAIL") = CommonUtil.Xss(Me.PARTNERS_E_MAIL.Text)    '提携先情報　E-mail
                dsItinerary.PAGE_03.Rows(0)("AGT_CHARGE_TEL") = CommonUtil.Xss(Me.PARTNERS_TEL_NO.Text)    '提携先情報　電話番号
        End Select

        If Me.COUPON_CDPanel.Visible Then
            dsItinerary.PAGE_03.Rows(0)("CLIENT_COMMENT") += vbCrLf & vbCrLf & LABEL_0161 & vbCrLf & CommonUtil.Xss(Me.COUPON_CD.Text)
        End If

        If dsItinerary.WEB_TRANSACTION.Rows.Count > 0 Then
            dsItinerary.WEB_TRANSACTION.Rows(0)("COUPON_CD") = CommonUtil.Xss(Me.COUPON_CD.Text)
        End If

        '航空券

        'ホテル
        For Each o In dsItinerary.RES_HOTEL.Rows
            If CStr(o("JP_DEP_TIME")).StartsWith("1900") Then
                o("JP_DEP_TIME") = DEP_DATE
            End If
            If CStr(o("LOCAL_DEP_TIME")).StartsWith("1900") Then
                o("LOCAL_DEP_TIME") = RET_DATE
            End If

            Dim VENDER_KBN As String = o("VENDER_KBN")

            Select Case VENDER_KBN
                Case "54"

                    Dim XML_DATA As String = ""
                    XML_DATA += "<RES_DATA>"
                    If Me.INPUT_MAIN_MAN.Checked Then
                        XML_DATA += "<SEX>" & "01" & "</SEX>" ' 性別
                    ElseIf Me.INPUT_MAIN_WOMAN.Checked Then
                        XML_DATA += "<SEX>" & "02" & "</SEX>" ' 性別
                    End If
                    XML_DATA += "<SURNAME_KANJI>" & CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_KANJI.Text) & "</SURNAME_KANJI>" ' 名前・姓（漢字）
                    XML_DATA += "<NAME_KANJI>" & CommonUtil.Xss(Me.INPUT_MAIN_NAME_KANJI.Text) & "</NAME_KANJI>" ' 名前・名（漢字）
                    XML_DATA += "<SURNAME_KANA>" & CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_KANA.Text) & "</SURNAME_KANA>" ' 名前・姓 (カナ)
                    XML_DATA += "<NAME_KANA>" & CommonUtil.Xss(Me.INPUT_MAIN_NAME_KANA.Text) & "</NAME_KANA>" ' 名前・姓 (カナ)
                    XML_DATA += "<TEL_NO>" & MAIN_TEL_NO.Replace("-", "") & "</TEL_NO>" ' 電話番号
                    XML_DATA += "<E_MAIL>" & CommonUtil.Xss(StrConv(Me.INPUT_MAIN_E_MAIL.Text, VbStrConv.Narrow)) & "</E_MAIL>" ' メールアドレス
                    XML_DATA += "<ZIPCODE>" & CommonUtil.Xss(Me.INPUT_MAIN_ZIPCODE.Text).Replace("-", "") & "</ZIPCODE>" ' 郵便番号
                    XML_DATA += "<ADDRESS>" & CommonUtil.Xss(Me.INPUT_MAIN_PREFECTURE.SelectedItem.Text) & CommonUtil.Xss(Me.INPUT_MAIN_ADDRESS.Text) & "</ADDRESS>" ' 住所
                    XML_DATA += "</RES_DATA>"

                    dsItinerary.WEB_TRANSACTION.Rows(0)("HOTEL_XML") = XML_DATA

            End Select

        Next

        'ツアー

        'オプション
        For Each o In dsItinerary.RES_OPTION.Rows
            If Not DEP_DATE.Equals("") Then
                o("JP_DEP_TIME") = DEP_DATE
            End If

            If Not LOCAL_ARR_DATE.Equals("") Then
                o("LOCAL_ARR_TIME") = LOCAL_ARR_DATE
            End If

            If Not LOCAL_DEP_DATE.Equals("") Then
                o("LOCAL_DEP_TIME") = LOCAL_DEP_DATE
            End If

            If Not RET_DATE.Equals("") Then
                o("JP_ARR_TIME") = RET_DATE
            End If

            If Not LOCAL_ARR_FLIGHT.Equals("") Then
                o("LOCAL_ARR_FLIGHT") = LOCAL_ARR_FLIGHT
            End If

            If Not LOCAL_DEP_FLIGHT.Equals("") Then
                o("LOCAL_DEP_FLIGHT") = LOCAL_DEP_FLIGHT
            End If
        Next

        'dsItinerary.RES_TICKET_SEGMENT_PAX.Clear()
        dsItinerary.RES_ADD_SERVICE.Clear()

        Select Case Me.RT_CD.Value
            Case "ATR"
                Dim r() As DataRow = dsItinerary.PAGE_04.Select("MAIN_PERSON_FLG")

                If 0 < r.Length Then


                    Dim BIRTH_ As String = "1900/01/01"

                    Try
                        Dim BIRTH As DateTime = Me.INPUT_MAIN_BIRTH_YYYY.Text & "/" &
                                            Me.INPUT_MAIN_BIRTH_MM.SelectedValue & "/" &
                                            Me.INPUT_MAIN_BIRTH_DD.SelectedValue


                        BIRTH_ = CommonUtil.Xss(BIRTH.ToString("yyyy/MM/dd"))

                    Catch ex As Exception

                    End Try

                    Dim AGE As Integer = 0
                    Select Case Me.RT_CD.Value
                        Case "A0027"
                            If _isValid(BIRTH_) AndAlso Not SetRRValue.setDate(RET_DATE).StartsWith("1900") Then

                                Dim dtRET_DATE As String = CDate(RET_DATE).ToString("yyyy/MM/dd")

                                Dim iAge As Integer
                                iAge = DateDiff(DateInterval.Year, CDate(BIRTH_), CDate(dtRET_DATE))
                                If CDate(BIRTH_).ToString("MM/dd") > CDate(dtRET_DATE).ToString("MM/dd") Then
                                    iAge = iAge - 1
                                End If
                                AGE = iAge
                            End If

                        Case Else
                            If _isValid(BIRTH_) AndAlso Not SetRRValue.setDate(DEP_DATE).StartsWith("1900") Then

                                Dim dtDEP_DATE As String = CDate(DEP_DATE).ToString("yyyy/MM/dd")

                                Dim iAge As Integer
                                iAge = DateDiff(DateInterval.Year, CDate(BIRTH_), CDate(dtDEP_DATE))
                                If CDate(BIRTH_).ToString("MM/dd") > CDate(dtDEP_DATE).ToString("MM/dd") Then
                                    iAge = iAge - 1
                                End If
                                AGE = iAge
                            End If

                    End Select
                    r(0)("FAMILY_NAME") = CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_ROMAN.Text.ToUpper)
                    r(0)("FIRST_NAME") = CommonUtil.Xss(Me.INPUT_MAIN_NAME_ROMAN.Text.ToUpper)
                    r(0)("MIDDLE_NM") = ""
                    r(0)("FAMILY_NAME_KANA") = CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_KANA.Text.ToUpper)
                    r(0)("FIRST_NAME_KANA") = CommonUtil.Xss(Me.INPUT_MAIN_NAME_KANA.Text.ToUpper)
                    r(0)("FAMILY_NAME_KANJI") = CommonUtil.Xss(Me.INPUT_MAIN_SURNAME_KANJI.Text.ToUpper)
                    r(0)("FIRST_NAME_KANJI") = CommonUtil.Xss(Me.INPUT_MAIN_NAME_KANJI.Text.ToUpper)
                    If Me.INPUT_MAIN_MAN.Checked Then
                        r(0)("SEX") = "01"
                        r(0)("SEX_NM") = "男性"
                        r(0)("TITLE") = "MR"
                    ElseIf Me.INPUT_MAIN_WOMAN.Checked Then
                        r(0)("SEX") = "02"
                        r(0)("SEX_NM") = "女性"
                        r(0)("TITLE") = "MS"
                    End If
                    r(0)("BIRTH") = CommonUtil.Xss(BIRTH_)
                    r(0)("AGE") = CommonUtil.Xss(AGE)
                    r(0)("NATIONALITY") = "JP"


                    If Me.MAIN_PERSON_TOUR_SERVICEPanel.Visible Then

                        For n = 0 To Me.MAIN_PERSON_TOUR_SERVICERepeater.Items.Count - 1

                            Dim SERVICE_CD As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("SERVICE_CD"), Label).Text
                            Dim SERVICE_NM As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("SERVICE_NM"), Label).Text
                            Dim SELECT_WAY_KBN As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("SELECT_WAY_KBN"), Label).Text

                            Select Case SELECT_WAY_KBN
                                Case "1"
                                    If Not CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                                        Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                        rRES_ADD_SERVICE.NAME_NO = r(0)("NAME_NO")
                                        rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                        rRES_ADD_SERVICE.SERVICE_PRICE_CD = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue
                                        rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                        rRES_ADD_SERVICE.SERVICE_PRICE_NM = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                                        rRES_ADD_SERVICE.REMARKS = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                                        dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                                    End If
                                Case "2"
                                    Dim RES_SERVICE_TOUR_CHECKLISTRepeater As Repeater = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                                    For m = 0 To RES_SERVICE_TOUR_CHECKLISTRepeater.Items.Count - 1
                                        Dim SERVICE_PRICE_CD As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD"), Label).Text
                                        Dim SERVICE_PRICE_NM As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_NM"), Label).Text
                                        Dim SERVICE_PRICE_CD_CHECKBOX As Boolean = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked

                                        If SERVICE_PRICE_CD_CHECKBOX Then
                                            Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                            rRES_ADD_SERVICE.NAME_NO = r(0)("NAME_NO")
                                            rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                            rRES_ADD_SERVICE.SERVICE_PRICE_CD = SERVICE_PRICE_CD
                                            rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                            rRES_ADD_SERVICE.SERVICE_PRICE_NM = SERVICE_PRICE_NM
                                            rRES_ADD_SERVICE.REMARKS = SERVICE_PRICE_NM
                                            dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                                        End If

                                    Next
                                Case "3"

                                    Dim REMARKS As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(n).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text

                                    If SERVICE_CD.Equals("FC") Then
                                        REMARKS = StrConv(REMARKS, VbStrConv.Narrow)
                                    End If

                                    Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                    rRES_ADD_SERVICE.NAME_NO = r(0)("NAME_NO")
                                    rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                    rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                    rRES_ADD_SERVICE.SERVICE_PRICE_CD = ""
                                    rRES_ADD_SERVICE.SERVICE_PRICE_NM = ""
                                    rRES_ADD_SERVICE.REMARKS = REMARKS
                                    dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)

                            End Select

                        Next
                    End If

                End If


        End Select

        '旅客
        If Me.PASSENGER_OTHERPanel.Visible Then
            For i = 0 To Me.PASSENGER_OTHERRepeater.Items.Count - 1
                Dim NAME_NO As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_NO"), Label).Text
                Dim AGE_KBN As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("AGE_KBN"), Label).Text
                Dim SEX_MAN_FLG As Boolean = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MAN"), RadioButton).Checked
                Dim SEX_WOMAN_FLG As Boolean = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("WOMAN"), RadioButton).Checked
                Dim BIRTH_YYYY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).Text
                Dim BIRTH_MM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).SelectedValue
                Dim BIRTH_DD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).SelectedValue
                Dim PASSPORT_NO As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_NO"), TextBox).Text
                Dim PASSPORT_LIMIT_YYYY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_YYYY"), TextBox).Text
                Dim PASSPORT_LIMIT_MM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_MM"), DropDownList).SelectedValue
                Dim PASSPORT_LIMIT_DD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_DD"), DropDownList).SelectedValue
                Dim PASSPORT_DATE_YYYY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_YYYY"), TextBox).Text
                Dim PASSPORT_DATE_MM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_MM"), DropDownList).SelectedValue
                Dim PASSPORT_DATE_DD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_DATE_DD"), DropDownList).SelectedValue
                Dim NATIONALITY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITY"), DropDownList).SelectedValue
                Dim PASSPORT_ISSUE_COUNTRY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).SelectedValue

                Dim VISA_NO As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_NO"), TextBox).Text
                Dim VISA_COUNTRY_CD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).SelectedValue
                Dim VISA_VALID_DATE_YYYY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_YYYY"), TextBox).Text
                Dim VISA_VALID_DATE_MM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).SelectedValue
                Dim VISA_VALID_DATE_DD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).SelectedValue
                Dim VISA_JOIN_COUNTRY_CD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).SelectedValue
                Dim MILEAGE_CARRIER As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_CARRIER"), DropDownList).SelectedValue
                Dim MILEAGE_NO As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_NO"), TextBox).Text
                Dim SEAT_REQUEST As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SEAT_REQUEST"), DropDownList).SelectedValue

                Dim BIRTH As String = ""
                Dim TITLE As String = ""
                Dim SEX As String = ""
                Dim SEX_NM As String = ""
                Dim AGE As String = ""
                Dim PASSPORT_LIMIT As String = ""
                Dim PASSPORT_DATE As String = ""
                Dim VISA_LIMIT As String = ""

                Try
                    Dim dt As Date = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD
                    BIRTH = dt.ToString("yyyy/MM/dd")
                Catch ex As Exception
                    BIRTH = "1900/01/01"
                End Try

                Try
                    Dim dt As Date = PASSPORT_LIMIT_YYYY & "/" & PASSPORT_LIMIT_MM & "/" & PASSPORT_LIMIT_DD
                    PASSPORT_LIMIT = dt.ToString("yyyy/MM/dd")
                Catch ex As Exception
                    PASSPORT_LIMIT = "1900/01/01"
                End Try

                Try
                    Dim dt As Date = PASSPORT_DATE_YYYY & "/" & PASSPORT_DATE_MM & "/" & PASSPORT_DATE_DD
                    PASSPORT_DATE = dt.ToString("yyyy/MM/dd")
                Catch ex As Exception
                    PASSPORT_DATE = "1900/01/01"
                End Try

                Try
                    Dim dt As Date = VISA_VALID_DATE_YYYY & "/" & VISA_VALID_DATE_MM & "/" & VISA_VALID_DATE_DD
                    VISA_LIMIT = dt.ToString("yyyy/MM/dd")
                Catch ex As Exception
                    VISA_LIMIT = "1900/01/01"
                End Try

                If SEX_MAN_FLG Then
                    SEX = "01"
                    SEX_NM = LABEL_0117

                    If AGE_KBN.Equals("ADT") Then
                        TITLE = "MR"
                    Else
                        TITLE = "MSTR"
                    End If

                Else
                    SEX = "02"
                    SEX_NM = LABEL_0118

                    If AGE_KBN.Equals("ADT") Then
                        TITLE = "MS"
                    Else
                        TITLE = "MISS"
                    End If

                End If


                Select Case Me.RT_CD.Value
                    Case "A0027"
                        If _isValid(BIRTH) AndAlso Not SetRRValue.setDate(RET_DATE).StartsWith("1900") Then

                            Dim dtRET_DATE As String = CDate(RET_DATE).ToString("yyyy/MM/dd")

                            Dim iAge As Integer
                            iAge = DateDiff(DateInterval.Year, CDate(BIRTH), CDate(dtRET_DATE))
                            If CDate(BIRTH).ToString("MM/dd") > CDate(dtRET_DATE).ToString("MM/dd") Then
                                iAge = iAge - 1
                            End If
                            AGE = iAge
                        End If

                    Case Else
                        If _isValid(BIRTH) AndAlso Not SetRRValue.setDate(DEP_DATE).StartsWith("1900") Then

                            Dim dtDEP_DATE As String = CDate(DEP_DATE).ToString("yyyy/MM/dd")

                            Dim iAge As Integer
                            iAge = DateDiff(DateInterval.Year, CDate(BIRTH), CDate(dtDEP_DATE))
                            If CDate(BIRTH).ToString("MM/dd") > CDate(dtDEP_DATE).ToString("MM/dd") Then
                                iAge = iAge - 1
                            End If
                            AGE = iAge
                        End If

                End Select

                Dim r() As DataRow = dsItinerary.PAGE_04.Select("NAME_NO='" & NAME_NO & "'")

                If r.Length = 0 Then
                    Continue For
                End If

                r(0)("FAMILY_NAME") = CommonUtil.Xss(CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SURNAME_ROMAN"), TextBox).Text.ToUpper)
                r(0)("FIRST_NAME") = CommonUtil.Xss(CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMAN"), TextBox).Text.ToUpper)
                r(0)("MIDDLE_NM") = CommonUtil.Xss(CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MIDDLE_NM"), TextBox).Text.ToUpper)
                r(0)("FAMILY_NAME_KANA") = CommonUtil.Xss(CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANA"), TextBox).Text.ToUpper)
                r(0)("FIRST_NAME_KANA") = CommonUtil.Xss(CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANA"), TextBox).Text.ToUpper)
                r(0)("FAMILY_NAME_KANJI") = CommonUtil.Xss(CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANJI"), TextBox).Text)
                r(0)("FIRST_NAME_KANJI") = CommonUtil.Xss(CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANJI"), TextBox).Text)
                r(0)("TITLE") = TITLE
                r(0)("SEX") = SEX
                r(0)("SEX_NM") = SEX_NM
                r(0)("BIRTH") = CommonUtil.Xss(BIRTH)
                r(0)("AGE") = CommonUtil.Xss(AGE)
                r(0)("PASSPORT_NO") = CommonUtil.Xss(PASSPORT_NO)
                r(0)("PASSPORT_LIMIT") = CommonUtil.Xss(PASSPORT_LIMIT)
                r(0)("PASSPORT_DATE") = CommonUtil.Xss(PASSPORT_DATE)
                r(0)("NATIONALITY") = CommonUtil.Xss(NATIONALITY)
                r(0)("PASSPORT_ISSUE_COUNTRY") = CommonUtil.Xss(PASSPORT_ISSUE_COUNTRY)
                r(0)("MILLAGE_CARRIER") = CommonUtil.Xss(MILEAGE_CARRIER)
                r(0)("MILAGE_CARD") = CommonUtil.Xss(MILEAGE_NO)
                r(0)("VISA_NO") = CommonUtil.Xss(VISA_NO)
                r(0)("VISA_COUNTRY_CD") = CommonUtil.Xss(VISA_COUNTRY_CD)
                r(0)("VISA_LIMIT") = CommonUtil.Xss(VISA_LIMIT)
                r(0)("VISA_ENTRY_COUNTRY_CD") = CommonUtil.Xss(VISA_JOIN_COUNTRY_CD)
                r(0)("REMARKS") = CommonUtil.Xss(SEAT_REQUEST)

                '付帯サービス
                If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICEPanel"), Panel).Visible Then

                    Dim ServiceRepeater As Repeater = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICERepeater"), Repeater)

                    For k = 0 To ServiceRepeater.Items.Count - 1

                        Dim SEGMENT_SEQ As String = CType(ServiceRepeater.Items(k).FindControl("SEGMENT_SEQ"), Label).Text

                        Dim ServiceSubRepeater As Repeater = CType(ServiceRepeater.Items(k).FindControl("RES_SERVICE_SUBRepeater"), Repeater)

                        Dim SERVICE_KBN As String = "" ' カンマ区切り

                        For m = 0 To ServiceSubRepeater.Items.Count - 1

                            Dim SERVICE As String = CType(ServiceSubRepeater.Items(m).FindControl("SERVICE"), DropDownList).SelectedValue

                            If Not SERVICE.Equals("") Then
                                SERVICE_KBN += SERVICE & ","
                            End If

                        Next

                        SERVICE_KBN = SERVICE_KBN.TrimEnd(",")


                        If SERVICE_KBN.Equals("") Then
                            Continue For
                        End If

                        Dim rPAGE_07() As DataRow = dsItinerary.PAGE_07.Select("SEGMENT_SEQ='" & SEGMENT_SEQ & "'")

                        Dim rCheckDupe() As DataRow = dsItinerary.RES_TICKET_SEGMENT_PAX.Select("SEGMENT_SEQ='" & SEGMENT_SEQ & "' And NAME_NO='" & NAME_NO & "'")

                        If 0 < rCheckDupe.Length Then
                            rCheckDupe(0)("SERVICE_CD") = SERVICE_KBN
                        Else
                            Dim rRES_TICKET_SEGMENT_PAX As TriphooRR097DataSet.RES_TICKET_SEGMENT_PAXRow = dsItinerary.RES_TICKET_SEGMENT_PAX.NewRES_TICKET_SEGMENT_PAXRow
                            rRES_TICKET_SEGMENT_PAX.SEGMENT_SEQ = SEGMENT_SEQ
                            rRES_TICKET_SEGMENT_PAX.NAME_NO = NAME_NO
                            rRES_TICKET_SEGMENT_PAX.TICKET_NO = ""
                            rRES_TICKET_SEGMENT_PAX.VALID_FLG = True
                            rRES_TICKET_SEGMENT_PAX.REMARKS = ""
                            rRES_TICKET_SEGMENT_PAX.SERVICE_CD = SERVICE_KBN
                            rRES_TICKET_SEGMENT_PAX.PNR = ""
                            rRES_TICKET_SEGMENT_PAX.AIR_COMPANY_CD = rPAGE_07(0)("AIR_COMPANY_CD")
                            rRES_TICKET_SEGMENT_PAX.FLIGHT_NO = rPAGE_07(0)("FLIGHT_NO")
                            rRES_TICKET_SEGMENT_PAX.BOOKING_CLASS = rPAGE_07(0)("BOOKING_CLASS")
                            rRES_TICKET_SEGMENT_PAX.DEP_CD = rPAGE_07(0)("DEP_CD")
                            rRES_TICKET_SEGMENT_PAX.DEP_TIME = rPAGE_07(0)("DEP_TIME")
                            rRES_TICKET_SEGMENT_PAX.DEP_WEEK = rPAGE_07(0)("DEP_WEEK")
                            rRES_TICKET_SEGMENT_PAX.ARR_CD = rPAGE_07(0)("ARR_CD")
                            rRES_TICKET_SEGMENT_PAX.ARR_TIME = rPAGE_07(0)("ARR_TIME")
                            rRES_TICKET_SEGMENT_PAX.ARR_WEEK = rPAGE_07(0)("ARR_WEEK")
                            rRES_TICKET_SEGMENT_PAX.GDS_STATUS = ""
                            dsItinerary.RES_TICKET_SEGMENT_PAX.AddRES_TICKET_SEGMENT_PAXRow(rRES_TICKET_SEGMENT_PAX)

                        End If

                    Next
                End If

                'ツアー付帯サービス
                If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURPanel"), Panel).Visible Then

                    Dim TourServiceRepeater As Repeater = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURRepeater"), Repeater)

                    For n = 0 To TourServiceRepeater.Items.Count - 1

                        Dim SERVICE_CD As String = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_CD"), Label).Text
                        Dim SERVICE_NM As String = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_NM"), Label).Text
                        Dim SELECT_WAY_KBN As String = CType(TourServiceRepeater.Items(n).FindControl("SELECT_WAY_KBN"), Label).Text

                        Select Case SELECT_WAY_KBN
                            Case "1"
                                If Not CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                                    Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                    rRES_ADD_SERVICE.NAME_NO = NAME_NO
                                    rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                    rRES_ADD_SERVICE.SERVICE_PRICE_CD = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue
                                    rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                    rRES_ADD_SERVICE.SERVICE_PRICE_NM = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                                    rRES_ADD_SERVICE.REMARKS = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                                    dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                                End If
                            Case "2"
                                Dim RES_SERVICE_TOUR_CHECKLISTRepeater As Repeater = CType(TourServiceRepeater.Items(n).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                                For m = 0 To RES_SERVICE_TOUR_CHECKLISTRepeater.Items.Count - 1
                                    Dim SERVICE_PRICE_CD As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD"), Label).Text
                                    Dim SERVICE_PRICE_NM As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_NM"), Label).Text
                                    Dim SERVICE_PRICE_CD_CHECKBOX As Boolean = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked

                                    If SERVICE_PRICE_CD_CHECKBOX Then
                                        Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                        rRES_ADD_SERVICE.NAME_NO = NAME_NO
                                        rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                        rRES_ADD_SERVICE.SERVICE_PRICE_CD = SERVICE_PRICE_CD
                                        rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                        rRES_ADD_SERVICE.SERVICE_PRICE_NM = SERVICE_PRICE_NM
                                        rRES_ADD_SERVICE.REMARKS = SERVICE_PRICE_NM
                                        dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                                    End If

                                Next
                            Case "3"

                                Dim REMARKS As String = CType(TourServiceRepeater.Items(n).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text

                                If SERVICE_CD.Equals("FC") Then
                                    REMARKS = StrConv(REMARKS, VbStrConv.Narrow)
                                End If

                                Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                rRES_ADD_SERVICE.NAME_NO = NAME_NO
                                rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                rRES_ADD_SERVICE.SERVICE_PRICE_CD = ""
                                rRES_ADD_SERVICE.SERVICE_PRICE_NM = ""
                                rRES_ADD_SERVICE.REMARKS = REMARKS
                                dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                        End Select

                    Next

                End If


            Next
        End If

        If Me.PASSENGER_HOTELPanel.Visible Then

            For i = 0 To Me.PASSENGER_HOTELRepeater.Items.Count - 1

                Dim REP_HOTEL_NAME As Repeater = CType(Me.PASSENGER_HOTELRepeater.Items(i).FindControl("PASSENGER_HOTEL_NAMERepeater"), Repeater)

                For k = 0 To REP_HOTEL_NAME.Items.Count - 1
                    Dim NAME_NO As String = CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_NO"), Label).Text
                    Dim AGE_KBN As String = CType(REP_HOTEL_NAME.Items(k).FindControl("AGE_KBN"), Label).Text
                    Dim SURNAME_ROMAN As String = CType(REP_HOTEL_NAME.Items(k).FindControl("SURNAME_ROMAN"), TextBox).Text.ToUpper '姓（ローマ字）
                    Dim NAME_ROMAN As String = CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_ROMAN"), TextBox).Text.ToUpper '      名（ローマ字）
                    Dim MIDDLE_NM As String = CType(REP_HOTEL_NAME.Items(k).FindControl("MIDDLE_NM"), TextBox).Text.ToUpper
                    Dim FAMILY_NAME_KANA As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FAMILY_NAME_KANA"), TextBox).Text.ToUpper
                    Dim FIRST_NAME_KANA As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FIRST_NAME_KANA"), TextBox).Text.ToUpper
                    Dim FAMILY_NAME_KANJI As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FAMILY_NAME_KANJI"), TextBox).Text
                    Dim FIRST_NAME_KANJI As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FIRST_NAME_KANJI"), TextBox).Text
                    Dim MAN As Boolean = CType(REP_HOTEL_NAME.Items(k).FindControl("MAN"), RadioButton).Checked '           性別
                    Dim WOMAN As Boolean = CType(REP_HOTEL_NAME.Items(k).FindControl("WOMAN"), RadioButton).Checked '       性別
                    Dim NOANSWER As Boolean = CType(REP_HOTEL_NAME.Items(k).FindControl("NOANSWER"), RadioButton).Checked '       性別
                    Dim BIRTH_YYYY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).Text '      生年月日
                    Dim BIRTH_MM As String = CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).Text '          生年月日
                    Dim BIRTH_DD As String = CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).Text '          生年月日
                    Dim PASSPORT_NO As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_NO"), TextBox).Text
                    Dim PASSPORT_DATE_YYYY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_DATE_YYYY"), TextBox).Text
                    Dim PASSPORT_DATE_MM As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_DATE_MM"), DropDownList).SelectedValue
                    Dim PASSPORT_DATE_DD As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_DATE_DD"), DropDownList).SelectedValue
                    Dim PASSPORT_LIMIT_YYYY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_YYYY"), TextBox).Text
                    Dim PASSPORT_LIMIT_MM As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).SelectedValue
                    Dim PASSPORT_LIMIT_DD As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).SelectedValue
                    Dim PASSPORT_ISSUE_COUNTRY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).SelectedValue
                    Dim NATIONALITY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("NATIONALITY"), DropDownList).SelectedValue
                    Dim BIRTH As String = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD

                    Dim TITLE As String = ""
                    Dim SEX As String = ""
                    Dim SEX_NM As String = ""
                    Dim AGE As String = ""
                    Dim PASSPORT_LIMIT As String = ""
                    Dim PASSPORT_DATE As String = ""

                    Try
                        Dim dt As Date = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD
                        BIRTH = dt.ToString("yyyy/MM/dd")
                    Catch ex As Exception
                        BIRTH = "1900/01/01"
                    End Try

                    Try
                        Dim dt As Date = PASSPORT_LIMIT_YYYY & "/" & PASSPORT_LIMIT_MM & "/" & PASSPORT_LIMIT_DD
                        PASSPORT_LIMIT = dt.ToString("yyyy/MM/dd")
                    Catch ex As Exception
                        PASSPORT_LIMIT = "1900/01/01"
                    End Try

                    Try
                        Dim dt As Date = PASSPORT_DATE_YYYY & "/" & PASSPORT_DATE_MM & "/" & PASSPORT_DATE_DD
                        PASSPORT_DATE = dt.ToString("yyyy/MM/dd")
                    Catch ex As Exception
                        PASSPORT_DATE = "1900/01/01"
                    End Try

                    If CType(REP_HOTEL_NAME.Items(k).FindControl("SEX_FLGPanel"), Panel).Visible Then

                        If MAN Then
                            SEX = "01"
                            SEX_NM = LABEL_0117

                            If AGE_KBN.Equals("ADT") Then
                                TITLE = "MR"
                            Else
                                TITLE = "MSTR"
                            End If
                        ElseIf WOMAN Then
                            SEX = "02"
                            SEX_NM = LABEL_0118

                            If AGE_KBN.Equals("ADT") Then
                                TITLE = "MS"
                            Else
                                TITLE = "MISS"
                            End If
                        ElseIf NOANSWER Then
                            SEX = "03"
                            SEX_NM = "回答しない"
                            TITLE = "OTH"
                        End If
                    Else
                        SEX = ""
                        SEX_NM = ""
                        TITLE = ""
                    End If


                    Select Case Me.RT_CD.Value
                        Case "A0027"
                            If _isValid(BIRTH) AndAlso Not SetRRValue.setDate(RET_DATE).StartsWith("1900") Then

                                Dim dtRET_DATE As String = CDate(RET_DATE).ToString("yyyy/MM/dd")

                                Dim iAge As Integer
                                iAge = DateDiff(DateInterval.Year, CDate(BIRTH), CDate(dtRET_DATE))
                                If CDate(BIRTH).ToString("MM/dd") > CDate(dtRET_DATE).ToString("MM/dd") Then
                                    iAge = iAge - 1
                                End If
                                AGE = iAge
                            End If

                        Case Else
                            If _isValid(BIRTH) AndAlso Not SetRRValue.setDate(DEP_DATE).StartsWith("1900") Then

                                Dim dtDEP_DATE As String = CDate(DEP_DATE).ToString("yyyy/MM/dd")

                                Dim iAge As Integer
                                iAge = DateDiff(DateInterval.Year, CDate(BIRTH), CDate(dtDEP_DATE))
                                If CDate(BIRTH).ToString("MM/dd") > CDate(dtDEP_DATE).ToString("MM/dd") Then
                                    iAge = iAge - 1
                                End If
                                AGE = iAge
                            End If

                    End Select

                    'If _isValid(BIRTH) AndAlso Not SetRRValue.setDate(DEP_DATE).StartsWith("1900") Then
                    '    Dim dtDEP_DATE As String = CDate(DEP_DATE).ToString("yyyy/MM/dd")

                    '    Dim iAge As Integer
                    '    iAge = DateDiff(DateInterval.Year, CDate(BIRTH), CDate(dtDEP_DATE))
                    '    If CDate(BIRTH).ToString("MM/dd") > CDate(dtDEP_DATE).ToString("MM/dd") Then
                    '        iAge = iAge - 1
                    '    End If
                    '    AGE = iAge
                    'End If

                    Dim r() As DataRow = dsItinerary.PAGE_04.Select("NAME_NO='" & NAME_NO & "'")

                    If r.Length = 0 Then
                        Continue For
                    End If

                    r(0)("FAMILY_NAME") = CommonUtil.Xss(SURNAME_ROMAN)
                    r(0)("FIRST_NAME") = CommonUtil.Xss(NAME_ROMAN)
                    r(0)("MIDDLE_NM") = CommonUtil.Xss(MIDDLE_NM)
                    r(0)("FAMILY_NAME_KANA") = CommonUtil.Xss(FAMILY_NAME_KANA)
                    r(0)("FIRST_NAME_KANA") = CommonUtil.Xss(FIRST_NAME_KANA)
                    r(0)("FAMILY_NAME_KANJI") = CommonUtil.Xss(FAMILY_NAME_KANJI)
                    r(0)("FIRST_NAME_KANJI") = CommonUtil.Xss(FIRST_NAME_KANJI)
                    r(0)("TITLE") = CommonUtil.Xss(TITLE)
                    r(0)("SEX") = CommonUtil.Xss(SEX)
                    r(0)("SEX_NM") = CommonUtil.Xss(SEX_NM)
                    r(0)("BIRTH") = CommonUtil.Xss(BIRTH)
                    r(0)("AGE") = CommonUtil.Xss(AGE)
                    r(0)("PASSPORT_NO") = CommonUtil.Xss(PASSPORT_NO)
                    r(0)("PASSPORT_LIMIT") = CommonUtil.Xss(PASSPORT_LIMIT)
                    r(0)("PASSPORT_DATE") = CommonUtil.Xss(PASSPORT_DATE)
                    r(0)("NATIONALITY") = CommonUtil.Xss(NATIONALITY)
                    r(0)("PASSPORT_ISSUE_COUNTRY") = CommonUtil.Xss(PASSPORT_ISSUE_COUNTRY)

                    '付帯サービス
                    If CType(REP_HOTEL_NAME.Items(k).FindControl("RES_SERVICEPanel"), Panel).Visible Then

                        Dim ServiceRepeater As Repeater = CType(REP_HOTEL_NAME.Items(k).FindControl("RES_SERVICERepeater"), Repeater)

                        For l = 0 To ServiceRepeater.Items.Count - 1

                            Dim SEGMENT_SEQ As String = CType(ServiceRepeater.Items(l).FindControl("SEGMENT_SEQ"), Label).Text

                            Dim ServiceSubRepeater As Repeater = CType(ServiceRepeater.Items(l).FindControl("RES_SERVICE_SUBRepeater"), Repeater)

                            Dim SERVICE_KBN As String = "" ' カンマ区切り

                            For m = 0 To ServiceSubRepeater.Items.Count - 1

                                Dim SERVICE As String = CType(ServiceSubRepeater.Items(m).FindControl("SERVICE"), DropDownList).SelectedValue

                                If Not SERVICE.Equals("") Then
                                    SERVICE_KBN += SERVICE & ","
                                End If

                            Next

                            SERVICE_KBN = SERVICE_KBN.TrimEnd(",")


                            If SERVICE_KBN.Equals("") Then
                                Continue For
                            End If

                            Dim rPAGE_07() As DataRow = dsItinerary.PAGE_07.Select("SEGMENT_SEQ='" & SEGMENT_SEQ & "'")

                            Dim rCheckDupe() As DataRow = dsItinerary.RES_TICKET_SEGMENT_PAX.Select("SEGMENT_SEQ='" & SEGMENT_SEQ & "' And NAME_NO='" & NAME_NO & "'")

                            If 0 < rCheckDupe.Length Then
                                rCheckDupe(0)("SERVICE_CD") = SERVICE_KBN
                            Else
                                Dim rRES_TICKET_SEGMENT_PAX As TriphooRR097DataSet.RES_TICKET_SEGMENT_PAXRow = dsItinerary.RES_TICKET_SEGMENT_PAX.NewRES_TICKET_SEGMENT_PAXRow
                                rRES_TICKET_SEGMENT_PAX.SEGMENT_SEQ = SEGMENT_SEQ
                                rRES_TICKET_SEGMENT_PAX.NAME_NO = NAME_NO
                                rRES_TICKET_SEGMENT_PAX.TICKET_NO = ""
                                rRES_TICKET_SEGMENT_PAX.VALID_FLG = True
                                rRES_TICKET_SEGMENT_PAX.REMARKS = ""
                                rRES_TICKET_SEGMENT_PAX.SERVICE_CD = SERVICE_KBN
                                rRES_TICKET_SEGMENT_PAX.PNR = ""
                                rRES_TICKET_SEGMENT_PAX.AIR_COMPANY_CD = rPAGE_07(0)("AIR_COMPANY_CD")
                                rRES_TICKET_SEGMENT_PAX.FLIGHT_NO = rPAGE_07(0)("FLIGHT_NO")
                                rRES_TICKET_SEGMENT_PAX.BOOKING_CLASS = rPAGE_07(0)("BOOKING_CLASS")
                                rRES_TICKET_SEGMENT_PAX.DEP_CD = rPAGE_07(0)("DEP_CD")
                                rRES_TICKET_SEGMENT_PAX.DEP_TIME = rPAGE_07(0)("DEP_TIME")
                                rRES_TICKET_SEGMENT_PAX.DEP_WEEK = rPAGE_07(0)("DEP_WEEK")
                                rRES_TICKET_SEGMENT_PAX.ARR_CD = rPAGE_07(0)("ARR_CD")
                                rRES_TICKET_SEGMENT_PAX.ARR_TIME = rPAGE_07(0)("ARR_TIME")
                                rRES_TICKET_SEGMENT_PAX.ARR_WEEK = rPAGE_07(0)("ARR_WEEK")
                                rRES_TICKET_SEGMENT_PAX.GDS_STATUS = ""
                                dsItinerary.RES_TICKET_SEGMENT_PAX.AddRES_TICKET_SEGMENT_PAXRow(rRES_TICKET_SEGMENT_PAX)

                            End If

                        Next

                    End If

                    'ツアー付帯サービス
                    If CType(REP_HOTEL_NAME.Items(k).FindControl("RES_SERVICE_TOURPanel"), Panel).Visible Then

                        Dim TourServiceRepeater As Repeater = CType(REP_HOTEL_NAME.Items(k).FindControl("RES_SERVICE_TOURRepeater"), Repeater)

                        For n = 0 To TourServiceRepeater.Items.Count - 1

                            Dim SERVICE_CD As String = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_CD"), Label).Text
                            Dim SERVICE_NM As String = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_NM"), Label).Text
                            Dim SELECT_WAY_KBN As String = CType(TourServiceRepeater.Items(n).FindControl("SELECT_WAY_KBN"), Label).Text

                            Select Case SELECT_WAY_KBN
                                Case "1"
                                    If Not CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                                        Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                        rRES_ADD_SERVICE.NAME_NO = NAME_NO
                                        rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                        rRES_ADD_SERVICE.SERVICE_PRICE_CD = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue
                                        rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                        rRES_ADD_SERVICE.SERVICE_PRICE_NM = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                                        rRES_ADD_SERVICE.REMARKS = CType(TourServiceRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                                        dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                                    End If
                                Case "2"
                                    Dim RES_SERVICE_TOUR_CHECKLISTRepeater As Repeater = CType(TourServiceRepeater.Items(n).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                                    For m = 0 To RES_SERVICE_TOUR_CHECKLISTRepeater.Items.Count - 1
                                        Dim SERVICE_PRICE_CD As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD"), Label).Text
                                        Dim SERVICE_PRICE_NM As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_NM"), Label).Text
                                        Dim SERVICE_PRICE_CD_CHECKBOX As Boolean = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked

                                        If SERVICE_PRICE_CD_CHECKBOX Then
                                            Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                            rRES_ADD_SERVICE.NAME_NO = NAME_NO
                                            rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                            rRES_ADD_SERVICE.SERVICE_PRICE_CD = SERVICE_PRICE_CD
                                            rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                            rRES_ADD_SERVICE.SERVICE_PRICE_NM = SERVICE_PRICE_NM
                                            rRES_ADD_SERVICE.REMARKS = SERVICE_PRICE_NM
                                            dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                                        End If
                                    Next
                                Case "3"

                                    Dim REMARKS As String = CType(TourServiceRepeater.Items(n).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text

                                    If SERVICE_CD.Equals("FC") Then
                                        REMARKS = StrConv(REMARKS, VbStrConv.Narrow)
                                    End If

                                    Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                    rRES_ADD_SERVICE.NAME_NO = NAME_NO
                                    rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                    rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                    rRES_ADD_SERVICE.SERVICE_PRICE_CD = ""
                                    rRES_ADD_SERVICE.SERVICE_PRICE_NM = ""
                                    rRES_ADD_SERVICE.REMARKS = REMARKS
                                    dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)

                            End Select

                        Next

                    End If

                Next
            Next

        End If


        '手荷物追加料金
        Dim AIR_SUP_CD As String = Me.RT_CD.Value

        If 0 < dsItinerary.PAGE_05.Rows.Count Then
            AIR_SUP_CD = dsItinerary.PAGE_05.Rows(0)("SUP_CD")
        End If

        Dim _7400 As String = "7400"
        Dim _7400ADT As String = "7400ADT"
        Dim _7400CHD As String = "7400CHD"
        Dim _7400INF As String = "7400INF"

        Try
            Dim dsM035_ACCOUNT_TYPE As TriphooB2CAPI.M035_COMMON_ACCOUNT_TYPE_DataSet = B2CAPIClient.SelectM035_COMMON_ACCOUNT_TYPEGateway(Me.RT_CD.Value, "", _7400, dsB2CUser)

            If 0 < dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows.Count Then
                _7400 = dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows(0)("ACCOUNT_TYPE_CD")
            End If
        Catch ex As Exception
        End Try

        Try
            Dim dsM035_ACCOUNT_TYPE As TriphooB2CAPI.M035_COMMON_ACCOUNT_TYPE_DataSet = B2CAPIClient.SelectM035_COMMON_ACCOUNT_TYPEGateway(Me.RT_CD.Value, "", _7400ADT, dsB2CUser)

            If 0 < dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows.Count Then
                _7400ADT = dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows(0)("ACCOUNT_TYPE_CD")
            End If
        Catch ex As Exception
        End Try

        Try
            Dim dsM035_ACCOUNT_TYPE As TriphooB2CAPI.M035_COMMON_ACCOUNT_TYPE_DataSet = B2CAPIClient.SelectM035_COMMON_ACCOUNT_TYPEGateway(Me.RT_CD.Value, "", _7400CHD, dsB2CUser)

            If 0 < dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows.Count Then
                _7400CHD = dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows(0)("ACCOUNT_TYPE_CD")
            End If
        Catch ex As Exception
        End Try

        Try
            Dim dsM035_ACCOUNT_TYPE As TriphooB2CAPI.M035_COMMON_ACCOUNT_TYPE_DataSet = B2CAPIClient.SelectM035_COMMON_ACCOUNT_TYPEGateway(Me.RT_CD.Value, "", _7400INF, dsB2CUser)

            If 0 < dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows.Count Then
                _7400INF = dsM035_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Rows(0)("ACCOUNT_TYPE_CD")
            End If
        Catch ex As Exception
        End Try

        Dim rRES_ORDER_DATA() As DataRow = dsItinerary.RES_ORDER_DATA.Select("ACCOUNT_TYPE_CD='" & _7400ADT & "' OR " &
                                                                             "ACCOUNT_TYPE_CD='" & _7400CHD & "' OR " &
                                                                             "ACCOUNT_TYPE_CD='" & _7400INF & "' OR " &
                                                                             "ACCOUNT_TYPE_CD='" & _7400 & "'")
        For Each row As DataRow In rRES_ORDER_DATA
            row.Delete()
        Next

        For Each row In dsItinerary.RES_TICKET_SEGMENT_PAX.Rows
            Dim NAME_NO As String = row("NAME_NO")
            Dim SEGMENT_SEQ As String = row("SEGMENT_SEQ")
            Dim SERVICE_CD As String = row("SERVICE_CD")

            Dim rPAGE_04() As DataRow = dsItinerary.PAGE_04.Select("NAME_NO=" & NAME_NO)
            Dim rPAGE_07() As DataRow = dsItinerary.PAGE_07.Select("SEGMENT_SEQ=" & SEGMENT_SEQ)

            Dim AGE_KBN As String = ""

            If 0 < rPAGE_04.Length Then
                AGE_KBN = rPAGE_04(0)("AGE_KBN")
            End If


            Dim ss() As String = SERVICE_CD.Split(",".ToCharArray, StringSplitOptions.RemoveEmptyEntries)

            For Each str As String In ss

                Dim rSERVICE_CD() As DataRow = dsItinerary.RES_SERVICE.Select("SERVICE_CD='" & str & "' And FLIGHT_SEGMENT_LINE_NO='" & SEGMENT_SEQ & "'")
                Dim _SERVICE_KBN As String = rSERVICE_CD(0)("SERVICE_KBN")
                Dim _SERVICE_NM As String = rSERVICE_CD(0)("SERVICE_NM")
                Dim _SALE_PRICE As String = rSERVICE_CD(0)("SALE_PRICE")
                Dim _SUP_PRICE As String = rSERVICE_CD(0)("SUP_PRICE")

                Dim ACCOUNT_TYPE_NM As String = ""

                Select Case _SERVICE_KBN
                    Case "3" : ACCOUNT_TYPE_NM = "座席指定 " & _SERVICE_NM
                    Case Else : ACCOUNT_TYPE_NM += _SERVICE_NM
                End Select

                Select Case rPAGE_07(0)("GOING_RETURN_KBN")
                    Case "01" : ACCOUNT_TYPE_NM += "　(往路・" & NAME_NO & "人目)"
                    Case "02" : ACCOUNT_TYPE_NM += "　(復路・" & NAME_NO & "人目)"
                End Select

                Dim ACCOUNT_TYPE_CD As String = ""

                Select Case AGE_KBN
                    Case "ADT"
                        ACCOUNT_TYPE_CD = _7400ADT
                    Case "CHD"
                        ACCOUNT_TYPE_CD = _7400CHD
                    Case "INF"
                        ACCOUNT_TYPE_CD = _7400INF
                    Case ""
                        ACCOUNT_TYPE_CD = _7400
                End Select


                CartUtil.createResOrder(dsItinerary,
                                        "01",
                                        1,
                                        True,
                                        ACCOUNT_TYPE_CD,
                                        ACCOUNT_TYPE_NM,
                                        1,
                                        1,
                                        1,
                                        "JPY",
                                        1,
                                        _SALE_PRICE,
                                        0,
                                        "JPY",
                                        1,
                                        _SUP_PRICE,
                                        0,
                                        AIR_SUP_CD,
                                        800,
                                        True,
                                        "",
                                        _SALE_PRICE,
                                        _SUP_PRICE)

            Next

        Next


        'ツアー付帯サービス
        If Me.RES_SERVICE_TOURPanel.Visible Then

            For n = 0 To Me.RES_SERVICE_TOURRepeater.Items.Count - 1

                Dim SERVICE_CD As String = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("SERVICE_CD"), Label).Text
                Dim SERVICE_NM As String = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("SERVICE_NM"), Label).Text
                Dim SELECT_WAY_KBN As String = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("SELECT_WAY_KBN"), Label).Text

                Select Case SELECT_WAY_KBN
                    Case "1"
                        If Not CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                            Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                            rRES_ADD_SERVICE.NAME_NO = 0
                            rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                            rRES_ADD_SERVICE.SERVICE_PRICE_CD = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue
                            rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                            rRES_ADD_SERVICE.SERVICE_PRICE_NM = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                            rRES_ADD_SERVICE.REMARKS = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedItem.Text
                            dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                        End If
                    Case "2"
                        Dim RES_SERVICE_TOUR_CHECKLISTRepeater As Repeater = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                        For m = 0 To RES_SERVICE_TOUR_CHECKLISTRepeater.Items.Count - 1
                            Dim SERVICE_PRICE_CD As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD"), Label).Text
                            Dim SERVICE_PRICE_NM As String = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_NM"), Label).Text
                            Dim SERVICE_PRICE_CD_CHECKBOX As Boolean = CType(RES_SERVICE_TOUR_CHECKLISTRepeater.Items(m).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked

                            If SERVICE_PRICE_CD_CHECKBOX Then
                                Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                                rRES_ADD_SERVICE.NAME_NO = 0
                                rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                                rRES_ADD_SERVICE.SERVICE_PRICE_CD = SERVICE_PRICE_CD
                                rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                                rRES_ADD_SERVICE.SERVICE_PRICE_NM = SERVICE_PRICE_NM
                                rRES_ADD_SERVICE.REMARKS = SERVICE_PRICE_NM
                                dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)
                            End If

                        Next
                    Case "3"

                        Dim REMARKS As String = CType(Me.RES_SERVICE_TOURRepeater.Items(n).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text

                        If SERVICE_CD.Equals("FC") Then
                            REMARKS = StrConv(REMARKS, VbStrConv.Narrow)
                        End If

                        Dim rRES_ADD_SERVICE As TriphooRR097DataSet.RES_ADD_SERVICERow = dsItinerary.RES_ADD_SERVICE.NewRES_ADD_SERVICERow
                        rRES_ADD_SERVICE.NAME_NO = 0
                        rRES_ADD_SERVICE.SERVICE_CD = SERVICE_CD
                        rRES_ADD_SERVICE.SERVICE_NM = SERVICE_NM
                        rRES_ADD_SERVICE.SERVICE_PRICE_CD = ""
                        rRES_ADD_SERVICE.SERVICE_PRICE_NM = ""
                        rRES_ADD_SERVICE.REMARKS = REMARKS
                        dsItinerary.RES_ADD_SERVICE.AddRES_ADD_SERVICERow(rRES_ADD_SERVICE)

                End Select

            Next
        End If

        If 0 < dsItinerary.RES_ADD_SERVICE.Rows.Count Then
            Dim dsM222_PACKAGE_SERVICE As New DataSet
            dsM222_PACKAGE_SERVICE.Merge(B2CAPIClient.SelectM222_PACKAGE_SERVICE_FOR_WEB_Gateway(Me.RT_CD.Value,
                                                                                                     dsItinerary.PAGE_20.Rows(0)("SEASONALITY"),
                                                                                                     dsItinerary.PAGE_20.Rows(0)("SEASONALITY_KBN"),
                                                                                                     dsItinerary.PAGE_20.Rows(0)("GOODS_CD"),
                                                                                                     dsItinerary.PAGE_20.Rows(0)("DEP_DATE"),
                                                                                                     dsB2CUser))

            For Each row In dsItinerary.RES_ADD_SERVICE.Rows
                Dim NAME_NO As String = row("NAME_NO")
                Dim SERVICE_CD As String = row("SERVICE_CD")
                Dim SERVICE_PRICE_CD As String = row("SERVICE_PRICE_CD")

                Dim rPAGE_04() As DataRow = dsItinerary.PAGE_04.Select("NAME_NO=" & NAME_NO)

                Dim AGE_KBN As String = ""

                If 0 < rPAGE_04.Length Then
                    AGE_KBN = rPAGE_04(0)("AGE_KBN")
                End If

                Dim rM222_PACKAGE_SERVICE() As DataRow '

                If Not SERVICE_PRICE_CD.Equals("") Then
                    rM222_PACKAGE_SERVICE = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").Select("SERVICE_CD='" & SERVICE_CD & "' AND SERVICE_PRICE_CD='" & SERVICE_PRICE_CD & "'")
                Else
                    rM222_PACKAGE_SERVICE = dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").Select("SERVICE_CD='" & SERVICE_CD & "'")
                End If

                Dim _SERVICE_NM As String = rM222_PACKAGE_SERVICE(0)("SERVICE_NM")
                Dim _SERVICE_PRICE_NM As String = rM222_PACKAGE_SERVICE(0)("SERVICE_PRICE_NM")
                Dim _SALES_PRICE As String = rM222_PACKAGE_SERVICE(0)("SALES_PRICE")
                Dim _SUP_PRICE As String = rM222_PACKAGE_SERVICE(0)("SUP_PRICE")

                If _SALES_PRICE = 0 And _SUP_PRICE = 0 Then
                    Continue For
                End If

                Dim ACCOUNT_TYPE_CD As String = ""
                Dim ACCOUNT_TYPE_NM As String = _SERVICE_NM & "(" & _SERVICE_PRICE_NM & ")"

                If Not AGE_KBN.Equals("") Then
                    ACCOUNT_TYPE_NM += "　(" & NAME_NO & "人目)"
                End If

                Select Case AGE_KBN
                    Case "ADT"
                        ACCOUNT_TYPE_CD = _7400ADT
                    Case "CHD"
                        ACCOUNT_TYPE_CD = _7400CHD
                    Case "INF"
                        ACCOUNT_TYPE_CD = _7400INF
                    Case ""
                        ACCOUNT_TYPE_CD = _7400
                End Select

                CartUtil.createResOrder(dsItinerary,
                                            "03",
                                            1,
                                            True,
                                            ACCOUNT_TYPE_CD,
                                            ACCOUNT_TYPE_NM,
                                            1,
                                            1,
                                            1,
                                            "JPY",
                                            1,
                                            _SALES_PRICE,
                                            0,
                                            "JPY",
                                            1,
                                            _SUP_PRICE,
                                            0,
                                            Me.RT_CD.Value,
                                            800,
                                            True,
                                            "",
                                            _SALES_PRICE,
                                            _SUP_PRICE)

            Next
        End If

    End Sub
#End Region

#End Region

#Region "アクション"

#Region "確認"
    Protected Sub CONFIRMLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet)

        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")

        '値保持
        If Me.DEP_DATEPanel.Visible Then
            Me.DEP_DATE.Text = Request.Item("DEP_DATE")
        End If

        If Me.RET_DATEPanel.Visible Then
            Me.RET_DATE.Text = Request.Item("RET_DATE")
        End If

        Dim ErrMsg As String = DataCheck(dsItinerary, dsUser)

        If ErrMsg.Equals("") Then
            Dim isCreate As Boolean = False

            If dsUser Is Nothing Then
                isCreate = True
            End If

            If MEMBER_ADD_KBN.Equals("5") Then
                isCreate = True
            End If

            If isCreate Then

                '顧客データ　アップデート
                dsItinerary.M019_CLIENT.Clear()
                dsItinerary.M021_CLIENT_ADDRESS.Clear()
                dsItinerary.M023_CLIENT_TEL.Clear()

                Dim SEX_KBN As String = ""

                If Me.INPUT_MAIN_MAN.Checked Then
                    SEX_KBN = "01"
                ElseIf Me.INPUT_MAIN_WOMAN.Checked Then
                    SEX_KBN = "02"
                End If

                Dim BIRTH As String = ""
                Dim BIRTH_YYYY As String = Me.INPUT_MAIN_BIRTH_YYYY.Text
                Dim BIRTH_MM As String = Me.INPUT_MAIN_BIRTH_MM.SelectedValue
                Dim BIRTH_DD As String = Me.INPUT_MAIN_BIRTH_DD.SelectedValue

                Try
                    Dim dt As Date = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD
                    BIRTH = dt.ToString("yyyy/MM/dd")
                Catch ex As Exception
                    BIRTH = "1900/01/01"
                End Try

                ' ●M019_CLIENT
                Dim rM019_CLIENT As TriphooRR097DataSet.M019_CLIENTRow =
                    dsItinerary.M019_CLIENT.NewM019_CLIENTRow
                rM019_CLIENT.RT_CD = RT_CD.Value
                rM019_CLIENT.CLIENT_CD = ""
                rM019_CLIENT.E_MAIL = Me.INPUT_MAIN_E_MAIL.Text
                rM019_CLIENT.PASSWORD = ""
                rM019_CLIENT.SURNAME_KANJI = Me.INPUT_MAIN_SURNAME_KANJI.Text.ToUpper
                rM019_CLIENT.NAME_KANJI = Me.INPUT_MAIN_NAME_KANJI.Text.ToUpper
                rM019_CLIENT.SURNAME_KANA = ""
                rM019_CLIENT.NAME_KANA = ""
                rM019_CLIENT.SURNAME_ROMAN = Me.INPUT_MAIN_SURNAME_ROMAN.Text.ToUpper
                rM019_CLIENT.NAME_ROMAN = Me.INPUT_MAIN_NAME_ROMAN.Text.ToUpper
                rM019_CLIENT.SEX_KBN = SEX_KBN
                rM019_CLIENT.SEX_KBN_NM = ""
                rM019_CLIENT.NATIONALITY = ""
                rM019_CLIENT.BIRTH = BIRTH
                rM019_CLIENT.OCCUPATION = ""
                rM019_CLIENT.PASSPORT_NO = ""
                rM019_CLIENT.PASSPORT_DATE = "1900/01/01"
                rM019_CLIENT.PASSPORT_LIMIT = "1900/01/01"
                rM019_CLIENT.PASSPORT_NAME_ROMAN = ""
                rM019_CLIENT.RE_ENCTY_NO = ""
                rM019_CLIENT.RE_ENCTY_LIMIT = "1900/01/01"
                rM019_CLIENT.FGN_ADD_NO = ""
                rM019_CLIENT.FGN_ADD_LIMIT = "1900/01/01"
                rM019_CLIENT.LOOKUP = ""
                rM019_CLIENT.REMARKS = ""
                rM019_CLIENT.MEMBER_TYPE_KBN = "01"
                rM019_CLIENT.INV_KBN = ""
                rM019_CLIENT.COMPANY_KBN = "01"
                rM019_CLIENT.COMPANY_NM = ""
                rM019_CLIENT.COMPANY_NM_KANA = ""
                rM019_CLIENT.COMPANY_SECTION_NM = ""
                rM019_CLIENT.COMPANY_SECTION_NM_KANA = ""
                rM019_CLIENT.SECRET_Q_KBN = ""
                rM019_CLIENT.SECRET_ANSWER = ""
                rM019_CLIENT.MAIL_MAGAZINE_FLG = False
                rM019_CLIENT.COMPANY_CLIENT_CD = ""
                rM019_CLIENT.DEPARTMENT_CD = ""
                rM019_CLIENT.CHARGE_EMP_CD = ""
                rM019_CLIENT.CLIENT_GROUP = ""
                rM019_CLIENT.EDIT_TIME = Now
                rM019_CLIENT.EDIT_RT_CD = RT_CD.Value
                rM019_CLIENT.EDIT_EMP_CD = "WEB"
                dsItinerary.M019_CLIENT.AddM019_CLIENTRow(rM019_CLIENT)

                'M023_CLIENT_TEL
                Dim rM023_CLIENT_TEL As TriphooRR097DataSet.M023_CLIENT_TELRow
                rM023_CLIENT_TEL = dsItinerary.M023_CLIENT_TEL.NewM023_CLIENT_TELRow
                rM023_CLIENT_TEL.RT_CD = RT_CD.Value
                rM023_CLIENT_TEL.CLIENT_CD = ""
                rM023_CLIENT_TEL.TEL_KBN = "01"
                rM023_CLIENT_TEL.NO = Me.INPUT_MAIN_TEL_NO.Text
                rM023_CLIENT_TEL.REMARKS = ""
                dsItinerary.M023_CLIENT_TEL.AddM023_CLIENT_TELRow(rM023_CLIENT_TEL)

                Dim ZIPCODE As String = ""
                Dim PREFECTURE As String = ""
                Dim PREFECTURE_NM As String = ""
                Dim ADDRESS1 As String = ""

                If Not Me.INPUT_MAIN_ZIPCODE.Text.Equals("") Then
                    ZIPCODE = Me.INPUT_MAIN_ZIPCODE.Text
                End If

                If Not Me.INPUT_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                    PREFECTURE = Me.INPUT_MAIN_PREFECTURE.SelectedValue
                End If

                If Not Me.INPUT_MAIN_PREFECTURE.SelectedItem.Text.Equals("") Then
                    PREFECTURE_NM = Me.INPUT_MAIN_PREFECTURE.SelectedItem.Text
                End If

                If Not Me.INPUT_MAIN_ADDRESS.Text.Equals("") Then
                    ADDRESS1 = Me.INPUT_MAIN_ADDRESS.Text
                End If

                'M021_CLIENT_ADDRESS
                Dim rM021_CLIENT_ADDRESS As TriphooRR097DataSet.M021_CLIENT_ADDRESSRow
                rM021_CLIENT_ADDRESS = dsItinerary.M021_CLIENT_ADDRESS.NewM021_CLIENT_ADDRESSRow
                rM021_CLIENT_ADDRESS.RT_CD = RT_CD.Value
                rM021_CLIENT_ADDRESS.CLIENT_CD = ""
                rM021_CLIENT_ADDRESS.ADDRESS_KBN = "01"
                rM021_CLIENT_ADDRESS.ZIPCODE = ZIPCODE
                rM021_CLIENT_ADDRESS.PREFECTURE = PREFECTURE
                rM021_CLIENT_ADDRESS.ADDRESS1 = ADDRESS1
                rM021_CLIENT_ADDRESS.ADDRESS2 = ""
                rM021_CLIENT_ADDRESS.TEL_NO = ""
                rM021_CLIENT_ADDRESS.FAX_NO = ""
                rM021_CLIENT_ADDRESS.EDIT_TIME = Now
                rM021_CLIENT_ADDRESS.EDIT_RT_CD = RT_CD.Value
                rM021_CLIENT_ADDRESS.EDIT_EMP_CD = "WEB"
                rM021_CLIENT_ADDRESS.ADDRESS_KBN_NM = ""
                rM021_CLIENT_ADDRESS.PREFECTURE_NM = PREFECTURE_NM
                dsItinerary.M021_CLIENT_ADDRESS.AddM021_CLIENT_ADDRESSRow(rM021_CLIENT_ADDRESS)
            Else

            End If

            setPage(dsItinerary, dsUser)

            Select Case Me.RT_CD.Value
                Case "A0057"

                    Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")
                    Dim RES_METHOD_KBN As String = dsItinerary.PAGE_03.Rows(0)("RES_METHOD_KBN")


                    If PACKAGE_FLG AndAlso RES_METHOD_KBN.Equals("01") Then

                        Dim SALE_UNIT_TOTAL As Integer = 0
                        Dim SUP_UNIT_TOTAL As Integer = 0

                        For Each row In dsItinerary.RES_ORDER_DATA.Rows
                            Dim ACCOUNT_TYPE_CD As String = row("ACCOUNT_TYPE_CD")

                            Select Case ACCOUNT_TYPE_CD
                                Case "5701", "5703"
                                Case Else
                                    SALE_UNIT_TOTAL += (row("SALES_UNIT") * row("AMOUNT"))
                                    SUP_UNIT_TOTAL += (row("SUP_UNIT") * row("AMOUNT"))
                            End Select

                        Next

                        If SALE_UNIT_TOTAL < SUP_UNIT_TOTAL Then

                            dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = ""
                            dsItinerary.PAGE_03.Rows(0)("RES_STATUS_KBN") = "RQ"
                            dsItinerary.PAGE_03.Rows(0)("RES_METHOD_KBN") = "02"

                            For Each row In dsItinerary.PAGE_07.Rows
                                row("CLIENT_STS") = "RQ"
                                row("CONTROL_STS") = "RQ"
                            Next

                            For Each row In dsItinerary.RES_HOTEL.Rows
                                row("CLIENT_STS") = "RQ"
                                row("RES_STS") = "RQ"
                            Next

                            For Each row In dsItinerary.RES_OPTION.Rows
                                row("CLIENT_STS") = "RQ"
                                row("RES_STS") = "RQ"
                            Next
                        End If

                    End If


            End Select




            Session.Add("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value, dsItinerary)

            Dim url As String = "cart003"
            url += "?RT_CD=" & Me.RT_CD.Value
            url += "&S_CD=" & Me.S_CD.Value

            Dim ESTIMATE_NO As String = SetRRValue.setNothingValueWeb(Request.Item("ESTIMATE_NO"))
            If Not ESTIMATE_NO.Equals("") Then
                url += "&ESTIMATE_NO=" & ESTIMATE_NO
            End If

            Response.Redirect(url, True)

        Else

            ScriptManager.RegisterStartupScript(
            Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

        End If
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

#Region "顧客の取込"
    Protected Sub GET_CLIENT_INFOLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet, actionEve As String)

        Dim CEREMONY_NO As String = ""
        Dim CLIENT_NO As String = ""

        Dim ErrMsg As String = ""

        If actionEve.Contains("GET_CLIENT_INFO1LinkButton") Then

            If Me.PARTNERS_AGENT_RES_NO.Text.Equals("") Then
                ErrMsg += "御社予約番号を入力して下さい"
            Else
                '御社予約番号
                CEREMONY_NO = Me.PARTNERS_AGENT_RES_NO.Text
            End If

        ElseIf actionEve.Contains("GET_CLIENT_INFO2LinkButton") Then

        ElseIf actionEve.Contains("GET_CLIENT_INFO3LinkButton") Then

            If Me.PARTNERS_CLIENT_CD.Text.Equals("") Then
                ErrMsg += "顧客コードを入力して下さい"
            Else
                '顧客コード
                CLIENT_NO = Me.PARTNERS_CLIENT_CD.Text
            End If
        End If

        If ErrMsg.Equals("") Then
            Dim ds As New TriphooRR004s007DataSet

            Dim rPAGE_01 As TriphooRR004s007DataSet.PAGE_01Row = ds.PAGE_01.NewPAGE_01Row
            rPAGE_01.RT_CD = Me.RT_CD.Value
            rPAGE_01.CEREMONY_DATE_FROM = ""
            rPAGE_01.CEREMONY_DATE_TO = ""
            rPAGE_01.NEW_USER_NAME = ""
            rPAGE_01.MEETING_USER_NAME = ""
            rPAGE_01.SHOP_CD = ""
            rPAGE_01.GROOM_SURNAME = ""
            rPAGE_01.GROOM_NAME = ""
            rPAGE_01.BRIDE_SURNAME = ""
            rPAGE_01.BRIDE_NAME = ""
            rPAGE_01.CLIENT_NO = CLIENT_NO
            rPAGE_01.E_MAIL = ""
            rPAGE_01.TEL = ""
            rPAGE_01.CEREMONY_NO = CEREMONY_NO
            ds.PAGE_01.AddPAGE_01Row(rPAGE_01)

            Dim ReqXml As String = ds.GetXml()
            Dim UserXml As String = dsB2CUser.GetXml()

            Dim ResXml As String = B2CAPIClient.GetClientInfo(ReqXml, UserXml)
            Dim dsRes As New DataSet
            Try
                Dim strRes As System.IO.TextReader = New System.IO.StringReader(ResXml)
                dsRes.ReadXml(strRes)
            Catch ex As Exception
            End Try

            If Not dsRes Is Nothing AndAlso 0 < dsRes.Tables.Count Then

                '提携先情報
                'Me.PARTNERS_E_MAIL.Text = ""
                dsItinerary.PAGE_03.Rows(0)("PORTAL_RES_NO") = dsRes.Tables(0).Rows(0)("CEREMONY_NO")
                dsItinerary.PAGE_03.Rows(0)("B2B_CLIENT_CD") = dsRes.Tables(0).Rows(0)("CLIENT_NO")
                'Me.PARTNERS_EMP_NM.Text = ""
                Me.PARTNERS_CLIENT_CD.Text = dsRes.Tables(0).Rows(0)("CLIENT_NO")
                'Me.PARTNERS_TEL_NO.Text = dsRes.Tables(0).Rows(0)("CEREMONY_NO")

                'ご予約代表者情報
                Me.INPUT_MAIN_E_MAIL.Text = dsRes.Tables(0).Rows(0)("GROOM_E_MAIL")
                Me.INPUT_MAIN_E_MAIL_CONF.Text = ""
                Me.INPUT_MAIN_TEL_NO.Text = dsRes.Tables(0).Rows(0)("GROOM_HOME_TEL")
                Me.INPUT_MAIN_SURNAME_KANJI.Text = dsRes.Tables(0).Rows(0)("GROOM_SURNAME")
                Me.INPUT_MAIN_NAME_KANJI.Text = dsRes.Tables(0).Rows(0)("GROOM_NAME")



                'Dim NAME_NO As Integer = 1
                'dsItinerary.PAGE_04.Clear()

                Dim rPAGE_04() As DataRow = dsItinerary.PAGE_04.Select("NAME_NO = 1 AND AGE_KBN='ADT'")
                If 0 < rPAGE_04.Length Then
                    rPAGE_04(0)("FAMILY_NAME") = dsRes.Tables(0).Rows(0)("GROOM_SURNAME_ROMAN")
                    rPAGE_04(0)("FIRST_NAME") = dsRes.Tables(0).Rows(0)("GROOM_NAME_ROMAN")
                    rPAGE_04(0)("SEX") = "01"
                End If

                rPAGE_04 = dsItinerary.PAGE_04.Select("NAME_NO = 2 AND AGE_KBN='ADT'")
                If 0 < rPAGE_04.Length Then
                    rPAGE_04(0)("FAMILY_NAME") = dsRes.Tables(0).Rows(0)("BRIDE_SURNAME_ROMAN")
                    rPAGE_04(0)("FIRST_NAME") = dsRes.Tables(0).Rows(0)("BRIDE_NAME_ROMAN")
                    rPAGE_04(0)("SEX") = "02"
                End If
                'rPAGE_04 = dsItinerary.PAGE_04.NewPAGE_04Row
                'rPAGE_04.NAME_NO = 1
                'rPAGE_04.MAIN_PERSON_FLG = True
                'rPAGE_04.STATUS_KBN = "02"
                'rPAGE_04.FAMILY_NAME = dsRes.Tables(0).Rows(0)("GROOM_SURNAME_ROMAN")
                'rPAGE_04.FIRST_NAME = dsRes.Tables(0).Rows(0)("GROOM_NAME_ROMAN")
                'rPAGE_04.TITLE = "MR"
                'rPAGE_04.AGE_KBN = "ADT"
                'rPAGE_04.AGE_KBN_NM = "大人"
                'rPAGE_04.AGE = 0
                'rPAGE_04.BIRTH = "1900/01/01"
                'rPAGE_04.SEX = "01"
                'rPAGE_04.SEX_NM = ""
                'rPAGE_04.NATIONALITY = ""
                'rPAGE_04.PASSPORT_NO = ""
                'rPAGE_04.PASSPORT_DATE = "1900/01/01"
                'rPAGE_04.PASSPORT_LIMIT = "1900/01/01"
                'rPAGE_04.PASSPORT_ISSUE_COUNTRY = ""
                'rPAGE_04.TICKET_NO = ""
                'rPAGE_04.VISA_COUNTRY_CD = ""
                'rPAGE_04.VISA_NO = ""
                'rPAGE_04.VISA_LIMIT = "1900/01/01"
                'rPAGE_04.INV_FLG = False
                'rPAGE_04.GROUP_CD = ""
                'rPAGE_04.MILLAGE_CARRIER = ""
                'rPAGE_04.MILAGE_CARD = ""
                'rPAGE_04.MIDDLE_NM = ""
                'rPAGE_04.CLIENT_CD = ""
                'rPAGE_04.INSURANCE_JOIN_KBN = ""
                'rPAGE_04.FAMILY_NAME_KANJI = ""
                'rPAGE_04.FIRST_NAME_KANJI = ""
                'rPAGE_04.E_MAIL = ""
                'rPAGE_04.DOMESTIC_CONTACT_FAMILY_NAME = ""
                'rPAGE_04.DOMESTIC_CONTACT_FIRST_NAME = ""
                'rPAGE_04.DOMESTIC_CONTACT_RELATIONSHIP = ""
                'rPAGE_04.DOMESTIC_CONTACT_ADDRESS = ""
                'rPAGE_04.DOMESTIC_CONTACT_TEL = ""
                'rPAGE_04.SORT_NO = 1
                'rPAGE_04.INSURANCE_NO = ""
                'rPAGE_04.INSURANCE_PRICE = 0
                'rPAGE_04.REMARKS = ""
                'dsItinerary.PAGE_04.AddPAGE_04Row(rPAGE_04)

                'rPAGE_04 = dsItinerary.PAGE_04.NewPAGE_04Row
                'rPAGE_04.NAME_NO = 2
                'rPAGE_04.MAIN_PERSON_FLG = False
                'rPAGE_04.STATUS_KBN = "02"
                'rPAGE_04.FAMILY_NAME = dsRes.Tables(0).Rows(0)("BRIDE_SURNAME_ROMAN")
                'rPAGE_04.FIRST_NAME = dsRes.Tables(0).Rows(0)("BRIDE_NAME_ROMAN")
                'rPAGE_04.TITLE = "MS"
                'rPAGE_04.AGE_KBN = "ADT"
                'rPAGE_04.AGE_KBN_NM = "大人"
                'rPAGE_04.AGE = 0
                'rPAGE_04.BIRTH = "1900/01/01"
                'rPAGE_04.SEX = "02"
                'rPAGE_04.SEX_NM = ""
                'rPAGE_04.NATIONALITY = ""
                'rPAGE_04.PASSPORT_NO = ""
                'rPAGE_04.PASSPORT_DATE = "1900/01/01"
                'rPAGE_04.PASSPORT_LIMIT = "1900/01/01"
                'rPAGE_04.PASSPORT_ISSUE_COUNTRY = ""
                'rPAGE_04.TICKET_NO = ""
                'rPAGE_04.VISA_COUNTRY_CD = ""
                'rPAGE_04.VISA_NO = ""
                'rPAGE_04.VISA_LIMIT = "1900/01/01"
                'rPAGE_04.INV_FLG = False
                'rPAGE_04.GROUP_CD = ""
                'rPAGE_04.MILLAGE_CARRIER = ""
                'rPAGE_04.MILAGE_CARD = ""
                'rPAGE_04.MIDDLE_NM = ""
                'rPAGE_04.CLIENT_CD = ""
                'rPAGE_04.INSURANCE_JOIN_KBN = ""
                'rPAGE_04.FAMILY_NAME_KANJI = ""
                'rPAGE_04.FIRST_NAME_KANJI = ""
                'rPAGE_04.E_MAIL = ""
                'rPAGE_04.DOMESTIC_CONTACT_FAMILY_NAME = ""
                'rPAGE_04.DOMESTIC_CONTACT_FIRST_NAME = ""
                'rPAGE_04.DOMESTIC_CONTACT_RELATIONSHIP = ""
                'rPAGE_04.DOMESTIC_CONTACT_ADDRESS = ""
                'rPAGE_04.DOMESTIC_CONTACT_TEL = ""
                'rPAGE_04.SORT_NO = 2
                'rPAGE_04.INSURANCE_NO = ""
                'rPAGE_04.INSURANCE_PRICE = 0
                'rPAGE_04.REMARKS = ""
                'dsItinerary.PAGE_04.AddPAGE_04Row(rPAGE_04)
                Session.Add("Itinerary" & Me._RT_CD.Value & Me.S_CD.Value, dsItinerary)
                iniPage(dsItinerary, dsUser)
            Else

                ScriptManager.RegisterStartupScript(
                Me, Me.GetType(), "myscript", "alert('対象の顧客は存在しません。');", True)

            End If

        Else

            ScriptManager.RegisterStartupScript(
            Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

        End If

    End Sub
#End Region

#End Region

#Region "util"

#Region "DataCheck"
    Private Function DataCheck(dsItinerary As TriphooRR097DataSet, dsUser As DataSet) As String

        Dim OVERSEAS_DOMESTIC_KBN As String = dsItinerary.PAGE_03.Rows(0)("OVERSEAS_DOMESTIC_KBN")
        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")

        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")

        Dim isHotelOnly As Boolean = False

        If HOTEL_FLG And Not TICKET_FLG And Not PACKAGE_FLG Then
            isHotelOnly = True
        End If

        ' 背景色初期化
        resetBackColor()

        Dim ErrMsg As String = ""

        Dim JP_DEP_DATE As String = Request.Item("DEP_DATE")
        Dim JP_RET_DATE As String = Request.Item("RET_DATE")

        If Not Me.DEP_DATEPanel.Visible Then
            If Not dsItinerary.PAGE_03.Rows(0)("DEP_TIME").Equals("") Then
                JP_DEP_DATE = SetRRValue.setDispDate(CDate(dsItinerary.PAGE_03.Rows(0)("DEP_TIME")).ToString("yyyy/MM/dd"))
            End If
        End If

        If Not Me.RET_DATEPanel.Visible Then
            If Not dsItinerary.PAGE_03.Rows(0)("RET_TIME").Equals("") Then
                JP_RET_DATE = SetRRValue.setDispDate(CDate(dsItinerary.PAGE_03.Rows(0)("RET_TIME")).ToString("yyyy/MM/dd"))
            End If
        End If

        Dim AIR_GDS_KBN As String = "01"
        Try
            If dsItinerary.PAGE_05.Rows.Count > 0 Then
                AIR_GDS_KBN = dsItinerary.PAGE_05.Rows(0)("GDS_KBN")
            End If
        Catch ex As Exception

        End Try

        Select Case MEMBER_ADD_KBN
            Case "5"
                If Me.PARTNERS_E_MAIL.Text.Equals("") Then
                    createMsg(ErrMsg, "提携先情報　E-mailアドレスは入力必須です")
                    Me.PARTNERS_E_MAIL.BackColor = Drawing.Color.Pink
                Else
                    Dim checker As New DataCheckUtil
                    If Not checker.IsValidMailAddress(PARTNERS_E_MAIL.Text) Then
                        createMsg(ErrMsg, "提携先情報　E-mailアドレスが不正です")
                        Me.PARTNERS_E_MAIL.BackColor = Drawing.Color.Pink
                    ElseIf Me.PARTNERS_E_MAIL.Text.Contains(".@") Then
                        createMsg(ErrMsg, "提携先情報　E-mailアドレスが不正です (@マーク直前にドットが指定されています)")
                        Me.PARTNERS_E_MAIL.BackColor = Drawing.Color.Pink
                    ElseIf Me.PARTNERS_E_MAIL.Text.Contains("..") Then
                        createMsg(ErrMsg, "提携先情報　E-mailアドレスが不正です (ドットが連続しています)")
                        Me.PARTNERS_E_MAIL.BackColor = Drawing.Color.Pink
                    End If
                End If

                If Me.PARTNERS_EMP_NM.Text.Equals("") Then
                    createMsg(ErrMsg, "提携先情報　ご担当者様名は入力必須です")
                    Me.PARTNERS_EMP_NM.BackColor = Drawing.Color.Pink
                End If
        End Select

        If Me.INPUT1_MAIN_ADDRESSPanel.Visible Then
            ' 郵便番号-01
            If Me.INPUT1_MAIN_ZIPCODE.Text.Equals("") Then
                createMsg(ErrMsg, LABEL_0134)
                Me.INPUT1_MAIN_ZIPCODE.BackColor = Drawing.Color.Pink
            Else
                '入力形式
                If Not checker.checkNumeric(LABEL_0135, Me.INPUT1_MAIN_ZIPCODE.Text).Equals("") Then
                    createMsg(ErrMsg, checker.checkNumeric(LABEL_0135, Me.INPUT1_MAIN_ZIPCODE.Text))
                    Me.INPUT1_MAIN_ZIPCODE.BackColor = Drawing.Color.Pink
                End If
            End If
            ' 都道府県
            If Me.INPUT1_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                createMsg(ErrMsg, LABEL_0136)
                Me.INPUT1_MAIN_PREFECTURE.BackColor = Drawing.Color.Pink
            End If
            ' 住所
            If Me.INPUT1_MAIN_ADDRESS.Text.Equals("") Then
                createMsg(ErrMsg, LABEL_0136)
                Me.INPUT1_MAIN_ADDRESS.BackColor = Drawing.Color.Pink
            End If
        End If

        ' 予約代表者情報
        If Me.MAIN_PERSON_INPUTPanel.Visible Then
            ' E-mailアドレス
            Dim isEmailError As Boolean = False
            If Me.INPUT_MAIN_E_MAIL.Text.Equals("") Then
                createMsg(ErrMsg, LABEL_0121)
                Me.INPUT_MAIN_E_MAIL.BackColor = Drawing.Color.Pink
                isEmailError = True
            Else
                Dim _checker As New DataCheckUtil
                If Not _checker.IsValidMailAddress(INPUT_MAIN_E_MAIL.Text) Then
                    Select Case lang
                        Case "1"
                            ErrMsg += LABEL_0122 & "\n"
                        Case Else
                            ErrMsg += "E-mail address is invalid." & "\n"
                    End Select
                    Me.INPUT_MAIN_E_MAIL.BackColor = Drawing.Color.Pink
                    isEmailError = True

                ElseIf Me.INPUT_MAIN_E_MAIL.Text.Contains(".@") Then

                    ErrMsg += LABEL_0123 & "\n"
                    Me.INPUT_MAIN_E_MAIL.BackColor = Drawing.Color.Pink
                    isEmailError = True

                ElseIf Me.INPUT_MAIN_E_MAIL.Text.Contains("..") Then

                    ErrMsg += LABEL_0124 & "\n"
                    Me.INPUT_MAIN_E_MAIL.BackColor = Drawing.Color.Pink
                    isEmailError = True

                ElseIf Not checker.checkHankaku(Me.LABEL_0010.Text, Me.INPUT_MAIN_E_MAIL.Text).Equals("") Then

                    ErrMsg += checker.checkHankaku(Me.LABEL_0010.Text, Me.INPUT_MAIN_E_MAIL.Text) & "\n"
                    Me.INPUT_MAIN_E_MAIL.BackColor = Drawing.Color.Pink
                    isEmailError = True

                End If
            End If

            If Not isEmailError AndAlso Me.INPUT_MAIN_E_MAIL_CONFPanel.Visible Then
                If Not Me.INPUT_MAIN_E_MAIL.Text.Equals(Me.INPUT_MAIN_E_MAIL_CONF.Text) Then
                    ErrMsg += LABEL_0180 & "\n"
                    Me.INPUT_MAIN_E_MAIL.BackColor = Drawing.Color.Pink
                    Me.INPUT_MAIN_E_MAIL_CONF.BackColor = Drawing.Color.Pink
                End If
            End If

            ' 連絡先電話番号
            If Me.INPUT_MAIN_TEL_NOPanel.Visible Then
                If Me.INPUT_MAIN_TEL_NO.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0125)
                    Me.INPUT_MAIN_TEL_NO.BackColor = Drawing.Color.Pink
                End If
            End If

            If Me.INPUT_MAIN_TEL_NO_SEPARATEPanel.Visible Then
                If Me.INPUT_MAIN_TEL_NO_1.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0125)
                    Me.INPUT_MAIN_TEL_NO_1.BackColor = Drawing.Color.Pink
                End If
                If Me.INPUT_MAIN_TEL_NO_2.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0125)
                    Me.INPUT_MAIN_TEL_NO_2.BackColor = Drawing.Color.Pink
                End If
                If Me.INPUT_MAIN_TEL_NO_3.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0125)
                    Me.INPUT_MAIN_TEL_NO_3.BackColor = Drawing.Color.Pink
                End If
            End If

            If Me.INPUT_MAIN_SURNAME_KANJIPanel.Visible Then

                ' 氏名（姓）
                If Me.INPUT_MAIN_SURNAME_KANJI.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0126)
                    Me.INPUT_MAIN_SURNAME_KANJI.BackColor = Drawing.Color.Pink
                End If
                ' 氏名（名）
                If Me.INPUT_MAIN_NAME_KANJI.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0127)
                    Me.INPUT_MAIN_NAME_KANJI.BackColor = Drawing.Color.Pink
                End If

            End If

            If Me.INPUT_MAIN_NAME_ROMANPanel.Visible Then
                ' 氏名（ローマ字）
                If Me.INPUT_MAIN_SURNAME_ROMAN.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0128)
                    Me.INPUT_MAIN_SURNAME_ROMAN.BackColor = Drawing.Color.Pink
                Else
                    '入力形式
                    If Not checker.checkAlpha(LABEL_0129, Me.INPUT_MAIN_SURNAME_ROMAN.Text).Equals("") Then
                        createMsg(ErrMsg, checker.checkAlpha(LABEL_0129, Me.INPUT_MAIN_SURNAME_ROMAN.Text))
                        Me.INPUT_MAIN_SURNAME_ROMAN.BackColor = Drawing.Color.Pink
                    End If
                End If
                ' 氏名（ローマ字）
                If Me.INPUT_MAIN_NAME_ROMAN.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0130)
                    Me.INPUT_MAIN_NAME_ROMAN.BackColor = Drawing.Color.Pink
                    '入力形式
                Else
                    If Not checker.checkAlpha(LABEL_0131, Me.INPUT_MAIN_NAME_ROMAN.Text).Equals("") Then
                        createMsg(ErrMsg, checker.checkAlpha(LABEL_0131, Me.INPUT_MAIN_NAME_ROMAN.Text))
                        Me.INPUT_MAIN_NAME_ROMAN.BackColor = Drawing.Color.Pink
                    End If
                End If
            End If

            If Me.INPUT_MAIN_NAME_KANAPanel.Visible Then
                ' 氏名（カナ）
                If Me.INPUT_MAIN_SURNAME_KANA.Text.Equals("") Then
                    createMsg(ErrMsg, "ご予約代表者情報　姓（カナ）を入力してください")
                    Me.INPUT_MAIN_SURNAME_KANA.BackColor = Drawing.Color.Pink
                Else
                    '入力形式
                    If Not checker.checkZenkakuKana("ご予約代表者情報　姓（カナ）", Me.INPUT_MAIN_SURNAME_KANA.Text).Equals("") Then
                        createMsg(ErrMsg, checker.checkZenkakuKana("ご予約代表者情報　姓（カナ）", Me.INPUT_MAIN_SURNAME_KANA.Text))
                        Me.INPUT_MAIN_SURNAME_KANA.BackColor = Drawing.Color.Pink
                    End If
                End If
                ' 氏名（カナ）
                If Me.INPUT_MAIN_NAME_KANA.Text.Equals("") Then
                    createMsg(ErrMsg, "ご予約代表者情報　名（カナ）を入力してください")
                    Me.INPUT_MAIN_NAME_KANA.BackColor = Drawing.Color.Pink
                Else
                    '入力形式
                    If Not checker.checkZenkakuKana("ご予約代表者情報　名（カナ）", Me.INPUT_MAIN_NAME_KANA.Text).Equals("") Then
                        createMsg(ErrMsg, checker.checkZenkakuKana("ご予約代表者情報　名（カナ）", Me.INPUT_MAIN_NAME_KANA.Text))
                        Me.INPUT_MAIN_NAME_KANA.BackColor = Drawing.Color.Pink
                    End If
                End If
            End If

            ' 性別
            If Me.INPUT_MAIN_SEXPanel.Visible Then
                If Not Me.INPUT_MAIN_MAN.Checked And Not Me.INPUT_MAIN_WOMAN.Checked Then
                    createMsg(ErrMsg, LABEL_0132)
                End If
            End If
            '生年月日
            If Me.INPUT_MAIN_BIRTHPanel.Visible Then
                If Me.INPUT_MAIN_BIRTH_YYYY.Text.Equals("") Or
                           Me.INPUT_MAIN_BIRTH_MM.SelectedValue.Equals("") Or
                           Me.INPUT_MAIN_BIRTH_DD.SelectedValue.Equals("") Then
                    createMsg(ErrMsg, LABEL_0133)
                    Me.INPUT_MAIN_BIRTH_YYYY.BackColor = Drawing.Color.Pink
                    Me.INPUT_MAIN_BIRTH_MM.BackColor = Drawing.Color.Pink
                    Me.INPUT_MAIN_BIRTH_DD.BackColor = Drawing.Color.Pink
                End If
            End If

            If Me.INPUT_MAIN_ADDRESSPanel.Visible Then
                ' 郵便番号-01
                If Me.INPUT_MAIN_ZIPCODE.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0134)
                    Me.INPUT_MAIN_ZIPCODE.BackColor = Drawing.Color.Pink
                Else
                    '入力形式
                    If Not checker.checkNumeric(LABEL_0135, Me.INPUT_MAIN_ZIPCODE.Text).Equals("") Then
                        createMsg(ErrMsg, checker.checkNumeric(LABEL_0135, Me.INPUT_MAIN_ZIPCODE.Text))
                        Me.INPUT_MAIN_ZIPCODE.BackColor = Drawing.Color.Pink
                    End If
                End If
                ' 都道府県
                If Me.INPUT_MAIN_PREFECTURE.SelectedValue.Equals("") Then
                    createMsg(ErrMsg, LABEL_0136)
                    Me.INPUT_MAIN_PREFECTURE.BackColor = Drawing.Color.Pink
                End If
                ' 住所
                If Me.INPUT_MAIN_ADDRESS.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0136)
                    Me.INPUT_MAIN_ADDRESS.BackColor = Drawing.Color.Pink
                End If
            End If

            If Me.COUPON_CDPanel.Visible Then
                If Me.COUPON_CD.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0162)
                    Me.COUPON_CD.BackColor = Drawing.Color.Pink
                End If
            End If
        End If

        'FC番号重複チェック用
        Dim dtCheckFcNo As New DataTable
        dtCheckFcNo.Columns.Add("SERVICE_CD", GetType(String))
        dtCheckFcNo.Columns.Add("REMARKS", GetType(String))

        'ツアー付帯サービス
        If Me.MAIN_PERSON_TOUR_SERVICEPanel.Visible Then
            For m = 0 To Me.MAIN_PERSON_TOUR_SERVICERepeater.Items.Count - 1
                Dim SERVICE_CD As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("SERVICE_CD"), Label).Text
                Dim SERVICE_NM As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("SERVICE_NM"), Label).Text
                Dim SELECT_KBN As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("SELECT_KBN"), Label).Text
                Dim SELECT_WAY_KBN As String = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("SELECT_WAY_KBN"), Label).Text

                If SELECT_KBN.Equals("1") Then '1:必須 2:選択

                    Select Case SELECT_WAY_KBN '1:一つのみ 2:複数回答 3:フリーフォーム
                        Case "1" '1:一つのみ
                            If CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                                createMsg(ErrMsg, "ご予約代表者情報　" & SERVICE_NM & "を選択してください。")
                            End If
                        Case "2" '2:複数回答
                            Dim iCheck As Integer = 0
                            Dim TourServiceCheckRepeater As Repeater = CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                            For o = 0 To TourServiceCheckRepeater.Items.Count - 1
                                If CType(TourServiceCheckRepeater.Items(o).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked Then
                                    iCheck += 1
                                End If
                            Next

                            If iCheck = 0 Then
                                createMsg(ErrMsg, "ご予約代表者情報　" & SERVICE_NM & "を選択してください。")
                            End If

                        Case "3" '3:フリーフォーム
                            If CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text.Equals("") Then
                                createMsg(ErrMsg, "ご予約代表者情報　" & SERVICE_NM & "を入力してください。")
                                CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).BackColor = Drawing.Color.Pink
                            Else
                                If SERVICE_CD.Equals("FC") Then
                                    dtCheckFcNo.Rows.Add(SERVICE_CD, StrConv(CType(Me.MAIN_PERSON_TOUR_SERVICERepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text, VbStrConv.Narrow))


                                End If
                            End If
                    End Select
                End If
            Next
        End If

        Select Case MEMBER_ADD_KBN
            Case "3", "4"
                If Me.INPUT_MAIN_E_MAIL_B2B.Text.Equals("") Then
                    createMsg(ErrMsg, LABEL_0121)
                    Me.INPUT_MAIN_E_MAIL_B2B.BackColor = Drawing.Color.Pink
                Else
                    Dim checker As New DataCheckUtil
                    If Not checker.IsValidMailAddress(INPUT_MAIN_E_MAIL_B2B.Text) Then
                        Select Case lang
                            Case "1"
                                ErrMsg += "ご予約代表者情報　E-mailアドレスが不正です" & "\n"
                            Case Else
                                ErrMsg += "E-mail address is invalid." & "\n"
                        End Select
                        Me.INPUT_MAIN_E_MAIL_B2B.BackColor = Drawing.Color.Pink
                    ElseIf Me.INPUT_MAIN_E_MAIL_B2B.Text.Contains(".@") Then
                        ErrMsg += LABEL_0123 & "\n"
                        Me.INPUT_MAIN_E_MAIL_B2B.BackColor = Drawing.Color.Pink
                    ElseIf Me.INPUT_MAIN_E_MAIL_B2B.Text.Contains("..") Then
                        ErrMsg += LABEL_0124 & "\n"
                        Me.INPUT_MAIN_E_MAIL_B2B.BackColor = Drawing.Color.Pink
                    End If
                End If
        End Select

        ' 旅行者情報
        If Me.PASSENGERPanel.Visible Then

            Dim MAIN_PERSON_FLG As Boolean = False

            'ホテル以外用
            If Me.PASSENGER_OTHERPanel.Visible Then

                For i = 0 To Me.PASSENGER_OTHERRepeater.Items.Count - 1

                    Dim AGE_KBN As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("AGE_KBN"), Label).Text.ToUpper
                    Dim SURNAME_ROMAN As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SURNAME_ROMAN"), TextBox).Text '姓（ローマ字）
                    Dim NAME_ROMAN As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMAN"), TextBox).Text '      名（ローマ字）
                    Dim MIDDLE_NM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MIDDLE_NM"), TextBox).Text '      ミドル（ローマ字）
                    Dim FAMILY_NAME_KANA As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANA"), TextBox).Text '姓（カナ字）
                    Dim FIRST_NAME_KANA As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANA"), TextBox).Text '      名（カナ字）
                    Dim FAMILY_NAME_KANJI As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANJI"), TextBox).Text '姓（カナ字）
                    Dim FIRST_NAME_KANJI As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANJI"), TextBox).Text '      名（カナ字）
                    Dim MAN As Boolean = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MAN"), RadioButton).Checked '         性別
                    Dim WOMAN As Boolean = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("WOMAN"), RadioButton).Checked '       性別
                    Dim BIRTH_YYYY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).Text '      生年月日
                    Dim BIRTH_MM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).SelectedValue '          生年月日
                    Dim BIRTH_DD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).SelectedValue '          生年月日

                    Dim PASSPORT_NO As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_NO"), TextBox).Text '      パスポート番号
                    Dim PASSPORT_LIMIT_YYYY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_YYYY"), TextBox).Text '          パスポート有効期限
                    Dim PASSPORT_LIMIT_MM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_MM"), DropDownList).SelectedValue '          パスポート有効期限
                    Dim PASSPORT_LIMIT_DD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_LIMIT_DD"), DropDownList).SelectedValue '          パスポート有効期限

                    Dim NATIONALITY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NATIONALITY"), DropDownList).SelectedValue '      国籍
                    Dim PASSPORT_ISSUE_COUNTRY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).SelectedValue '          パスポート発行国
                    Dim VISA_COUNTRY_CD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).SelectedValue '          VISA・国名
                    Dim VISA_VALID_DATE_YYYY As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_YYYY"), TextBox).Text '          VISA有効期間
                    Dim VISA_VALID_DATE_MM As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).SelectedValue '          VISA有効期間
                    Dim VISA_VALID_DATE_DD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).SelectedValue '          VISA有効期間
                    Dim VISA_JOIN_COUNTRY_CD As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).SelectedValue '          VISA発行国
                    Dim MILEAGE_CARRIER As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_CARRIER"), DropDownList).SelectedValue '          マイレージ・航空会社
                    Dim MILEAGE_NO As String = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_NO"), TextBox).Text '          マイレージ・番号

                    Dim BIRTH As String = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD
                    Dim PASSPORT_LIMIT As String = PASSPORT_LIMIT_YYYY & "/" & PASSPORT_LIMIT_MM & "/" & PASSPORT_LIMIT_DD
                    Dim VISA_VALID_DATE As String = VISA_VALID_DATE_YYYY & "/" & VISA_VALID_DATE_MM & "/" & VISA_VALID_DATE_DD

                    'ローマ字
                    If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMANPanel"), Panel).Visible Then

                        If SURNAME_ROMAN.Equals("") Then
                            createMsg(ErrMsg, LABEL_0137)
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SURNAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                        Else
                            '入力形式
                            If Not checker.checkAlpha(LABEL_0129, SURNAME_ROMAN).Equals("") Then
                                createMsg(ErrMsg, LABEL_0163)
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SURNAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                            End If
                        End If

                        If NAME_ROMAN.Equals("") Then
                            createMsg(ErrMsg, LABEL_0138)
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                        Else
                            '入力形式
                            If Not checker.checkAlpha(LABEL_0129, NAME_ROMAN).Equals("") Then
                                createMsg(ErrMsg, LABEL_0164)
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                            End If
                        End If

                        If Not MIDDLE_NM.Equals("") Then
                            '入力形式
                            If Not checker.checkAlpha(LABEL_0129, MIDDLE_NM).Equals("") Then
                                createMsg(ErrMsg, LABEL_0164)
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MIDDLE_NM"), TextBox).BackColor = Drawing.Color.Pink
                            End If
                        End If

                    End If

                    '漢字
                    If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_KANJIPanel"), Panel).Visible Then

                        If FAMILY_NAME_KANJI.Equals("") Then
                            createMsg(ErrMsg, LABEL_0202)
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANJI"), TextBox).BackColor = Drawing.Color.Pink
                        End If

                        If FIRST_NAME_KANJI.Equals("") Then
                            createMsg(ErrMsg, LABEL_0203)
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANJI"), TextBox).BackColor = Drawing.Color.Pink
                        End If

                    End If

                    'カナ字
                    If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_KANAPanel"), Panel).Visible Then

                        If FAMILY_NAME_KANA.Equals("") Then
                            createMsg(ErrMsg, LABEL_0198)
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                        Else
                            '入力形式
                            If Not checker.checkZenkakuKana(LABEL_0196, FAMILY_NAME_KANA).Equals("") Then
                                createMsg(ErrMsg, LABEL_0200)
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                            End If
                        End If

                        If FIRST_NAME_KANA.Equals("") Then
                            createMsg(ErrMsg, LABEL_0199)
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                        Else
                            '入力形式
                            If Not checker.checkZenkakuKana(LABEL_0197, FIRST_NAME_KANA).Equals("") Then
                                createMsg(ErrMsg, LABEL_0201)
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                            End If
                        End If

                    End If

                    If Not MAN And Not WOMAN Then
                        createMsg(ErrMsg, LABEL_0139)
                    End If

                    '生年月日
                    If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).Visible Then
                        '生年月日入力必須だった場合
                        If BIRTH_YYYY.Equals("") Or BIRTH_MM.Equals("") Or BIRTH_DD.Equals("") Then
                            createMsg(ErrMsg, LABEL_0140)
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                        Else
                            Try
                                Dim dt As Date = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD

                                ' 出発日との整合性
                                Dim RET_DATE As String = ""

                                Select Case Me.RT_CD.Value
                                    Case "A0027"
                                        Try
                                            RET_DATE = JP_RET_DATE
                                        Catch ex As Exception
                                            RET_DATE = Today.AddDays(10)
                                        End Try

                                    Case Else
                                        Try
                                            RET_DATE = JP_DEP_DATE
                                        Catch ex As Exception
                                            RET_DATE = Today.AddDays(10)
                                        End Try

                                End Select

                                If CType(BIRTH, Date) > CType(RET_DATE, Date) Then
                                    createMsg(ErrMsg, LABEL_0141)
                                    CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                    CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                    CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                Else
                                    ' 年齢と年代区分の妥当性
                                    Dim iAge As Integer = CalcAgeByBirth.calc(BIRTH, RET_DATE)

                                    If AGE_KBN.Equals("ADT") Then
                                        If Not (12 <= iAge) Then
                                            createMsg(ErrMsg, LABEL_0142)
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                        ElseIf iAge > 120 Then
                                            createMsg(ErrMsg, LABEL_0143 & iAge & LABEL_0144)
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                        End If

                                    ElseIf AGE_KBN.Equals("CHD") Then
                                        If Not (iAge < 12) Then
                                            createMsg(ErrMsg, LABEL_0145)
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                        End If

                                    ElseIf AGE_KBN.Equals("INF") Then
                                        If Not (2 > iAge) Then
                                            createMsg(ErrMsg, LABEL_0146)
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                        End If
                                    End If

                                End If

                            Catch ex As Exception
                                createMsg(ErrMsg, LABEL_0147)
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                            End Try
                        End If
                    End If

                    'ＶＩＳＡ
                    If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISAPanel"), Panel).Visible Then

                        If Not VISA_VALID_DATE_YYYY.Equals("") Or Not VISA_VALID_DATE_MM.Equals("") Or Not VISA_VALID_DATE_DD.Equals("") Then
                            Try
                                Dim dt As Date = VISA_VALID_DATE
                            Catch ex As Exception
                                createMsg(ErrMsg, "VISA有効期間の入力に誤りがあります。")
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).BackColor = Drawing.Color.Pink
                            End Try
                        End If

                    End If

                    'マイレージ
                    If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGEPanel"), Panel).Visible Then

                        'どちらかが入力されていたら
                        If Not MILEAGE_CARRIER.Equals("") Or Not MILEAGE_NO.Equals("") Then

                            Dim isNG As Boolean = False

                            If MILEAGE_CARRIER.Equals("") Then
                                isNG = True
                            End If

                            If MILEAGE_NO.Equals("") Then
                                isNG = True
                            End If

                            If isNG Then
                                createMsg(ErrMsg, "マイレージ 航空会社または番号の入力に誤りがあります。")
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_CARRIER"), DropDownList).BackColor = Drawing.Color.Pink
                                CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_NO"), TextBox).BackColor = Drawing.Color.Pink
                            End If

                        End If

                    End If

                    'ツアー付帯サービス
                    If CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURPanel"), Panel).Visible Then
                        Dim TourServiceRepeater As Repeater = CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("RES_SERVICE_TOURRepeater"), Repeater)
                        For m = 0 To TourServiceRepeater.Items.Count - 1
                            Dim SERVICE_CD As String = CType(TourServiceRepeater.Items(m).FindControl("SERVICE_CD"), Label).Text
                            Dim SERVICE_NM As String = CType(TourServiceRepeater.Items(m).FindControl("SERVICE_NM"), Label).Text
                            Dim SELECT_KBN As String = CType(TourServiceRepeater.Items(m).FindControl("SELECT_KBN"), Label).Text
                            Dim SELECT_WAY_KBN As String = CType(TourServiceRepeater.Items(m).FindControl("SELECT_WAY_KBN"), Label).Text

                            If SELECT_KBN.Equals("1") Then '1:必須 2:選択

                                Select Case SELECT_WAY_KBN '1:一つのみ 2:複数回答 3:フリーフォーム
                                    Case "1" '1:一つのみ
                                        If CType(TourServiceRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                                            createMsg(ErrMsg, SERVICE_NM & "を選択してください。")
                                        End If
                                    Case "2" '2:複数回答
                                        Dim iCheck As Integer = 0
                                        Dim TourServiceCheckRepeater As Repeater = CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                                        For o = 0 To TourServiceCheckRepeater.Items.Count - 1
                                            If CType(TourServiceCheckRepeater.Items(o).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked Then
                                                iCheck += 1
                                            End If
                                        Next

                                        If iCheck = 0 Then
                                            createMsg(ErrMsg, SERVICE_NM & "を選択してください。")
                                        End If

                                    Case "3" '3:フリーフォーム
                                        If CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text.Equals("") Then
                                            createMsg(ErrMsg, SERVICE_NM & "を入力してください。")
                                            CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).BackColor = Drawing.Color.Pink
                                        Else
                                            If SERVICE_CD.Equals("FC") Then
                                                dtCheckFcNo.Rows.Add(SERVICE_CD, StrConv(CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text, VbStrConv.Narrow))
                                            End If
                                        End If
                                End Select

                            End If

                        Next
                    End If
                Next

            End If

            'ホテル用
            If Me.PASSENGER_HOTELPanel.Visible Then

                For i = 0 To Me.PASSENGER_HOTELRepeater.Items.Count - 1

                    Dim REP_HOTEL_NAME As Repeater = CType(Me.PASSENGER_HOTELRepeater.Items(i).FindControl("PASSENGER_HOTEL_NAMERepeater"), Repeater)

                    For k = 0 To REP_HOTEL_NAME.Items.Count - 1
                        Dim AGE_KBN As String = CType(REP_HOTEL_NAME.Items(k).FindControl("AGE_KBN"), Label).Text.ToUpper
                        Dim SURNAME_ROMAN As String = CType(REP_HOTEL_NAME.Items(k).FindControl("SURNAME_ROMAN"), TextBox).Text '姓（ローマ字）
                        Dim NAME_ROMAN As String = CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_ROMAN"), TextBox).Text '      名（ローマ字）
                        Dim FAMILY_NAME_KANA As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FAMILY_NAME_KANA"), TextBox).Text '姓（カナ字）
                        Dim FIRST_NAME_KANA As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FIRST_NAME_KANA"), TextBox).Text '      名（カナ字）
                        Dim FAMILY_NAME_KANJI As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FAMILY_NAME_KANJI"), TextBox).Text '姓（カナ字）
                        Dim FIRST_NAME_KANJI As String = CType(REP_HOTEL_NAME.Items(k).FindControl("FIRST_NAME_KANJI"), TextBox).Text '      名（カナ字）

                        Dim MAN As Boolean = CType(REP_HOTEL_NAME.Items(k).FindControl("MAN"), RadioButton).Checked '           性別
                        Dim WOMAN As Boolean = CType(REP_HOTEL_NAME.Items(k).FindControl("WOMAN"), RadioButton).Checked '       性別
                        Dim NOANSWER As Boolean = CType(REP_HOTEL_NAME.Items(k).FindControl("NOANSWER"), RadioButton).Checked '       性別
                        Dim BIRTH_YYYY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).Text '      生年月日
                        Dim BIRTH_MM As String = CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).Text '          生年月日
                        Dim BIRTH_DD As String = CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).Text '          生年月日
                        Dim BIRTH As String = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD

                        Dim PASSPORT_NO As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_NO"), TextBox).Text
                        Dim PASSPORT_LIMIT_YYYY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_YYYY"), TextBox).Text
                        Dim PASSPORT_LIMIT_MM As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).SelectedValue
                        Dim PASSPORT_LIMIT_DD As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).SelectedValue
                        Dim PASSPORT_ISSUE_COUNTRY As String = CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).SelectedValue

                        Dim SEX_KBN As String = ""

                        If CType(REP_HOTEL_NAME.Items(k).FindControl("SEX_FLGPanel"), Panel).Visible Then

                            If MAN Then
                                SEX_KBN = "01"
                            ElseIf WOMAN Then
                                SEX_KBN = "02"
                            ElseIf NOANSWER Then
                                SEX_KBN = "03"
                            End If

                        End If

                        If CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_ROMANPanel"), Panel).Visible Then

                            If SURNAME_ROMAN.Equals("") Then
                                createMsg(ErrMsg, LABEL_0137)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("SURNAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                            Else
                                '入力形式
                                If Not checker.checkAlpha(LABEL_0148, SURNAME_ROMAN).Equals("") Then
                                    createMsg(ErrMsg, checker.checkAlpha(LABEL_0148, SURNAME_ROMAN))
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("SURNAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                                End If
                            End If

                            If NAME_ROMAN.Equals("") Then
                                createMsg(ErrMsg, LABEL_0138)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                            Else
                                '入力形式
                                If Not checker.checkAlpha(LABEL_0149, NAME_ROMAN).Equals("") Then
                                    createMsg(ErrMsg, checker.checkAlpha(LABEL_0149, NAME_ROMAN))
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_ROMAN"), TextBox).BackColor = Drawing.Color.Pink
                                End If
                            End If
                        End If

                        '漢字
                        If CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_KANJIPanel"), Panel).Visible Then

                            If FAMILY_NAME_KANJI.Equals("") Then
                                createMsg(ErrMsg, LABEL_0202)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("FAMILY_NAME_KANJI"), TextBox).BackColor = Drawing.Color.Pink
                            End If

                            If FIRST_NAME_KANJI.Equals("") Then
                                createMsg(ErrMsg, LABEL_0203)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("FIRST_NAME_KANJI"), TextBox).BackColor = Drawing.Color.Pink
                            End If

                        End If

                        'カナ字
                        If CType(REP_HOTEL_NAME.Items(k).FindControl("NAME_KANAPanel"), Panel).Visible Then

                            If FAMILY_NAME_KANA.Equals("") Then
                                createMsg(ErrMsg, LABEL_0198)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("FAMILY_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                            Else
                                '入力形式
                                If Not checker.checkZenkakuKana(LABEL_0196, FAMILY_NAME_KANA).Equals("") Then
                                    createMsg(ErrMsg, LABEL_0200)
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("FAMILY_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                                End If
                            End If

                            If FIRST_NAME_KANA.Equals("") Then
                                createMsg(ErrMsg, LABEL_0199)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("FIRST_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                            Else
                                '入力形式
                                If Not checker.checkZenkakuKana(LABEL_0197, FIRST_NAME_KANA).Equals("") Then
                                    createMsg(ErrMsg, LABEL_0201)
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("FIRST_NAME_KANA"), TextBox).BackColor = Drawing.Color.Pink
                                End If
                            End If

                        End If

                        If CType(REP_HOTEL_NAME.Items(k).FindControl("SEX_FLGPanel"), Panel).Visible Then
                            If SEX_KBN.Equals("") Then
                                createMsg(ErrMsg, LABEL_0139)
                            End If
                        End If

                        If CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).Visible Then
                            '生年月日入力必須だった場合
                            If BIRTH_YYYY.Equals("") Or BIRTH_MM.Equals("") Or BIRTH_DD.Equals("") Then
                                createMsg(ErrMsg, LABEL_0140)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                            Else
                                Try
                                    Dim dt As Date = BIRTH_YYYY & "/" & BIRTH_MM & "/" & BIRTH_DD

                                    ' 出発日との整合性
                                    Dim RET_DATE As String = ""
                                    'If AIR_GDS_KBN.Equals("22") Then
                                    '    Try
                                    '        RET_DATE = JP_DEP_DATE
                                    '    Catch ex As Exception
                                    '        RET_DATE = Today.AddDays(10)
                                    '    End Try
                                    'Else
                                    '    Try
                                    '        RET_DATE = JP_RET_DATE
                                    '    Catch ex As Exception
                                    '        RET_DATE = Today.AddDays(10)
                                    '    End Try
                                    'End If

                                    Select Case Me.RT_CD.Value
                                        Case "A0027"
                                            Try
                                                RET_DATE = JP_RET_DATE
                                            Catch ex As Exception
                                                RET_DATE = Today.AddDays(10)
                                            End Try

                                        Case Else
                                            Try
                                                RET_DATE = JP_DEP_DATE
                                            Catch ex As Exception
                                                RET_DATE = Today.AddDays(10)
                                            End Try

                                    End Select


                                    If CType(BIRTH, Date) > CType(RET_DATE, Date) Then
                                        createMsg(ErrMsg, LABEL_0141)
                                        CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                        CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                        CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                    Else
                                        ' 年齢と年代区分の妥当性
                                        Dim iAge As Integer = CalcAgeByBirth.calc(BIRTH, RET_DATE)
                                        If AGE_KBN.Equals("ADT") Then
                                            If isHotelOnly Then
                                                If Not (2 <= iAge) Then
                                                    createMsg(ErrMsg, LABEL_0142)
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                                ElseIf iAge > 120 Then
                                                    createMsg(ErrMsg, LABEL_0143 & iAge & LABEL_0144)
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                                End If
                                            Else
                                                If Not (12 <= iAge) Then
                                                    createMsg(ErrMsg, LABEL_0142)
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                                ElseIf iAge > 120 Then
                                                    createMsg(ErrMsg, LABEL_0143 & iAge & LABEL_0144)
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                                End If
                                            End If
                                        ElseIf AGE_KBN.Equals("CHD") Then
                                            If Not iAge < 12 Then
                                                createMsg(ErrMsg, LABEL_0145)
                                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                            End If
                                        ElseIf AGE_KBN.Equals("INF") Then
                                            If Not (2 > iAge) Then
                                                createMsg(ErrMsg, LABEL_0146)
                                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                                CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                            End If
                                        End If

                                    End If


                                Catch ex As Exception
                                    createMsg(ErrMsg, LABEL_0147)
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                End Try
                            End If
                        End If

                        If CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_NOPanel"), Panel).Visible Then
                            If PASSPORT_NO.Equals("") Then
                                createMsg(ErrMsg, LABEL_0191)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_NO"), TextBox).BackColor = Drawing.Color.Pink
                            End If
                        End If

                        If CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMITPanel"), Panel).Visible Then
                            If PASSPORT_LIMIT_YYYY.Equals("") Or PASSPORT_LIMIT_MM.Equals("") Or PASSPORT_LIMIT_DD.Equals("") Then
                                createMsg(ErrMsg, LABEL_0192)
                                CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).BackColor = Drawing.Color.Pink
                            Else
                                Try
                                    Dim dt As Date = PASSPORT_LIMIT_YYYY & "/" & PASSPORT_LIMIT_MM & "/" & PASSPORT_LIMIT_DD
                                Catch ex As Exception
                                    createMsg(ErrMsg, LABEL_0194)
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_YYYY"), TextBox).BackColor = Drawing.Color.Pink
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).BackColor = Drawing.Color.Pink
                                    CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).BackColor = Drawing.Color.Pink
                                End Try

                            End If
                        End If

                        If CType(REP_HOTEL_NAME.Items(k).FindControl("PASSPORT_ISSUE_COUNTRYPanel"), Panel).Visible Then
                            If PASSPORT_ISSUE_COUNTRY.Equals("") Then
                                createMsg(ErrMsg, LABEL_0193)
                            End If
                        End If

                        'ツアー付帯サービス
                        If CType(REP_HOTEL_NAME.Items(k).FindControl("RES_SERVICE_TOURPanel"), Panel).Visible Then
                            Dim TourServiceRepeater As Repeater = CType(REP_HOTEL_NAME.Items(k).FindControl("RES_SERVICE_TOURRepeater"), Repeater)
                            For m = 0 To TourServiceRepeater.Items.Count - 1
                                Dim SERVICE_CD As String = CType(TourServiceRepeater.Items(m).FindControl("SERVICE_CD"), Label).Text
                                Dim SERVICE_NM As String = CType(TourServiceRepeater.Items(m).FindControl("SERVICE_NM"), Label).Text
                                Dim SELECT_KBN As String = CType(TourServiceRepeater.Items(m).FindControl("SELECT_KBN"), Label).Text
                                Dim SELECT_WAY_KBN As String = CType(TourServiceRepeater.Items(m).FindControl("SELECT_WAY_KBN"), Label).Text

                                If SELECT_KBN.Equals("1") Then '1:必須 2:選択

                                    Select Case SELECT_WAY_KBN '1:一つのみ 2:複数回答 3:フリーフォーム
                                        Case "1" '1:一つのみ
                                            If CType(TourServiceRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                                                createMsg(ErrMsg, SERVICE_NM & "を選択してください。")
                                            End If
                                        Case "2" '2:複数回答
                                            Dim iCheck As Integer = 0
                                            Dim TourServiceCheckRepeater As Repeater = CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                                            For o = 0 To TourServiceCheckRepeater.Items.Count - 1
                                                If CType(TourServiceCheckRepeater.Items(o).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked Then
                                                    iCheck += 1
                                                End If
                                            Next

                                            If iCheck = 0 Then
                                                createMsg(ErrMsg, SERVICE_NM & "を選択してください。")
                                            End If

                                        Case "3" '3:フリーフォーム
                                            If CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text.Equals("") Then
                                                createMsg(ErrMsg, SERVICE_NM & "を入力してください。")
                                                CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).BackColor = Drawing.Color.Pink
                                            Else
                                                If SERVICE_CD.Equals("FC") Then
                                                    dtCheckFcNo.Rows.Add(SERVICE_CD, StrConv(CType(TourServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text, VbStrConv.Narrow))
                                                End If
                                            End If
                                    End Select

                                End If

                            Next
                        End If
                    Next
                Next

            End If

        End If

        '●備考

        '出発日
        If Me.DEP_DATEPanel.Visible Then
            If JP_DEP_DATE.Equals("") Then
                createMsg(ErrMsg, LABEL_0150)
                Me.DEP_DATE.BackColor = Drawing.Color.Pink
            End If
        End If

        '現地到着時間
        If Me.LOCAL_ARR_TIMEPanel.Visible Then
            If Me.LOCAL_ARR_TIME_HH.SelectedValue.Equals("") Then
                createMsg(ErrMsg, LABEL_0168)
                Me.LOCAL_ARR_TIME_HH.BackColor = Drawing.Color.Pink
            End If

            If Me.LOCAL_ARR_TIME_MM.SelectedValue.Equals("") Then
                createMsg(ErrMsg, LABEL_0169)
                Me.LOCAL_ARR_TIME_MM.BackColor = Drawing.Color.Pink
            End If
        End If

        '現地到着便
        If Me.LOCAL_ARR_FLIGHTPanel.Visible Then
            If Me.LOCAL_ARR_CARRIER_CD.SelectedValue.Equals("") Or Me.LOCAL_ARR_FLIGHT_NO.Text.Equals("") Then
                createMsg(ErrMsg, LABEL_0170)
                Me.LOCAL_ARR_CARRIER_CD.BackColor = Drawing.Color.Pink
                Me.LOCAL_ARR_FLIGHT_NO.BackColor = Drawing.Color.Pink
            End If
        End If

        '帰国時間
        If Me.LOCAL_DEP_TIMEPanel.Visible Then
            If Me.LOCAL_DEP_TIME_HH.SelectedValue.Equals("") Then
                createMsg(ErrMsg, LABEL_0165)
                Me.LOCAL_DEP_TIME_HH.BackColor = Drawing.Color.Pink
            End If

            If Me.LOCAL_DEP_TIME_MM.SelectedValue.Equals("") Then
                createMsg(ErrMsg, LABEL_0166)
                Me.LOCAL_DEP_TIME_MM.BackColor = Drawing.Color.Pink
            End If
        End If

        '帰国便
        If Me.LOCAL_DEP_FLIGHTPanel.Visible Then
            If Me.LOCAL_DEP_CARRIER_CD.SelectedValue.Equals("") Or Me.LOCAL_DEP_FLIGHT_NO.Text.Equals("") Then
                createMsg(ErrMsg, LABEL_0167)
                Me.LOCAL_DEP_CARRIER_CD.BackColor = Drawing.Color.Pink
                Me.LOCAL_DEP_FLIGHT_NO.BackColor = Drawing.Color.Pink
            End If
        End If

        '帰国日
        If Me.RET_DATEPanel.Visible Then
            If JP_RET_DATE.Equals("") Then
                createMsg(ErrMsg, LABEL_0151)
                Me.RET_DATE.BackColor = Drawing.Color.Pink
            End If
        End If

        'ツアー付帯サービス
        If Me.RES_SERVICE_TOURPanel.Visible Then
            For m = 0 To Me.RES_SERVICE_TOURRepeater.Items.Count - 1
                Dim SERVICE_CD As String = CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_CD"), Label).Text
                Dim SERVICE_NM As String = CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_NM"), Label).Text
                Dim SELECT_KBN As String = CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("SELECT_KBN"), Label).Text
                Dim SELECT_WAY_KBN As String = CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("SELECT_WAY_KBN"), Label).Text

                If SELECT_KBN.Equals("1") Then '1:必須 2:選択

                    Select Case SELECT_WAY_KBN '1:一つのみ 2:複数回答 3:フリーフォーム
                        Case "1" '1:一つのみ
                            If CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).SelectedValue.Equals("") Then
                                createMsg(ErrMsg, SERVICE_NM & "を選択してください。")
                            End If
                        Case "2" '2:複数回答
                            Dim iCheck As Integer = 0
                            Dim TourServiceCheckRepeater As Repeater = CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("RES_SERVICE_TOUR_CHECKLISTRepeater"), Repeater)
                            For o = 0 To TourServiceCheckRepeater.Items.Count - 1
                                If CType(TourServiceCheckRepeater.Items(o).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Checked Then
                                    iCheck += 1
                                End If
                            Next

                            If iCheck = 0 Then
                                createMsg(ErrMsg, SERVICE_NM & "を選択してください。")
                            End If

                        Case "3" '3:フリーフォーム
                            If CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).Text.Equals("") Then
                                createMsg(ErrMsg, SERVICE_NM & "を入力してください。")
                                CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).BackColor = Drawing.Color.Pink
                            Else
                                If SERVICE_CD.Equals("FC") Then
                                    dtCheckFcNo.Rows.Add(SERVICE_CD, StrConv(CType(Me.RES_SERVICE_TOURRepeater.Items(m).FindControl("SERVICE_CD"), Label).Text, VbStrConv.Narrow))
                                End If
                            End If
                    End Select

                End If

            Next
        End If

        If Me.REGULATIONPanel.Visible Then
            If Not Me.Agree1CheckBox.Checked Then
                createMsg(ErrMsg, LABEL_0152)
            End If
        End If

        If Me.PASSPORT_AGREEPanel.Visible Then
            If Not Me.Agree2CheckBox.Checked Then
                createMsg(ErrMsg, LABEL_0153)
            End If
        End If

        If 0 < dtCheckFcNo.Rows.Count Then

            Dim dvCheckFcNo As DataView = dtCheckFcNo.DefaultView
            Dim dtCheck As DataTable = dvCheckFcNo.ToTable(True, "SERVICE_CD", "REMARKS")

            If Not dtCheck.Rows.Count = dtCheckFcNo.Rows.Count Then
                createMsg(ErrMsg, "入力したファンクラブ番号が重複しています。")
            End If

        End If


        Return ErrMsg

    End Function
#End Region

#Region "isValid"
    ''' <summary>
    ''' DBNullや""(brank)でない場合にTrueを返す
    ''' </summary>
    Public Function _isValid(ByVal value As Object) As Boolean

        Dim result As Boolean = False

        If value Is DBNull.Value OrElse value Is Nothing Then
            result = False
        ElseIf value.Equals("") Then
            result = False
        Else
            result = True
        End If

        Return result
    End Function
#End Region

#Region "createMsg"
    Private Sub createMsg(ByRef msg As String, str As String)

        If msg.Contains(str) Then
            Exit Sub
        End If

        msg += str & "\n"

    End Sub
#End Region

#Region "resetBackColor"
    Private Sub resetBackColor()

        Me.INPUT_MAIN_E_MAIL.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_TEL_NO.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_TEL_NO_1.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_TEL_NO_2.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_TEL_NO_3.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_SURNAME_KANJI.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_NAME_KANJI.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_SURNAME_ROMAN.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_NAME_ROMAN.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_BIRTH_YYYY.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_BIRTH_MM.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_BIRTH_DD.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_ZIPCODE.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_PREFECTURE.BackColor = Drawing.Color.White
        Me.INPUT_MAIN_ADDRESS.BackColor = Drawing.Color.White
        Me.INPUT1_MAIN_ZIPCODE.BackColor = Drawing.Color.White
        Me.INPUT1_MAIN_PREFECTURE.BackColor = Drawing.Color.White
        Me.INPUT1_MAIN_ADDRESS.BackColor = Drawing.Color.White
        Me.DEP_DATE.BackColor = Drawing.Color.White
        Me.RET_DATE.BackColor = Drawing.Color.White
        Me.COUPON_CD.BackColor = Drawing.Color.White

        Me.LOCAL_DEP_DATE.BackColor = Drawing.Color.White
        Me.LOCAL_DEP_TIME_HH.BackColor = Drawing.Color.White
        Me.LOCAL_DEP_TIME_MM.BackColor = Drawing.Color.White

        Me.LOCAL_ARR_DATE.BackColor = Drawing.Color.White
        Me.LOCAL_ARR_TIME_HH.BackColor = Drawing.Color.White
        Me.LOCAL_ARR_TIME_MM.BackColor = Drawing.Color.White

        Me.LOCAL_ARR_CARRIER_CD.BackColor = Drawing.Color.White
        Me.LOCAL_ARR_FLIGHT_NO.BackColor = Drawing.Color.White

        Me.LOCAL_DEP_CARRIER_CD.BackColor = Drawing.Color.White
        Me.LOCAL_DEP_FLIGHT_NO.BackColor = Drawing.Color.White

        Me.PARTNERS_E_MAIL.BackColor = Drawing.Color.White
        Me.PARTNERS_EMP_NM.BackColor = Drawing.Color.White

        For i = 0 To Me.PASSENGER_OTHERRepeater.Items.Count - 1
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("SURNAME_ROMAN"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("NAME_ROMAN"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANA"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANA"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FAMILY_NAME_KANJI"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("FIRST_NAME_KANJI"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("PASSPORT_NO"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_COUNTRY_CD"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_YYYY"), TextBox).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_MM"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_VALID_DATE_DD"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("VISA_JOIN_COUNTRY_CD"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_CARRIER"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.PASSENGER_OTHERRepeater.Items(i).FindControl("MILEAGE_NO"), TextBox).BackColor = Drawing.Color.White
        Next

        For i = 0 To Me.PASSENGER_HOTELRepeater.Items.Count - 1
            Dim subRepeater As Repeater = CType(Me.PASSENGER_HOTELRepeater.Items(i).FindControl("PASSENGER_HOTEL_NAMERepeater"), Repeater)
            For k = 0 To subRepeater.Items.Count - 1
                CType(subRepeater.Items(k).FindControl("SURNAME_ROMAN"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("NAME_ROMAN"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("FAMILY_NAME_KANA"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("FIRST_NAME_KANA"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("FAMILY_NAME_KANJI"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("FIRST_NAME_KANJI"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("BIRTH_YYYY"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("BIRTH_MM"), DropDownList).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("BIRTH_DD"), DropDownList).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("PASSPORT_NO"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("PASSPORT_LIMIT_YYYY"), TextBox).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("PASSPORT_LIMIT_MM"), DropDownList).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("PASSPORT_LIMIT_DD"), DropDownList).BackColor = Drawing.Color.White
                CType(subRepeater.Items(k).FindControl("PASSPORT_ISSUE_COUNTRY"), DropDownList).BackColor = Drawing.Color.White
            Next
        Next

        For i = 0 To Me.PASSENGER_HOTELRepeater.Items.Count - 1
            Dim subRepeater As Repeater = CType(Me.PASSENGER_HOTELRepeater.Items(i).FindControl("PASSENGER_HOTEL_NAMERepeater"), Repeater)
            For k = 0 To subRepeater.Items.Count - 1
                Dim ServiceRepeater As Repeater = CType(subRepeater.Items(k).FindControl("RES_SERVICE_TOURRepeater"), Repeater)
                For m = 0 To ServiceRepeater.Items.Count - 1
                    CType(ServiceRepeater.Items(m).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).BackColor = Drawing.Color.White
                    CType(ServiceRepeater.Items(m).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).BackColor = Drawing.Color.White
                Next
            Next
        Next

        For i = 0 To Me.RES_SERVICE_TOURRepeater.Items.Count - 1
            CType(Me.RES_SERVICE_TOURRepeater.Items(i).FindControl("SERVICE_PRICE_CD_DROPDOWNLIST"), DropDownList).BackColor = Drawing.Color.White
            CType(Me.RES_SERVICE_TOURRepeater.Items(i).FindControl("RES_SERVICE_TOUR_INPUT"), TextBox).BackColor = Drawing.Color.White
        Next

    End Sub
#End Region

#Region "GetMealType"
    Private Function GetMealType(ByRef dsItinerary As TriphooRR097DataSet, ByVal DisplayText As String) As DataTable

        Dim dtMeal As New DataTable
        dtMeal.Columns.Add("VALUE", GetType(String))
        dtMeal.Columns.Add("TEXT", GetType(String))
        dtMeal.Columns.Add("FLIGHT_NO", GetType(String))
        dtMeal.Columns.Add("PRICE", GetType(String))

        'Regexオブジェクトを作成 
        Dim r As New System.Text.RegularExpressions.Regex("[(].*[)]", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        '分割
        Dim s() As String = DisplayText.Replace("Please Select a Meal Type:", "").Replace(";", ",").Split(",".ToCharArray, StringSplitOptions.RemoveEmptyEntries)

        For Each Str As String In s

            'Str : 523-Beancurd Kung Pao(@549.00JPY@)

            Dim m As System.Text.RegularExpressions.Match = r.Match(Str) '523-Beancurd Kung Pao(@549.00JPY@) ⇒ (@549.00JPY@)

            '名称部分解析
            Dim NAME_INFO As String = Str.Replace(m.Value, "").TrimStart(" ") '523-Beancurd Kung Pao(@549.00JPY@) ⇒ 523-Beancurd Kung Pao
            Dim MEAL_NM As String = ""
            Dim FLIGHT_NO As String = ""

            '料金部分解析
            Dim PRICE_INFO As String = ""
            Dim CURR_CD As String = ""
            Dim PRICE As String = ""

            Dim ss() As String = NAME_INFO.Split("-")

            If ss.Length >= 2 Then
                'MMパターン
                For i = 0 To ss.Length - 1
                    If i = 0 Then
                        FLIGHT_NO = ss(i) '523
                    Else
                        MEAL_NM += ss(i)  'Beancurd Kung Pao
                    End If
                Next

                PRICE_INFO = m.Value.Replace("(@", "").Replace("@)", "") '(@549.00JPY@) ⇒ 549.00JPY
                CURR_CD = PRICE_INFO.Substring(PRICE_INFO.Length - 3, 3) '549.00JPY ⇒ JPY
                PRICE = PRICE_INFO.Substring(0, PRICE_INFO.Length - 3)      '549.00JPY ⇒ 549.00

            Else
                'JQパターン
                'Please Select a Meal Type: 1 (VGML - 3558.00 JPY) ,2 (SPML - 3558.00 JPY)

                PRICE_INFO = m.Value.Replace(" ", "").Replace("(", "").Replace(")", "") '(@549.00JPY@) ⇒ 549.00JPY

                Dim sss() As String = PRICE_INFO.Split("-")

                MEAL_NM += sss(0)  'Beancurd Kung Pao
                CURR_CD = sss(1).Substring(sss(1).Length - 3, 3) '549.00JPY ⇒ JPY
                PRICE = sss(1).Substring(0, sss(1).Length - 3)      '549.00JPY ⇒ 549.00

            End If

            If CURR_CD.Equals("JPY") Then
                PRICE = CInt(PRICE)
            Else
                'JPY以外は返さない
                Continue For
            End If

            If FLIGHT_NO.Equals("") Then
                For Each sbrow In dsItinerary.PAGE_07.Rows
                    'Dim rMeal As DataRow = dtMeal.NewRow
                    'rMeal("VALUE") = NAME_INFO
                    'rMeal("TEXT") = MEAL_NM & " (" & MoneyComma.addYen2(CInt(PRICE), LABEL_0154) & ")"
                    'rMeal("FLIGHT_NO") = sbrow("SEGMENT_SEQ")
                    'rMeal("PRICE") = PRICE
                    'dtMeal.Rows.Add(rMeal)

                    Dim rRES_SERVICE As TriphooRR097DataSet.RES_SERVICERow = dsItinerary.RES_SERVICE.NewRES_SERVICERow
                    rRES_SERVICE.FLIGHT_SEGMENT_LINE_NO = sbrow("SEGMENT_SEQ")
                    rRES_SERVICE.SERVICE_KBN = "2"
                    rRES_SERVICE.SERVICE_NM = MEAL_NM
                    rRES_SERVICE.SERVICE_CD = NAME_INFO
                    rRES_SERVICE.SALES_CURR_CD = "JPY"
                    rRES_SERVICE.SALE_PRICE = PRICE
                    rRES_SERVICE.SUP_CURR_CD = "JPY"
                    rRES_SERVICE.SUP_PRICE = PRICE
                    dsItinerary.RES_SERVICE.AddRES_SERVICERow(rRES_SERVICE)
                Next
            Else
                'Dim rMeal As DataRow = dtMeal.NewRow
                'rMeal("VALUE") = NAME_INFO
                'rMeal("TEXT") = MEAL_NM & " (" & MoneyComma.addYen2(CInt(PRICE), LABEL_0154) & ")"
                'rMeal("FLIGHT_NO") = FLIGHT_NO
                'rMeal("PRICE") = PRICE
                'dtMeal.Rows.Add(rMeal)

                Dim rRES_SERVICE As TriphooRR097DataSet.RES_SERVICERow = dsItinerary.RES_SERVICE.NewRES_SERVICERow
                rRES_SERVICE.FLIGHT_SEGMENT_LINE_NO = FLIGHT_NO
                rRES_SERVICE.SERVICE_KBN = "2"
                rRES_SERVICE.SERVICE_NM = MEAL_NM & " (" & MoneyComma.addYen2(CInt(PRICE), LABEL_0154) & ")"
                rRES_SERVICE.SERVICE_CD = NAME_INFO
                rRES_SERVICE.SALES_CURR_CD = "JPY"
                rRES_SERVICE.SALE_PRICE = PRICE
                rRES_SERVICE.SUP_CURR_CD = "JPY"
                rRES_SERVICE.SUP_PRICE = PRICE
                dsItinerary.RES_SERVICE.AddRES_SERVICERow(rRES_SERVICE)

            End If

        Next

        Return dtMeal

    End Function
#End Region

#Region "GetBuggage"
    Private Sub GetBuggage(ByVal xmlDoc As XmlDocument, ByVal DisplayText As String, ByRef dsItinerary As TriphooRR097DataSet, GOING_RETURN As String)

        'Dim dtBuggage As New DataTable
        'dtBuggage.Columns.Add("VALUE", GetType(String))
        'dtBuggage.Columns.Add("DISPLAY_NAME", GetType(String))
        'dtBuggage.Columns.Add("NAME", GetType(String))
        'dtBuggage.Columns.Add("PRICE", GetType(String))

        Dim COUNT_NUMBER As Integer = 1

        If GOING_RETURN.Equals("02") Then
            COUNT_NUMBER = 2
        End If

        'Please Select Baggage Weight: 1 (1bags-20Kg), 2 (1bags-25Kg), 3 (1bags-30Kg), 4 (1bags-40Kg)

        'Regexオブジェクトを作成 
        Dim r As New System.Text.RegularExpressions.Regex("[(].*[)]", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        '分割
        Dim s() As String = DisplayText.Replace("Please Select Outward Luggage Option: ", "").Replace("Please Select Return Luggage Option: ", "").Replace(";", ",").Split(",".ToCharArray, StringSplitOptions.RemoveEmptyEntries)

        'If s.Length > 0 Then
        '    dtBuggage.Rows.Add("", LABEL_0114, "0")
        'End If

        For Each Str As String In s

            '正規表現と一致する対象を1つ検索 

            '2 (1 bags - 20Kg total - 2600.00 JPY)⇒(1 bags - 20Kg total - 2600.00 JPY)
            Dim m As System.Text.RegularExpressions.Match = r.Match(Str)

            '2
            Dim val As String = Str.Replace(m.Value, "").Replace(" ", "")
            Dim txtArray() As String = m.Value.Replace("(", "").Replace(")", "").Split("-")

            If txtArray.Length <> 3 Then
                Continue For
            End If

            Dim Quantity As String = txtArray(0).Replace(" ", "") ' 1bags
            Dim Weight As String = txtArray(1).Replace("total", "").Replace(" ", "") ' 20Kg
            Dim Amount As String = txtArray(2).Replace(" ", "") ' 2600.00JPY

            If Not Amount.EndsWith("JPY") Then
                Continue For
            End If

            Amount = Amount.Replace("JPY", "")

            'Dim rBuggage As DataRow = dtBuggage.NewRow
            'rBuggage("VALUE") = val
            'rBuggage("DISPLAY_NAME") = Quantity & "(" & Weight & ")    ＋" & MoneyComma.add(CInt(Amount), "0") & LABEL_0171
            'rBuggage("NAME") = Quantity & "(" & Weight & ")"
            'rBuggage("PRICE") = Amount
            'dtBuggage.Rows.Add(rBuggage)


            Dim rRES_SERVICE As TriphooRR097DataSet.RES_SERVICERow = dsItinerary.RES_SERVICE.NewRES_SERVICERow
            rRES_SERVICE.FLIGHT_SEGMENT_LINE_NO = COUNT_NUMBER
            rRES_SERVICE.SERVICE_KBN = "1"
            rRES_SERVICE.SERVICE_NM = Quantity & "(" & Weight & ")"
            rRES_SERVICE.SERVICE_CD = val
            rRES_SERVICE.SALES_CURR_CD = "JPY"
            rRES_SERVICE.SALE_PRICE = Amount
            rRES_SERVICE.SUP_CURR_CD = "JPY"
            rRES_SERVICE.SUP_PRICE = Amount
            dsItinerary.RES_SERVICE.AddRES_SERVICERow(rRES_SERVICE)

        Next

    End Sub
#End Region

#Region "CreateDropDownListItems"
    Private Sub CreateDropDownListItems(ByVal dtM222_PACKAGE_SERVICE_SUB As DataTable, DropDownListControl As DropDownList)

        'dtM222_PACKAGE_SERVICE_SUB.Tables("M222_PACKAGE_SERVICE").Columns.Add("SERVICE_PRICE_NM_DISP", GetType(String))

        'For Each row In dsM222_PACKAGE_SERVICE.Tables("M222_PACKAGE_SERVICE").Rows

        '    Dim SERVICE_PRICE_NM As String = row("SERVICE_PRICE_NM")

        '    If Not CInt(row("SALES_PRICE")) = 0 Then
        '        If CStr(row("SALES_PRICE")).StartsWith("-") Then
        '            SERVICE_PRICE_NM += "(" & MoneyComma.add(row("SALES_PRICE"), "0") & "円)"
        '        Else
        '            SERVICE_PRICE_NM += "(＋" & MoneyComma.add(row("SALES_PRICE"), "0") & "円)"
        '        End If
        '    End If

        '    row("SERVICE_PRICE_NM_DISP") = SERVICE_PRICE_NM

        'Next

        Dim _ListItem As New ListItem
        _ListItem.Text = ""
        _ListItem.Value = ""
        DropDownListControl.Items.Add(_ListItem)

        For Each row In dtM222_PACKAGE_SERVICE_SUB.Rows

            Dim FROM_STOCK_COUNT As Integer = row("FROM_STOCK_COUNT")
            Dim STOCK_COUNT As Integer = row("STOCK_COUNT")
            Dim SERVICE_PRICE_NM As String = row("SERVICE_PRICE_NM")
            Dim SALES_PRICE As Integer = row("SALES_PRICE")

            _ListItem = New ListItem

            If FROM_STOCK_COUNT = 0 And STOCK_COUNT = 0 Then
            Else
                If STOCK_COUNT = 0 Then
                    _ListItem.Attributes.Add("disabled", "disabled")
                    SERVICE_PRICE_NM += "(〆切)"
                Else
                    If Not SALES_PRICE = 0 Then
                        If CStr(SALES_PRICE).StartsWith("-") Then
                            SERVICE_PRICE_NM += "(" & MoneyComma.add(SALES_PRICE, "0") & "円)"
                        Else
                            SERVICE_PRICE_NM += "(＋" & MoneyComma.add(SALES_PRICE, "0") & "円)"
                        End If
                    End If
                End If
            End If

            _ListItem.Text = SERVICE_PRICE_NM
            _ListItem.Value = row("SERVICE_PRICE_CD")

            DropDownListControl.Items.Add(_ListItem)
        Next

    End Sub
#End Region

#Region "CreateCheckBoxItems"
    Private Sub CreateCheckBoxItems(ByVal dtM222_PACKAGE_SERVICE_SUB As DataTable, CheckBoxRepeater As Repeater)

        CheckBoxRepeater.DataSource = dtM222_PACKAGE_SERVICE_SUB
        CheckBoxRepeater.DataBind()

        For i = 0 To dtM222_PACKAGE_SERVICE_SUB.Rows.Count - 1
            Dim FROM_STOCK_COUNT As Integer = dtM222_PACKAGE_SERVICE_SUB.Rows(i)("FROM_STOCK_COUNT")
            Dim STOCK_COUNT As Integer = dtM222_PACKAGE_SERVICE_SUB.Rows(i)("STOCK_COUNT")
            Dim SERVICE_PRICE_NM As String = dtM222_PACKAGE_SERVICE_SUB.Rows(i)("SERVICE_PRICE_NM")
            Dim SALES_PRICE As Integer = dtM222_PACKAGE_SERVICE_SUB.Rows(i)("SALES_PRICE")

            If FROM_STOCK_COUNT = 0 And STOCK_COUNT = 0 Then
            Else
                If STOCK_COUNT = 0 Then
                    SERVICE_PRICE_NM += "(〆切)"
                    CType(CheckBoxRepeater.Items(i).FindControl("SERVICE_PRICE_CD_CHECKBOX"), CheckBox).Enabled = False
                Else
                    If Not SALES_PRICE = 0 Then
                        If CStr(SALES_PRICE).StartsWith("-") Then
                            SERVICE_PRICE_NM += "(" & MoneyComma.add(SALES_PRICE, "0") & "円)"
                        Else
                            SERVICE_PRICE_NM += "(＋" & MoneyComma.add(SALES_PRICE, "0") & "円)"
                        End If
                    End If
                End If
            End If
            CType(CheckBoxRepeater.Items(i).FindControl("SERVICE_PRICE_NM_DISP"), Label).Text = SERVICE_PRICE_NM
        Next

        'For Each row In dtM222_PACKAGE_SERVICE_SUB.Rows

        '    Dim _ListItem As New ListItem
        '    Dim SERVICE_PRICE_NM As String = row("SERVICE_PRICE_NM")
        '    If row("STOCK_COUNT") = 0 Then
        '        _ListItem.Attributes.Add("disabled", "disabled")
        '        SERVICE_PRICE_NM += "(〆切)"
        '    Else
        '        If Not CInt(row("SALES_PRICE")) = 0 Then
        '            If CStr(row("SALES_PRICE")).StartsWith("-") Then
        '                SERVICE_PRICE_NM += "(" & MoneyComma.add(row("SALES_PRICE"), "0") & "円)"
        '            Else
        '                SERVICE_PRICE_NM += "(＋" & MoneyComma.add(row("SALES_PRICE"), "0") & "円)"
        '            End If
        '        End If
        '    End If

        '    _ListItem.Text = SERVICE_PRICE_NM
        '    _ListItem.Value = row("SERVICE_PRICE_CD")

        '    DropDownListControl.Items.Add(_ListItem)
        'Next

    End Sub
#End Region

#End Region

#Region "言語対応"
    Private Sub setlang(lang As String)

        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/cart/" & lang & "/cart002.xml")
        Dim rXml As DataRow = dsXml.Tables("LABEL").Rows(0)

        Me.LABEL_0001.Text = rXml("LABEL_0001")
        Me.LABEL_0002.Text = rXml("LABEL_0002")
        Me.LABEL_0003.Text = rXml("LABEL_0003")
        Me.LABEL_0004.Text = rXml("LABEL_0004")
        Me.LABEL_0005.Text = rXml("LABEL_0005")
        Me.LABEL_0006.Text = rXml("LABEL_0006")
        'Me.LABEL_0007.Text = rXml("LABEL_0007")
        Me.LABEL_0008.Text = rXml("LABEL_0008")
        Me.LABEL_0009.Text = rXml("LABEL_0009")
        Me.LABEL_0010.Text = rXml("LABEL_0010")
        'Me.INPUT_MAIN_E_MAIL.Attributes("placeholder") = rXml("INPUT_MAIN_E_MAIL_PLACEHOLDER")
        Me.LABEL_0011.Text = rXml("LABEL_0011")
        Me.LABEL_0012.Text = rXml("LABEL_0012")
        Me.INPUT_MAIN_TEL_NO.Attributes("placeholder") = rXml("INPUT_MAIN_TEL_NO_PLACEHOLDER")
        Me.LABEL_0013.Text = rXml("LABEL_0013")
        Me.LABEL_0014.Text = rXml("LABEL_0014")
        Me.INPUT_MAIN_ZIPCODE.Attributes("placeholder") = rXml("INPUT_MAIN_ZIPCODE_PLACEHOLDER")
        Me.LABEL_0015.Text = rXml("LABEL_0015")
        Me.LABEL_0016.Text = rXml("LABEL_0016")
        Me.LABEL_0017.Text = rXml("LABEL_0017")
        Me.LABEL_0018.Text = rXml("LABEL_0018")
        Me.LABEL_0019.Text = rXml("LABEL_0019")
        Me.INPUT_MAIN_ADDRESS.Attributes("placeholder") = rXml("INPUT_MAIN_ADDRESS_PLACEHOLDER")
        Me.LABEL_0020.Text = rXml("LABEL_0020")
        Me.LABEL_0021.Text = rXml("LABEL_0021")
        Me.INPUT_MAIN_MAN.Text = rXml("INPUT_MAIN_MAN")
        Me.INPUT_MAIN_WOMAN.Text = rXml("INPUT_MAIN_WOMAN")
        Me.LABEL_0022.Text = rXml("LABEL_0022")
        Me.LABEL_0023.Text = rXml("LABEL_0023")
        Me.INPUT_MAIN_BIRTH_YYYY.Attributes("placeholder") = rXml("INPUT_MAIN_BIRTH_YYYY_PLACEHOLDER")
        Me.LABEL_0024.Text = rXml("LABEL_0024")
        Me.LABEL_0025.Text = rXml("LABEL_0025")
        Me.LABEL_0026.Text = rXml("LABEL_0026")
        Me.INPUT_MAIN_SURNAME_KANJI.Attributes("placeholder") = rXml("INPUT_MAIN_SURNAME_KANJI_PLACEHOLDER")
        Me.LABEL_0027.Text = rXml("LABEL_0027")
        Me.INPUT_MAIN_NAME_KANJI.Attributes("placeholder") = rXml("INPUT_MAIN_NAME_KANJI_PLACEHOLDER")
        Me.LABEL_0028.Text = rXml("LABEL_0028")
        Me.LABEL_0029.Text = rXml("LABEL_0029")
        Me.LABEL_0030.Text = rXml("LABEL_0030")
        Me.INPUT_MAIN_SURNAME_ROMAN.Attributes("placeholder") = rXml("INPUT_MAIN_SURNAME_ROMAN_PLACEHOLDER")
        Me.LABEL_0031.Text = rXml("LABEL_0031")
        Me.INPUT_MAIN_NAME_ROMAN.Attributes("placeholder") = rXml("INPUT_MAIN_NAME_ROMAN_PLACEHOLDER")
        Me.LABEL_0032.Text = rXml("LABEL_0032")
        Me.LABEL_0033.Text = rXml("LABEL_0033")
        Me.LABEL_0034.Text = rXml("LABEL_0034")
        'Me.INPUT_MAIN_E_MAIL_B2B.Attributes("placeholder") = rXml("INPUT_MAIN_E_MAIL_B2B_PLACEHOLDER")
        Me.LABEL_0035.Text = rXml("LABEL_0035")
        Me.LABEL_0036.Text = rXml("LABEL_0036")
        Me.LABEL_0037.Text = rXml("LABEL_0037")
        Me.LABEL_0038.Text = rXml("LABEL_0038")
        Me.LABEL_0039.Text = rXml("LABEL_0039")
        Me.LABEL_0040.Text = rXml("LABEL_0040")
        Me.LABEL_0041.Text = rXml("LABEL_0041")
        Me.LABEL_0042.Text = rXml("LABEL_0042")
        Me.INPUT1_MAIN_ZIPCODE.Attributes("placeholder") = rXml("INPUT1_MAIN_ZIPCODE_PLACEHOLDER")
        Me.LABEL_0043.Text = rXml("LABEL_0043")
        Me.LABEL_0044.Text = rXml("LABEL_0044")
        Me.LABEL_0045.Text = rXml("LABEL_0045")
        Me.INPUT1_MAIN_ADDRESS.Attributes("placeholder") = rXml("INPUT1_MAIN_ADDRESS_PLACEHOLDER")
        Me.LABEL_0046.Text = rXml("LABEL_0046")
        Me.LABEL_0047.Text = rXml("LABEL_0047")
        Me.LABEL_0048.Text = rXml("LABEL_0048")
        Me.MAIN_PERSON_CHANGELinkButton.Text = rXml("MAIN_PERSON_CHANGELinkButton")
        Me.LABEL_0049.Text = rXml("LABEL_0049")
        Me.LABEL_0050.Text = rXml("LABEL_0050")
        Me.LABEL_0051.Text = rXml("LABEL_0051")
        Me.LABEL_0052.Text = rXml("LABEL_0052")
        Me.LABEL_0053 = rXml("LABEL_0053_COM")
        Me.LABEL_0054 = rXml("LABEL_0054_COM")
        Me.LABEL_0055 = rXml("LABEL_0055_COM")
        Me.LABEL_0056 = rXml("LABEL_0056_COM")
        Me.LABEL_0057 = rXml("LABEL_0057_COM")
        Me.LABEL_0058 = rXml("LABEL_0058_COM")
        Me.LABEL_0059 = rXml("LABEL_0059_COM")
        Me.LABEL_0060 = rXml("LABEL_0060_COM")
        Me.LABEL_0061 = rXml("LABEL_0061_COM")
        Me.LABEL_0062 = rXml("LABEL_0062_COM")
        Me.LABEL_0063 = rXml("LABEL_0063_COM")
        Me.LABEL_0064 = rXml("LABEL_0064_COM")
        Me.LABEL_0065 = rXml("LABEL_0065_COM")
        Me.LABEL_0066 = rXml("LABEL_0066_COM")
        Me.LABEL_0067 = rXml("LABEL_0067_COM")
        Me.LABEL_0068 = rXml("LABEL_0068_COM")
        Me.LABEL_0069 = rXml("LABEL_0069_COM")
        Me.LABEL_0070 = rXml("LABEL_0070_COM")
        Me.LABEL_0071 = rXml("LABEL_0071_COM")
        Me.LABEL_0072 = rXml("LABEL_0072_COM")
        Me.LABEL_0073 = rXml("LABEL_0073_COM")
        Me.LABEL_0074 = rXml("LABEL_0074_COM")
        Me.LABEL_0075 = rXml("LABEL_0075_COM")
        Me.LABEL_0076 = rXml("LABEL_0076_COM")
        Me.LABEL_0077 = rXml("LABEL_0077_COM")
        Me.LABEL_0078 = rXml("LABEL_0078_COM")
        Me.LABEL_0079 = rXml("LABEL_0079_COM")
        Me.LABEL_0080 = rXml("LABEL_0080_COM")
        Me.LABEL_0081 = rXml("LABEL_0081_COM")
        Me.LABEL_0082 = rXml("LABEL_0082_COM")
        Me.LABEL_0083 = rXml("LABEL_0083_COM")
        Me.LABEL_0084 = rXml("LABEL_0084_COM")
        Me.LABEL_0085 = rXml("LABEL_0085_COM")
        Me.LABEL_0086 = rXml("LABEL_0086_COM")
        Me.LABEL_0087 = rXml("LABEL_0087_COM")
        Me.LABEL_0088 = rXml("LABEL_0088_COM")
        Me.LABEL_0089 = rXml("LABEL_0089_COM")
        Me.LABEL_0090 = rXml("LABEL_0090_COM")
        Me.LABEL_0091 = rXml("LABEL_0091_COM")
        Me.LABEL_0092 = rXml("LABEL_0092_COM")
        Me.LABEL_0093.Text = rXml("LABEL_0093")
        Me.LABEL_0094.Text = rXml("LABEL_0094")
        Me.LABEL_0095.Text = rXml("LABEL_0095")
        Me.LABEL_0096.Text = rXml("LABEL_0096")
        Me.LABEL_0097.Text = rXml("LABEL_0097")
        Me.LABEL_0098.Text = rXml("LABEL_0098")
        Me.LABEL_0099.Text = rXml("LABEL_0099")
        Me.LABEL_0100.Text = rXml("LABEL_0100")
        Me.LABEL_0101.Text = rXml("LABEL_0101")
        Me.LABEL_0106.Text = rXml("LABEL_0106")
        Me.LABEL_0107.Text = rXml("LABEL_0107")
        Me.LABEL_0108.Text = rXml("LABEL_0108")
        Me.LABEL_0109.Text = rXml("LABEL_0109")
        Me.LABEL_0110.Text = rXml("LABEL_0110")
        Me.LABEL_0111.Text = rXml("LABEL_0111")
        Me.BackLinkButton.Text = rXml("BackLinkButton")
        Me.CONFIRMLinkButton.Text = rXml("CONFIRMLinkButton")
        Me.LABEL_0112.Text = rXml("LABEL_0112")
        Me.LABEL_0112_2.Text = rXml("LABEL_0112")
        Me.LABEL_0113.Text = rXml("LABEL_0113")
        Me.LABEL_0114 = rXml("LABEL_0114_COM")
        Me.LABEL_0115 = rXml("LABEL_0115_COM")
        Me.LABEL_0116 = rXml("LABEL_0116_COM")
        Me.LABEL_0117 = rXml("LABEL_0117_COM")
        Me.LABEL_0118 = rXml("LABEL_0118_COM")
        Me.LABEL_0119 = rXml("LABEL_0119_COM")
        Me.LABEL_0120 = rXml("LABEL_0120_COM")
        Me.LABEL_0121 = rXml("LABEL_0121_COM")
        Me.LABEL_0122 = rXml("LABEL_0122_COM")
        Me.LABEL_0123 = rXml("LABEL_0123_COM")
        Me.LABEL_0124 = rXml("LABEL_0124_COM")
        Me.LABEL_0125 = rXml("LABEL_0125_COM")
        Me.LABEL_0126 = rXml("LABEL_0126_COM")
        Me.LABEL_0127 = rXml("LABEL_0127_COM")
        Me.LABEL_0128 = rXml("LABEL_0128_COM")
        Me.LABEL_0129 = rXml("LABEL_0129_COM")
        Me.LABEL_0130 = rXml("LABEL_0130_COM")
        Me.LABEL_0131 = rXml("LABEL_0131_COM")
        Me.LABEL_0132 = rXml("LABEL_0132_COM")
        Me.LABEL_0133 = rXml("LABEL_0133_COM")
        Me.LABEL_0134 = rXml("LABEL_0134_COM")
        Me.LABEL_0135 = rXml("LABEL_0135_COM")
        Me.LABEL_0136 = rXml("LABEL_0136_COM")
        Me.LABEL_0137 = rXml("LABEL_0137_COM")
        Me.LABEL_0138 = rXml("LABEL_0138_COM")
        Me.LABEL_0139 = rXml("LABEL_0139_COM")
        Me.LABEL_0140 = rXml("LABEL_0140_COM")
        Me.LABEL_0141 = rXml("LABEL_0141_COM")
        Me.LABEL_0142 = rXml("LABEL_0142_COM")
        Me.LABEL_0143 = rXml("LABEL_0143_COM")
        Me.LABEL_0144 = rXml("LABEL_0144_COM")
        Me.LABEL_0145 = rXml("LABEL_0145_COM")
        Me.LABEL_0146 = rXml("LABEL_0146_COM")
        Me.LABEL_0147 = rXml("LABEL_0147_COM")
        Me.LABEL_0148 = rXml("LABEL_0148_COM")
        Me.LABEL_0149 = rXml("LABEL_0149_COM")
        Me.LABEL_0150 = rXml("LABEL_0150_COM")
        Me.LABEL_0151 = rXml("LABEL_0151_COM")
        Me.LABEL_0152 = rXml("LABEL_0152_COM")
        Me.LABEL_0153 = rXml("LABEL_0153_COM")
        Me.LABEL_0154 = rXml("LABEL_0154_COM")
        Me.LABEL_0155 = rXml("LABEL_0155_COM")
        Me.LABEL_0156 = rXml("LABEL_0156_COM")
        Me.LABEL_0157 = rXml("LABEL_0157_COM")
        Me.LABEL_0158 = rXml("LABEL_0158_COM")
        Me.LABEL_0159 = rXml("LABEL_0159_COM")
        Me.LABEL_0160 = rXml("LABEL_0160_COM")
        Me.LABEL_0161 = rXml("LABEL_0161_COM")
        Me.LABEL_0162 = rXml("LABEL_0162_COM")
        Me.LABEL_0163 = rXml("LABEL_0163_COM")
        Me.LABEL_0164 = rXml("LABEL_0164_COM")
        Me.LABEL_0165 = rXml("LABEL_0165_COM")
        Me.LABEL_0166 = rXml("LABEL_0166_COM")
        Me.LABEL_0167 = rXml("LABEL_0167_COM")
        Me.LABEL_0168 = rXml("LABEL_0168_COM")
        Me.LABEL_0169 = rXml("LABEL_0169_COM")
        Me.LABEL_0170 = rXml("LABEL_0170_COM")
        Me.LABEL_0171 = rXml("LABEL_0171_COM")
        Me.LABEL_0172.Text = rXml("LABEL_0172")
        Me.LABEL_0173.Text = rXml("LABEL_0173")
        Me.LABEL_0174.Text = rXml("LABEL_0174")
        Me.LABEL_0175.Text = rXml("LABEL_0175")
        Me.LABEL_0177.Text = rXml("LABEL_0177")
        Me.LABEL_0178.Text = rXml("LABEL_0178")
        Me.LABEL_0179.Text = rXml("LABEL_0179")
        Me.LABEL_0180 = rXml("LABEL_0180_COM")
        Me.LABEL_0181.Text = rXml("LABEL_0181")
        Me.LABEL_0182.Text = rXml("LABEL_0182")
        Me.LABEL_0183.Text = rXml("LABEL_0183")
        Me.LABEL_0184.Text = rXml("LABEL_0184")
        Me.LABEL_0185.Text = rXml("LABEL_0185")
        Me.LABEL_0186.Text = rXml("LABEL_0186")
        Me.LABEL_0187.Text = rXml("LABEL_0187")
        Me.LABEL_0188 = rXml("LABEL_0188")
        Me.LABEL_0189 = rXml("LABEL_0189")
        Me.LABEL_0190 = rXml("LABEL_0190")
        Me.LABEL_0191 = rXml("LABEL_0191")
        Me.LABEL_0192 = rXml("LABEL_0192")
        Me.LABEL_0193 = rXml("LABEL_0193")
        Me.LABEL_0194 = rXml("LABEL_0194")
        Me.LABEL_0195 = rXml("LABEL_0195")
        Me.LABEL_0196 = rXml("LABEL_0196")
        Me.LABEL_0197 = rXml("LABEL_0197")
        Me.LABEL_0198 = rXml("LABEL_0198")
        Me.LABEL_0199 = rXml("LABEL_0199")
        Me.LABEL_0200 = rXml("LABEL_0200")
        Me.LABEL_0201 = rXml("LABEL_0201")
        Me.LABEL_0202 = rXml("LABEL_0202")
        Me.LABEL_0203 = rXml("LABEL_0203")
        Me.LABEL_0204 = rXml("LABEL_0204")
        Me.LABEL_0205 = rXml("LABEL_0205")

        Dim MAIL_DOMAIN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MAIL_DOMAIN")
        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")

        Select Case MEMBER_ADD_KBN
            Case "3", "4"
                Me.LABEL_0006.Text = rXml("LABEL_0176")
        End Select

        Me.LABEL_0181.Text = Me.LABEL_0181.Text.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0182.Text = Me.LABEL_0182.Text.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0183.Text = Me.LABEL_0183.Text.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0184.Text = Me.LABEL_0184.Text.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0185.Text = Me.LABEL_0185.Text.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0186.Text = Me.LABEL_0186.Text.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)
        Me.LABEL_0187.Text = Me.LABEL_0187.Text.Replace("@@MAIL_DOMAIN@@", MAIL_DOMAIN)

        If Not lang.Equals("1") Then
            Me.Agree1CheckBox.Text = rXml("LABEL_0110")
            Me.LABEL_0110.Text = ""
        Else
            Me.Agree1CheckBox.Text = "同意します"
        End If

        'WOWOW独自対応
        'If Me.RT_CD.Value.Equals("WOW") Then
        '    Me.LABEL_0010.Text = "WEB会員ID"
        '    Me.LABEL_0178.Text = "WEB会員ID(確認)"
        '    Me.LABEL_0122 = "ご予約代表者情報　WEB会員IDが不正です"
        '    Me.LABEL_0123 = "ご予約代表者情報　WEB会員IDが不正です (@マーク直前にドットが指定されています)"
        '    Me.LABEL_0124 = "ご予約代表者情報　WEB会員IDが不正です (ドットが連続しています)"
        'End If

    End Sub
#End Region


End Class
