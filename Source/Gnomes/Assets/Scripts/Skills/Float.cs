using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour
{

    private Rigidbody rb;
    public float floatvelocity = 0.8f;
    private bool floating = false;
    private int playerNum;
    public Animation anim;
    private bool isactive;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerNum = GetComponent<PlayerController>().playerNum;
        anim = GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        isactive = GetComponent<PlayerController>().enabled;
        if (Input.GetButtonDown("Item" + playerNum) && isactive)
        {

            if (floating != true)
            {
                floating = true;
            }
            else
            {
                floating = false;
            }
        }
        if (floating && rb.velocity.y < -floatvelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, -floatvelocity, rb.velocity.z);
        }
        if (floating)
        {
            anim.Play("Zweven");
        }
    }
}
