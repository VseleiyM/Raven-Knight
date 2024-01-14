using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class LoadNextLevel : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                GlobalEvents.SendLoadNextLevel();
        }
    }
}