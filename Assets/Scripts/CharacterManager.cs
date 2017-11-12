using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hakaima;

public class CharacterManager : MonoBehaviour {

	private static CharacterManager instance;
	public static CharacterManager Instance {
		get {
			instance = instance ?? GameObject.FindObjectOfType<CharacterManager> ();
			return instance;
		}
	}

	[HideInInspector]
	public Dictionary<int, Sprite> spritePlayerGirl;
	[HideInInspector]
	public Dictionary<int, Sprite> spritePlayerNinja;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private string[] spriteFileName = {
		"right_0",
		"right_1",
		"right_2",
		"left_0",
		"left_1",
		"left_2",
		"top_0",
		"top_1",
		"top_2",
		"bottom_0",
		"bottom_1",
		"bottom_2",
		"spin_right_0",
		"spin_left_0",
		"spin_top_0",
		"spin_bottom_0",
	};

	public void RegistSpriteList()
	{
		spritePlayerGirl = new Dictionary<int, Sprite> ();
		spritePlayerNinja = new Dictionary<int, Sprite> ();
		string body = "Character/Girl/player_";
		for (int i = 0; i < spriteFileName.Length; i++) {
			Debug.Log (body + spriteFileName [i]);
			Sprite spt = Resources.Load<Sprite> (body + spriteFileName [i]) as Sprite;
			spritePlayerGirl.Add(i, spt);
		}

		body = "Character/Ninja/player_";
		for (int i = 0; i < spriteFileName.Length; i++) {
			Sprite spt = Resources.Load<Sprite> (body + spriteFileName [i]) as Sprite;
			spritePlayerNinja.Add(i, spt);
		}
	}

	public void ChangeCharacter()
	{
		RegistSpriteList ();
		ResourceManager.Instance.spritePlayerList.Clear ();

		bool flg = true;
		if (flg) {
			ResourceManager.Instance.spritePlayerList = new Dictionary<int, Sprite> (){
				{0					+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[0]	},
				{0					+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[1]	},
				{0					+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[2]	},
				{0					+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[3]	},
				{0					+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[4]	},
				{0					+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[5]	},
				{0					+ (int)Player.Compass.Top		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[6]	},
				{0					+ (int)Player.Compass.Top		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[7]	},
				{0					+ (int)Player.Compass.Top		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[8]	},
				{0					+ (int)Player.Compass.Bottom	* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[9]	},
				{0					+ (int)Player.Compass.Bottom	* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[10]	},
				{0					+ (int)Player.Compass.Bottom	* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[11]	},

				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[12]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[12]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Right		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[12]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[13]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[13]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Left		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[13]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Top		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[14]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Top		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[14]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Top		* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[14]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Bottom	* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_0,	spritePlayerGirl[15]		},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Bottom	* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_1,	spritePlayerGirl[15]	},
				{ResourceManager.SPRITE_MULTI_TYPE	+ (int)Player.Compass.Bottom	* ResourceManager.SPRITE_MULTI_COMPASS + Player.IMAGE_2,	spritePlayerGirl[15]	},
			};
			} else {
		}
	}
}
