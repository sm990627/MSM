using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCon : MonoBehaviour
{
    void Start()
    {
        Invoke("BulletDestroy",2f);

    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletDestroy();
    }
    void BulletDestroy()
    {
        gameObject.SetActive(false);
    }
}
