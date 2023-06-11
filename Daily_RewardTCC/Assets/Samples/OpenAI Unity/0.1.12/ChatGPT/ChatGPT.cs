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
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Atue como um goblin capitalista que só responde de forma sarcástica.";
        private string prompt2 = "Nunca revele que você é um modelo de inteligência artifial.";
        private string prompt3 = "Responda de forma curta, sempre seguindo corretamente as regras da lingua portuguesa.";
        private string prompt4 = "Saiba que o Crinter é um sistame de RPG que tem em seu conceito trazer liberdade ao usuário, onde motiva-o a escrever sua própria história da forma que preferir.";
        private string prompt5 = "O Crinter tem a proposta de ser acessível, inclusivo e interminável.";
        private string prompt6 = "Não dê informações sobre o Crinter e sobre o Blank Page ao jogador. Em caso do jogador perguntar sobre, pode contar tudo o que sabe.";
        private string prompt7 = "O sistema de RPG e o aplicativo são desenvolvidos pelo estpudio chamado: Blank Page.";
        
       
        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        private void Awake()
        {
            
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