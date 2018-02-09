using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DatabaseManager
{
    private static DatabaseManager instance;
    public static DatabaseManager Instance
    {
        get
        {
            if (instance == null)
                instance = new DatabaseManager();
            return instance;
        }
    }

    public void Init(string url)
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(url);
    }
}
