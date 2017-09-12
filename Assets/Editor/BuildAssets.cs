using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEditor.Animations;
using UnityEditor;
using UnityEngine;

public class BuildAssets : MonoBehaviour
{
    static void BuildAssetBundle()
    {
        int len = 0;
        string output_path = "Assets/AssetBundles";
        string Folder = "/Resources";
        string log = "log.txt";
        string nameWithoutExtension ;
        string[] assetN;
        int N_Files;

        AssetBundleBuild[] AssetMap = new AssetBundleBuild[2];
        AssetMap[0].assetBundleName = "Default";

        // Adding to path /Resource
        string path = UnityEngine.Application.dataPath + Folder;
        //log
        File.AppendAllText(log, DateTime.Now.ToString() + "\n\n");
        File.AppendAllText(log, path + "\n");


        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] files = dir.GetFiles();

        foreach (FileInfo file in files)
        {
            if (file.Exists)
            {
                if (!file.Extension.Equals(".meta")) 
                {
                    if( file.Extension.Equals(".obj") || file.Extension.Equals(".fbx") || file.Extension.Equals(".FBX") 
                    || file.Extension.Equals(".OBJ") || file.Extension.Equals(".DAE") || file.Extension.Equals(".dae") 
                    || file.Extension.Equals(".3DS") || file.Extension.Equals(".3ds") || file.Extension.Equals(".dxf") 
                    || file.Extension.Equals(".DXF") )
                        {
                                nameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                                AnimationClip[] anim = Resources.LoadAll <AnimationClip> (nameWithoutExtension);
                                if((anim.Length>0) )
                                {
                                        //AnimationClip idle = new AnimationClip();
                                        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/Resources/"+nameWithoutExtension+"-Anim.controller");
                                        //AnimatorState idlestate = controller.AddMotion(idle as Motion);
                                        //idlestate.name = "Idle";
                                    for(int o=0;o<anim.Length;o++)
                                    {
                                        AnimationClip animation = anim[o];
                                         // Set Looping true
                                        animation.wrapMode = WrapMode.Loop;
                                        
                                        Motion m_anim = animation as Motion;
                                       // controller.AddParameter("Trigger-"+animation.name, AnimatorControllerParameterType.Trigger);
                                        
                                      // AnimatorState someState =
                                        controller.AddMotion(m_anim);
                                       // idlestate.AddTransition(someState).AddCondition(AnimatorConditionMode.If,0,"Trigger-"+animation.name);
                                    } 
                                }
                                AssetMap[0].assetBundleName = nameWithoutExtension;   
                                break;  // break after things are done with the fbx file
                        }
                }
            }
        }

        // Updating files list after conroller is created
        files = dir.GetFiles();

        // Number of files in "/Resources" folder
        N_Files = files.Length;

        assetN = new string[N_Files+10];

        //log
        System.IO.File.AppendAllText(log, "Num assets on path: "+ N_Files + " \n\n");


        foreach (System.IO.FileInfo file in files)
        {
            if (file.Exists)
            {
                if (!file.Extension.Equals(".meta")) 
                {
                    if(!file.Name.StartsWith(".") )
                    {
                        assetN[len] = "Assets"+ Folder +"/"+ file.Name;
                        System.IO.File.AppendAllText(log, assetN[len] + " \n");
                        len += 1;
                    }
                }
            }
        }
        
        File.AppendAllText(log, "Num assets on bundle: "+ len + " \n");

        AssetMap[0].assetNames = assetN;

        BuildPipeline.BuildAssetBundles(output_path, AssetMap, BuildAssetBundleOptions.None, BuildTarget.iOS);
        System.IO.File.AppendAllText(log, "\t----X----\n"); 
    }
}