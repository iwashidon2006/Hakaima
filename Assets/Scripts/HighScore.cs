using NCMB;
using System.Collections.Generic;

namespace NCMB
{
	public class HighScore
	{
		public const int RANKING_MAX = 20;

		public int score   { get; set; }
		public int stage   { get; set; }
		public int time    { get; set; }
		public string name { get; private set; }
		public bool isCorrect { get; set; }
		public bool isCorrectFinish { get; set; }
		public string errorCode { get; set; }

		// コンストラクタ -----------------------------------
		public HighScore(int _score, int _stage, string _name)
		{
			score = _score;
			stage = _stage;
			name  = _name;
			Init ();
		}

		public void Init()
		{
			isCorrectFinish = false;
			isCorrect = false;
		}

		// ランキングで表示するために文字列を整形 -----------
		public string print()
		{
			return name + " " + score;
		}

		// サーバーにハイスコアを保存 -------------------------
		public void save()
		{
			isCorrect = true;
			errorCode = null;

			// データストアの「HighScore」クラスから、Nameをキーにして検索
			NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("HighScore");
			query.WhereEqualTo ("Name", name);
			query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {

				//検索成功したら    
				if (e == null) {
					objList[0]["Score"] = score;
					objList[0]["Stage"] = stage;
					objList[0].SaveAsync();
					isCorrectFinish = true;
					errorCode = null;
				}
				else {
					errorCode = e.ErrorCode;
				}
			});
		}

		// サーバーからハイスコアを取得  -----------------
		public void fetch()
		{
			isCorrect = true;
			errorCode = null;

			// データストアの「HighScore」クラスから、Nameをキーにして検索
			NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("HighScore");
			query.WhereEqualTo ("Name", name);
			query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {

				//検索成功したら  
				if (e == null) {
					// ハイスコアが未登録だったら
					if( objList.Count == 0 )
					{
						NCMBObject obj = new NCMBObject("HighScore");
						obj["Name"]  = name;
						obj["Score"] = 0;
						obj["Stage"] = 0;
						obj.SaveAsync();
					} 
					// ハイスコアが登録済みだったら
					else {
						score = System.Convert.ToInt32( objList[0]["Score"] ); 
						stage = System.Convert.ToInt32( objList[0]["Stage"] ); 
					}

					isCorrectFinish = true;
					errorCode = null;
				}
				else {
					errorCode = e.ErrorCode;
				}
			});
		}

	}

	public class LeaderBoard {

		public int currentRank = 0;
		public List<NCMB.HighScore> topRankers = null;
		public List<NCMB.HighScore> neighbors  = null;
		public bool isCorrect { get; set; }
		public bool isfetchRankFinish { get; set; }
		public bool isfetchTopRankersFinish { get; set; }
		public string errorCode { get; set; }

		public void Init()
		{
			isfetchRankFinish = false;
			isfetchTopRankersFinish = false;
			isCorrect = false;
		}

		// 現プレイヤーのハイスコアを受けとってランクを取得 ---------------
		public void fetchRank( int currentScore )
		{
			isCorrect = true;
			errorCode = null;
			isfetchRankFinish = false;

			// データスコアの「HighScore」から検索
			NCMBQuery<NCMBObject> rankQuery = new NCMBQuery<NCMBObject> ("HighScore");
			rankQuery.WhereGreaterThan("Score", currentScore);
			rankQuery.CountAsync((int count , NCMBException e )=>{

				if(e != null){
					//件数取得失敗
					errorCode = e.ErrorCode;
				}else{
					//件数取得成功
					currentRank = count+1; // 自分よりスコアが上の人がn人いたら自分はn+1位
				}
				isfetchRankFinish = true;
			});
		}

		// サーバーからTop10を取得 ---------------    
		public void fetchTopRankers()
		{
			isCorrect = true;
			errorCode = null;
			isfetchTopRankersFinish = false;

			// データストアの「HighScore」クラスから検索
			NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("HighScore");
			query.OrderByDescending ("Score");
			query.Limit = HighScore.RANKING_MAX;
			query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {

				if (e != null) {
					//検索失敗時の処理
					errorCode = e.ErrorCode;
				} else {
					//検索成功時の処理
					List<NCMB.HighScore> list = new List<NCMB.HighScore>();
					// 取得したレコードをHighScoreクラスとして保存
					foreach (NCMBObject obj in objList) {
						int    score = System.Convert.ToInt32(obj["Score"]);
						int    stage = System.Convert.ToInt32(obj["Stage"]);
						string name  = System.Convert.ToString(obj["Name"]);
						list.Add( new HighScore( score, stage, name ) );
					}
					topRankers = list;
				}
				isfetchTopRankersFinish = true;
			});
		}

		// サーバーからrankの前後2件を取得 ---------------
		public void fetchNeighbors()
		{
			neighbors = new List<NCMB.HighScore>();
			isCorrect = true;
			errorCode = null;

			// スキップする数を決める（ただし自分が1位か2位のときは調整する）
			int numSkip = currentRank - 3;
			if(numSkip < 0) numSkip = 0;

			// データストアの「HighScore」クラスから検索
			NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("HighScore");
			query.OrderByDescending ("Score");
			query.Skip  = numSkip;
			query.Limit = HighScore.RANKING_MAX;
			query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {

				if (e != null) {
					//検索失敗時の処理
					errorCode = e.ErrorCode;
				} else {
					//検索成功時の処理
					List<NCMB.HighScore> list = new List<NCMB.HighScore>();
					// 取得したレコードをHighScoreクラスとして保存
					foreach (NCMBObject obj in objList) {
						int    score = System.Convert.ToInt32(obj["Score"]);
						int    stage = System.Convert.ToInt32(obj["Stage"]);
						string name  = System.Convert.ToString(obj["Name"]);
						list.Add( new HighScore( score, stage, name ) );
					}
					neighbors = list;
				}
			});
		}
	}
}