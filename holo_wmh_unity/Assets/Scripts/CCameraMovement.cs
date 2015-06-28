using UnityEngine;
using System.Collections;

public class CCameraMovement : MonoBehaviour {

	public float CamSpeed;
	GameObject CamTarget;
	GameObject MainCam;

	// Use this for initialization
	void Start () {
		CamTarget = GameObject.Find ("CameraTarget");
		MainCam = GameObject.Find ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 UpdatedDirection = MainCam.transform.forward;
		UpdatedDirection.y = 0;
		CamTarget.transform.forward = UpdatedDirection;

		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		CamTarget.transform.Translate(new Vector3(xAxisValue*Time.deltaTime*CamSpeed, 0.0f, zAxisValue*Time.deltaTime*CamSpeed));

	}
}
