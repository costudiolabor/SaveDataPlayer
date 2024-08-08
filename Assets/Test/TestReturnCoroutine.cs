using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestReturnCoroutine : MonoBehaviour {
    [SerializeField] private RawImage _rawImage;
    // private bool isDone = false;
    // private void Start() {
    //     Go();
    // }
    //
    // private void Update() {
    //     Debug.Log("ldklskdls");
    // }
    //
    //
    // private async void Go() {
    //     int abc = await GetInt();
    //     Debug.Log(abc);
    // }
    //
    //
    // public async Task<int> GetInt() {
    //     isDone = false;
    //     int abc = 203;
    //     StartCoroutine(GetIntCoroutine());
    //     while (isDone == false) { await Task.Yield(); }
    //     return abc;
    // }
    //
    //
    // IEnumerator GetIntCoroutine() {
    //     yield return new WaitForSeconds(2.0f);
    //     isDone = true;
    // }
    
    //[SerializeField] private string pathJson = "http://solotest.tech/DataPlayers/DataPlayer.json";
    [SerializeField] private string pathPhoto = "http://solotest.tech/DataPlayers/Player2/Photo352.jpg";
    private Texture2D texture = null;
    private bool doneTexture = false;
    public event Action<PlayerData> DoneJsonEvent;
    // public void GetJson() { StartCoroutine(GetJson(pathJson)); }
    //
    // public void GetTexture(Texture2D texture) {
    //     this.texture = texture;
    //     DoneJsonEvent += (playerData) => { StartCoroutine(GetTextureCor(pathPhoto)); };
    //     StartCoroutine(GetJson(pathJson));
    // }
    private async void Start() {
        _rawImage.texture = await GetTexture2();
    }
    public async Task<Texture2D> GetTexture2() {
        doneTexture = false;
        StartCoroutine(GetTextureCor(pathPhoto));
        while (doneTexture == false) { await Task.Yield(); }
        return texture;
    }
    // private IEnumerator GetJson(string url) {
    //     UnityWebRequest request = UnityWebRequest.Get(url);
    //     yield return request.SendWebRequest();
    //     if (request.isNetworkError || request.isHttpError)
    //         Debug.Log(request.error);
    //     else {
    //         json = request.downloadHandler.text;
    //         playerData = JsonUtility.FromJson<PlayerData>(json);
    //         pathPhoto = playerData.pathPhoto;
    //         DoneJsonEvent?.Invoke(playerData);
    //     }
    //     DoneJsonEvent = null;
    //     request.Dispose();
    // }
    private IEnumerator GetTextureCor(string pathPhoto) {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(pathPhoto);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else {
            texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
        doneTexture = true;
        request.Dispose();
    }
    

}
