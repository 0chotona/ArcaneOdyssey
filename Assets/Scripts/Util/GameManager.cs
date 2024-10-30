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

    [Header("플레이어 체력"), SerializeField] PlayerHealth _playerHealth;
    public void UpgradeLevel()
    {
        _level++;
        UIManager.Instance.ShowUpgradePanel();
    }
    public void SetCharacter(CChar cChar)
    {
        _selectedChar = cChar;
    }
    public void EnemyDamage(float amount)
    {
        if(_selectedChar._charType == eCHARACTER.Chise)
        {
            _playerHealth.SetShield(amount * 0.25f, 1f);
        }
        
    }
    public void NextStage()
    {
        _curStage++;
    }
}
