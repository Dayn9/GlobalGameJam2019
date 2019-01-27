using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    private AudioSource music;
    private static Music instance = null;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(this.gameObject);
            return;
        }
        music = GetComponent<AudioSource>();
        music.Play();
    }
}
