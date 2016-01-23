using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class FeedbackSubmit : MonoBehaviour {

    private Text feedbacktext;

    public void Sendfeedback()
    {
        feedbacktext = GetComponent<Text>();
        string fbtxt = feedbacktext.text;
        Analytics.CustomEvent("feedback", new Dictionary<string, object>
        {
            { "feedback", fbtxt },

        });
    }
    
}
