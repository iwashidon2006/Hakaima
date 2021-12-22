using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Hakaima;
//using NendUnityPlugin.AD;
using GoogleMobileAds.Api;
using NCMB;


public class TitleManager : MonoBehaviour
{

	public enum State
	{
		Menu,
		Ranking,
		Record,
		Help,
		End,
		Extra,
	}

	
	
	public class Catalog
	{

		public int pageNum			{ get; private set; }
		public float positionX		{ get; private set; }
		public int nowPageIndex		{ get; private set; }
		public int prePageIndex		{ get; private set; }
		public bool isMove			{ get; private set; }
		public bool isArrowRight	{ get; private set; }
		public bool isArrowLeft		{ get; private set; }
		
		private float moveTime;
		
		
		public void Init (int pageNum)
		{
			this.pageNum = pageNum;
			this.positionX = 0;
			this.nowPageIndex = 0;
			this.prePageIndex = 0;
			this.isMove = false;
			this.isArrowRight = true;
			this.isArrowLeft = false;
		}
		
		
		public void Move (float deltaTime, int frameRate)
		{
			if (this.isMove) {
				this.moveTime += deltaTime * 2.5f;
				this.positionX = -(-(this.nowPageIndex - this.prePageIndex) * this.moveTime * (this.moveTime - 2) + this.prePageIndex) * Data.SCREEN_WIDTH;
				
				if (this.moveTime >= 1) {
					this.isMove = false;
				}
			}
		}

		
		public void Next ()
		{
			if (!this.isMove) {
				if (this.nowPageIndex < this.pageNum - 1) {
					this.prePageIndex = this.nowPageIndex;
					this.nowPageIndex++;
					this.isMove = true;
					this.moveTime = 0;
					this.isArrowRight = this.nowPageIndex < this.pageNum - 1;
					this.isArrowLeft = true;
				}
			}
		}
		
		
		public void Prev ()
		{
			if (!this.isMove) {
				if (this.nowPageIndex > 0) {
					this.prePageIndex = this.nowPageIndex;
					this.nowPageIndex--;
					this.isMove = true;
					this.moveTime = 0;
					this.isArrowRight = true;
					this.isArrowLeft = this.nowPageIndex > 0;
				}
			}
		}
	}



	private class Bird
	{
		public enum State
		{
			Wait,
			Fly,
		}


		public const int IMAGE_0	= 0;
		public const int IMAGE_1	= 1;


		public State state		{ get; private set; }
		public float time		{ get; private set; }

		public float positionX	{ get; private set; }
		public float positionY	{ get; private set; }
		public float scaleX		{ get; private set; }
		public float scaleY		{ get; private set; }
		public bool visible		{ get; private set; }
		public int imageIndex	{ get; private set; }

		private float speedX;
		private float speedY;
		private float imageTime;

		private float sidePositionX;


		public void Init (float sidePositionX)
		{
			this.sidePositionX = sidePositionX;
			this.Wait ();
		}


		public void Move (float deltaTime, int frameRate)
		{
			switch (this.state) {
			case State.Wait:
				{
				}
				break;
			case State.Fly:
				{
					this.positionX += this.speedX * deltaTime * frameRate;
					this.positionY += this.speedY * deltaTime * frameRate;
					if (Math.Abs (this.positionX) >= this.sidePositionX) {
						this.Wait ();
					}

					int index = (int)this.imageTime % 2;
					switch (index) {
					case 0:
						this.imageIndex = IMAGE_0;
						break;
					case 1:
						this.imageIndex = IMAGE_1;
						break;
					}
					this.imageTime += 0.1f * deltaTime * frameRate;
				}
				break;
			}
		}


		public void Wait ()
		{
			this.state = State.Wait;
			this.time = 0;
			this.visible = false;
		}


		public void Fly ()
		{
			this.state = State.Fly;
			this.time = 0;
			this.imageIndex = 0;
			this.imageTime = 0;
			this.visible = true;

			int distance = UnityEngine.Random.value * 2 < 1 ? 1 : -1;
			this.positionX = -this.sidePositionX * distance;
			this.positionY = 0 + UnityEngine.Random.value * 600;
			this.scaleX = distance;
			this.scaleY = 1;
			this.speedX = 4 * distance;
			this.speedY = 1;
		}

	}



	public class Cover
	{
		public bool visible { get; set; }
		public Color color	{ get; private set; }

		public Cover ()
		{
			this.visible = true;
			this.color = Color.black;
		}

		public void SetAlpha (float alpha)
		{
			Color color = Color.black;
			color.a = alpha;
			this.color = color;
		}

	}



	private class Ranking
	{
		public enum State
		{
			Fetch,		// サーバーからハイスコアを取得.
			Save,		// サーバーにハイスコアを保存.
			Goto,		// サーバーからTop10を取得へ.
			FetchRank,	// 現プレイヤーのハイスコアを受けとってランクを取得.
			TopRank,	// サーバーからTop10を取得.
			Finish,		// 処理終了.
		}

		public State state { get; set; }
		private NCMB.HighScore hiscore;
		private LeaderBoard leaderBoard;

		public void Init()
		{
			if (leaderBoard == null) {
				leaderBoard = new LeaderBoard ();
			}
			if (hiscore == null) {
				hiscore = new NCMB.HighScore (0, 0, "");
			}
			state = State.Fetch;

			if (hiscore != null) {
				hiscore.isCorrect = false;
				hiscore.isCorrectFinish = false;
			}
		}

		public void Fetch(string name)
		{
			if (!hiscore.isCorrect) {
				hiscore = new NCMB.HighScore (0, 0, name);
				hiscore.fetch ();
			}
		}

		public void Save(int score, int stage)
		{
			if (!hiscore.isCorrect) {
				if (hiscore.score < score) {
					hiscore.score = score;
					hiscore.stage = stage;
					hiscore.save ();
				} else {
					hiscore.isCorrectFinish = true;
				}
			}
		}

		public void FetchRank()
		{
			if (!leaderBoard.isCorrect) {
				leaderBoard.fetchRank (hiscore.score);
			}
		}

		public void FetchTopRank()
		{
			if (!leaderBoard.isCorrect) {
				leaderBoard.fetchTopRankers ();
			}
		}

		public void Next(State state)
		{
			if (hiscore.isCorrectFinish) {
				this.state = state;
				hiscore.isCorrect = false;
				hiscore.isCorrectFinish = false;
			}
		}

		public void NextLeaderBoard(State state)
		{
			if (leaderBoard.isfetchRankFinish) {
				this.state = state;
				leaderBoard.isCorrect = false;
				leaderBoard.isfetchRankFinish = false;
			}
			if (leaderBoard.isfetchTopRankersFinish) {
				if (leaderBoard.currentRank > 0 ) {
					MainManager.Instance.loginInfo.SetMyRankInfo(leaderBoard.currentRank, leaderBoard.topRankers[leaderBoard.currentRank-1].score);
					for(int i = 0; i < leaderBoard.topRankers.Count; i++ )
					{
						MainManager.Instance.loginInfo.SetUserName(leaderBoard.topRankers [i].name);
						MainManager.Instance.loginInfo.SetUserScore(leaderBoard.topRankers [i].score);
					}
				}
				this.state = state;
				leaderBoard.isCorrect = false;
				leaderBoard.isfetchTopRankersFinish = false;
			}
		}

		public void debug()
		{
			//Debug.Log ("score = " + hiscore.score + ", stage = " + hiscore.stage+", name = "+hiscore.name+", rank = "+leaderBoard.currentRank);
		}
	}

	private class Gacha
	{
		public float y					{ get; set; }
		public float timer				{ get; set; }
		public float gachabarSet		{ get; set; }
		public float gachabarAngle		{ get; set; }
		public int gachaMode			{ get; set; }
		public int selectedGachaNumber	{ get; set; }

		public void keepY( float y )
		{
			this.y = y;
		}

		public void clear()
		{
			timer = 0;
			gachaMode = 0;
			gachabarSet = 0;
			gachabarAngle = 0;
			selectedGachaNumber = 0;
		}

		public void lottery(int n)
		{
			UnityEngine.Random.InitState ((int)Time.time);

			int start = UnityEngine.Random.Range (0, n - 20);
			int randomNum = UnityEngine.Random.Range (0, n);
			int hit = n / (n / 20);

			//Debug.Log ("Gacha Resut : " + start + " < " + randomNum+" < "+(start+hit));
			// あたり.
			if (randomNum >= start && randomNum < start + hit) {
				//Debug.Log ("HIT");
				selectedGachaNumber = UnityEngine.Random.Range (1, Data.CHARACTER_MAX);
			} else {
				// -1 = life 1up.
				selectedGachaNumber = -1;
			}
		}
	}

	public const int RANKING_PAGE_NUM = 2;
	public const int RECORD_PAGE_NUM = 3;
	public const int HELP_PAGE_NUM = 10;
	private const int EXCHANGE_CODE = 0x10101010;

	private State state;
	private float time;

	private GameObject goMenu;
	private GameObject goRanking;
	private GameObject goRecord;
	private GameObject goHelp;
	private GameObject goEnd;
	private GameObject goExtra;
	private GameObject goCover;
	private GameObject goConceal;
	private GameObject goSelectCharacter;
	private GameObject goGacha;
	private GameObject goGachaStart;
	private GameObject goGachaResult;
	private GameObject goInformation;

	private GameObject goMenuLogo;
	private GameObject goMenuJapaneseTitle;
	private GameObject goMenuButtonStart;
	private GameObject goMenuButtonContinue;
	private GameObject goMenuButtonRanking;
	private GameObject goMenuButtonRecord;
	private GameObject goMenuButtonHelp;
	private GameObject goMenuButtonExtra;
	private GameObject goMenuVolumeOn;
	private GameObject goMenuVolumeOff;
	private GameObject goMenuBird;
	private GameObject goMenuTwitter;
	private GameObject goMenuLoginBonus;
	private GameObject goMenuLoginBonusButtonOk;
	private GameObject goMenuHiscore;

	private GameObject goRankingMe;
	private GameObject goRankingPage;
	private GameObject goRankingConnecting;
	private GameObject goRankingPoint;
	private GameObject goRankingSwipe;
	private GameObject goRankingArrowRight;
	private GameObject goRankingArrowLeft;
	private GameObject goRankingButtonLogout;
	private GameObject goRankingButtonChangeUserName;
	private GameObject goRankingButtonBack;
	private GameObject goRankingExplanation;

	private GameObject goLogin;
	private GameObject goSignup;
	private GameObject goLoginButton;
	private GameObject goSignupButton;
	private GameObject goRegistButton;
	private GameObject goLoginDescription;
	private GameObject goSignupDescription;
	private GameObject goSignupButtonRanking;
	private GameObject goChagne;
	private GameObject goChagneButtonUserName;
	private GameObject goChagneButtonUserPassword;
	private GameObject goChangeResultUserName;
	private GameObject goLoginFirst;
	private GameObject goLoginFirstExplanation;
	private GameObject goSignupFinish;
	private GameObject goSignupRegistedExplanation;

	private GameObject goRecordPage;
	private GameObject goRecordPoint;
	private GameObject goRecordSwipe;
	private GameObject goRecordArrowRight;
	private GameObject goRecordArrowLeft;
	private GameObject goRecordButtonBack;

	private GameObject goHelpPage;
	private GameObject goHelpPoint;
	private GameObject goHelpSwipe;
	private GameObject goHelpArrowRight;
	private GameObject goHelpArrowLeft;
	private GameObject goHelpButtonBack;
	private GameObject goHelpButtonPrivacy;

	private GameObject goCatalogPage;
	private GameObject goCatalogPoint;
	private GameObject goCatalogArrowRight;
	private GameObject goCatalogArrowLeft;

	private GameObject goCaution;
	private GameObject goCautionButtonYes;
	private GameObject goCautionButtonNo;

	private GameObject goEndDescription;
	private GameObject goEndButtonYes;
	private GameObject goEndButtonNo;

	private GameObject goExtraDescription;
	private GameObject goExtraButtonBack;
	private GameObject goExtraItemTitle;
	private GameObject goExtraItemDescription;
	private GameObject goExtraItemButtonMovie;
	private GameObject goExtraLifeTitle;
	private GameObject goExtraLifeDescription;
	private GameObject goExtraLifeButtonMovie;
	private GameObject goExtraLifeNow;
	private GameObject goExtraRecommendedTitle;
	private GameObject goExtraRecommendedButtonMoreGame;

	private GameObject goSelectCharacterGachaTicketText;
	private GameObject goSelectCharacterGachaTicket;
	private GameObject goSelectCharacterFrame;
	private GameObject goSelectCharacterButtonStart;
	private GameObject goSelectCharacterButtonGacha;
	private GameObject goSelectCharacterButtonPlayAds;
	private GameObject goSelectCharacterButtonBack;
	private List<GameObject> goCharacter;

	private GameObject goGachaBar;
	private GameObject goGachaCupsule;
	private GameObject goGachaResultChara;
	private GameObject goGachaResultGachaTicketText;
	private GameObject goGachaResultGachaTicket;
	private GameObject goGachaResultButtonGacha;
	private GameObject goGachaResultButtonPlayAds;
	private GameObject goGachaResultButtonBack;
	private GameObject goGachaResultGotText;
	private GameObject goGachaLotteryChara;
	private GameObject goGachaLotteryLife;

	private GameObject goInformationButton;
	private GameObject goVersion;


	private Catalog catalog;
	private Bird bird;
	private Cover cover;
	private UserAuth user;
	private Ranking ranking;

	private float birdIndex;
    //private int bookedSelectCharacter;

	public Sprite spriteLogo;
	public Sprite spriteLogoEn;

	private List<Sprite> spriteBirdList;
	[SerializeField]
	private Sprite spriteBird0;
	[SerializeField]
	private Sprite spriteBird1;
	[SerializeField]
	private Sprite spriteDragonFly0;
	[SerializeField]
	private Sprite spriteDragonFly1;

	private static bool isCoverOnce = true;

	private string connectingText;

	private Gacha gacha;
	private LoginBonus loginBonus;

	private void Awake ()
	{
		Init ();

		this.state = State.Menu;
		Create ();
	}



	private void Update ()
	{
		Run ();
		Draw ();
	}



	private void Init ()
	{
		string path = Application.systemLanguage == SystemLanguage.Japanese ? Data.HELP_PATH_JAPANESE : Data.HELP_PATH_ENGLISH;

		goMenu							= transform.Find ("UI/Menu").gameObject;
		goRanking						= transform.Find ("UI/Ranking").gameObject;
		goRecord						= transform.Find ("UI/Record").gameObject;
		goHelp							= Instantiate (Resources.Load<GameObject> (path));
		goHelp							.transform.SetParent (transform.Find ("UI"));
		goCaution						= transform.Find ("UI/Caution").gameObject;
		goEnd							= transform.Find ("UI/End").gameObject;
		goExtra							= transform.Find ("UI/Extra").gameObject;
		goCover							= transform.Find ("UI/Cover").gameObject;
		goConceal						= transform.Find ("UI/Conceal").gameObject;
		goSelectCharacter				= transform.Find ("UI/SelectCharacter").gameObject;
		goGacha							= transform.Find ("UI/Gacha").gameObject;
		goGachaStart					= transform.Find ("UI/Gacha/Playing").gameObject;
		goGachaResult					= transform.Find ("UI/Gacha/Result").gameObject;
		goInformation					= transform.Find ("UI/Information").gameObject;

		goMenuLogo						= goMenu.transform.Find ("Logo").gameObject;
		goMenuJapaneseTitle				= goMenu.transform.Find ("JapaneseTitle").gameObject;
		goMenuButtonStart				= goMenu.transform.Find ("ButtonStart").gameObject;
		goMenuButtonContinue			= goMenu.transform.Find ("ButtonContinue").gameObject;
		goMenuButtonRanking				= goMenu.transform.Find ("ButtonRanking").gameObject;
		goMenuButtonRecord				= goMenu.transform.Find ("ButtonRecord").gameObject;
		goMenuButtonHelp				= goMenu.transform.Find ("ButtonHelp").gameObject;
		goMenuButtonExtra				= goMenu.transform.Find ("ButtonExtra").gameObject;
		goMenuVolumeOn					= goMenu.transform.Find ("Volume/On").gameObject;
		goMenuVolumeOff					= goMenu.transform.Find ("Volume/Off").gameObject;
		goMenuBird						= goMenu.transform.Find ("Bird").gameObject;
		goMenuTwitter					= goMenu.transform.Find ("Twitter").gameObject;
		goMenuLoginBonus				= goMenu.transform.Find ("LoginBonus").gameObject;
		goMenuLoginBonusButtonOk		= goMenu.transform.Find ("LoginBonus/ButtonOk").gameObject;
		goMenuHiscore					= goMenu.transform.Find ("Hiscore").gameObject;

		goRankingMe						= goRanking.transform.Find ("Me").gameObject;
		goRankingPage					= goRanking.transform.Find ("Page").gameObject;
		goRankingConnecting				= goRanking.transform.Find ("Connecting").gameObject;
		goRankingPoint					= goRanking.transform.Find ("Point").gameObject;
		goRankingSwipe					= goRanking.transform.Find ("Swipe").gameObject;
		goRankingArrowRight				= goRanking.transform.Find ("ArrowRight").gameObject;
		goRankingArrowLeft				= goRanking.transform.Find ("ArrowLeft").gameObject;
		goRankingButtonChangeUserName	= goRanking.transform.Find ("ButtonChangeUserName").gameObject;
		goRankingButtonBack				= goRanking.transform.Find ("ButtonBack").gameObject;
		goRankingButtonLogout			= goRanking.transform.Find ("ButtonLogout").gameObject;
		goRankingExplanation			= goRanking.transform.Find ("Explanation").gameObject;

		goLogin							= goRanking.transform.Find ("Login").gameObject;
		goSignup						= goRanking.transform.Find ("Signup").gameObject;
		goLoginButton					= goLogin.transform.Find ("ButtonLogin").gameObject;
		goSignupButton					= goLogin.transform.Find ("ButtonSignup").gameObject;
		goRegistButton					= goSignup.transform.Find ("ButtonRegist").gameObject;
		goLoginDescription				= goLogin.transform.Find ("Description").gameObject;
		goSignupDescription				= goSignup.transform.Find ("Description").gameObject;
		goSignupButtonRanking			= goSignup.transform.Find ("ButtonRanking").gameObject;
		goChagne						= goRanking.transform.Find ("Change").gameObject;
		goChagneButtonUserName			= goChagne.transform.Find ("ButtonChangeId").gameObject;
		goChagneButtonUserPassword		= goChagne.transform.Find ("ButtonChangePassword").gameObject;
		goChangeResultUserName			= goChagne.transform.Find ("DescriptionId").gameObject;
		goLoginFirst					= goLogin.transform.Find ("FirstMember").gameObject;
		goLoginFirstExplanation			= goLogin.transform.Find ("FirstMemberExplanation").gameObject;
		goSignupFinish					= goSignup.transform.Find ("Finish").gameObject;
		goSignupRegistedExplanation		= goSignup.transform.Find ("RegistedExplanation").gameObject;

		goRecordPage					= goRecord.transform.Find ("Page").gameObject;
		goRecordPoint					= goRecord.transform.Find ("Point").gameObject;
		goRecordSwipe					= goRecord.transform.Find ("Swipe").gameObject;
		goRecordArrowRight				= goRecord.transform.Find ("ArrowRight").gameObject;
		goRecordArrowLeft				= goRecord.transform.Find ("ArrowLeft").gameObject;
		goRecordButtonBack				= goRecord.transform.Find ("ButtonBack").gameObject;

		goHelpPage						= goHelp.transform.Find ("Page").gameObject;
		goHelpPoint						= goHelp.transform.Find ("Point").gameObject;
		goHelpSwipe						= goHelp.transform.Find ("Swipe").gameObject;
		goHelpArrowRight				= goHelp.transform.Find ("ArrowRight").gameObject;
		goHelpArrowLeft					= goHelp.transform.Find ("ArrowLeft").gameObject;
		goHelpButtonBack				= goHelp.transform.Find ("ButtonBack").gameObject;
		goHelpButtonPrivacy				= goHelp.transform.Find ("ButtonPrivacy").gameObject;
		Destroy (goHelp.transform.Find ("Attention").gameObject);

		goCautionButtonYes				= goCaution.transform.Find ("ButtonYes").gameObject;
		goCautionButtonNo				= goCaution.transform.Find ("ButtonNo").gameObject;

		goEndDescription				= goEnd.transform.Find ("Description").gameObject;
		goEndButtonYes					= goEnd.transform.Find ("ButtonYes").gameObject;
		goEndButtonNo					= goEnd.transform.Find ("ButtonNo").gameObject;

		goExtraDescription				= goExtra.transform.Find ("Description").gameObject;
		goExtraButtonBack				= goExtra.transform.Find ("ButtonBack").gameObject;
		goExtraItemTitle				= goExtra.transform.Find ("Item/Title").gameObject;
		goExtraItemDescription			= goExtra.transform.Find ("Item/Description").gameObject;
		goExtraItemButtonMovie			= goExtra.transform.Find ("Item/ButtonMovie").gameObject;
		goExtraLifeTitle				= goExtra.transform.Find ("Life/Title").gameObject;
		goExtraLifeDescription			= goExtra.transform.Find ("Life/Description").gameObject;
		goExtraLifeButtonMovie			= goExtra.transform.Find ("Life/ButtonMovie").gameObject;
		goExtraLifeNow					= goExtra.transform.Find ("Life/Now").gameObject;
		goExtraRecommendedTitle			= goExtra.transform.Find ("Recommended/Title").gameObject;
		goExtraRecommendedButtonMoreGame= goExtra.transform.Find ("Recommended/ButtonMoreGame").gameObject;

		goSelectCharacterGachaTicketText= goSelectCharacter.transform.Find ("GachaTicketText").gameObject;
		goSelectCharacterGachaTicket	= goSelectCharacter.transform.Find ("GachaTicket").gameObject;
		goSelectCharacterFrame			= goSelectCharacter.transform.Find ("Frame").gameObject;
		goSelectCharacterFrame.GetComponent<Animation> ().wrapMode = WrapMode.Loop;
		goSelectCharacterButtonStart	= goSelectCharacter.transform.Find ("ButtonStart").gameObject;
		goSelectCharacterButtonGacha	= goSelectCharacter.transform.Find ("ButtonGacha").gameObject;
		goSelectCharacterButtonPlayAds	= goSelectCharacter.transform.Find ("ButtonPlayAds").gameObject;
		goSelectCharacterButtonBack		= goSelectCharacter.transform.Find ("ButtonBack").gameObject;

		goGachaBar						= goGachaStart.transform.Find ("GachaBar").gameObject;
		goGachaCupsule					= goGachaStart.transform.Find ("Cupsule").gameObject;
		goGachaResultChara				= goGachaResult.transform.Find ("Chara").gameObject;
		goGachaResultGachaTicketText	= goGachaResult.transform.Find ("GachaTicketText").gameObject;
		goGachaResultGachaTicket		= goGachaResult.transform.Find ("GachaTicket").gameObject;
		goGachaResultButtonGacha		= goGachaResult.transform.Find ("ButtonGacha").gameObject;
		goGachaResultButtonPlayAds		= goGachaResult.transform.Find ("ButtonPlayAds").gameObject;
		goGachaResultButtonBack			= goGachaResult.transform.Find ("ButtonBack").gameObject;
		goGachaResultGotText			= goGachaResult.transform.Find ("GotText").gameObject;
		goGachaLotteryChara				= goGachaStart.transform.Find ("LotteryChara").gameObject;
		goGachaLotteryLife				= goGachaStart.transform.Find ("LotteryLife").gameObject;

		goInformationButton				= goInformation.transform.Find ("ButtonOk").gameObject;
		goVersion						= goMenu.transform.Find ("Version").gameObject;
		goVersion.GetComponent<Text> ().text = "Ver." + Application.version;

		goMenuLogo						.GetComponent<Image> ().sprite = Language.sentence == Language.sentenceEn ? spriteLogoEn : spriteLogoEn;
		goMenuLogo						.GetComponent<Image> ().SetNativeSize ();
		goMenuJapaneseTitle				.SetActive (Language.sentence == Language.sentenceJa);
		goMenuButtonStart				.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonStartSelectCharacter ());
		goMenuButtonContinue			.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonContinue ());
		goMenuButtonRanking				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Ranking));
		goMenuButtonRecord				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Record));
		goMenuButtonHelp				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Help));
		goMenuButtonExtra				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Extra));
		goMenuVolumeOn					.GetComponent<Button> ().onClick.AddListener (() => OnVolume (true));
		goMenuVolumeOff					.GetComponent<Button> ().onClick.AddListener (() => OnVolume (false));
		goMenuTwitter					.GetComponent<Button> ().onClick.AddListener (() => OnTwitter ());
		goMenuLoginBonusButtonOk		.GetComponent<Button> ().onClick.AddListener (() => OnButtonLoginBonusClose ());
		if (MainManager.Instance.isTutorial) {
			goMenuButtonStart.transform.localPosition = goMenuButtonContinue.transform.localPosition;
			goMenuButtonContinue.SetActive (false);
		}
		goMenuHiscore.GetComponent<Text> ().text = "HISCORE  " + MainManager.Instance.scoreHigh.ToString ("D7");

		goLoginButton					.GetComponent<Button> ().onClick.AddListener (() => OnLogin ());
		goSignupButton					.GetComponent<Button> ().onClick.AddListener (() => OnButtonRegist ());
		goRegistButton					.GetComponent<Button> ().onClick.AddListener (() => OnButtonRegist ());
		goRankingButtonChangeUserName	.GetComponent<Button> ().onClick.AddListener (() => OnChange ());
		goRankingButtonBack				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));
		goRankingButtonLogout			.GetComponent<Button> ().onClick.AddListener (() => OnLogout ());
		goRankingArrowRight				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogNextPage ());
		goRankingArrowLeft				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogPrevPage ());
		goChagneButtonUserName			.GetComponent<Button> ().onClick.AddListener (() => OnChangeUserName ());
		goChagneButtonUserPassword		.GetComponent<Button> ().onClick.AddListener (() => OnChangeUserPassword ());
		goSignupButtonRanking			.GetComponent<Button> ().onClick.AddListener (() => OnRanking ());

		goRecordButtonBack				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));
		goRecordArrowRight				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogNextPage ());
		goRecordArrowLeft				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogPrevPage ());

		goHelpButtonBack				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));
		goHelpButtonPrivacy				.GetComponent<Button> ().onClick.AddListener (() => OnPrivacyPorisy ());
		goHelpArrowRight				.GetComponent<Button> ().onClick.AddListener (() => OnCatalogNextPage ());
		goHelpArrowLeft					.GetComponent<Button> ().onClick.AddListener (() => OnCatalogPrevPage ());
		goRankingSwipe					.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.Drag).callback.AddListener (eventData => OnSwipe ((PointerEventData)eventData));
		goRecordSwipe					.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.Drag).callback.AddListener (eventData => OnSwipe ((PointerEventData)eventData));
		goHelpSwipe						.GetComponent<EventTrigger> ().triggers.Find (obj => obj.eventID == EventTriggerType.Drag).callback.AddListener (eventData => OnSwipe ((PointerEventData)eventData));

		goCaution						.transform.Find ("Text").GetComponent<Text> ().text = Language.sentence [Language.START_CAUTION];
		goCautionButtonYes				.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonStart ());
		goCautionButtonNo				.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonCaution (false));

		goEndDescription				.GetComponent<Text> ().text = Language.sentence [Language.APPLICATION_QUIT];
		goEndDescription				.GetComponent<Text> ().fontSize = Language.sentence == Language.sentenceJa ? 50 : 70;
		goEndButtonYes					.GetComponent<Button> ().onClick.AddListener (() => OnEnd ());
		goEndButtonNo					.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));

		goExtraDescription				.GetComponent<Text> ().text = Language.sentence [Language.EXTRA_DESCRIPTION];
		goExtraItemTitle				.GetComponent<Text> ().text = Language.sentence [Language.EXTRA_ITEM_TITLE];
		goExtraItemDescription			.GetComponent<Text> ().text = Language.sentence [Language.EXTRA_ITEM_DESCRIPTION];
		goExtraLifeTitle				.GetComponent<Text> ().text = Language.sentence [Language.EXTRA_LIFE_TITLE];
		goExtraLifeDescription			.GetComponent<Text> ().text = Language.sentence [Language.EXTRA_LIFE_DESCRIPTION];
		goExtraRecommendedTitle			.GetComponent<Text> ().text = Language.sentence [Language.EXTRA_RECOMMENDED_TITLE];
		goExtraButtonBack				.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));
		goExtraItemButtonMovie			.GetComponent<Button> ().onClick.AddListener (() => OnExtraItemButtonMovie ());
		goExtraLifeButtonMovie			.GetComponent<Button> ().onClick.AddListener (() => OnExtraLifeButtonMovie ());
		goExtraLifeNow					.GetComponent<Text> ().text = MainManager.Instance.life.ToString ();
		goExtraRecommendedButtonMoreGame.GetComponent<Button> ().onClick.AddListener (() => OnExtraButtonMoreGame ());

		if (MainManager.Instance.isTutorial) {
			goSelectCharacterButtonStart	.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonStart ());
		} else {
			goSelectCharacterButtonStart	.GetComponent<Button> ().onClick.AddListener (() => OnMenuButtonCaution (true));
		}
		goSelectCharacterButtonGacha	.GetComponent<Button> ().onClick.AddListener (() => OnGacha ());
		goSelectCharacterButtonPlayAds	.GetComponent<Button> ().onClick.AddListener (() => ShowAdsMovie ());
		goSelectCharacterButtonBack		.GetComponent<Button> ().onClick.AddListener (() => OnButton (State.Menu, false));
		goGachaResultButtonGacha		.GetComponent<Button> ().onClick.AddListener (() => OnGacha ());
		goGachaResultButtonPlayAds		.GetComponent<Button> ().onClick.AddListener (() => ShowAdsMovie ());
		goGachaResultButtonBack			.GetComponent<Button> ().onClick.AddListener (() => OnGachaResultBackButton ());
		goGachaLotteryChara				.GetComponent<Text> ().text = Language.sentence [Language.GACHA_LOTTERY_CHARA];
		goGachaLotteryLife				.GetComponent<Text> ().text = Language.sentence [Language.GACHA_LOTTERY_LIFE];

		goInformationButton				.GetComponent<Button> ().onClick.AddListener (() => OnButtonInformationClose ());


		goCharacter = new List<GameObject> ();
		for (int i = 0; i < Data.CHARACTER_MAX; i++) {
			goCharacter.Add (goSelectCharacter.transform.Find ("Character/Chara" + i).gameObject);
		}
		for (int i = 0; i < Data.CHARACTER_MAX; i++) {
			GameObject obj = goSelectCharacter.transform.Find ("Character/Chara" + i).gameObject;
			goCharacter [i].GetComponent<SelectCharacter> ().buttonNumber = i;
			goCharacter [i].GetComponent<Button> ().onClick.AddListener (() => obj.GetComponent<SelectCharacter>().OnSelectCharacter ());
		}

		gacha = new Gacha ();
		gacha.clear ();
		gacha.keepY (goGachaCupsule.transform.localPosition.y);
		InitSelectCharacterFrame ();
		loginBonus = new LoginBonus ();
		loginBonus.LoadLoginTime ();
		loginBonus.prevGachaTicket = MainManager.Instance.gachaTicket;
		// ログインボーナス.
		if (!IsOffline()) {
			StartCoroutine (loginBonus.GetLoginBonus());
		}
			
		bird 	= new Bird ();
		catalog = new Catalog ();
		cover 	= new Cover ();
		user 	= new UserAuth ();
		ranking = new Ranking ();

		SoundManager.Instance.PlaySe (SoundManager.SeName.JINGLE_TITLE);
		OnVolume (PlayerPrefs.GetInt (Data.SOUND_MUTE) == 1);


		spriteBirdList = new List<Sprite> (){
			spriteDragonFly0,
			spriteDragonFly1,
		};


		goConceal.SetActive (true);
		goCover.SetActive (isCoverOnce);
		isCoverOnce = false;

		// ランキングアクセス時に毎回通信しないためにここで初期化。
		// もし毎回させたい場合は、Create()の中で呼ぶ。
		ranking.Init ();

		// Add 2017.11.7
		#if UNITY_IOS
		goExtraRecommendedButtonMoreGame.transform.Find("Image/Text").GetComponent<Text>().text = "App Store";
		#endif
	}
	
	
	
	private void Create ()
	{
		goMenu.SetActive (false);
		goRanking.SetActive (false);
		goRecord.SetActive (false);
		goHelp.SetActive (false);
		goCaution.SetActive (false);
		goEnd.SetActive (false);
		goExtra.SetActive (false);
		//MainManager.Instance.nendAdIcon.Hide ();
		MainManager.Instance.bannerView.Hide ();

		switch (this.state) {
		case State.Menu:
			{
				goMenu.SetActive (true);
				goSelectCharacter.SetActive (false);
				//SetInformation ();    // 2019.4.9 Ver.1.2.9はちょっとした不具合修正のため表示しない
				SetLoginBonus ();

				time = 0;
				birdIndex = 0;
				bird.Init (800);

				//if (MainManager.Instance.isAdvertise)
				//	MainManager.Instance.nendAdIcon.Show ();
				//goMenu.transform.Find ("PrivateAds").gameObject.SetActive (IsOffline ());	// オフラインの時のみプライベート広告.
				MainManager.Instance.bannerView.Show ();
				FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_MENU);
			}
			break;
		case State.Ranking:
			{
				goRanking.SetActive (true);
				goLoginDescription.SetActive (false);
				goSignupDescription.SetActive (false);
				goRankingConnecting.SetActive (false);
				goLogin.SetActive (false);
				goSignup.SetActive (false);
				goChagne.SetActive (false);
				goRankingMe.SetActive (false);
				goRankingPage.SetActive (false);
				goRankingPoint.SetActive (false);
				goRankingSwipe.SetActive (false);
				goRankingArrowLeft.SetActive (false);
				goRankingArrowRight.SetActive (false);
				goRankingButtonChangeUserName.SetActive (false);
				goRankingButtonBack.SetActive (true);
				goRankingButtonLogout.SetActive (false);
				goLogin.transform.Find ("Id/TextId").GetComponent<Text> ().text = Language.sentence [Language.RANKING_NAME];
				goLogin.transform.Find ("Password/TextPassword").GetComponent<Text> ().text = Language.sentence [Language.RANKING_PASSWORD];
				goSignup.transform.Find ("Id/TextId").GetComponent<Text> ().text = Language.sentence [Language.RANKING_NAME];
				goSignup.transform.Find ("Password/TextPassword").GetComponent<Text> ().text = Language.sentence [Language.RANKING_PASSWORD];
				goSignup.transform.Find ("RePassword/TextPassword").GetComponent<Text> ().text = Language.sentence [Language.RANKING_PASSWORD_CONFIRM];
				goSignup.transform.Find ("Id/InputField/Placeholder").GetComponent<Text> ().text = Language.sentence [Language.RANKING_NAME_FORM];
				goSignup.transform.Find ("Password/InputField/Placeholder").GetComponent<Text> ().text = Language.sentence [Language.RANKING_PASSWORD_FORM];
				goRankingExplanation.GetComponent<Text> ().text = Language.sentence [Language.RANKING_THISSYSTEM];
				goLoginFirst.GetComponent<Text>().text = Language.sentence [Language.LOGIN_BIGINNER];
				goLoginFirstExplanation.GetComponent<Text>().text = Language.sentence [Language.RANKING_FIRST_EXPLANATION];
				goSignupFinish.GetComponent<Text>().text = Language.sentence [Language.LOGIN_FINISH];
				goSignupRegistedExplanation.GetComponent<Text>().text = Language.sentence [Language.RANKING_REGISTED_EXPLANATION];
				goChagne.transform.Find ("Id/TextId").GetComponent<Text> ().text = Language.sentence [Language.RANKING_NAME];
				goChagne.transform.Find ("ButtonChangeId/Image/Text").GetComponent<Text> ().text = Language.sentence [Language.RANKING_NAME_CHANGE];

				// 8/19 エラーの原因は、毎回TitleManagerを削除しているので、未ログイン扱いになってしまう、ゲーム終了後は毎回ランキングデータを持ってこないといけない
				// もしくはローカルに保存してそれを表示する　こっちのほうが現実的

				if (IsOffline ()) {
					SetConnecting (false);
					goRankingConnecting.SetActive (true);
					goRankingButtonBack.SetActive (true);
					//goRankingConnecting.GetComponent<Text> ().color = new Color (1, 0, 0, 0);
					connectingText = Language.sentence [Language.OFFLINE];
					goRankingConnecting.GetComponent<Text> ().text = connectingText;
				} else {
					// 未ログイン
					if (!MainManager.Instance.isLogin) {
						goLogin.SetActive (true);
						// 以前ログインしていればストレージから情報を得て自動ログイン
						AutoLogin ();
						FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_RANKING);
						// ログイン済み
					} else {
						goRankingButtonChangeUserName.SetActive (true);
						OnRanking ();
						FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_RANKING);
					}
				}
			}
			break;
		case State.Record:
			{
				goRecord.SetActive (true);

				string text = null;
				text += Language.sentence [Language.RECORD_ENEMY_DIE_TO_TOMB] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ENEMY_DIE_TO_TOMB)) + "\n";
				text += Language.sentence [Language.RECORD_ENEMY_DIE_TO_HOLE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ENEMY_DIE_TO_HOLE)) + "\n";
				text += Language.sentence [Language.RECORD_TOMB_COLLAPSE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_TOMB_COLLAPSE)) + "\n";
				text += Language.sentence [Language.RECORD_HOLE_OPEN] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_HOLE_OPEN)) + "\n";
				text += Language.sentence [Language.RECORD_HOLE_CLOSE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_HOLE_CLOSE)) + "\n";
				text += Language.sentence [Language.RECORD_HOLE_FALL] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_HOLE_FALL)) + "\n";
				text += Language.sentence [Language.RECORD_BONUS_APPEAR] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_BONUS_APPEAR)) + "\n";
				text += Language.sentence [Language.RECORD_BONUS_GET] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_BONUS_GET)) + "\n";
				text += Language.sentence [Language.RECORD_ITEM_GET] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ITEM_GET)) + "\n";
				text += Language.sentence [Language.RECORD_DAMAGE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_DAMAGE)) + "\n";
				text += Language.sentence [Language.RECORD_ESCAPE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_ESCAPE)) + "\n";
				text += Language.sentence [Language.RECORD_MAX_TOMB_COLLAPSE] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_MAX_TOMB_COLLAPSE)) + "\n";
				text += Language.sentence [Language.RECORD_SCORE_ALL] + string.Format ("{0,5}", PlayerPrefs.GetInt (Data.RECORD_SCORE_ALL));
				goRecordPage.transform.Find ("Page0").GetComponent<Text> ().text = text;
				if (Language.sentence == Language.sentenceEn) {
					goRecordPage.transform.Find ("Page0").GetComponent<RectTransform> ().sizeDelta = new Vector2 (925, 1000);
				}

				string text0 = null;
				string text1 = null;
				for (int i = 0; i < 20; i++) {
					TimeSpan span = TimeSpan.FromSeconds (Data.GetStageData (i).limitTime - PlayerPrefs.GetFloat (Data.RECORD_CLEAR_TIME + i));
					string record = string.Format (Language.sentence [Language.RECORD_CLEAR_TIME] + "\t{1:00}:{2:00}:{3}\n", i + 1, span.Minutes, span.Seconds, span.Milliseconds.ToString ("000").Substring (0, 2));
					if (PlayerPrefs.GetInt (Data.RECORD_CLEAR + i) == 0) {
						record = string.Format (Language.sentence [Language.RECORD_CLEAR_TIME] + "\t--:--:--\n", i + 1);
					}
					if (i < 10) {
						text0 += record;
					} else {
						text1 += record;
					}
				}
				goRecordPage.transform.Find ("Page1").GetComponent<Text> ().text = text0;
				goRecordPage.transform.Find ("Page2").GetComponent<Text> ().text = text1;

				catalog.Init (RECORD_PAGE_NUM);
				goCatalogPage = goRecordPage;
				goCatalogPoint = goRecordPoint;
				goCatalogArrowRight = goRecordArrowRight;
				goCatalogArrowLeft = goRecordArrowLeft;
				FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_RECORD);
			}
			break;
		case State.Help:
			{
				goHelp.SetActive (true);

				catalog.Init (HELP_PAGE_NUM);
				goCatalogPage = goHelpPage;
				goCatalogPoint = goHelpPoint;
				goCatalogArrowRight = goHelpArrowRight;
				goCatalogArrowLeft = goHelpArrowLeft;
				FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_HELP);
			}
			break;
		case State.End:
			{
				goEnd.SetActive (true);
			}
			break;
		case State.Extra:
			{
				goExtra.SetActive (true);
				FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_EXTRA);

				if (MainManager.Instance.isExtraItemSandal)
				if (MainManager.Instance.isExtraItemHoe)
				if (MainManager.Instance.isExtraItemStone)
				if (MainManager.Instance.isExtraItemParasol)
					goExtraItemDescription.GetComponent<Text> ().text = Language.sentence [Language.EXTRA_ITEM_DESCRIPTION_HAVE];
			}
			break;
		}
	}



	private void Run ()
	{
		CheckBackKey ();


		switch (this.state) {
		case State.Menu:
			{
				if (time >= 0.05f)
					cover.visible = false;

				if (time >= birdIndex * 10) {
					birdIndex++;
					bird.Fly ();
				}
				bird.Move (Time.deltaTime, Data.TARGET_FRAME_RATE);

				time += Time.deltaTime;

				ProcGacha ();
			}
			break;
		case State.Ranking:
			{
				if (Logined()) {
					//MainManager.Instance.isDebug = true;
					if (Ranking.State.Fetch == ranking.state) {
						//goRankingButtonChangeUserName.SetActive (true);
						goRankingButtonBack.SetActive (true);
					}
					HighScoreData ();
					RankingData ();
				}

				Connecting ();
				if ( goRankingPoint.activeSelf ) {
					catalog.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
				}
			}
			break;
		case State.Record:
		case State.Help:
			{
				catalog.Move (Data.DELTA_TIME, Data.TARGET_FRAME_RATE);
			}
			break;
		}
	}



	private void Draw ()
	{
		switch (this.state) {
		case State.Menu:
			{
				if (goCover.activeSelf != cover.visible) {
					goCover.SetActive (cover.visible);
				}
				if (cover.visible) {
					if (goCover.GetComponent<Image> ().color != cover.color) {
						goCover.GetComponent<Image> ().color = cover.color;
					}
				}
				if (goMenuBird.activeSelf != bird.visible) {
					goMenuBird.SetActive (bird.visible);
				}
				if (bird.visible) {
					if (goMenuBird.transform.localPosition.x != bird.positionX || goMenuBird.transform.localPosition.y != bird.positionY) {
						goMenuBird.transform.localPosition = new Vector3 (bird.positionX, bird.positionY);
					}
					if (goMenuBird.transform.localScale.x != bird.scaleX || goMenuBird.transform.localScale.y != bird.scaleY) {
						goMenuBird.transform.localScale = new Vector3 (bird.scaleX, bird.scaleY);
					}
					if (goMenuBird.GetComponent<Image> ().sprite != spriteBirdList [bird.imageIndex]) {
						goMenuBird.GetComponent<Image> ().sprite = spriteBirdList [bird.imageIndex];
					}
				}
			}
			break;
		case State.Ranking:
			{
				if ( goRankingPoint.activeSelf ) {
					if (goCatalogPage.transform.localPosition.x != catalog.positionX) {
						goCatalogPage.transform.localPosition = new Vector3 (catalog.positionX, goCatalogPage.transform.localPosition.y);
					}
					if (goCatalogArrowRight.activeSelf != catalog.isArrowRight) {
						goCatalogArrowRight.SetActive (catalog.isArrowRight);
					}
					if (goCatalogArrowLeft.activeSelf != catalog.isArrowLeft) {
						goCatalogArrowLeft.SetActive (catalog.isArrowLeft);
					}
					goCatalogPoint.transform.Find ("PointNow").localPosition = goCatalogPoint.transform.Find ("Point" + catalog.nowPageIndex).localPosition;
				}
			}
			break;
		case State.Record:
		case State.Help:
			{
				if (goCatalogPage.transform.localPosition.x != catalog.positionX) {
					goCatalogPage.transform.localPosition = new Vector3 (catalog.positionX, goCatalogPage.transform.localPosition.y);
				}
				if (goCatalogArrowRight.activeSelf != catalog.isArrowRight) {
					goCatalogArrowRight.SetActive (catalog.isArrowRight);
				}
				if (goCatalogArrowLeft.activeSelf != catalog.isArrowLeft) {
					goCatalogArrowLeft.SetActive (catalog.isArrowLeft);
				}
				goCatalogPoint.transform.Find ("PointNow").localPosition = goCatalogPoint.transform.Find ("Point" + catalog.nowPageIndex).localPosition;
			}
			break;
		}
	}
	
	
	
	private void OnMenuButtonStart ()
	{
        //MainManager.Instance.selectCharacter = bookedSelectCharacter;   // 2019.4.9 iwasaki 予約しておいた番号を代入 => 廃止
        //MainManager.Instance.SaveCharacter ();
        MainManager.Instance.StoryPrologue ();
		MainManager.Instance.RecordSave ();
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
		//MainManager.Instance.nendAdIcon.Hide ();
		MainManager.Instance.bannerView.Hide ();
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_START);
	}

	private void OnMenuButtonStartSelectCharacter()
	{
		goSelectCharacter.SetActive (true);
		ReflashGachaTicket ();
		ShowCharacter ();
		ChangeCharacterName ();
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
		//MainManager.Instance.nendAdIcon.Hide ();
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_SELECT_CHARACTER);
	}
	
	
	private void OnMenuButtonCaution (bool active)
	{
		goCaution.SetActive (active);
		SoundManager.Instance.PlaySe (active ? SoundManager.SeName.SE_OK : SoundManager.SeName.SE_CANCEL);
	}



	private void OnMenuButtonContinue ()
	{
		MainManager.Instance.CurrentStage (MainManager.Instance.life, MainManager.Instance.weapon);
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
		//MainManager.Instance.nendAdIcon.Hide ();
		MainManager.Instance.bannerView.Hide ();
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_CONTINUE);
	}



	private void OnSwipe (PointerEventData eventData)
	{
		float ratio = 1.0f * Screen.width / Data.SCREEN_WIDTH;

		Vector2 delta = eventData.delta / ratio;
		if (delta.x < -5) {
			OnCatalogNextPage ();
		} else if (delta.x > 5) {
			OnCatalogPrevPage ();
		}
	}



	private void OnCatalogNextPage ()
	{
		if (!catalog.isMove) {
			catalog.Next ();
			if (catalog.isMove)
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_MOVE);
		}
	}



	private void OnCatalogPrevPage ()
	{
		if (!catalog.isMove) {
			catalog.Prev ();
			if (catalog.isMove)
				SoundManager.Instance.PlaySe (SoundManager.SeName.SE_MOVE);
		}
	}



	private void OnButton (State state, bool ok = true)
	{
		this.state = state;
		Create ();
		SoundManager.Instance.PlaySe (ok ? SoundManager.SeName.SE_OK : SoundManager.SeName.SE_CANCEL);
	}


	private void OnPrivacyPorisy()
	{
		string url = Data.PRIVACY_POLICY_URL;
		Application.OpenURL(url);
	}


	private void OnVolume (bool isMute)
	{
		SoundManager.Instance.SetMute (isMute);
		goMenuVolumeOn.SetActive (!isMute);
		goMenuVolumeOff.SetActive (isMute);
		PlayerPrefs.SetInt (Data.SOUND_MUTE, isMute ? 1 : 0);
	}



	private void OnTwitter ()
	{
		// Add 2017.11.7
		#if UNITY_ANDROID
		SocialConnector.SocialConnector.Share (Language.sentence [Language.TWITTER], Data.URL, null);
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_TWITTER);
		#elif UNITY_IOS
		SocialConnector.SocialConnector.Share (Language.sentence [Language.TWITTER], Data.URL_IOS, null);
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_TWITTER);
		#endif
	}



	private void OnEnd ()
	{
		Application.Quit ();
	}



	private void OnExtraItemButtonMovie ()
	{
		MainManager.Instance.ShowInterstitial (() => {
			FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_EXTRA_ITEM);
			MainManager.Instance.isExtraItemSandal = true;
			MainManager.Instance.isExtraItemHoe = true;
			MainManager.Instance.isExtraItemStone = true;
			MainManager.Instance.isExtraItemParasol = true;
			Create ();
		});
	}



	private void OnExtraLifeButtonMovie ()
	{
		MainManager.Instance.ShowInterstitial (() => {
			FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_EXTRA_LIFE);
			MainManager.Instance.life += 5;
			goExtraLifeNow.GetComponent<Text> ().text = MainManager.Instance.life.ToString ();
		});
	}

	

	// アプリ紹介
	private void OnExtraButtonMoreGame ()
	{
		#if UNITY_ANDROID
		// market://details?id=パッケージ名
		Application.OpenURL (Data.MORE_GAME_PACKAGENAME_ANDROID);
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_EXTRA_MOREGAME);
		#elif UNITY_IOS
		// http://appstore.com/アプリ名
		Application.OpenURL (Data.MORE_GAME_PACKAGENAME_IOS);
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_EXTRA_MOREGAME);
		#endif
	}

	// ガチャ.
	private void OnGacha()
	{
		if (MainManager.Instance.gachaTicket <= 0) {
			SoundManager.Instance.PlaySe (SoundManager.SeName.SE_CANCEL);
			return;
		}
		
		FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_GACHA_PLAY);
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
		MainManager.Instance.gachaTicket--;
		ReflashGachaTicket ();
		gacha.clear ();
		goGachaCupsule.transform.localPosition = new Vector3 (goGachaCupsule.transform.localPosition.x, gacha.y, goGachaCupsule.transform.localPosition.z);
		gacha.gachaMode = 1;
		goGacha.SetActive (true);
		goGachaStart.SetActive (true);
		goGachaResult.SetActive (false);
		goSelectCharacter.SetActive (false);
	}

	private void OnGachaResultBackButton()
	{
		SoundManager.Instance.PlaySe (SoundManager.SeName.SE_CANCEL);
		ShowCharacter ();
		goGacha.SetActive (false);
		goGachaStart.SetActive (false);
		goGachaResult.SetActive (false);
		goSelectCharacter.SetActive (true);
	}

	public void OnSelectCharacter(int number)
	{
		if (MainManager.Instance.IsCharacter (number)) {
			float x = goCharacter [number].transform.localPosition.x;
			float y = goCharacter [number].transform.localPosition.y + 22;
			goSelectCharacterFrame.transform.localPosition = new Vector3 (x, y, 0);
			SoundManager.Instance.PlaySe (SoundManager.SeName.SE_OK);
            //bookedSelectCharacter = number;   // 2019.4.9 iwasaki 予約のみ => 廃止
            MainManager.Instance.selectCharacter = number;
			MainManager.Instance.SaveCharacter ();
		} else {
			SoundManager.Instance.PlaySe (SoundManager.SeName.SE_CANCEL);
		}
	}

	private void InitSelectCharacterFrame()
	{
		float x = goCharacter [MainManager.Instance.selectCharacter].transform.localPosition.x;
		float y = goCharacter [MainManager.Instance.selectCharacter].transform.localPosition.y + 22;
		goSelectCharacterFrame.transform.localPosition = new Vector3 (x, y, 0);
	}

	// ガチャ結果で表示するキャラクター or ハート.
	private void ChangeSprite()
	{
		//Debug.Log (gacha.selectedGachaNumber);
		if (gacha.selectedGachaNumber == -1) {
			string uri = "Textures/hart";
			//Debug.Log (uri);
			Sprite spt = Resources.Load<Sprite> (uri) as Sprite;
			goGachaResultChara.GetComponent<Image> ().sprite = spt;
			goGachaResultChara.GetComponent<Image> ().SetNativeSize ();
			//goGachaResultChara.transform.localScale = new Vector3 (2, 2, 2);
		} else {
			string uri = "Textures/player" + gacha.selectedGachaNumber + "_bottom_0";
			Sprite spt = Resources.Load<Sprite> (uri) as Sprite;
			//Debug.Log (uri);
			goGachaResultChara.GetComponent<Image> ().sprite = spt;
			goGachaResultChara.GetComponent<Image> ().SetNativeSize ();
			goGachaResultChara.transform.localScale = new Vector3 (2, 2, 2);
		}
	}

	// ガチャ演出と抽選.
	private void ProcGacha()
	{
		// ガチャチケット獲得.
		if (MainManager.Instance.isInterstitialClose) {
			MainManager.Instance.gachaTicket += 1;
			ReflashGachaTicket();
			MainManager.Instance.isInterstitialClose = false;
			FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_GACHA_BANNER_ADS);
		}

		if (gacha.gachaMode > 0) {
			// 演出スキップ.
			bool isSkip = false;
			if (Input.GetMouseButton (0)) {
				gacha.gachaMode = 8;
				isSkip = true;
			}
			// カプセルが転がる.
			if (gacha.gachaMode == 8) {
				goGachaCupsule.SetActive (true);
			}
			// ガチャアプセルのアニメスタート.
			if (goGachaCupsule.activeSelf) {
				if (goGachaCupsule.transform.localPosition.y <= -599 || isSkip) {
					gacha.lottery (100);
					ChangeSprite();
					MainManager.Instance.SaveCharacter ();
					if (gacha.selectedGachaNumber == -1) {
						goGachaResultGotText.GetComponent<Text> ().text = Language.sentence [Language.LIFEIS1UP];
						MainManager.Instance.life++;
						FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_GACHA_LIFE);
					} else {
						goGachaResultGotText.GetComponent<Text> ().text = Language.sentence [Language.YOUGOTCHARA];
						MainManager.Instance.GetCharacter (gacha.selectedGachaNumber);
						string chara = Data.FIREBASE_EVENT_GACHA_KUNOICHI;
						if (gacha.selectedGachaNumber == Data.CHARACTER_MIKO)
							chara = Data.FIREBASE_EVENT_GACHA_MIKO;
						if (gacha.selectedGachaNumber == Data.CHARACTER_NINJA)
							chara = Data.FIREBASE_EVENT_GACHA_MIKO;
						if (gacha.selectedGachaNumber == Data.CHARACTER_MATIMUSUME)
							chara = Data.FIREBASE_EVENT_GACHA_MACHIMUSUME;
						if (gacha.selectedGachaNumber == Data.CHARACTER_HACHI)
							chara = Data.FIREBASE_EVENT_GACHA_HACHI;
						FirebaseAnalyticsManager.Instance.LogEvent (chara);
					}
					// 結果へ.
					ShowAdsBanner();
					SoundManager.Instance.PlaySe (SoundManager.SeName.SE_RESULT);
					goGachaResult.SetActive (true);
					goGachaStart.SetActive (!goGachaResult.activeSelf);
					goGachaCupsule.SetActive(!goGachaResult.activeSelf);
					gacha.clear ();
					goGachaCupsule.transform.localPosition = new Vector3 (goGachaCupsule.transform.localPosition.x, gacha.y, goGachaCupsule.transform.localPosition.z);
				}

				return;
			}
			// 回転処理.
			if (gacha.gachaMode % 2 == 1) {
				gacha.timer = Time.time;
				gacha.gachabarAngle = 0;
				gacha.gachabarSet = (20 + UnityEngine.Random.Range (10, 30));
				gacha.gachaMode++;
			}
			if (gacha.gachaMode % 2 == 0) {
				if (Time.time >= gacha.timer + UnityEngine.Random.Range (0.5f, 1.25f)) {
					SoundManager.Instance.PlaySe (SoundManager.SeName.SE_GACHA);
					gacha.gachabarAngle += 10;
					if (gacha.gachabarAngle >= gacha.gachabarSet) {
						gacha.gachaMode++;
					}
					goGachaBar.transform.localEulerAngles += new Vector3 (0, 0, -(gacha.gachabarAngle % 360));
				}
			}
		}
	}

	// キャラクター選択でキャラクター表示.
	private void ShowCharacter()
	{
		for (int i = 0; i < Data.CHARACTER_MAX; i++) {
			if (MainManager.Instance.IsCharacter (i)) {
				goCharacter [i].GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			}
		}
	}

	// キャラクター名、日本語 or 英語.
	private void ChangeCharacterName()
	{
		for (int i = 0; i < Data.CHARACTER_MAX; i++) {
			goSelectCharacter.transform.Find ("Character/Chara" + i + "Name").GetComponent<Text> ().text = Language.sentence [Language.CHARANAME_SAMURAI + i];
		}
	}

	// チケット数を更新.
	private void ReflashGachaTicket()
	{
		goSelectCharacterGachaTicketText.GetComponent<Text> ().text = Language.sentence [Language.GACHA_RESULT_GACHATICKETTEXT];
		goSelectCharacterGachaTicket.GetComponent<Text> ().text = MainManager.Instance.gachaTicket.ToString();
		goGachaResultGachaTicketText.GetComponent<Text> ().text = Language.sentence [Language.GACHA_RESULT_GACHATICKETTEXT];
		goGachaResultGachaTicket.GetComponent<Text> ().text = MainManager.Instance.gachaTicket.ToString();
	}

	// ガチャチケットを獲得するための動画視聴.
	private void ShowAdsMovie()
	{
		UnityEngine.Random.InitState ((int)Time.time);
		MainManager.Instance.ShowInterstitial (() => {
			MainManager.Instance.gachaTicket += 5;
			ReflashGachaTicket();
			FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_EVENT_GACHA_ADS);
		});
	}

	// バナーが出たらガチャチケット１枚.
	private void ShowAdsBanner()
	{
		UnityEngine.Random.InitState ((int)Time.time);
		if (UnityEngine.Random.Range (0, 100) < 20) {
			MainManager.Instance.ShowInterstitialNoMovie ();
		}
	}

	// 起動時に出すポップアップインフォ.
	private void SetInformation()
	{
		if (MainManager.Instance.IsInformation ()) {
			if (Language.sentence == Language.sentenceEn) {
				goInformation.transform.Find ("Title").GetComponent<Text> ().fontSize = 32;
				goInformation.transform.Find ("Explanation").GetComponent<Text> ().fontSize = 30;
			} else {
				goInformation.transform.Find ("Title").GetComponent<Text> ().fontSize = 30;
				goInformation.transform.Find ("Explanation").GetComponent<Text> ().fontSize = 26;
			}
			goInformation.transform.Find ("Title").GetComponent<Text> ().text = Language.sentence [Language.INFORMATION_TITLE];
			goInformation.transform.Find ("Explanation").GetComponent<Text> ().text = Language.sentence [Language.INFORMATION_EXPLANATION];
			goInformation.SetActive (true);
			FirebaseAnalyticsManager.Instance.LogEvent (Data.FIREBASE_SCREEN_INFORMATION);
		}
	}

	// ガチャチケットを獲得した時に出すポップアップ.
	public void SetLoginBonus()
	{
		if (loginBonus.prevGachaTicket < MainManager.Instance.gachaTicket) {
			goMenuLoginBonus.transform.Find ("Text").GetComponent<Text> ().text = Language.sentence [Language.LOGIN_BONUS_TEXT];
			goMenuLoginBonus.SetActive (true);
			loginBonus.prevGachaTicket = MainManager.Instance.gachaTicket;
			FirebaseAnalyticsManager.Instance.LogScreen (Data.FIREBASE_SCREEN_BONUS);
		}
	}

	// ポップアップインフォを非表示.
	private void OnButtonInformationClose()
	{
		MainManager.Instance.SaveInformation ();
		goInformation.SetActive (false);
	}

	// ログインボーナスのポップアップを非表示.
	private void OnButtonLoginBonusClose()
	{
		goMenuLoginBonus.SetActive (false);
	}

	private void CheckBackKey ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			switch (this.state) {
			case State.Menu:
				if (goGacha.activeSelf) {
					OnGachaResultBackButton ();
				} else if (goCaution.activeSelf) {
					OnMenuButtonCaution (false);
				} else if (goSelectCharacter.activeSelf) {
					OnButton (State.Menu, false);
				} else {
					OnButton (State.End);
				}
				break;
			default:
				OnButton (State.Menu, false);
				break;
			}
		}
	}




// ========================================
// 以下、ランキング関連.
// ========================================
	private void OnLogin()
	{
		string id = goLogin.transform.Find("Id/InputField/Text").GetComponent<Text>().text;
		string password = goLogin.transform.Find("Password/InputField/Text").GetComponent<Text>().text;

		//Debug mode.
		//id = "Cookie";
		//password = "1111";

		bool flg = ErrorUserAuth(goLoginDescription, id, password);
		if (flg) {
			return;
		}

		ranking.Init ();
		MainManager.Instance.loginInfo.Reset ();

		SetConnecting (true);
		user.logIn (id, password);
	}
	
	// ログアウト.
	// 未使用予定.
	private void OnLogout()
	{
		SetPreparationLogout ();
		user.logOut ();
	}

	// 登録後の表示.
	private void OnSignup()
	{
		goSignup.SetActive (true);
		goLogin.SetActive (false);

		goSignup.transform.Find ("Description").gameObject.SetActive(false);
		goRankingConnecting.GetComponent<Text> ().text = Language.sentence [Language.SIGNUP];
		goRankingConnecting.SetActive(false);

		goSignup.transform.Find ("UserName").GetComponent<Text> ().text = user.userName;
		goSignup.transform.Find ("UserPassword").GetComponent<Text> ().text = user.password;
	}

	// 登録処理.
	private void OnButtonRegist()
	{
		string name = goSignup.transform.Find("Id/InputField/Text").GetComponent<Text>().text;
		string password = goSignup.transform.Find("Password/InputField/Text").GetComponent<Text>().text;
		string rePassword = goSignup.transform.Find("RePassword/InputField/Text").GetComponent<Text>().text;
		string mail = null;	// No nessesary this time.

		name = AutoGenerateUserName ();
		password = AutoGenerateUserPassword ();
		rePassword = password;

		//Debug mode.
		//id = "Cookie";
		//password = "1111";
		//rePassword = "1111";

		bool flg = ErrorUserAuth(goSignupDescription, name, password, rePassword);
		if (flg) {
			return;
		}

		ranking.Init ();
		MainManager.Instance.loginInfo.Reset ();

		SetConnecting (false);
		user.signUp (name, mail, password);
	}

	// ランキング表示へ.
	private void OnRanking()
	{
		if (IsOffline ()) {
			return;
		}

		SetRanking (false);

		string textRank = null;
		string textName = null;
		string textScore = null;

		// No.1 - 10をセット
		int max = MainManager.Instance.loginInfo.GetRankMax ();
		int count = (max > (HighScore.RANKING_MAX >> 1)) ? HighScore.RANKING_MAX >> 1 : max;
		for (int i = 0; i < count; i++) {
			textRank += "No." + (i + 1) + "\n";
			textName += MainManager.Instance.loginInfo.GetUserName (i) + "\n";
			textScore += MainManager.Instance.loginInfo.GetUserScore (i) + "\n";
		}

		string text = null;
		text = "My Rank.\n";
		text += "No." + MainManager.Instance.loginInfo.GetRank () + " " + MainManager.Instance.loginInfo.GetName () + " " + MainManager.Instance.loginInfo.GetScore () + "点\n";

		goRankingPage.SetActive (true);
		goRankingMe.SetActive (true);
		goRankingPage.transform.Find ("Page0/Rank").GetComponent<Text> ().text = textRank;
		goRankingPage.transform.Find ("Page0/Name").GetComponent<Text> ().text = textName;
		goRankingPage.transform.Find ("Page0/Score").GetComponent<Text> ().text = textScore;
		goRankingMe.GetComponent<Text> ().text = text;
		if (Language.sentence == Language.sentenceEn) {
			goRankingPage.transform.Find ("Page0").GetComponent<RectTransform> ().sizeDelta = new Vector2 (925, 1000);
		}

		// No.11 - 20までをセット
		count = max - (HighScore.RANKING_MAX >> 1);
		if (max >= HighScore.RANKING_MAX)
			count = HighScore.RANKING_MAX >> 1;
		//Debug.Log (max+", count = "+count);
		if (count > 0) {
			SetRanking (true);

			textRank = null;
			textName = null;
			textScore = null;
			// For debug.
			/*for (int i = 0; i < 10; i++) {
							textRank += "No."+(10+i+1)+"\n";
							textName += "user name "+i+"\n";
							textScore += ((i+1)*1000)+"\n";
						}*/
			for (int i = 0; i < count; i++) {
				textRank += "No." + (10 + i + 1) + "\n";
				textName += MainManager.Instance.loginInfo.GetUserName ((HighScore.RANKING_MAX >> 1) + i) + "\n";
				textScore += MainManager.Instance.loginInfo.GetUserScore ((HighScore.RANKING_MAX >> 1) + i) + "\n";
			}

			goRankingPage.transform.Find ("Page1/Rank").GetComponent<Text> ().text = textRank;
			goRankingPage.transform.Find ("Page1/Name").GetComponent<Text> ().text = textName;
			goRankingPage.transform.Find ("Page1/Score").GetComponent<Text> ().text = textScore;

			catalog.Init (RANKING_PAGE_NUM);
			goCatalogPage = goRankingPage;
			goCatalogPoint = goRankingPoint;
			goCatalogArrowRight = goRankingArrowRight;
			goCatalogArrowLeft = goRankingArrowLeft;
		}

		goRankingButtonChangeUserName.SetActive (true);
		goRankingButtonBack.SetActive (true);
		goRankingConnecting.SetActive (false);
		goSignup.SetActive (false);
	}

	private void SetRanking( bool flg )
	{
		goRankingPoint.SetActive (flg);
		goRankingSwipe.SetActive (flg);
		goRankingArrowLeft.SetActive (flg);
		goRankingArrowRight.SetActive (flg);
		//goRankingButtonLogout.SetActive (flg);
	}

	private void OnChange()
	{
		goChangeResultUserName.SetActive (false);
		goRankingPage.SetActive (false);
		goRankingMe.SetActive (false);
		goRankingButtonChangeUserName.SetActive (false);
		goChagne.SetActive (true);
		SetRanking (false);
	}

	private void OnChangeUserName()
	{
		NCMBUser.CurrentUser.UserName = goChagne.transform.Find("Id/InputField/Text").GetComponent<Text>().text;
		NCMBUser.CurrentUser.SignUpAsync ( (NCMBException e) => {
			if( e == null ){
				PlayerPrefs.SetString (Data.LOGIN_NAME, goChagne.transform.Find("Id/InputField/Text").GetComponent<Text>().text);
				goChangeResultUserName.GetComponent<Text> ().color = new Color (1, 1, 1, 1);
				goChangeResultUserName.GetComponent<Text>().text = Language.sentence [Language.RANKING_NAME_CHANGE_CORRECT];
				goChangeResultUserName.SetActive(true);
				goSignupButtonRanking.SetActive (false);
				goRankingButtonBack.SetActive(true);
			}
			else{
				// Error.
				goChangeResultUserName.GetComponent<Text> ().color = new Color (1, 0, 0, 0);
				goChangeResultUserName.GetComponent<Text>().text = Language.sentence [Language.RANKING_NAME_CHANGE_INCORRECT];
				goChangeResultUserName.SetActive(true);
				goRankingButtonBack.SetActive(true);
			}
		});
	}

	// 未対応.
	private void OnChangeUserPassword()
	{
	}

	private bool isLogin;
	private bool isLogout;
	private bool isConnecting;
	private void SetConnecting( bool isLogin )
	{
		this.isLogin = isLogin;
		this.isLogout = false;
		this.isConnecting = true;
		goRankingConnecting.SetActive (true);
		goRankingButtonChangeUserName.SetActive (false);
		goRankingButtonBack.SetActive (false);
		goLogin.SetActive (false);
		goSignup.SetActive (false);
		goRankingConnecting.GetComponent<Text> ().color = new Color (1, 1, 1, 1);
		connectingText = Language.sentence [Language.CONNECTING];
	}

	private void SetPreparationLogout()
	{
		this.isLogin = false;
		this.isLogout = true;
		this.isConnecting = true;
		goRankingConnecting.SetActive (true);
		goRankingMe.SetActive(false);
		goRankingPage.SetActive(false);
		goRankingPoint.SetActive(false);
		goRankingSwipe.SetActive(false);
		goRankingArrowLeft.SetActive(false);
		goRankingArrowRight.SetActive(false);
		goRankingButtonLogout.SetActive(false);
		goRankingButtonChangeUserName.SetActive (false);
		goRankingButtonBack.SetActive (false);
		goRankingConnecting.GetComponent<Text> ().color = new Color (1, 1, 1, 1);
		connectingText = Language.sentence [Language.CONNECTING];
	}

	private void Connecting()
	{
		if (IsOffline ()) {
			return;
		}

		// 接続中...
		if (goRankingConnecting.activeSelf) {
			// エラーコードなし
			if (user.errorCode == null) {
				// 正常ログアウト
				if (MainManager.Instance.isLogin == false && isLogout ) {
					isConnecting = false;
					this.isLogout = false;
					goRankingConnecting.GetComponent<Text> ().text = Language.sentence [Language.LOGOUT];
					goRankingButtonBack.SetActive (true);
				}
				// 未ログイン = 接続中アニメ
				if (isConnecting) {
					float i = Time.time % 3;
					goRankingConnecting.GetComponent<Text> ().text = connectingText.Substring (0, connectingText.Length - 2 + (int)i);
				}
				// 正常ログイン
				if (Logined()) {
					isConnecting = false;
					if (isAutoLogin) {
						if (Ranking.State.Finish == ranking.state) {
							OnRanking ();
							goRankingConnecting.SetActive (false);
						}
					}
					else
						OnSignup ();
				}
			}
			// ログイン、サインインに失敗
			if (user.errorCode != null) {
				// テキストの設定
				if (!goRankingButtonBack.activeSelf) {
					goRankingConnecting.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
					// 名前とパスワードが不一致
					if (user.errorCode == NCMBException.INCORRECT_PASSWORD ||
						user.errorCode == NCMBException.INCORRECT_HEADER   ||
						user.errorCode == NCMBException.OAUTH_ERROR
					) {
						goRankingConnecting.GetComponent<Text> ().text = Language.sentence [Language.ERROR_LOGIN_INCORRECT];
					}
					// すでに名前が使われている.
					if (user.errorCode == NCMBException.DUPPLICATION_ERROR) {
						goRankingConnecting.GetComponent<Text> ().text = Language.sentence [Language.ERROR_SIGNUP_INCORRECT];
					}
					// 使用制限（APIコール数、PUSH通知数、ストレージ容量）超過エラーです.
					if (user.errorCode == NCMBException.DUPPLICATION_ERROR) {
						goRankingConnecting.GetComponent<Text> ().text = Language.sentence [Language.REQUEST_OVERLOAD];
					}
					// 戻るボタンを表示
					goRankingButtonBack.SetActive (true);
				}
			}
		}
	}
		
	private bool Logined()
	{
		return (user.currentPlayer () != null && !isLogout);
	}

	private bool isAutoLogin;
	private void AutoLogin()
	{
		if (IsOffline ()) {
			return;
		}

		// Read them from strage.
		string name = PlayerPrefs.GetString (Data.LOGIN_NAME);
		string password = PlayerPrefs.GetString (Data.LOGIN_PASSWORD);

		isAutoLogin = false;
		//Debug.Log (name + ", " + password);
		if (!(name.Equals ("") && password.Equals (""))) {
			SetConnecting (true);
			user.logIn (name, password);
			isAutoLogin = true;
		}
	}

	private bool IsOffline()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			return true;
		}

		return false;
	}

	// user name.
	private string AutoGenerateUserName()
	{
		int name = int.Parse (DateTime.Now.ToString ("MMddHHmmss"));
		name ^= EXCHANGE_CODE;
		return name.ToString();
	}

	// user password.
	private string AutoGenerateUserPassword()
	{
		int pass = int.Parse (DateTime.Now.ToString ("MMddHHmmss"));
		pass ^= EXCHANGE_CODE;
		return pass.ToString();
	}


	private bool ErrorUserAuth(GameObject obj, string id, string password, string repassword = null)
	{
		bool error = false;
		string text = null;

		// Incorrect name.
		if (id.Equals ("") || id.Length >= 14) {
			error = true;
			text = Language.sentence [Language.ERROR_NONAME];
		// Incorrect password.
		} else if (password.Equals ("")) {
			error = true;
			text = Language.sentence [Language.ERROR_NOPASSWORD];
		}

		// Incorrect password and confirmatioin password.
		if (repassword != null) {
			if (!password.Equals (repassword)) {
				error = true;
				text = Language.sentence [Language.ERROR_SIGNUP_NOMATCH_PASSWORD];
			}
		}

		// Draw error message.
		if (error) {
			obj.SetActive (true);
			obj.GetComponent<Text> ().text = text;
			goRankingButtonBack.SetActive (true);
		}

		return error;
	}


	private void HighScoreData()
	{
		switch (ranking.state) {
		case Ranking.State.Fetch:
			{
				ranking.Fetch (user.currentPlayer());
				ranking.Next (Ranking.State.Save);
			}
			break;
		case Ranking.State.Save:
			{
				int score = 0;
				int stage = 0;

				if (PlayerPrefs.HasKey (Data.RECORD_SCORE_HIGH)) {
					score = PlayerPrefs.GetInt (Data.RECORD_SCORE_HIGH);
					int present = PlayerPrefs.GetInt (Data.RECORD_SCORE);
					if (present < 10000)
						score = present;
				}

				ranking.Save (score, stage);
				ranking.Next (Ranking.State.Goto);
			}
			break;
		case Ranking.State.Goto:
			ranking.state = Ranking.State.FetchRank;
			ranking.debug ();
			break;
		}

	}


	private void RankingData()
	{
		switch (ranking.state) {
		case Ranking.State.FetchRank:
			{
				ranking.FetchRank ();
				ranking.NextLeaderBoard (Ranking.State.TopRank);
			}
			break;
		case Ranking.State.TopRank:
			{
				ranking.FetchTopRank ();
				ranking.NextLeaderBoard (Ranking.State.Finish);
			}
			break;
		case Ranking.State.Finish:
			ranking.debug ();
			break;
		}

	}


	public class UserAuth {

		public string userName;
		public string password;
		public string errorCode;
		private string currentPlayerName;

		// mobile backendに接続してログイン ------------------------

		public void logIn( string id, string pw ) {

			errorCode = null;
			NCMBUser.LogInAsync (id, pw, (NCMBException e) => {
				
				// 接続成功したら
				if( e == null ){
					currentPlayerName = id;
					userName = id;
					password = pw;
					MainManager.Instance.isLogin = true;
					MainManager.Instance.loginInfo.SetLoginInfo(userName,password);
					PlayerPrefs.SetString (Data.LOGIN_NAME, userName);
					PlayerPrefs.SetString (Data.LOGIN_PASSWORD, password);
					FirebaseAnalyticsManager.Instance.LogEvent(Data.FIREBASE_EVENT_RANKING_LOGIN);
				}
				else {
					errorCode = e.ErrorCode;
				}
			});
		}

		// mobile backendに接続して新規会員登録 ------------------------

		public void signUp( string id, string mail, string pw ) {

			NCMBUser user = new NCMBUser();
			user.UserName = id;
			user.Email    = mail;
			user.Password = pw;
			errorCode = null;
			user.SignUpAsync((NCMBException e) => { 

				if( e == null ){
					user.ACL = new NCMBACL(NCMBUser.CurrentUser.ObjectId);
					user.SaveAsync();
					currentPlayerName = id;
					userName = id;
					password = pw;
					MainManager.Instance.isLogin = true;
					MainManager.Instance.loginInfo.SetLoginInfo(userName,password);
					PlayerPrefs.SetString (Data.LOGIN_NAME, userName);
					PlayerPrefs.SetString (Data.LOGIN_PASSWORD, password);
					FirebaseAnalyticsManager.Instance.LogEvent(Data.FIREBASE_EVENT_RANKING_SIGNIN);
					//Debug.Log ("### name = " + userName + ", pass = " + password);
				}
				else {
					errorCode = e.ErrorCode;
				}
			});
		}

		// mobile backendに接続してログアウト ------------------------

		public void logOut() {

			errorCode = null;
			NCMBUser.LogOutAsync ( (NCMBException e) => {
				if( e == null ){
					currentPlayerName = null;
					MainManager.Instance.isLogin = false;
				}
			});
		}

		// 現在のプレイヤー名を返す --------------------
		public string currentPlayer()
		{
			return currentPlayerName;
		}
	}
}
