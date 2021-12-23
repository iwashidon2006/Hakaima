using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseAnalyticsManager : MonoBehaviour {

	private static FirebaseAnalyticsManager instance;
	public static FirebaseAnalyticsManager Instance {
		get {
			instance = instance ?? GameObject.FindObjectOfType<FirebaseAnalyticsManager> ();
			return instance;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void LogEvent(string log)
	{
		//Debug.Log (log);

		if (Application.internetReachability == NetworkReachability.NotReachable)
			return;

		#if !UNITY_EDITOR
		//Firebase.Analytics.FirebaseAnalytics.LogEvent(log);
		#endif
	}

	public void LogScreen(string log)
	{
		//Debug.Log (log);

		if (Application.internetReachability == NetworkReachability.NotReachable)
			return;

		#if !UNITY_EDITOR
		//Firebase.Analytics.FirebaseAnalytics.LogEvent(log);
		#endif
	}

}
