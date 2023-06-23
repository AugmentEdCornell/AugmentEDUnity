using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace OpenAI
{
    public class GPTPlant : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_OutputText;
        [SerializeField] private TextMeshProUGUI m_QuestionText;
        [SerializeField] private TMP_InputField m_InputField;
        [SerializeField] private Button button;

        private OpenAIApi openai = new OpenAIApi("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");

        private List<ChatMessage> messages = new List<ChatMessage>();
        
        private string prompt = 
            "You are a K-12 teacher. You will answer student questions on class contents on plant cells. " +
            "You should keep your answers shorter than 2 sentences. Don't reply to messages before this position. " +
            "You will respond to messages below: ";

        private string m_StartMessage = 
            "You can ask me anything about plant cells!";

        private string m_ChooseElementPrompt = 
            "Here is a list of elements with integer labels: \n" +
            "0 Plasmodesmata\n1 Plasma Membrane\n2 Cytoplasm\n3 Cytoskeleton\n4 Reticulum\n 5 Nucleus\n" +
            "6 Ribosome\n7 Golgi Apparatus\n8 Bubble Golgi\n9 Vacuole\n10 Mitochondrion\n11 Chloroplast\n" +
            "12 Peroxisomes\n13 Leukoplast\n14 None of above" +
            "In the conversations below, tell me the integer label the element you are talking about. Only tell me the integer number:\n" +
            "\n";

        [SerializeField] private Button2Model[] m_ElementButtons;
        
        private void Start()
        {
            button.onClick.AddListener(SendReply);
            m_OutputText.text = m_StartMessage;
            m_QuestionText.text = "";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Call your function here
                if (button.enabled) SendReply();
            }
        }

        private async void SendReply()
        {
            button.enabled = false;
            if (m_InputField.text.Length < 1)
            {
                return;
            }
            
            var userInputMessage = new ChatMessage()
            {
                Role = "user",
                Content = m_InputField.text
            };
            
            // update question text
            m_QuestionText.text = userInputMessage.Content;
            m_OutputText.text = "Typing ... ";

            if (messages.Count == 0) userInputMessage.Content = prompt + "\n" + m_InputField.text; 
            
            messages.Add(userInputMessage);
            
            button.enabled = false;
            string tempInput = m_InputField.text;
            m_InputField.text = "";
            m_InputField.enabled = false;
            
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
                var replyMessageGPT = completionResponse.Choices[0].Message;
                //message.Content = message.Content.Trim();
                
                messages.Add(replyMessageGPT);
                
                //AppendMessage(message);
                m_OutputText.text = replyMessageGPT.Content;
                
                // use the most recent pair of conversation to zoom into object
                List<ChatMessage> mostRecentMessages = new List<ChatMessage>();
                //userInputMessage.Content = m_ChooseElementPrompt + "\n" + userInputMessage.Content; 
                userInputMessage.Content = m_ChooseElementPrompt; 
                mostRecentMessages.Add(userInputMessage);
                mostRecentMessages.Add(replyMessageGPT);
                StartCoroutine(WaitBeforeFocus(mostRecentMessages));
                m_InputField.text = "";
            }
            else
            {
                m_OutputText.text = "Sorry I did not generate any answer. Please retry!";
                Debug.LogWarning("No text was generated from this prompt.");
                m_InputField.text = tempInput;
            }

            button.enabled = true;
            m_InputField.enabled = true;
            
            //button.enabled = true;
        }

        private IEnumerator WaitBeforeFocus(List<ChatMessage> mostRecentMessages)
        {
            Debug.Log("Before waiting");

            yield return new WaitForSeconds(1f);
            FocusOnObject(mostRecentMessages);

            Debug.Log("After waiting");
        }
        
        private async void FocusOnObject(List<ChatMessage> mostRecentMessages)
        {
            Debug.Log("Focus on object called");
            // let gpt find the object to focus on
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = mostRecentMessages,
                Temperature = 0.1f,
                MaxTokens = 1,
            });
            
            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var replyMessageGPT = completionResponse.Choices[0].Message;
                Debug.Log(replyMessageGPT.Content);
                int index = -1;
                if (int.TryParse(replyMessageGPT.Content, out index))
                {
                    if (index >= 0 && index < m_ElementButtons.Length)
                    {
                        ZoomIn(index);
                    }
                }
            }
            else
            {
                Debug.LogWarning("No response is received from gpt when focusing on object.");
            }
        }

        private void ZoomIn(int index)
        {
            Debug.Log("Zoom in object " + index);
            if (m_ElementButtons[index]) m_ElementButtons[index].ButtonOnClick();
        }
    }
}

