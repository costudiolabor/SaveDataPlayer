using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SaveDataPC : MonoBehaviour
{
}
//     void Start()
//     {
//         //StartCoroutine(UpApk());
//         string text = "kdskdksjkdjksjd";
//         //string pathSave = "//10.1.1.98/LEVELS/BLACK/HARD/75";
//         //string pathSave = "//192.168.1.77/";
//         string pathSave = "//192.168.1.77/D$/Chameleon";
//         File.WriteAllText(pathSave, text);
//     }
//
//     // IEnumerator UpApk()
//     // {
//     //     byte[] APK =
//     //         File.ReadAllBytes(
//     //             //"C:/Users/Administrator/Desktop/Vuforia_Jay.apk"); //Read local files and convert them to binary streams
//     //             "D:/Chameleon/TestHandAR.apk"); //Read local files and convert them to binary streams
//     //     WWWForm form = new WWWForm(); //Use WWWForm to upload the file as a form server
//     //     //Key-value pair
//     //     form.AddBinaryData("file", APK, "myApk.apk", "app/apk"); //Add binary byte stream to the form
//     //     
//     //     Debug.Log(" File.ReadAllBytes ");
//     //     UnityWebRequest webRequest =
//     //         //UnityWebRequest.Post("http://180.76.143.91:8989/jxarchives/api/file/update/addFile", form);
//     //         UnityWebRequest.Post("http://192.168.1.77", form);
//     //     //Use UnitywebRequest to send form data to the server
//     //     yield return webRequest.SendWebRequest(); //Start sending data
//     //     //Exception handling
//     //     if (webRequest.isHttpError || webRequest.isNetworkError)
//     //     {
//     //         Debug.Log(webRequest.error); //If an error occurs, print server error information
//     //     }
//     //     else
//     //     {
//     //         string text = webRequest.downloadHandler.text;
//     //         Debug.Log("Server return value" + text); //Print server return value correctly
//     //     }
//     // }
// }



//     void Start()
//     {
//         StartCoroutine(UploadMultipleFiles());
//     }
//
//     IEnumerator UploadMultipleFiles()
//     {
//         // string[] path = new string[3];
//         string path = "D:/File1.txt";
//         // path[0] = "D:/File1.txt";
//         // path[1] = "D:/File2.txt";
//         // path[2] = "D:/File3.txt";
//
//         //UnityWebRequest[] files = new UnityWebRequest[path.Length];
//         UnityWebRequest[] file = new UnityWebRequest[path.Length];
//         WWWForm form = new WWWForm();
//
//         for (int i = 0; i < files.Length; i++)
//         {
//             //files[i] = UnityWebRequest.Get(path[i]);
//             file = UnityWebRequest.Get(path);
//             yield return files[i].SendWebRequest();
//             form.AddBinaryData("files[]", files[i].downloadHandler.data, Path.GetFileName(path[i]));
//         }
//
//         UnityWebRequest req = UnityWebRequest.Post("http://localhost/File%20Upload/Uploader.php", form);
//         yield return req.SendWebRequest();
//
//         if (req.isHttpError || req.isNetworkError)
//             Debug.Log(req.error);
//         else
//             Debug.Log("Uploaded " + files.Length + " files Successfully");
//     }
// }

    //string url = "file:///192.168.1.77/D$/Chameleon";
    //string url = "http://localhost/File%20Upload/Uploader.php";
//     string url = "http://localhost/";
//
//     void Start()
//     {
//         StartCoroutine(UploadJPG());
//     }
//
//     IEnumerator UploadJPG()
//     {
//         // We should only read the screen after all rendering is complete
//         yield return new WaitForEndOfFrame();
//
//         // Create a texture the size of the screen, RGB24 format
//         int width = Screen.width;
//         int height = Screen.height;
//         var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
//
//         // Read screen contents into the texture
//         tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
//         tex.Apply();
//
//         // Encode texture into PNG
//         byte[] bytes = tex.EncodeToPNG();
//         Destroy(tex);
//
//         // Create a Web Form
//         WWWForm form = new WWWForm();
//         form.AddField("frameCount", Time.frameCount.ToString());
//         form.AddBinaryData("fileUpload", bytes, "img_file.jpg", "image/jpg");
//         
//
//        UnityWebRequest req = UnityWebRequest.Post(url, form);
//         yield return req.SendWebRequest();
//
//         if (req.isHttpError || req.isNetworkError)
//             Debug.Log(req.error);
//         else
//             Debug.Log("Uploaded files Successfully");
//     }
// }


//     string mainUrl = "file:///192.168.1.77/D$/Chameleon";
//     
//     void Start() {
//          StartCoroutine(UploadPNG());
//      }
//     
//     IEnumerator UploadPNG()
//     {
//         // We should only read the screen after all rendering is complete
//         yield return new WaitForEndOfFrame();
//
//         // Create a texture the size of the screen, RGB24 format
//         var width = Screen.width;
//         var height = Screen.height;
//         var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
//         // Read screen contents into the texture
//         tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
//         tex.Apply();
//         // Encode texture into PNG
//         byte[] bytes = tex.EncodeToPNG();
//         Destroy(tex);
//         string filename = "";
// // #if UNITY_STANDALONE_WIN
// //         filename = ScreenShotName(shotName);
// //         System.IO.File.WriteAllBytes(filename, bytes);
// //         Debug.Log(string.Format("Took screenshot to: {0}", filename));
// // #endif
// //#if UNITY_WEBGL
//         //filename = ScreenShotName(shotName, false) + ".png";
//         filename = "Test.png";
//         // Create a Web Form
//         WWWForm form = new WWWForm();
//         //form.AddField("action", "image upload");
//         //form.AddField("frameCount", Time.frameCount.ToString());
//         form.AddBinaryData("fileUpload", bytes, filename, "image/png");
//         // Upload to a cgi script
//        // WWW w = new WWW("http://www.yourdomainname.com/ImageUpload.php", form); // Change to wherever we put the .php file :)
//         WWW w = new WWW(mainUrl, form); // Change to wherever we put the .php file :)
//         yield return w;
//         if (!string.IsNullOrEmpty(w.error)) {
//             print(w.error);
//         } else {
//             print("Finished Uploading Screenshot");
//         }
// //#endif
//         //takeScreenshot = false;
    //}
    
    
    
//     //string mainUrl = "file:///192.168.1.77/D$/Chameleon/";
//     string mainUrl = "https://192.168.1.77/D$/Chameleon/";
//     //string mainUrl = @"D:\Chameleon\";
//     string saveLocation;
//
//     void Start() 
//     {
//         //saveLocation = "ftp:///home/xxx/x.zip"; // The file path.
//         saveLocation = "file://192.168.1.77/D$/Chameleon/x.zip"; // The file path.
//         StartCoroutine(PrepareFile());
//     }
//
// // Prepare The File.
//     IEnumerator PrepareFile() 
//     {
//         Debug.Log("saveLoacation = " + saveLocation);
//      
//         // Read the zip file.
//         WWW loadTheZip = new WWW(saveLocation);
//
//         yield return loadTheZip;
//
//         PrepareStepTwo(loadTheZip);
//     }
//
//     void PrepareStepTwo(WWW post) 
//     {
//         StartCoroutine(UploadTheZip(post));
//     }
//
// // Upload.
//     IEnumerator UploadTheZip(WWW post) 
//     {
//         // Create a form.
//         WWWForm form = new WWWForm();
//  
//         // Add the file.
//         //form.AddBinaryData("myTestFile.zip",post.bytes,"myFile.zip","application/zip");
//         form.AddBinaryData("fileUpload",post.bytes,"myFile.zip","application/zip");
//  
//         
//         //using (UnityWebRequest www = UnityWebRequest.Post("http://www.roastpartygame.com/database/storeImage.php", form))
//         using (UnityWebRequest www = UnityWebRequest.Post(mainUrl, form))
//         {
//             yield return www.SendWebRequest();
//
//             if (www.isNetworkError || www.isHttpError)
//             {
//                 Debug.Log(www.error);
//             }
//             else
//             {
//                 // Print response
//                 Debug.Log(www.downloadHandler.text);
//             }
//         }
        
        // Send POST request.
        //string url = mainUrl;
        // WWW POSTZIP = new WWW(url,form);
        //
        // Debug.Log("Sending zip...");
        // yield return POSTZIP;
        //
        // if (!string.IsNullOrEmpty(POSTZIP.error))
        // {
        //     print(POSTZIP.error);
        // }
        // else
        // {
        //     print("Finished Uploading Screenshot");
        // }
        // Debug.Log("Zip sent!");
    //  }
//}
