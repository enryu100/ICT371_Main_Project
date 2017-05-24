using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ICT371 {
    public class ConversationDisplay : MonoBehaviour {
        public Text m_textDisplay;

        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        }

        public void ChangeDisplayedText(string changeTo) {
            m_textDisplay.text = changeTo;
        }
    }
}