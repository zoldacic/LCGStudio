using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.L5R.Scripts
{
    public class MarkCardOnClick : MonoBehaviour
    {  
        private void Start()
        {
            Physics.queriesHitTriggers = true;

            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().color = Color.blue;
            }
        }

        void OnMouseDown()
        {
            GetComponent<Image>().color = Color.red;
            Destroy(this.gameObject);
            Debug.Log("Clicked");
        }

        void OnMouseUpAsButton()
        {
            GetComponent<Image>().color = Color.red;
            Destroy(this.gameObject);
            Debug.Log("Clicked");
        }

        //void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        //Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        RaycastHit2D hit = Physics2D.Raycast(
        //            new Vector2(
        //                Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
        //                Camera.main.ScreenToWorldPoint(Input.mousePosition).y), 
        //                Vector2.zero, 
        //                0);

        //        if (hit)
        //        {
        //            Debug.Log("Name = " + hit.collider.gameObject.name);

        //            //Debug.Log("Name = " + hit.collider.name);
        //            //Debug.Log("Tag = " + hit.collider.tag);
        //            //Debug.Log("Hit Point = " + hit.point);
        //            //Debug.Log("Object position = " + hit.collider.gameObject.transform.position);
        //            //Debug.Log("--------------");
        //        }
        //    }
        //}

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    hit.collider.attachedRigidbody.AddForce(Vector2.up);
                }
            }
        } 
    }
}
