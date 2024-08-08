using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TestImageString : MonoBehaviour
{
    [SerializeField] InputField chatMessage;
    [SerializeField] private RawImage _rawImage;
    //public Texture2D texture;
    public Image imageToPutTex;
    public string json;
    private void Start() {
        StartCoroutine(ScreenShot());
    }
    
    IEnumerator ScreenShot() {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tempTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        // Read screen contents into the texture
        tempTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tempTexture.Apply();
        // Encode texture into PNG
        byte[] bytes = tempTexture.EncodeToPNG();
        _rawImage.texture = tempTexture;
        
        Debug.Log("bytes " + bytes.Length);
        
        int rawImageWidth = _rawImage.texture.width;
        int rawImageHeight = _rawImage.texture.height;
        
        //Texture2D result = new Texture2D( rawImageWidth, rawImageHeight, TextureFormat.RGB24, false);
       // result.ReadPixels(new Rect(_rawImage.transform.position.x, _rawImage.transform.position.y, rawImageWidth, rawImageHeight), 0, 0);
        //result.Apply();
        
        //ConvertTexture(tempTexture);
    }
    
    
    void ConvertTexture(Texture2D texture) {
        json = ConvertTextureToJson(texture);
        //Sprite outputSprite = ConvertTextureJsonToSprite(json);
        Debug.Log(json);
        //imageToPutTex.sprite = outputSprite;
        chatMessage.text = json;
    }

    
    
    //Convert a texture to a string and then store it in Json
    private string ConvertTextureToJson(Texture2D tex) {
        string TextureArray = Convert.ToBase64String(tex.EncodeToPNG());
        string jsonOutput = JsonUtility.ToJson(new StoreJson(TextureArray));
        return jsonOutput;
    }
    
    //Convert a json string to Sprite
     public Sprite ConvertTextureJsonToSprite(string json)
     {
         StoreJson test = JsonUtility.FromJson<StoreJson>(json);
         byte[] b64_bytes = Convert.FromBase64String(test.imageFile);
         Texture2D tex = new Texture2D(1, 1);
         tex.LoadImage(b64_bytes);
         tex.Apply();
         Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.zero);
         imageToPutTex.sprite = sprite;
         return sprite;
     }
}

/// <summary>
/// Store your Image as a string in this class
/// </summary>
[Serializable]
public class StoreJson
{
    public string imageFile;
    public StoreJson(string imageFile)
    {
        this.imageFile = imageFile;
    }
}
