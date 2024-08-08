using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

// public class FTPDownLoader : MonoBehaviour
// {
//     //public string url = "https://unity3d.com/files/images/ogimg.jpg";
//     public string url = "ftp://31.31.196.224/www/solotest.tech/DataPlayers/Player2/Photo292.jpg";
//     IEnumerator Start()
//     {
//         using (WWW www = new WWW(url))
//         {
//             yield return www;
//             Renderer renderer = GetComponent<Renderer>();
//             renderer.material.mainTexture = www.texture;
//         }
//     }
// }


public class FTPDownLoader : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    // According to the network credentials created by the FTP username and password
    private NetworkCredential networkCredential;
    [Header("FTP IP path")]
    [Tooltip("Note: The port number defaults to 21;")]
    [SerializeField]
    private string FTPIpPath = "ftp://0.0.0.0:21/";
    [Header("FTP user name")]
    [SerializeField]
    private string UserName = "abc";
    [Header("FTP password")]
    [SerializeField]
    private string Password = "000000";
    [Header("FTP folder path")]
    [SerializeField]
    [Tooltip("Note: IP will be / www / wwwroot / FTP directory under this;")]
    private string FTPFolderPath = "file";
    /// Total path
    private string FTPPath
    {
        get { return FTPIpPath + FTPFolderPath; }
    }
    //public static FTPDownLoader a;
    private void Awake()
    {
       // a = this;
        CreatNetworkCredential();
    }

    private void Start() {
        //Debug.Log("FTPPath + FileName " + FTPPath + FileName);
       // DownloadFile(FTPPath + FileName, "D:/Chameleon/");
    }

    /// Create "Internet credentials" according to user name password
    private void CreatNetworkCredential()
    {
        networkCredential = new NetworkCredential(UserName, Password);
    }
    
    /// <summary>
    /// FTP Upload Note Setting FTP Permissions
    /// </summary>
    /// <param name = "loadfilepath"> Path of local file </ param>
    /// <param name = "ftppath"> To upload the FTP folder path </ param>
    private bool UpLoadFile(string LoadFilePath, string FTPPath)
    {
        FileInfo fileInfo = new FileInfo(LoadFilePath);
        FtpWebRequest request = CreateFtpWebRequest(FTPPath + Path.GetFileName(LoadFilePath), WebRequestMethods.Ftp.UploadFile);
        int buffLength = 2048;// How many bytes are read and written in a time
        byte[] buff = new byte[buffLength]; // Write bytes
        int contentLen;
        FileStream fs = fileInfo.OpenRead(); // Read files to convert files into byte files
        Stream strm = request.GetRequestStream();
        try
        {
            contentLen = fs.Read(buff, 0, buffLength);// read the file 2048 bytes, offset to 0, and assign the value to the buff byte group;
            while (contentLen != 0)
            {
                strm.Write(buff, 0, contentLen); // Key method, write flow
                contentLen = fs.Read(buff, 0, buffLength);
            }
            fs.Close();
            strm.Close();
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);
        }
        return true;

    }
    /// <summary>
    /// ftp Download Note Set FTP Permissions
    /// </summary>
    /// <param name = "ftpfilepath"> The path to the FTP file to download </ param>
    /// <param name = "loadpath"> Path of the local folder </ param>
    private bool DownloadFile(string FTPFilePath, string LocalPath)
    {
        try
        {
            
            FtpWebRequest request = CreateFtpWebRequest(FTPFilePath, WebRequestMethods.Ftp.DownloadFile);
            FtpWebResponse response = GetFtpResponse(request);
            Debug.Log(LocalPath + Path.GetFileName(FTPFilePath));
            //FileStream filestream = File.Create(LocalPath + Path.GetFileName(FTPFilePath));
            Stream responseStream = response.GetResponseStream();
            int buflength = 2048;
            byte[] buffer = new byte[buflength];
               
            int bytesRead;
            bytesRead = responseStream.Read(buffer, 0, buflength);
            
            while (bytesRead != 0)
            {
                Debug.Log("Progress: ");
                
                //filestream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, buflength);
            }
            responseStream.Close();
            //filestream.Close();
            
            int width = Screen.width;
            int height = Screen.height;
            
            //Texture2D tex = new Texture2D(2, 2, TextureFormat.RGB24, false);
            ///tex.LoadRawTextureData(buffer);
            //tex.Apply();
            //Texture2D tex = new Texture2D ();
            //tex.LoadImage (buffer, false);
            //var texture = new Texture2D(2, 2) { wrapMode = TextureWrapMode.Clamp };

            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false, false);
             
            texture.LoadImage(buffer, true);
            rawImage.texture = texture;
            
        }
        catch (WebException ex)
        {
            return false;
            throw new Exception(ex.Message);
        }
        return true;
    }
    
    
    #region  Interact with the server
    // Create an FTP connection
    public FtpWebRequest CreateFtpWebRequest(string uri, string requestMethod)
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
        request.Credentials = networkCredential;
        request.KeepAlive = true; // Do you keep an active status after the request is completed?
        request.UseBinary = true; // Indicate the binary data to be transmitted
        request.UsePassive = true; //passive
        request.Method = requestMethod;
        return request;
    }
    // Get the responsive body returned by the server
    public FtpWebResponse GetFtpResponse(FtpWebRequest request)
    {
        FtpWebResponse response = null;
        try
        {
            response = (FtpWebResponse)request.GetResponse();
            return response;
        }
        catch (WebException ex)
        {
            throw new Exception("Ftphelper Upload Error --> " + ex.Message);
        }

    }
    #endregion

    //P————————————————————————————————————————————————

    /// <summary>
    /// download the file, or upload files to XML folder
    /// </summary>

    //[Header("To upload or download the file name under the STREAMINGASSETS folder")]
    //public string FileName = "User.xml";

    //[ContextMenu("Download to StreamingAssets folder")]
    // public void DownXMLToStreaming()
    // {
    //     if (!System.IO.Directory.Exists(Application.streamingAssetsPath))
    //     {
    //         Directory.CreateDirectory(Application.streamingAssetsPath);
    //         UnityEditor.AssetDatabase.Refresh();
    //     }
    //
    //     DownloadFile(FTPPath + FileName, Application.streamingAssetsPath);
    //     UnityEditor.AssetDatabase.Refresh();
    //     print("Download completed!");
    // }
    // [ContextMenu("Upload to FTP Server")]
    // public void UpLoadXML()
    // {
    //     if (!System.IO.Directory.Exists(Application.streamingAssetsPath))
    //     {
    //         Directory.CreateDirectory(Application.streamingAssetsPath);
    //         UnityEditor.AssetDatabase.Refresh();
    //         Debug.LogError("No file");
    //         return;
    //     }
    //     if (!File.Exists(Application.streamingAssetsPath + "/" + FileName))
    //     {
    //         Debug.LogError("No file");
    //         return;
    //     }
    //
    //     UpLoadFile(Application.streamingAssetsPath + "/" + FileName, FTPPath);
    //     print("upload completed!");
    // }
}
