//f
// 2006/01/31 created by wenliming
function execute(command)
{
	executeDefaultCommand(command);
}

function executePub(command)
{
	switch (command)
	{
		case "INSERT_PAPER": // 新規原竵E
			showModalDlg("Sa0401.htm", null, "880", "640");		
			break;
		case "UPDATE_PAPER": // update原竵E
			showModalDlg("Sa0405.htm", null, "880", "640");		
			break;
		default:
			break;
	}
}

function executeDefaultCommand(command)
{
	switch (command)
	{
		case "BTN001": // 新襾E
		
			break;
		case "BTN002": // 変竵E
		
			break;
		case "BTN003": // 保磥E
		
			break;
		
		case "BTN004": // 削除
		
			break;		
		case "BTN005": // 検藖E
		
			break;		
		case "BTN006": // 登録
		
			break;		
		case "BTN007": // Cancel
		
			break;		
		case "BTN009": // 閉じE
			window.close();
			break;
		case "BTN010": // 戻E
		
			break;		
		case "BTN011": // 竵E?
		
			break;		
		case "BTN012": // 次へ
		
			break;		
		case "BTN013": // 前へ
		
			break;		
		case "BTN015": // 中止
		
			break;		
		case "BTN016":
		
			break;		
		case "BTN017":
		
			break;		
		case "BTN019":
		
			break;		
		case "BTN020":
		
			break;		
		case "BTN021":
		
			break;		
		case "BTN022": // 料金設定
			showModalDlg("../Sa/Sa0202.htm", null, "978", "732");
			break;		
		case "BTN023":
		
			break;		
		case "BTN024":
		
			break;		
		case "BTN025": // 蓙Ez詳紒E
			showModalDlg("../Sa/Sa0301.htm", null, "1005", "720");
			break;		
		case "BTN026":
		
			break;		
		case "BTN027":
		
			break;		
		case "BTN028": // 耕远状況
			showModalDlg("../Sa/Sa0203.htm", null, "1000", "700");
			break;		
		case "BTN029": // 広告謥E
			showDialog('ad_nsi');
			break;		
		case "BTN030": // 広告会蓙E
			showDialog('adc');
			break;		
		case "BTN031": // 企画
			showDialog('kik');
			break;		
		case "BTN032": // 
			showDialog('calendar');
			break;		
		case "BTN033": // 希望脕E
			showDialog('kbo_jkn');
			break;		
		case "BTN034":
		
			break;		
		case "BTN035":
			selectAll();
			break;		
		case "BTN036": // 月極契約
			showDialog('kek');
			break;		
		case "BTN037": // 丒業担当
			showDialog('user');
			break;		
		case "BTN038": // 月極継続
			showModalDlg("../Sa/Sa0403.htm", null, "1000", "700");
			break;		
		case "BTN039":
		
			break;		
		case "BTN040": //紙面柄婢
			//showModalDlg("Sa0204.htm", null, "800", "600");
			OpenWindow("Sa0204.htm", null, "1000", "700", "0", "0", false);
			break;		
		case "BTN041":
		
			break;		
		case "BTN042": // 詳細情垇E
			showModalDlg("../Sa/Sa0101.htm", null, "1000", "700");
			break;	
		case "BTN043":
		
			break;		
		case "BTN044": // 担当部蕘E
			showDialog('bus');
			break;		
		case "BTN045": // 変竵E臍s
			showModalDlg("../Sa/Sa0302.htm", null, "800", "600");
			break;		
		case "BTN046": // 手数料率
			showDialog('bmg');
			break;		
		case "BTN047": // 蓙Ez詳紒E
			showModalDlg("../Sa/Sa0301.htm", null, "1000", "700");
			break;		
		case "BTN048": // 付加料手数料率
			showDialog('bmg');
			break;		
		case "BTN049":
		
			break;		
		case "BTN050":
		
			break;		
		case "BTN051":
			showModalDlg("../Sa/Sa0404.htm", null, "1000", "700");
			window.close();
			break;		
		case "BTN052": // 受付日時
			showDialog('calendar');
			break;		
		case "BTN053":
		
			break;		
		case "BTN054": // 希望脕E
			showDialog('kbo_pap');
			break;		
		case "BTN055": // 不可历锷
			showDialog('no_reason');
			break;		
		case "BTN056":
		
			break;		
		case "BTN057":
		
			break;		
		case "BTN058":
			showModalDlg("../Sc/Sc0402.htm", null, "1000", "700");
			break;		
		case "BTN059": // 掲載日
			showDialog('calendar');
			break;		
		case "BTN060":
			showModalDlg("../Sc/Sc0102.htm", null, "980", "690");
			break;		
		case "BTN061":
		
			break;		
		case "BTN062":
		    showModalDlg("../Sa/Sc0103.htm", null, "1000", "700");
			break;		
		case "BTN063":
		
			break;		
		case "BTN064":
		
			break;		
		case "BTN065":
		
			break;		
		case "BTN066": // 売上確定日選択
			showDialog('calendar');
			break;		
		case "BTN067":
			showModalDlg("../Sc/Sc0201.htm", null, "1000", "700");
			break;	
		case "BTN068":		   
			break;		
		case "BTN069":
		
			break;		
		case "BTN070":
			showModalDlg("../Sc/Sc0302.htm", null, "1000", "700");
			break;		
		case "BTN071":
		
			break;		
		case "BTN072": // 売上確定日
			showDialog('calendar');
			break;		
		case "BTN073":
		
			break;		
		case "BTN074":
		
			break;		
		case "BNT075":
		
			break;		
		case "BTN076":
			showModalDlg("../Sc/Sc0502.htm", null, "1000", "700");
			break;		
		case "BTN077":
			showModalDlg("../Sc/Sc0602.htm", null, "1000", "700");
			break;		
		case "BTN078":
		
			break;		
		case "BTN079": // 売上確定日セット
			showDialog('calendar');
			break;		
		case "BTN080": // 売上確定
			showModalDlg("../Sc/Sc0703.htm", null, "800", "600");
			break;		
		case "BTN081":
		
			break;		
		case "BTN082":
		
			break;		
		case "BTN083": // 掲載日
			showDialog('calendar');
			break;		
		case "BTN084":
		
			break;		
		case "BTN085":
		
			break;		
		case "BTN086":
		
			break;		
		case "BTN087":// 登録日
			showDialog('calendar');
			break;		
		case "BTN088":
		
			break;		
		case "BTN089":
		
			break;		
		case "BTN090":
		
			break;		
		case "BTN091":
		
			break;		
		case "BTN092":
		
			break;		
		case "BTN093": // 請求日付セット
			showDialog('calendar');
			break;		
		case "BTN094":
		
			break;		
		case "BTN095":
		
			break;		
		case "BTN096":
		
			break;		
		case "BTN097":
		
			break;		
		case "BTN098":
		
			break;		
		case "BTN099":
		
			break;		
		case "BTN100":
		
			break;		
		case "BTN101":
		
			break;		
		case "BTN102":
		
			break;		
		case "BTN103":
		
			break;		
		case "BTN104":
		
			break;		
		case "BTN105":
		
			break;		
		case "BTN106":
		
			break;		
		case "BTN107":
		
			break;		
		case "BTN108":
		
			break;		
		case "BTN109":
		
			break;		
		case "BTN110":
		
			break;		
		case "BTN111":
		
			break;		
		case "BTN112":
		
			break;		
		case "BTN007":
		
			break;		
		case "BTN113":
		
			break;		
		case "BTN114":
		
			break;		
		case "BTN115":
		
			break;		
		case "BTN116":
		
			break;		
		case "BTN117":
		
			break;		
		case "BTN118":
		
			break;		
		case "BTN119":
		
			break;		
		case "BTN120":
		
			break;		
		case "BTN121": // 発売日
			showDialog('calendar');
			break;		
		case "BTN122":
		
			break;		
		case "BTN123":
		
			break;		
		case "BTN124":
		
			break;		
		case "BTN125": // 始版配本日
			showDialog('calendar');
			break;		
		case "BTN126":
		
			break;		
		case "BTN127":
		
			break;		
		case "BTN128": // 新規パターE
			break;		
		case "BTN129":
		
			break;		
		case "BTN130":
		
			break;		
		case "BTN131":
		
			break;		
		case "BTN132":
		
			break;		
		case "BTN133":
		
			break;		
		case "BTN134":
		
			break;		
		case "BTN135":
		
			break;		
		case "BTN136":
		
			break;		
		case "BTN137":
		
			break;		
		case "BTN138":
		
			break;		
		case "BTN139":
		
			break;		
		case "BTN140":
		
			break;		
		case "BTN141":
			showDialog('kik_type_group');
			break;			
		case "BTN142": // 企画分
			showDialog('kik_type');
			break;		
		case "BTN143":
		
			break;		
		case "BTN144":
			showDialog('user');
			break;		
		case "BTN145":
		
			break;		
		case "BTN146":
		
			break;		
		case "BTN147":
		
			break;		
		case "BTN148":
		
			break;		
		case "BTN149":
		
			break;		
		case "BTN150":
		
			break;		
		case "BTN151":
		
			break;		
		case "BTN152":
		
			break;		
		case "BTN153": // 記事下丒業サブ担当
			showDialog('user');
			break;		
		case "BTN154":
		
			break;		
		case "BTN155": // 饮廒記事下丒業サブ担当
			showDialog('user');
			break;		
		case "BTN156":
		
			break;		
		case "BTN157":
		
			break;		
		case "BTN158":
		
			break;		
		case "BTN159":
		
			break;		
		case "BTN160":
		
			break;		
		case "BTN161":
		
			break;		
		case "BTN162":
		
			break;		
		case "BTN163":
		
			break;		
		case "BTN164": // 記事下丒業担当
			showDialog('user');
			break;		
		case "BTN165":
		
			break;		
		case "BTN166":
		
			break;		
		case "BTN167": // 契約適用日
			showDialog('calendar');
			break;		
		case "BTN168":
		
			break;		
		case "BTN169":
		
			break;		
		case "BTN170":
		
			break;		
		case "BTN171": // 丒報丒業担当
			showDialog('user');
			break;		
		case "BTN172": // 蓙Ez用広告会蓙E
			showDialog('adc');
			break;		
		case "BTN173":
		
			break;		
		case "BTN174": // 適用開始日
			showDialog('calendar');
			break;		
		case "BTN175": // 適用終了日
			showDialog('calendar');
			break;		
		case "BTN176":
		
			break;		
		case "BTN177": // 当期記事下丒業サブ担当
			showDialog('user');
			break;		
		case "BTN178":
		
			break;		
		case "BTN179": // 当期記事下丒業担当
			showDialog('user');
			break;		
		case "BTN180":
		
			break;		
		case "BTN181": // 当期丒報丒業担当
			showDialog('user');
			break;		
		case "BTN182":
		
			break;		
		case "BTN183":
		
			break;		
		case "BTN184":
		
			break;		
		case "BTN185": // 売上確定日FROM
			showDialog('calendar');
			break;		
		case "BTN186": // 売上確定日TO
			showDialog('calendar');
			break;		
		case "BTN187":
		
			break;		
		case "BTN188":
		
			break;		
		case "BTN189":
		
			break;		
		case "BTN190": // 本緛E瓒ㄈ?
			showDialog('calendar');
			break;		
		case "BTN191":
		
			break;		
		case "BTN192": // 饮廒記事下丒業担当
			showDialog('user');
			break;		
		case "BTN193":
		
			break;		
		case "BTN194": // 饮廒丒報丒業担当
			showDialog('user');
			break;		
		case "BTN195":
		
			break;		
		case "BTN196":
		
			break;		
		case "BTN197":

			break;
		case "BTN198":
			
			break;
		case "BTN199":
			showModalDlg("../Sc/Sc0205.htm", null, "950", "700");
			break;
		case "BTN200": // 組織変竵E?
			showDialog('calendar');
			break;
		case "BTN202": // 掲載料
		case "BTN203": // 日指定料
		case "BTN204": // 面指定料
		case "BTN205": // 色刷料
		case "BTN206": // 切替料
		case "BTN207": // 二連版料
		case "BTN208": // ????????料
		case "BTN209": // 変形料
			showDialog('kek');
			break;
		case "BTN211": // 組織変竵E?
			showDialog('calendar');
			break;
		case "BTN212": // 組織変竵E?
			showDialog('calendar');
			break;
		case "BTN213": // 組織変竵E?
			showDialog('calendar');
			break;
		case "BTN220": // 掲載希望日
			showDialog('calendar');
			break;	
		case "BTN233": // 売上觼E敿丒
			showModalDlg("../Sc/Sc0603.htm", null, "1000", "700");
			break;
		case "BTN236": // 局内振替詳紒E
			showModalDlg("../Sc/Sc0303.htm", null, "1000", "700");
			break;
		case "BTN237": // 社内振替詳紒E
			showModalDlg("../Sc/Sc0403.htm", null, "1000", "700");
			break;
		case "BTN238": // 回し詳紒E
			showModalDlg("../Sc/Sc0503.htm", null, "1000", "700");
			break;
		case "BTN241": // 作成日
			showDialog('calendar');
			break;
		case "BTN245": // 依赖日
			showDialog('calendar');
			break;
		case "BTN246": // 葋E逶ざㄈ?
			showDialog('calendar');
			break;
		case "BTN247": // 大组希望日
			showDialog('calendar');
			break;
		case "BTN248": 
			showModalDlg("../Sd/Sd0701.htm", null, "1005", "720");
			break;
		default:
			
	}
	
		
}