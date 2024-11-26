using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void OnEnable()
    {
        CharacterSelector.Instance.StartGame();
    }
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
    public void ClearStage()
    {
        int deathCount = PassiveManager.Instance._DeathCount;
        UIManager.Instance.ShowResult(DataHandler.Instance._CurStage, deathCount);
        _curStage++;
        Time.timeScale = 0f;
    }
}
