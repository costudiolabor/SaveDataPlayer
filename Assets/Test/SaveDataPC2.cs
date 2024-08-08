using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
 
public class SaveDataPC2 : MonoBehaviour {
    void Start() {
        StartCoroutine(Upload());
    }
 
    IEnumerator Upload() {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        //UnityWebRequest www = UnityWebRequest.Put("https://www.my-server.com/upload", myData);
        UnityWebRequest www = UnityWebRequest.Put("ftp://31.31.196.224", myData);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Upload complete!");
        }
    }
}
