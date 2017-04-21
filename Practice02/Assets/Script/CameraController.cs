using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  public float speed;
  public Transform lookPos;

  private void Update()
  {
    if (Input.GetKey(KeyCode.E))
    {
      this.transform.localPosition += this.transform.forward * speed;
    }
    if (Input.GetKey(KeyCode.D))
    {
      this.transform.localPosition -= this.transform.forward * speed;
    }
    if (Input.GetKey(KeyCode.F))
    {
      this.transform.localPosition += this.transform.right * speed;
    }
    if (Input.GetKey(KeyCode.S))
    {
      this.transform.localPosition -= this.transform.right * speed;
    }

    this.transform.LookAt(this.lookPos);

  }

}
