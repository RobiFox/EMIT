using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using UnityEngine;

public class Messages {
    //private Dictionary<SystemLanguage, TextAsset> _language = new Dictionary<SystemLanguage, TextAsset>();
    private Dictionary<string, string> _values = new Dictionary<string, string>();

    private static Messages _instance;
    public static Messages Instance {
        get {
            return _instance ?? (_instance = new Messages());
        }
    }

    private Messages() {
        SystemLanguage systemLanguage = Application.systemLanguage;
        string loaded = PlayerPrefs.GetString(PlayerValues.Language, systemLanguage.ToString());
        Enum.TryParse(loaded, out systemLanguage);
        LoadLanguage(systemLanguage);
    }

    public void LoadLanguage(SystemLanguage lang) {
        _values.Clear();
        
        TextAsset asset = (TextAsset) Resources.Load("Language/" + lang, typeof(TextAsset));
        if (asset == null) {
            Debug.LogError("No language " + lang + " found.");
            asset = (TextAsset) Resources.Load("Language/" + SystemLanguage.English, typeof(TextAsset));
        }
        StringReader reader = new StringReader(asset.text);
        string line;
        while ((line = reader.ReadLine()) != null) {
            if (!line.StartsWith("#")) {
                Match match = Regex.Match(line, "([\\w_.]+)=\"(.+)\"");
                if (match.Success) {
                    string key = match.Groups[1].Value;
                    string value = match.Groups[2].Value;

                    Debug.Log("Added " + key + " with value " + value);

                    _values.Add(key, value);
                }
            }
        }
    }

    public string GetMessage(string input, params string[] objects) {
        string str = _values.ContainsKey(input) ? _values[input] : input;
        if(input == str) Debug.LogError("Key " + input + " does not exist.");
        for (int i = 0; i < objects.Length; i++) {
            str = str.Replace("{val." + i + "}", objects[i] == null ? "null" : objects[i]);
        }

        return str;
    }
    
   /*public const string AreYouSureToQuit = "Are you sure you want to quit the game?";
    public const string AreYouSureToStartNewGame = "Starting a New Game overwrites your current progress. Are you sure?";
    
    // TODO replace with {key.Interact} and stuff
    public const string PressToGrabCube = "Press [E] to Grab Cube.";
    public const string PressToJump = "Press [SPACE] to Jump.";
    public const string PressToRestart = "Press [R] to Restart your Current Level.";
    public const string PressToInteract = "Press [E] to Interact.";
    public const string PressToSlow = "Hold [RIGHT MOUSE BUTTON] to Slow Time.";
    public const string PressToRewind = "Hold [LEFT MOUSE BUTTON] to Rewind Time.";*/
}
