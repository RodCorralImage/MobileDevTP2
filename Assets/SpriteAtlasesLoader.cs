using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.U2D;

public class SpriteAtlasesLoader {
    public enum AtlasVariant {
        Hd,
        Sd
    }

    AtlasVariant _variant;

    static readonly Dictionary<AtlasVariant, string> ATLASES_DIR = new Dictionary<AtlasVariant, string> {
                    { AtlasVariant.Hd, "SpriteAtlases/hd" },
                    { AtlasVariant.Sd, "SpriteAtlases/sd" }
                };

    Dictionary<string, SpriteAtlas> _atlases = new Dictionary<string, SpriteAtlas>();
    Dictionary<string, List<Action<SpriteAtlas>>> _atlasesLoading = new Dictionary<string, List<Action<SpriteAtlas>>>();

    public SpriteAtlasesLoader(AtlasVariant variant) {
        _variant = variant;
        SpriteAtlasManager.atlasRequested += AtlasRequested;
    }
    public void Release() {
        SpriteAtlasManager.atlasRequested -= AtlasRequested;

        foreach (var atlas in _atlases)
            if (atlas.Value != null)
                Resources.UnloadAsset(atlas.Value);
        Resources.UnloadUnusedAssets();

        _atlases.Clear();
    }

    public void ForceLoadAtlas(string tag) {
        AtlasRequested(tag, null);
    }

    public void ForceUnloadAtlas(string atlasName, bool unloadUnusedAssets = true) {
        if (string.IsNullOrEmpty(atlasName))
            return;

        string atlasPath = GetAtlasPath(atlasName, _variant);
        if (_atlases.TryGetValue(atlasPath, out SpriteAtlas atlas) && atlas != null) {
            Resources.UnloadAsset(atlas);
            _atlases.Remove(atlasPath);

            if (unloadUnusedAssets)
                Resources.UnloadUnusedAssets();
        }
    }

    void AtlasRequested(string tag, Action<SpriteAtlas> callback) {
        string atlasPath = GetAtlasPath(tag, _variant);
        if (_atlases.TryGetValue(atlasPath, out SpriteAtlas atlas)) { //the atlas is already loaded
            callback?.Invoke(atlas);
        }
        else if (_atlasesLoading.ContainsKey(atlasPath)) { //the atlas is loading right now
            _atlasesLoading[atlasPath].Add(callback);
        }
        else {
            _atlasesLoading.Add(atlasPath, new List<Action<SpriteAtlas>>());
            _atlasesLoading[atlasPath].Add(callback);

            Resources.LoadAsync<SpriteAtlas>(atlasPath).completed += (aop) => { //the atlas has to be loaded
                ResourceRequest req = (ResourceRequest)aop;

                List<Action<SpriteAtlas>> callbacksList = new List<Action<SpriteAtlas>>();
                callbacksList.AddRange(_atlasesLoading[atlasPath].ToArray());

                _atlasesLoading.Remove(atlasPath);

                if (req.asset == null) {
                    Debug.LogError($"A Sprite Atlas not found - {atlasPath}");
                    callbacksList.ForEach(c => c?.Invoke(null));
                }
                else {
                    SpriteAtlas newAtlas = (SpriteAtlas)((ResourceRequest)aop).asset;
                    if (!_atlases.ContainsKey(atlasPath)) {
                        _atlases.Add(atlasPath, newAtlas);
                    }
                    else {
                        Debug.LogError($"{atlasPath} is already loaded");
                        Resources.UnloadAsset(newAtlas);
                        newAtlas = _atlases[atlasPath];
                    }

                    callbacksList.ForEach(c => c?.Invoke(newAtlas));
                }
            };
        }
    }

    static string GetAtlasPath(string atlas, AtlasVariant variant) {
        string path = Path.Combine(ATLASES_DIR[variant], atlas);
        return path;
    }
}