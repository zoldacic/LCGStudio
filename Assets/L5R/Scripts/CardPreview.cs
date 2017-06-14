using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.L5R.Scripts
{
    class CardPreview : MonoBehaviour
    {
        public void ShowCardPreview(GameObject card)
        {
            var preview = new GameObject("Icon");                                              // Create object for item's icon
            Image image = preview.AddComponent<Image>();
            image.sprite = card.GetComponent<Image>().sprite;
            image.raycastTarget = false;

            Canvas canvas = card.GetComponentInParent<Canvas>();                             // Get parent canvas
            if (canvas != null)
            {
                // Display on top of all GUI (in parent canvas)
                preview.transform.SetParent(canvas.transform, true);                       // Set canvas as parent
                preview.transform.SetAsLastSibling();                                      // Set as last child in canvas transform
            }

            //GameObject previewCard = (GameObject)Instantiate(Resources.Load("Prefabs/Card")); ;
            //Texture2D img = (Texture2D)Resources.Load("Images/Crab/Dynasty/" + cardName);
            //card.GetComponent<Image>().sprite.
            //var sprite = Sprite.Create(img, new Rect(0.0f, 0.0f, img.width, img.height), new Vector2(0.0f, 0.0f));
            //previewCard.GetComponent<Image>().sprite = Sprite.Create(img, new Rect(0.0f, 0.0f, img.width, img.height), new Vector2(0.0f, 0.0f));
            //card.transform.parent = parent.transform;
        }
    }
}
