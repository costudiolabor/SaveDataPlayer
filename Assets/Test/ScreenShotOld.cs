using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotOld : MonoBehaviour {
    //[SerializeField] private RawImage _rawImage;
    private void Start() {
        StartCoroutine(UploadPNG());
    }
    IEnumerator UploadPNG() {
        yield return new WaitForEndOfFrame();
        var width = Screen.width;
        var height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
      //  _rawImage.texture = tex;
    }
}
