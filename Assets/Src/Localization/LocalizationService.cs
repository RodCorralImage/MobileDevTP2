using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class LocalizationService {

    static readonly SystemLanguage[] SupportedLanuguages = new SystemLanguage[6] {
        SystemLanguage.English,
        SystemLanguage.Spanish,
        SystemLanguage.French,
        SystemLanguage.Japanese,
        SystemLanguage.Portuguese,
        SystemLanguage.Italian
    };



    static LocalizationService instance = null;
    public static LocalizationService Instance {
        get {
            if (instance == null) {
                instance = new LocalizationService();
                instance.Init(Application.systemLanguage);
            }
            return instance;
        }
    }

    const string FILE_NAME = "Localization";

    Dictionary<string, string> table = new Dictionary<string, string>();

    SystemLanguage currenLang;


    void Init(SystemLanguage iniLang) {
        if(System.Array.Find(SupportedLanuguages, e => e == iniLang) == iniLang)
            currenLang = iniLang;
        else
            currenLang = SupportedLanuguages[0];

        LoadTable(currenLang);
    }

    public static void WriteExampleFile() {
        LocalizedTextNode node = new LocalizedTextNode();
        node.key = "key";
        node.texts = new string[SupportedLanuguages.Length];
        for (int i = 0; i < SupportedLanuguages.Length; i++) {
            node.texts[i] = SupportedLanuguages[i].ToString();
        }
        LocalizedTextNodeList nodeList = new LocalizedTextNodeList();
        nodeList.nodes = new LocalizedTextNode[1] {
            node
        };
        string txt = JsonUtility.ToJson(nodeList);
        string path = Path.Combine(Application.dataPath, "Resources", FILE_NAME + ".json");
        Debug.Log("path: " + path);
        Debug.Log("txt: " + txt);
        File.WriteAllText(path, txt);
    }

    void LoadTable(SystemLanguage lang) {
        var jsonTextFile = Resources.Load<TextAsset>(FILE_NAME);
        LocalizedTextNodeList nodes = JsonUtility.FromJson<LocalizedTextNodeList>(jsonTextFile.text);

        //busca el indice del lang
        int idx = 0;
        for (int i = 0; i < SupportedLanuguages.Length; i++) {
            if (lang == SupportedLanuguages[i]) {
                idx = i;
                break;
            }
        }

        //carga la table
        table.Clear();
        foreach (var item in nodes.nodes) {
            table.Add(item.key, item.texts[idx]);
        }
    }

    public string LocalizeString(string key) {
        if (table.ContainsKey(key)) {
            return table[key];
        }
        else {
            return key;
        }
    }

    Dictionary<int, LocalizedTextData> localizationRefs = new Dictionary<int, LocalizedTextData>();

    public void LocalizeGameObject(GameObject go) {
        foreach (Text t in go.GetComponentsInChildren<Text>(true)) {
            string id = t.text;
            t.text = LocalizeString(id);

            if (!localizationRefs.ContainsKey(t.GetInstanceID())) {
                localizationRefs[t.GetInstanceID()] = new LocalizedTextData(t, id);
            }
        }
    }

    public void ChangeLang(SystemLanguage newLang) {
        currenLang = newLang;
        LoadTable(newLang);
        ReLocalize();
    }

    void ReLocalize() {
        List<int> removeList = new List<int>();
        foreach (var locRef in localizationRefs) {
            if (locRef.Value.textRef.IsAlive && locRef.Value.textRef.Target.Equals(null)) {
                var target = locRef.Value.textRef.Target;
                if (target is Text) {
                    Text t = target as Text;
                    t.text = LocalizeString(locRef.Value.id);
                }
            }
            else {
                removeList.Add(locRef.Key);
            }
        }

        foreach (var key in removeList) {
            localizationRefs.Remove(key);
        }
    }

    public class LocalizedTextData {
        public System.WeakReference textRef;
        public string id;

        public LocalizedTextData(Text t, string id) {
            textRef = new System.WeakReference(t);
            this.id = id;
        }
    }


}