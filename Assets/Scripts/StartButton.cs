using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class StartButton : MonoBehaviour
{
  [DllImport("__Internal")]
  private static extern void StartGameEvent();

  public void startGame()
  {
    GameObject gmobjct = GameObject.FindWithTag("GameManager");
    Destroy(GameObject.Find("MusicTime"));
    SceneManager.LoadScene("B0.5 - Intro", LoadSceneMode.Single);
    StartGameEvent();
  }
}
