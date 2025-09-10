using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerConfigData
{
    public float lookSensitivity = 1.0f;
    public float movementSpeed = 5.0f;
    public KeyCode interactKey = KeyCode.E;
    public float masterVolume = 0.8f;
}

public static class FreeDoomSettingsSaveLoad
{
    private static string configDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "FreeDoomAlbum"
    );

    private static string configPath = Path.Combine(configDir, "player_config.json");

    public static void Save(PlayerConfigData data)
    {
        if (!Directory.Exists(configDir))
            Directory.CreateDirectory(configDir);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(configPath, json);
    }

    public static PlayerConfigData Load()
    {
        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            return JsonUtility.FromJson<PlayerConfigData>(json);
        }
        else
        {
            return new PlayerConfigData(); // default fallback
        }
    }
}