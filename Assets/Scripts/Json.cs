using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool FirstTime;
    public int Quality; // 1: Low, 2:Mid, 3: High
    public int Speed; // 1: 0.5倍速, 2: 0.75倍速, 3: 1倍速, 4: 1.25倍速, 5: 1.5倍速
    public int BestScore;
    public int WorstScore;
    public int Ochinchin;
}

public class Json : MonoBehaviour
{
    static public Json instance;
    string datapath;

    private void Awake()
    {
        datapath = Application.dataPath + "/Json/Player.json";

        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        PlayerData player = Load();
        if (player.FirstTime == false)
        {
            player.FirstTime = true;
            player.Quality = 1;
            player.Speed = 3;
            player.BestScore = 0;
            player.WorstScore = 0;
            player.Ochinchin = 0;
        };
        UpdateResolution(player.Quality);
        UpdateTimeScale(player.Speed);
        Save(player);
    }

    public void UpdateTimeScale(int SpeedType)
    {
        if (SpeedType == 1)
        {
            Time.timeScale = 0.5f;
        }
        else if (SpeedType == 2)
        {
            Time.timeScale = 0.75f;
        }
        else if (SpeedType == 3)
        {
            Time.timeScale = 1f;
        }
        else if (SpeedType == 4)
        {
            Time.timeScale = 1.25f;
        }
        else
        {
            Time.timeScale = 1.5f;
        }
    }

    private void UpdateResolution(int QualityType)
    {
        if (QualityType == 1)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (QualityType == 2)
        {
            Screen.SetResolution(2560, 1440, false);
        }
        else
        {
            Screen.SetResolution(3840, 2160, false);
        }
    }

    public PlayerData Load()
    {
        StreamReader reader = new StreamReader(datapath);
        string datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<PlayerData>(datastr);
    }

    public void Save(PlayerData player)
    {
        string jsonstr = JsonUtility.ToJson(player, true);
        StreamWriter writer = new StreamWriter(datapath, false);
        writer.WriteLine(jsonstr);
        writer.Flush();
        writer.Close();
    }
}