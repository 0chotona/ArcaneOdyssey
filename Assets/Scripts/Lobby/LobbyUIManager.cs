using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    static LobbyUIManager _instance;

    public static LobbyUIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<LobbyUIManager>();
            return _instance;
        }

    }

    List<CChar> _charList = new List<CChar>();
    [Header("상품 창"),SerializeField] GameObject _productPanel;
    [Header("초상화 리스트"), SerializeField] List<Sprite> _productImgList;
    [SerializeField] Transform _contentTrs;

    GameObject _spawnedObj = null;
    [Header("모델링"), SerializeField] List<GameObject> _charModels;
    [SerializeField] Transform _modelSpawnPos;
    [Header("게임 시작 버튼"), SerializeField] Button _gameStartButton;

    private void Awake()
    {
        _gameStartButton.onClick.AddListener(() => Click_StartGame());
    }
    public void SetProductInfos(List<CChar> charList)
    {
        _charList = charList;
        for(int i = 0; i < _charList.Count; i++)
        {
            GameObject panel = Instantiate(_productPanel, _contentTrs);
            ProductInfo info = panel.GetComponent<ProductInfo>();

            Sprite img = GetImgByID(_charList[i]._modelName);
            info.SetInfo(img, _charList[i]);
            info.SetShopIndex(i);
        }
    }
    public void SpawnModelObj(string modelName)
    {
        if (_spawnedObj != null)
            Destroy(_spawnedObj);
        GameObject modelObj = GetModelObjByName(modelName);
        _spawnedObj = Instantiate(modelObj, _modelSpawnPos);
    }
    Sprite GetImgByID(string id)
    {
        Sprite targetImg = null;
        foreach(Sprite img in _productImgList)
        {
            if(img.name == id)
            {
                targetImg = img;
                break;
            }
        }
        return targetImg;
    }
    public void Click_Select(CChar cchar)
    {
        SpawnModelObj(cchar._modelName);
        CharacterSelector.Instance.SetSelectedChar(cchar);
    }

    void Click_StartGame()
    {
        CharacterSelector.Instance.StartGame();
    }
    GameObject GetModelObjByName(string modelName)
    {
        GameObject modelObj = null;
        foreach (GameObject obj in _charModels)
        {
            if (obj.name == modelName)
            {
                modelObj = obj;
                break;
            }
        }
        return modelObj;
    }
}
