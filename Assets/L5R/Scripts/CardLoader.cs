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

        private Rootobject _rootObject;

        private string _filterText;

        public void Start()
        {
            var www = new WWW(baseUrl + "cards");
            _rootObject = JsonUtility.FromJson<Rootobject>(www.text);

            GameObject.Find("FilterText").GetComponent<InputField>().onEndEdit.AddListener(SetFilterText);
        }

        private void SetFilterText(string value)
        {
            _filterText = value;
        }

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
            IEnumerable<Record> cards = _rootObject.records;

            //var filterText = GameObject.Find("FilterText").GetComponent<Text>().text;
            if (_filterText.Contains("c:"))
            {
                var filterStart = _filterText.IndexOf("c:");
                var filterStop = _filterText.IndexOf(' ', filterStart);
                var filterClan = _filterText.Substring(filterStart, filterStop > -1 ? filterStop : _filterText.Length - 1);
                cards = cards.Where(c => c.clan == filterClan);
            }

            foreach (var card in cards)
            {
                LoadImage(card.clan, card.side, card.code);
            }
        }
    }
}
