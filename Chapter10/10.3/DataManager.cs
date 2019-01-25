using UnityEngine;
using System.Collections;
using System;
using JsonFx.Json;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// 数据管理器
/// </summary>
public class DataManager
{
	#region Config
	//文档相对路径
	static string fileName = "Chapter8/8.3/data.dat";
	//Rijndael加密算法的256位秘钥
	static string key = "12348578902223367877723456789012";
	#endregion
	
	private static CharData CharData_instance;
	public static CharData CharData_Instance {
		get {
			if (CharData_instance == null) {
				if (ExistFile () == false)
					CharData_instance = new CharData ();
				else
					Load ();
			}
			return CharData_instance;
		}
		set {
			CharData_instance = value;
			Save ();
		}
	}
	
	/// <summary>
	/// 是否存在该文件
	/// </summary>
	public static bool ExistFile ()
	{
		return File.Exists (GetDataPath () + "/" + fileName);
	}
	/// <summary>
	/// 删除该文件
	/// </summary>
	public static void DeleteDataFile ()
	{
		if (ExistFile ())
			File.Delete (GetDataPath () + "/" + fileName);
	}
	/// <summary>
	/// 保存该文件
	/// </summary>
	public static void Save ()
	{
		string text = JsonFx.Json.JsonWriter.Serialize (CharData_Instance);
	//	text = Encrypt(text);
		File.WriteAllText (GetDataPath () + "/" + fileName, text);
	}
	/// <summary>
	/// 读取该文件
	/// </summary>
	public static void Load ()
	{
		if (File.Exists (GetDataPath () + "/" + fileName) == false)
			return;
		
		string orginText = File.ReadAllText (GetDataPath () + "/" + fileName);
	//	orginText = Decrypt(orginText);
		
		CharData_Instance = JsonFx.Json.JsonReader.Deserialize<CharData> (orginText);
	}
	/// <summary>
	/// 得到设备路径
	/// </summary>
	private static string GetDataPath ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			//iphone路径
			string path = Application.dataPath.Substring (0, Application.dataPath.Length - 5); 
			path = path.Substring (0, path.LastIndexOf ('/'));  
			return path + "/Documents";
		} else if (Application.platform == RuntimePlatform.Android) {
			//安卓路径
			return Application.persistentDataPath + "/";
		} else
		{
			//其他路径
			return Application.dataPath;
		}
	}
	/// <summary>
	/// 字符串加密
	/// </summary>
	private static string Encrypt (string toE)
	{
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes (key);
		
		RijndaelManaged rDel = new RijndaelManaged ();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateEncryptor ();
		
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes (toE);
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		
		return Convert.ToBase64String (resultArray, 0, resultArray.Length);
	}
	/// <summary>
	/// 字符串解密
	/// </summary>
	private static string Decrypt (string toD)
	{
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes (key);
		
		RijndaelManaged rDel = new RijndaelManaged ();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateDecryptor ();
		
		byte[] toEncryptArray = Convert.FromBase64String (toD);
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		
		return UTF8Encoding.UTF8.GetString (resultArray);
	}
}
