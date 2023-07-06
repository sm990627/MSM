using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;


public enum ExitDirection
{
    right,
    left,
    down,
    up,
}

public class Gate : MonoBehaviour
{
    [SerializeField] string sceneName = "";
    [SerializeField] int doornumber = 0;
    [SerializeField] ExitDirection direction = ExitDirection.down;
    public int DoorNumber { get { return doornumber; } set {  doornumber = value; } }
    public ExitDirection Direction { get { return direction; } } 
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
             StageManager.ChangeScene(sceneName, doornumber);

        }
    }
    
}
