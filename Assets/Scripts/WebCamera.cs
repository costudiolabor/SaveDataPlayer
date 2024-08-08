    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    [Serializable]
    public class WebCamera : MonoBehaviour {
        [SerializeField] private RawImage rawImage;
        [SerializeField] private AspectRatioFitter aspectFitter;
        private WebCamTexture webCamTexture;
        private void WebCamPlay() =>   webCamTexture.Play();
        public void WebCamStop() =>   webCamTexture.Stop();
        IEnumerator Start () {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) yield break;
            webCamTexture = new WebCamTexture(1920, 1080, 30);
            WebCamPlay();
            yield return new WaitUntil(() => webCamTexture.width != 16 && webCamTexture.height != 16);
            rawImage.texture = webCamTexture;
            rawImage.gameObject.SetActive(true);
            aspectFitter.aspectRatio = (float)webCamTexture.width / webCamTexture.height;
        }
    }
