using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] GameObject _heart;
   
    [SerializeField] float _moveSpeed = 3;
    Rigidbody2D _rig;
    Transform _player;
    void Start()
    {
        GameObject player = GameObject.Find("PlayerHead");
        _player = player.transform;
        _rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_heart != null)
        {
            
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
