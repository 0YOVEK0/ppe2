using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoSystem
{
    [GBehaviourAttributeAttribute("Audio Speaker", false)]
    public class AudioSpeaker : GoSystemsBehaviour
    {
       public AudioClip[] Clip;
   
       
         void Play(AudioClip audioSource)
        {
            gameObject.GetComponent<AudioSource>().clip = audioSource;

            gameObject.GetComponent<AudioSource>().Play();
        }
        public void IndexClip(int Index)
        {
          
          
            Play(Clip[Index]);
        }
    }
  
}