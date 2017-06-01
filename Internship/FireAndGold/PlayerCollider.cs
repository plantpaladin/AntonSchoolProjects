using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour
{
    public float maxHealth = 300;//the max health of the player, used to determinate the % dmg taken

    public float fireDamage = 30;
    public float fireThrowback = 10;//the amount of force the player is thrown back with after hitting a fireball

    public Color damageColor = Color.red;

    public float currentSpeed = 40;

    public float stunDuration = 2;
    public float dashCooldown = 4;
    public float dashDuration = 1;
    public float dashThrowback = 10;
    public float dashSpeed = 1600;
    public float jumpSpeed = 10;//should be lower than other speedsas it doesnt depend on deltatime

    //for all keyboard input used, for others than the first player the names will be changed
    private string dashAxisName = "Dash";
    private string jumpAxisName = "Jump"; 
    private string xAxisName = "Horizontal";
    private string zAxisName = "Vertical";
    private string dashing = "DashingPlayer";
    
    private float defaultSpeed = 40;//used to reset the players speed after it is changed

    private float stunDurationLeft = 0;//countdown for ddurationsa and cooldowns
    private float dashCooldownLeft = 0;
    private float dashDurationLeft = 0;

    private float damageTaken = 0;//values for handling pickups and death
    private float score = 0;
    private float playerNumber = 0;
    private Vector3 startingPosition;
    //different parts used 
    private Color baseColor = Color.white;
    private Color currentColor;
    private Renderer playerRenderer;
    private Rigidbody rigidBody;
    private Behaviour dashEffect;
    private enum playerState
    {
        WALK,
        DASH,
        JUMP,
        STUNNED
    };
    playerState currentState;
    void Awake()
    {
        playerRenderer = GetComponent<Renderer>();
        rigidBody = GetComponent<Rigidbody>();//makes the parts more easily acessible

        dashEffect = (Behaviour)GetComponent("Halo");//this one also has to be cast to a behaviour
        dashEffect.enabled = false;//the halo is enabled when dashing
        defaultSpeed = currentSpeed;
        currentState = playerState.WALK;
        startingPosition = transform.position;
    }

    public void setVariables(int i,Color newColor)
    {//changes the axis this particular player uses
        xAxisName = xAxisName + i;
        zAxisName = zAxisName + i;
        dashAxisName = dashAxisName + i;
        jumpAxisName = jumpAxisName + i;
        playerNumber = i;
        //changes the color the player uses
        baseColor = newColor;
        currentColor = newColor;
        playerRenderer.material.color = newColor;
    }
    void FixedUpdate()
    {
        DashAndJump();//checks for more advanced moves based on state
        KeyboardMovement();
    }

    private void KeyboardMovement()
    {
        //adding force based on the movement 
        float xMove = Input.GetAxis(xAxisName) * currentSpeed * Time.deltaTime;
        float zMove = Input.GetAxis(zAxisName) * currentSpeed * Time.deltaTime;        
        Vector3 newMovement = new Vector3(xMove, 0, zMove);
        rigidBody.AddForce(newMovement);
        if (transform.position.y < -2)
        {
            dieAndRespawn();
        }
    }
    private void DashAndJump()
    {
        if(dashCooldownLeft>0)
        {//just a safeguard to prevent the numbers from bugging from going too low
            dashCooldownLeft -= Time.deltaTime;
        }//the cooldown is tracked from when the button is pressed
        switch(currentState)
       {//this is the different states the player can be in,states are not classes since all the states are 
        //only connected to WALK except JUMP which can turn to STUNNED
        //but moving to STUNNED are handled through the OnCollisionEnter function
            case(playerState.WALK):
           {

               if (Input.GetAxis(dashAxisName) == 1 && dashCooldownLeft <= 0)
               {//ensures the button is pressed and the ability is not on colldown
                   dashEffect.enabled = true;
                   currentSpeed = dashSpeed;
                   dashCooldownLeft = dashCooldown;
                   dashDurationLeft = dashDuration;
                   currentState = playerState.DASH;
                   tag = dashing;//used for collision
               }
               else if (Input.GetAxis(jumpAxisName) == 1)
               {//checks if the button is pressed, there is no cooldown
                   rigidbody.AddForce(0, jumpSpeed, 0);
                   enterJump();
               }
               //leaving space for jump function
               break;
           }
            case (playerState.JUMP):
           {//the player cant dash or jump while jumping
            //it checks if the player is back on the ground 
               if (Physics.Raycast(transform.position, Vector3.down, 1))
               {
                   enterWalk();
               }
               break;
           }
            case (playerState.DASH):
           {

               dashDurationLeft -= Time.deltaTime;
               if (dashDurationLeft <= 0)
               {
                   tag = "Player";
                   dashEffect.enabled = false;
                   enterWalk();
               }
               break;
           }
            case (playerState.STUNNED):
           {//players cant walk and is just waiting for the stun to end
               stunDurationLeft -= Time.deltaTime;
               if (stunDurationLeft <= 0)
               {
                   enterWalk();
               }
               break;
           }

       
        }
    }
   

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Gold"))
        {//the gold is picked up
            score += 2;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Fire"))
        {//the player takes damage and the fire 
            damageTaken += fireDamage;
           
            Destroy(other.gameObject);
            if (currentState != playerState.DASH)
            {//if the player days the rest of the function should not be run
                if (ExamineDamageTaken() == false)
                {
                currentState = playerState.STUNNED;
                stunDurationLeft = stunDuration;
                currentSpeed = 0;
                
                Vector3 fireDirection = transform.position - other.transform.position;
                fireDirection.Normalize();//should always be thrown back with the same force
                rigidbody.AddForce(fireDirection * fireThrowback);
                }

            }
        }
        else if (other.gameObject.CompareTag(dashing))
        {
            if (currentState != playerState.DASH)
            { //the player cant be stunned while it is dashing
                stunDurationLeft = stunDuration;
                currentSpeed = 0;
                currentState = playerState.STUNNED;
                
                Vector3 dashDirection = transform.position - other.transform.position;
                dashDirection.Normalize();//should always be thrown back with the same force
                rigidbody.AddForce(dashDirection * dashThrowback);
            }
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
    
    private bool ExamineDamageTaken()
    {
        float percentageDamageTaken = damageTaken / maxHealth;
        if (damageTaken < maxHealth)
        {
            currentColor = Color.Lerp(baseColor, damageColor, percentageDamageTaken);
            playerRenderer.material.color = currentColor;
            return false;
        }
        else
        {
            dieAndRespawn();
            return true;
        }

    }

    private void dieAndRespawn()
    {//is called by the functions that determinate death
    //lowers score and respawn player
        score -= 5;
        Vector3 respawnPosition = startingPosition;
        respawnPosition.y += 3;     //the respawn position is above the starting position
        transform.position = respawnPosition;
   
        damageTaken = 0;
        tag = "Player";
        enterJump();
        stunDurationLeft = 0;//countdown for different cooldowns
        dashCooldownLeft = 0;//is reset
        dashDurationLeft = 0;
        dashEffect.enabled = false;
        rigidbody.velocity = Vector3.zero;
        ExamineDamageTaken();//resets the color
    }
    private void enterWalk()
    {//the changes done after the player goes back to walking
        currentSpeed = defaultSpeed;
        currentState = playerState.WALK;
    }
    public void enterJump()
    {//this is the state the players begin in
        currentSpeed = defaultSpeed/2;
        currentState = playerState.JUMP;
    }
    public float getScore()
    {
        return score;
    }
    public float getPercentageCooldownRemaining()
    {
        return dashCooldownLeft/dashCooldown;
    }
}