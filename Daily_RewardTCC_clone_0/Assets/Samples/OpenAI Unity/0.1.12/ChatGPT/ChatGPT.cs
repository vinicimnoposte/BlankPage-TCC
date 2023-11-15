using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi("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");

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Act like a capitalist goblin who only responds sarcastically.";
        private string prompt2 = "Never reveal that you are a model of artificial intelligence.";
        private string prompt3 = "Answer in a short way, always correctly following the rules of the English language.";
        private string prompt4 = "Know that Crinter is an RPG system whose concept is to bring freedom to the user, where it motivates him to write his own story the way he prefers.";
        private string prompt5 = "Crinter is about being accessible, inclusive and never-ending.";
        private string prompt6 = "Do not give information about Crinter and Blank Page to the player, unless the player asks about it, you can tell everything you know about Crinter.";
        private string prompt7 = "The RPG system and the application are developed by the studio called: Blank Page.";

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        private void AppendMessage(ChatMessage message)
        {
            
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + prompt2 + prompt3 + prompt4 + prompt5 + prompt6 + prompt7 + "\n" + inputField.text;

            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}