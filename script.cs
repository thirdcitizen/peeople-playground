using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using UnityEngine.UI;
//i dont know how to code 

namespace Mod
{
    public class Mod
    {
        public static void Main()
        {
			ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"), 
                    NameOverride = " name ",  //name
                    DescriptionOverride = "description", //description
                    CategoryOverride = ModAPI.FindCategory("Entities"), 
                    ThumbnailOverride = ModAPI.LoadSprite(".png"), //thumbnail (find file location)
                    AfterSpawn = (Instance) => 
                    {
                        var person = Instance.GetComponent<PersonBehaviour>();

                        var Head = Instance.transform.Find("Head").gameObject;

                        Head.GetOrAddComponent<MultipleSpriteHumanBehaviour>().person = person;
                        Head.GetComponent<MultipleSpriteHumanBehaviour>().Scale = 3;  //3 is cool
                        Head.GetComponent<MultipleSpriteHumanBehaviour>().Textures = new Texture2D[]
                        {
                            ModAPI.LoadTexture(".png"), //get rid of this if u only want 1
							ModAPI.LoadTexture(".png"), //npc texture u can copy paste more of this if u want more
                        };
                    }
                }
            );
			
	    } //dont get rid of the 2 bracket
	}
	
	//milmod texture thing (DONT TOUCH THIS !!!)
public class MultipleSpriteHumanBehaviour : MonoBehaviour
    {
        public Texture2D[] Textures = new Texture2D[0];
        public PersonBehaviour person;
        public int CurrentTexture = -2;
        public int Scale = 3;

        void Start()
        {
            SetTexture();
        }

        public void SetTexture()
        {
            if (Textures.Length == 0)
                return;

            if(CurrentTexture == -2)
            {
                CurrentTexture = UnityEngine.Random.Range(0, Textures.Length);
            }
            person.SetBodyTextures(Textures[CurrentTexture], null, null, Scale);
            for (int i = 0; i < person.Limbs.Length; i++)
            {
                person.Limbs[i].gameObject.GetComponent<PhysicalBehaviour>().RefreshOutline();
            }
        }
}   }  
