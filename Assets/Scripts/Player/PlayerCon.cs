using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCon : MonoBehaviour
{
    [Header("플레이어 이동스탯")]
    [SerializeField] float _hp = 3.0f;
    [SerializeField] float _speed = 4.0f;
    float axisH;
    float axisV;
    float angleM;
    bool inDamage = false;
    Rigidbody2D _rbody;
    GameObject _head;
    [SerializeField] GameObject _bomb;
    [SerializeField] GameObject bullet;

    [Header("플레이어 공격스탯")]
    [SerializeField] float _power = 1.0f;
    [SerializeField] float _attackSpeed = 0.4f;
    [SerializeField] int _bulletCnt = 1;
    [SerializeField] float _range = 8.0f;
    [SerializeField] float _bulletSpeed = 6.0f;


    
    string upAnime = "PlayerUp";
    string downAnime = "PlayerDown";
    string rightAnime = "PlayerRight";
    string idleAnime = "PlayerIdle";

    SpriteRenderer _rend;
    void Start()
    {
        _head = GameObject.FindGameObjectWithTag("Head");
        _rbody = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        AttackCon attCon = _head.GetComponent<AttackCon>();
        attCon.Init(_power,_attackSpeed,_bulletCnt,_range,_bulletSpeed,bullet);
    }
    
    void Update()
    {
        
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleM = GetAngleM(fromPt, toPt);

        if (axisH != 0 || axisV != 0)
        {
            if (angleM >= -45 && angleM < 45)
            {
                GetComponent<Animator>().Play(rightAnime);
                _rend.flipX = false;
            }
            else if (angleM >= 45 && angleM <= 135)
            {
                GetComponent<Animator>().Play(upAnime);
            }
            else if (angleM >= -135 && angleM <= -45)
            {
                GetComponent<Animator>().Play(downAnime);
            }
            else
            {
                GetComponent<Animator>().Play(rightAnime);
                _rend.flipX = true;
            }

        }
        else
        {
            GetComponent<Animator>().Play(idleAnime);
        }
        

        if (Input.GetButtonDown("Bomb"))
        {
            SetBomb();
        }
       
        
    }
    void FixedUpdate()
    {
        if (inDamage)
        {
            float val = Mathf.Sin(Time.time * 50);

            if (val > 0)
            {   
                _head.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                _head.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            
        }
        else
        {
            //if (Mathf.Abs(axisH) > 0f || Mathf.Abs(axisV) > 0)
            {
                _rbody.velocity = new Vector2(axisH, axisV) * _speed;
            }
           
        }

        //_rbody.AddForce (new Vector2(axisH, axisV) * _speed);
        
    }

    
    //벡터 두개를 받아 이동각 계산
    float GetAngleM(Vector2 v1, Vector2 v2)
    {
        float angle;
        if (axisH != 0 || axisV != 0)
        {
            float dx = v2.x - v1.x;
            float dy = v2.y - v1.y;
            float rad = Mathf.Atan2(dy, dx);
            angle = Mathf.Rad2Deg * rad;
            
        }
        else
        {
            angle = angleM;
        }
        return angle;
    }
    
    void SetBomb()
    {
        GameObject bombprefab = Instantiate(_bomb, transform.position, Quaternion.identity);
    }
    void OnDamage(GameObject enemy)
    {
        _hp--;
        if (_hp > 0)
        {
            Debug.Log("밀려남");
            _rbody.velocity = new Vector2(0, 0);
            Vector2 dirVector = (transform.position - enemy.transform.position).normalized;
            _rbody.AddForce(new Vector2(dirVector.x * 4 , dirVector.y * 4), ForceMode2D.Impulse);
            inDamage = true;
            gameObject.GetComponent<Animator>().SetBool("OnDamage",true);
            Invoke("DamageEnd", 0.2f);
        }
        else
        {
             gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            OnDamage(collision.gameObject);
        }
    }
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<Animator>().SetBool("OnDamage", false);
        _head.GetComponent<SpriteRenderer>().color= new Color(255,255,255,255);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

    }
    public Vector2 GetAxis()
    {
        Vector2 axis = new Vector2(axisH, axisV);
        return (axis);
    }
}
