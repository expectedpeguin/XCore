using System;
using System.Collections.Generic;
using System.IO;

public class IniFile
{
    private readonly string _filePath;
    private readonly Dictionary<string, Dictionary<string, string>> _data;

    public IniFile(string path)
    {
        _filePath = path;
        _data = new Dictionary<string, Dictionary<string, string>>();
        LoadIniFile();
    }

    private void LoadIniFile()
    {
        string currentSection = null;

        foreach (var line in File.ReadAllLines(_filePath))
        {
            var trimmedLine = line.Trim();

            if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith(";") || trimmedLine.StartsWith("#"))
            {
                continue; // Skip empty lines and comments
            }

            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                if (!_data.ContainsKey(currentSection))
                {
                    _data[currentSection] = new Dictionary<string, string>();
                }
            }
            else if (currentSection != null)
            {
                var kvp = trimmedLine.Split(new[] { '=' }, 2);
                if (kvp.Length == 2)
                {
                    _data[currentSection][kvp[0].Trim()] = kvp[1].Trim();
                }
                else
                {
                    Console.WriteLine($"Invalid line in section '{currentSection}': {trimmedLine}");
                }
            }
            else
            {
                Console.WriteLine($"Line outside of any section: {trimmedLine}");
            }
        }
    }

    public string GetValue(string section, string key)
    {
        if (_data.ContainsKey(section) && _data[section].ContainsKey(key))
        {
            return _data[section][key];
        }
        throw new ArgumentException($"Key '{key}' not found in section '{section}'.");
    }

    public void SetValue(string section, string key, string value)
    {
        if (!_data.ContainsKey(section))
        {
            _data[section] = new Dictionary<string, string>();
        }
        _data[section][key] = value;
    }

    public void Save()
    {
        using (var writer = new StreamWriter(_filePath))
        {
            foreach (var section in _data)
            {
                writer.WriteLine($"[{section.Key}]");
                foreach (var kvp in section.Value)
                {
                    writer.WriteLine($"{kvp.Key} = {kvp.Value}");
                }
            }
        }
    }
}
