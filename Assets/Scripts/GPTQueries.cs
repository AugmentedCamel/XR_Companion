using BitSplash.AI.GPT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTQueries : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //var convo = ChatGPTConversation.Start(this);
        //convo.Say("Hello chat GPT");
    }

    void OnConversationResponse(string text)
    {
        Debug.Log("GPT Response: " + text);
    }

    void OnConversationError(string text)
    {
        Debug.Log("error gpt : " + text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
