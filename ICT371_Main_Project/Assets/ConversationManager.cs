using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ICT371 {
    public class ConversationManager : MonoBehaviour {

        private bool m_textChanged = true;
        private ConversationDisplay m_textConvoDisplay;
        private string m_changedText;
        public Text m_textDisplay;

        // Use this for initialization
        void Start() {
            m_textConvoDisplay = m_textDisplay.GetComponent<ConversationDisplay>();
            //TODO Register any other components
        }

        // Update is called once per frame
        void Update() {
            if(m_textChanged) {
                m_textConvoDisplay.ChangeDisplayedText(m_changedText);
            }
        }
    }
}