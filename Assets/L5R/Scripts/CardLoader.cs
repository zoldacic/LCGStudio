using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleDragAndDrop.Scripts
{
    class CardLoader : MonoBehaviour
    {
        private List<string> CrabDynasty = new List<string> {
            "Borderlands Defender",
            "Borderlands Fortifications",
            "Eager Scout",
            "Shrewd Yasaki"
        };

        public void LoadImages(GameObject parent)
        {
            CrabDynasty.ForEach(cardName =>
            {
                GameObject card = (GameObject)Instantiate(Resources.Load("Prefabs/Card")); ;
                Texture2D img = (Texture2D)Resources.Load("Images/Crab/Dynasty/" + cardName);

                card.GetComponent<Image>().sprite = Sprite.Create(img, new Rect(0.0f, 0.0f, img.width, img.height), new Vector2(0.0f, 0.0f));
                card.transform.parent = parent.transform;
            });


        }
    }
}
