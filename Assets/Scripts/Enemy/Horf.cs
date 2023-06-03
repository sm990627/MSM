using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horf : MonoBehaviour
{
    [Header("¿˚ Ω∫≈»")]
    [SerializeField] float _hp = 5.0f;
    [SerializeField] float _attackSpeed = 3f;
    [SerializeField] float _bulletSpeed = 6.0f;
    [SerializeField] GameObject _bullet;
    GameObject[] _bulletPool;
    GameObject _bulletParent;
    int _poolIndex;
    bool _inAttack = false;
    Transform _playerTf;
    Animator _animator;
    GameObject player;
    AttackCon attcnt;
    private void Start()
    {
        _bulletPool = new GameObject[5];
        _bulletParent = GameObject.FindWithTag("Pool");
        player = GameObject.Find("PlayerHead");
        attcnt = player.GetComponent<AttackCon>();
        _animator = GetComponent<Animator>();
        _playerTf = player.transform;
        Invoke("StopAttack", _attackSpeed);
        for (int i = 0; i < _bulletPool.Length; i++)
        {
            GameObject gameObject = Instantiate(_bullet, _bulletParent.transform);
            _bulletPool[i] = gameObject;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_inAttack)
        {
            _inAttack = false;
            _animator.SetBool("inAttack", false);
            Invoke("StopAttack", _attackSpeed);
        }
        
    }
    void OnDamage()
    {
        _hp = _hp - attcnt.GetPower();
        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            OnDamage();
        }
    }
    void Attack()
    {
        //ª˝º∫ ¿ßƒ° ∫§≈Õ
        Vector3 VI = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //GameObject bulletprefab = Instantiate(_bullet, VI, Quaternion.identity);
        _bulletPool[_poolIndex].SetActive(true);
        _bulletPool[_poolIndex].transform.position = VI;
        Rigidbody2D rb = _bulletPool[_poolIndex++].GetComponent<Rigidbody2D>();
        IndexReset();
        //πﬂªÁ ∫§≈Õ  
        Vector2 dirVector = (_playerTf.position - transform.position).normalized;
        rb.AddForce(dirVector * _bulletSpeed, ForceMode2D.Impulse);

    }
    void StopAttack()
    {
        _inAttack = true;
        _animator.SetBool("inAttack", true);
    }
    void IndexReset()
    {
        if (_poolIndex == 5) _poolIndex = 0;
    }


}