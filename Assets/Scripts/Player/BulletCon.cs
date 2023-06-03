using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCon : MonoBehaviour
{
    float deleteTime;
    void Start()
    {
        GameObject player = GameObject.Find("PlayerHead");
        AttackCon attcnt = player.GetComponent<AttackCon>();
        deleteTime = attcnt.GetRange() / attcnt.GetBulletSpeed();
        Invoke("BulletDestroy", deleteTime);

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
