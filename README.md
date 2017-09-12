# DynamicAssetBundles

This Project will create asset bundles from any supported 3d model format file that is placed in the /Assets/Resources folder. The asset bundle will contain textures, animation controller and the 3d model with default compression. The best part is, you can automate assetbundle generation through bash scripts and commandline interface of unity.

# What it does

- Maintain a seperate project for assetbundle generation

- Copy your Assets(which needs to be bundled) into the /Assets/Resources folder either with a bash script or manually.

- Run the command

- If your 3d Model contains animationclips, these clips will be added to the Mecanim animation controller on FCFS basis. The controller will be generated in your /Assets/Resources folder with the name "<modelname>-anim.controller"

- Your bundle will be generated in the /Assets/AssetBundles folder

# Note

- Currently, this works with one model with its textures and generates assetbundle in the name of the 3d model file but this can be customized to your needs.
- You should delete your assets from the /Assets/Resources folder after generating the asset bundle. Otherwise, this may cause problems with naming on your next run. Always check .manifest to ensure you have all your assets in the bundle.

# Terminal COMMAND (MAC)
$unitypath -quit -batchmode -projectPath<path/to/this/project> -buildTarget<.iOS/.Android/..> -executeMethod BuildAssets.BuildAssetBundle -logFile "

$unitypath: Path to your unity installation, this is usually /Applications/Unity/Unity.app/Contents/MacOS/Unity

buildTarget - Can be anything ranging from iOS, Android, Windows. But this should be the platform on which the assetbundle is going to be used on.

The command is the same for windows and linux as well.

For more info, check https://docs.unity3d.com/Manual/CommandLineArguments.html
