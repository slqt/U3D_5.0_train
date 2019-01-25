using UnityEngine;
using System.Collections;
using UnityEditor;

public class C_13_3_1{
	[MenuItem ("AssetBundle/Build AssetBundles")]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("Assets/Chapter13/13.3/ABs",BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android);
	}
}
