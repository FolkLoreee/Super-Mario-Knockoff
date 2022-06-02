using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject mushroomPrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite disabledQuestionBox;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hit)
        {
            hit = true;
            rigidBody.AddForce(new Vector2(0, rigidBody.mass * 20), ForceMode2D.Impulse);
            Instantiate(mushroomPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);
            StartCoroutine(DisableHittable());
        }

    }
    bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }
        //continues here when the function returns true
        spriteRenderer.sprite = disabledQuestionBox;
        rigidBody.bodyType = RigidbodyType2D.Static;

        //resets box position
        this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false;
    }
}
