using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //Sound will play when picking up a checkpoint
    private Transform currentChecpoint; //Store last checkpoint here
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        //Check if checkpoint available
        if(currentChecpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();
            GameObject.Destroy(gameObject);

            return; //Don't execute the rest of this function
        }

        playerHealth.Respawn(); //Restore player health and reset animation
        transform.position = currentChecpoint.position;//Move player to checkpoint position

        //Move camera to checkpoint room (for this to work the checkpoint objects has to placed as a child of the room object)
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentChecpoint.parent);

    }

    //Activate checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            currentChecpoint = collision.transform;//Store the checkpoint that we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;//Deactivae checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear");//Trigger checkpoint animation
        }
    }
}
