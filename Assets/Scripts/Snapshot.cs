using System.Threading.Tasks;
using UnityEngine;

public class Snapshot : MonoBehaviour {
   //  [SerializeField] private Vector2Int screenSizeStream = new (1280, 720);
   //  private Vector2Int GetScreenSize(bool isRealWear) {
   //      int screenWidth;
   //      int screenHeight;
   //      if (isRealWear) {
   //          screenWidth = screenSizeStream.x;
   //          screenHeight = screenSizeStream.y;
   //      }
   //      else {
   //          screenWidth = Screen.width;
   //          screenHeight = Screen.height;
   //      }
   //      return new Vector2Int(screenWidth, screenHeight);
   //  }
   //  
   // public async Task<Texture2D> Take(bool isRealWear){
   //      Debug.Log("StartSnapshot");
   //      var screenSize = GetScreenSize(isRealWear);
   //      var screenShot = new Texture2D(screenSize.x, screenSize.y, TextureFormat.RGB24, false);
   //      //await Task.WaitForEndOfFrame(this);
   //      screenShot.ReadPixels(new Rect(0, 0, screenSize.x, screenSize.y), 0, 0);
   //      screenShot.Apply();
   //      return screenShot;
   //  }
    
    // public async UniTask<Texture2D> Take(bool isConnect){
    //     var screenSize = GetScreenSize(isConnect);
    //     var renderTexture = new RenderTexture(screenSize.x, screenSize.y, 0, RenderTextureFormat.ARGB32);
    //     await UniTask.WaitForEndOfFrame(this);
    //     ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
    //     _resultTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
    //     Graphics.ConvertTexture(renderTexture, _resultTexture);
    //     //Graphics.CopyTexture(renderTexture, _resultTexture);
    //     Destroy(renderTexture);
    //     return _resultTexture;
    // }
    //
    
    // public async UniTask<Texture2D> Take(bool isConnect){
    //     var screenSize = GetScreenSize(isConnect);
    //     var renderTexture = new RenderTexture(screenSize.x, screenSize.y, 0, RenderTextureFormat.ARGB32);
    //     await UniTask.WaitForEndOfFrame(this);
    //     ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
    //     _resultTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
    //     Graphics.ConvertTexture(renderTexture, _resultTexture);
    //     Destroy(renderTexture);
    //     return _resultTexture;
    // }
}
   
