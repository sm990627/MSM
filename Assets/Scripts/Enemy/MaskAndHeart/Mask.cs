using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] GameObject _heart;
   
    [SerializeField] float _moveSpeed = 3;
    Rigidbody2D _rig;
    void Start()
    {
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
