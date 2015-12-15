using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour {

    public Image background;
    public Image Fill;
    private Slider healthbar;
    
    void Awake()
    {
        healthbar = GetComponent<Slider>();
    }	

	// Update is called once per frame
	void Update () {
	    HealthBarColor();
	}

    void HealthBarColor()
    {
        background.color = new Color(0.5f+(0.5f-healthbar.value/200), healthbar.value/100, 0, 0.3f);
        Fill.color = new Color(0.5f + (0.5f - healthbar.value / 200), healthbar.value/100, 0, 1);
    }
}
