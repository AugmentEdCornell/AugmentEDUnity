// using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;
// using Newtonsoft.Json;
// using UnityEngine.UI;
// using TMPro;

// public class DataObject
// {
//     public string username;
//     public string key2;
// }

// public class AWS_Connect : MonoBehaviour
// {
//     public TextMeshProUGUI textMeshPro;

//     private void Awake() 
//     {
//         GameObject reactTextObject = GameObject.Find("React-Text");
        
//         // Get the TextMeshProUGUI component from the GameObject
//         textMeshPro = reactTextObject.GetComponent<TextMeshProUGUI>();
//     }

//     public void OnButtonClicked()
//     {
//         StartCoroutine(PostRequest("https://oxqbb3foya.execute-api.us-east-1.amazonaws.com/default/dynamodbTesting"));
//     }

//     IEnumerator PostRequest(string uri)
//     {
//         var data = new DataObject
//         {
//             username = textMeshPro.text,
//             key2 = "Augment ED testing"
//         };

//         string jsonData = JsonConvert.SerializeObject(data);

//         var request = new UnityWebRequest(uri, "POST");
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
//         request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
//         request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
//         request.SetRequestHeader("Content-Type", "application/json");

//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.ConnectionError)
//         {
//             Debug.LogError(request.error);
//         }
//         else
//         {
//             Debug.Log(request.downloadHandler.text);
//         }
//     }
// }



using System.Collections;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class DataObject
{
    public string username;
    public string key2;
}

public class AWS_Connect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    private void Awake() 
    {
        GameObject reactTextObject = GameObject.Find("React-Text");
        
        // Get the TextMeshProUGUI component from the GameObject
        textMeshPro = reactTextObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnButtonClicked()
    {
        StartCoroutine(PostRequest("https://oxqbb3foya.execute-api.us-east-1.amazonaws.com/default/dynamodbTesting"));
    }

    IEnumerator PostRequest(string uri)
    {
        var data = new DataObject
        {
            username = textMeshPro.text,
            key2 = "Augment ED testing"
        };

        var client = new RestClient(uri);
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(data);
        yield return client.ExecuteAsync(request, response =>
        {
            Debug.Log("client request");
            Debug.Log(client);
            Debug.Log(request);
            Debug.Log(response.StatusCode);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Debug.Log(007);
                Debug.LogError(response.ErrorMessage);
            }
            else
            {
                Debug.Log(response.Content);
            }
        });
    }
}





// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
// using Newtonsoft.Json;
// using UnityEngine.UI;
// using TMPro;

// public class DataObject
// {
//     public string username;
//     public string key2;
// }

// public class AWS_Connect : MonoBehaviour
// {

//     public TextMeshProUGUI textMeshPro;
//     // public string PlayerUsername = "Parthit CPO";

//     IEnumerator PostRequest(string uri)
//     {


//         var data = new DataObject{
//             username = textMeshPro.text,
//             key2 = "Nidhi"
//         };

//         string jsonData = JsonConvert.SerializeObject(data);

//         UnityWebRequest request = UnityWebRequest.Post(uri, jsonData);
//         request.SetRequestHeader("Content-Type", "application/json");

//         yield return request.SendWebRequest();
//         if (request.result == UnityWebRequest.Result.ConnectionError)
//         {
//             Debug.Log("Something went wrong");
//             Debug.LogError(request.error);
//         }
//         else
//         {
//             Debug.Log("Success! Response: " + request.downloadHandler.text);
//         }
//     }

//     public void OnButtonClicked()
//     {
//         Debug.Log("Button was clicked");
//         StartCoroutine(PostRequest("https://oxqbb3foya.execute-api.us-east-1.amazonaws.com/default/dynamodbTesting"));
//     }

//     private void Awake() 
//     {
//         GameObject reactTextObject = GameObject.Find("React-Text");
        
//         // Get the TextMeshProUGUI component from the GameObject
//         textMeshPro = reactTextObject.GetComponent<TextMeshProUGUI>();
//     }

// }
