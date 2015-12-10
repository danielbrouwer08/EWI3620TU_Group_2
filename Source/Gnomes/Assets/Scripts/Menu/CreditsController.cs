using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public float speed;
    private RectTransform rect;
    public MenuController Menu;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.pivot = new Vector2(0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Menu.IsOpen == true)
        {
            CreditsScrolling();
        }
        else
        {
            rect.pivot = new Vector2(0.5f, 1);
        }
    }

    void CreditsScrolling()
    {
        if (rect.pivot.y > -0.5f)
        {
            rect.pivot -= new Vector2(0, speed * 0.01f);
        }
        else
        {
            rect.pivot = new Vector2(0.5f, 1);
        }
    }
}
