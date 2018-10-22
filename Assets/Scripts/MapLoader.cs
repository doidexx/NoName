using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour {

	public GameObject[] prefabs;
    public bool loading;
    public Vector3 smooth;
	// Use this for initialization
    void Start(){
        
    }
	
    void FixedUpdate () {
        
        if (loading) {
        }
    }
	// Update is called once per frame
	private void OnTriggerEnter (Collider other)
    {
        Debug.Log("On Trigger Enter");
        if (other.gameObject.name.Equals("Player") && WorldScript.load < 20){
			WorldScript.load++;
            Instantiate(prefabs[Random.Range(1,11)], new Vector3(0, -20, WorldScript.load * 44.5f), Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (WorldScript.load == 20) {
            Instantiate(prefabs[12], new Vector3( 0, 0, WorldScript.load * 44.5f), Quaternion.identity);
        }
    }
}
