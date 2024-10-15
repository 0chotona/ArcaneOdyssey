using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("�ִ� ü��"), SerializeField] int _maxHp = 10;
    [Header("���� ü��"), SerializeField] float _curHp;

    [Header("����"),SerializeField] int _def;

    [Header("Hp ��"), SerializeField] Slider _hpSlider;
    bool _isDead;
    public bool _IsDead => _isDead;

    [SerializeField] GameObject _jewelObj;
    Color[] _jewelColor = new Color[3] { Color.green, Color.cyan, Color.yellow };

    Transform _spawnerTrs;
    GameObject _tmpObj;

    bool _isPlanted = false;

    GameObject _explodeObj = null;
    float _explodeDamage = 0f;

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
        {
            _hpSlider.value = _curHp;
        }

        if (_curHp <= 0)
        {
            if(_isPlanted)
            {
                GameObject explode = Instantiate(_explodeObj, transform.position, Quaternion.identity);
                Explosion explosion = explode.GetComponent<Explosion>();
                explosion.UpdateDamage(_explodeDamage);
                explosion.Explode();
                explosion.DestroyObj(1f);
            }
            Dead();
        }

        Debug.Log(damage);
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
    public void SetPlant(bool isPlanted)
    {
        _isPlanted = isPlanted;
    }
    public void PlantExplosion(GameObject explodeObj, float damage)
    {
        _explodeObj = explodeObj;
        _explodeDamage = damage;
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

        PassiveManager.Instance.UpdateDeathCount();
        Destroy(_tmpObj);
    }
    public float GetHpPercent()
    {
        float percent = 0;
        percent = _curHp / _maxHp * 100;
        return percent;
    }
}
