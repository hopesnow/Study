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
  private bool endFlg = false;

  /// ボタン押下処理
  public void OnClick()
  {
    this.onClick = true;
  }

  /// 初期化処理
  private IEnumerator Start()
  {

    this.scoreText.text = "score:0";
    this.button.GetComponent<Button>().enabled = false;

    DOTween.To(() => this.scoreNum, x => this.scoreNum = x, 10000, 1.5f).SetEase(Ease.Linear);

    yield return new WaitForSeconds(1.0f);
    
    Tweener tween = this.button.DOLocalMoveY(-200f, 0.5f).SetEase(Ease.Linear)
      .OnComplete(() => this.button.GetComponent<Button>().enabled = true);

    yield return tween.WaitForCompletion();


    /*
    this.button.DOLocalMoveY(-200f, 0.5f)
      .OnComplete(() => this.button.DOLocalMoveX(-100f, 0.5f))
      .OnComplete(() => this.button.DOLocalMoveY(-200f, 0.5f))
      .OnComplete(() => this.endFlg = true);
    */

    /*
    Sequence seq = DOTween.Sequence();
    seq.Append(this.button.DOLocalMoveY(-100f, 0.5f))
      .Append(this.button.DOLocalMoveX(-100f, 0.5f))
      .AppendInterval(1.0f)
      .Append(this.button.DOLocalMoveY(-200f, 0.5f));
    */

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