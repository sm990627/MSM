using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PlayerCon : MonoBehaviour
{
    [Header("플레이어 스탯")]
    [SerializeField] float  _maxTotalHp = 12;
    [SerializeField] float _maxHp = 3;
    [SerializeField] float _speed = 4.0f;
    [SerializeField] float _power = 1.0f;
    [SerializeField] float _attackSpeed = 0.4f;
    [SerializeField] int _bulletCnt = 1;
    [SerializeField] float _range = 8.0f;
    [SerializeField] float _bulletSpeed = 6.0f;
    [SerializeField] float _hp = 3;

    public static PlayerCon instance;

    //상태관련 변수
    float axisH;
    float axisV;
    float angleM;
    bool inDamage = false;
    bool itemGain = false;
    GameObject newItem;

    //사용할 컴포넌트
    Rigidbody2D _rbody;
    GameObject _player;
    SpriteRenderer _rend;
    AttackCon _attCon;
    GameObject _hpBar;
    HpBarCon _hbc;
    ItemManger im = new ItemManger();
    PlayerStat pStat;

    //생성할 오브젝트
    [SerializeField] GameObject _bomb;
    [SerializeField] GameObject bullet;

    //이동 애니메이션
    string upAnime = "PlayerUp";
    string downAnime = "PlayerDown";
    string rightAnime = "PlayerRight";
    string idleAnime = "PlayerIdle";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        pStat = new PlayerStat(_maxHp,_maxTotalHp,_hp,_speed,_power,_attackSpeed,_bulletCnt,_range,_bulletSpeed);
        _hpBar = GameObject.Find("Canvas");
        _hbc =_hpBar.GetComponent<HpBarCon>();
        pStat.Hp = pStat.MaxHp;
        _hbc.Init(pStat.MaxHp, _maxTotalHp, pStat.Hp);
        _player = GameObject.Find("PlayerHead");
        _attCon = _player.GetComponent<AttackCon>();
        _attCon.Init(pStat,bullet);
    }
    public void Hitted(float dmg)
    {
        //내 플레이어 스텟에서 적당한 데미지를 뺀다.
    }
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();       
    }
    
    void Update()
    { 
        
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleM = GetAngleM(fromPt, toPt);
        if (!itemGain)
        {
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
                _player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                _player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            
        }
        else if (itemGain)
        {
            _rbody.velocity = new Vector2(0, 0);
        }
        else 
        {
            //if (Mathf.Abs(axisH) > 0f || Mathf.Abs(axisV) > 0)
            {
                _rbody.velocity = new Vector2(axisH, axisV) * pStat.Speed;
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
        if (!inDamage)
        {
            pStat.Hp = pStat.Hp - 0.5f;
            _hbc.Init(pStat.MaxHp, _maxTotalHp, pStat.Hp);
            _hbc.HpCon();
            if (pStat.Hp > 0)
            {

                _rbody.velocity = new Vector2(0, 0);
                Vector2 dirVector = (transform.position - enemy.transform.position).normalized;
                _rbody.AddForce(new Vector2(dirVector.x * 3, dirVector.y * 3), ForceMode2D.Impulse);
                inDamage = true;
                gameObject.GetComponent<Animator>().SetBool("OnDamage", true);
                Invoke("DamageEnd", 0.25f);
                Debug.Log(pStat.MaxHp+" "+_maxTotalHp+" "+pStat.Hp);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
       
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            OnDamage(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            newItem = collision.gameObject;
            itemGain = true;
            collision.transform.parent = transform;
            collision.transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f); //아이템 머리위로
            gameObject.GetComponent<Animator>().SetBool("ItemGain",true);  

            im.AddItem(pStat, collision.GetComponent<ItemIdx>().Idx); //아이템 정보전달

             //눈물갯수와 hp정보 전달
            _attCon.Init(pStat, bullet);
            _hbc.Init(pStat.MaxHp, _maxTotalHp, pStat.Hp);
            _hbc.MaxHpCon();
            _hbc.HpCon();
            Debug.Log(pStat.MaxHp + " " + _maxTotalHp + " " + pStat.Hp);


            Invoke("ItemGainEnd", 1.5f);  //애니메이션끄기
            _player.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<Animator>().SetBool("OnDamage", false);
        _player.GetComponent<SpriteRenderer>().color= new Color(255,255,255,255);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);

    }
    public Vector2 GetAxis()
    {
        Vector2 axis = new Vector2(axisH, axisV);
        return (axis);
    }
    void ItemGainEnd()
    {
        newItem.SetActive(false);
        itemGain = false;
        gameObject.GetComponent<Animator>().SetBool("ItemGain", false);
        _player.GetComponent<SpriteRenderer>().enabled = true;
    }

}

