using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    private Animator anim;
    private CanvasGroup canvgroup;

    public bool IsOpen
    {
        get { return anim.GetBool("IsOpen"); }
        set { anim.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        anim = GetComponent<Animator>();
        canvgroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            canvgroup.blocksRaycasts = canvgroup.interactable = false;
        }
        else
        {
            canvgroup.blocksRaycasts = canvgroup.interactable = true;
        }
    }
}
