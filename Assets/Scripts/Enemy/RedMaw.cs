using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMaw : MonoBehaviour
{
    [Header("Àû ½ºÅÈ")]
    [SerializeField] float _speed = 3.0f;
    [SerializeField] float _hp = 5.0f;
    [SerializeField] Rigidbody2D _rig;
    Transform _playerTf;
    GameObject player;
    AttackCon attcnt;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Head");
        attcnt = player.GetComponent<AttackCon>();
        _playerTf = player.transform;
    }

    void Update()
    {
        Vector2 dirVector = (_playerTf.position - transform.position).normalized;
        _rig.velocity = dirVector * _speed;
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
}
