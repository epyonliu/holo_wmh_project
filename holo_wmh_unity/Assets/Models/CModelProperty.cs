using UnityEngine;
using System.Collections;

public class CModelProperty : MonoBehaviour {

	// Model stats
	public int Spd, Str, Mat, Rat, Arm, Def, Cmd;

	// Model status
	public bool Fire_Cont;       // model in fire continuous effect
	public bool Corrosion_cont;  // model in corrosion continuous effect
	public bool Stationary;      // model is stationary
	public bool Knocked_down;    // model is knocked down
	public bool Fleeing;         // model is fleeing
	public bool Disabled;        // model is disabled
	public bool Boxed;           // model is boxed
	public bool Destroyed;       // model is destroyed
	public bool RFP;             // model is removed from play

	// Model abilities
	public bool Fearless;
	public bool Terror;
	public bool Abomination;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
