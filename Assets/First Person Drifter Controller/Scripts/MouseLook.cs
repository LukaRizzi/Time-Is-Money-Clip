// original by asteins
// adapted by @torahhorse
// http://wiki.unity3d.com/index.php/SmoothMouseLook

// Instructions:
// There should be one MouseLook script on the Player itself, and another on the camera
// player's MouseLook should use MouseX, camera's MouseLook should use MouseY

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour
{
 
	public enum RotationAxes { MouseX = 1, MouseY = 2 }
	public RotationAxes Axes = RotationAxes.MouseX;
	public bool InvertY = false;
	
	public float SensitivityX = 10F;
	public float SensitivityY = 9F;
 
	public float MinimumX = -360F;
	public float MaximumX = 360F;
 
	public float MinimumY = -85F;
	public float MaximumY = 85F;

	public float FramesOfSmoothing = 5;

	float _rotationX = 0F;
	float _rotationY = 0F;
 
	private List<float> _rotArrayX = new List<float>();
	float _rotAverageX = 0F;	
 
	private List<float> _rotArrayY = new List<float>();
	float _rotAverageY = 0F;
 
	Quaternion _originalRotation;
	
	void Start ()
	{			
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		
		_originalRotation = transform.localRotation;
	}
 
	void Update ()
	{
		if (Axes == RotationAxes.MouseX)
		{			
			_rotAverageX = 0f;
 
			_rotationX += Input.GetAxis("Mouse X") * SensitivityX * Time.timeScale;
 
			_rotArrayX.Add(_rotationX);
 
			if (_rotArrayX.Count >= FramesOfSmoothing)
			{
				_rotArrayX.RemoveAt(0);
			}
			for(int i = 0; i < _rotArrayX.Count; i++)
			{
				_rotAverageX += _rotArrayX[i];
			}
			_rotAverageX /= _rotArrayX.Count;
			_rotAverageX = ClampAngle(_rotAverageX, MinimumX, MaximumX);
 
			Quaternion xQuaternion = Quaternion.AngleAxis (_rotAverageX, Vector3.up);
			transform.localRotation = _originalRotation * xQuaternion;			
		}
		else
		{			
			_rotAverageY = 0f;
 
 			float invertFlag = 1f;
 			if( InvertY )
 			{
 				invertFlag = -1f;
 			}
			_rotationY += Input.GetAxis("Mouse Y") * SensitivityY * invertFlag * Time.timeScale;
			
			_rotationY = Mathf.Clamp(_rotationY, MinimumY, MaximumY);
 	
			_rotArrayY.Add(_rotationY);
 
			if (_rotArrayY.Count >= FramesOfSmoothing)
			{
				_rotArrayY.RemoveAt(0);
			}
			for(int j = 0; j < _rotArrayY.Count; j++)
			{
				_rotAverageY += _rotArrayY[j];
			}
			_rotAverageY /= _rotArrayY.Count;
 
			Quaternion yQuaternion = Quaternion.AngleAxis (_rotAverageY, Vector3.left);
			transform.localRotation = _originalRotation * yQuaternion;
		}
	}
	
	public void SetSensitivity(float s)
	{
		SensitivityX = s;
		SensitivityY = s;
	}
 
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
}