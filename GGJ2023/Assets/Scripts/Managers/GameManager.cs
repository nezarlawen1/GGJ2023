using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Core CurrentCore;
    public MazeGenerator CurrentMaze;
    public RawImage HitFalshImage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public IEnumerator disableFlash()
    {
        yield return new WaitForSeconds(3);
        HitFalshImage.gameObject.SetActive(false);
    }
}
