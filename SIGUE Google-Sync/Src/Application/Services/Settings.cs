namespace GMapsSync.Src.Application.Services;

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Settings
{
    private readonly string _path;
    private Dictionary<string, object> _settings;

    public Settings()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var settingsDir = Path.Combine(appDataPath, "AddonSettings");

        Directory.CreateDirectory(settingsDir);
        this._path = Path.Combine(settingsDir, "config.json");
        this._settings = new Dictionary<string, object>();
        this.Load();
    }

    public void Set<T>(string key, T value) where T : notnull
    {
        this._settings[key] = value;
        this.Save();
    }

    public T? Get<T>(string key)
    {
        if (!(_settings.TryGetValue(key, out var value) && value is JsonElement element))
        {
            return default;
        }
        try
        {
            return element.Deserialize<T>();
        }
        catch
        {
            return default;
        }
    }

    private void Load()
    {
        if (File.Exists(this._path))
        {
            var json = File.ReadAllText(this._path);
            this._settings = JsonSerializer.Deserialize<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
            return;
        }
        this._settings = new Dictionary<string, object>();
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(this._settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(this._path, json);
    }
}
