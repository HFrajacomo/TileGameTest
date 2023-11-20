using UnityEngine;

public class WorldCamera : MonoBehaviour{
	public Camera cam;
	public MainControllerManager controller;
	private float acceleration = .04f;
	private float maxSpeed = .3f;
	private float frames_divider = 1.4f;

	private int mouseSensitivityX = 120;
	private int mouseSensitivityZ = 120;
	private int scrollSensitivity = 10;

	//private int maxHeight = 5;
	//private int minHeight = 2;

	private Vector2 move = new Vector2(0,0);
	private float xRotation = 0f;
	private float zRotation = 0f;
	//private float yMove = 0f;

	private Vector3 movement = new Vector3(0,0,0);

	private bool IS_MOVING = false;

	void Update(){
		if(controller.left)
			MoveLeft();
		if(controller.right)
			MoveRight();
		if(controller.up)
			MoveUp();
		if(controller.down)
			MoveDown();
		if(controller.secondary){
			LockCursor();
			Rotate();
		}
		else{
			UnlockCursor();
		}

		Move();

		IS_MOVING = false;
	}

	private void Move(){
		if(this.move.magnitude == 0 && controller.scroll == 0)
			return;
		if(!IS_MOVING)
			this.move /= this.frames_divider;
		if(this.move.magnitude < 0.0002)
			this.move = new Vector2(0,0);

		this.movement = this.cam.transform.right * this.move.x + this.cam.transform.forward * this.move.y + Vector3.up * (controller.scroll / this.scrollSensitivity);
		this.movement = new Vector3(this.movement.x*Limit1(this.movement.y), this.movement.y, this.movement.z*Limit1(this.movement.y));

		this.cam.transform.position = new Vector3(this.cam.transform.position.x + this.movement.x, 3, this.cam.transform.position.z + this.movement.z);
	}

	private void MoveLeft(){
		this.move.x -= this.acceleration * Time.deltaTime;
		
		if(this.move.x < -this.maxSpeed)
			this.move.x = -this.maxSpeed;

		IS_MOVING = true;
	}

	private void MoveRight(){
		this.move.x += this.acceleration * Time.deltaTime;
		
		if(this.move.x > this.maxSpeed)
			this.move.x = this.maxSpeed;

		IS_MOVING = true;

	}

	private void MoveUp(){
		this.move.y += this.acceleration * Time.deltaTime;
		
		if(this.move.y > this.maxSpeed)
			this.move.y = this.maxSpeed;

		IS_MOVING = true;

	}

	private void MoveDown(){
		this.move.y -= this.acceleration * Time.deltaTime;
		
		if(this.move.y < -this.maxSpeed)
			this.move.y = -this.maxSpeed;

		IS_MOVING = true;

	}

	private void Rotate(){
        float mouseX = controller.mouseX * this.mouseSensitivityX * Time.deltaTime;
        float mouseY = controller.mouseY * this.mouseSensitivityZ * Time.deltaTime;

        this.xRotation -= mouseY;
        this.xRotation = Mathf.Clamp(this.xRotation, 30f, 90f);
        this.zRotation += mouseX;
        this.zRotation = Mathf.Clamp(zRotation, -90f, 90f);

        this.cam.transform.localRotation = Quaternion.Euler(this.xRotation, this.zRotation, 0);
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