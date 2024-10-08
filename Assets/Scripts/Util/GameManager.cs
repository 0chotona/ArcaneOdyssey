using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        } 
    }

    int _level = 1;
    public int _Level => _level;

    int _curStage = 1;
    public int _CurStage => _curStage;

    
    public void UpgradeLevel()
    {
        _level++;
        UIManager.Instance.ShowUpgradePanel();
    }
    public void NextStage()
    {
        _curStage++;
    }
}
