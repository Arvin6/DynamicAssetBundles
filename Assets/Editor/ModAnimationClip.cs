using UnityEditor;
using UnityEngine;

public class ModAnimationClip : AssetPostprocessor
{
    void OnPreprocessAnimation()
    {
        ModelImporter modelImporter = assetImporter as ModelImporter;
        modelImporter.animationCompression = ModelImporterAnimationCompression.Off;
        ModelImporterClipAnimation[] clipAnimations = modelImporter.defaultClipAnimations;

        for (int i = 0; i < clipAnimations.Length; i++)
        {
            clipAnimations[i].loopTime = true;
        }

        modelImporter.clipAnimations = clipAnimations;

        modelImporter.SaveAndReimport();
    }
}