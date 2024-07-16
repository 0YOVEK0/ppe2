using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI;
namespace GoSystem
{
    [GBehaviourAttributeAttribute("Message", false)]
    public class GoMessge : GoSystemsBehaviour
    {
        [TextArea(3, 10)]
        [SerializeField] string Message;
        [SerializeField] GameObject MessageUi;
        private Text TextMessageUi;
        private void OnTriggerStay(Collider other)
        {
            if (other.tag != "Player") return;
            TextMessageUi = MessageUi.GetComponentInChildren<Text>();
            TextMessageUi.text = Message;
            MessageUi.SetActive(true);
        }
        private void OnTriggerExit(Collider other)
        {
            MessageUi.SetActive(false);
        }

        private void Update()
        {

            if (MessageUi == null)
            {
                MessageUi = FindObjectOfType<GoCharacterController>().transform.Find("GUI").transform.Find("message").gameObject;
            }

        }
    }
}