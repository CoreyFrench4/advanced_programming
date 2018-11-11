using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CardGame
{
    public class CardDisplay : MonoBehaviour
    {
        public CardGame.Card card;
        public Text nameText, descriptionText, flavourText, costText, attackText, hitpointText;
        public Image art;
        void Start()
        {
            nameText.text = card.name;
           
            flavourText.text = card.flavour;
            costText.text = card.cost.ToString();
            attackText.text = card.attack.ToString();
            hitpointText.text = card.hitpoints.ToString();
            art.sprite = card.sprite;

        }
    }
}
