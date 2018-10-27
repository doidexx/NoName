using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	public Transform target;
	public float offsetZ, offsetY;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Camera follows the player
		transform.position = Vector3.Lerp(transform.position ,new Vector3(0, offsetY, target.position.z - offsetZ), 0.5f);
		transform.LookAt(target);
	}
}
