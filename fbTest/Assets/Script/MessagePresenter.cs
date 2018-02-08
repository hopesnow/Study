using System;
using UnityEngine;
using UnityEngine.UI;

public class MessagePresenter : MonoBehaviour
{
    public Text message;
    public Button deleteButton;

    // 初期化処理
    public void Init(string msg, bool canDelete, Action deleteEvent)
    {
        this.message.text = msg;

        if (canDelete)
        {
            this.deleteButton.gameObject.SetActive(true);
            this.deleteButton.onClick.AddListener(() =>
            {
                if (deleteEvent != null)
                    deleteEvent();
            });
        }
        else
        {
            this.deleteButton.gameObject.SetActive(false);
        }
    }

    // メッセージの編集の更新
    public void UpdateMessage(string msg)
    {
        this.message.text = msg;
    }
}
