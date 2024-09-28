using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurManager : MonoBehaviour
{
    private static BlurManager instance;
    private static Camera blurCamera;
    public static RenderTexture renderTexture;
    private static Material blurMaterialStatic;
    private static GameObject blurPanelStatic;
    [SerializeField] private Material blurMaterial;
    [SerializeField] private GameObject blurPanel;
    [SerializeField] private float blurValue = 0.01f;
    private static float blurValueStatic;
    private static DepthOfField depthOfFieldStatic;
    
    void Awake()
    {
        instance = this;
        if (FindAnyObjectByType<Volume>().profile.TryGet(out DepthOfField tmp))
        {
            depthOfFieldStatic = tmp;
        }
        
        blurCamera = Camera.main.transform.GetChild(0).GetComponent<Camera>();

        blurPanelStatic = blurPanel;
        blurValueStatic = blurValue;
        
        if (blurCamera.targetTexture != null)
        {
            blurCamera.targetTexture.Release();
        }

        //renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        //blurCamera.targetTexture = renderTexture;
        //blurMaterialStatic = blurMaterial;
        //blurMaterialStatic.SetTexture("_RenderTexture", renderTexture);
        //blurMaterialStatic.SetFloat("_blur", 0);

        blurCamera.gameObject.SetActive(false);
    }

    public static void SetBlur(bool value)
    {
        instance.StartCoroutine(LerpFocusDistance(value));
    }

    private static IEnumerator LerpFocusDistance(bool value)
    {
        float targetValue = value ? 0 : 20;
        float currentValue = depthOfFieldStatic.focusDistance.value;
        float speed = 10f;

        if (value)
            currentValue = 5f;

        while (true)
        {
            float add = 0;

            add = speed * Time.deltaTime;

            if (value)
                add = -add;

            currentValue += add;

            depthOfFieldStatic.focusDistance.value = currentValue;

            if (value && currentValue < 0)
                yield break;
            
            if(!value && currentValue > 20)
                yield break;

            yield return null;
        }
    }
    
}
