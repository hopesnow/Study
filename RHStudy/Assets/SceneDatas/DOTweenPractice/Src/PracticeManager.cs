namespace DGPractice
{
using System.Collections;
using DG.Tweening;
using UnityEngine;


public class PracticeManager : MonoBehaviour
{

  public RectTransform button;


  private bool onClick = false;

  /// ボタン押下処理
  public void OnClick()
  {
    this.onClick = true;
  }

  /// 初期化処理
  private IEnumerator Start()
  {
    yield return new WaitForSeconds(1.0f);
    
    Tweener tween = this.button.DOLocalMoveY(-200f, 0.5f).SetEase(Ease.Linear);

    yield return tween.WaitForCompletion();

    yield return new WaitUntil(() => this.onClick);

    tween = this.button.DOScale(0f, 1.0f);

    yield return 0;
  }
    
}
}