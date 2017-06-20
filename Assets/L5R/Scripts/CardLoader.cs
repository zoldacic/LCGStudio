using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.L5R.Scripts
{
    class CardLoader : MonoBehaviour
    {
        private string baseUrl = @"http://fiveringsdb.com/api/v1/";

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

            // Find Parent By Name
            var parent = GameObject.Find("CardSelection");
            card.transform.parent = parent.transform;
        }

        public void LoadCardInfo()
        {             
            var www = new WWW(baseUrl + "cards");
            //yield return www;           

            while (!www.isDone)
                System.Threading.Thread.Sleep(100);

            var rootObject = JsonUtility.FromJson<Rootobject>(www.text);
            var cards = rootObject.records;

            foreach (var card in cards)
            {
                LoadImage(card.clan_code, card.side, card.code);
            }
        }
    }
}
