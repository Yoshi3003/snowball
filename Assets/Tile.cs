using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private bool triggerSpawn;
	private const int RECYCLE_DELAY = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setTriggerSpawnTile(bool enable) {
		triggerSpawn = enable;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player" && triggerSpawn) {
			TileManager.Instance.SpawnTiles (3);
		}
	}

	void OnCollisionExit(Collision collision) {
		StartCoroutine (Recycle ());
	}

	IEnumerator Recycle() {
		yield return new WaitForSeconds (RECYCLE_DELAY);
		switch (gameObject.name) {
		case "FlatTile":
			{
				TileManager.Instance.FlatTilePool.Push (gameObject);
				gameObject.SetActive (false);
				break;
			}
		}
	}
}
