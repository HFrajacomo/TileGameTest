using UnityEngine;
using UnityEngine.InputSystem;

public class MainControllerManager : MonoBehaviour
{
    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;
    public bool primary = false;
    public bool secondary = false;
    public bool rotateLeft = false;
    public bool rotateRight = false;
    public float mouseX = 0f;
    public float mouseY = 0f;
    public float mouseScroll = 0f;

    public void OnMoveLeft(){
        if(!this.left)
            this.left = true;
        else
            this.left = false;
    }
    public void OnMoveRight(){
        if(!this.right)
            this.right = true;
        else
            this.right = false;
    }
    public void OnMoveUp(){
        if(!this.up)
            this.up = true;
        else
            this.up = false;
    }
    public void OnMoveDown(){
        if(!this.down)
            this.down = true;
        else
            this.down = false;
    }
    public void OnPrimaryClick(){
        if(!this.primary)
            this.primary = true;
        else
            this.primary = false;
    }
    public void OnSecondaryClick(){
        if(!this.secondary)
            this.secondary = true;
        else
            this.secondary = false;
    }
    public void OnRotateLeft(){
        if(!this.rotateLeft)
            this.rotateLeft = true;
        else
            this.rotateLeft = false;
    }
    public void OnRotateRight(){
        if(!this.rotateRight)
            this.rotateRight = true;
        else
            this.rotateRight = false;
    }
    public void OnMouseLook(InputValue val){
        this.mouseX = val.Get<Vector2>().x;
        this.mouseY = val.Get<Vector2>().y;
    }
    public void OnMouseScroll(InputValue val){
        this.mouseScroll = val.Get<float>()/120;
        Debug.Log(this.mouseScroll);
    }
}