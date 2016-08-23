namespace DGPractice
{
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class PracticeManager : MonoBehaviour
{

  public RectTransform button;
  public Text scoreText;


  private bool onClick = false;
  private int scoreNum = 0;

  /// ボタン押下処理
  public void OnClick()
  {
    this.onClick = true;
  }

  /// 初期化処理
  private IEnumerator Start()
  {

    this.scoreText.text = "score:0";

    DOTween.To(() => this.scoreNum, x => this.scoreNum = x, 10000, 1.5f).SetEase(Ease.Linear);

    yield return new WaitForSeconds(1.0f);
    
    Tweener tween = this.button.DOLocalMoveY(-200f, 0.5f).SetEase(Ease.Linear);

    yield return tween.WaitForCompletion();

    yield return new WaitUntil(() => this.onClick);

    tween = this.button.DOScale(0f, 1.0f).OnComplete(() =>
      {
        this.button.localPosition = new Vector3(0f, -360f, 0f);
        this.button.localScale = Vector3.one;
      });

    yield return 0;
  }

  /// 更新処理
  private void Update()
  {
    this.scoreText.text = string.Format("Score:{0:##,##0}", this.scoreNum);
  }
    
}
}