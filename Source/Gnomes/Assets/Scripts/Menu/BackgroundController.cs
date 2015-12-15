using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    private Animator anim;

    public bool IsPause
    {
        get { return anim.GetBool("IsPause"); }
        set { anim.SetBool("IsPause", value); }
    }

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
