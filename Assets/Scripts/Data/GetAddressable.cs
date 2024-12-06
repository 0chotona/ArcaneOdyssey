using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class GetAddressable : MonoBehaviour
{
    static GetAddressable _instance;

    public static GetAddressable Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GetAddressable>();
            return _instance;
        }
    }
    [Header("스킬 아이콘 라벨"), SerializeField] string _skillIconLabel = "SkillIcons";
    [Header("버프 아이콘 라벨"), SerializeField] string _buffIconLabel = "BuffIcons";
    [Header("캐릭터 아이콘 라벨"), SerializeField] string _charIconLabel = "CharIcons";
    Dictionary<string, Sprite> _skillIconDic = new Dictionary<string, Sprite>();
    Dictionary<string, Sprite> _buffIconDic = new Dictionary<string, Sprite>();
    Dictionary<string, Sprite> _charIconDic = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> _SkillIconDic => _skillIconDic;
    public Dictionary<string, Sprite> _BuffIconDic => _buffIconDic;
    public Dictionary <string, Sprite> _CharIconDic => _charIconDic;

    [Header("스킬 아이콘 리스트"), SerializeField] List<Sprite> _skillIconList;
    [Header("버프 아이콘 리스트"), SerializeField] List<Sprite> _buffIconList;
    [Header("캐릭터 아이콘 리스트"), SerializeField] List<Sprite> _charIconList;
    private void Awake()
    {
        /*
        LoadIcons(_skillIconLabel, SetSkillIconDic);
        LoadIcons(_buffIconLabel, SetBuffIconDic);
        LoadIcons(_charIconLabel, SetCharIconDic);
        */
        SetIconLists();
        UIManager.Instance.SetCharIcon();
    }
    void SetIconLists()
    {
        foreach(Sprite icon in _skillIconList)
        {
            _skillIconDic.Add(icon.name, icon);
        }
        foreach (Sprite icon in _buffIconList)
        {
            _buffIconDic.Add(icon.name, icon);
        }
        foreach (Sprite icon in _charIconList)
        {
            _charIconDic.Add(icon.name, icon);
        }
    }
    void SetSkillIconDic(Dictionary<string, Sprite> skillIconDic)
    {
        _skillIconDic = skillIconDic;
    }
    void SetBuffIconDic(Dictionary<string, Sprite> buffIconDic)
    {
        _buffIconDic = buffIconDic;
    }
    void SetCharIconDic(Dictionary<string, Sprite> charIconDic)
    {
        _charIconDic = charIconDic;
        //Debug.Log("SetCharIconDic : " + _charIconDic["Model_100"].name);

        UIManager.Instance.SetCharIcon();
    }
    public void LoadIcons(string labelName, Action<Dictionary<string, Sprite>> callback)
    {
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        AsyncOperationHandle<IList<IResourceLocation>> labelOperation = Addressables.LoadResourceLocationsAsync(labelName);
        labelOperation.Completed += (labelResponse) => {
            int totalCount = labelResponse.Result.Count;
            foreach (IResourceLocation item in labelResponse.Result)
            {
                AsyncOperationHandle<Sprite> resourceOperation = Addressables.LoadAssetAsync<Sprite>(item.PrimaryKey);
                resourceOperation.Completed += (result) =>
                {
                    totalCount--;
                    switch (labelResponse.Status)
                    {
                        case AsyncOperationStatus.Succeeded:
                            if (!sprites.ContainsKey(result.Result.name))
                            {
                                sprites.Add(result.Result.name, result.Result);
                            }
                            else
                            {
                                //Debug.LogWarning($"Duplicate key found: {result.Result.name}. Skipping this sprite.");
                            }
                            Addressables.Release(resourceOperation);
                            break;
                        case AsyncOperationStatus.Failed:
                            Debug.LogError("Failed to load audio clips.");
                            break;
                        default:
                            break;
                    }
                    // When we've finished loading all items in the directory, let's continue
                    if (totalCount == 0)
                    {
                        callback(sprites);
                    }
                };
            }
        };
    }

}
