using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {

	private GameObject obj;
	[HideInInspector]
	public int buttonNumber;

	// Use this for initialization
	void Start () {
		obj = GameObject.Find ("MainManager/TitleManager(Clone)");
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnSelectCharacter()
	{
		//string no = gameObject.name.Substring (gameObject.name - 1, gameObject.name.Length);
		//buttonNumber = int.Parse (no);
		obj.GetComponent<TitleManager> ().OnSelectCharacter (buttonNumber);
	}
}
