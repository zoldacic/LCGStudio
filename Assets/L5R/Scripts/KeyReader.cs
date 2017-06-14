using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.L5R.Scripts
{
    class KeyReader : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown("p"))
            {
                print("p key was pressed");

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    print(hit.collider.name);
                }

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    print("IsPointerOverGameObject");
                }
            }

        }

        void OnMouseOver()
        {
            print(gameObject.name);
        }
    }
}
