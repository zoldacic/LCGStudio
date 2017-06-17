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
        private string baseUrl = @"http://fiveringsdb.com/api/";

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

        public void LoadImage(string clan, string type, string cardName)
        {
            GameObject card = (GameObject)Instantiate(Resources.Load("Prefabs/Card")); ;
            Texture2D img = (Texture2D)Resources.Load("Images/" + clan + "/" + type + "/" + cardName);

            card.GetComponent<Image>().sprite = Sprite.Create(img, new Rect(0.0f, 0.0f, img.width, img.height), new Vector2(0.0f, 0.0f));
            //card.GetComponent<CardInfo>().sprite = 

            // Find Parent By Name

            card.transform.parent = parent.transform;
        }

        public void LoadCardInfo()
        {             
            var www = new WWW(baseUrl + "cards");
            //yield return www;           

            while (!www.isDone)
                System.Threading.Thread.Sleep(100);

            // Use Edit > Paste Special to create class

            var cards = JsonUtility.FromJson<>(www.text).Cards;

            foreach (card in cards)
            {
                LoadImage(card.clan_code, card.side, card.code);
            }
        }
    }
}
