using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "Card", fileName = "New Card")]
    public class Card : ScriptableObject
    {
        public new string name;
        
        public string flavour;
        public Sprite sprite;
        public int cost;
        public int attack;
        public int hitpoints;


    }
}
