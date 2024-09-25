using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("�ִ� ü��"), SerializeField] int _maxHp = 10;
    public float _curHp;

    [Header("����"),SerializeField] int _def;

    [Header("Hp ��"), SerializeField] Slider _hpSlider;
    bool _isDead;
    public bool _IsDead => _isDead;

    [SerializeField] GameObject _jewelObj;
    Color[] _jewelColor = new Color[3] { Color.green, Color.cyan, Color.yellow };

    Transform _spawnerTrs;
    GameObject _tmpObj;

    private void Awake()
    {
        _isDead = false;
        

        _tmpObj = gameObject;
        
    }
    private void Start()
    {
        if(_hpSlider != null)
        {
            _hpSlider.maxValue = _maxHp;
            _hpSlider.value = _curHp;
        }
        
    }
    public void LoseDamage(float damage)
    {
        _curHp -= damage;
        if (_hpSlider != null)
            _hpSlider.value = _curHp;
        if (_curHp <= 0)
            Dead();
    }
    public void GetBurn(int damage)
    {
        StartCoroutine(CRT_Burn(damage));
    }
    IEnumerator CRT_Burn(int damage)
    {
        for(int i = 0; i < 5; i++)
        {
            LoseDamage(damage);
            yield return new WaitForSeconds(1f);
        }
    }
    public void SetSpawner(Transform spawnerTrs)
    {
        _spawnerTrs = spawnerTrs;
    }
    public void SetEnemyStat(int hp, int def)
    {
        _maxHp = hp;
        _curHp = _maxHp;

        _def = def;
    }
    void SpawnJewel()
    {
        float rnd = Random.value;
        if(rnd > 0.3f)
        {
            GameObject jewel = Instantiate(_jewelObj, transform.position, Quaternion.identity, _spawnerTrs);
            Renderer renderer = jewel.GetComponent<Renderer>();
            renderer.material.color = _jewelColor[(int)(GameManager.Instance._Level * 0.1f)];
            
        }
    }
    void Dead()
    {
        SpawnJewel();
        _isDead = true;
        
        Destroy(_tmpObj);
    }
}
