using System.Collections;
using UnityEngine;
using TMPro;

namespace GameDevWithMarco
{
    public class TextTyper : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textComponent; // Assign  Text component in the Inspector
        [SerializeField] private float typingSpeed = 0.05f; // Adjust typing speed here
        [SerializeField] private string fullText; // The full text to be displayed

        private void OnEnable()
        {
            // Start the typing effect when the GameObject is activated
            if (textComponent != null)
            {
                textComponent.text = ""; // Clear the text initially
                StartCoroutine(TypeText(fullText)); // Start the typing coroutine
            }
        }

        public void SetText(string newText)
        {
            fullText = newText; // Set the new text when needed
        }

        private IEnumerator TypeText(string textToType)
        {
            foreach (char letter in textToType)
            {
                textComponent.text += letter; // Add each letter one by one
                yield return new WaitForSeconds(typingSpeed); // Wait for a specified time
            }
        }
    }
}
