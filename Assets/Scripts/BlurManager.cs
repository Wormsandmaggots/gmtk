using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class BlurManager : MonoBehaviour
{
    private static Camera blurCamera;
    public static RenderTexture renderTexture;
    private static Material blurMaterialStatic;
    private static GameObject blurPanelStatic;
    [SerializeField] private Material blurMaterial;
    [SerializeField] private GameObject blurPanel;
    [SerializeField] private float blurValue = 0.01f;
    private static float blurValueStatic;
    
    void Awake()
    {
        blurCamera = Camera.main.transform.GetChild(0).GetComponent<Camera>();

        blurPanelStatic = blurPanel;
        blurValueStatic = blurValue;
        
        if (blurCamera.targetTexture != null)
        {
            blurCamera.targetTexture.Release();
        }

        renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurCamera.targetTexture = renderTexture;
        blurMaterialStatic = blurMaterial;
        blurMaterialStatic.SetTexture("_RenderTexture", renderTexture);
        blurMaterialStatic.SetFloat("_blur", 0);

        blurCamera.gameObject.SetActive(false);
    }

    public static void SetBlur(bool value)
    {
        blurCamera.gameObject.SetActive(value);
        
        if (value)
        {
            blurPanelStatic.SetActive(value);
            blurMaterialStatic.DOFloat(blurValueStatic, "_blur", 0.2f);
        }
        else
            blurMaterialStatic.DOFloat(0.0f, "_blur", 0.2f).onComplete = () => blurPanelStatic.SetActive(value);
    }
    
}
