using UnityEngine;

public class WorldCamera : MonoBehaviour{
	public Camera cam;
	public MainControllerManager controller;

	private float acceleration = .02f;
	private float rotAcceleration = 1f;


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

		/*

		this.cachedVector = new Vector3(this.cam.transform.forward.x , 0, 0);//this.cam.transform.forward.z - this.cam.transform.forward.y);

		this.movement = this.move.x * this.cam.transform.right + this.move.y * this.cachedVector;
		this.movement = new Vector3(this.movement.x, 0, this.movement.z);

		this.cam.transform.position = new Vector3(this.cam.transform.position.x + this.movement.x, 3, this.cam.transform.position.z + this.movement.z);
		*/
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