namespace DGPractice
{
using System.Collections;
using DG.Tweening;
using UnityEngine;


public class PracticeManager : MonoBehaviour
{

  public RectTransform button;


  private bool onClick = false;

  public void OnClick()
  {
    this.onClick = true;
  }

  private IEnumerator Start()
  {
    Tweener tween = this.button.DOLocalMoveY(-100f, 0.5f).SetEase(Ease.Linear);

    yield return tween.WaitForCompletion();

    yield return new WaitUntil(() => this.onClick);

    tween = this.button.DOLocalPath(new Vector3[]{new Vector3(0f, -100f, 0f), new Vector3(0f, 100f, 0f), new Vector3(760f, 100f, 0f)}, 1.0f);

    yield return 0;
  }
    
}
}