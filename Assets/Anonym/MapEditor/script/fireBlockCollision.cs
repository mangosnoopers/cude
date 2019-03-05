using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fireBlockCollision : MonoBehaviour
{

  public bool fireCollision = false;

  void Start()
  {
    Debug.Log("fireBlockCollision start");
  }

  void OnControllerColliderHit(ControllerColliderHit hit)
  {

    if (hit.collider.tag == "FireBlock")
    {
      fireCollision = true;
      //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  public bool hasCollided()
  {
    return fireCollision;
  }
}
