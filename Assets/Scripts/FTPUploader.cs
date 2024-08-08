using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


// server = "ftp://31.31.196.224/www/";
// username = "u2567361";
// password = "e9ePw5MWVh7d6x1C";
// pathDir = "solotest.tech/DataPlayers/";

class FTPUploader : MonoBehaviour {
	[SerializeField] private GameObject ftpUploaderView;
	[SerializeField] private Slider sliderProgress;
	[SerializeField] private TMP_Text textProgress;
	[SerializeField] private GameObject errorPanel;
	
	private string _serverFtp;
	private string _serverHttp;
	private string _username;
	private string _password;
	private string _pathDir;

	private string _url;
	private bool _doneUpLoad;
	
	private void Awake() { Hide();
	}
	public void SetSetting(HostSetting hostSetting) {
		_serverFtp = hostSetting.serverFtp;
		_serverHttp = hostSetting.serverHttp;
		_username = hostSetting.username;
		_password = hostSetting.password;
		_pathDir = hostSetting.pathDir;
	}
	public string GetServerFtp() { return _serverFtp; }
	public string GetServerHttp() { return _serverHttp; }
	public string GetPathDir() { return _pathDir; }

	public async Task<string> GetUpLoadFile(string dirPlayer, string filename, bool randomName = false) {
		_doneUpLoad = false;
		StartCoroutine(UpLoadFile(dirPlayer, filename, randomName));
		while (_doneUpLoad == false) { await Task.Yield(); }
		return _url;
	}

	public IEnumerator UpLoadFile (string dirPlayer, string filename, bool randomName) {
		_url = "";
		string path = _pathDir + dirPlayer;
		Show();
		SetValueSlider(0);
		yield return null;
		FileInfo file = new FileInfo(filename);
		string name = file.Name;

		if (randomName) {
			int rnd = Random.Range(0, 1000);
			string oldValue = "DataPlayer";
			string newValue = oldValue + "_" + rnd;
			name = name.Replace(oldValue, newValue);
		}
		
		_url = _serverFtp + Path.Combine(path, name);
		Uri address = new Uri(_url);
		FtpWebRequest request = FtpWebRequest.Create(address) as FtpWebRequest;
		request.Credentials = new NetworkCredential(_username, _password);
		request.KeepAlive = false;
		request.Method = WebRequestMethods.Ftp.UploadFile;
		request.UseBinary = true;
		request.ContentLength = file.Length;
		int bufferLength = 2048;
		byte[] buffer = new byte[bufferLength];
		int contentLength = 0;
		FileStream fileStream = file.OpenRead();
		try {
			Stream stream = request.GetRequestStream();
			contentLength = fileStream.Read(buffer, 0, bufferLength);
			while (contentLength != 0) {
				float progress = fileStream.Position / fileStream.Length * 100f;
				SetValueSlider(progress);
				stream.Write(buffer, 0, contentLength);
				contentLength = fileStream.Read(buffer, 0, bufferLength);
			}
			stream.Close();
			fileStream.Close();
			SetValueSlider(100);
			Debug.Log("Upload successful.");
			
		} 
		catch (Exception e) {
			errorPanel.SetActive(true);
			Debug.Log("Error uploading file: " + e.Message);
		}
		_doneUpLoad = true;
		yield return new WaitForSeconds(1.0f);
		Hide();
	}

	private void SetValueSlider(float progress) {
		//textProgress.text = "UPLOAD   " + progress + " % ";
		textProgress.text = progress + " % ";
		sliderProgress.value = progress;
		Debug.Log("Progress: " + progress);
	} 

	private void Show() {
		ftpUploaderView.SetActive(true);
	}
	
	private void Hide() {
		ftpUploaderView.SetActive(false);
	}
}


