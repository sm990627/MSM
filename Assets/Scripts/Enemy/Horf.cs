using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horf : MonoBehaviour
{
    [Header("¿˚ Ω∫≈»")]
    [SerializeField] float _hp = 5.0f;
    [SerializeField] float _power = 1f;
    [SerializeField] float _attackSpeed = 3f;
    [SerializeField] float _bulletSpeed = 6.0f;
    [SerializeField] GameObject _bullet;
    bool _inAttack = false;
    Transform _playerTf;
    Animator _animator;
    GameObject player;
    AttackCon attcnt;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Head");
        attcnt = player.GetComponent<AttackCon>();
        _animator = GetComponent<Animator>();
        _playerTf = player.transform;
        Invoke("StopAttack", _attackSpeed);
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
            Destroy(gameObject);
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
        GameObject bulletprefab = Instantiate(_bullet, VI, Quaternion.identity);
        Rigidbody2D rb = bulletprefab.GetComponent<Rigidbody2D>();
        //πﬂªÁ ∫§≈Õ  
        Vector2 dirVector = (_playerTf.position - transform.position).normalized;
        rb.AddForce(dirVector * _bulletSpeed, ForceMode2D.Impulse);

    }
    void StopAttack()
    {
        _inAttack = true;
        _animator.SetBool("inAttack", true);
    }
 

}