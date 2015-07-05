using UnityEngine;
using System.Collections;

public class CTargetRaycast : MonoBehaviour {

	Camera MainCam;
	GameObject WayPointStart, WayPointEnd; // reserved for the way point display object
	GameObject ModelClone, PrevModel; // reserved for the model picekd up by the user

	bool isModel;

	// Use this for initialization
	void Start () {
		MainCam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Get the direction that the main camera is looking at, as well as the position of the camera
		// These info are used to cast a ray to detect what point/object the crosshair is aiming at
		Vector3 CamDirection = MainCam.transform.forward;
		Vector3 CamPosition = MainCam.transform.position;

		// Raycasting onto the terrain ground (which is on layer 8 by itself)
		RaycastHit TerrainHitInfo;
		Physics.Raycast (CamPosition, CamDirection, out TerrainHitInfo, Mathf.Infinity, 1<<8);

		// Raycasting onto any model object in the world
		RaycastHit ObjectHitInfo;
		Physics.Raycast (CamPosition, CamDirection, out ObjectHitInfo, Mathf.Infinity, 1<<9);


		// Resolving the point annotation procedure where an icon is placed on the terrain ground aimed by the crosshair
	    if (Input.GetKeyDown (KeyCode.Space)) {
			Vector3 WayPointPosition = new Vector3 (TerrainHitInfo.point.x, TerrainHitInfo.point.y+0.1f, TerrainHitInfo.point.z); // Position of the icon
			Vector3 WayPointScale = new Vector3(0.5f, 1f, 0.5f); // Scale the size of the icon

			if (WayPointStart == null && WayPointEnd == null) { // Put down the start icon
				MakeWayPoint (WayPointScale, WayPointPosition, "Materials/StartIcon", out WayPointStart);
			} else if (WayPointStart != null && WayPointEnd == null) { // Put down the end icon if the start icon is already in place
				MakeWayPoint (WayPointScale, WayPointPosition, "Materials/StartIcon", out WayPointEnd);
			}
			else if (WayPointStart != null && WayPointEnd != null) { // If both waypoints exist, destroy them
				Destroy (WayPointStart);
				Destroy (WayPointEnd);
			}
		}


		// The part where the player picks up a model pointed by the crosshair and moves it
		if (Input.GetKeyDown (KeyCode.F)) {

			if (ObjectHitInfo.transform != null)
		        isModel = ObjectHitInfo.transform.parent.gameObject.name == "Model Collection";
			else
				isModel = false;

			if (ModelClone == null && isModel == true) { // Pick up a new model
				PrevModel = ObjectHitInfo.transform.gameObject;
				ModelClone = (GameObject) Instantiate(ObjectHitInfo.transform.gameObject);
				ModelClone.GetComponent<Rigidbody>().useGravity = false;
				ModelClone.GetComponent<BoxCollider>().enabled = false;
				ModelClone.transform.parent = GameObject.Find("Model Collection").transform;
			} else if (ModelClone != null) { // Place down the picked up model
				var PrevModelName = PrevModel.name;
				Destroy (PrevModel); // Destroy the original model
				ModelClone.name = PrevModelName; // Pass the name to the cloned model object
				ModelClone.GetComponent<Rigidbody>().useGravity = true;
				ModelClone.GetComponent<BoxCollider>().enabled = true;
				ModelClone = null; // Remove the reference to the clone object
			}

		} else if (ModelClone != null) { // if a model is picked up, attach it to the crosshair
			ModelClone.transform.position = TerrainHitInfo.point;
		}
	}


	// This is the method to create a icon on the terrain ground, positioned at the point that the crosshair aims at
	void MakeWayPoint (Vector3 WayPointScale, Vector3 WayPointPosition, string WayPointMat, out GameObject WayPoint) {
		WayPoint = GameObject.CreatePrimitive(PrimitiveType.Plane); // Create a plane object to hold the icon
		WayPoint.transform.localScale = WayPointScale; // Scale the size of the icon
		WayPoint.transform.position = WayPointPosition; // Put the icon at the desired position
		WayPoint.GetComponent<Collider>().enabled = false; // The icon should not have collider
		WayPoint.GetComponent<Renderer>().material = Resources.Load(WayPointMat) as Material; // Set the texture of the icon
	}
	
}
