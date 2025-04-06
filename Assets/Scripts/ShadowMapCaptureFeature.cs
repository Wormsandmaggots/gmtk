using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShadowMapCaptureFeature : ScriptableRendererFeature
{
    class ShadowMapPass : ScriptableRenderPass
    {
        RenderTargetIdentifier shadowMap = new("_CameraDepthTexture");

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var shadowMap = Shader.GetGlobalTexture("_ScreenSpaceShadowmapTexture");
            if (shadowMap != null)
            {
                Shader.SetGlobalTexture("_ShadowMap", shadowMap);
            }
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, FrameResources frameResources,
            ref RenderingData renderingData)
        {
            TextureHandle mainShadowsTexture = frameResources.GetTexture(UniversalResource.MainShadowsTexture); 

            if (mainShadowsTexture.IsValid())
                Shader.SetGlobalTexture("_Shadows", mainShadowsTexture);
        }
    }

    ShadowMapPass shadowMapPass;

    public override void Create()
    {
        shadowMapPass = new ShadowMapPass
        {
            renderPassEvent = RenderPassEvent.AfterRenderingShadows
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(shadowMapPass);
    }
}