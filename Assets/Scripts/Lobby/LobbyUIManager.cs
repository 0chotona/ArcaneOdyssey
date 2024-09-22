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
    [Header("��ǰ â"),SerializeField] GameObject _productPanel;
    [Header("�ʻ�ȭ ����Ʈ"), SerializeField] List<Sprite> _productImgList;
    [SerializeField] Transform _contentTrs;
    [SerializeField] CharacterSelector _characterSelector;

    GameObject _spawnedObj = null;
    [Header("�𵨸�"), SerializeField] List<GameObject> _charModels;
    [SerializeField] Transform _modelSpawnPos;
    [Header("���� ���� ��ư"), SerializeField] Button _gameStartButton;

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
        _characterSelector.SetSelectedChar(cchar);
    }

    void Click_StartGame()
    {
        _characterSelector.StartGame();
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
