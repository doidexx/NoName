using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		// Camera follows the player
		transform.position = Vector3.Lerp(target.position ,target.position + offset, 0.5f);
		transform.LookAt(target);
		
		// Camera stays at the center
		offset.x = target.position.x * -1;
	}
}
