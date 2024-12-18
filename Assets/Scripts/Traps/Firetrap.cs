using UnityEngine;
using System.Collections;


public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;


    [Header("Firetrap Timers")]
    [SerializeField]private float activationDelay;
    [SerializeField]private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;//when the trap get triggered
    private bool active;//when trap is active and can hurt the player

    private Health playerHealth;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = GetComponent<Health>();
            if (!triggered)
                //trigger the firetrap
                StartCoroutine(ActivateFiretrap());
            
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = null;
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        //Turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red; 

        //Wait for delay, ativate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; //Turn the sprite back to its initial color
        active = true;
        anim.SetBool("activated", true);

        //Wait until X seconds, deactivate trap, reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }
}
