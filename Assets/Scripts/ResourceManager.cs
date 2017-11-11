using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hakaima;


public class ResourceManager : MonoBehaviour
{

	public const int SPRITE_MULTI_TYPE		= 100;
	public const int SPRITE_MULTI_COMPASS	= 10;


	[SerializeField]
	private Sprite spriteTerrainSoil;
	[SerializeField]
	private Sprite spriteTerrainGrass;
	[SerializeField]
	private Sprite spriteTerrainMuddy;
	[SerializeField]
	private Sprite spriteTerrainPavement;
	[SerializeField]
	private Sprite spriteTerrainIce;
	[SerializeField]
	private Sprite spriteTerrainRiver0;
	[SerializeField]
	private Sprite spriteTerrainRiver1;
	[SerializeField]
	private Sprite spriteTerrainBridgeVertical;
	[SerializeField]
	private Sprite spriteTerrainBridgeHorizontal;
	[HideInInspector]
	public Dictionary<int, Sprite> spriteTerrainList;

	[SerializeField]
	private Sprite spriteObstacleTree;
	[SerializeField]
	private Sprite spriteObstacleStone;
	[SerializeField]
	private Sprite spriteObstacleTomb;
	[SerializeField]
	private Sprite spriteObstacleTombCollapse0;
	[SerializeField]
	private Sprite spriteObstacleTombCollapse1;
	[SerializeField]
	private Sprite spriteObstacleTombCollapseEnd;
	[SerializeField]
	private Sprite spriteObstacleTombPiece0;
	[SerializeField]
	private Sprite spriteObstacleTombPiece1;
	[SerializeField]
	private Sprite spriteObstacleTombPieceEnd;
	[SerializeField]
	private Sprite spriteObstacleCartRight;
	[SerializeField]
	private Sprite spriteObstacleCartLeft;
	[SerializeField]
	private Sprite spriteObstacleWell;
	[SerializeField]
	private Sprite spriteObstacleBale;
	[SerializeField]
	private Sprite spriteObstacleBathtub0;
	[SerializeField]
	private Sprite spriteObstacleBathtub1;
	[SerializeField]
	private Sprite spriteObstacleBathtub2;
	[SerializeField]
	private Sprite spriteObstacleBathtubCollapse0;
	[SerializeField]
	private Sprite spriteObstacleBathtubCollapse1;
	[SerializeField]
	private Sprite spriteObstacleBathtubCollapseEnd;
	[SerializeField]
	private Sprite spriteObstacleStockpile;
	[SerializeField]
	private Sprite spriteObstacleStockpileCollapse0;
	[SerializeField]
	private Sprite spriteObstacleStockpileCollapse1;
	[SerializeField]
	private Sprite spriteObstacleStockpileCollapseEnd;
	[SerializeField]
	private Sprite spriteObstacleSignboard;
	[SerializeField]
	private Sprite spriteObstacleTower0;
	[SerializeField]
	private Sprite spriteObstacleTower1;
	[SerializeField]
	private Sprite spriteObstacleTower2;
	[SerializeField]
	private Sprite spriteObstacleTower3;
	[SerializeField]
	private Sprite spriteObstacleTower4;
	[SerializeField]
	private Sprite spriteObstacleFallenTree;
	[SerializeField]
	private Sprite spriteObstacleStump;
	[SerializeField]
	private Sprite spriteObstacleBucket;
	[SerializeField]
	private Sprite spriteObstacleLantern;
	[SerializeField]
	private Sprite spriteObstacleStupaFence;
	[SerializeField]
	private Sprite spriteObstacleStupa;
	[SerializeField]
	private Sprite spriteObstacleLargeTreeRight;
	[SerializeField]
	private Sprite spriteObstacleLargeTreeLeft;
	[SerializeField]
	private Sprite spriteObstacleRubble;
	[SerializeField]
	private Sprite spriteObstacleRubbleOff;
	[SerializeField]
	private Sprite spriteObstaclePicket;
	[HideInInspector]
	public Dictionary<int, Sprite> spriteObstacleList;

	[SerializeField]
	private Sprite spriteHoleMiddle1;
	[SerializeField]
	private Sprite spriteHoleMiddle2;
	[SerializeField]
	private Sprite spriteHoleMiddle3;
	[SerializeField]
	private Sprite spriteHoleComplete;
	[HideInInspector]
	public Dictionary<int, Sprite> spriteHoleList;
	
	[HideInInspector]
	public Dictionary<int, Sprite> spritePlayerList;
	[HideInInspector]
	public Sprite spriteUpperPlayer;

	[SerializeField]
	private Sprite spriteEnemyPersonCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyPersonCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyGhostCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemySoulCompassRight0;
	[SerializeField]
	private Sprite spriteEnemySoulCompassRight1;
	[SerializeField]
	private Sprite spriteEnemySoulCompassRight2;
	[SerializeField]
	private Sprite spriteEnemySoulCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemySoulCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemySoulCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemySoulCompassTop0;
	[SerializeField]
	private Sprite spriteEnemySoulCompassTop1;
	[SerializeField]
	private Sprite spriteEnemySoulCompassTop2;
	[SerializeField]
	private Sprite spriteEnemySoulCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemySoulCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemySoulCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassRight0;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassRight1;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassRight2;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassTop0;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassTop1;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassTop2;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemySkeletonCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyMummyCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyShadowmanCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyGolemCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyGoblinCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyParasolCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyKappaCompassBottom2;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassRight0;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassRight1;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassRight2;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassLeft0;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassLeft1;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassLeft2;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassTop0;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassTop1;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassTop2;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassBottom0;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassBottom1;
	[SerializeField]
	private Sprite spriteEnemyTenguCompassBottom2;
	[HideInInspector]
	public Dictionary<int, Sprite> spriteEnemyList;
	
	[SerializeField]
	public Sprite spriteWeapon;

	[SerializeField]
	private Sprite spriteItemSandal;
	[SerializeField]
	private Sprite spriteItemHoe;
	[SerializeField]
	private Sprite spriteItemStone;
	[SerializeField]
	private Sprite spriteItemAmulet;
	[SerializeField]
	private Sprite spriteItemParasol;
	[HideInInspector]
	public Dictionary<int, Sprite> spriteItemList;
	
	[SerializeField]
	private Sprite spriteBonus0;
	[SerializeField]
	private Sprite spriteBonus1;
	[SerializeField]
	private Sprite spriteBonus2;
	[SerializeField]
	private Sprite spriteBonus3;
	[SerializeField]
	private Sprite spriteBonus4;
	[SerializeField]
	private Sprite spriteBonus5;
	[SerializeField]
	private Sprite spriteBonus6;
	[SerializeField]
	public Dictionary<int, Sprite> spriteBonusList;

	[SerializeField]
	public Sprite spriteUpperItemFrame;
	[SerializeField]
	public Sprite spriteUpperItemFrameSelect;

	[SerializeField]
	public Material materialReversal;
	[SerializeField]
	public Material materialReversalFall;
	[SerializeField]
	public Material materialReversalHide;


	private static ResourceManager instance;
	public static ResourceManager Instance {
		get {
			instance = instance ?? GameObject.FindObjectOfType<ResourceManager> ();
			return instance;
		}
	}



	private void Awake ()
	{
		spriteTerrainList = new Dictionary<int, Sprite> (){
			{(int)Hakaima.Terrain.Type.Soil					* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainSoil					},
			{(int)Hakaima.Terrain.Type.Grass				* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainGrass					},
			{(int)Hakaima.Terrain.Type.Muddy				* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainMuddy					},
			{(int)Hakaima.Terrain.Type.Pavement				* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainPavement				},
			{(int)Hakaima.Terrain.Type.Ice					* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainIce					},
			{(int)Hakaima.Terrain.Type.River				* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainRiver0					},
			{(int)Hakaima.Terrain.Type.River				* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_1,		spriteTerrainRiver1					},
			{(int)Hakaima.Terrain.Type.BridgeVertical		* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainBridgeVertical			},
			{(int)Hakaima.Terrain.Type.BridgeHorizontal		* SPRITE_MULTI_TYPE + Hakaima.Terrain.IMAGE_0,		spriteTerrainBridgeHorizontal		},
		};

		spriteObstacleList = new Dictionary<int, Sprite> (){
			{(int)Hakaima.Obstacle.Type.Tree					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTree					},
			{(int)Hakaima.Obstacle.Type.Stone					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleStone					},
			{(int)Hakaima.Obstacle.Type.Tomb					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTomb					},
			{(int)Hakaima.Obstacle.Type.TombCollapse			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTombCollapse0			},
			{(int)Hakaima.Obstacle.Type.TombCollapse			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_1,		spriteObstacleTombCollapse1			},
			{(int)Hakaima.Obstacle.Type.TombCollapseEnd			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTombCollapseEnd		},
			{(int)Hakaima.Obstacle.Type.TombPiece				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTombPiece0			},
			{(int)Hakaima.Obstacle.Type.TombPiece				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_1,		spriteObstacleTombPiece1			},
			{(int)Hakaima.Obstacle.Type.TombPieceEnd			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTombPieceEnd			},
			{(int)Hakaima.Obstacle.Type.CartRight				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleCartRight				},
			{(int)Hakaima.Obstacle.Type.CartLeft				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleCartLeft				},
			{(int)Hakaima.Obstacle.Type.Well					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleWell					},
			{(int)Hakaima.Obstacle.Type.Bale					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleBale					},
			{(int)Hakaima.Obstacle.Type.Bathtub					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleBathtub0				},
			{(int)Hakaima.Obstacle.Type.Bathtub					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_1,		spriteObstacleBathtub1				},
			{(int)Hakaima.Obstacle.Type.Bathtub					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_2,		spriteObstacleBathtub2				},
			{(int)Hakaima.Obstacle.Type.BathtubCollapse			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleBathtubCollapse0		},
			{(int)Hakaima.Obstacle.Type.BathtubCollapse			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_1,		spriteObstacleBathtubCollapse1		},
			{(int)Hakaima.Obstacle.Type.BathtubCollapseEnd		* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleBathtubCollapseEnd	},
			{(int)Hakaima.Obstacle.Type.Stockpile				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleStockpile				},
			{(int)Hakaima.Obstacle.Type.StockpileCollapse		* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleStockpileCollapse0	},
			{(int)Hakaima.Obstacle.Type.StockpileCollapse		* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_1,		spriteObstacleStockpileCollapse1	},
			{(int)Hakaima.Obstacle.Type.StockpileCollapseEnd	* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleStockpileCollapseEnd	},
			{(int)Hakaima.Obstacle.Type.Signboard				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleSignboard				},
			{(int)Hakaima.Obstacle.Type.Tower					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTower0				},
			{(int)Hakaima.Obstacle.Type.Tower					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_1,		spriteObstacleTower1				},
			{(int)Hakaima.Obstacle.Type.Tower					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_2,		spriteObstacleTower2				},
			{(int)Hakaima.Obstacle.Type.Tower					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_3,		spriteObstacleTower3				},
			{(int)Hakaima.Obstacle.Type.Tower					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_4,		spriteObstacleTower4				},
			{(int)Hakaima.Obstacle.Type.FallenTree				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleFallenTree			},
			{(int)Hakaima.Obstacle.Type.Stump					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleStump					},
			{(int)Hakaima.Obstacle.Type.Bucket					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleBucket				},
			{(int)Hakaima.Obstacle.Type.Lantern					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleLantern				},
			{(int)Hakaima.Obstacle.Type.StupaFence				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleStupaFence			},
			{(int)Hakaima.Obstacle.Type.Stupa					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleStupa					},
			{(int)Hakaima.Obstacle.Type.LargeTreeRight			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleLargeTreeRight		},
			{(int)Hakaima.Obstacle.Type.LargeTreeLeft			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleLargeTreeLeft			},
			{(int)Hakaima.Obstacle.Type.Rubble					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleRubble				},
			{(int)Hakaima.Obstacle.Type.RubbleOff				* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleRubbleOff				},
			{(int)Hakaima.Obstacle.Type.Picket					* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstaclePicket				},
			{(int)Hakaima.Obstacle.Type.FallTombPiece			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTombPiece0			},
			{(int)Hakaima.Obstacle.Type.FallTombPiece			* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_1,		spriteObstacleTombPiece1			},
			{(int)Hakaima.Obstacle.Type.FallTombPieceEnd		* SPRITE_MULTI_TYPE + Hakaima.Obstacle.IMAGE_0,		spriteObstacleTombPieceEnd			},
		};

		spriteHoleList = new Dictionary<int, Sprite> (){
			{Hakaima.Hole.IMAGE_MIDDLE_1,		spriteHoleMiddle1	},
			{Hakaima.Hole.IMAGE_MIDDLE_2,		spriteHoleMiddle2	},
			{Hakaima.Hole.IMAGE_MIDDLE_3,		spriteHoleMiddle3	},
			{Hakaima.Hole.IMAGE_COMPLETE,		spriteHoleComplete	},
		};

		spriteEnemyList = new Dictionary<int, Sprite> ();
		SetEnemy (Enemy.Type.Person);
		SetEnemy (Enemy.Type.Ghost);
		SetEnemy (Enemy.Type.Soul);
		SetEnemy (Enemy.Type.Skeleton);
		SetEnemy (Enemy.Type.Mummy);
		SetEnemy (Enemy.Type.Shadowman);
		SetEnemy (Enemy.Type.Golem);
		SetEnemy (Enemy.Type.Goblin);
		SetEnemy (Enemy.Type.Parasol);
		SetEnemy (Enemy.Type.Kappa);
		SetEnemy (Enemy.Type.Tengu);

		spriteItemList = new Dictionary<int, Sprite> (){
			{(int)Item.Type.Sandal		* SPRITE_MULTI_TYPE,			spriteItemSandal	},
			{(int)Item.Type.Hoe			* SPRITE_MULTI_TYPE,			spriteItemHoe		},
			{(int)Item.Type.Stone		* SPRITE_MULTI_TYPE,			spriteItemStone		},
			{(int)Item.Type.Amulet		* SPRITE_MULTI_TYPE,			spriteItemAmulet	},
			{(int)Item.Type.Parasol		* SPRITE_MULTI_TYPE,			spriteItemParasol	},
		};
		
		spriteBonusList = new Dictionary<int, Sprite> (){
			{(int)Bonus.Type.Bonus0		* SPRITE_MULTI_TYPE,			spriteBonus0	},
			{(int)Bonus.Type.Bonus1		* SPRITE_MULTI_TYPE,			spriteBonus1	},
			{(int)Bonus.Type.Bonus2		* SPRITE_MULTI_TYPE,			spriteBonus2	},
			{(int)Bonus.Type.Bonus3		* SPRITE_MULTI_TYPE,			spriteBonus3	},
			{(int)Bonus.Type.Bonus4		* SPRITE_MULTI_TYPE,			spriteBonus4	},
			{(int)Bonus.Type.Bonus5		* SPRITE_MULTI_TYPE,			spriteBonus5	},
			{(int)Bonus.Type.Bonus6		* SPRITE_MULTI_TYPE,			spriteBonus6	},
		};
	}



	public void SetPlayer (int charaId)
	{
		spritePlayerList = new Dictionary<int, Sprite> (){
			{0					+ (int)Player.Compass.Right		* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_right_0",	charaId))},
			{0					+ (int)Player.Compass.Right		* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_right_1",	charaId))},
			{0					+ (int)Player.Compass.Right		* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_right_2",	charaId))},
			{0					+ (int)Player.Compass.Left		* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_left_0",		charaId))},
			{0					+ (int)Player.Compass.Left		* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_left_1",		charaId))},
			{0					+ (int)Player.Compass.Left		* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_left_2",		charaId))},
			{0					+ (int)Player.Compass.Top		* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_top_0",		charaId))},
			{0					+ (int)Player.Compass.Top		* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_top_1",		charaId))},
			{0					+ (int)Player.Compass.Top		* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_top_2",		charaId))},
			{0					+ (int)Player.Compass.Bottom	* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_bottom_0",	charaId))},
			{0					+ (int)Player.Compass.Bottom	* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_bottom_1",	charaId))},
			{0					+ (int)Player.Compass.Bottom	* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_bottom_2",	charaId))},
			
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Right		* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_right_0",	charaId))},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Right		* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_right_0",	charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Right		* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_right_0",	charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Left		* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_left_0",	charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Left		* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_left_0",	charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Left		* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_left_0",	charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Top		* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_top_0",		charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Top		* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_top_0",		charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Top		* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_top_0",		charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Bottom	* SPRITE_MULTI_COMPASS + Player.IMAGE_0,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_bottom_0",	charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Bottom	* SPRITE_MULTI_COMPASS + Player.IMAGE_1,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_bottom_0",	charaId))	},
			{SPRITE_MULTI_TYPE	+ (int)Player.Compass.Bottom	* SPRITE_MULTI_COMPASS + Player.IMAGE_2,	Resources.Load<Sprite> (string.Format ("Textures/player{0}_spin_bottom_0",	charaId))	},
		};

		spriteUpperPlayer = Resources.Load<Sprite> (string.Format ("Textures/upper_player{0}",	charaId));
	}



	public void SetEnemy (Enemy.Type enemyType)
	{
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Right		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_0, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_right_0",	enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Right		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_1, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_right_1",	enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Right		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_2, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_right_2",	enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Left		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_0, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_left_0",		enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Left		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_1, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_left_1",		enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Left		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_2, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_left_2",		enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Top		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_0, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_top_0",		enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Top		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_1, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_top_1",		enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Top		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_2, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_top_2",		enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Bottom		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_0, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_bottom_0",	enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Bottom		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_1, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_bottom_1",	enemyType.ToString ().ToLower ())));
		spriteEnemyList.Add ((int)enemyType * SPRITE_MULTI_TYPE + (int)Enemy.Compass.Bottom		* SPRITE_MULTI_COMPASS + Enemy.IMAGE_2, Resources.Load<Sprite> (string.Format ("Textures/enemy_{0}_bottom_2",	enemyType.ToString ().ToLower ())));
	}
	
}
