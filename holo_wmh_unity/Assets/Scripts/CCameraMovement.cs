using UnityEngine;
using System.Collections;

public class CCameraMovement : MonoBehaviour {

	public float RotateSpeed;
	Camera MainCam;

	// Use this for initialization
	void Start () {
		MainCam = GetComponent<Camera> ();
		MainCam.transform.RotateAround(MainCam.transform.position,Vector3.right,45);
	}
	
	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		MainCam.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
		if (Input.GetMouseButton(1)) {
			float MouseHorizontal = Input.GetAxis("Mouse X");
			float MouseVertical = -1.0f * Input.GetAxis("Mouse Y");
			Vector3 CamAngle = MainCam.transform.eulerAngles;



			//MainCam.transform.Rotate (0,MouseHorizontal*Time.deltaTime*RotateSpeed,0,Space.World);
			//MainCam.transform.Rotate (MouseVertical*Time.deltaTime*RotateSpeed,0,0,Space.World);

			MainCam.transform.RotateAround(MainCam.transform.position, Vector3.down, MouseHorizontal*Time.deltaTime*RotateSpeed);

			//MainCam.transform.RotateAround(MainCam.transform.position,Vector3.right,MouseVertical*Time.deltaTime*RotateSpeed);
	

	


			//MainCam.transform.RotateAround(MainCam.transform.position, Vector3.down, 20);

			//MainCam.transform.eulerAngles = new Vector3(CamAngle.x, MainCam.transform.eulerAngles.y, MainCam.transform.eulerAngles.z);
			//MainCam.transform.eulerAngles.Set (45f,MainCam.transform.eulerAngles.y,MainCam.transform.eulerAngles.z);
			//print (string.Concat("before: ", CamAngle.x, "   after: ", MainCam.transform.eulerAngles.x));

		}
	}
}
