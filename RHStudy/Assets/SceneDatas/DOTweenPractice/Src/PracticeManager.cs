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

  public Image[] lamps;


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

    this.scoreText.text = "score:10,000";
    this.scoreNum = 10000;
    this.button.GetComponent<Button>().enabled = false;

    DOTween.To(() => this.scoreNum, x => this.scoreNum = x, 0, 1.5f).SetEase(Ease.Linear);

    yield return new WaitForSeconds(1.0f);
    
    Tweener tween = this.button.DOLocalMoveY(-200f, 0.5f).SetEase(Ease.Linear)
      .OnComplete(() => this.button.GetComponent<Button>().enabled = true);

    yield return tween.WaitForCompletion();

    yield return new WaitForSeconds(1f);

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

    bool tmpFlg = false;


    // ex.)all
    /*
    Sequence seq = DOTween.Sequence();
    seq.Append(this.button.DOLocalMoveX(200f, 1f));
    seq.Join(this.button.DOLocalMoveY(100f, 1f));
    seq.AppendInterval(0.5f);
    seq.Join(this.button.DOPunchScale(Vector3.one * 0.2f, 0.5f));
    seq.AppendCallback(() => tmpFlg = true);
    */

    // ex.)insert
    Sequence seq = DOTween.Sequence();
    seq.Append(this.button.DOLocalMoveX(200f, 1f));
    seq.AppendInterval(0.5f);
    seq.AppendCallback(() => tmpFlg = true);
    seq.Insert(0.5f, this.button.DOLocalMoveY(100f, 0.5f));
    seq.InsertCallback(1f, this.OnStopEffect);



    /*
    Sequence seq = DOTween.Sequence();
    seq.OnStart(() => 
      {
        this.OnRightWalk();
      });
    seq.Append(this.button.DOLocalMoveY(200f, 1f));
    seq.OnComplete(this.OnEndWalk);
    */

    yield return new WaitUntil(() => this.onClick);

    Sequence lampSeq = DOTween.Sequence();
    Tween tmp;
    lampSeq.AppendInterval((lamps.Length + 1) * 1f);
    for(int i = 0;i < lamps.Length;i++)
    {
      tmp = DOTween.To(() => this.lamps[i].color.a, value =>
        {
          this.lamps[i].color = new Color(1f, 0f, 0f, value);
        }, 1f, 0.5f);
      lampSeq.Insert(1f * (i + 1), tmp);
      tmp = DOTween.To(() => this.lamps[i].color.g, value =>
        {
          this.lamps[i].color = new Color(1.0f - value, value, 0f, 1f);
        }, 1f, 0.5f);
      lampSeq.Join(tmp);
    }
    lampSeq.Play();

    Sequence clickSeq = DOTween.Sequence();
    clickSeq.Append(this.button.DOScale(0f, 1.0f));
    clickSeq.AppendCallback(()=>
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

  private void OnRightWalk()
  {
  }

  private void OnEndWalk()
  {
  }

  private void OnStopEffect()
  {
    Debug.Log("StopEffect");
    this.button.GetComponent<Image>().color = Color.yellow;
  }

}
}