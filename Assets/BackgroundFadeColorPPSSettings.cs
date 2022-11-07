// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( BackgroundFadeColorPPSRenderer ), PostProcessEvent.AfterStack, "BackgroundFadeColor", true )]
public sealed class BackgroundFadeColorPPSSettings : PostProcessEffectSettings
{
}

public sealed class BackgroundFadeColorPPSRenderer : PostProcessEffectRenderer<BackgroundFadeColorPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "BackgroundFadeColor" ) );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
