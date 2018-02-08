using Firebase.Database;

public class ChatMessageModel
{
    public long uid;
    public string userName;
    public string message;

    public ChatMessageModel()
    {
    }

    public ChatMessageModel(int id, string userName, string msg)
    {
        this.uid = id;
        this.userName = userName;
        this.message = msg;
    }

    // データの受け取り時の設定
    public ChatMessageModel(DataSnapshot data)
    {
        this.uid = (long)data.Child("uid").Value;
        this.userName = (string)data.Child("userName").Value;
        this.message = (string)data.Child("message").Value;
    }

    // 中身の書き換え
    public void Update(DataSnapshot data)
    {
        this.uid = (long)data.Child("uid").Value;
        this.userName = (string)data.Child("userName").Value;
        this.message = (string)data.Child("message").Value;
    }
}