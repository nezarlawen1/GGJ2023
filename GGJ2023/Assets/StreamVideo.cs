using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage stage;
    public VideoPlayer background;
    public RawImage staticImage;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
    }
    IEnumerator PlayVideo()
    {
        background.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!background.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        if (background.isPrepared)
        {
            Destroy(staticImage);
            stage.gameObject.SetActive(true);
            stage.texture = background.texture;
            background.Play();

        }
    
    }

}
