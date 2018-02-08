using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatManager : MonoBehaviour
{
    // public ScrollRect scroll;
    public Scrollbar scrollBar;
    public Button sendButton;
    public InputField inputText;
    public MessagePresenter originText;

    private DatabaseReference testRef;

    private const int guildId = 12345;
    private const int uid = 10517;
    private const string userName = "noname";

    // private List<GameObject> textList = new List<GameObject>();
    private Dictionary<string, ChatMessageModel> messageList = new Dictionary<string, ChatMessageModel>();
    private Dictionary<string, MessagePresenter> textList = new Dictionary<string, MessagePresenter>();

    // 初期化処理
    public void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://msgtest-6ba4f.firebaseio.com/");

        // 送信ボタンの処理
        this.sendButton.onClick.AddListener(SendMessage);

        // イベントの登録
        this.testRef = FirebaseDatabase.DefaultInstance.GetReference("test");
        this.testRef.ChildAdded += MessageChildAdded;
        this.testRef.ChildChanged += MessageChildChanged;
        this.testRef.ChildRemoved += MessageChildRemoved;

        ReceiveAllMessage();
    }

    // メッセージの全受取
    public void ReceiveAllMessage()
    {
        FirebaseDatabase.DefaultInstance.GetReference("test").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("取得失敗");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot result = task.Result;

                foreach (var data in result.Children)
                {
                    // メッセージの追加処理
                    AddMessage(data);
                }
            }
        });
    }

    // メッセージ送信処理
    private void SendMessage()
    {
        var msg = this.inputText.text;
        if (string.IsNullOrEmpty(msg))
            return;

        this.inputText.text = string.Empty; // 空にする

        string key = this.testRef.Push().Key;
        var data = new ChatMessageModel(uid, userName, msg);
        string json = JsonUtility.ToJson(data);

        this.testRef.Child(key).SetRawJsonValueAsync(json);
    }

    // メッセージ追加処理
    private void AddMessage(DataSnapshot data)
    {
        var model = new ChatMessageModel(data);
        this.messageList.Add(data.Key, model);

        // objectの生成
        this.originText.gameObject.SetActive(true);
        var textObj = Instantiate(this.originText, this.originText.transform.parent);
        this.textList.Add(data.Key, textObj);
        this.originText.gameObject.SetActive(false);

        // データの設定
        var msg = string.Format("{0}:{1}", model.userName, model.message);
        textObj.Init(msg, uid == model.uid, () =>
        {
            // メッセージの削除処理
            this.testRef.Child(data.Key).RemoveValueAsync();
        });
    }

    // メッセージ編集処理
    private void ChangeMessage(DataSnapshot data)
    {
        ChatMessageModel model = null;

        // データの検索、更新
        if (this.messageList.ContainsKey(data.Key))
        {
            model = this.messageList[data.Key];
            model.Update(data);
        }

        // オブジェクトの検索、編集
        if (this.textList.ContainsKey(data.Key))
        {
            if (model != null)
                this.textList[data.Key].UpdateMessage(model.message);
        }
    }

    // メッセージ削除処理
    private void RemoveMessage(DataSnapshot data)
    {
        // データの検索、削除
        if (this.messageList.ContainsKey(data.Key))
        {
            this.messageList.Remove(data.Key);
        }

        // オブジェクトの検索、削除
        if (this.textList.ContainsKey(data.Key))
        {
            Destroy(this.textList[data.Key].gameObject);
            this.textList.Remove(data.Key);
        }
    }

    // メッセージ追加イベント
    private void MessageChildAdded(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        AddMessage(args.Snapshot);
    }

    // メッセージ編集イベント
    private void MessageChildChanged(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        ChangeMessage(args.Snapshot);
    }

    // メッセージ削除イベント
    private void MessageChildRemoved(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        RemoveMessage(args.Snapshot);
    }

    private void OnDestroy()
    {
        this.testRef.ChildAdded -= MessageChildAdded;
        this.testRef.ChildChanged -= MessageChildChanged;
        this.testRef.ChildRemoved -= MessageChildRemoved;
    }
}