using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

	public GameObject currentTile;
	public List<GameObject> spawnTiles;

	private static TileManager instance;

	public static TileManager Instance {
		get { 
			if (instance == null)
				instance = GameObject.FindObjectOfType<TileManager> ();
			return TileManager.instance; 
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	GameObject GetNextTile() {
		return spawnTiles [Random.Range (0, spawnTiles.Count)];
	}

	public void SpawnTile() {
		currentTile = (GameObject) Instantiate (GetNextTile(), 
			currentTile.transform.Find ("AttachPoint").position, Quaternion.identity);
	}
}
