using System.IO;
using UnityEngine;

public class HandlerFile {
    private Texture2D _texture2D;
    public void SaveTextureToFile(string pathFile, Texture2D texture) {
        _texture2D = new Texture2D(texture.width, texture.height);
        _texture2D.SetPixels(texture.GetPixels());
        _texture2D.Apply();
        byte[] bytes = _texture2D.EncodeToJPG();
        File.WriteAllBytes(pathFile, bytes);
    }
    
    public string SaveCurrentPlayerJson(string dirPath, string nameJson, PlayerData playerData) {
        string pathJson = dirPath + "/" + nameJson; //"DataPlayer" + ".json";
        return SaveFileCurrentJson(pathJson, playerData);
    }
    
    public string SaveAllPlayerJson(string dirPath, string nameJson, PlayersData playersData) {
        string pathJson = dirPath + "/" + nameJson; //"DataPlayers" + ".json";
        return SaveFileAllPlayerJson(pathJson, playersData);
    }
    
    
    public string SaveFileAllPlayerJson(string pathFile, PlayersData playerData) {
        string dataUpLoad = JsonUtility.ToJson(playerData);
        File.WriteAllText(pathFile,dataUpLoad);
        return pathFile;
    }
    
    public string SaveFileCurrentJson(string pathFile, PlayerData playerData) {
        string dataUpLoad = JsonUtility.ToJson(playerData);
        File.WriteAllText(pathFile,dataUpLoad);
        return pathFile;
    }
    
}