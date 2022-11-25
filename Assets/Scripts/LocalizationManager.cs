using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LocalizationManager : MonoBehaviour {
    private static LocalizationManager _instance;
        
    public static LocalizationManager Instance {
        get {
            if(_instance == null) {
                return _instance = new LocalizationManager();
            }
            return _instance;
        }
    }

    [SerializeField]
    public TextAsset[] files {
        get { return _languageFiles; }
        set { _languageFiles = value; }
    }
    
    private static TextAsset[] _languageFiles;
    private Dictionary<string, string> _localizedText;
    
    /*public void LoadLocalization(string lang) {
        PlayerPrefs.SetString("language", lang);
        _localizedText = new Dictionary<string, string> ();
        string filePath = Path.Combine (Application.streamingAssetsPath, "language_" + lang + ".json");

        if (File.Exists(filePath)) {
            string dataAsJson = File.ReadAllText (filePath);
            /*LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (dataAsJson);
            for (int i = 0; i < loadedData.items.Length; i++) {
                localizedText.Add(loadedData.items [i].key, loadedData.items [i].value);
            }*/
            /*JSONNode data = JSON.Parse(dataAsJson);
            foreach(JSONNode key in data.Keys) {
                string k = key;
                _localizedText.Add(key, data[k]);
            }*/
            /* foreach(JSONNode key in data) {
                 Debug.Log("Adding " + data.Value + ", " + key + ", " + key.);
                 localizedText.Add(key, key.Value);
             }*
            Debug.Log ("Data loaded (" + language + "), dictionary contains: " + _localizedText.Count + " entries");
            var objs = GameObject.FindObjectsOfType<TMProLocalize>();
            foreach (TMProLocalize go in objs) {
                go.Translate();
            }
        } else {
            Debug.LogError ("Cannot find file! Loading English...");
            LoadLocalization("en");
        }
    }*/
}
