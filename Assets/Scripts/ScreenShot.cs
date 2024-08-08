using System;
using System.Collections;
using UnityEngine;

public class ScreenShot : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    public void GetScreenShot(Action<Texture2D> screenShotEvent) => StartCoroutine(Take(screenShotEvent));
    private Texture2D _screenShot;
   
    private IEnumerator Take(Action<Texture2D> screenShotEvent){
        yield return new WaitForEndOfFrame();
        audioSource.Play();
        int width = Screen.width;
        int height = Screen.height;
        _screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        _screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        _screenShot.Apply();
        screenShotEvent?.Invoke(_screenShot);
    }
}
