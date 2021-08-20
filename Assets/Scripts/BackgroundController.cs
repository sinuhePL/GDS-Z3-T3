using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class BackgroundController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                ScrollingController[] scrollers = GetComponentsInChildren<ScrollingController>();
                foreach(ScrollingController sc in scrollers)
                {
                    sc._isScrolling = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameObject.SetActive(false);
            }
        }
    }
}