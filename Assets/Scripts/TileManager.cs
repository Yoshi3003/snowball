using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	public GameObject currentTile;
	public List<GameObject> spawnTiles;

	private static TileManager instance;

	private Stack<GameObject> flatTilePool = new Stack<GameObject>();
	private const int FLAT_TILE = 0;

	public static TileManager Instance {
		get { 
			if (instance == null)
				instance = GameObject.FindObjectOfType<TileManager> ();
			return TileManager.instance; 
		}
	}

	public Stack<GameObject> FlatTilePool {
		get { return flatTilePool; }
		set { flatTilePool = value; }
	}

	// Use this for initialization
	void Start () {
		SpawnTiles (3);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CreateTiles() {
		Debug.Log ("Running out of recycled tiles, creating 10 new ones...");
		for (int i = 0; i < 10; i++) {
			flatTilePool.Push (Instantiate (spawnTiles [FLAT_TILE]));
			flatTilePool.Peek ().SetActive (false);
			flatTilePool.Peek ().name = "FlatTile";
		}
	}

	public void SpawnTiles(int spawnAmount) {
		if (flatTilePool.Count < spawnAmount) {
			CreateTiles ();
		}

		int randomIndex = Random.Range (0, spawnTiles.Count);
		switch (randomIndex) {
		case FLAT_TILE:
			{
				for (int i = 0; i < spawnAmount; i++) {
					GameObject tmp = flatTilePool.Pop ();
					tmp.SetActive (true);
					tmp.transform.position = currentTile.transform.Find ("AttachPoint").position;
					currentTile = tmp;
					currentTile.GetComponent<Tile> ().setTriggerSpawnTile (i == 1);
				}
				break;
			}
		default:
			break;
		}
	}
}
