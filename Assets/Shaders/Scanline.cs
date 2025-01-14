using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


[Serializable, PostProcess(typeof(ScanlineRenderer), PostProcessEvent.AfterStack, "Scanline", allowInSceneView:false)]
public class Scanline : PostProcessEffectSettings
{
    [Range(144, 1000)] public IntParameter Height = new IntParameter() { value = 720 };
    [ColorUsage(true, false)] public ColorParameter Color = new ColorParameter() { value = new Color(0,0,0,0.75f) };
    public FloatParameter Speed = new FloatParameter() { value = 0.005f };

}

public sealed class ScanlineRenderer : PostProcessEffectRenderer<Scanline>
{
    private const string SHADER = "Hidden/Scanline";
    public override void Render(PostProcessRenderContext context)
    {
        PropertySheet sheet = context.propertySheets.Get(Shader.Find(SHADER));
        sheet.properties.SetInt("_Height", settings.Height);
        sheet.properties.SetColor("_Color", settings.Color);
        sheet.properties.SetFloat("_Speed", settings.Speed);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
