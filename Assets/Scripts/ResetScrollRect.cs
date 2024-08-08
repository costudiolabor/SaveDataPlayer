using UnityEngine;
using UnityEngine.UI;

public class ResetScrollRect : MonoBehaviour {
    [SerializeField] private ScrollRect scrollRect;
    public void OnEnable() {
        //Change the current vertical scroll position.
        scrollRect.verticalNormalizedPosition = 1.0f;
    }
}
