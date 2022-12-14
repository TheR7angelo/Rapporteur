using System;
using System.IO;
using Newtonsoft.Json;

namespace Rapporteur.Function;

public static class Setting
{
    private static string SettingPath { get; }
    private static SSetting SettingData { get; set; }

    private static SSetting BaseSSetting { get; } = new()
    {
        Step = 5,
        Size = 400,
        LeftKey = 23,
        UpKey = 24,
        RightKey = 25,
        DownKey = 26
    };

    static Setting()
    {
        SettingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "rapporteur.json");
        if (!File.Exists(SettingPath))
        {
            InitSettings();
        }

        SettingData = ReadAllSettings();
    }

    public static void InitSettings(SSetting? settings=null)
    {
        var setting = settings ?? BaseSSetting;

        SettingData = setting;
        var str = JsonConvert.SerializeObject(setting, Formatting.Indented);
        File.WriteAllText(SettingPath, str);
    }
    
    public static SSetting ReadAllSettings()
    {
        var str = File.ReadAllText(SettingPath);
        return JsonConvert.DeserializeObject<SSetting>(str);
    }

    public static void AddUpdateAppSettings()  
    {  
        // pass
    }  
    
    
}