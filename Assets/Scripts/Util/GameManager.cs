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

    CChar _selectedChar = new CChar();
    public CChar _SelectedChar => _selectedChar;

    [SerializeField] EnemyFactory _enemyFactory;
    public void UpgradeLevel()
    {
        _level++;
        UIManager.Instance.ShowUpgradePanel();
    }
    public void SetCharacter(CChar cChar)
    {
        _selectedChar = cChar;
    }
    /*
    public void SetMob(EnemyData data)
    {
        _enemyFactory.SetData(data);

    }
    */
    public void NextStage()
    {
        _curStage++;
    }
}
