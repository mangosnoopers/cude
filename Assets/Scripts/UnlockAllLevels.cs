using UnityEngine;

public class UnlockAllLevels : MonoBehaviour {

	// Use this for initialization
	public void UnlockAll () {
		PlayerPrefs.SetInt("LevelPassed", 25);
	}
}
