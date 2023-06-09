using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _hp = 5;
    Rigidbody2D _rig;
    Transform _playerTf;
    GameObject _player;
    AttackCon _attcnt;
    private void Start()
    {
        _rig =GetComponent<Rigidbody2D>();
        _player = GameObject.Find("PlayerHead");
        _attcnt = _player.GetComponent<AttackCon>();
        _playerTf = _player.transform;
    }

    void Update()
    {
        Vector2 dirVector = (_playerTf.position - transform.position).normalized;
        _rig.velocity = -dirVector * _moveSpeed;
    }
    void OnDamage()
    {

        _hp = _hp - _attcnt.GetPower();
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
}