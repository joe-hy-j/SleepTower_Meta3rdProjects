using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChatItem : MonoBehaviour
{
    TMP_Text chatText;
    RectTransform rt;

    private void Awake()
    {
        chatText = GetComponent<TMP_Text>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        //chatText = GetComponent<TMP_Text>();
        //rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string s)
    {
        chatText.text = s;

        StartCoroutine(UpdateSize());
    }

    IEnumerator UpdateSize()
    {
        yield return null;

        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText. preferredHeight); 
    }
}
