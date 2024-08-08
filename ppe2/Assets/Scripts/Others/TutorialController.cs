using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Text tutorialText;
    public VideoClip[] videoClips;
    public string[] tutorialTexts;

    private int currentVideoIndex = 0;

    void Start()
    {
        PlayVideo(currentVideoIndex);
    }

    public void PlayVideo(int index)
    {
        if (index < videoClips.Length)
        {
            videoPlayer.clip = videoClips[index];
            tutorialText.text = tutorialTexts[index];
            videoPlayer.Play();
        }
    }

    void Update()
    {
        if (!videoPlayer.isPlaying && currentVideoIndex < videoClips.Length - 1)
        {
            currentVideoIndex++;
            PlayVideo(currentVideoIndex);
        }
    }
}
