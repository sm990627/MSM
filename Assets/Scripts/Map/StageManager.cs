using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    static int doornumber = 0;
    void Start()
    {
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");
        for (int i = 0; i < enters.Length; i++)
        {
            GameObject temp = enters[i];
            Gate exit = temp.GetComponent<Gate>();
            if (doornumber == exit.DoorNumber)
            {
                float x = temp.transform.position.x;
                float y = temp.transform.position.y;

                if (exit.Direction == ExitDirection.up)
                {
                    y += 0.5f;
                }
                else if (exit.Direction == ExitDirection.down)
                {
                    y -= 0.5f;
                }
                else if (exit.Direction == ExitDirection.left)
                {
                    x -= 0.5f;
                }
                else if (exit.Direction == ExitDirection.right)
                {
                    x += 0.5f;
                }
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y);

                break;
            }
        }


    }

    void Update()
    {

    }
    public static void ChangeScene(string scenename, int doornum)
    {
        doornumber = doornum;
        SceneManager.LoadScene(scenename);
    }
}

