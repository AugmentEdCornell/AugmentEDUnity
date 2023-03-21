using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeUIText : MonoBehaviour
{

    public string react_txt;
    private TextMeshProUGUI textMeshPro;
    // TextMeshProUGUI react_text_text_component;

    // public void CallableFunctionReact(string react_txt)
    // {
    //     //TextMeshProUGUI react_text_text_component;
    //     react_text_text_component = react_text.GetComponent<TextMeshProUGUI>();
    //     react_text_text_component.text = react_txt;

    // }
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = "Modi Ji";
        Debug.Log(textMeshPro.text);
    }
    
    public void CallableFunctionReact(string react_txt){
        textMeshPro.text = react_txt;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     CallableFunctionReact(react_txt);
    //     Debug.Log(textMeshPro.text);
    // }
}
