using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICT371 {
    public class Interacable : MonoBehaviour {

        private Transform m_objectOriginalTransform;

        private float m_speed = 1.0F;
        private float m_startTime;
        private float m_journeyLength;
        private bool m_doLerp = false;

        private Vector3 m_startLerp;
        private Vector3 m_endLerp;

        public Transform m_playerTransform;
        public bool m_pickingUpObject = false;
        public bool m_puttingDownObject = false;

        // Use this for initialization
        void Start() {
            m_objectOriginalTransform = this.GetComponent<Transform>();
        }

        void LERP() {
            Debug.Log("Entered LERP");
            float distCovered = (Time.time - m_startTime) * m_speed;
            float fracJourney = distCovered / m_journeyLength;
            Debug.Log("just before Lerp call");
            this.GetComponent<Transform>().position = Vector3.Lerp(m_startLerp, m_endLerp, fracJourney);
            Debug.Log("Passed Lerp call");
        }

        // Update is called once per frame
        void Update() {
            Debug.Log("Entered update: ");
            if(m_pickingUpObject && !m_doLerp) {
                Debug.Log("Picking up object entered");
                //prepare to bring object up to inspect
                if(this.GetComponent<Transform>().position != m_playerTransform.position) {
                    Debug.Log("Passed position comparison");
                    Vector3 compensate = new Vector3(m_objectOriginalTransform.position.x + 0.2f, 0 - 0.3f, m_objectOriginalTransform.position.z + 0.2f);
                    m_journeyLength = Vector3.Distance(m_objectOriginalTransform.position, m_playerTransform.position);
                    Debug.Log("Passed dist");
                    m_startTime = Time.time;
                    Debug.Log("Passed Time");
                    m_startLerp = m_objectOriginalTransform.position;
                    Debug.Log("Passed start Lerp");
                    m_endLerp = m_playerTransform.position;
                    Debug.Log("Passed end Lerp");
                    m_doLerp = true;
                    Debug.Log("Passed do Lerp");
                }
            } else if(m_puttingDownObject && ! m_doLerp) {
                Debug.Log("Putting down object entered");
                //prepare to put object back
                if(this.GetComponent<Transform>().position != m_playerTransform.position) {
                    Debug.Log("Passed position comparison");
                    m_journeyLength = Vector3.Distance(m_objectOriginalTransform.position, m_playerTransform.position);
                    Debug.Log("Passed dist");
                    m_startTime = Time.time;
                    Debug.Log("Passed Time");
                    m_startLerp = m_playerTransform.position;
                    Debug.Log("Passed start Lerp");
                    m_endLerp = m_objectOriginalTransform.position;
                    Debug.Log("Passed end Lerp");
                    m_doLerp = true;
                    Debug.Log("Passed do Lerp");
                }
            }

            Debug.Log("Prior to m_doLerp");
            if(m_doLerp) {
                Debug.Log("m_doLerp is true");
                if(m_puttingDownObject) {
                    LERP();
                    if(this.GetComponent<Transform>().position != m_playerTransform.position) {
                        m_puttingDownObject = false;
                    } 
                }else {//m_pickingUpObject
                    LERP();
                    if(this.GetComponent<Transform>().position != m_playerTransform.position) {
                        m_pickingUpObject = false;
                    }
                }
            }
        }
    }
}