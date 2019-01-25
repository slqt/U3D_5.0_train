using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

public class C_10_2_3{
	static string key = "12348578902223367877723456789012";
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
