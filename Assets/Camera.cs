using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject player;
    Quaternion rotation;
    private Vector3 offset;

    void Start () {
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void LateUpdate() {
        transform.position = player.transform.position + offset;
    }
}
