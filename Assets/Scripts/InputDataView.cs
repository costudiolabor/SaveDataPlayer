using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputDataView : MonoBehaviour  {
    [SerializeField] private RawImage photo;
    [SerializeField] private TMP_InputField inputIdGlass;
    [SerializeField] private TMP_InputField inputNamePlayer;
    [SerializeField] private TMP_InputField inputIdPlayer;
    [SerializeField] private Button buttonApply;
    [SerializeField] private Button buttonClose;

    [SerializeField] private int maxGlass;
    [SerializeField] private int maxPlayers;
    
    public event Action<PlayerData> ApplyEvent;
    public event Action HideEvent;

    private bool doneIdGlass;
    private bool doneNamePlayer;
    private bool doneIdPlayer;

    public void Initialize() {
        inputIdGlass.onValueChanged.AddListener(OnCheckCorrectIdGlass);
        inputNamePlayer.onValueChanged.AddListener(OnCheckCorrectNamePlayer);
        inputIdPlayer.onValueChanged.AddListener(OnCheckCorrectIdPlayer);
        buttonApply.onClick.AddListener(OnApply);
        buttonClose.onClick.AddListener(OnClose);
        Hide();
    }

    private void OnClose() {
        HideEvent?.Invoke();
        ClearInputField();
        Hide();
    }

    public void OnCheckCorrectIdGlass(string value) {
        doneIdGlass = int.TryParse(value, out int result) && result > 0;
        InteractableApply();
    }
    
    public void OnCheckCorrectNamePlayer(string value) {
        doneNamePlayer = value != "";
        InteractableApply();
    }

    public void OnCheckCorrectIdPlayer(string value) {
        doneIdPlayer = int.TryParse(value, out int result) && result <= maxPlayers && result > 0;;
        InteractableApply();
    }
    
    private void OnApply() {
        PlayerData playerData = new PlayerData {
            idGlass = int.Parse(inputIdGlass.text),
            namePlayer = inputNamePlayer.text,
            idPlayer = int.Parse(inputIdPlayer.text)
        };
        ApplyEvent?.Invoke(playerData);
        ClearInputField();
    }

    private void ClearInputField() {
        inputIdGlass.text = "";
        inputNamePlayer.text = "";
        inputIdPlayer.text = "";
    }

    private void InteractableApply() { buttonApply.interactable = (doneIdGlass & doneNamePlayer & doneIdPlayer); }
    
    public void Show() { gameObject.SetActive(true); }
    
    public void Hide() { gameObject.SetActive(false); }

    public void SetPhoto(Texture2D texture) { photo.texture = texture; }
  
}
