using System;

[Serializable]
public class GlobalData
{
    public UserInput UserInput = new UserInput();
    public PlayerData PlayerData;

    public GlobalData()
    {
        PlayerData = new PlayerData();
    }
}