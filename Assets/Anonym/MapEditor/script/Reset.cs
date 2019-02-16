using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Reset : MonoBehaviour
{

  //public Image deathImage;
  private AudioSource music;

  [DllImport("__Internal")]
  private static extern void ReplayEvent();

  // Use this for initialization
  void Start()
  {
    //deathImage.enabled = false;
    music = GameObject.Find("MusicTime").GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      Restart();
    }

  }

  public void Restart()
  {
    //if(!GameObject.Find("MusicTime").GetComponent<AudioSource>().isPlaying)
    if (music != null) music.volume = 0.1f;
    // if(!music.isPlaying)
    // 	music.UnPause();
    
    #if UNITY_WEBGL
    ReplayEvent();
    #endif

    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
