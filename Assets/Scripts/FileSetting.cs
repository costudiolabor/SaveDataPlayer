using System;
using File = System.IO.File;
using UnityEngine;
using System.IO;

[Serializable]
public class FileSetting {
    private const string nameFile = "HostSetting.config";
    public void SaveFile(HostSetting hostSetting ) {
        string path = $"{ Application.persistentDataPath}/{nameFile}";
        string data = JsonUtility.ToJson(hostSetting);
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine(data);
        sw.Close();
    }

    public HostSetting LoadFile() {
        var path = $"{ Application.persistentDataPath}/{nameFile}";
        var fileExist = File.Exists(path);
        if (fileExist == false) {
            Debug.Log(nameFile + " файл не существует");
            return null;
        }
        StreamReader sw = new StreamReader(path);
        string data = sw.ReadLine();
        sw.Close();
        HostSetting hostSetting = JsonUtility.FromJson<HostSetting>(data);
        return hostSetting;
    }
}