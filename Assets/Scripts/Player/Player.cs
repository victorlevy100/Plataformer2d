using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;

    [Header("Movement")]
    public Vector2 friction = new Vector2(.1f,0);
    public float speed;
    public float speedRun;
    public float forceJump = 2f;

    [Header("Animation setup")]
    public float scaleJumpY = 1.5f;
    public float scaleJumpX = 1.7f;
    public float scaleImpactY = 0.5f;
    public float scaleImpactX = 1.7f;
    public float animationDuration = 0.5f;
    public Ease ease = Ease.OutBack;

    private float _currentSpeed;
    private bool _hasTouchedGround = false;

    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        HandleJump();
    }
    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
        }
        else
        {
            _currentSpeed = speed;

        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody2D.velocity = new Vector2(-_currentSpeed, myRigidbody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody2D.velocity = new Vector2(_currentSpeed, myRigidbody2D.velocity.y);
        }

        if(myRigidbody2D.velocity.x < 0)
        {
            myRigidbody2D.velocity += friction;
        }
        else if (myRigidbody2D.velocity.x > 0)
        {
            myRigidbody2D.velocity -= friction;
        }
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * forceJump;
            myRigidbody2D.transform.localScale = Vector2.one;
            DOTween.Kill(myRigidbody2D.transform);
            HandleScaleJump();
            _hasTouchedGround = false;
        }
    }
    void HandleScaleJump()
    {
        myRigidbody2D.transform.DOScaleY(scaleJumpY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody2D.transform.DOScaleX(scaleJumpX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !_hasTouchedGround)
        {
            _hasTouchedGround = true;
            // Mata qualquer animação anterior no transform
            DOTween.Kill(myRigidbody2D.transform);

            // Cria uma sequência de animações
            Sequence sequence = DOTween.Sequence();

            // Adiciona a animação de escala Y à sequência
            sequence.Append(myRigidbody2D.transform.DOScaleY(scaleImpactY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease));

            // Adiciona a animação de escala X à sequência
            sequence.Join(myRigidbody2D.transform.DOScaleX(scaleImpactX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease));

            // Adiciona uma ação ao final da sequência para redefinir a escala
            sequence.AppendCallback(() => myRigidbody2D.transform.localScale = Vector2.one);
        }
    }
}
    

