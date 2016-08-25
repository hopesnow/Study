namespace DGPractice
{
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TestEaseType : MonoBehaviour
{
  public Ease easeType;
  public Text text;

  private bool inBottom;


  public void OnClick()
  {
    float moveYPos = this.inBottom ? 150f : -250f;
    this.inBottom = !this.inBottom;
    this.GetComponent<Button>().enabled = false;
    this.transform.DOLocalMoveY(moveYPos, 1.0f).SetEase(this.easeType).OnComplete(() =>
      {
        this.GetComponent<Button>().enabled = true;
      });
  }



  private void Start()
  {
    this.inBottom = true;
    this.text.text = this.easeType.ToString();
  }

  private void Update()
  {
    this.text.text = this.easeType.ToString();
  }


}
}