using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private bool triggerSpawn;

	// Use this for initialization
	void Start () {
		triggerSpawn = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			TileManager.Instance.SpawnTile ();
		}
	}
}
