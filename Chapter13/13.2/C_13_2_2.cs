using UnityEngine;
using System.Collections;
using System.IO;
public class C_13_2_2 : MonoBehaviour {
	public string screenShotURL;

	void OnGUI () {
		if (GUILayout.Button ("Upload")) {
			StartCoroutine (UploadPNG ());
		}
	}
	
	IEnumerator UploadPNG() {
		//等待当前帧都渲染完成时
		yield return new WaitForEndOfFrame();

		string path = Application.dataPath + "/shot.png";
		//截屏并储存
		Application.CaptureScreenshot (path);
		//读取截屏图片
		byte[] bytes = File.ReadAllBytes (path);
		//使用form上传图片
		WWWForm form = new WWWForm();
		form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");
		WWW w = new WWW(screenShotURL, form);
		yield return w;
		if (!string.IsNullOrEmpty(w.error)) {
			print("有错误："+w.error);
		}
		else {
			print("完成上传");
		}
	}
}