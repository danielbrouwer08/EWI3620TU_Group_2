using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Experimental.Networking;
using SimpleJSON;
using System.Collections.Generic;
using System;

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

    public void highscores()
    {
        StartCoroutine(gethighscores());
    }

    IEnumerator gethighscores()
    {
        //WWWForm form = new WWWForm ();
        //Hashtable headers = form.headers;
        //headers["Authorization"] = "Basic " + System.Convert.ToBase64String(
        //	System.Text.Encoding.ASCII.GetBytes(username + ":" + password));

        UnityWebRequest www = UnityWebRequest.Get("https://drproject.twi.tudelft.nl:8083/getHighscores");

        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            string receivedString = www.downloadHandler.text;

            string[] parts = receivedString.Split(new string[] { "},{" }, System.StringSplitOptions.None);
            parts[0] = parts[0].Replace("[{", "");
            parts[parts.Length - 1] = parts[parts.Length - 1].Replace("}]", "");

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = "{" + parts[i] + "}";
                var temp = JSON.Parse(parts[i]);
                transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetChild(i+1).GetComponent<Text>().text = temp["Name"];
                transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetChild(i+1).GetComponent<Text>().text = (temp["P1Score"].AsFloat + temp["P2Score"].AsFloat).ToString();
                transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(2).GetChild(i+1).GetComponent<Text>().text = temp["P1Score"];
                transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(3).GetChild(i+1).GetComponent<Text>().text = temp["P2Score"];
            }
            //serverTimeStamp = DateTime.Parse(receivedString.Replace("\"", ""));
            //Debug.Log (receivedString);
        }
    }
}
