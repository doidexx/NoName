using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerS2 : MonoBehaviour {

	private Rigidbody playerRB;
	public GameObject[] prefabs;
	private GameObject clone;
	private Animator playerAni;
	public float speed, jumpSpeed, timer, jumpT, grav, l, c, r;
	private int jumpNum;
	private bool sliding, jumping, falling, movingR, movingL, onFloor, land, leftT, rightT, centerT, canMove;
	// Use this for initialization
	void Start () {
		clone =  null;
		playerRB =  GetComponent<Rigidbody>();
		playerAni = GetComponent<Animator>();
		speed = 30f;
		jumpSpeed = 10f;
		grav = 16f;
		l = -5f;
		c = 0f;
		r = 5f;
		centerT = true;
	}
	
	// Update is called once per frame
	void Update () {
        ////////////////////////////////////Get Space to jump/////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Space) && onFloor && !jumping) {
			jumping = true;
		}
		if (jumping) {
			jumpT += Time.deltaTime;
			jumpNum = Random.Range(1, 20);
			if (jumpNum > 10) {
				jumpNum = 2;
			} else {
				jumpNum = 1;
			}
            playerAni.SetInteger("state", 2);
		}
		///////////////////////////////////Get S to slide/////////////////////////////////////////////////////
		if (Input.GetKeyDown(KeyCode.S) && onFloor) {
			sliding = true;
        }
		if (sliding) {
            timer += Time.deltaTime;
            playerAni.SetInteger("state", 3);
		}
		////////////////////////////////////Stopping Sliding and Jumping////////////////////////////////////
		if (timer >= 1f) {
            sliding = false;
            timer = 0;
        }
		if (jumpT >= 0.35f) {
            jumping = false;
            jumpT = 0;
		}
		/////////////////////////////////////Run if on the floor////////////////////////////////////////////
		if (!sliding && !jumping && onFloor) {
            playerAni.SetInteger("state", 0);
		}
		///////////////////////////////////Changing tracks/////////////////////////////////////////////////////
		if (Input.GetKeyDown(KeyCode.D) && !movingL) {
			movingR = true;
		}
        if (Input.GetKeyDown(KeyCode.A) & !movingR) {
            movingL = true;
        }
		////////////////////////////////////Stopping on the Center Track///////////////////////////////////////
		if ((movingR || movingL) && !canMove) {
			if (transform.position.x <= c + 0.5f && transform.position.x >= c - 0.5f) {
				centerT = true;
                movingR = false;
                movingL = false;
			}
		}
		if (centerT) {
            transform.position = new Vector3(c, transform.position.y, transform.position.z);
			canMove = true;
            rightT = false;
            leftT = false;
			Debug.Log("at center");
		}
		if (canMove) {
			centerT = false;
		}
		////////////////////////////////////Stopping on the Right Track/////////////////////////////////////////
		if (movingR && !rightT) {
			leftT = false;
			if (transform.position.x >= r) {
                movingR = false;
				rightT = true;
			}
		}
		if (rightT) {
            transform.position = new Vector3(r, transform.position.y, transform.position.z);
			canMove = false;
            centerT = false;
            Debug.Log("right");
        }
		////////////////////////////////////Stopping on the Left Track/////////////////////////////////////////
        if (movingL && !leftT) {
			rightT = false;
            if (transform.position.x <= l) {
                movingL = false;
                leftT = true;
            }
        }
        if (leftT) {
            transform.position = new Vector3(l, transform.position.y, transform.position.z);
            canMove = false;
            centerT = false;
            Debug.Log("left");
        }
		///////////////////////////////////Landing After Jumping///////////////////////////////////////////////
		if (land) {
            playerAni.SetInteger("state", 4);
			land = false;
		}
		////////////////////////////////////Mid air animation if falling of the map////////////////////////////
		if (falling) {
            playerAni.SetInteger("state", 5);
		}
        ////////////////////////////////////Map animation loader/////////////////////////////////////////////////
		if (WorldScript.load > 4) {
        	clone.transform.position = Vector3.Lerp(clone.transform.position, new Vector3(0, 0, clone.transform.position.z), 0.125f);
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////
	}

	void OnTriggerEnter(Collider other){
    	if (other.gameObject.tag == "floor") {
			land = true;
			falling = false;
        }
        if (other.gameObject.CompareTag("loader")) {
            if (WorldScript.load < WorldScript.lenght) {
                WorldScript.load++;
                clone = Instantiate(prefabs[Random.Range(1, 11)], new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), WorldScript.load * 44.3f), Quaternion.identity);
            }
            if (WorldScript.load == WorldScript.lenght) {
                clone = Instantiate(prefabs[12], new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), WorldScript.load * 44.3f), Quaternion.identity);
            }
			other.gameObject.SetActive(false);
        }
	}
	void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "floor") {
            onFloor = false;
            falling = true;
        }
	}

	void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "floor"){
            onFloor = true;
			falling = false;
        }
		// if (other.gameObject.tag == "empty") {
		// 	grav /= 2f;
		// }
	}
	
	void FixedUpdate() {
		//////////////////////////////////Moving Right/////////////////////////////////////////////////
        if (movingR) {
            playerRB.velocity += Vector3.right * 3;
        }
		//////////////////////////////////Moving Left/////////////////////////////////////////////////
        if (movingL) {
            playerRB.velocity += Vector3.left * 3;
        }
        ///////////////////////////////Constant Movement Forward///////////////////////////////////////
        playerRB.MovePosition(playerRB.transform.position += Vector3.forward * speed * Time.deltaTime);
        /////////////////////////////////////Gravity///////////////////////////////////////////////////
        playerRB.AddForce(Vector3.down * grav, ForceMode.Acceleration);
		if (jumping) {
            //playerRB.velocity += Vector3.up * jumpSpeed * Time.deltaTime;
			playerRB.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
		}
	}
}
