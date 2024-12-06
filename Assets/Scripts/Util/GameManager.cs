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
    [Header("시작 딜레이"), SerializeField] float _startDelay = 3f;
    [Header("카메라 이동"), SerializeField] MoveCamera _moveCam;

    [Header("플레이어 이동"), SerializeField] PlayerMove _playerMove;
    private void OnEnable()
    {
        CharacterSelector.Instance.StartGame();
        //StartCoroutine(CRT_SetDelay(_startDelay));
        _moveCam.MoveStartAction(_startDelay);
        _enemyFactory.StartTestPatern(_startDelay);
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
    IEnumerator CRT_SetDelay(float delay)
    {
        _playerMove.SetCanMove(false);
        //UIManager.Instance.SetUIActive(false);
        yield return new WaitForSeconds(delay);
        UIManager.Instance.SetUIActive(true);
        //_playerMove.SetCanMove(true);
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
