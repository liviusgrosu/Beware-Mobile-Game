using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FrameCounter : MonoBehaviour
{
    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;
 
    public Text counterText;
 
    void Start(){
        StartCoroutine(FPS());
    }
 
    private IEnumerator FPS() {
        for(;;){
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
           
            // Display it
 
            fps = string.Format(Mathf.RoundToInt(frameCount / timeSpan).ToString());
            counterText.text = fps;
        }
    }
}
