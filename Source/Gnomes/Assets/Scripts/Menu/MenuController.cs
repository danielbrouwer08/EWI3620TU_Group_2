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
        // Load Global highscores
        StartCoroutine(gethighscores());
        Transform viewport = transform.FindChild("MaskPanel").FindChild("Panel").FindChild("Scores").FindChild("Local").FindChild("Viewport");
        // Load Local highscores
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.GetString("Team" + i) != null)
            {
                viewport.FindChild("Team").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = PlayerPrefs.GetString("Team" + i);
                viewport.FindChild("Total").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = PlayerPrefs.GetInt("Total" + i).ToString();
                viewport.FindChild("P1").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = PlayerPrefs.GetInt("P1" + i).ToString();
                viewport.FindChild("P2").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = PlayerPrefs.GetInt("P2" + i).ToString();
            }
        }
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
            string receivedString = www.downloadHandler.text;

            string[] parts = receivedString.Split(new string[] { "},{" }, System.StringSplitOptions.None);
            parts[0] = parts[0].Replace("[{", "");
            parts[parts.Length - 1] = parts[parts.Length - 1].Replace("}]", "");

            Transform viewport = transform.FindChild("MaskPanel").FindChild("Panel").FindChild("Scores").FindChild("Global").FindChild("Viewport");

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = "{" + parts[i] + "}";
                var temp = JSON.Parse(parts[i]);
                viewport.FindChild("Team").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = temp["Name"];
                viewport.FindChild("Total").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = (temp["P1Score"].AsFloat + temp["P2Score"].AsFloat).ToString();
                viewport.FindChild("P1").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = temp["P1Score"];
                viewport.FindChild("P2").FindChild("Text (" + (i + 1) + ")").GetComponent<Text>().text = temp["P2Score"];
            }
            //serverTimeStamp = DateTime.Parse(receivedString.Replace("\"", ""));
            //Debug.Log (receivedString);
        }
    }
}
