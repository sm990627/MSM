using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMaw : MonoBehaviour
{
    [Header("�� ����")]
    [SerializeField] float _moveSpeed = 3.0f;
    [SerializeField] float _hp = 5.0f;
    Rigidbody2D _rig;
    Transform _playerTf;
    GameObject player;
    AttackCon attcnt;
    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PlayerHead");
        attcnt = player.GetComponent<AttackCon>();
        _playerTf = player.transform;
    }

    void Update()
    {
        Vector2 dirVector = (_playerTf.position - transform.position).normalized;
        _rig.velocity = dirVector * _moveSpeed;
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
}
