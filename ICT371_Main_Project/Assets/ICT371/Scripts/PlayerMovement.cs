using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICT371 {
    public class PlayerMovement : MonoBehaviour {
        private bool m_positionChanged = false;
        private bool m_VRMode;

        public Transform m_fpsCameraTransform;
        public Transform m_VRCameraTransform;
        public Transform m_playerTransform;

        // Use this for initialization
        void Start() {
            m_VRMode = GameObject.Find("VRFacade").GetComponent<VRFacade>().m_VRMode;
            m_VRCameraTransform.position = m_playerTransform.position;
            m_fpsCameraTransform.position = m_playerTransform.position;
        }

        // Update is called every frame
        void Update() {
            CheckForChange();

            if(m_positionChanged) {
                if(m_VRMode) {
                    UpdateVRCamera();
                } else {
                    UpdateFpsCamera();
                }

                m_positionChanged = false;
            }
        }

        private void CheckForChange() {
            if(m_VRMode){
                if(m_playerTransform.position != m_VRCameraTransform.position){
                    m_positionChanged = true;
                }
            }else{
                if(m_playerTransform.position != m_fpsCameraTransform.position){
                    m_positionChanged = true;
                }
            }
        }

        private void UpdateFpsCamera() {
            m_playerTransform.position += m_fpsCameraTransform.localPosition;
            m_fpsCameraTransform.localPosition = Vector3.zero;
        }

        private void UpdateVRCamera() {
            m_playerTransform.position += m_VRCameraTransform.localPosition;
            m_playerTransform.localPosition = Vector3.zero;
        }
    }
}