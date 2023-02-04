using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoPlayerScript : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public string sceneToLoad;
    public bool switchSceneAfterVideoEnds;

    void Start()
    {
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += VideoPrepared;
    }

    void VideoPrepared(VideoPlayer vp)
    {
        rawImage.texture = vp.texture;
        videoPlayer.Play();
        videoPlayer.loopPointReached += VideoEnded;
    }

    void VideoEnded(VideoPlayer vp)
    {
        if (switchSceneAfterVideoEnds)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}