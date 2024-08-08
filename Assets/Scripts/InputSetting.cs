using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputSetting : MonoBehaviour {
    [SerializeField] private TMP_InputField inputFTP;
    [SerializeField] private TMP_InputField inputHTTP;
    [SerializeField] private TMP_InputField inputUser;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputPathDir;
    [SerializeField] private Button buttonOpen;
    [SerializeField] private Button buttonApply;
    [SerializeField] private Button buttonClose;
    
    private FileSetting fileSetting = new();

    public event Action ApplyEvent; 

    public void Initialize() {
        buttonOpen.onClick.AddListener(OnOpen);
        buttonApply.onClick.AddListener(OnApply);
        buttonClose.onClick.AddListener(OnClose);
        SetHostSetting();
        Hide();
    }

    private void SetHostSetting() {
        HostSetting hostSetting = fileSetting.LoadFile();
        if (hostSetting == null) {
            SaveSetting();
        }
        else {
            inputFTP.text = hostSetting.serverFtp;
            inputHTTP.text = hostSetting.serverHttp;
            inputUser.text = hostSetting.username;
            inputPassword.text = hostSetting.password;
            inputPathDir.text = hostSetting.pathDir;
        }
    }
    
    public void Show() { gameObject.SetActive(true); }
    public void Hide() { gameObject.SetActive(false); }

    private void SaveSetting() {
        HostSetting hostSetting = new HostSetting() {
            serverFtp = inputFTP.text,
            serverHttp = inputHTTP.text,
            username = inputUser.text,
            password = inputPassword.text,
            pathDir = inputPathDir.text
        };
        fileSetting.SaveFile(hostSetting); 
        ApplyEvent?.Invoke();
    }
    
    private void OnApply() { SaveSetting(); }

    private void OnOpen() {
        Show();
    }
    private void OnClose() {
        Hide();
    }


}
