using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Entry : MonoBehaviour {
    [SerializeField] private GameObject contentCanvas;
    [SerializeField] private Button buttonScreenShot;
    [SerializeField] private FTPUploader ftpUploader;
    [SerializeField] private InputDataView inputDataView;
    [SerializeField] private ScreenShot screenShot;
    [SerializeField] private DownLoader downLoader;
    [SerializeField] private InputSetting inputSetting;
    
    [SerializeField] private string dirDataBase;
    [SerializeField] private string dirPlayer;
    [SerializeField] private string nameJsonCurrentPlayer;   //"DataPlayer" + ".json"
    [SerializeField] private string nameJsonAllPlayers;      //"DataPlayers" + ".json"
    
    private PlayersData _playersData;
    private string _dirPath;
    private string _nameFile;
    private string _pathFile;

    private readonly FileSetting _fileSetting = new();
    private readonly HandlerFile _handlerFile = new();
    
    private Action<Texture2D> _screenShotEvent;
    private int _currentIndexPlayer;


    private void Awake() {
        inputSetting.Initialize();
        HostSetting hostSetting = _fileSetting.LoadFile();
        if (hostSetting == null) return;
        ftpUploader.SetSetting(hostSetting);
    }

    private void Start() { Initialize(); }

    public void Initialize() {
        buttonScreenShot.onClick.AddListener(OnScreenShot);
        inputDataView.Initialize();
        inputDataView.ApplyEvent += OnApply;
        inputDataView.HideEvent += OnHideInputData;
        _screenShotEvent += OnDoneScreenShot;
        inputSetting.ApplyEvent += OnApplySetting;
        _dirPath = Application.persistentDataPath;
    }

    private void OnApplySetting() {
        HostSetting hostSetting = _fileSetting.LoadFile();
        if (hostSetting == null) return;
        ftpUploader.SetSetting(hostSetting);
    }

    private void OnHideInputData() { contentCanvas.SetActive(true); }
    private void OnApply(PlayerData playerData) { FindPlayer(playerData); }
    private async void FindPlayer(PlayerData playerData) {
        bool isIDGlass = false;
        _playersData = await GetPlayers();
        if (_playersData.players.Count == 0) _playersData.players.Add(new PlayerData());
        
        int idGlass = playerData.idGlass;
        int idPlayer = playerData.idPlayer;

        for (int i = 0; i < _playersData.players.Count; i++) {
            if (_playersData.players[i].idGlass == idGlass) {
                _playersData.players[i] = playerData;
                isIDGlass = true;
                _currentIndexPlayer = i;
                break;
            }
        }

        if (isIDGlass == false) {
            for (int i = 0; i < _playersData.players.Count; i++) {
                if (_playersData.players[i].idGlass == 0) {
                    _playersData.players[i] = playerData;
                    isIDGlass = true;
                    _currentIndexPlayer = i;
                    break;
                }
            }
        }

        if (isIDGlass == false) {
            PlayerData temp = playerData;
            _playersData.players.Add(playerData);
            _currentIndexPlayer = _playersData.players.IndexOf(temp);
        }
        
        string dirData = dirPlayer + idPlayer;
        UpLoadPhoto(dirData);
    } 
    
    
    private async Task<PlayersData> GetPlayers() {
        _playersData = await downLoader.GetPlayers();
        return _playersData;
    }

    private void OnScreenShot() {
        contentCanvas.SetActive(false);
        screenShot.GetScreenShot(_screenShotEvent);
        
        int rnd = Random.Range(0, 1000);
        _nameFile = "Photo" + rnd + ".jpg";
        _pathFile = _dirPath + "/" + _nameFile;
    }
    
    private void OnDoneScreenShot(Texture2D texture) {
        inputDataView.SetPhoto(texture);
        SaveTexture(texture);
    }
    private void SaveTexture(Texture2D texture) {
        inputDataView.Show();
        _handlerFile.SaveTextureToFile(_pathFile, texture);
    }
   
   private async void UpLoadPhoto(string dirPlayer) {
       string urlPhoto = await ftpUploader.GetUpLoadFile(dirPlayer, _pathFile);
       string serverFtp = ftpUploader.GetServerFtp();
       string serverHttp = ftpUploader.GetServerHttp();
       urlPhoto = urlPhoto.Replace(serverFtp, serverHttp);
       _playersData.players[_currentIndexPlayer].pathPhoto = urlPhoto;
       SaveJsonCurrentPlayer(_playersData.players[_currentIndexPlayer]);
       SaveJsonAllPlayers();
   }
   
   private void SaveJsonCurrentPlayer(PlayerData playerData) {
       string pathFile = _handlerFile.SaveCurrentPlayerJson(_dirPath, nameJsonCurrentPlayer, playerData);
       UpLoadJson(pathFile);
       UpLoadFileToDataBase(pathFile);
   }
   
   private void SaveJsonAllPlayers() {
       string pathFile = _handlerFile.SaveAllPlayerJson(_dirPath, nameJsonAllPlayers, _playersData);
       UpLoadJson(pathFile);
   }

   private async void UpLoadJson(string pathFile) {
       string dirPlayer = "";
       await ftpUploader.GetUpLoadFile(dirPlayer, pathFile);
   } 
   
   private void UpLoadFileToDataBase(string pathFile) {
       UpLoadJsonToDataBase(pathFile);
   }
   
   private async void UpLoadJsonToDataBase(string pathFile) {
       string dirPlayer = dirDataBase;
       await ftpUploader.GetUpLoadFile(dirPlayer, pathFile, true);
   }
}


