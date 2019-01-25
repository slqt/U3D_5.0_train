using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float rotationSpeed;
	public float moveSpeed;

	public ParticleSystem gunShotEffect;
	public GameObject gunShotLight;
	public AudioSource gunShotSound;

	private bool isShooting = false;
	private Animator animator;
	private Rigidbody rigidbody;

	void Start () {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();
	}
	void Update () {
		//锁定鼠标光标并隐藏
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		//处理移动
		Moving();
		//处理旋转
		Rotating ();

		//按下鼠标左键射击
		if (Input.GetMouseButtonDown (0)) {
			GunShotStart();
		}

		//按下ESC键退出游戏
		if (Input.GetKey (KeyCode.Escape)) {
			#if !UNITY_EDITOR
			Application.Quit();	
			#else
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
	}  
	void Moving()
	{ 
		float x = Input.GetAxis ("Horizontal");
		Vector3 mH = x * transform.right.normalized * moveSpeed * Time.deltaTime;
		rigidbody.MovePosition(rigidbody.position+mH);
		
		float y = Input.GetAxis ("Vertical");
		Vector3 mV = y * transform.forward.normalized * moveSpeed * Time.deltaTime;
		rigidbody.MovePosition(rigidbody.position+mV);
		
		bool moving = x != 0 || y != 0;
		animator.SetBool ("IsMoving", moving);
	}
	void Rotating()
	{
		float asixX = Input.GetAxis ("Mouse X");
		transform.eulerAngles += new Vector3 (0, rotationSpeed *Time.deltaTime*asixX, 0);
	}

	void GunShotStart()
	{
		if(isShooting)
			return;
		isShooting = true;
		gunShotLight.SetActive(true);
		gunShotEffect.Play();
		gunShotSound.Play();
		Invoke("GunShotEnd",0.1f);
	}
	void GunShotEnd()
	{
		isShooting = false;
		gunShotLight.SetActive (false);
	}
}




