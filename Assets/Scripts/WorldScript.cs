﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScript : MonoBehaviour {
    public GameObject[] prefabs;
    private GameObject clone;

	public int worldLenght;
    public static int load, lenght;
    public float distance;
	// Use this for initialization
	void Start () {
        clone = null;
        distance = 44.3f;
        lenght = 35;
        load = 4;
        for (int i = 0; i < 5; i++)
        {
            if (i < 3){
                Instantiate(prefabs[0], new Vector3(0, 0, i * distance), Quaternion.identity);
            }
            if (i == 3) {
                Instantiate(prefabs[11], new Vector3(0, 0, i * distance), Quaternion.identity);
            }
            if (i == 4){
                Instantiate(prefabs[Random.Range(1,11)], new Vector3(0, 0, i * distance), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
    void Update () {
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "loader" && other.gameObject.tag == "player") {
            for (int i = 0; i < 5; i++) {
                if (WorldScript.load < WorldScript.lenght) {
                    WorldScript.load++;
                    clone = Instantiate(prefabs[Random.Range(1, 11)], new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), WorldScript.load * 44.3f), Quaternion.identity);
                }
                if (WorldScript.load == WorldScript.lenght) {
                    clone = Instantiate(prefabs[12], new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), WorldScript.load * 44.3f), Quaternion.identity);
                }
            }
        }
    }
}
