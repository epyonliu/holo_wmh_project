using UnityEngine;
using System.Collections;

public class CTargetRaycast : MonoBehaviour {

	Camera MainCam;
	GameObject WayPointStart, WayPointEnd;

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

		// Raycasting onto any object in the world
		RaycastHit ObjectHitInfo;
		Physics.Raycast (CamPosition, CamDirection, out ObjectHitInfo, Mathf.Infinity);


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
		if (Input.GetMouseButtonDown (1)) {
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
