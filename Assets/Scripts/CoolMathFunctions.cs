using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolMathFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Import the javascript function that redirects to another URL   
		[DllImport("__Internal")]privatestaticexternvoidRedirectTo();

		// Import the javascript function that redirects to another URL   
		[DllImport("__Internal")]privatestaticexternvoidStartGameEvent();

		// Import the javascript function that redirects to another URL 
		[DllImport("__Internal")]privatestaticexternvoidStartLevelEvent(int level);

		// Import the javascript function that redirects to another URL   
		[DllImport("__Internal")]privatestaticexternvoidReplayEvent();
	}
}
