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

            if (img == null)
            {
                img = (Texture2D)Resources.Load("Images/Crab/Dynasty/borderlands-defender");
            }

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

            //var text = "{    \"records\": [ { \"clan\": \"crane\" }, ],    \"size\": 65,    \"success\": true, \"last_updated\": \"2017-06-24T08:21:51+00:00\"}";
            //var text = "{    \"records\": [ { \"clan\": \"crane\" }, ],     \"success\": true }";
            //var text = www.text.Replace("\n", ""); //.Replace("\"", "Ö").Replace('Ö','"');
            //var text = @""{    "records": [ {            "clan": "crane",            "code": "above-question",            "cost": 1,            "cycles":   {                "core": 1            },            "illustrator": "Stu Barnes",            "influence_cost": 	2,            "is_unique": false,            "keywords": "Condition.",            "military_strength_mod": "+0",            "name": "Above Question",            "packs": {                "core": 1            },            "political_strength_mod": "+0",            "side": "conflict",            "text": "Attached character cannot be chosen as a target of an opponent's event.",            "type": "attachment"           },       ],    "size": 65,    "success": true,    "last_updated": "2017-06-24T08:21:51+00:00"}                "";

            //var text = "{ \"records\": [ { \"clan\": \"crane\", }, ], \"size\": 65, \"success\": true, \"last_updated\": \"2017-06-24T08:21:51+00:00\" }";
            //var text = "{ \"records\": [ { \"clan\": \"crane\" } ], \"size\": 65, \"success\": true }";
            //var text = "{ \"records\": [ { \"clan\": \"crane\" } ] }";
            //var text = "{ \"size\": 65, \"success\": true }";

            var rootObject = JsonUtility.FromJson<Rootobject>(www.text);
            //var a = rootObject.size;
            var cards = rootObject.records;

            foreach (var card in cards)
            {
                LoadImage(card.clan, card.side, card.code);
            }
        }
    }
}
