using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * NICTサーバーの時間を見る
 * 日にちをまたいでいればログインボーナスのチャンス
 * ログインボーナスもらうまで何度もトライできる.
 */ 

public class LoginBonus : MonoBehaviour {

	[HideInInspector]
	public int prevGachaTicket { get; set; }	// メニューのポップアップで表示する獲得した数をみるためのもの.
	[HideInInspector]
	public double loginTime { get; set; }
	[HideInInspector]
	public bool isLoginBonus { get; set; }

	[Serializable]
	public class Time
	{
		public double st;
		public DateTime nowTime;
	}

	public static DateTime UnixTimeToDateTime(double unixTime){
		DateTime dt =
			new DateTime (1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		dt = dt.AddSeconds (unixTime);//UNIXTIMEは、1970年からの経過秒であるため
		dt = dt.AddHours(9);//NICTは＋９時間で日本標準時となるため 
		return dt;//
	}

	//NICTサーバから、UNIXタイムのJSONをGETしてくる
	public IEnumerator GetLoginBonus()
	{
		string url = "https://ntp-a1.nict.go.jp/cgi-bin/json";
		WWW www = new WWW(url);
		yield return www;

		if (www.error == null) {

			Time time = JsonUtility.FromJson<Time> (www.text);

			time.nowTime = UnixTimeToDateTime (time.st);
			//Debug.Log("サーバ時間＝"+time.nowTime+" / PC時間＝"+DateTime.Now);

			int prevDay = UnixTimeToDateTime (loginTime).Day;
			int prevMonth = UnixTimeToDateTime (loginTime).Month;
			int prevYear = UnixTimeToDateTime (loginTime).Year;
			int PrevHour = time.nowTime.Hour;
			int PrevMin = time.nowTime.Minute;
			int PrevSec = time.nowTime.Second;

			// For Debug.
			//prevDay = 30;
			//prevMonth = 10;
			//prevYear = 2016;

			//prevDay = 18;
			//prevMonth = 11;
			//prevYear = 2017;

			prevGachaTicket = MainManager.Instance.gachaTicket;

			// ログインボーナス.
			if (UnityEngine.Random.Range (0, 100) < 20) {
				// 20%の確率でログインボーナス.
				if (time.nowTime.Day > prevDay) {
					obtainGachaTicket (2);
					//Debug.Log ("Login Bonus Day");
				} else {
					if (time.nowTime.Month > prevMonth) {
						obtainGachaTicket (2);
						//Debug.Log ("Login Bonus Month");
					} else {
						if (time.nowTime.Year > prevYear) {
							obtainGachaTicket (2);
							//Debug.Log ("Login Bonus Year");
						}
					}
				}

				MainManager.Instance.transform.Find ("TitleManager(Clone)").GetComponent<TitleManager> ().SendMessage ("SetLoginBonus");

				// ログインボーナスの時間を保存.
				loginTime = time.st;
				PlayerPrefs.SetString (Data.LOGIN_TIME, loginTime.ToString());
			}
		} else {
			loginTime = 0;
		}
	}

	public void obtainGachaTicket(int number)
	{
		MainManager.Instance.gachaTicket += number;
		if (MainManager.Instance.gachaTicket > 99)
			MainManager.Instance.gachaTicket = 99;
		PlayerPrefs.SetInt (Data.RECORD_GACHATICKET, MainManager.Instance.gachaTicket);
	}

	public void LoadLoginTime()
	{
		loginTime = 0;
		if (PlayerPrefs.HasKey (Data.LOGIN_TIME)) {
			loginTime = double.Parse (PlayerPrefs.GetString (Data.LOGIN_TIME));
		}
	}
}
