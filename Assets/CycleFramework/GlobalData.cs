using System;

[Serializable]
public class GlobalData
{
    public UserInput UserInput;
    public PlayerData PlayerData;
    public ConstructionsRepository ConstructionsRepository;

    public GlobalData()
    {
        UserInput = new UserInput();
        PlayerData = new PlayerData();
        ConstructionsRepository = new ConstructionsRepository();
    }
}