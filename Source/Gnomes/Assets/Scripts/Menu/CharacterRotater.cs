using UnityEngine;
using System.Collections;

public class CharacterRotater : MonoBehaviour {

    public MenuManager menu;
    public MenuController charsel2;
    public GameObject kab1;
    public GameObject kab2;

    void Awake()
    {
        kab1.gameObject.SetActive(false);
        kab2.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        kab1.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        kab2.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        if (menu.currMenu == charsel2)
        {
            StartCoroutine(CharacterDisplay());
        }
        else
        {
            kab1.gameObject.SetActive(false);
            kab2.gameObject.SetActive(false);
        }
	}

    IEnumerator CharacterDisplay()
    {
        yield return new WaitForSeconds(1);
        kab1.gameObject.SetActive(true);
        kab2.gameObject.SetActive(true);
    }
}
