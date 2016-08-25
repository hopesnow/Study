#define USE_DOTWEEN_PRO

namespace DGPractice
{
using System;
using System.Collections;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PracticeManager : MonoBehaviour
{

  public RectTransform button;
  public Text scoreText;

  public Image[] lamps;

  public Text story;


  private bool onClick = false;
  private int scoreNum = 0;

  private Sequence lampSeq;

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


    // ex.)join
//    /*
    Sequence seq = DOTween.Sequence();
    seq.Append(this.button.DOLocalMoveX(200f, 1f));
    seq.Join(this.button.DOLocalMoveY(100f, 1f));
    seq.AppendInterval(0.5f);
    seq.Join(this.button.DOPunchScale(Vector3.one * 0.2f, 0.5f));
    seq.AppendCallback(() => {tmpFlg = true; this.OnStopEffect();});
//    */

    // ex.)insert
    /*
    Sequence seq = DOTween.Sequence();
    seq.Append(this.button.DOLocalMoveX(200f, 1f));
    seq.AppendInterval(0.5f);
    seq.AppendCallback(() => tmpFlg = true);
    seq.Insert(0.5f, this.button.DOLocalMoveY(100f, 0.5f));
    seq.InsertCallback(1f, this.OnStopEffect);
    */


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


    // Pro Only
#if USE_DOTWEEN_PRO
    this.lampSeq = DOTween.Sequence();
    Tween tmp;
    this.lampSeq.AppendInterval((lamps.Length + 1) * 1f);
    // フェードで出現させる処理
    for(int i = 0;i < lamps.Length;i++)
    {
      tmp = this.lamps[i].DOFade(1f, 0.5f);
      this.lampSeq.Insert(1f * (i + 1), tmp);
    }
    this.lampSeq.AppendInterval(1f);
    // 色を変える処理
    foreach (Image img in lamps)
    {
      tmp = img.DOColor(Color.green, 0.5f);
      this.lampSeq.Join(tmp);
    }
    this.lampSeq.AppendInterval(3f);
    // 画面外への移動処理
    foreach (Image img in lamps)
    {
      this.lampSeq.Join(img.transform.DOLocalMoveY(360f, 1f).SetEase(Ease.Linear));
    }
    this.lampSeq.Play();
#endif

    Sequence clickSeq = DOTween.Sequence();
    clickSeq.Append(this.button.DOScale(0f, 1.0f));
    clickSeq.AppendCallback(()=>
      {
        this.button.localPosition = new Vector3(0f, -360f, 0f);
        this.button.localScale = Vector3.one;
      });

    yield return this.lampSeq.WaitForCompletion();

    Debug.Log("aaa");

    string storyText = "ご清聴\nありがとうございました。";
    // string emptyText = "　　　\n　　　　　　　　　　　　";
    string emptyText = this.ToEmptyStrings(storyText);

    this.story.text = emptyText;
    this.story.DOText(storyText, storyText.Length * 0.15f).SetEase(Ease.Linear);

    yield return 0;
  }

  /// 更新処理
  private void Update()
  {
    this.scoreText.text = string.Format("Score:{0:##,##0}", this.scoreNum);
    // if(lampSeq != null)
    //   Debug.Log("lamp sequence is Playing : " + lampSeq.IsPlaying());
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

  private string ToEmptyStrings(string str)
  {
    string result = string.Empty;
    foreach (char c in str)
    {
      if (c == '\n')
      {
        result += c;
        continue;
      }
      else if (Encoding.GetEncoding("shift_jis").GetByteCount(c.ToString()) == 1)
      {
        result += " ";
      }
      else
      {
        result += "　";
      }
    }
    return result;
  }

}
}