using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class DownLoader : MonoBehaviour {
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private string pathJson = "http://solotest.tech/DataPlayers/DataPlayers.json";
    //[SerializeField] private string pathJson = "http://solotest.tech/DataPlayers/DataPlayer.json";
    private string _json;
    private Texture2D _texture;
    //[SerializeField] 
    private PlayersData _playersData = new PlayersData();
    private bool _doneTexture = false;
    private bool _donePlayerData = false;
    
    // public async Task<PlayerData> GetPlayerData() {
    //     _playerData = null;
    //     _donePlayerData = false;
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

    private void Start() {
        errorPanel.SetActive(false);
    }

    public async Task<PlayersData> GetPlayers() {
        _donePlayerData = false;
        StartCoroutine(DownLoadJson(pathJson));
        while (_donePlayerData == false) { await Task.Yield(); }
        return _playersData;
    }
    
    private IEnumerator DownLoadJson(string url) {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) {
            errorPanel.SetActive(true);
            Debug.Log(request.error);
        }
        else {
            _json = request.downloadHandler.text;
            _playersData = JsonUtility.FromJson<PlayersData>(_json);
        }
        _donePlayerData = true;
        request.Dispose();
    }
    
    
    
    public async Task<Texture2D> GetTexture(string pathPhoto) {
        _texture = null;
        _doneTexture = false;
        StartCoroutine(DownLoadTexture(pathPhoto));
        while (_doneTexture == false) { await Task.Yield(); }
        return _texture;
    }
    
    private IEnumerator DownLoadTexture(string pathPhoto) {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(pathPhoto);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else {
            _texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
        _doneTexture = true;
        request.Dispose();
    }
    
}
