using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace OpenAI
{
    public class MyGPTController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_InputText;
        [SerializeField] private TextMeshProUGUI m_OutputText;
        [SerializeField] private TextMeshProUGUI m_QuestionText;
        [SerializeField] private TMP_InputField m_InputField;
        
        //[SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        //[SerializeField] private ScrollRect scroll;
        
        //[SerializeField] private RectTransform sent;
        //[SerializeField] private RectTransform received;

        //private float height;
        private OpenAIApi openai = new OpenAIApi("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");

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = 
            "You are a K-12 teacher. You will answer student questions on class contents on plant cells. " +
            "You should keep your answers shorter than 2 sentences. Don't reply to messages before this position. " +
            "You will respond to messages below: ";

        private string m_StartMessage = 
            "You can ask me anything about plant cells!";
        private void Start()
        {
            button.onClick.AddListener(SendReply);
            m_OutputText.text = m_StartMessage;
            m_QuestionText.text = "";
        }

        private void AppendMessage(ChatMessage message)
        {
            //scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            //var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            //item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            //item.anchoredPosition = new Vector2(0, -height);
            //LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            //height += item.sizeDelta.y;
            //scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            //scroll.verticalNormalizedPosition = 0;
            m_OutputText.text = message.Content;
            Debug.Log("Append Message --- " + message.Content);
        }

        private async void SendReply()
        {
            button.enabled = false;
            if (m_InputText.text.Length < 1)
            {
                return;
            }
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = m_InputText.text
            };
            
            // update question text
            m_QuestionText.text = newMessage.Content;
            m_OutputText.text = "Typing ... ";

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + m_InputText.text; 
            
            messages.Add(newMessage);
            
            button.enabled = false;
            m_InputText.text = "";
            m_InputText.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages,
                Temperature = 0.1f,
                MaxTokens = 60,
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                //message.Content = message.Content.Trim();
                
                messages.Add(message);
                
                //AppendMessage(message);
                m_OutputText.text = message.Content;
            }
            else
            {
                m_OutputText.text = "Sorry I did not generate any answer. Please retry!";
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            m_InputText.enabled = true;
            m_InputField.text = "";
            button.enabled = true;
        }
    }
}

