using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string roomLevel;

    public void LoadNewRoom()
    {
        SceneManager.LoadScene(roomLevel);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
            LoadNewRoom();
        }   
    }
}
