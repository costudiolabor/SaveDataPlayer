using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class DownLoader2 : MonoBehaviour {
    [SerializeField] private string pathJson = "http://solotest.tech/DataPlayers/DataPlayer.json";
    private string _json;
    private Texture2D _texture = null;
    private PlayerData _playerData;
    private bool _doneTexture = false;
    private bool _donePlayerData = false;
    
    // public async Task<PlayerData> GetPlayerData() {
    //     UnityWebRequest request = UnityWebRequest.Get(pathJson);
    //     await request.SendWebRequest();
    //     if (request.isNetworkError || request.isHttpError)
    //         Debug.Log(request.error);
    //     else {
    //         _json = request.downloadHandler.text;
    //         _playerData = JsonUtility.FromJson<PlayerData>(_json);
    //     }
    //     request.Dispose();
    //     return _playerData;
    // }
    //
    // public async Task<Texture2D> GetTexture(string pathPhoto) {
    //     UnityWebRequest request = UnityWebRequestTexture.GetTexture(pathPhoto);
    //     await request.SendWebRequest();
    //     if(request.isNetworkError || request.isHttpError) 
    //         Debug.Log(request.error);
    //     else  
    //         _texture = ((DownloadHandlerTexture) request.downloadHandler).texture; 
    //     request.Dispose();
    //     return _texture;
    // }
    
    // public async Task<PlayerData> GetPlayerData() {
    //     _doneTexture = false;
    //     StartCoroutine(DownLoadJson(pathJson));
    //     while (_donePlayerData == false) { await Task.Yield(); }
    //     return _playerData;
    // }
    //
    // private IEnumerator DownLoadJson(string url) {
    //     UnityWebRequest request = UnityWebRequest.Get(url);
    //     yield return request.SendWebRequest();
    //     if (request.isNetworkError || request.isHttpError)
    //         Debug.Log(request.error);
    //     else {
    //         _json = request.downloadHandler.text;
    //         _playerData = JsonUtility.FromJson<PlayerData>(_json);
    //     }
    //     _donePlayerData = true;
    //     request.Dispose();
    // }
    //
    //
    // public async Task<Texture2D> GetTexture(string pathPhoto) {
    //     _doneTexture = false;
    //     StartCoroutine(DownLoadTexture(pathPhoto));
    //     while (_doneTexture == false) { await Task.Yield(); }
    //     return _texture;
    // }
    //
    // private IEnumerator DownLoadTexture(string pathPhoto) {
    //     UnityWebRequest request = UnityWebRequestTexture.GetTexture(pathPhoto);
    //     yield return request.SendWebRequest();
    //     if(request.isNetworkError || request.isHttpError)
    //         Debug.Log(request.error);
    //     else {
    //         _texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
    //     }
    //     _doneTexture = true;
    //     request.Dispose();
    // }
    
}
