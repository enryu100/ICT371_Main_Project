using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICT371
{
    public class VRFacade : MonoBehaviour
    {

        public GameObject m_mainCam;
        public GameObject m_VRCam;
        public GameObject m_VRManager;
        public bool m_VRMode;
        
        // Checks connection of the camera after each frame.
        private void SetCameras()
        {
            if(!UnityEngine.VR.VRDevice.isPresent) {
                m_VRMode = false;
            }

            if (m_VRMode)
            {
                m_mainCam.SetActive(false);
                m_VRCam.SetActive(true);
                m_VRManager.SetActive(true);
            }
            else
            {
                m_mainCam.SetActive(true);                
                m_VRCam.SetActive(false);
                m_VRManager.SetActive(false);
            }
        }
        
        // Use this for initialization
        void Start()
        {
            SetCameras();
        }

        //Fixed update to lower performance drain as opposed to Update()
        private void FixedUpdate()
        {
            SetCameras();
        }
    }
}