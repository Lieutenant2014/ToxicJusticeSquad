using UnityEngine;

public class PlaySoundOnClientOnly : MonoBehaviour
{
    public AudioSource Audio;

	// Use this for initialization
	void Start () {
        Audio.Play();
    }
}
