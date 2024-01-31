using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class LoadNextLevel : MonoBehaviour
    {
        [SerializeField] private Collider2D trigger;
        [SerializeField] private GameObject notification;

        private ContactPoint2D[] contacts;
        private Collider2D target;

        private void Awake()
        {
            notification.SetActive(false);
            enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                notification.SetActive(true);
                enabled = true;
                target = collision;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == target)
            {
                notification.SetActive(false);
                enabled = false;
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.F))
            {
                GlobalEvents.SendLoadNextLevel();
            }
        }
    }
}