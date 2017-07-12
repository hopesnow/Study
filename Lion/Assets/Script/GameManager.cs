/** ********************************************************************************
* @summary ゲームの管理
* @author  Ryosuke
* @date 17/07/11
***********************************************************************************/
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/** ********************************************************************************
* @summary ゲームの管理クラス
* @author  Ryosuke
***********************************************************************************/
public class GameManager : MonoBehaviour
{
    [SerializeField] private Image sitRabbit;
    [SerializeField] private Image standRabbit;
    [SerializeField] private Button switchButton;
    [SerializeField] private Text buttonText;
    [SerializeField] private float animDuration = 0.5f;

    private bool isAnim = false;
    private bool isStand = false;

    private const string SitText = "Tap To Sit";
    private const string StandText = "Tap To Stand";

    // 初期化処理
    public void Start()
    {
        this.isAnim = false;
        this.isStand = false;

        this.sitRabbit.color = new Color(1f, 1f, 1f, 1f);
        this.standRabbit.color = new Color(1f, 1f, 1f, 0f);

        this.buttonText.text = StandText;

        // 切り替えボタンの処理登録
        this.switchButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                if (this.isStand)
                {
                    SitDownAnim();
                }
                else
                {
                    StandUpAnim();
                }
            });
    }

    // 立つ時のアニメーション開始
    private void StandUpAnim()
    {
        // アニメーション中は実行無視する
        if (this.isAnim)
            return;

        var seq = DOTween.Sequence();

        this.isAnim = true;
        seq.Append(sitRabbit.DOFade(0f, this.animDuration).SetEase(Ease.Linear));
        seq.Join(standRabbit.DOFade(1f, this.animDuration).SetEase(Ease.Linear));
        seq.OnComplete(() =>
            {
                this.buttonText.text = SitText;
                this.isStand = true;
                this.isAnim = false;
            });

        seq.Play();
    }

    // 座る時のアニメーション開始
    private void SitDownAnim()
    {
        // アニメーション中は実行無視する
        if (this.isAnim)
            return;
        
        var seq = DOTween.Sequence();

        this.isAnim = true;
        seq.Append(sitRabbit.DOFade(1f, this.animDuration).SetEase(Ease.Linear));
        seq.Join(standRabbit.DOFade(0f, this.animDuration).SetEase(Ease.Linear));
        seq.OnComplete(() =>
            {
                this.buttonText.text = StandText;
                this.isStand = false;
                this.isAnim = false;
            });

        seq.Play();
    }

}