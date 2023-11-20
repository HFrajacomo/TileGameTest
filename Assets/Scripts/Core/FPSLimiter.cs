using UnityEngine;
 
public class FPSLimiter : MonoBehaviour 
{
    public int targetFrameRate = 120;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = targetFrameRate;
    }
}