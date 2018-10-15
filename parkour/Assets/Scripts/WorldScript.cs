using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour {
	public GameObject rwall, lwall, roof, cFloor, rFloor, lFloor, rEmptySpace, lEmptySpace, tables, lockers;

	public int worldLenght;
	// Use this for initialization
	void Start () {
		worldLenght = 60;
		for (int i = 0; i < worldLenght; i++){
			Instantiate(cFloor, new Vector3 (0, 0, i * 30f), Quaternion.identity);
			Instantiate(roof, new Vector3 (roof.transform.position.x, roof.transform.position.y, i * 30f), roof.transform.rotation);
			Instantiate(lwall, new Vector3 (lwall.transform.position.x, lwall.transform.position.y, i * 30f), lwall.transform.rotation);
			Instantiate(rwall, new Vector3 (rwall.transform.position.x, rwall.transform.position.y, i * 30f), rwall.transform.rotation);
			if (rFloor.transform.position.z > rEmptySpace.transform.position.z - 35f && rFloor.transform.position.z < rEmptySpace.transform.position.z + 35f) {
			}
			else {	Instantiate(rFloor, new Vector3 (5, 0, i * 30f), Quaternion.identity);	}
			if (lFloor.transform.position.x == lEmptySpace.transform.position.x && lFloor.transform.position.z > lEmptySpace.transform.position.z - 30f &&
			lFloor.transform.position.z < lEmptySpace.transform.position.z + 30f) {
			}
			else {	Instantiate(lFloor, new Vector3 (-5, 0, i * 30f), Quaternion.identity);	}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
