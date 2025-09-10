using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerConfigData
{
    public float lookSensitivity = 0.1f;
    public float masterVolume = 0.8f;
    public float musicVolume = 0.8f;
    public float effectsVolume = 0.8f;
}

public static class FreeDoomSettingsSaveLoad
{
    private static string configDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Dwarf"
    );

    private static string configPath = Path.Combine(configDir, "dwarf_config.json");

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
        return new PlayerConfigData(); // default fallback
    }
}