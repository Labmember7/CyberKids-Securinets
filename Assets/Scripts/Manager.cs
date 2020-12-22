using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    DatabaseReference mDatabaseRef;
    public InputField input;
     

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://juniorapp-3bd1e.firebaseio.com/");
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        mDatabaseRef.Child("Users").Child("User 1").Child("Email").SetValueAsync(input.text.ToString());
    }
    private void writeNewUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);

        mDatabaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }


    public class User
    {
        public string username;
        public string email;

        public User()
        {
        }

        public User(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }
}
