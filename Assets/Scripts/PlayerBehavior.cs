using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	public float speed, leftTrack, rightTrack, centerTrack, gravit, hSpeed, jumpTime, jumpTimeMax, jumpSpeed, slideSpeed, slideTime;
	public bool jump, movingLeft, movingRight, atCenter, slide, slideback, sliding, wallrun, wallToRun, fall, grabUp;
	public Quaternion initialR, slidingPosition, rSpeed;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {

		//floats
		speed = 30.0f;
		slideSpeed = 180.0f;
		hSpeed = 100f;
		rightTrack = 5.0f;
		leftTrack = -5.0f;
		centerTrack = 0.0f;
		jumpTime = 0.0f;
		gravit = 10.0f;
		slideTime = 0f;

		//booleans
		jump = false;
		movingLeft = false;
		movingRight = false;
		atCenter = false;
		slide = false;
		slideback = false;
		sliding = false;
		wallToRun = false;
		wallrun = false;
		fall = false;
		grabUp = false;

		//Positions and rotations
		initialR.eulerAngles = transform.rotation.eulerAngles;
		slidingPosition.eulerAngles = new Vector3 (-90, 0, 0);
		rSpeed.eulerAngles = new Vector3 (-1, 0, 0);

		//getting rigidbody
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerController();
		JumpController();
		WallRunController();
		SlideController();
		// Constant Movement forward
		transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
	}
	void OnTriggerEnter (Collider other) {
		// collide with obstacles
		if (other.gameObject.tag == "table" || other.gameObject.tag == "locker") {
			transform.position = new Vector3 (0, 3, -7.1f);
		}
		// wall to run
		if (other.gameObject.tag == "empty") {
			wallToRun = true;
		}
		// grabing back up
		
		if (other.gameObject.tag == "floor"){
			if (transform.position.y >-1) {
				grabUp = true;
			}
			if (grabUp){
				transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
			}
		}
	}

	void OnTriggerExit (Collider other) {
		// not on the wall
		if (other.gameObject.tag == "empty") {
			wallToRun = false;
		}
		
	}

	void PlayerController () {
		// Input.
		if (Input.GetKeyDown(KeyCode.A) && transform.position.x > leftTrack && !movingRight){
			movingLeft = true;
		}
		if (movingLeft) {
			transform.Translate(Vector3.left * (Time.deltaTime * hSpeed));
		}
		if (transform.position.x <= leftTrack) {
			movingLeft = false;
			transform.position = new Vector3(leftTrack, transform.position.y, transform.position.z);
		}
		if (Input.GetKeyDown(KeyCode.D) && transform.position.x < rightTrack && !movingLeft){
			movingRight = true;
		}
		if (movingRight){
			transform.Translate(Vector3.right * (Time.deltaTime * hSpeed));
		}
		if (transform.position.x >= rightTrack) {
			movingRight = false;
			transform.position = new Vector3(rightTrack, transform.position.y, transform.position.z);
		}
		if (transform.position.x < centerTrack + 1f && transform.position.x > centerTrack - 1f && (movingRight || movingLeft) && !atCenter) {
			transform.position = new Vector3(centerTrack, transform.position.y, transform.position.z);
			atCenter = true;
			movingRight = false;
			movingLeft = false;
		}
		if (atCenter && (movingLeft || movingRight) && (transform.position.x > centerTrack + 1f || transform.position.x < centerTrack - 1f)) {
			atCenter = false;
		}
	}

	void WallRunController() {
		// Wallrun
		if (transform.position.x == centerTrack) {
			wallToRun = false;
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			wallrun = true;
		}
		if (Input.GetKeyUp(KeyCode.W)) {
			wallrun = false;
		}
		// slowing down drag
		if (wallrun && wallToRun && !fall) {
			rb.drag = 5.0f;
		}
		//drag back to normal
		if (!wallrun || !wallToRun || fall) {
			rb.drag = 0f;
		}
		//dying by falling.
		if (transform.position.y <= -6){
			fall = true;
            transform.position = new Vector3(0, 3, -7.1f);
		} else {
			fall = false;
		}
	}

	void SlideController() {

		// //slide by rotation
		if (Input.GetKeyDown(KeyCode.S) && transform.rotation == initialR) {
			slide = true;
		}
		if (slide) {
			slideTime += Time.deltaTime;
			transform.Rotate(Vector3.left * Time.deltaTime * slideSpeed);
		}
		if (slideTime >= 0.5f && slide) {
			sliding = true;
			slide = false;
		}
		if (sliding){
			slideTime += Time.deltaTime;
			transform.rotation = slidingPosition;
		}
		if (slideTime >= 1.2f){
			slideback = true;
			sliding = false;
		}
		if (slideback) {
			slideTime += Time.deltaTime;
			transform.Rotate(Vector3.right * (Time.deltaTime * slideSpeed));
		}
		if (slideTime >=1.7f && slideback){
			slideTime = 0f;
			slideback = false;
			transform.rotation = initialR;
		}
	}

	void JumpController () {
		// Jump
		if (Input.GetKeyDown(KeyCode.Space) && jumpTime == 0 && transform.position.y == 3) {
			jump = true;
		}
		if (jump) {
			jumpTime += Time.deltaTime;
			transform.Translate(Vector3.up * (Time.deltaTime * jumpSpeed));
		}
		if (jumpTime >= jumpTimeMax) {
			jump = false;
			jumpTime = 0;
		}
	}
}
