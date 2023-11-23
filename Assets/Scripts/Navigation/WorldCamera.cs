using UnityEngine;

public class WorldCamera : MonoBehaviour{
	public Camera cam;
	public MainControllerManager controller;

	private float acceleration = .03f;
	private float rotAcceleration = 1f;
	private float scrollAcceleration = 0.1f;


	private float maxY = 8f;
	private float minY = 1.5f;

	private Vector2 move = new Vector2(0,0);
	private Vector3 movement = new Vector3(0,0,0);

	private bool IS_MOVING_X = false;
	private bool IS_MOVING_Y = false;
	private Vector3 cachedVector;

	void Update(){
		if(controller.left)
			MoveLeft();
		if(controller.right)
			MoveRight();
		if(controller.up)
			MoveUp();
		if(controller.down)
			MoveDown();
		if(controller.rotateLeft){
			RotateLeft();
		}
		if(controller.rotateRight){
			RotateRight();
		}
		if(controller.mouseScroll != 0){
			Scroll();
		}

		Move();

		IS_MOVING_X = false;
		IS_MOVING_Y = false;
	}

	private void Move(){
		if(!IS_MOVING_X && !IS_MOVING_Y)
			return;

		if(!IS_MOVING_X)
			this.move.x = 0f;
		if(!IS_MOVING_Y)
			this.move.y = 0f;

        this.cachedVector = new Vector3(move.x, 0f, move.y).normalized;
        this.cachedVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * this.cachedVector;
        this.cam.transform.Translate(this.cachedVector * acceleration, Space.World);
	}

	private void MoveLeft(){
		this.move.x = -this.acceleration;

		IS_MOVING_X = true;
	}

	private void MoveRight(){
		this.move.x = this.acceleration;

		IS_MOVING_X = true;

	}

	private void MoveUp(){
		this.move.y = this.acceleration;

		IS_MOVING_Y = true;

	}

	private void MoveDown(){
		this.move.y = -this.acceleration;

		IS_MOVING_Y = true;

	}

	private void Scroll(){
		if(this.cam.transform.localPosition.y - this.scrollAcceleration <= this.minY && controller.mouseScroll < 0)
			return;
		if(this.cam.transform.localPosition.y + this.scrollAcceleration >= this.maxY && controller.mouseScroll > 0)
			return;

		this.cam.transform.localPosition = this.cam.transform.localPosition + (Vector3.up * controller.mouseScroll * this.scrollAcceleration);
	}

	private void RotateRight(){
        this.cam.transform.localRotation = Quaternion.Euler(this.cam.transform.localEulerAngles.x, this.cam.transform.localEulerAngles.y + this.rotAcceleration, this.cam.transform.localEulerAngles.z);
	}

	private void RotateLeft(){
        this.cam.transform.localRotation = Quaternion.Euler(this.cam.transform.localEulerAngles.x, this.cam.transform.localEulerAngles.y - this.rotAcceleration, this.cam.transform.localEulerAngles.z);

	}

	private void LockCursor(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	private void UnlockCursor(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private float Limit1(float f){
		if(f > 1)
			return f;
		return 1;
	}
}