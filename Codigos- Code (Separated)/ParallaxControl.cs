using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxControl : MonoBehaviour {


	public Transform   background;
	public float       speed;

	private Transform cam;
	private  Vector3 previewCamPosition;

	// Use this for initialization
	void Start () {

		cam = Camera.main.transform;
		previewCamPosition = cam.position;
		
	}
	
	// Update is called once per frame
	void Update () {

		float parallaxx = previewCamPosition.x - cam.position.x;
		float bgTargetX = background.position.x + parallaxx ;
		Vector3 bgPosition = new Vector3 (  bgTargetX,background.position.y,background.position.z);
		background.position = Vector3.Lerp (background.position, bgPosition, speed * Time.deltaTime);

		previewCamPosition = cam.position;
		
	}
}
