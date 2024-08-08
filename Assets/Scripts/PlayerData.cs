using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData {
    public int idGlass;
    public string namePlayer;
    public int idPlayer;
    public string pathPhoto;
}

[Serializable]
public class PlayersData {
    public List<PlayerData> players;
}