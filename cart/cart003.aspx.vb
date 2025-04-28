Imports TriphooConfig.Src.ApplicationContext
Imports tw.src.util
Imports twClient
Imports twClient.TriphooB2CAPI
Imports UsaelFrameWork.src.datacheck

Partial Class page_cart_cart003
    Inherits System.Web.UI.Page

#Region "Common Parameter"

    'src.util
    Dim CartUtil As CartUtil
    Dim checker As New CommonDataCheck
    Dim CommonUtil As CommonUtil
    Dim CreateWebServiceManager As New CreateWebServiceManager
    Dim CommonWebAuthentication As New CommonWebAuthentication
    Dim E_Connect As E_Connect
    Dim ParameterUtil As New ParameterUtil
    Dim SetValue As New SetValue
    Dim MoneyComma As New MoneyComma
    Dim WebSessionUtil As WebSessionUtil

    ' NSSOL負荷性能検証 2023/02/20
    'Dim logger As NSTSTLogger

    'TriphooRRUtil.src.util
    Dim CalcAgeByBirth As New TriphooRRUtil.src.util.CalcAgeByBirth
    Dim SetRRValue As New TriphooRRUtil.src.util.SetValue

    'WebService
    Dim TriphooRMClient As TriphooRMWebService.Service
    Dim B2CAPIClient As TriphooB2CAPI.Service
    Dim wwwB2CClient As New TriphooB2CAPI.Service

    'DataSet
    Dim dsRTUser As UserDataSet
    Dim dsB2CUser As DataSet
    Dim dswwwB2CUser As DataSet
    Dim dsUser As New DataSet
    Dim lang As String = "1"
    Dim PANKUZU001URL As String = ""

    Public LABEL_0039 As String = ""
    Public LABEL_0040 As String = ""
    Public LABEL_0041 As String = ""
    Public LABEL_0042 As String = ""
    Public LABEL_0043 As String = ""
    Public LABEL_0044 As String = ""
    Public LABEL_0045 As String = ""
    Public LABEL_0046 As String = ""
    Public LABEL_0047 As String = ""
    Public LABEL_0048 As String = ""
    Public LABEL_0049 As String = ""
    Public LABEL_0050 As String = ""
    Public LABEL_0051 As String = ""
    Public LABEL_0052 As String = ""
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
    Public LABEL_0067 As String = ""
    Public LABEL_0075 As String = ""
    Public LABEL_0076 As String = ""
    Public LABEL_0077 As String = ""
    Public LABEL_0078 As String = ""
    Public LABEL_0083 As String = ""
    Public LABEL_0086 As String = ""
    Public LABEL_0087 As String = ""
    Public LABEL_0091 As String = ""
    Public LABEL_0095 As String = ""
    Public LABEL_0096 As String = ""
    Public LABEL_0101 As String = ""
    Public LABEL_0102 As String = ""

    Dim ESTIMATE_NO As String = ""
    Private AF As String = ""
    Private DEMO_FLG As Boolean = False

#End Region

#Region "初期処理"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' ロガー初期化 NSSOL負荷性能検証 2023/02/20
        'logger = New NSTSTLogger("cart003", "D:\", Request.Item("RT_CD"))

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "Page_Load()", 95, "開始", HttpContext.Current.Session.SessionID)

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
                Session("lang") = SetRRValue.setNothingValueWeb(Request.Item("lang"))
            End If
            If Not Session("lang") Is Nothing Then
                lang = Session("lang")
            End If
            Me.HD_LANG.Value = lang

            Try
                DEMO_FLG = dsRTUser.M137_RT_SITE_CD.Rows(0)("DEMO_FLG")
            Catch ex As Exception
            End Try

            '●インスタンス化
            TriphooRMClient = CreateWebServiceManager.CreateTriphooRMClient(Me.RT_CD.Value, Me.S_CD.Value)
            B2CAPIClient = CreateWebServiceManager.CreateTriphooB2CAPIClient(Me.RT_CD.Value, Me.S_CD.Value)
            dsB2CUser = CreateWebServiceManager.CreateTriphooB2CAPIUser(Me.RT_CD.Value, Me.S_CD.Value, Request)
            WebSessionUtil = New WebSessionUtil(Me.RT_CD.Value, Me.S_CD.Value)

            ' セッション存在チェック 2025.3.13
            If Not WebSessionUtil.SessionKeyCheck(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value) Then
                Throw New Exception("セッション(Itinerary)エラー : RT_CD=" & Me._RT_CD.Value & " , SITE_CD=" & Me.S_CD.Value)
            End If

            wwwB2CClient.Url = "https://www.triphoo.jp/TriphooB2CAPI/service.asmx"
            wwwB2CClient.Timeout = 600000

            CommonUtil = New CommonUtil(Me.RT_CD.Value, Me.S_CD.Value, Request)
            CartUtil = New CartUtil(Me.RT_CD.Value, Me.S_CD.Value, Request, lang)
            E_Connect = New E_Connect(Me.RT_CD.Value, Me.S_CD.Value, DEMO_FLG)

            '●ぱんくずリンク先設定
            PANKUZU001URL = "cart001.aspx?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&skey=" & Me.SESSION_NO.Value
            Dim PANKUZU002URL As String = "cart002.aspx?RT_CD=" & Me.RT_CD.Value & "&S_CD=" & Me.S_CD.Value & "&mt=modify" & "&skey=" & Me.SESSION_NO.Value

            ESTIMATE_NO = SetRRValue.setNothingValueWeb(Request.Item("ESTIMATE_NO"))
            If Not ESTIMATE_NO.Equals("") Then
                PANKUZU001URL += "&ESTIMATE_NO=" & ESTIMATE_NO
                PANKUZU002URL += "&ESTIMATE_NO=" & ESTIMATE_NO
            End If
            'PANKUZU001URL = SetRRValue.setNothingValueWeb(PANKUZU001URL)
            'PANKUZU002URL = SetRRValue.setNothingValueWeb(PANKUZU002URL)

            Me.PANKUZU001LinkButton.Attributes.Add("href", PANKUZU001URL)
            Me.PANKUZU002LinkButton.Attributes.Add("href", PANKUZU002URL)

            '●セッション 更新
            Dim dsItinerary As New TriphooRR097DataSet

            'Dim SessionCheck As Boolean = True
            'SessionCheck = CommonWebAuthentication.SessionCheck(Me.RT_CD.Value, Me.S_CD.Value, "Itinerary", dsItinerary)

            'If Not SessionCheck Or dsItinerary.PAGE_03.Rows.Count = 0 Then
            '    Response.Redirect(PANKUZU001URL, True)
            'End If

            dsItinerary = WebSessionUtil.GetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsB2CUser)

            If dsItinerary Is Nothing Then
                Throw New Exception("セッション(Itinerary)エラー : RT_CD=" & Me._RT_CD.Value & " , SITE_CD=" & Me.S_CD.Value)
            End If

            '●言語対応
            setlang(lang)

            dsUser = Session("user" & Me._RT_CD.Value & Me.S_CD.Value)

            '●スクリーン設定
            ScreenSet(dsItinerary)

            If Not IsPostBack Then

                Dim auth As String = SetRRValue.setNothingValueWeb(Request.Item("auth"))
                If auth.Equals("secured") Then

                    ' /* 本人認証 (3DS) ページから遷移 */
                    Dim dsM031_RT_APP_KBN As DataSet = TriphooRMClient.SelectM031_RT_APP_KBNGateway(Me.RT_CD.Value, "06", dsUser)
                    Dim USER_ID As String = ""
                    Dim PASSWORD As String = ""
                    Try
                        USER_ID = SetRRValue.setDBNullValue(dsM031_RT_APP_KBN.Tables("M031_RT_APP_KBN").Rows(0)("USER_ID"))
                        PASSWORD = SetRRValue.setDBNullValue(dsM031_RT_APP_KBN.Tables("M031_RT_APP_KBN").Rows(0)("PASSWORD"))
                    Catch ex As Exception
                    End Try

                    Dim MD As String = SetRRValue.setNothingValueWeb(Request.Item("MD"))
                    Dim PaRes As String = SetRRValue.setNothingValueWeb(Request.Item("PaRes"))

                    Dim RES_ORDER_ID As String = MD
                    Dim RES_3D_SECURE_AUTHKEY As String = PaRes

                    Dim RES_CARD_KIND As String = dsItinerary.E_CONNECT.Rows(0)("CARD_KIND")
                    Dim RES_SESSION_ID As String = dsItinerary.E_CONNECT.Rows(0)("SESSION_ID")

                    Dim okFlg As Boolean = False
                    Dim DupeErrMsg As String = ""

                    ' M204対応　2022/08/26 照井
                    Dim M204_FLG As Boolean = False
                    Dim dsM204_RT_SITE_CD_CARD_KBN As DataSet = B2CAPIClient.SelectM204_RT_SITE_CD_CARD_KBNGateway(Me.RT_CD.Value, Me.S_CD.Value, "", "", dsB2CUser)

                    If Not dsM204_RT_SITE_CD_CARD_KBN Is Nothing AndAlso dsM204_RT_SITE_CD_CARD_KBN.Tables(0).Rows.Count > 0 Then
                        M204_FLG = True
                        Dim SETTLE_PROXY_COMPANY_KBN As String = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("SETTLE_PROXY_COMPANY_KBN"))
                        Dim M204_USER_ID As String = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("USER_ID"))
                        If Not M204_USER_ID.Equals("") AndAlso Not SETTLE_PROXY_COMPANY_KBN.Equals("") Then
                            USER_ID = E_Connect.GetUSER_ID_BY_SETTLE_PROXY_COMPANY_KBN(SETTLE_PROXY_COMPANY_KBN)
                            PASSWORD = "TOKEN"
                        End If
                    End If

                    Select Case USER_ID

                        Case "GMO", "SMBCGMO"

                            RES_ORDER_ID = dsItinerary.E_CONNECT.Rows(0)("ORDER_ID")

                            Dim GmoUtil As New GmoUtil(USER_ID, DEMO_FLG)

                            ' Dupe Check
                            Dim dsW020_RT_CREDIT_CERTIFIED_FIELD As DataSet = B2CAPIClient.SelectW020_RT_CREDIT_CERTIFIED_FIELDGateway(Me.RT_CD.Value, RES_ORDER_ID, dsB2CUser)
                            If dsW020_RT_CREDIT_CERTIFIED_FIELD.Tables("W020_RT_CREDIT_CERTIFIED_FIELD").Rows.Count = 0 Then

                                If M204_FLG Then

                                    ' /* 3DS2.0 */
                                    Dim AccessID As String = RES_SESSION_ID
                                    Dim AccessPass As String = dsItinerary.E_CONNECT.Rows(0)("3D_SECURE_AUTHKEY")
                                    Dim _3D_SECURE_URL As String = SetRRValue.setDBNullValue(dsItinerary.E_CONNECT.Rows(0)("3D_SECURE_URL")) ' チャレンジ後はCallenge
                                    Dim Param As String = SetRRValue.setNothingValueWeb(Request.Item("Param"))
                                    Dim ChallengeUrl As String = ""

                                    ' チャレンジ済か否かは文字列ではなく追加フラグで対応 2023.8.25 Mitsuta
                                    'Dim isChallenge As Boolean = False
                                    'If _3D_SECURE_URL.Contains("Tds2StubChallenge") Then
                                    '    isChallenge = True
                                    'End If
                                    Dim isChallenge As Boolean = SetRRValue.SetRowValue(dsItinerary.E_CONNECT.Rows(0), "IS_CHALLENGE", False)

                                    If Not AccessID.Equals("") Then

                                        ' 認証実行. チャレンジ後は不要
                                        If Not isChallenge Then
                                            okFlg = GmoUtil.Tds2Auth(dsItinerary, Me.RT_CD.Value, Me.S_CD.Value, RES_ORDER_ID, AccessID, AccessPass, Param, ChallengeUrl)
                                        End If

                                        ' チャレンジフラグ初期化
                                        If dsItinerary.E_CONNECT.Columns.Contains("IS_CHALLENGE") Then
                                            dsItinerary.E_CONNECT.Rows(0)("IS_CHALLENGE") = False
                                        End If

                                        ' チャレンジURLが返却されたらチャレンジする
                                        If Not ChallengeUrl.Equals("") Then
                                            dsItinerary.E_CONNECT.Rows(0)("3D_SECURE_URL") = ChallengeUrl

                                            If Not dsItinerary.E_CONNECT.Columns.Contains("IS_CHALLENGE") Then
                                                dsItinerary.E_CONNECT.Columns.Add("IS_CHALLENGE", GetType(Boolean))
                                            End If
                                            dsItinerary.E_CONNECT.Rows(0)("IS_CHALLENGE") = True

                                            'Session.Add("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value, dsItinerary)
                                            WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)

                                            Response.Redirect(ChallengeUrl, True)
                                        End If

                                        ' 認証結果取得. チャレンジ後だけ
                                        If isChallenge Then
                                            okFlg = GmoUtil.Tds2Result(dsItinerary, Me.RT_CD.Value, Me.S_CD.Value, RES_ORDER_ID, AccessID, AccessPass, Param)
                                        End If

                                        ' 決済実行
                                        If okFlg Then
                                            okFlg = GmoUtil.SecureTran2(dsItinerary, Me.RT_CD.Value, Me.S_CD.Value, RES_ORDER_ID, AccessID, AccessPass, Param)
                                        End If

                                    End If
                                Else
                                    If Not MD.Equals("") And Not PaRes.Equals("") Then

                                        dsItinerary.E_CONNECT.Rows(0)("3D_SECURE_AUTHKEY") = PaRes
                                        Dim AuthKey As String = dsItinerary.E_CONNECT.Rows(0)("AUTH_KEY")

                                        okFlg = GmoUtil.Order3dSecure(dsItinerary, Me.RT_CD.Value, Me.S_CD.Value, RES_ORDER_ID, PaRes, MD)

                                        If Not okFlg Then
                                            okFlg = GmoUtil.OrderAlter(Me.RT_CD.Value, Me.S_CD.Value, RES_ORDER_ID, "VOID", RES_CARD_KIND, AuthKey, "", dsItinerary)
                                        Else

                                            Dim dtTransfer As New DataTable("REQUEST")
                                            dtTransfer.Columns.Add("RT_CD", GetType(String))
                                            dtTransfer.Columns.Add("CREDIT_CERTIFIED_FIELD", GetType(String))

                                            dtTransfer.Rows.Add(Me.RT_CD.Value, RES_ORDER_ID)

                                            Dim dsTransfer As New DataSet
                                            dsTransfer.Merge(dtTransfer)

                                            B2CAPIClient.InsertW020_RT_CREDIT_CERTIFIED_FIELDMapper(dsTransfer, dsB2CUser)

                                        End If
                                    End If

                                End If

                                ' ログ
                                Try
                                    ' エラーの場合はログを書き込む。OKの場合は CartUtil.PayUpdateMypege にて
                                    If Not okFlg Then
                                        B2CAPIClient.TriphooRR097s038act001Mapper(dsItinerary, dsB2CUser)
                                        dsItinerary.RES_VENDER_LOG.Clear()
                                    End If
                                Catch ex As Exception
                                End Try

                            Else
                                okFlg = False
                                DupeErrMsg += "当記録は既に作成済みの可能性がございます。\n"
                                DupeErrMsg += "しばらくお待ち頂き、お申込み完了メールが届いていないかご確認下さい。"
                            End If

                        Case "ECON"
                            Dim sessionToken As String = SetRRValue.setNothingValueWeb(Request.Item("sessionToken"))
                            Dim cd3secResFlg As String = SetRRValue.setNothingValueWeb(Request.Item("cd3secResFlg"))
                            RES_ORDER_ID = dsItinerary.E_CONNECT.Rows(0)("ORDER_ID")

                            Select Case cd3secResFlg
                                Case "0", "1", "2" '1:3Dセキュア対応, 2:3Dセキュア非対応
                                    okFlg = PayEconToken(dsItinerary, sessionToken)
                                Case Else
                                    okFlg = False
                            End Select

                        Case "SMBCSTATION"
                            If Not MD.Equals("") And Not PaRes.Equals("") Then
                                dsItinerary.E_CONNECT.Rows(0)("3D_SECURE_AUTHKEY") = PaRes

                                Dim SmbcStationUtil As New SmbcStationUtil(Me.RT_CD.Value, Me.S_CD.Value)
                                okFlg = SmbcStationUtil.order3dSecure(Me.RT_CD.Value, RES_CARD_KIND, RES_ORDER_ID, RES_SESSION_ID, RES_3D_SECURE_AUTHKEY, Me.S_CD.Value)
                            End If

                        Case "VERITRANS"
                            RES_ORDER_ID = dsItinerary.E_CONNECT.Rows(0)("ORDER_ID")

                            Dim mpiMstatus As String = SetRRValue.setNothingValueWeb(Request.Item("mpiMstatus"))
                            Dim vResultCode As String = SetRRValue.setNothingValueWeb(Request.Item("vResultCode"))

                            ' 結果の検証
                            If mpiMstatus.Equals("success") Then
                                If vResultCode.Length >= 8 Then
                                    Select Case vResultCode.Substring(4, 4)
                                        Case "A001", "A004"
                                            okFlg = True
                                    End Select
                                End If
                            End If

                            ' ログ (ファイル保存)
                            Try
                                Dim _RESPONSE As String = ""
                                _RESPONSE += "mpiMstatus: " & mpiMstatus
                                _RESPONSE += ", vResultCode: " & vResultCode

                                Dim rRES_VENDER_LOG As TriphooRR097DataSet.RES_VENDER_LOGRow = dsItinerary.RES_VENDER_LOG.NewRES_VENDER_LOGRow
                                rRES_VENDER_LOG.SEQ = 0
                                rRES_VENDER_LOG.TYPE = "VeriTrans"
                                rRES_VENDER_LOG.GDS_KBN = ""
                                rRES_VENDER_LOG.ACTION = "3DS"
                                rRES_VENDER_LOG.REQUEST = ""
                                rRES_VENDER_LOG.REQUEST_TIME = "1900/01/01"
                                rRES_VENDER_LOG.RESPONSE = _RESPONSE
                                rRES_VENDER_LOG.RESPONSE_TIME = Now
                                rRES_VENDER_LOG.EDIT_TIME = Now
                                rRES_VENDER_LOG.EDIT_EMP_CODE = "tw"
                                dsItinerary.RES_VENDER_LOG.AddRES_VENDER_LOGRow(rRES_VENDER_LOG)

                                Dim dsLog As New E_ConnectLogDataSet
                                Dim drLog As E_ConnectLogDataSet.LOGRow = dsLog.LOG.NewLOGRow
                                drLog.TIME = Now
                                drLog.URL = ""
                                drLog.POST = ""
                                drLog.RESPONSE = _RESPONSE
                                drLog.CARD_FAMILY_NAME = ""
                                drLog.CARD_FIRST_NAME = ""
                                drLog.FLT_FAMILY_NAME = ""
                                drLog.FLT_FIRST_NAME = ""
                                drLog.DEP_DATE = ""
                                dsLog.LOG.AddLOGRow(drLog)

                                Dim logPath As String = ParameterUtil.eConnectLog & "veritrans4g\" & Me.RT_CD.Value & "\"
                                IO.Directory.CreateDirectory(logPath)
                                logPath = logPath & "\" & RES_ORDER_ID & "-3DC.log"
                                dsLog.WriteXml(logPath)

                                ' エラーの場合はログを書き込む。OKの場合は CartUtil.PayUpdateMypege にて
                                If Not okFlg Then
                                    B2CAPIClient.TriphooRR097s038act001Mapper(dsItinerary, dsB2CUser)
                                    dsItinerary.RES_VENDER_LOG.Clear()
                                End If

                            Catch ex As Exception
                            End Try

                            '' NGの場合キャンセルする
                            'If Not okFlg Then
                            '    Dim ordAmount As Integer = 0
                            '    For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
                            '        ordAmount += row("SALES_SUB_TOTAL")
                            '    Next
                            '    Dim VeriTrans4GUtil = New VeriTrans4GUtil(Me.RT_CD.Value, Me.S_CD.Value)
                            '    VeriTrans4GUtil.OrderCancel(RES_ORDER_ID, ordAmount)
                            'End If

                    End Select

                    If okFlg Then
                        '/***予約処理***/
                        CallBook(dsItinerary)
                    Else

                        '決済実行 失敗した場合は元に戻す　
                        dsItinerary.PAGE_03(0)("SETTLE_STATUS_KBN") = "01"
                        WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)

                        If Not DupeErrMsg.Equals("") Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & DupeErrMsg & "');", True)
                        Else
                            ' 与信・失敗
                            Dim ErrMsg As String = ""
                            ErrMsg = LABEL_0044 & "\n"
                            ErrMsg += "\n"
                            ErrMsg += LABEL_0045 & "\n"
                            ErrMsg += LABEL_0046 & "\n"
                            ErrMsg += LABEL_0047 & "\n"
                            ErrMsg += LABEL_0048 & "\n"
                            Select Case Me.RT_CD.Value
                                Case "RT01", "ATR"
                                    ErrMsg += LABEL_0095 & "\n" ' ・ご利用になるクレジットカード発行会社の3Dセキュア（クレジットカード本人認証サービス）のパスワード登録がされていない。
                                    ErrMsg += LABEL_0096 & "\n" ' ※現在、不正利用防止の為お支払い時に3Dセキュアのパスワード認証が必須となっております。
                            End Select

                            ErrMsg += "\n"
                            ErrMsg += LABEL_0049 & "\n"
                            ErrMsg += LABEL_0050
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)
                        End If
                    End If
                End If

                '●ページ設定
                iniPage(dsItinerary)

            Else
                '●アクション群
                Dim actionEve As String = Request.Item("__EVENTTARGET")
                If actionEve.Contains("RES_COMMITLinkButton") Or
                   actionEve.Contains("RES_COMMIT_TOKENLinkButton") Then  '予約確定
                    CONFIRMLinkButton_Click(dsItinerary, dsUser, False)
                ElseIf actionEve.Contains("BackLinkButton") Then    '戻る
                    Response.Redirect(PANKUZU002URL, True)
                End If
            End If

            Dim TOUR_CANCELPOLICY_FLG As Boolean = False
            Dim TERMS_USE_FLG As Boolean = False

            '文言個別対応
            If Me.TOUR_CANCELPOLICY_ModalButton.Visible Or Me.TOUR_CANCELPOLICY_LinkButton.Visible Then
                TOUR_CANCELPOLICY_FLG = True
            End If
            If Me.TERMS_USE_LinkButton.Visible Or Me.TERMS_USE_ModalButton.Visible Then
                TERMS_USE_FLG = True
            End If

            If TOUR_CANCELPOLICY_FLG And TERMS_USE_FLG Then
                '両方表示
                Me.LABEL_0103.Text = LABEL_0102

            ElseIf TOUR_CANCELPOLICY_FLG And Not TERMS_USE_FLG Then
                'キャンセル規約のみ表示
                Me.LABEL_0103.Text = LABEL_0078

            ElseIf Not TOUR_CANCELPOLICY_FLG And TERMS_USE_FLG Then
                '利用規約のみ表示
                Me.LABEL_0103.Text = LABEL_0101

            End If

        Catch ex As Exception

            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "Page_Load()", 484, "終了（エラーorリダイレクト）", HttpContext.Current.Session.SessionID)

            ' ログファイルへの書き込み NSSOL負荷性能検証 2023/02/09
            'logger.WriteLogToFile()

            '●エラー処理
            If Not (ex.Message.StartsWith("スレッドを中止") Or ex.Message.StartsWith("Thread was being aborted")) Then
                Dim ConcreteException As New src.common.ConcreteException
                ConcreteException.Exception(Request.Item("RT_CD"), Request.Item("S_CD"), "cart003", ex)
                Response.Redirect("../err/err002.aspx?RT_CD=" & Request.Item("RT_CD") & "&S_CD=" & Request.Item("S_CD"))
            End If
        End Try

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "Page_Load()", 498, "終了", HttpContext.Current.Session.SessionID)

        ' ログファイルへの書き込み NSSOL負荷性能検証 2023/02/09
        'logger.WriteLogToFile()

    End Sub
#End Region

#Region "スクリーン設定"
    Private Sub ScreenSet(dsItinerary As TriphooRR097DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "ScreenSet()", 510, "開始", HttpContext.Current.Session.SessionID)

        '/* 初期設定 * /
        Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
        Dim dsXml As New DataSet
        dsXml.ReadXml(xmlPath & "/lang/base/lang.xml")

        Dim rXml() As DataRow = dsXml.Tables("TITLE").Select("LANGUAGE_KBN='" & lang & "'")

        Dim PAGE_TITLE As String = ""

        If rXml.Length > 0 Then
            PAGE_TITLE = rXml(0)("cart003")
            PAGE_TITLE = PAGE_TITLE.Replace("@@SITE_TITLE@@", dsRTUser.M137_RT_SITE_CD.Rows(0)("SITE_TITLE"))
            Me.Title = PAGE_TITLE
        End If
        '/* 初期設定 * /

        Dim dsWebScrnRes As TriphooCMSAPI.ScreenSettingDataSet = CommonUtil.WEB_SCREEN_SETTING(Me.RT_CD.Value, Me.S_CD.Value, "cart003", lang)

        If dsWebScrnRes.DETAIL_RES.Rows.Count = 0 Then
            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "ScreenSet()", 532, "終了", HttpContext.Current.Session.SessionID)

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

        Me.PageHeader.Text = PAGE_HEADER
        'Me.PageSide.Text = dsWebScrnRes.DETAIL_RES.Rows(0)("SIDE")
        Me.Pagefooter.Text = dsWebScrnRes.DETAIL_RES.Rows(0)("FOOTER")

        If Not dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE").Equals("") Then
            Me.Title = dsWebScrnRes.DETAIL_RES.Rows(0)("TITLE")
        End If

        Me.MetaDescription = dsWebScrnRes.DETAIL_RES.Rows(0)("DESCRIPTION")
        Me.MetaKeywords = dsWebScrnRes.DETAIL_RES.Rows(0)("KEYWORD")
        Me.SCRIPTLiteral.Text = CSS & SCRIPT

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "ScreenSet()", 564, "終了", HttpContext.Current.Session.SessionID)
    End Sub
#End Region

#Region "ページ設定"

#Region "iniPage"
    Private Sub iniPage(dsItinerary As TriphooRR097DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "iniPage()", 574, "開始", HttpContext.Current.Session.SessionID)
        Dim RES_OPTION_SUP_CD As String = ""

        Dim RES_METHOD_KBN As String = dsItinerary.PAGE_03.Rows(0)("RES_METHOD_KBN")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")
        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
        Dim RES_STS As String = dsItinerary.PAGE_03.Rows(0)("RES_STATUS_KBN")
        Dim OVERSEAS_DOMESTIC_KBN As String = dsItinerary.PAGE_03.Rows(0)("OVERSEAS_DOMESTIC_KBN")
        Dim KARTE_KBN As String = dsItinerary.PAGE_03.Rows(0)("KARTE_KBN")
        Dim OFFLINE_NOTICE_FLG As Boolean = dsRTUser.M137_RT_SITE_CD.Rows(0)("OFFLINE_NOTICE_FLG")
        Dim MANAGE_CD As String = SetRRValue.setNothingValue(dsItinerary.PAGE_03.Rows(0)("MANAGE_CD"))

        Dim NUM_ADULT As Integer = dsItinerary.PAGE_03.Rows(0)("NUM_ADULT")
        Dim NUM_CHILD As Integer = dsItinerary.PAGE_03.Rows(0)("NUM_CHILD")
        Dim NUM_INFANT As Integer = dsItinerary.PAGE_03.Rows(0)("NUM_INFANT")

        Dim MEMBER_ADD_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("MEMBER_ADD_KBN")
        Dim PAYMENT_PAGE_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("PAYMENT_PAGE_KBN")

        If dsUser Is Nothing Then
            Select Case MEMBER_ADD_KBN
                Case "2"
                    ' ログ生成 NSSOL負荷性能検証 2023/02/09
                    'logger.CreateLog("page_cart_cart003", "iniPage()", 598, "終了", HttpContext.Current.Session.SessionID)

                    Response.Redirect(PANKUZU001URL, True)
            End Select
        End If

        If MEMBER_ADD_KBN.Equals("4") Then
            Me.TOUR_REGULATIONPanel.Visible = False
            Me.TICKET_REGULATIONPanel.Visible = False
            Me.HOTEL_REGULATIONPanel.Visible = False
            Me.OPTION_REGULATIONPanel.Visible = False
            Me.DP_REGULATIONPanel.Visible = False
            Me.AgreeCheckPanel.Attributes.Add("style", "display:none;")
            Me.AgreeCheckBox.Checked = True
        End If

        '予約商品によって表示する条件書を変更する
        Dim dsRegulation As New DataSet

        Dim REGULATION_KBN As String = ""

        Dim isPackage As Boolean = False

        If PACKAGE_FLG Then
            isPackage = True
        End If

        If TICKET_FLG And HOTEL_FLG Then

            Select Case KARTE_KBN
                Case "06" : isPackage = True
            End Select

        End If

        'ツアー
        If isPackage Then
            ' 01:オンライン
            ' 02:募集型
            ' 03:モデルプラン
            ' 04:受注型
            ' 05:オンライン (PEX)
            ' 06:募集型 (PEX)
            ' 07:ユニット
            ' 08:電車
            ' 09:飛行機
            ' 10:バス
            ' 11:ユニット (オンライン) 
            ' 12:募集型グループ
            ' 13:募集型 (在庫)
            ' 14:オンライン (チャーター)
            ' 15:募集型 (チャーター)

            Dim SCREEN_KBN As String = "agreement001"
            Dim SUP_CD As String = ""
            If 0 < dsItinerary.PAGE_20.Rows.Count Then
                Dim GOODS_CLASS As String = dsItinerary.PAGE_20.Rows(0)("GOODS_CLASS")
                ' ORG:自社造成 / UNIT:ユニット / OTPKG:他社PKG / BASE:BASE / OTHER:その他
                Dim PKG_TYPE As String = dsItinerary.PAGE_20.Rows(0)("IMAGE_04")

                SUP_CD = dsItinerary.PAGE_20.Rows(0)("SUP_CD")

                Select Case GOODS_CLASS
                    Case "04"
                        ' 04:受注型
                        Me.LABEL_0022.Text = LABEL_0062
                        SCREEN_KBN = "agreement023"
                    Case "05", "06"
                        ' 06:募集型 (PEX)
                        Me.LABEL_0022.Text = LABEL_0063
                        If OVERSEAS_DOMESTIC_KBN.Equals("02") Then
                            SCREEN_KBN = "agreement027"

                            If PKG_TYPE.Equals("OTPKG") Then
                                '他社PKG
                                SCREEN_KBN = "agreement021"
                            End If

                        Else
                            SCREEN_KBN = "agreement022"

                            If PKG_TYPE.Equals("OTPKG") Then
                                '他社PKG
                                SCREEN_KBN = "agreement020"
                            End If

                        End If
                    Case "14", "15"
                        ' 14:オンライン (チャーター),15:募集型 (チャーター)
                        Me.LABEL_0022.Text = LABEL_0063
                        If OVERSEAS_DOMESTIC_KBN.Equals("02") Then
                            '国内
                            SCREEN_KBN = "agreement009"
                        Else
                            '海外
                            SCREEN_KBN = "agreement024"
                        End If
                    Case "16"
                        If OVERSEAS_DOMESTIC_KBN.Equals("02") Then
                            '国内
                            SCREEN_KBN = "agreement029"
                        Else
                            '海外
                            SCREEN_KBN = "agreement028"
                        End If
                    Case "28", "29"
                        SCREEN_KBN = "agreement040"
                    Case Else
                        Me.LABEL_0022.Text = LABEL_0063

                        ' その他
                        If OVERSEAS_DOMESTIC_KBN.Equals("02") Then
                            '国内
                            SCREEN_KBN = "agreement009"

                            If PKG_TYPE.Equals("OTPKG") Then
                                '他社PKG
                                SCREEN_KBN = "agreement021"
                            End If
                        Else
                            '海外
                            ' agreement001

                            If PKG_TYPE.Equals("OTPKG") Then
                                '他社PKG
                                SCREEN_KBN = "agreement020"
                            End If
                        End If
                End Select
            Else
                SCREEN_KBN = "agreement027"
            End If

            ' スクリーン区分＋仕入先で取得
            Dim _REGULATION As String = ""
            Dim _URL As String = ""

            If Not SUP_CD.Equals("") Then
                dsRegulation = CommonUtil.REGULATION_SUP(Me.RT_CD.Value, Me.S_CD.Value, SCREEN_KBN, SUP_CD)
                If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
                    _REGULATION = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
                    _URL = SetRRValue.setDBNullValue(dsRegulation.Tables("DETAIL_RES").Rows(0)("URL"))
                End If
            End If

            If _REGULATION.Equals("") And _URL.Equals("") Then
                ' なかったらスクリーン区分で取得
                dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, SCREEN_KBN, lang)
                If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
                    _REGULATION = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
                    _URL = SetRRValue.setDBNullValue(dsRegulation.Tables("DETAIL_RES").Rows(0)("URL"))
                End If
            End If

            If Not _URL.Equals("") And Not _REGULATION.Equals("") Then
                'URLと内容が登録されている場合
                Me.TOUR_REGULATIONPanel.Visible = True
                Me.TOUR_REGULATION.InnerHtml = _REGULATION
                Me.TOUR_REGULATION_DOWNLOADPanel.Visible = True
                Me.TOUR_REGULATION_DOWNLOADLinkButton.HRef = _URL
            ElseIf Not _URL.Equals("") Then
                'URLが登録されている場合
                Me.TOUR_REGULATIONPanel.Visible = False
                Me.TOUR_REGULATION_DOWNLOADPanel.Visible = True
                Me.TOUR_REGULATION_DOWNLOADLinkButton.HRef = _URL
            Else
                '内容が登録されている場合
                Me.TOUR_REGULATION_DOWNLOADLinkButton.Visible = False
                Me.TOUR_REGULATION_DOWNLOADPanel.Visible = False
                Me.TOUR_REGULATIONPanel.Visible = True
                Me.TOUR_REGULATION.InnerHtml = _REGULATION
            End If
        End If

        '航空券
        If Not isPackage And TICKET_FLG Then
            dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "agreement002", lang)
            If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
                Dim _REGULATION As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
                Dim _URL As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("URL")

                If Not _URL.Equals("") And Not _REGULATION.Equals("") Then
                    'URLと内容が登録されている場合
                    Me.TICKET_REGULATION.InnerHtml = _REGULATION
                    Me.TICKET_REGULATION_DOWNLOADLinkButton.HRef = _URL
                    Me.TICKET_REGULATION_DOWNLOADPanel.Visible = True
                ElseIf Not _URL.Equals("") Then
                    'URLが登録されている場合
                    Me.TICKET_REGULATION_DOWNLOADPanel.Visible = True
                    Me.TICKET_REGULATION_TEXTPanel.Visible = False
                    Me.TICKET_REGULATION_DOWNLOADLinkButton.HRef = _URL
                Else
                    '内容が登録されている場合
                    Me.TICKET_REGULATION_DOWNLOADPanel.Visible = False
                    Me.TICKET_REGULATION.InnerHtml = _REGULATION
                End If
            End If
        End If


        'ホテル
        If Not isPackage And HOTEL_FLG Then
            dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "agreement003", lang)
            If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
                Dim _REGULATION As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
                Dim _URL As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("URL")

                If Not _URL.Equals("") And Not _REGULATION.Equals("") Then
                    'URLと内容が登録されている場合
                    Me.HOTEL_REGULATION.InnerHtml = _REGULATION
                    Me.HOTEL_REGULATION_DOWNLOADLinkButton.HRef = _URL
                    Me.HOTEL_REGULATION_DOWNLOADPanel.Visible = True
                ElseIf Not _URL.Equals("") Then
                    'URLが登録されている場合
                    Me.HOTEL_REGULATION_DOWNLOADPanel.Visible = True
                    Me.HOTEL_REGULATION_TEXTPanel.Visible = False
                    Me.HOTEL_REGULATION_DOWNLOADLinkButton.HRef = _URL
                Else
                    '内容が登録されている場合
                    Me.HOTEL_REGULATION_DOWNLOADPanel.Visible = False
                    Me.HOTEL_REGULATION.InnerHtml = _REGULATION
                End If
            End If
        End If


        'オプション
        If Not isPackage And OPTION_FLG Then

            Dim NO_SUP_CD_FLG As Boolean = True

            If dsItinerary.RES_OPTION.Rows.Count > 0 Then
                RES_OPTION_SUP_CD = dsItinerary.RES_OPTION.Rows(0)("SUP_CD")

                If Not RES_OPTION_SUP_CD.Equals("") Then
                    dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "agreement048", lang, RES_OPTION_SUP_CD)

                    If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
                        NO_SUP_CD_FLG = False
                    End If
                End If
            End If

            If NO_SUP_CD_FLG Then
                dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "agreement004", lang)
            End If

            If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
                Dim _REGULATION As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
                Dim _URL As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("URL")

                If Not _URL.Equals("") And Not _REGULATION.Equals("") Then
                    'URLと内容が登録されている場合
                    Me.OPTION_REGULATION.InnerHtml = _REGULATION
                    Me.OPTION_REGULATION_DOWNLOADLinkButton.HRef = _URL
                    Me.OPTION_REGULATION_DOWNLOADPanel.Visible = True
                ElseIf Not _URL.Equals("") Then
                    'URLが登録されている場合
                    Me.OPTION_REGULATION_DOWNLOADPanel.Visible = True
                    Me.OPTION_REGULATION_TEXTPanel.Visible = False
                    Me.OPTION_REGULATION_DOWNLOADLinkButton.HRef = _URL
                Else
                    '内容が登録されている場合
                    Me.OPTION_REGULATION_DOWNLOADPanel.Visible = False
                    Me.OPTION_REGULATION.InnerHtml = _REGULATION
                End If
            End If
        End If

        'DP
        If Not isPackage And TICKET_FLG And HOTEL_FLG Then

            REGULATION_KBN = "agreement005"

            dsRegulation = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, REGULATION_KBN, lang)
            If dsRegulation.Tables("DETAIL_RES").Rows.Count > 0 Then
                Dim _REGULATION As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("CONTENTS")
                Dim _URL As String = dsRegulation.Tables("DETAIL_RES").Rows(0)("URL")

                If Not _URL.Equals("") And Not _REGULATION.Equals("") Then
                    'URLと内容が登録されている場合
                    Me.DP_REGULATION.InnerHtml = _REGULATION
                    Me.DP_REGULATION_DOWNLOADLinkButton.HRef = _URL
                    Me.DP_REGULATION_DOWNLOADPanel.Visible = True
                ElseIf Not _URL.Equals("") Then
                    'URLが登録されている場合
                    Me.DP_REGULATION_DOWNLOADPanel.Visible = True
                    Me.DP_REGULATION_TEXTPanel.Visible = False
                    Me.DP_REGULATION_DOWNLOADLinkButton.HRef = _URL
                Else
                    '内容が登録されている場合
                    Me.DP_REGULATION_DOWNLOADPanel.Visible = False
                    Me.DP_REGULATION.InnerHtml = _REGULATION
                End If
            End If
        End If

        If isPackage Then
            'Me.TOUR_REGULATIONPanel.Visible = False
            'Me.TOUR_REGULATION_DOWNLOADPanel.Visible = False
            Me.TICKET_REGULATIONPanel.Visible = False
            Me.HOTEL_REGULATIONPanel.Visible = False
            Me.OPTION_REGULATIONPanel.Visible = False
            Me.DP_REGULATIONPanel.Visible = False
        Else

            Me.TOUR_REGULATIONPanel.Visible = False
            Me.TOUR_REGULATION_DOWNLOADPanel.Visible = False

            If Not TICKET_FLG Then
                Me.TICKET_REGULATIONPanel.Visible = False
            End If

            If Not HOTEL_FLG Then
                Me.HOTEL_REGULATIONPanel.Visible = False
            End If

            If Not OPTION_FLG Then
                Me.OPTION_REGULATIONPanel.Visible = False
            End If

            'DP
            If TICKET_FLG And HOTEL_FLG Then
                If Me.DP_REGULATION.InnerHtml.Equals("") Then
                    Me.DP_REGULATIONPanel.Visible = False
                Else
                    Me.TICKET_REGULATIONPanel.Visible = False
                    Me.HOTEL_REGULATIONPanel.Visible = False
                End If
            Else
                Me.DP_REGULATIONPanel.Visible = False
            End If

        End If


        'キャンセルポリシー
        ' agreement010 : キャンセルポリシー(最終確認画面)

        Dim SCRN_KBN As String = "agreement010"
        RES_OPTION_SUP_CD = ""

        If TICKET_FLG Then
            SCRN_KBN = "agreement016"
        End If

        If HOTEL_FLG Then
            SCRN_KBN = "agreement017"
        End If

        If OPTION_FLG Then
            SCRN_KBN = "agreement019"
        End If

        If isPackage Then

            If 0 < dsItinerary.PAGE_20.Rows.Count Then

                Dim GOODS_CLASS As String = dsItinerary.PAGE_20.Rows(0)("GOODS_CLASS")
                Dim DAYS As Integer = dsItinerary.PAGE_20.Rows(0)("DAYS")

                Select Case OVERSEAS_DOMESTIC_KBN
                    Case "01"
                        If DAYS = 1 Then
                            '日帰り
                            Select Case GOODS_CLASS
                                Case "05", "06" ' PEX
                                    SCRN_KBN = "agreement035"
                                Case "16" ' イベント
                                    SCRN_KBN = "agreement036"
                                Case Else
                                    SCRN_KBN = "agreement034"
                            End Select
                        Else
                            Select Case GOODS_CLASS
                                Case "05", "06" ' PEX
                                    SCRN_KBN = "agreement026"
                                Case "16" ' イベント
                                    SCRN_KBN = "agreement030"
                                Case Else
                                    SCRN_KBN = "agreement018"
                            End Select
                        End If
                    Case "02"
                        If DAYS = 1 Then
                            '日帰り
                            Select Case GOODS_CLASS
                                Case "05", "06" ' PEX
                                    SCRN_KBN = "agreement038"
                                Case "16" ' イベント
                                    SCRN_KBN = "agreement039"
                                Case Else
                                    SCRN_KBN = "agreement037"
                            End Select
                        Else
                            Select Case GOODS_CLASS
                                Case "05", "06" ' PEX
                                    SCRN_KBN = "agreement032"
                                Case "16" ' イベント
                                    SCRN_KBN = "agreement033"
                                Case "28", "29"
                                    SCRN_KBN = "agreement042"
                                Case Else
                                    SCRN_KBN = "agreement031"
                            End Select
                        End If
                End Select
            Else
                SCRN_KBN = "agreement032"
            End If

        End If

        'greement051 : キャンセルポリシー 最終確認画面 現地ツアー (他社PKG)
        Select Case Me.RT_CD.Value
            Case "URUTO"
                If dsItinerary.RES_OPTION.Rows.Count > 0 Then
                    RES_OPTION_SUP_CD = dsItinerary.RES_OPTION.Rows(0)("SUP_CD")

                    If Not RES_OPTION_SUP_CD.Equals("") Then
                        SCRN_KBN = "agreement051"
                    End If
                End If
        End Select

        Dim RT_CANCEL_POLICY_URL As String = ""
        Dim RT_CANCEL_POLICY_CONTENTS As String = ""

        Dim dsCancelPolicy As DataSet = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, SCRN_KBN, lang, RES_OPTION_SUP_CD)

        If dsCancelPolicy.Tables("DETAIL_RES").Rows.Count > 0 Then
            RT_CANCEL_POLICY_URL = dsCancelPolicy.Tables("DETAIL_RES").Rows(0)("URL")
            RT_CANCEL_POLICY_CONTENTS += CStr(dsCancelPolicy.Tables("DETAIL_RES").Rows(0)("CONTENTS")).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
        Else
            dsCancelPolicy = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "agreement010", lang)

            If dsCancelPolicy.Tables("DETAIL_RES").Rows.Count > 0 Then
                RT_CANCEL_POLICY_URL = dsCancelPolicy.Tables("DETAIL_RES").Rows(0)("URL")
                RT_CANCEL_POLICY_CONTENTS += CStr(dsCancelPolicy.Tables("DETAIL_RES").Rows(0)("CONTENTS")).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
            End If
        End If

        '利用規約の取得 2022/10/28
        Dim RT_TERMS_USE_URL As String = ""
        Dim RT_TERMS_USE_CONTENTS As String = ""

        Dim TERMS_USE_SCRN_KBN As String = "agreement043"
        RES_OPTION_SUP_CD = ""

        'agreement052 : 利用規約 (他社PKG)
        Select Case Me.RT_CD.Value
            Case "URUTO"
                If dsItinerary.RES_OPTION.Rows.Count > 0 Then
                    RES_OPTION_SUP_CD = dsItinerary.RES_OPTION.Rows(0)("SUP_CD")

                    If Not RES_OPTION_SUP_CD.Equals("") Then
                        TERMS_USE_SCRN_KBN = "agreement052"
                    End If
                End If
        End Select

        Dim dsTermsUse As DataSet = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, TERMS_USE_SCRN_KBN, lang, RES_OPTION_SUP_CD)

        If dsTermsUse.Tables("DETAIL_RES").Rows.Count > 0 Then
            RT_TERMS_USE_URL = dsTermsUse.Tables("DETAIL_RES").Rows(0)("URL")
            RT_TERMS_USE_CONTENTS += CStr(dsTermsUse.Tables("DETAIL_RES").Rows(0)("CONTENTS")).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
        Else
            dsCancelPolicy = CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "agreement043", lang)

            If dsCancelPolicy.Tables("DETAIL_RES").Rows.Count > 0 Then
                RT_CANCEL_POLICY_URL = dsCancelPolicy.Tables("DETAIL_RES").Rows(0)("URL")
                RT_CANCEL_POLICY_CONTENTS += CStr(dsCancelPolicy.Tables("DETAIL_RES").Rows(0)("CONTENTS")).Replace(vbCrLf, "<br/>").Replace(vbLf, "<br/>")
            End If
        End If

        Select Case MEMBER_ADD_KBN
            Case "3", "4"
                Me.TOUR_REGULATIONPanel.Visible = False
                Me.TOUR_REGULATION_DOWNLOADPanel.Visible = False
                Me.TICKET_REGULATIONPanel.Visible = False
                Me.HOTEL_REGULATIONPanel.Visible = False
                Me.OPTION_REGULATIONPanel.Visible = False
                Me.DP_REGULATIONPanel.Visible = False
                Me.AgreeCheckPanel.Visible = False
                Me.Agree1Panel.Visible = False
                Me.Agree2Panel.Visible = False
                Me.AgreePanel.Visible = False
                If Not RT_CANCEL_POLICY_CONTENTS.Equals("") Then
                    Me.TOUR_REGULATION_B2B.Text = RT_CANCEL_POLICY_CONTENTS
                    Me.TOUR_REGULATION_B2BPanel.Visible = True
                Else
                    Me.TOUR_REGULATION_B2BPanel.Visible = False
                End If

            Case Else
                Me.TOUR_REGULATION_B2BPanel.Visible = False
                If Not RT_CANCEL_POLICY_URL.Equals("") Or Not RT_CANCEL_POLICY_CONTENTS.Equals("") Then
                    If Not RT_CANCEL_POLICY_URL.Equals("") Then
                        Me.TOUR_CANCELPOLICY_LinkButton.HRef = RT_CANCEL_POLICY_URL
                        Me.TOUR_CANCELPOLICY_ModalButton.Visible = False
                    Else
                        Me.TOUR_CANCELPOLICY_LinkButton.Visible = False

                        If Not RT_CANCEL_POLICY_CONTENTS.Equals("") Then
                            Me.CancelPolicyLabel.Text = RT_CANCEL_POLICY_CONTENTS
                        Else
                            Me.TOUR_CANCELPOLICY_ModalButton.Visible = False
                        End If
                    End If

                    Me.CANCELPOLICYPanel.Visible = True
                Else
                    Me.TOUR_CANCELPOLICY_LinkButton.Visible = False
                    Me.TOUR_CANCELPOLICY_ModalButton.Visible = False
                End If

                '利用規約 2022/10/28
                If Not RT_TERMS_USE_URL.Equals("") Or Not RT_TERMS_USE_CONTENTS.Equals("") Then
                    If Not RT_TERMS_USE_URL.Equals("") Then
                        Me.TERMS_USE_LinkButton.HRef = RT_TERMS_USE_URL
                        Me.TERMS_USE_ModalButton.Visible = False
                    Else
                        Me.TERMS_USE_LinkButton.Visible = False

                        If Not RT_TERMS_USE_CONTENTS.Equals("") Then
                            Me.TermsUseLabel.Text = RT_TERMS_USE_CONTENTS
                        Else
                            Me.TERMS_USE_ModalButton.Visible = False
                        End If
                    End If

                    Me.TERMS_USEPanel.Visible = True
                Else
                    Me.TERMS_USE_LinkButton.Visible = False
                    Me.TERMS_USE_ModalButton.Visible = False
                End If
        End Select

        Me.CARD_EXP_YEAR.DataSource = SetValue.setCARD_EXP_YEAR(True)
        Me.CARD_EXP_YEAR.DataTextField = "TEXT"
        Me.CARD_EXP_YEAR.DataValueField = "VALUE"
        Me.CARD_EXP_YEAR.DataBind()

        Me.CARD_EXP_MONTH.DataSource = SetValue.setCARD_EXP_MONTH(True)
        Me.CARD_EXP_MONTH.DataTextField = "TEXT"
        Me.CARD_EXP_MONTH.DataValueField = "VALUE"
        Me.CARD_EXP_MONTH.DataBind()

        Dim isPay As Boolean = False

        If RES_STS.Equals("OK") Then
            isPay = True
        Else
            If 0 < dsItinerary.WEB_TRANSACTION.Rows.Count Then
                Dim GOODS_SETTLE_KBN As String = dsItinerary.WEB_TRANSACTION.Rows(0)("GOODS_SETTLE_KBN") ' :指定無し 01:振込限定 02:カード限定

                Select Case GOODS_SETTLE_KBN
                    Case "04", "05"
                        '04:カード(仮売上)　商品設定優先
                        '05:振込＆カード(仮売上)　商品設定優先
                        isPay = True
                End Select

            End If
        End If

        '●支払方法 (ステータスがリクエストの場合非表示)
        If isPay Then

            ' 初期化
            Me.PayPanel.Visible = True
            Me.RES_CREDITRadioButton.Checked = False
            Me.RES_CREDIT_REQUESTRadioButton.Checked = False
            Me.RES_BANKRadioButton.Checked = False

            'リテーラー支払表示 設定
            Dim CARD_FLG As Boolean = False
            Dim CARD_REQUEST_FLG As Boolean = False
            Dim BANK_FLG As Boolean = False

            E_Connect.SetPay(Me.RT_CD.Value, Me.S_CD.Value, BANK_FLG, CARD_FLG, False, CARD_REQUEST_FLG, False, False, False, False, dsItinerary)

            If Me.RT_CD.Value.Equals("RT33") And Me.S_CD.Value.Equals("02") Then
                BANK_FLG = True
            End If

            'If CARD_FLG And CARD_REQUEST_FLG Then
            '    CARD_FLG = False
            'End If

            'リテーラーカード種別表示 設定
            If CARD_FLG Then

                '01 MASTER
                '02 VISA
                '03 JCB
                '04 AMERICAN EXPRESS
                '05 DINERS CLUB

                Dim dtCARD_KIND As KbnDataSet.KBNDataTable = E_Connect.setCARD_KIND(Me.RT_CD.Value, Me.S_CD.Value)
                Me.CARD_KIND.DataSource = dtCARD_KIND
                Me.CARD_KIND.DataTextField = "TEXT"
                Me.CARD_KIND.DataValueField = "VALUE"
                Me.CARD_KIND.DataBind()

                'カード種別　初期化
                Me.VISA.Visible = False
                Me.AMEX.Visible = False
                Me.DINERS.Visible = False
                Me.JCB.Visible = False
                Me.MASTER.Visible = False

                If dtCARD_KIND.Rows.Count > 0 Then
                    For Each row As DataRow In dtCARD_KIND.Rows
                        Select Case row("VALUE")
                            Case "01" : Me.MASTER.Visible = True
                            Case "02" : Me.VISA.Visible = True
                            Case "03" : Me.JCB.Visible = True
                            Case "04" : Me.AMEX.Visible = True
                            Case "05" : Me.DINERS.Visible = True
                        End Select
                    Next
                Else
                    CARD_FLG = False
                End If

            End If

            '支払方法 初期チェック 設定
            Dim SETTLE_KBN As String = SetRRValue.setNothingValue(dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN"))

            Select Case SETTLE_KBN
                Case "01" 'カード
                    If CARD_FLG Then
                        Me.RES_CREDITRadioButton.Checked = True
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                        Me.RES_BANKRadioButton.Checked = False
                    End If
                Case "02" '銀行
                    If BANK_FLG Then
                        Me.RES_CREDITRadioButton.Checked = False
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                        Me.RES_BANKRadioButton.Checked = True
                    End If
                Case "03" '03:カード(リクエスト)　商品設定優先
                    Me.RES_CREDITRadioButton.Checked = False
                    Me.RES_CREDIT_REQUESTRadioButton.Checked = True
                    Me.RES_BANKRadioButton.Checked = False
            End Select

            ' 商品の支払い方法指定
            If dsItinerary.WEB_TRANSACTION.Rows.Count <> 0 Then
                Dim GOODS_SETTLE_KBN As String = dsItinerary.WEB_TRANSACTION.Rows(0)("GOODS_SETTLE_KBN") ' :指定無し 01:振込限定 02:カード限定
                Select Case GOODS_SETTLE_KBN
                    Case "01" '01:振込限定
                        If BANK_FLG Then
                            Me.RES_CREDITRadioButton.Checked = False
                            Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                            Me.RES_BANKRadioButton.Checked = True
                        End If
                        CARD_FLG = False
                        CARD_REQUEST_FLG = False
                    Case "02" '02:カード限定
                        If CARD_FLG Then
                            Me.RES_BANKRadioButton.Checked = False
                            Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                            Me.RES_CREDITRadioButton.Checked = True
                        End If
                        BANK_FLG = False
                        CARD_REQUEST_FLG = False
                    Case "03" '03:カード(リクエスト)　商品設定優先
                        Me.RES_CREDITRadioButton.Checked = False
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = True
                        Me.RES_BANKRadioButton.Checked = False
                        BANK_FLG = False
                    Case "04" '04:カード(仮売上)　商品設定優先
                        Me.RES_CREDITRadioButton.Checked = True
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                        Me.RES_BANKRadioButton.Checked = False
                        CARD_FLG = True
                        BANK_FLG = False
                        CARD_REQUEST_FLG = False
                    Case "05" '05:振込＆カード(仮売上)　商品設定優先
                        Me.RES_CREDITRadioButton.Checked = False
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                        Me.RES_BANKRadioButton.Checked = True
                    Case "06" '06:現地払い
                        CARD_FLG = False
                        BANK_FLG = False
                        CARD_REQUEST_FLG = False
                    Case "09" '09:支払方法なし　商品設定優先
                        Me.RES_CREDITRadioButton.Checked = False
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = True
                        Me.RES_BANKRadioButton.Checked = False
                        CARD_FLG = False
                        BANK_FLG = False
                        CARD_REQUEST_FLG = False
                End Select
            Else
                BANK_FLG = False
            End If

            If CARD_FLG Or BANK_FLG Or CARD_REQUEST_FLG Then
                Me.RES_BANKPanel.Visible = BANK_FLG
                Me.RES_CREDITPanel.Visible = CARD_FLG
                Me.RES_CREDIT_REQUESTPanel.Visible = CARD_REQUEST_FLG

                Dim iCount As Integer = 0
                If BANK_FLG Then
                    iCount += 1
                End If
                If CARD_FLG Then
                    iCount += 1
                End If
                If CARD_REQUEST_FLG Then
                    iCount += 1
                End If

                If iCount = 1 Then
                    If BANK_FLG Then
                        Me.RES_BANKRadioButton.Checked = True
                        Me.RES_CREDITRadioButton.Checked = False
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                    End If
                    If CARD_FLG Then
                        Me.RES_BANKRadioButton.Checked = False
                        Me.RES_CREDITRadioButton.Checked = True
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = False
                    End If
                    If CARD_REQUEST_FLG Then
                        Me.RES_BANKRadioButton.Checked = False
                        Me.RES_CREDITRadioButton.Checked = False
                        Me.RES_CREDIT_REQUESTRadioButton.Checked = True
                    End If
                End If

                'M303にコメントが登録されていればLABELを上書き 22.4.28 terui
                Dim GOODS_KBN As String = ""
                If dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG") Then
                    GOODS_KBN = "03"
                ElseIf dsItinerary.PAGE_03.Rows(0)("TICKET_FLG") And dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG") Then
                    GOODS_KBN = "99" ' DP
                ElseIf dsItinerary.PAGE_03.Rows(0)("TICKET_FLG") Then
                    GOODS_KBN = "01"
                ElseIf dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG") Then
                    GOODS_KBN = "02"
                ElseIf dsItinerary.PAGE_03.Rows(0)("OPTION_FLG") Then
                    GOODS_KBN = "05"
                End If

                Dim dsM303_RT_SITE_CD_CTRL_FLG_LANG As DataSet =
                    B2CAPIClient.SelectM303_RT_SITE_CD_CTRL_FLG_LANGGateway(Me.RT_CD.Value, Me.S_CD.Value, "", GOODS_KBN, lang, dsB2CUser)

                For Each row In dsM303_RT_SITE_CD_CTRL_FLG_LANG.Tables(0).Rows

                    Dim SYS_CTRL_KBN As String = row("SYS_CTRL_KBN") ' 01:振込 02:カード 03:コンビニ 04:カード (メール)
                    Dim TITLE As String = row("TITLE")
                    Dim REMARKS As String = row("REMARKS")

                    Select Case SYS_CTRL_KBN
                        Case "01"
                            If Me.RES_BANKPanel.Visible Then
                                If Not TITLE.Equals("") Then
                                    Me.RES_BANKRadioButton.Text = TITLE
                                End If
                                If Not REMARKS.Equals("") Then
                                    Me.LABEL_0092.Text = REMARKS
                                End If
                            End If
                        Case "02"
                            If Me.RES_CREDITPanel.Visible Then
                                If Not TITLE.Equals("") Then
                                    Me.RES_CREDITRadioButton.Text = TITLE
                                End If
                                If Not REMARKS.Equals("") Then
                                    Me.LABEL_0094.Text = REMARKS
                                End If
                            End If
                        Case "04"
                            If Me.RES_CREDIT_REQUESTPanel.Visible Then
                                If Not TITLE.Equals("") Then
                                    Me.RES_CREDIT_REQUESTRadioButton.Text = TITLE
                                End If
                                If Not REMARKS.Equals("") Then
                                    Me.LABEL_0093.Text = REMARKS
                                End If
                            End If
                    End Select
                Next


            Else
                Me.PayPanel.Visible = False
            End If
        Else
            Me.PayPanel.Visible = False
        End If

        If dsItinerary.M019_CLIENT.Rows.Count = 0 Then
            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "iniPage()", 1343, "終了", HttpContext.Current.Session.SessionID)

            Throw New Exception("顧客情報0件エラー")
        End If

        Me.EDIT_TIME.Value = dsItinerary.PAGE_09.Rows(0)("EDIT_TIME")

        'token考慮
        Me.RES_COMMIT_PANEL.Visible = True
        Me.RES_COMMIT_TOKEN_PANEL.Visible = False

        If Me.RES_CREDITPanel.Visible Then
            Dim dsM031_RT_APP_KBN As DataSet = TriphooRMClient.SelectM031_RT_APP_KBNGateway(Me.RT_CD.Value, "06", dsUser)
            Dim USER_ID As String = ""
            Dim PASSWORD As String = ""
            Dim URL As String = ""
            Try
                USER_ID = SetRRValue.setDBNullValue(dsM031_RT_APP_KBN.Tables("M031_RT_APP_KBN").Rows(0)("USER_ID"))
                PASSWORD = SetRRValue.setDBNullValue(dsM031_RT_APP_KBN.Tables("M031_RT_APP_KBN").Rows(0)("PASSWORD"))
                URL = SetRRValue.setDBNullValue(dsM031_RT_APP_KBN.Tables("M031_RT_APP_KBN").Rows(0)("URL"))
            Catch ex As Exception
            End Try

            'M204対応　2022/08/26 照井
            Dim dsM204_RT_SITE_CD_CARD_KBN As DataSet = B2CAPIClient.SelectM204_RT_SITE_CD_CARD_KBNGateway(Me.RT_CD.Value, Me.S_CD.Value, "", "", dsB2CUser)
            Dim M204_FLG As Boolean = False

            If Not dsM204_RT_SITE_CD_CARD_KBN Is Nothing AndAlso dsM204_RT_SITE_CD_CARD_KBN.Tables(0).Rows.Count > 0 Then

                Dim SETTLE_PROXY_COMPANY_KBN As String = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("SETTLE_PROXY_COMPANY_KBN"))
                Dim M204_USER_ID As String = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("USER_ID"))
                URL = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("URL"))

                If Not M204_USER_ID.Equals("") AndAlso Not SETTLE_PROXY_COMPANY_KBN.Equals("") Then
                    USER_ID = E_Connect.GetUSER_ID_BY_SETTLE_PROXY_COMPANY_KBN(SETTLE_PROXY_COMPANY_KBN)
                    PASSWORD = "TOKEN"
                    M204_FLG = True
                End If
            End If

            If USER_ID.Equals("GMO") Then
                Me.GmoTokenPay.Visible = True
                Me.EconTokenPay.Visible = False
                Me.GmoTokenPaySmbc.Visible = False
                Me.GmoTokenPaySmbcStation.Visible = False
                Me.VeriTrans4GTokenPay.Visible = False

                Me.GmoTokenPay.ID = "GmoTokenPay"
                Me.GmoTokenPaySmbc.ID = "dummy1"
                Me.GmoTokenPaySmbcStation.ID = "dummy2"
                Me.EconTokenPay.ID = "dummy3"
                Me.VeriTrans4GTokenPay.ID = "dummy4"

            ElseIf USER_ID.Equals("SMBCGMO") Then
                Me.GmoTokenPay.Visible = False
                Me.EconTokenPay.Visible = False
                Me.GmoTokenPaySmbc.Visible = True
                Me.GmoTokenPaySmbcStation.Visible = False
                Me.VeriTrans4GTokenPay.Visible = False

                Me.GmoTokenPaySmbc.ID = "GmoTokenPaySmbc"
                Me.GmoTokenPay.ID = "dummy1"
                Me.GmoTokenPaySmbcStation.ID = "dummy2"
                Me.EconTokenPay.ID = "dummy3"
                Me.VeriTrans4GTokenPay.ID = "dummy4"

            ElseIf USER_ID.Equals("SMBCSTATION") Then
                Me.GmoTokenPay.Visible = False
                Me.EconTokenPay.Visible = False
                Me.GmoTokenPaySmbc.Visible = False
                Me.GmoTokenPaySmbcStation.Visible = True
                Me.VeriTrans4GTokenPay.Visible = False

                Me.GmoTokenPaySmbcStation.ID = "GmoTokenPay"
                Me.GmoTokenPay.ID = "dummy1"
                Me.GmoTokenPaySmbc.ID = "dummy2"
                Me.EconTokenPay.ID = "dummy3"
                Me.VeriTrans4GTokenPay.ID = "dummy4"

                Me.CARD_EXP_YEAR.DataSource = SetValue.setCARD_EXP_YEAR_SHORT(True)
                Me.CARD_EXP_YEAR.DataTextField = "TEXT"
                Me.CARD_EXP_YEAR.DataValueField = "VALUE"
                Me.CARD_EXP_YEAR.DataBind()

            ElseIf USER_ID.Equals("VERITRANS") Then
                Me.GmoTokenPay.Visible = False
                Me.EconTokenPay.Visible = False
                Me.GmoTokenPaySmbc.Visible = False
                Me.GmoTokenPaySmbcStation.Visible = False
                Me.VeriTrans4GTokenPay.Visible = True

                Me.VeriTrans4GTokenPay.ID = "GmoTokenPay"
                Me.GmoTokenPay.ID = "dummy1"
                Me.GmoTokenPaySmbcStation.ID = "dummy2"
                Me.EconTokenPay.ID = "dummy3"
                Me.GmoTokenPaySmbc.ID = "dummy4"

                Me.CARD_EXP_YEAR.DataSource = SetValue.setCARD_EXP_YEAR_SHORT(True)
                Me.CARD_EXP_YEAR.DataTextField = "TEXT"
                Me.CARD_EXP_YEAR.DataValueField = "VALUE"
                Me.CARD_EXP_YEAR.DataBind()

            Else
                Me.EconTokenPay.Visible = False
                Me.GmoTokenPay.Visible = False
                Me.GmoTokenPaySmbc.Visible = False
                Me.GmoTokenPaySmbcStation.Visible = False
                Me.VeriTrans4GTokenPay.Visible = False

                Me.GmoTokenPaySmbcStation.ID = "dummy3"
                Me.GmoTokenPay.ID = "dummy1"
                Me.GmoTokenPaySmbc.ID = "dummy2"
                Me.VeriTrans4GTokenPay.ID = "dummy4"
            End If

            If PASSWORD.Equals("TOKEN") Then
                Me.RES_COMMIT_PANEL.Visible = False
                Me.RES_COMMIT_TOKEN_PANEL.Visible = True


                If USER_ID.Equals("ECON") Then

                    Me.GmoTokenPay.Visible = False
                    Me.GmoTokenPaySmbc.Visible = False
                    Me.GmoTokenPaySmbcStation.Visible = False
                    Me.EconTokenPay.Visible = True
                    Me.VeriTrans4GTokenPay.Visible = False

                    'セッショントークン発行
                    Dim sessionToken As String = ""
                    Dim requestID As String = ""
                    Dim orderID As String = ""
                    Dim kanjiName1_1 As String = ""
                    Dim kanjiName1_2 As String = ""
                    Dim returnUrl As String = Request.Url.AbsoluteUri
                    If Not returnUrl.Contains("auth=secured") Then
                        returnUrl += "&auth=secured"
                    End If

                    If dsItinerary.M019_CLIENT.Rows.Count > 0 Then
                        kanjiName1_1 = dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI")
                        kanjiName1_2 = dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI")
                    End If

                    Dim ordAmount As Integer = 0
                    For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
                        ordAmount += row("SALES_SUB_TOTAL")
                    Next

                    E_Connect.createSession(Me.RT_CD.Value, Me.S_CD.Value, "", sessionToken, requestID, orderID, ordAmount, returnUrl, kanjiName1_1, kanjiName1_2)

                    Session.Remove("SSID1")
                    Session.Remove("SSID2")
                    Session.Remove("SSID3")

                    Session.Add("SSID1", sessionToken)
                    Session.Add("SSID2", requestID)
                    Session.Add("SSID3", orderID)

                    Me.token.Value = sessionToken
                    Me.requestID.Value = requestID
                    Me.orderID.Value = orderID

                End If

                If M204_FLG Then
                    Dim SHOP_ID As String = dsM204_RT_SITE_CD_CARD_KBN.Tables(0).Rows(0)("USER_ID")

                    'Select Case Me.RT_CD.Value & Me.S_CD.Value
                    '    Case "JGA03" 'MUN海外
                    '        SHOP_ID = "9200001914189"
                    '    Case "JGA04" 'MUN国外
                    '        SHOP_ID = "9200001914196"
                    'End Select

                    If Not SHOP_ID.Equals("") Then

                        Dim ss() As String = SHOP_ID.Split("|")

                        If 0 < ss.Length Then
                            Me.shopid.Value = ss(0)
                        End If

                        If 1 < ss.Length Then
                            Me.syuno_co_cd.Value = ss(1)
                        End If

                    End If
                Else
                    Dim dsM077_RT_CARD_KBN As Data.DataSet = TriphooRMClient.SelectM077_RT_CARD_KBNGateway(Me.RT_CD.Value, "", dsUser)

                    For i = 0 To dsM077_RT_CARD_KBN.Tables(0).Rows.Count - 1
                        Dim SHOP_ID As String = dsM077_RT_CARD_KBN.Tables(0).Rows(i)("USER_ID")

                        'Select Case Me.RT_CD.Value & Me.S_CD.Value
                        '    Case "JGA03" 'MUN海外
                        '        SHOP_ID = "9200001914189"
                        '    Case "JGA04" 'MUN国外
                        '        SHOP_ID = "9200001914196"
                        'End Select

                        If Not SHOP_ID.Equals("") Then

                            Dim ss() As String = SHOP_ID.Split("|")

                            If 0 < ss.Length Then
                                Me.shopid.Value = ss(0)
                            End If

                            If 1 < ss.Length Then
                                Me.syuno_co_cd.Value = ss(1)
                            End If

                            Exit For
                        End If
                    Next
                End If

                Me.api_key.Value = URL

            End If

        End If

        Select Case PAYMENT_PAGE_KBN
            Case "2"
                Me.RES_COMMIT_PANEL.Visible = True
                Me.RES_COMMIT_TOKEN_PANEL.Visible = False
                Me.GmoTokenPay.Visible = False
                Me.GmoTokenPaySmbc.Visible = False
                Me.GmoTokenPaySmbcStation.Visible = False
                Me.VeriTrans4GTokenPay.Visible = False
                Me.CreditDetailPanel.Visible = False
        End Select

        If Me.RES_BANKPanel.Visible Then
            Select Case Me.RT_CD.Value
                Case "A0027", "JTBM"
                Case Else
                    If PACKAGE_FLG AndAlso Not MANAGE_CD.Equals("") Then
                        Dim dsM074_RT_DEPARTMENT_ACCOUNT As DataSet = TriphooRMClient.SelectM074_RT_DEPARTMENT_ACCOUNTGateway(Me.RT_CD.Value, MANAGE_CD, "", dsUser)

                        Dim dvM074_RT_DEPARTMENT_ACCOUNT As DataView = dsM074_RT_DEPARTMENT_ACCOUNT.Tables(0).DefaultView
                        dvM074_RT_DEPARTMENT_ACCOUNT.RowFilter = String.Format("ACCOUNT_KBN IN ('01','03') AND ACCOUNT_NO <> '' AND APPLY_FROM_DATE <= #{0}# AND #{0}# <= APPLY_TO_DATE AND INVOICE_HIDDEN_FLG = 0", Today.ToString("yyyy/MM/dd"))

                        Dim dtM074_RT_DEPARTMENT_ACCOUNT As DataTable = dvM074_RT_DEPARTMENT_ACCOUNT.ToTable(True, "ACCOUNT_TYPE", "ACCOUNT_NO", "ACCOUNT_NAME", "BANK_NM", "BRANCH_NM")
                        dtM074_RT_DEPARTMENT_ACCOUNT.Columns.Add("ACCOUNT_TYPE_NM", GetType(String))

                        For Each row In dtM074_RT_DEPARTMENT_ACCOUNT.Rows
                            Dim ACCOUNT_TYPE As String = row("ACCOUNT_TYPE")
                            Dim ACCOUNT_TYPE_NM As String = ""
                            Select Case ACCOUNT_TYPE
                                Case "01" ' 01 口座・種別	普通
                                    ACCOUNT_TYPE_NM = "普通"
                                Case "02" ' 02 口座・種別	当座
                                    ACCOUNT_TYPE_NM = "当座"
                            End Select
                            row("ACCOUNT_TYPE_NM") = ACCOUNT_TYPE_NM
                        Next

                        Me.M074_RT_DEPARTMENT_ACCOUNTRepeater.DataSource = dtM074_RT_DEPARTMENT_ACCOUNT
                        Me.M074_RT_DEPARTMENT_ACCOUNTRepeater.DataBind()
                    End If
            End Select
        End If

        If Me.RES_CREDITPanel.Visible And Not Me.RES_BANKPanel.Visible Then
            Me.BankCautionPanel.Visible = True
        Else
            Me.BankCautionPanel.Visible = False
        End If

        '「※銀行振込をご希望のお客様はお手数ですが別途お電話にてご連絡ください」非表示 22.6.21 Mitsuta
        Select Case Me.RT_CD.Value
            Case "58"
                Me.BankCautionPanel.Visible = False
        End Select


        ' 申込金
        If ESTIMATE_NO.Equals("") AndAlso isPackage AndAlso RES_METHOD_KBN.Equals("01") Then

            Dim GOODS_CLASS As String = ""
            Dim REPORT_LIMIT_DATE As Date = "1900/01/01"
            Dim RECEIPT_LIMIT_DATE As Date = "1900/01/01"

            If 0 < dsItinerary.PAGE_20.Rows.Count Then
                GOODS_CLASS = dsItinerary.PAGE_20.Rows(0)("GOODS_CLASS")
            Else
                GOODS_CLASS = "05"
            End If

            '出発日ー今日
            Dim DEP_DATE As Date = CDate(dsItinerary.PAGE_03.Rows(0)("DEP_TIME")).ToString("yyyy/MM/dd")
            Dim FARE_TYPE As String = "" 'dsItinerary.PAGE_05.Rows(0)("FARE_TYPE")
            Dim AIR_COMPANY_CD As String = "" 'dsItinerary.PAGE_05.Rows(0)("AIR_COMPANY_CD")
            Dim TKT_SUP_CD As String = "" ' dsItinerary.PAGE_05.Rows(0)("XML_DATA")
            Dim TKT_GDS_KBN As String = ""

            If 0 < dsItinerary.PAGE_05.Rows.Count Then
                FARE_TYPE = dsItinerary.PAGE_05.Rows(0)("FARE_TYPE")
                AIR_COMPANY_CD = dsItinerary.PAGE_05.Rows(0)("AIR_COMPANY_CD")
                TKT_SUP_CD = dsItinerary.PAGE_05.Rows(0)("XML_DATA")
                TKT_GDS_KBN = dsItinerary.PAGE_05.Rows(0)("GDS_KBN")

            Else

                AIR_COMPANY_CD = ""
                Select Case GOODS_CLASS
                    Case "05", "06"
                        FARE_TYPE = "02"
                    Case Else
                        FARE_TYPE = "01"
                End Select
            End If
            Dim diffDays As Integer = DateDiff(DateInterval.Day, Today, DEP_DATE)

            '1名の金額で条件取得？要確認
            Dim TOTAL_PRICE As Integer = 0

            '各商材にてWEB参照不可フラグがFalseのもの料金を表示する
            Dim dtRES_ORDER_DATA As New TriphooB2CAPI.TriphooRR097DataSet.RES_ORDER_DATADataTable
            Dim dvRES_ORDER_DATA As DataView = dsItinerary.RES_ORDER_DATA.DefaultView
            dvRES_ORDER_DATA.RowFilter = "DISP_FLG=1"
            dvRES_ORDER_DATA.Sort = "GOODS_KBN ASC ,DISP_ORDER ASC"
            dtRES_ORDER_DATA.Merge(dvRES_ORDER_DATA.ToTable())

            Dim dtRES_ORDER_DATA_COST As New TriphooB2CAPI.TriphooRR097DataSet.RES_ORDER_DATADataTable
            Dim dvRES_ORDER_DATA_COST As DataView = dsItinerary.RES_ORDER_DATA.DefaultView
            dvRES_ORDER_DATA_COST.RowFilter = "DISP_FLG=0"
            dvRES_ORDER_DATA_COST.Sort = "GOODS_KBN ASC ,DISP_ORDER ASC"
            dtRES_ORDER_DATA_COST.Merge(dvRES_ORDER_DATA_COST.ToTable())

            Dim isPex As Boolean = False

            Select Case FARE_TYPE
                Case "02" ' 02:PEX
                    isPex = True
            End Select

            If Me.RT_CD.Value.Equals("A0057") Then
                Select Case GOODS_CLASS
                    Case "05", "06"
                        isPex = True
                End Select
            End If

            Dim dsM035_COMMON_ACCOUNT_TYPE As TriphooB2CAPI.M035_COMMON_ACCOUNT_TYPE_DataSet =
                B2CAPIClient.SelectM035_COMMON_ACCOUNT_TYPEGateway(Me.RT_CD.Value, "", "", dsB2CUser)
            '接続先切替対応
            Dim dsM035_COMMON_ACCOUNT_TYPE_A0057 As TriphooB2CAPI.M035_COMMON_ACCOUNT_TYPE_DataSet
            If ServiceVendor.IsNSSOL Then
                'NOP
            Else
                dsM035_COMMON_ACCOUNT_TYPE_A0057 = wwwB2CClient.SelectM035_COMMON_ACCOUNT_TYPEGateway("A0057", "", "", dsB2CUser)
            End If

            Dim dtAGE_UNIT_PRICE As New DataTable
            dtAGE_UNIT_PRICE.Columns.Add("AGE_KBN", GetType(String))
            dtAGE_UNIT_PRICE.Columns.Add("UNIT_PRICE", GetType(Integer))

            For Each row In dtRES_ORDER_DATA.Rows
                Dim ACCOUNT_TYPE_CD As String = row("ACCOUNT_TYPE_CD")

                Dim AGE_KBN As String = ""
                Dim UNIT_PRICE As Integer = row("SALES_SUB_TOTAL")

                Dim rM035_COMMON_ACCOUNT_TYPE() As DataRow = dsM035_COMMON_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Select("ACCOUNT_TYPE_CD='" & ACCOUNT_TYPE_CD & "'")

                Select Case FARE_TYPE
                    Case "01" ' 01:IT

                        If 0 < rM035_COMMON_ACCOUNT_TYPE.Length Then
                            Dim COMMON_ACCOUNT_TYPE_CD As String = rM035_COMMON_ACCOUNT_TYPE(0)("COMMON_ACCOUNT_TYPE_CD")
                            Select Case COMMON_ACCOUNT_TYPE_CD
                                Case "ADTPKG", "ADTPKGD"
                                    AGE_KBN = "ADT"
                                Case "CHDPKG", "CHDPKGD"
                                    AGE_KBN = "CHD"
                                Case "INFPKG", "INFPKGD"
                                    AGE_KBN = "INF"
                            End Select
                        Else
                            Select Case ACCOUNT_TYPE_CD
                                Case "ADTPKG", "ADTPKGD"
                                    AGE_KBN = "ADT"
                                Case "CHDPKG", "CHDPKGD"
                                    AGE_KBN = "CHD"
                                Case "INFPKG", "INFPKGD"
                                    AGE_KBN = "INF"
                            End Select
                        End If

                    Case "02" ' 02:PEX

                        If 0 < rM035_COMMON_ACCOUNT_TYPE.Length Then
                            Dim COMMON_ACCOUNT_TYPE_CD As String = rM035_COMMON_ACCOUNT_TYPE(0)("COMMON_ACCOUNT_TYPE_CD")
                            If COMMON_ACCOUNT_TYPE_CD.Contains("ADT") Then
                                AGE_KBN = "ADT"
                            ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("CHD") Then
                                AGE_KBN = "CHD"
                            ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("INF") Then
                                AGE_KBN = "INF"
                            End If
                        Else
                            If ACCOUNT_TYPE_CD.Contains("ADT") Then
                                AGE_KBN = "ADT"
                            ElseIf ACCOUNT_TYPE_CD.Contains("CHD") Then
                                AGE_KBN = "CHD"
                            ElseIf ACCOUNT_TYPE_CD.Contains("INF") Then
                                AGE_KBN = "INF"
                            End If
                        End If

                End Select

                If AGE_KBN.Equals("") Then
                    Continue For
                End If

                dtAGE_UNIT_PRICE.Rows.Add(AGE_KBN, UNIT_PRICE)

            Next

            Dim q = From row In dtAGE_UNIT_PRICE
                    Group By _AGE_KBN = row.Item("AGE_KBN")
                            Into _SUB_SUM_TOTAL = Sum(CInt(row.Item("UNIT_PRICE")))

            Dim isOK As Boolean = True
            Dim REPORT_PRICE_TOTAL As Integer = 0

            For Each row In q

                Dim REPORT_PRICE As Integer = 0
                Dim AGE_KBN As String = row._AGE_KBN
                Dim SUB_SUM_TOTAL As String = row._SUB_SUM_TOTAL

                Select Case AGE_KBN
                    Case "ADT" : SUB_SUM_TOTAL = SUB_SUM_TOTAL / NUM_ADULT
                    Case "CHD" : SUB_SUM_TOTAL = SUB_SUM_TOTAL / NUM_CHILD
                    Case "INF" : SUB_SUM_TOTAL = SUB_SUM_TOTAL / NUM_INFANT
                End Select

                'Dim dsM102_RT_PACKAGE_REPORT_MONEY As DataSet = TriphooRMClient.SelectM102_RT_PACKAGE_REPORT_MONEY_FOR_WEBGateway(Me.RT_CD.Value, Me.S_CD.Value, SUB_SUM_TOTAL, diffDays, DEP_DATE, FARE_TYPE, dsUser)

                Dim dsM102_RT_PACKAGE_REPORT_MONEY As DataSet = TriphooRMClient.SelectM102_RT_PACKAGE_REPORT_MONEY_FOR_WEBGateway(Me.RT_CD.Value, Me.S_CD.Value, SUB_SUM_TOTAL, diffDays, FARE_TYPE, dsUser)

                If Not dsM102_RT_PACKAGE_REPORT_MONEY Is Nothing AndAlso 0 < dsM102_RT_PACKAGE_REPORT_MONEY.Tables("M102_RT_PACKAGE_REPORT_MONEY").Rows.Count Then
                    Dim RATE_PRICE_KBN As String = dsM102_RT_PACKAGE_REPORT_MONEY.Tables("M102_RT_PACKAGE_REPORT_MONEY").Rows(0)("RATE_PRICE_KBN") '01:利率 02:金額
                    Dim RATE_PRICE As Decimal = dsM102_RT_PACKAGE_REPORT_MONEY.Tables("M102_RT_PACKAGE_REPORT_MONEY").Rows(0)("RATE_PRICE")
                    Dim REPORT_MONEY_LIMIT_DAY As Integer = dsM102_RT_PACKAGE_REPORT_MONEY.Tables("M102_RT_PACKAGE_REPORT_MONEY").Rows(0)("REPORT_MONEY_LIMIT_DATE")
                    Dim RECEIPT_LIMIT_DAY As Integer = dsM102_RT_PACKAGE_REPORT_MONEY.Tables("M102_RT_PACKAGE_REPORT_MONEY").Rows(0)("RECEIPT_LIMIT_DATE")
                    Dim DEPOSIT_LIMIT_KBN As String = SetRRValue.setNothingValue(dsM102_RT_PACKAGE_REPORT_MONEY.Tables("M102_RT_PACKAGE_REPORT_MONEY").Rows(0)("DEPOSIT_LIMIT_KBN")) '1:予約後 2:出発前
                    Dim RECEIPT_LIMIT_KBN As String = SetRRValue.setNothingValue(dsM102_RT_PACKAGE_REPORT_MONEY.Tables("M102_RT_PACKAGE_REPORT_MONEY").Rows(0)("RECEIPT_LIMIT_KBN")) '1:予約後 2:出発前

                    Select Case RATE_PRICE_KBN
                        Case "01"
                            REPORT_PRICE = SUB_SUM_TOTAL * (RATE_PRICE / 100)
                        Case "02", "03"
                            REPORT_PRICE = RATE_PRICE
                    End Select

                    Select Case AGE_KBN
                        Case "ADT" : REPORT_PRICE = REPORT_PRICE * NUM_ADULT
                        Case "CHD" : REPORT_PRICE = REPORT_PRICE * NUM_CHILD
                        Case "INF" : REPORT_PRICE = REPORT_PRICE * NUM_INFANT
                    End Select

                    REPORT_PRICE_TOTAL += REPORT_PRICE

                    Select Case DEPOSIT_LIMIT_KBN '1:予約後 2:出発前
                        Case "1"
                            REPORT_LIMIT_DATE = Today.AddDays(REPORT_MONEY_LIMIT_DAY)
                        Case "2"
                            REPORT_LIMIT_DATE = DEP_DATE.AddDays(-REPORT_MONEY_LIMIT_DAY)
                    End Select

                    If 1900 < REPORT_LIMIT_DATE.Year And REPORT_LIMIT_DATE < Today Then
                        REPORT_LIMIT_DATE = Today
                    End If

                    Select Case RECEIPT_LIMIT_KBN '1:予約後 2:出発前
                        Case "1"
                            RECEIPT_LIMIT_DATE = Today.AddDays(RECEIPT_LIMIT_DAY)
                        Case "2"
                            RECEIPT_LIMIT_DATE = DEP_DATE.AddDays(-RECEIPT_LIMIT_DAY)
                    End Select

                    '入金期限が厳しい方をセット
                    Dim _RECEIPT_LIMIT_DATE As DateTime = SetRRValue.setDate(dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE"))

                    If 1900 < RECEIPT_LIMIT_DATE.Year AndAlso 1900 < _RECEIPT_LIMIT_DATE.Year AndAlso _RECEIPT_LIMIT_DATE < RECEIPT_LIMIT_DATE Then
                        RECEIPT_LIMIT_DATE = _RECEIPT_LIMIT_DATE
                    End If

                    If 1900 < RECEIPT_LIMIT_DATE.Year And RECEIPT_LIMIT_DATE < Today Then
                        RECEIPT_LIMIT_DATE = Today
                    End If

                Else
                    isOK = False
                    Exit For
                End If
            Next

            If REPORT_PRICE_TOTAL = 0 Then
                isOK = False
            End If

            If isOK Then
                dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE") = RECEIPT_LIMIT_DATE.ToString("yyyy/MM/dd") & " 15:00"
                dsItinerary.PAGE_03.Rows(0)("TEMP_RECEIPT_LIMIT_DATE") = REPORT_LIMIT_DATE.ToString("yyyy/MM/dd") & " 15:00"
                dsItinerary.PAGE_03.Rows(0)("REPORT_MONEY") = REPORT_PRICE_TOTAL
                Me.REPORT_MONEY.Text = "￥" & MoneyComma.add(REPORT_PRICE_TOTAL, "お申込み金については、お電話にてお問い合せください")
                Me.TEMP_RECEIPT_LIMIT_DATE.Text = CDate(dsItinerary.PAGE_03.Rows(0)("TEMP_RECEIPT_LIMIT_DATE")).ToString("yyyy年M月d日 H時")

                If TKT_SUP_CD.Equals("LOWFARE") Then
                    dsItinerary.PAGE_05.Rows(0)("SUP_FINLAL_LIMIT_DATE") = REPORT_LIMIT_DATE.ToString("yyyy/MM/dd")
                End If

            Else
                Me.REPORT_MONEYPanel.Visible = False
            End If

            Select Case Me.RT_CD.Value
                Case "ATR", "A0506", "RT01", "ASX", "ADV"
                    CalcReportMoney(dsItinerary)
            End Select


            ' /* 航空券の取り消し規定 */
            If isPex Then

                Dim TKT_PRICE As Integer = 0
                Dim TKT_UNIT_PRICE_ADT As Integer = 0  '商材区分：航空券　一人当たりの合計
                Dim TKT_UNIT_PRICE_CHD As Integer = 0  '商材区分：航空券　一人当たりの合計
                Dim TKT_UNIT_PRICE_INF As Integer = 0  '商材区分：航空券　一人当たりの合計

                ' 一人当たりの集計 (共通仕入先に関係なく、年代のみ？)
                For Each row In dtRES_ORDER_DATA.Rows
                    Dim ACCOUNT_TYPE_CD As String = row("ACCOUNT_TYPE_CD")

                    Dim AGE_KBN As String = ""
                    Dim SUP_UNIT As Integer = row("SUP_UNIT")

                    If Not row("GOODS_KBN").Equals("01") Then
                        Continue For
                    End If

                    Dim rM035_COMMON_ACCOUNT_TYPE() As DataRow =
                        dsM035_COMMON_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Select("ACCOUNT_TYPE_CD = '" & ACCOUNT_TYPE_CD & "'")

                    If rM035_COMMON_ACCOUNT_TYPE.Length > 0 Then
                        Dim COMMON_ACCOUNT_TYPE_CD As String = rM035_COMMON_ACCOUNT_TYPE(0)("COMMON_ACCOUNT_TYPE_CD")
                        If COMMON_ACCOUNT_TYPE_CD.Contains("ADT") Then
                            TKT_UNIT_PRICE_ADT += SUP_UNIT
                        ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("CHD") Then
                            TKT_UNIT_PRICE_CHD += SUP_UNIT
                        ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("INF") Then
                            TKT_UNIT_PRICE_INF += SUP_UNIT
                        End If
                    Else
                        If ACCOUNT_TYPE_CD.Contains("ADT") Then
                            TKT_UNIT_PRICE_ADT += SUP_UNIT
                        ElseIf ACCOUNT_TYPE_CD.Contains("CHD") Then
                            TKT_UNIT_PRICE_CHD += SUP_UNIT
                        ElseIf ACCOUNT_TYPE_CD.Contains("INF") Then
                            TKT_UNIT_PRICE_INF += SUP_UNIT
                        End If
                    End If
                Next

                ' 年代関係なく合計の集計 (共通仕入先に関係なく、年代のみ？)
                For Each row In dtRES_ORDER_DATA.Rows

                    Dim ACCOUNT_TYPE_CD As String = row("ACCOUNT_TYPE_CD")

                    Dim AGE_KBN As String = ""
                    Dim SUP_UNIT As Integer = row("SUP_UNIT")
                    Dim SUP_SUB_TOTAL As Integer = row("SUP_SUB_TOTAL")

                    If Not row("GOODS_KBN").Equals("01") Then
                        Continue For
                    End If

                    Dim rM035_COMMON_ACCOUNT_TYPE() As DataRow =
                        dsM035_COMMON_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Select("ACCOUNT_TYPE_CD = '" & ACCOUNT_TYPE_CD & "'")

                    If rM035_COMMON_ACCOUNT_TYPE.Length > 0 Then

                        Dim COMMON_ACCOUNT_TYPE_CD As String = rM035_COMMON_ACCOUNT_TYPE(0)("COMMON_ACCOUNT_TYPE_CD")
                        If COMMON_ACCOUNT_TYPE_CD.Contains("ADT") Then

                            Select Case COMMON_ACCOUNT_TYPE_CD
                                Case "ADT03", "7400ADT"
                                    TKT_PRICE += SUP_SUB_TOTAL
                            End Select

                        ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("CHD") Then

                            Select Case COMMON_ACCOUNT_TYPE_CD
                                Case "CHD03", "7400CHD"
                                    TKT_PRICE += SUP_SUB_TOTAL
                            End Select

                        ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("INF") Then

                            Select Case COMMON_ACCOUNT_TYPE_CD
                                Case "INF03", "7400INF"
                                    TKT_PRICE += SUP_SUB_TOTAL
                            End Select

                        End If
                    Else
                        If ACCOUNT_TYPE_CD.Contains("ADT") Then

                            Select Case ACCOUNT_TYPE_CD
                                Case "1009ADT", "7400ADT"
                                    TKT_PRICE += SUP_SUB_TOTAL
                            End Select

                        ElseIf ACCOUNT_TYPE_CD.Contains("CHD") Then

                            Select Case ACCOUNT_TYPE_CD
                                Case "1009CHD", "7400CHD"
                                    TKT_PRICE += SUP_SUB_TOTAL
                            End Select

                        ElseIf ACCOUNT_TYPE_CD.Contains("INF") Then

                            Select Case ACCOUNT_TYPE_CD
                                Case "1009INF", "7400INF"
                                    TKT_PRICE += SUP_SUB_TOTAL
                            End Select

                        End If
                    End If
                Next

                For Each row In dtRES_ORDER_DATA_COST.Rows

                    Dim ACCOUNT_TYPE_CD As String = row("ACCOUNT_TYPE_CD")

                    Dim AGE_KBN As String = ""
                    Dim SUP_UNIT As Integer = row("SUP_UNIT")
                    Dim SUP_SUB_TOTAL As Integer = row("SUP_SUB_TOTAL")

                    Dim rM035_COMMON_ACCOUNT_TYPE() As DataRow =
                        dsM035_COMMON_ACCOUNT_TYPE.M035_COMMON_ACCOUNT_TYPE.Select("ACCOUNT_TYPE_CD = '" & ACCOUNT_TYPE_CD & "'")

                    Select Case TKT_GDS_KBN
                        Case "44"
                            If Not Me.RT_CD.Value.Equals("A0057") Then

                                Dim REMARKS As String = SetRRValue.setNothingValue(row("REMARKS"))
                                If REMARKS.Equals("") Then
                                    Continue For
                                End If

                                rM035_COMMON_ACCOUNT_TYPE =
                                    dsM035_COMMON_ACCOUNT_TYPE_A0057.M035_COMMON_ACCOUNT_TYPE.Select("ACCOUNT_TYPE_CD = '" & ACCOUNT_TYPE_CD & "'")

                            End If
                    End Select

                    If rM035_COMMON_ACCOUNT_TYPE.Length > 0 Then
                        Dim COMMON_ACCOUNT_TYPE_CD As String = rM035_COMMON_ACCOUNT_TYPE(0)("COMMON_ACCOUNT_TYPE_CD")
                        If COMMON_ACCOUNT_TYPE_CD.Contains("ADT") Then

                            Select Case COMMON_ACCOUNT_TYPE_CD
                                Case "ADT00", "ADT00D", "ADDAIRADT", "ADT03D"
                                    TKT_PRICE += SUP_SUB_TOTAL
                                    TKT_UNIT_PRICE_ADT += SUP_UNIT
                            End Select

                        ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("CHD") Then

                            Select Case COMMON_ACCOUNT_TYPE_CD
                                Case "CHD00", "CHD00D", "ADDAIRCHD", "CHD03D"
                                    TKT_PRICE += SUP_SUB_TOTAL
                                    TKT_UNIT_PRICE_CHD += SUP_UNIT
                            End Select

                        ElseIf COMMON_ACCOUNT_TYPE_CD.Contains("INF") Then

                            Select Case COMMON_ACCOUNT_TYPE_CD
                                Case "INF00", "INF00D", "ADDAIRINF", "INF03D"
                                    TKT_PRICE += SUP_SUB_TOTAL
                                    TKT_UNIT_PRICE_INF += SUP_UNIT
                            End Select

                        End If
                    Else
                        If ACCOUNT_TYPE_CD.Contains("ADT") Then

                            Select Case ACCOUNT_TYPE_CD
                                Case "1000ADT", "4000ADT"
                                    TKT_PRICE += SUP_SUB_TOTAL
                                    TKT_UNIT_PRICE_ADT += SUP_UNIT
                            End Select

                        ElseIf ACCOUNT_TYPE_CD.Contains("CHD") Then

                            Select Case ACCOUNT_TYPE_CD
                                Case "1000CHD", "4000CHD"
                                    TKT_PRICE += SUP_SUB_TOTAL
                                    TKT_UNIT_PRICE_CHD += SUP_UNIT
                            End Select

                        ElseIf ACCOUNT_TYPE_CD.Contains("INF") Then

                            Select Case ACCOUNT_TYPE_CD
                                Case "1000INF", "4000INF"
                                    TKT_PRICE += SUP_SUB_TOTAL
                                    TKT_UNIT_PRICE_INF += SUP_UNIT
                            End Select
                        Else
                            TKT_PRICE += SUP_SUB_TOTAL
                            TKT_UNIT_PRICE_ADT += SUP_UNIT
                        End If
                    End If
                Next

                If dsItinerary.PAGE_07.Rows.Count > 0 Then

                    Dim Str As String = ""

                    For Each row In dsItinerary.PAGE_07.Rows
                        Dim FB_ADT As String = row("FB_ADT")
                        If FB_ADT.Equals("") Then
                            Continue For
                        End If
                        Str += FB_ADT & ", "
                    Next
                    Str = Str.TrimEnd(", ")

                    Dim ss() As String = Str.Split(", ")

                    Dim FARE_BASIS As String = ""
                    For Each row In ss
                        If Not FARE_BASIS.Contains(row.Trim()) Then
                            FARE_BASIS += row & ", "
                        End If
                    Next
                    FARE_BASIS = FARE_BASIS.TrimEnd(", ")

                    Dim BOOKING_CLASS As String = ""
                    Dim SEAT_CLASS As String = ""

                    For Each row In dsItinerary.PAGE_07.Rows

                        BOOKING_CLASS += row("BOOKING_CLASS") & ", "

                        If CStr(row("SEAT_NM")).Contains("プレミアムエコノミー") Then
                            SEAT_CLASS += "P, "
                        ElseIf CStr(row("SEAT_NM")).Contains("エコノミー") Then
                            SEAT_CLASS += "Y, "
                        ElseIf CStr(row("SEAT_NM")).Contains("ビジネス") Then
                            SEAT_CLASS += "C, "
                        ElseIf CStr(row("SEAT_NM")).Contains("ファースト") Then
                            SEAT_CLASS += "F, "
                        End If

                    Next
                    BOOKING_CLASS = BOOKING_CLASS.TrimEnd(", ")
                    SEAT_CLASS = SEAT_CLASS.TrimEnd(", ")

                    Dim rPAGE_07() As DataRow = dsItinerary.PAGE_07.Select("GOING_RETURN_KBN='01'", "SEGMENT_SEQ ASC")

                    Dim COUNTRY_CD As String = ""
                    Dim ARR_CD As String = rPAGE_07(0)("ARR_CD")
                    Dim dsM005_AIRPORT As DataSet = TriphooRMClient.SelectM005_AIRPORTGateway(ARR_CD, dsUser)
                    If 0 < dsM005_AIRPORT.Tables(0).Rows.Count Then
                        COUNTRY_CD = dsM005_AIRPORT.Tables(0).Rows(0)("COUNTRY_CD")
                        ARR_CD = dsM005_AIRPORT.Tables(0).Rows(0)("CITY_CD")
                    Else
                        Dim dsM004_CITY As TriphooRMWebService.M004_CITY_DataSet = TriphooRMClient.SelectM004_CITYGateway(ARR_CD, dsUser)
                        If 0 < dsM004_CITY.Tables(0).Rows.Count Then
                            COUNTRY_CD = dsM004_CITY.Tables(0).Rows(0)("COUNTRY_CD")
                        End If
                    End If

                    Select Case ARR_CD
                        Case "HNL", "KOA", "ITO", "OGG", "LIH", "JHM", "MKK", "LNY"
                            COUNTRY_CD = "HI"
                    End Select

                    Dim dsM194_RT_WEB_PACKAGE_CARRIER_PEX_CONDITION As DataSet =
                        B2CAPIClient.SelectM194_RT_WEB_PACKAGE_CARRIER_PEX_CONDITION_FOR_WEBGateway(
                            Me.RT_CD.Value, AIR_COMPANY_CD, COUNTRY_CD, ARR_CD, SEAT_CLASS.Replace(" ", ""), BOOKING_CLASS.Replace(" ", ""), dsB2CUser)

                    Dim PEX_CONDITIONS As String = ""

                    If Not dsM194_RT_WEB_PACKAGE_CARRIER_PEX_CONDITION Is Nothing AndAlso dsM194_RT_WEB_PACKAGE_CARRIER_PEX_CONDITION.Tables(0).Rows.Count > 0 Then
                        PEX_CONDITIONS = dsM194_RT_WEB_PACKAGE_CARRIER_PEX_CONDITION.Tables(0).Rows(0)("CONTENTS")
                    Else
                        Select Case Me.RT_CD.Value
                            Case "A0057", "TWC"
                                If TKT_PRICE > 0 Then
                                    PEX_CONDITIONS += "航空券取消料(一名様あたり)  "
                                    If 0 < NUM_ADULT Then
                                        PEX_CONDITIONS += "大人：" & MoneyComma.add(TKT_UNIT_PRICE_ADT, "お問合せ") & "円　"
                                    End If
                                    If 0 < NUM_CHILD Then
                                        PEX_CONDITIONS += "子供：" & MoneyComma.add(TKT_UNIT_PRICE_CHD, "お問合せ") & "円　"
                                    End If
                                    If 0 < NUM_INFANT Then
                                        PEX_CONDITIONS += "幼児：" & MoneyComma.add(TKT_UNIT_PRICE_INF, "お問合せ") & "円　"
                                    End If
                                    PEX_CONDITIONS += "<br/>※お支払い完了時点より発生いたします"
                                    PEX_CONDITIONS += "<br/>※約款規定に基づく取消料の高い方が適用となります"
                                End If
                            Case "58"
                                ' Nothing
                            Case Else
                                If TKT_PRICE > 0 Then
                                    PEX_CONDITIONS = LABEL_0091 & MoneyComma.addYen2(TKT_PRICE, "お問合せ")
                                End If
                        End Select
                    End If

                    If Not FARE_BASIS.Equals("") Then
                        Me.FARE_BASIS.Text = FARE_BASIS
                    Else
                        Me.FARE_BASISPanel.Visible = False
                    End If

                    If Not PEX_CONDITIONS.Equals("") Then
                        Me.PEX_CONDITIONS.Text = PEX_CONDITIONS
                        If dsItinerary.PAGE_05.Rows.Count > 0 Then
                            dsItinerary.PAGE_05.Rows(0)("REMARKS") = PEX_CONDITIONS
                            'Session.Add("Itinerary" & Me._RT_CD.Value & Me.S_CD.Value, dsItinerary)
                            WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)

                        End If
                    Else
                        Me.PEX_CONDITIONS_Panel.Visible = False
                    End If

                Else
                    Me.PEX_CONDITIONS_Panel.Visible = False
                    Me.FARE_BASISPanel.Visible = False
                End If
            Else
                Me.PEX_CONDITIONS_Panel.Visible = False
                Me.FARE_BASISPanel.Visible = False
            End If

        Else
            '逃げ
            Me.PEX_CONDITIONS_Panel.Visible = False
            Me.FARE_BASISPanel.Visible = False
            Me.REPORT_MONEYPanel.Visible = False
        End If

        Select Case Me.RT_CD.Value
            Case "A0500"
                If TICKET_FLG Or HOTEL_FLG Then
                    If Not CDate(dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE")).ToString("yyyy年M月d日 15時").Equals("1900") Then
                        If lang.Equals("1") Then
                            Me.RECEIPT_LIMIT_DATE.Text = CDate(dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE")).ToString("yyyy年M月d日 15時") & "までにお振り込み下さい。"
                        Else
                            ' Please transfer the total amount by 14/JUN/2022 15:00
                            Dim _strDate As String = CDate(dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE")).ToString("d/MMM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                            Me.RECEIPT_LIMIT_DATE.Text = "Please transfer the total amount by " & _strDate & " 15:00."
                        End If
                    End If
                End If
        End Select


        'b2bの場合、支払いパネルは非表示とする
        If MEMBER_ADD_KBN.Equals("3") Then
            Me.PayPanel.Visible = False
            Me.RES_COMMIT_PANEL.Visible = True
            Me.RES_COMMIT_TOKEN_PANEL.Visible = False
            Me.PEX_CONDITIONS_Panel.Visible = False
            Me.FARE_BASISPanel.Visible = False
            Me.REPORT_MONEYPanel.Visible = False

            Me.EconTokenPay.Visible = False
            Me.GmoTokenPay.Visible = False
            Me.GmoTokenPaySmbc.Visible = False
            Me.GmoTokenPaySmbcStation.Visible = False
            Me.VeriTrans4GTokenPay.Visible = False
            Me.GmoTokenPaySmbcStation.ID = "dummy4"
            Me.GmoTokenPay.ID = "dummy1"
            Me.GmoTokenPaySmbc.ID = "dummy2"
            Me.EconTokenPay.ID = "dummy3"
            Me.VeriTrans4GTokenPay.ID = "dummy5"
        End If

        Dim WAITING_ALLOWED As Boolean = False
        If 0 < dsItinerary.PAGE_20.Rows.Count Then
            Try
                WAITING_ALLOWED = dsItinerary.PAGE_20.Rows(0)("WAITING_ALLOWED")
            Catch ex As Exception
            End Try

            If WAITING_ALLOWED Then
                Me.PayPanel.Visible = False
                Me.RES_COMMIT_PANEL.Visible = True
                Me.RES_COMMIT_TOKEN_PANEL.Visible = False
                Me.PEX_CONDITIONS_Panel.Visible = False
                Me.FARE_BASISPanel.Visible = False
                Me.REPORT_MONEYPanel.Visible = False

                Me.EconTokenPay.Visible = False
                Me.GmoTokenPay.Visible = False
                Me.GmoTokenPaySmbc.Visible = False
                Me.GmoTokenPaySmbcStation.Visible = False
                Me.VeriTrans4GTokenPay.Visible = False
                Me.GmoTokenPaySmbcStation.ID = "dummy4"
                Me.GmoTokenPay.ID = "dummy1"
                Me.GmoTokenPaySmbc.ID = "dummy2"
                Me.EconTokenPay.ID = "dummy3"
                Me.VeriTrans4GTokenPay.ID = "dummy5"
            End If

        End If


        If 0 < dsItinerary.PAGE_04.Rows.Count AndAlso dsItinerary.PAGE_04.Select("AGE = ''").Length = 0 Then

            Dim rCheckAge() As DataRow = dsItinerary.PAGE_04.Select("AGE < 20")
            If dsItinerary.PAGE_04.Rows.Count = rCheckAge.Length Then
                Me.ConsentFormPanel.Visible = True
            End If
        End If

        Dim dsImportant As New DataSet

        If PACKAGE_FLG Then
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important001_tour", lang)) ' important001 : 重要事項・１
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important002_tour", lang)) ' important002 : 重要事項・２
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important003_tour", lang)) ' important003 : 重要事項・３
        End If

        If TICKET_FLG Then
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important001_air", lang)) ' important001 : 重要事項・１
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important002_air", lang)) ' important002 : 重要事項・２
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important003_air", lang)) ' important003 : 重要事項・３
        End If

        If HOTEL_FLG Then
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important001_hotel", lang)) ' important001 : 重要事項・１
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important002_hotel", lang)) ' important002 : 重要事項・２
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important003_hotel", lang)) ' important003 : 重要事項・３
        End If

        If TICKET_FLG And HOTEL_FLG Then
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important001_dp", lang)) ' important001 : 重要事項・１
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important002_dp", lang)) ' important002 : 重要事項・２
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important003_dp", lang)) ' important003 : 重要事項・３
        End If

        If OPTION_FLG Then
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important001_option", lang)) ' important001 : 重要事項・１
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important002_option", lang)) ' important002 : 重要事項・２
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important003_option", lang)) ' important003 : 重要事項・３
        End If

        If dsImportant.Tables("DETAIL_RES").Rows.Count = 0 Then
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important001", lang)) ' important001 : 重要事項・１
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important002", lang)) ' important002 : 重要事項・２
            dsImportant.Merge(CommonUtil.REGULATION(Me.RT_CD.Value, Me.S_CD.Value, "important003", lang)) ' important003 : 重要事項・３
        End If


        Dim dvDETAIL_RES As DataView = dsImportant.Tables("DETAIL_RES").DefaultView
        dvDETAIL_RES.RowFilter = "NOT CONTENTS='' OR NOT URL = ''"

        Dim dtDETAIL_RES As DataTable = dvDETAIL_RES.ToTable(True)

        If 0 < dtDETAIL_RES.Rows.Count Then

            Me.ImportantRepeater.DataSource = dtDETAIL_RES
            Me.ImportantRepeater.DataBind()

        Else

            Me.AgreeImportantPanel.Visible = False
            Me.ImportantPanel.Visible = False

        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "iniPage()", 2309, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#End Region

#Region "アクション"

#Region "予約確定"
    Protected Sub CONFIRMLinkButton_Click(dsItinerary As TriphooRR097DataSet, dsUser As DataSet, isSecured As Boolean)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2322, "開始", HttpContext.Current.Session.SessionID)

        Dim SESSION_EDIT_TIME As String = dsItinerary.PAGE_09.Rows(0)("EDIT_TIME")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")
        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")

        If dsItinerary.M019_CLIENT.Rows.Count = 0 Then
            Dim ErrMsg As String =
           LABEL_0039 & "\n" &
           LABEL_0040
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2337, "終了", HttpContext.Current.Session.SessionID)

            Exit Sub
        End If

        If CDate(Me.EDIT_TIME.Value) < CDate(SESSION_EDIT_TIME) Then
            Dim ErrMsg As String =
           LABEL_0039 & "\n" &
           LABEL_0040
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2349, "終了", HttpContext.Current.Session.SessionID)

            Exit Sub
        End If

        If Me.Agree1Panel.Visible Then
            Dim ErrMsg As String = ""
            If Not Me.Agree1CheckBox.Checked Then
                ErrMsg += LABEL_0086 & "\n"
            End If
            If Not ErrMsg.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

                ' ログ生成 NSSOL負荷性能検証 2023/02/09
                'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2363, "終了", HttpContext.Current.Session.SessionID)

                Exit Sub
            End If
        End If

        If Me.Agree2Panel.Visible Then
            Dim ErrMsg As String = ""
            If Not Me.Agree2CheckBox.Checked Then
                ErrMsg += LABEL_0087 & "\n"
            End If
            If Not ErrMsg.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

                ' ログ生成 NSSOL負荷性能検証 2023/02/09
                'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2378S, "終了", HttpContext.Current.Session.SessionID)

                Exit Sub
            End If
        End If

        If Me.AgreeImportantPanel.Visible Then
            Dim ErrMsg As String = ""
            If Not Me.AgreeImportantCheckBox.Checked Then
                ErrMsg += LABEL_0083 & "\n"
            End If
            If Not ErrMsg.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

                ' ログ生成 NSSOL負荷性能検証 2023/02/09
                'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2393, "終了", HttpContext.Current.Session.SessionID)

                Exit Sub
            End If
        End If

        If Me.AgreeCheckPanel.Visible Then
            Dim ErrMsg As String = ""
            If Not Me.AgreeCheckBox.Checked Then

                If Me.TOUR_CANCELPOLICY_ModalButton.Visible Or Me.TOUR_CANCELPOLICY_LinkButton.Visible Then
                    ErrMsg += LABEL_0077 & "\n"
                Else
                    ErrMsg += LABEL_0051 & "\n"
                End If

            End If
            If Not ErrMsg.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

                ' ログ生成 NSSOL負荷性能検証 2023/02/09
                'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2414, "終了", HttpContext.Current.Session.SessionID)

                Exit Sub
            End If
        End If

        Dim RES_METHOD_KBN As String = dsItinerary.PAGE_03.Rows(0)("RES_METHOD_KBN")

        ' ●予約記録重複チェック
        Dim isDupe As Boolean = False

        If OPTION_FLG And Not TICKET_FLG And Not HOTEL_FLG And Not PACKAGE_FLG Then
        Else
            Select Case Me.RT_CD.Value
                Case "RT01", "ATR"
                Case Else

                    If RES_METHOD_KBN.Equals("01") Then
                        Try
                            isDupe = CartUtil.bookDupe(dsItinerary)
                        Catch ex As Exception
                        End Try
                    End If

            End Select

        End If

        Dim isFcDupe As Boolean = False
        If 0 < dsItinerary.PAGE_20.Rows.Count Then
            Try
                isFcDupe = CartUtil.bookDupeFcNo(dsItinerary, Me.RT_CD.Value)
            Catch ex As Exception
            End Try
        End If

        If isDupe Then
            Dim ErrMsg As String =
            LABEL_0041 & "\n" &
            LABEL_0042
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2457, "終了", HttpContext.Current.Session.SessionID)

            Exit Sub
        ElseIf isFcDupe Then
            Dim ErrMsg As String = "入力したファンクラブ番号でのお申し込みは既に承っています。\n重複してのお申し込みはできません。"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2465, "終了", HttpContext.Current.Session.SessionID)

            Exit Sub
        Else
            'ログテーブル クリア
            dsItinerary.RT_SYS_LOG.Clear()

            ' 予約時の日時とセッションIDを保持する
            Dim orderID As String = Now.ToString("yyyyMMddHHmmsss")

            '/***支払処理***/

            If Me.PayPanel.Visible Then
                If Not Me.RES_CREDITRadioButton.Checked And
                   Not Me.RES_BANKRadioButton.Checked And
                   Not Me.RES_CREDIT_REQUESTRadioButton.Checked Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & LABEL_0043 & "');", True)

                    ' ログ生成 NSSOL負荷性能検証 2023/02/09
                    'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2484, "終了", HttpContext.Current.Session.SessionID)

                    Exit Sub
                End If
            End If

            If dsItinerary.PAGE_03(0)("SETTLE_STATUS_KBN").Equals("00") Then
                Dim ErrMsg As String = "決済処理が中断された恐れがあります。\nお手数ですがお問合せお願い致します。"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)
                Exit Sub
            End If

            Dim okFlg As Boolean = False
            dsItinerary.E_CONNECT.Clear()

            If Me.PayPanel.Visible Then

                If Me.RES_CREDITRadioButton.Checked And Me.CreditDetailPanel.Visible Then

                    Dim SESSION_ID As String = System.Web.HttpContext.Current.Session.SessionID
                    'okFlg = PayCredit(dsItinerary, _NOW, SESSION_ID, "08")
                    Dim dsM031_RT_APP_KBN As DataSet = TriphooRMClient.SelectM031_RT_APP_KBNGateway(Me.RT_CD.Value, "06", dsUser)
                    Dim USER_ID As String = ""
                    Dim PASSWORD As String = ""
                    Dim TOKEN As String = ""
                    Dim SESSIONID As String = ""
                    Dim SecureUrl As String = ""
                    Dim PaReq As String = ""
                    Dim AuthKey As String = ""
                    Dim ServiceOptionType As String = "mpi-company"
                    Dim SecureFlg As Boolean = False '3Dセキュアフラグ

                    Try
                        USER_ID = SetRRValue.setDBNullValue(dsM031_RT_APP_KBN.Tables("M031_RT_APP_KBN").Rows(0)("USER_ID"))
                        PASSWORD = SetRRValue.setDBNullValue(dsM031_RT_APP_KBN.Tables("M031_RT_APP_KBN").Rows(0)("PASSWORD"))
                    Catch ex As Exception
                    End Try

                    ' M204対応　2022/08/26 照井
                    Dim dsM204_RT_SITE_CD_CARD_KBN As DataSet = B2CAPIClient.SelectM204_RT_SITE_CD_CARD_KBNGateway(Me.RT_CD.Value, Me.S_CD.Value, "", "", dsB2CUser)
                    Dim M204_FLG As Boolean = False

                    If Not dsM204_RT_SITE_CD_CARD_KBN Is Nothing AndAlso dsM204_RT_SITE_CD_CARD_KBN.Tables(0).Rows.Count > 0 Then
                        Dim SETTLE_PROXY_COMPANY_KBN As String = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("SETTLE_PROXY_COMPANY_KBN"))
                        Dim M204_USER_ID As String = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("USER_ID"))
                        Dim _CERTIFIED_KBN As String = SetRRValue.setDBNullValue(dsM204_RT_SITE_CD_CARD_KBN.Tables("M204_RT_SITE_CD_CARD_KBN").Rows(0)("CERTIFIED_KBN"))
                        If Not M204_USER_ID.Equals("") AndAlso Not SETTLE_PROXY_COMPANY_KBN.Equals("") Then
                            USER_ID = E_Connect.GetUSER_ID_BY_SETTLE_PROXY_COMPANY_KBN(SETTLE_PROXY_COMPANY_KBN)
                            PASSWORD = "TOKEN"
                            M204_FLG = True
                            If _CERTIFIED_KBN.Equals("1") Then
                                ServiceOptionType = "mpi-complete"
                            End If
                        End If
                    End If

                    Dim CHD_ONLY_ACCEPT_KBN As String = ""

                    If dsItinerary.WEB_TRANSACTION.Rows.Count = 0 Then
                        CHD_ONLY_ACCEPT_KBN = SetRRValue.setDBNullValue(dsItinerary.WEB_TRANSACTION.Rows(0)("CHD_ONLY_ACCEPT_KBN"))
                    End If

                    ' カード名義人
                    Dim cardPersonLastName As String = Me.CARD_HOLDER_SURNAME.Text.ToUpper
                    Dim cardPersonFirstName As String = Me.CARD_HOLDER_NAME.Text.ToUpper

                    If CHD_ONLY_ACCEPT_KBN.Equals("") Then

                        Select Case Me.RT_CD.Value
                            Case "A0027", "ATR", "58", "A0057", "LTK" 'LTK追加 2025.3.17 Terui
                            Case Else
                                If 0 < dsItinerary.PAGE_04.Rows.Count AndAlso 0 < dsItinerary.PAGE_04.Select("FAMILY_NAME <> '' And FIRST_NAME <> ''").Length Then
                                    Dim rPAGE_04() As DataRow = dsItinerary.PAGE_04.Select("FAMILY_NAME='" & cardPersonLastName & "' And FIRST_NAME='" & cardPersonFirstName & "'")

                                    If rPAGE_04.Length = 0 Then

                                        If Me.RES_BANKPanel.Visible Then
                                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & LABEL_0064 & "');", True)

                                            ' ログ生成 NSSOL負荷性能検証 2023/02/09
                                            'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2558, "終了", HttpContext.Current.Session.SessionID)

                                            Exit Sub
                                        Else
                                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & LABEL_0067 & "');", True)

                                            ' ログ生成 NSSOL負荷性能検証 2023/02/09
                                            'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2565, "終了", HttpContext.Current.Session.SessionID)

                                            Exit Sub
                                        End If

                                    End If
                                End If
                        End Select

                    End If

                    '多重決済解消のため
                    '入金状況を「入金処理中」へ更新 2024/05/31 Terui
                    dsItinerary.PAGE_03(0)("SETTLE_STATUS_KBN") = "00"

                    Select Case USER_ID

                        Case "GMO", "SMBCGMO"

                            Dim dsM077_RT_CARD_KBN As DataSet = TriphooRMClient.SelectM077_RT_CARD_KBNGateway(Me.RT_CD.Value, Me.CARD_KIND.SelectedValue, dsUser)

                            Dim ShopId As String = ""

                            If dsM077_RT_CARD_KBN.Tables(0).Rows.Count > 0 Then
                                ShopId = dsM077_RT_CARD_KBN.Tables(0).Rows(0)("USER_ID")
                            End If

                            If M204_FLG Then
                                ShopId = dsM204_RT_SITE_CD_CARD_KBN.Tables(0).Rows(0)("USER_ID")
                            End If

                            If PASSWORD.Equals("TOKEN") Then
                                TOKEN = Request.Item("token")
                            End If

                            okFlg = True

                            ' 与信処理. 与信を行わない場合はB2CAPIでの即時売上となる                            
                            'Select Case ShopId
                            '    Case "tshop00041691", "tshop00042679", "9200001419363", "9102311881397", "9102250174746", "1103551000001",
                            '         "1102868000011", "9200001856083", "9101626954482", "9101180439973", "9101138240150"
                            '        okFlg = PayGmo(dsItinerary, dsUser, orderID, USER_ID, SecureUrl, SESSION_ID, PaReq, SecureFlg, AuthKey)
                            'End Select

                            ' 主流を与信->売上の流れとする 22.10.18 Mitsuta
                            Select Case Me.RT_CD.Value
                                Case "58", "A0495", "A0500", "KIC", "SPLN", "UAS", "WAI"
                                Case Else
                                    okFlg = PayGmo(dsItinerary, dsUser, orderID, USER_ID, SecureUrl, SESSION_ID, PaReq, SecureFlg, AuthKey)
                            End Select

                        Case "SMBCSTATION"

                            If PASSWORD.Equals("TOKEN") Then
                                TOKEN = Request.Item("token")
                            End If

                            If isSecured Then

                            Else

                                Select Case Me.RT_CD.Value
                                    Case "A0027"
                                        orderID = "999" & Now.ToString("yyyyMMddHHmmss") '【17桁固定】
                                    Case "STW", "RT01"
                                        orderID = "777" & Now.ToString("yyyyMMddHHmmss") '【17桁固定】
                                End Select

                                okFlg = PayCreditSmbcStation(dsItinerary, orderID, TOKEN, SecureUrl, SESSION_ID, PaReq, SecureFlg)
                            End If

                        Case "AXES"

                            okFlg = True

                        Case "VERITRANS"

                            SESSION_ID = Me.requestID.Value
                            TOKEN = Me.token.Value
                            okFlg = PayCreditVeriTrans(dsItinerary, orderID, TOKEN, AuthKey, SecureFlg, ServiceOptionType)

                        Case "ECON"
                            If PASSWORD.Equals("TOKEN") Then
                                okFlg = True
                                SecureFlg = True
                                orderID = Me.orderID.Value
                                SESSION_ID = Me.requestID.Value
                                TOKEN = Me.token.Value
                            Else
                                okFlg = PayCredit(dsItinerary, orderID, SESSION_ID, "08")
                            End If

                    End Select

                    If Not okFlg Then
                        dsItinerary.PAGE_03(0)("SETTLE_STATUS_KBN") = "01"

                        'ECON 与信・失敗
                        Dim ErrMsg As String = ""
                        ErrMsg = LABEL_0044 & "\n"
                        ErrMsg += "\n"
                        ErrMsg += LABEL_0045 & "\n"
                        ErrMsg += LABEL_0046 & "\n"
                        ErrMsg += LABEL_0047 & "\n"
                        ErrMsg += LABEL_0048 & "\n"
                        Select Case Me.RT_CD.Value
                            Case "RT01", "ATR"
                                ErrMsg += LABEL_0095 & "\n" ' ・ご利用になるクレジットカード発行会社の3Dセキュア（クレジットカード本人認証サービス）のパスワード登録がされていない。
                                ErrMsg += LABEL_0096 & "\n" ' ※現在、不正利用防止の為お支払い時に3Dセキュアのパスワード認証が必須となっております。
                        End Select

                        ErrMsg += "\n"
                        ErrMsg += LABEL_0049 & "\n"
                        ErrMsg += LABEL_0050
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myscript", "alert('" & ErrMsg & "');", True)

                        ' ログ生成 NSSOL負荷性能検証 2023/02/09
                        'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2676, "終了", HttpContext.Current.Session.SessionID)

                        Exit Sub
                    Else
                        'ECON 与信・成功
                        dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = "01"
                        'セッション上に予約日時とセッションIDを保持する
                        dsItinerary.PAGE_03.Rows(0)("CREDIT_CERTIFIED_FIELD") = orderID

                        '/***カード情報保持***/

                        Dim CARD_KIND As String = Me.CARD_KIND.SelectedValue
                        Dim CARD_HOLDER_SURNAME As String = Me.CARD_HOLDER_SURNAME.Text
                        Dim CARD_HOLDER_NAME As String = Me.CARD_HOLDER_NAME.Text
                        Dim CARD_NO As String = Me.CARD_NO.Text
                        Dim CARD_SECURITY_CODE As String = Me.CARD_SECURITY_CODE.Text
                        Dim CARD_EXP_YEAR As String = Me.CARD_EXP_YEAR.SelectedValue
                        Dim CARD_EXP_MONTH As String = Me.CARD_EXP_MONTH.SelectedValue.PadLeft(2, "0")

                        If Not USER_ID.Equals("SMBCSTATION") Then
                            CARD_EXP_YEAR = CARD_EXP_YEAR.PadLeft(4, "0")
                        End If

                        Dim rE_CONNECT As TriphooRR097DataSet.E_CONNECTRow = dsItinerary.E_CONNECT.NewE_CONNECTRow
                        rE_CONNECT.SITE_CD = Me.S_CD.Value
                        rE_CONNECT.ORDER_ID = orderID
                        rE_CONNECT.SESSION_ID = SESSION_ID
                        rE_CONNECT.CARD_KIND = CARD_KIND
                        rE_CONNECT.ECON_CD_NO = CARD_NO
                        rE_CONNECT.CD_EXP_D = CARD_EXP_YEAR & CARD_EXP_MONTH
                        rE_CONNECT.CVV = CARD_SECURITY_CODE
                        rE_CONNECT.KANJI_NAME1 = CARD_HOLDER_SURNAME.ToUpper
                        rE_CONNECT.KANJI_NAME2 = CARD_HOLDER_NAME.ToUpper
                        rE_CONNECT._3D_SECURE_URL = SecureUrl
                        rE_CONNECT._3D_SECURE_FLG = SecureFlg
                        rE_CONNECT._3D_SECURE_AUTHKEY = PaReq
                        rE_CONNECT.TOKEN = TOKEN
                        rE_CONNECT.AUTH_KEY = AuthKey
                        dsItinerary.E_CONNECT.AddE_CONNECTRow(rE_CONNECT)

                        If SecureFlg Then
                            'Session.Add("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value, dsItinerary)
                            WebSessionUtil.SetSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)

                            Dim url As String = "../parts/secureAuthorize.aspx"
                            url += "?RT_CD=" & Me.RT_CD.Value
                            url += "&S_CD=" & Me.S_CD.Value
                            url += "&skey=" & Me.SESSION_NO.Value
                            If Not AF.Equals("") Then
                                url += "&AF=" & AF
                            End If
                            '接続先切替対応
                            Dim retUrl As String = Request.Url.AbsoluteUri
                            If ServiceVendor.IsNSSOL Then
                                If Not retUrl.Contains("localhost") Then
                                    retUrl = retUrl.Replace("http://", "https://")
                                End If
                                retUrl = Regex.Replace(retUrl, ":[0-9]+/", "/")
                            End If

                            url += "&URL=" & HttpUtility.UrlEncode(retUrl)
                            Response.Redirect(url, True)
                        End If

                    End If

                ElseIf Me.RES_CREDITRadioButton.Checked And Not Me.CreditDetailPanel.Visible Then

                    dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = "01"
                    dsItinerary.PAGE_03.Rows(0)("CREDIT_CERTIFIED_FIELD") = ""

                ElseIf Me.RES_CREDIT_REQUESTRadioButton.Checked Then

                    ' IACE以外は後払いは後払いとする 2024.9.13 Mitsuta
                    'Select Case Me.RT_CD.Value
                    '    Case "RT01", "A0500"
                    '        dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = "08" ' カード (後払)
                    '    Case Else
                    '        dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = "01"
                    'End Select

                    Select Case Me.RT_CD.Value
                        Case "A0027"
                            dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = "01"
                        Case Else
                            dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = "08" ' カード (後払)
                    End Select

                    dsItinerary.PAGE_03.Rows(0)("CREDIT_CERTIFIED_FIELD") = ""

                ElseIf Me.RES_BANKRadioButton.Checked Then

                    dsItinerary.PAGE_03.Rows(0)("SETTLE_KBN") = "02"
                    dsItinerary.PAGE_03.Rows(0)("CREDIT_CERTIFIED_FIELD") = ""

                End If

            End If

            '/***予約処理***/
            CallBook(dsItinerary)

        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "CONFIRMLinkButton_Click()", 2760, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#End Region

#Region "util"

    Private Sub createMsg(ByRef msg As String, str As String)
        If msg.Contains(str) Then
            Exit Sub
        End If
        msg += str & "\n"
    End Sub

#Region "CardDataCheck"
    Private Function CardDataCheck() As String

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "CardDataCheck()", 2780, "開始", HttpContext.Current.Session.SessionID)

        Dim ErrMsg As String = ""

        '背景色初期化
        Me.CARD_KIND.BackColor = Drawing.Color.White
        Me.CARD_NO.BackColor = Drawing.Color.White
        Me.CARD_EXP_YEAR.BackColor = Drawing.Color.White
        Me.CARD_EXP_MONTH.BackColor = Drawing.Color.White
        Me.CARD_HOLDER_SURNAME.BackColor = Drawing.Color.White
        Me.CARD_HOLDER_NAME.BackColor = Drawing.Color.White
        Me.CARD_SECURITY_CODE.BackColor = Drawing.Color.White

        If Me.CARD_KIND.SelectedValue.Equals("") Then
            createMsg(ErrMsg, LABEL_0052)
            Me.CARD_KIND.BackColor = Drawing.Color.Pink
        End If

        If Me.CARD_NO.Text.Equals("") Then
            createMsg(ErrMsg, LABEL_0053)
            Me.CARD_NO.BackColor = Drawing.Color.Pink
        Else
            '入力形式
            If Not checker.checkNumeric(LABEL_0054, CARD_NO.Text).Equals("") Then
                createMsg(ErrMsg, checker.checkNumeric(LABEL_0054, CARD_NO.Text))
                Me.CARD_NO.BackColor = Drawing.Color.Pink
            End If
        End If

        If Me.CARD_EXP_YEAR.SelectedValue.Equals("") Then
            createMsg(ErrMsg, LABEL_0055)
            Me.CARD_EXP_YEAR.BackColor = Drawing.Color.Pink
        End If

        If Me.CARD_EXP_MONTH.SelectedValue.Equals("") Then
            createMsg(ErrMsg, LABEL_0056)
            Me.CARD_EXP_MONTH.BackColor = Drawing.Color.Pink
        End If

        If Me.CARD_HOLDER_SURNAME.Text.Equals("") Then
            createMsg(ErrMsg, LABEL_0057)
            Me.CARD_HOLDER_SURNAME.BackColor = Drawing.Color.Pink
        End If

        If Me.CARD_HOLDER_NAME.Text.Equals("") Then
            createMsg(ErrMsg, LABEL_0058)
            Me.CARD_HOLDER_NAME.BackColor = Drawing.Color.Pink
        End If

        If Me.CARD_SECURITY_CODE.Text.Equals("") Then
            createMsg(ErrMsg, LABEL_0059)
            Me.CARD_SECURITY_CODE.BackColor = Drawing.Color.Pink
        Else
            '入力形式
            If Not checker.checkNumeric(LABEL_0060, CARD_SECURITY_CODE.Text).Equals("") Then
                createMsg(ErrMsg, checker.checkNumeric(LABEL_0060, CARD_SECURITY_CODE.Text))
                Me.CARD_SECURITY_CODE.BackColor = Drawing.Color.Pink
            End If
        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "CardDataCheck()", 2841, "終了", HttpContext.Current.Session.SessionID)

        Return ErrMsg
    End Function
#End Region

#Region "PayCredit"
    Private Function PayCredit(dsItinerary As TriphooRR097DataSet,
                               orderID As String,
                               sessionID As String,
                               fncCode As String) As Boolean

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayCredit()", 2854, "開始", HttpContext.Current.Session.SessionID)

        Dim okFlg As Boolean = False

        Dim CARD_KIND As String = Me.CARD_KIND.SelectedValue
        Dim CARD_HOLDER_SURNAME As String = Me.CARD_HOLDER_SURNAME.Text
        Dim CARD_HOLDER_NAME As String = Me.CARD_HOLDER_NAME.Text
        Dim CARD_NO As String = Me.CARD_NO.Text
        Dim CARD_SECURITY_CODE As String = Me.CARD_SECURITY_CODE.Text
        Dim CARD_EXP_YEAR As String = Me.CARD_EXP_YEAR.SelectedValue
        Dim CARD_EXP_MONTH As String = Me.CARD_EXP_MONTH.SelectedValue

        Dim ecnEntry As String = "1" ' 0:利用しない 1:利用する
        Dim Language As String = "0" ' 0:日本語 1:英語
        Dim econCardno As String = CARD_NO
        Dim cardExpdate As String = CARD_EXP_YEAR.PadLeft(2, "0") & CARD_EXP_MONTH.PadLeft(2, "0")
        Dim payCnt As String = "00"
        Dim cd3secFlg As String = "1" ' 0:利用なし 1:利用あり
        Dim CVV2 As String = CARD_SECURITY_CODE
        Dim telNo As String = ""
        Dim kanjName1_1 As String = CARD_HOLDER_SURNAME
        Dim kanjName1_2 As String = CARD_HOLDER_NAME
        Dim itemName As String = LABEL_0061
        Dim ordAmount As Integer = 0
        Dim ordAmountTax As Integer = 0
        Dim commission As Integer = 0

        If dsItinerary.M023_CLIENT_TEL.Rows.Count > 0 Then
            telNo = dsItinerary.M023_CLIENT_TEL.Rows(0)("NO").ToString.Replace("-", "")
        End If

        For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
            ordAmount += row("SALES_SUB_TOTAL")
        Next

        okFlg = E_Connect.order(
        dsItinerary,
        RT_CD.Value,
        S_CD.Value,
        CARD_KIND,
        fncCode,
        orderID,
        sessionID,
        econCardno,
        cardExpdate,
        payCnt,
        cd3secFlg,
        CVV2,
        telNo,
        kanjName1_1,
        kanjName1_2,
        itemName,
        ordAmount,
        ordAmountTax,
        commission,
        "",
        "",
        "",
        Response,
        "",
        "",
        "")

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayCredit()", 2918, "終了", HttpContext.Current.Session.SessionID)

        Return okFlg
    End Function
#End Region

#Region "イーコンテクスト 売上計上 [トークン有]"
    Private Function PayEconToken(ByVal dsItinerary As TriphooRR097DataSet, TOKEN As String) As Boolean

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayEconToken()", 2928, "開始", HttpContext.Current.Session.SessionID)

        Dim okFlg As Boolean = False
        Dim ordAmount As Integer = 0

        Dim CARD_KIND As String = Me.CARD_KIND.SelectedValue
        For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
            ordAmount += row("SALES_SUB_TOTAL")
        Next

        okFlg = E_Connect.goPayment(RT_CD.Value, Me.S_CD.Value, CARD_KIND, TOKEN, ordAmount)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayEconToken()", 2941, "終了", HttpContext.Current.Session.SessionID)

        Return okFlg
    End Function
#End Region

#Region "GMO/SMBCGMO 売上計上[トークン有]"
    Private Function PayGmo(ByRef dsItinerary As TriphooRR097DataSet,
                            ByVal dsUser As DataSet,
                            ByVal orderID As String,
                            ByVal USER_ID As String,
                            ByRef SecureUrl As String,
                            ByRef SessionId As String,
                            ByRef pareq As String,
                            ByRef SecureFlg As Boolean,
                            ByRef AuthKey As String) As Boolean

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayGmo()", 2959, "開始", HttpContext.Current.Session.SessionID)

        Dim okFlg As Boolean = False

        Dim CARD_KIND As String = Me.CARD_KIND.SelectedValue
        Dim CARD_HOLDER_SURNAME As String = Me.CARD_HOLDER_SURNAME.Text
        Dim CARD_HOLDER_NAME As String = Me.CARD_HOLDER_NAME.Text
        Dim CARD_NO As String = Me.CARD_NO.Text
        Dim CARD_SECURITY_CODE As String = Me.CARD_SECURITY_CODE.Text
        Dim CD_EXP_D As String = Me.CARD_EXP_YEAR.SelectedValue & Me.CARD_EXP_MONTH.SelectedValue.PadLeft(2, "0")
        Dim TOKEN As String = Me.token.Value

        Dim JobCd As String = "AUTH" ' CHECK：有効性チェック CAPTURE：即時売上 AUTH：仮売上 SAUTH：簡易オーソリ
        Dim ecnEntry As String = "1" ' 0:利用しない 1:利用する
        Dim Language As String = "0" ' 0:日本語 1:英語
        Dim econCardno As String = ""
        Dim cardExpdate As String = "" '1901
        Dim payCnt As String = "00"
        Dim cd3secFlg As String = "1" ' 0:利用なし 1:利用あり
        Dim CVV2 As String = ""
        Dim telNo As String = ""
        Dim kanjName1_1 As String = ""
        Dim kanjName1_2 As String = ""
        Dim itemName As String = "旅行代金"
        Dim ordAmount As Integer = 0
        Dim ordAmountTax As Integer = 0
        Dim commission As Integer = 0
        Dim RetUrl As String = Request.Url.AbsoluteUri

        '接続先切替対応
        If Not RetUrl.Contains("localhost") Then
            RetUrl = RetUrl.Replace("http://", "https://")
            If ServiceVendor.IsNSSOL Then
                RetUrl = Regex.Replace(RetUrl, ":[0-9]+/", "/")
            End If
        End If

        If Not RetUrl.Contains("auth=secured") Then
            RetUrl += "&auth=secured"
        End If
        RetUrl = HttpUtility.UrlEncode(RetUrl)

        ' トークン利用しない場合は、各パラメータを設定
        If TOKEN.Equals("") Then
            econCardno = CARD_NO
            cardExpdate = CD_EXP_D.Substring(2, 4)
            CVV2 = CARD_SECURITY_CODE
            kanjName1_1 = CARD_HOLDER_SURNAME
            kanjName1_2 = CARD_HOLDER_NAME
        End If

        If dsItinerary.M023_CLIENT_TEL.Rows.Count > 0 Then
            telNo = dsItinerary.M023_CLIENT_TEL.Rows(0)("NO").ToString.Replace("-", "")
        End If

        For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
            ordAmount += row("SALES_SUB_TOTAL")
        Next

        Dim GmoUtil As New GmoUtil(USER_ID, DEMO_FLG)
        okFlg = GmoUtil.Order(dsItinerary, dsUser, Me.RT_CD.Value, Me.S_CD.Value, orderID, itemName, ordAmount,
                                   econCardno, cardExpdate, CVV2, CARD_KIND, TOKEN, "/", Request.UserAgent, SecureUrl,
                                   SessionId, pareq, SecureFlg, RetUrl, JobCd)

        AuthKey = SessionId & "," & pareq & "," & ordAmount

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayGmo()", 3018, "終了", HttpContext.Current.Session.SessionID)

        Return okFlg
    End Function
#End Region

#Region "PayCreditSmbcStation"
    Private Function PayCreditSmbcStation(dsItinerary As TriphooRR097DataSet,
                               orderID As String,
                               token As String,
                               ByRef SecureUrl As String,
                               ByRef SessionId As String,
                               ByRef pareq As String,
                               ByRef SecureFlg As Boolean) As Boolean

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayCreditSmbcStation()", 3034, "開始", HttpContext.Current.Session.SessionID)

        Dim okFlg As Boolean = False

        Dim clientName As String = ""
        Dim itemName As String = LABEL_0061
        Dim ordAmount As Integer = 0
        Dim CARD_KIND As String = Me.CARD_KIND.SelectedValue

        Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
        Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
        Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
        Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")

        ' AIR, HOTEL, OPTIONAL, TOUR
        If TICKET_FLG Then

            If dsItinerary.PAGE_05.Rows.Count > 0 Then
                itemName = dsItinerary.PAGE_05.Rows(0)("GOODS_NM")
            End If

        ElseIf HOTEL_FLG Then

            If dsItinerary.RES_HOTEL.Rows.Count > 0 Then
                itemName = dsItinerary.RES_HOTEL.Rows(0)("GOODS_NM")
            End If

        ElseIf OPTION_FLG Then

            If dsItinerary.RES_OPTION.Rows.Count > 0 Then
                itemName = dsItinerary.RES_OPTION.Rows(0)("GOODS_NM")
            End If

        ElseIf PACKAGE_FLG Then

            If dsItinerary.PAGE_20.Rows.Count > 0 Then
                itemName = dsItinerary.PAGE_20.Rows(0)("GOODS_NM")
            End If
        End If

        If dsItinerary.M019_CLIENT.Rows.Count > 0 Then
            clientName = dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI") & dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI")
        End If

        If clientName.Equals("") Then
            clientName = dsItinerary.M019_CLIENT.Rows(0)("SURNAME_ROMAN") & dsItinerary.M019_CLIENT.Rows(0)("NAME_ROMAN")
            clientName = StrConv(clientName, VbStrConv.Wide)
        End If


        For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
            ordAmount += row("SALES_SUB_TOTAL")
        Next

        'itemName = "ツアー代金"

        Dim SmbcStationUtil As New SmbcStationUtil(Me.RT_CD.Value, Me.S_CD.Value)

        okFlg = SmbcStationUtil.order(
        RT_CD.Value,
        CARD_KIND,
        orderID,
        clientName,
        itemName,
        token,
        ordAmount,
        SecureUrl,
        SessionId,
        pareq,
        SecureFlg,
        S_CD.Value)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayCreditSmbcStation()", 3107, "終了", HttpContext.Current.Session.SessionID)

        Return okFlg
    End Function
#End Region

#Region "PayCreditVeriTrans"
    Private Function PayCreditVeriTrans(ByVal dsItinerary As TriphooRR097DataSet,
                               ByVal orderID As String,
                               ByVal token As String,
                               ByRef SecureUrl As String,
                               ByRef SecureFlg As String,
                                ServiceOptionType As String) As Boolean

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayCreditVeriTrans()", 3122, "開始", HttpContext.Current.Session.SessionID)

        Dim okFlg As Boolean = False

        Dim ordAmount As Integer = 0
        Dim CARD_KIND As String = Me.CARD_KIND.SelectedValue
        Dim HttpAccept() As String = Request.ServerVariables.GetValues("HTTP_ACCEPT")
        Dim RedirectionUrl As String = Request.Url.AbsoluteUri
        Dim WithCapture As String = "false"

        If Not RedirectionUrl.Contains("auth=secured") Then
            RedirectionUrl += "&auth=secured"
        End If

        For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
            ordAmount += row("SALES_SUB_TOTAL")
        Next

        Dim VeriTrans4GUtil As New VeriTrans4GUtil(Me.RT_CD.Value, Me.S_CD.Value)

        okFlg = VeriTrans4GUtil.Order(
            Me.RT_CD.Value, Me.S_CD.Value, orderID, CARD_KIND, HttpAccept(0).ToString(), ordAmount, SecureUrl,
            RedirectionUrl, token, SecureFlg, WithCapture, ServiceOptionType, dsItinerary)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayCreditVeriTrans()", 3147, "終了", HttpContext.Current.Session.SessionID)

        Return okFlg
    End Function
#End Region

#Region "PayConvenience"
    Private Function PayConvenience(dsItinerary As TriphooRR097DataSet,
                               orderID As String,
                               sessionID As String,
                               fncCode As String,
                               ByRef url As String) As Boolean

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayConvenience()", 3161, "開始", HttpContext.Current.Session.SessionID)

        Dim okFlg As Boolean = False
        Dim CLIENT_E_MAIL As String = dsItinerary.M019_CLIENT.Rows(0)("E_MAIL")
        Dim SURNAME_KANJI As String = dsItinerary.M019_CLIENT.Rows(0)("SURNAME_KANJI")
        Dim NAME_KANJI As String = dsItinerary.M019_CLIENT.Rows(0)("NAME_KANJI")
        Dim ecnEntry As String = "1" ' 0:利用しない 1:利用する
        Dim Language As String = "0" ' 0:日本語 1:英語
        Dim payCnt As String = "00"
        Dim cd3secFlg As String = "1" ' 0:利用なし 1:利用あり
        Dim telNo As String = ""
        Dim kanjName1_1 As String = SURNAME_KANJI
        Dim kanjName1_2 As String = NAME_KANJI
        Dim itemName As String = LABEL_0061
        Dim ordAmount As Integer = 0
        Dim ordAmountTax As Integer = 0
        Dim commission As Integer = 0
        Dim payLimitDay As String = CDate(dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE")).ToString("yyyy/MM/dd")

        If dsItinerary.M023_CLIENT_TEL.Rows.Count > 0 Then
            telNo = dsItinerary.M023_CLIENT_TEL.Rows(0)("NO").ToString.Replace("-", "")
        End If

        For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
            ordAmount += row("SALES_SUB_TOTAL")
        Next

        okFlg = E_Connect.order(
        dsItinerary,
        RT_CD.Value,
        S_CD.Value,
        "10",
        fncCode,
        orderID,
        sessionID,
        "",
        "",
        payCnt,
        cd3secFlg,
        "",
        telNo,
        kanjName1_1,
        kanjName1_2,
        itemName,
        ordAmount,
        ordAmountTax,
        commission,
        "",
        "",
        "",
        Response,
        CLIENT_E_MAIL,
        payLimitDay,
        url)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "PayConvenience()", 3217, "終了", HttpContext.Current.Session.SessionID)

        Return okFlg

    End Function
#End Region

#Region "CallBook"
    Private Sub CallBook(dsItinerary As TriphooRR097DataSet)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "CallBook()", 3228, "開始", HttpContext.Current.Session.SessionID)

        Dim domain As String = Request.Url.Host

        Dim _car_cid As String = SetRRValue.setNothingValue(Session("_car-cid" & Me.RT_CD.Value & Me.S_CD.Value))
        Dim _car_af As String = SetRRValue.setNothingValue(Session("_car-af" & Me.RT_CD.Value & Me.S_CD.Value))

        Try
            Dim CookieItem As String = ""
            Select Case Me.RT_CD.Value
                Case "RT40" 'VictoryTour
                    CookieItem = "_car-af-41354"
                Case "A0059" 'Skycrew
                    CookieItem = "_car-af-41952"
                Case "RT14" 'TravelStory
                    CookieItem = "_car-af-41923"
            End Select

            If Not CookieItem.Equals("") Then
                If Not Request.Cookies(CookieItem) Is Nothing Then
                    _car_cid = CookieItem.Replace("_car-af-", "")
                    _car_af = Request.Cookies(CookieItem).Value
                End If
            End If

        Catch ex As Exception

        End Try

        Try
            dsItinerary.PAGE_03.Rows(0)("RES_DEVICE_KBN") = Me.RES_DEVICE_KBN.Value
        Catch ex As Exception
        End Try

        ' ログ生成 NSSOL負荷性能検証 2023/02/13
        'logger.CreateLog("page_cart_cart003", "CallBook()", 3228, "CartUtil.book前", HttpContext.Current.Session.SessionID)

        Dim okFlg As Boolean = CartUtil.book(dsItinerary, Me.RT_CD.Value, Me.S_CD.Value, Request, lang, True, Me.SESSION_NO.Value)

        ' ログ生成 NSSOL負荷性能検証 2023/02/13
        'logger.CreateLog("page_cart_cart003", "CallBook()", 3228, "CartUtil.book後", HttpContext.Current.Session.SessionID)

        '●遷移先 設定
        If okFlg Then

            Dim TICKET_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("TICKET_FLG")
            Dim HOTEL_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("HOTEL_FLG")
            Dim OPTION_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("OPTION_FLG")
            Dim PACKAGE_FLG As Boolean = dsItinerary.PAGE_03.Rows(0)("PACKAGE_FLG")

            Dim isGoCart004 As Boolean = True

            If Me.RT_CD.Value.Equals("KIC") And Me.S_CD.Value.Equals("02") Then
                If TICKET_FLG And Not HOTEL_FLG And Not OPTION_FLG And Not PACKAGE_FLG Then
                    'KICかつAOの場合MMX書込,メール送信はしない
                    isGoCart004 = False
                End If
            End If

            If isGoCart004 Then
                ' オンライン
                Dim url As String = "./cart005.aspx"
                url += "?RT_CD=" & Me.RT_CD.Value
                url += "&S_CD=" & Me.S_CD.Value
                url += "&skey=" & Me.SESSION_NO.Value
                If Not _car_af.Equals("") And Not _car_cid.Equals("") Then
                    url += "&_car-cid=" & _car_cid
                    url += "&_car-af=" & _car_af
                End If
                If Not AF.Equals("") Then
                    url += "&AF=" & AF
                End If

                Response.Redirect(url, True)
            Else
                Dim s_CRS_Ref As String = dsItinerary.PAGE_05.Rows(0)("SUP_GDS_LOCATOR") ' PNR
                Dim s_AGT_Mail As String = dsItinerary.M019_CLIENT.Rows(0)("E_MAIL") 'メールアドレス
                Dim s_AGT_Ref As String = dsItinerary.PAGE_03.Rows(0)("PORTAL_RES_NO") '御社予約番号
                Dim s_AGT_Tanto As String = dsItinerary.PAGE_03.Rows(0)("RES_CONFIRMATION_TO") '予約担当者

                '<ＵＲＬ>
                'http : //www.kronos.co.jp/webtkt/sso.php

                '<POSTパラメータ>
                's_Web_ID　エージェントＩＤ
                's_Web_PWエージェントパスワード 
                's_CRS_Ref リファレンス
                's_AGT_Tanto担当者
                's_AGT_Mail完了メール送信先アドレス 
                's_AGT_Ref代理店管理番号（完了メールに記載予定）

                'Session クリア
                'Session.Remove("Itinerary" & Me.RT_CD.Value & Me.S_CD.Value)
                WebSessionUtil.RemoveSessionDb(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "Itinerary", dsItinerary, dsB2CUser)
                Session.Remove("dsAir" & Me.RT_CD.Value & Me.S_CD.Value)
                Session.Remove("dsHote" & Me.RT_CD.Value & Me.S_CD.Value)
                'Session.Remove("dsTour" & Me.RT_CD.Value & Me.S_CD.Value)
                WebSessionUtil.RemoveSession(Me.RT_CD.Value, Me.S_CD.Value, Me.SESSION_NO.Value, "dsTour")
                Session.Remove("dsOption" & Me.RT_CD.Value & Me.S_CD.Value)
                Session.Remove("MEDIA" & Me.RT_CD.Value & Me.S_CD.Value)

                Dim url As String = "../parts/minixForm.aspx"
                url += "?RT_CD=" & Me.RT_CD.Value
                url += "&S_CD=" & Me.S_CD.Value
                url += "&s_CRS_Ref=" & s_CRS_Ref
                url += "&s_AGT_Tanto=" & s_AGT_Tanto
                url += "&s_AGT_Mail=" & s_AGT_Mail
                url += "&s_AGT_Ref=" & s_AGT_Ref
                Response.Redirect(url, True)
            End If


        Else

            ' オフライン
            Dim url As String = "./cart004.aspx"
            url += "?RT_CD=" & Me.RT_CD.Value
            url += "&S_CD=" & Me.S_CD.Value
            url += "&skey=" & Me.SESSION_NO.Value
            If Not _car_af.Equals("") And Not _car_cid.Equals("") Then
                url += "&_car-cid=" & _car_cid
                url += "&_car-af=" & _car_af
            End If
            If Not AF.Equals("") Then
                url += "&AF=" & AF
            End If

            ' ログ生成 NSSOL負荷性能検証 2023/02/09
            'logger.CreateLog("page_cart_cart003", "CallBook()", 3343, "終了", HttpContext.Current.Session.SessionID)

            Response.Redirect(url, True)

        End If

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "CallBook()", 3350, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

#Region "CalcReportMoney"
    Private Sub CalcReportMoney(ByRef dsItinerary As TriphooRR097DataSet)
        Try
            Dim DEPOSIT_RATE_PRICE As Integer = SetRRValue.setNumeric(dsItinerary.PAGE_20.Rows(0)("DEPOSIT_RATE_PRICE"))         '申込金・率・金額
            Dim DEPOSIT_RATE_PRICE_KBN As String = SetRRValue.setNothingValue(dsItinerary.PAGE_20.Rows(0)("DEPOSIT_RATE_PRICE_KBN")) '申込金・率・金額・区分

            Dim RECEIPT_LIMIT As Integer = SetRRValue.setNumeric(dsItinerary.PAGE_20.Rows(0)("RECEIPT_LIMIT")) '入金・期限
            Dim RECEIPT_LIMIT_KBN As String = SetRRValue.setNothingValue(dsItinerary.PAGE_20.Rows(0)("RECEIPT_LIMIT_KBN")) '入金・期限・区分

            Dim DEPOSIT_LIMIT As Integer = SetRRValue.setNumeric(dsItinerary.PAGE_20.Rows(0)("DEPOSIT_LIMIT")) '申込金・期限
            Dim DEPOSIT_LIMIT_KBN As String = SetRRValue.setNothingValue(dsItinerary.PAGE_20.Rows(0)("DEPOSIT_LIMIT_KBN")) '申込金・期限・区分

            Dim DEP_TIME As DateTime = dsItinerary.PAGE_03.Rows(0)("DEP_TIME")

            '申込金率
            If Not DEPOSIT_RATE_PRICE_KBN.Equals("") Then

                Dim ordAmount As Integer = 0
                For Each row As DataRow In dsItinerary.RES_ORDER_DATA.Select("COMMIT_FLG = 1")
                    ordAmount += row("SALES_SUB_TOTAL")
                Next

                Dim REPORT_MONEY As Integer = PriceAdjust(ordAmount, DEPOSIT_RATE_PRICE_KBN, DEPOSIT_RATE_PRICE)
                dsItinerary.PAGE_03.Rows(0)("REPORT_MONEY") = REPORT_MONEY
            End If

            '入金期限
            If Not RECEIPT_LIMIT_KBN.Equals("") Then

                Dim RECEIPT_LIMIT_DATE As DateTime = "1900/01/01"


                Select Case RECEIPT_LIMIT_KBN
                    Case "1" ' 1:予約後
                        RECEIPT_LIMIT_DATE = Today.AddDays(RECEIPT_LIMIT)
                    Case "2" ' 2:出発前
                        RECEIPT_LIMIT_DATE = DEP_TIME.AddDays(-RECEIPT_LIMIT)
                End Select

                '入金期限が厳しい方をセット
                Dim _RECEIPT_LIMIT_DATE As DateTime = SetRRValue.setDate(dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE"))

                If 1900 < RECEIPT_LIMIT_DATE.Year AndAlso 1900 < _RECEIPT_LIMIT_DATE.Year AndAlso _RECEIPT_LIMIT_DATE < RECEIPT_LIMIT_DATE Then
                    RECEIPT_LIMIT_DATE = _RECEIPT_LIMIT_DATE
                End If

                If 1900 < RECEIPT_LIMIT_DATE.Year AndAlso RECEIPT_LIMIT_DATE < Today Then
                    RECEIPT_LIMIT_DATE = Today
                End If

                dsItinerary.PAGE_03.Rows(0)("RECEIPT_LIMIT_DATE") = RECEIPT_LIMIT_DATE.ToString("yyyy/MM/dd") & " 15:00"
            End If

            '申込金期限
            If Not DEPOSIT_LIMIT_KBN.Equals("") Then

                Dim TEMP_RECEIPT_LIMIT_DATE As DateTime = "1900/01/01"

                Select Case DEPOSIT_LIMIT_KBN
                    Case "1" ' 1:予約後
                        TEMP_RECEIPT_LIMIT_DATE = Today.AddDays(DEPOSIT_LIMIT)
                    Case "2" ' 2:出発前
                        TEMP_RECEIPT_LIMIT_DATE = DEP_TIME.AddDays(-DEPOSIT_LIMIT)
                End Select

                '入金期限が過去の時 22/12/08 照井
                If 1900 < TEMP_RECEIPT_LIMIT_DATE.Year AndAlso TEMP_RECEIPT_LIMIT_DATE < Today Then
                    TEMP_RECEIPT_LIMIT_DATE = Today
                End If

                dsItinerary.PAGE_03.Rows(0)("TEMP_RECEIPT_LIMIT_DATE") = TEMP_RECEIPT_LIMIT_DATE.ToString("yyyy/MM/dd") & " 15:00"
            End If


        Catch ex As Exception
        End Try
    End Sub
#End Region

    Public Function PriceAdjust(PRICE As Decimal, PRICE_KBN As String, PROFIT_PRICE As Decimal) As Decimal

        Dim RETURN_PRICE As Integer = 0

        Select Case PRICE_KBN ' 01:利率 02:金額 03:固定
            Case "01" : RETURN_PRICE = PRICE * (PROFIT_PRICE / 100)
            Case "02" : RETURN_PRICE = PRICE + PROFIT_PRICE
            Case "03" : RETURN_PRICE = PROFIT_PRICE
        End Select

        Return RETURN_PRICE
    End Function

#End Region

#Region "言語対応"
    Private Sub setlang(lang As String)

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "setlang()", 3430, "開始", HttpContext.Current.Session.SessionID)

        Try
            Dim xmlPath As String = HttpContext.Current.Server.MapPath("../../")
            Dim dsXml As New DataSet
            dsXml.ReadXml(xmlPath & "/lang/cart/" & lang & "/cart003.xml")
            Dim rXml As DataRow = dsXml.Tables("LABEL").Rows(0)

            Me.LABEL_0001.Text = rXml("LABEL_0001")
            Me.LABEL_0002.Text = rXml("LABEL_0002")
            Me.LABEL_0003.Text = rXml("LABEL_0003")
            Me.LABEL_0004.Text = rXml("LABEL_0004")
            Me.LABEL_0005.Text = rXml("LABEL_0005")
            Me.LABEL_0006.Text = rXml("LABEL_0006")
            Me.LABEL_0007.Text = rXml("LABEL_0007")
            Me.LABEL_0008.Text = rXml("LABEL_0008")
            Me.LABEL_0009.Text = rXml("LABEL_0009")
            Me.LABEL_0010.Text = rXml("LABEL_0010")
            Me.LABEL_0011.Text = rXml("LABEL_0011")
            Me.LABEL_0012.Text = rXml("LABEL_0012")
            Me.LABEL_0013.Text = rXml("LABEL_0013")
            Me.CARD_HOLDER_SURNAME.Attributes("placeholder") = rXml("CARD_HOLDER_SURNAME_PLACEHOLDER")
            Me.LABEL_0014.Text = rXml("LABEL_0014")
            Me.LABEL_0015.Text = rXml("LABEL_0015")
            Me.CARD_HOLDER_NAME.Attributes("placeholder") = rXml("CARD_HOLDER_NAME_PLACEHOLDER")
            Me.LABEL_0016.Text = rXml("LABEL_0016")
            Me.LABEL_0017.Text = rXml("LABEL_0017")
            Me.LABEL_0018.Text = rXml("LABEL_0018")
            Me.LABEL_0019.Text = rXml("LABEL_0019")
            Me.LABEL_0020.Text = rXml("LABEL_0020")
            Me.LABEL_0021.Text = rXml("LABEL_0021")
            Me.LABEL_0022.Text = rXml("LABEL_0022")
            Me.LABEL_0023.Text = rXml("LABEL_0023")
            Me.LABEL_0024.Text = rXml("LABEL_0024")
            'Me.AgreeCheckBox.Text = rXml("AgreeCheckBox")
            Me.LABEL_0103.Text = rXml("AgreeCheckBox")
            Me.LABEL_0025.Text = rXml("LABEL_0025")
            Me.BackLinkButton.Text = rXml("BackLinkButton")
            Me.RES_COMMITLinkButton.Text = rXml("RES_COMMITLinkButton")
            Me.commit.InnerText = rXml("RES_COMMITLinkButton")
            Me.LABEL_0039 = rXml("LABEL_0039_COM")
            Me.LABEL_0040 = rXml("LABEL_0040_COM")
            Me.LABEL_0041 = rXml("LABEL_0041_COM")
            Me.LABEL_0042 = rXml("LABEL_0042_COM")
            Me.LABEL_0043 = rXml("LABEL_0043_COM")
            Me.LABEL_0044 = rXml("LABEL_0044_COM")
            Me.LABEL_0045 = rXml("LABEL_0045_COM")
            Me.LABEL_0046 = rXml("LABEL_0046_COM")
            Me.LABEL_0047 = rXml("LABEL_0047_COM")
            Me.LABEL_0048 = rXml("LABEL_0048_COM")
            Me.LABEL_0049 = rXml("LABEL_0049_COM")
            Me.LABEL_0050 = rXml("LABEL_0050_COM")
            Me.LABEL_0051 = rXml("LABEL_0051_COM")
            Me.LABEL_0052 = rXml("LABEL_0052_COM")
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
            Me.LABEL_0067 = rXml("LABEL_0067_COM")
            Me.LABEL_0066.Text = rXml("LABEL_0066")
            Me.LABEL_0069.Text = rXml("LABEL_0069")
            Me.LABEL_0070.Text = rXml("LABEL_0070")
            Me.LABEL_0071.Text = rXml("LABEL_0071")
            Me.LABEL_0072.Text = rXml("LABEL_0072")
            Me.LABEL_0073.Text = rXml("LABEL_0073")
            Me.LABEL_0074.Text = SetRRValue.SetRowValue(rXml, "LABEL_0074")
            Me.LABEL_0077 = rXml("LABEL_0077")
            Me.LABEL_0078 = rXml("LABEL_0078")
            Me.LABEL_0081.Text = rXml("LABEL_0081")
            Me.AgreeImportantCheckBox.Text = rXml("LABEL_0082")
            Me.LABEL_0083 = rXml("LABEL_0083")
            Me.LABEL_0084.Text = rXml("LABEL_0084")
            Me.LABEL_0085.Text = rXml("LABEL_0085")
            Me.LABEL_0086 = rXml("LABEL_0086")
            Me.LABEL_0087 = rXml("LABEL_0087")
            Me.LABEL_0089.Text = rXml("LABEL_0089")
            Me.LABEL_0090.Text = rXml("LABEL_0090")
            Me.LABEL_0091 = rXml("LABEL_0091")
            Me.LABEL_0023_TK.Text = rXml("LABEL_0023")
            Me.LABEL_0066_TK.Text = rXml("LABEL_0079")
            Me.LABEL_0023_HO.Text = rXml("LABEL_0023")
            Me.LABEL_0066_HO.Text = rXml("LABEL_0079")
            Me.LABEL_0023_DP.Text = rXml("LABEL_0023")
            Me.LABEL_0066_DP.Text = rXml("LABEL_0079")
            Me.LABEL_0023_OP.Text = rXml("LABEL_0023")
            Me.LABEL_0066_OP.Text = rXml("LABEL_0079")
            Me.LABEL_0095 = rXml("LABEL_0095")
            Me.LABEL_0096 = rXml("LABEL_0096")
            Me.LABEL_0097.Text = rXml("LABEL_0097")
            Me.LABEL_0098.Text = rXml("LABEL_0098")
            Me.LABEL_0099.Text = rXml("LABEL_0099")
            Me.LABEL_0100.Text = rXml("LABEL_0100")
            LABEL_0101 = rXml("LABEL_0101")
            LABEL_0102 = rXml("LABEL_0102")

            If Me.RES_BANKRadioButton.Text.Equals("") Then
                Me.RES_BANKRadioButton.Text = rXml("RES_BANKRadioButton")
            End If
            If Me.RES_CREDITRadioButton.Text.Equals("") Then
                Me.RES_CREDITRadioButton.Text = rXml("RES_CREDITRadioButton")
            End If
            If Me.RES_CREDIT_REQUESTRadioButton.Text.Equals("") Then
                Me.RES_CREDIT_REQUESTRadioButton.Text = rXml("RES_CREDIT_REQUESTRadioButton")
            End If
            If LABEL_0093.Equals("") Then
                Me.LABEL_0093.Text = rXml("LABEL_0093")
            End If

            Dim PAYMENT_PAGE_KBN As String = dsRTUser.M137_RT_SITE_CD.Rows(0)("PAYMENT_PAGE_KBN")
            If PAYMENT_PAGE_KBN.Equals("2") Then
                Me.LABEL_0073.Text = rXml("LABEL_0080")
            End If

            If Not lang.Equals("1") Then
                Me.Agree1Panel.Visible = False
                Me.Agree2Panel.Visible = False
            End If

            If Me.RT_CD.Value.Equals("ATR") Then
                Me.LABEL_0023.Text = "取引条件説明書面（旅行条件書）は、画面上の表示（PDF）をもって交付させていただきます。<br/>ご旅行にお申込みいただく前に、取引条件説明書面（旅行条件書）及びTASCITOURS利用規約、チャージ規定、各コースのホームページに掲載されている内容を必ずお読みください。<br/>またお申し込みの際には「旅行条件書」を確認の上、必ず印刷又は保存をしてください。"
                Me.LABEL_0120.Text = "<br/>「申し込みを確定する」ボタンを押してお申込みいただくことで、お客様は取引条件説明書面（旅行条件書）及びTASCITOURS利用規約、チャージ規定に同意の上、お申込みをされたことになります。"
            End If

        Catch ex As Exception
        End Try

        ' ログ生成 NSSOL負荷性能検証 2023/02/09
        'logger.CreateLog("page_cart_cart003", "setlang()", 3564, "終了", HttpContext.Current.Session.SessionID)

    End Sub
#End Region

End Class
