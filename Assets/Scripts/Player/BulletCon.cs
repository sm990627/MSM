using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCon : MonoBehaviour
{
    float deleteTime;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Head");
        AttackCon attcnt = player.GetComponent<AttackCon>();
        deleteTime = attcnt.GetRange() / attcnt.GetBulletSpeed();
        Destroy(gameObject, deleteTime);

    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
