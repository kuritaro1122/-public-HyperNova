using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Enemy;

namespace HyperNova.Player {
    [AddComponentMenu("HyperNova/Player/Player_Movement")]
    public class PlayerMovement : MonoBehaviour {
        [Header("--- GameObject ---")]
        [SerializeField] Camera mainCam;
        [SerializeField] Rigidbody rb;
        [Header("--- Status ---")]
        [SerializeField] float speed = 20f;
        //[Header("--- Option ----")]
        //[SerializeField] SnakeMovement options;
        //[SerializeField] SnakeMovement1 options;

        [Header("--- HumanLimit ---")]
        [SerializeField, Range(0, 1)] float right = 1f;
        [SerializeField, Range(0, 1)] float left = 0f;
        [SerializeField, Range(0, 1)] float up = 1f;
        [SerializeField, Range(0, 1)] float down = 0f;
        

        private void Awake() {
            //rb = gameObject.GetComponent<Rigidbody>();
            this.mainCam = Camera.main;
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if(PlayerStatus.AllowInput) Movement();
        }

        void OnValidate() {
            
        }

        void OnDrawGizmos() {

            Gizmos.color = Color.green;
            float distance = mainCam.WorldToViewportPoint(this.transform.position).z;

            Vector3[] edge = new Vector3[] {
                mainCam.ViewportToWorldPoint(new Vector3(left, up, distance)),
                mainCam.ViewportToWorldPoint(new Vector3(right, up, distance)),
                mainCam.ViewportToWorldPoint(new Vector3(right, down, distance)),
                mainCam.ViewportToWorldPoint(new Vector3(left, down, distance))
            };
            for (int i = 0; i < edge.Length; i++) {
                if (i >= edge.Length - 1)
                    Gizmos.DrawLine(edge[i], edge[0]);
                else
                    Gizmos.DrawLine(edge[i], edge[i + 1]);
            }

            for (int i = 0; i < 2; i++)
                Gizmos.DrawLine(edge[i], edge[i + 2]);
        }

        private void Movement() {
            Vector2 stick;
            stick.x = Input.GetAxisRaw("Horizontal");
            stick.y = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector3(stick.x * speed, stick.y * speed, 0f);

            /*Vector3 rot = this.transform.eulerAngles;
            rot.x = stick.y * 20f;
            this.transform.eulerAngles = rot;*/

            //options.AddPosition(speed * (Mathf.Abs(stick.x) + Mathf.Abs(stick.y)) * Time.deltaTime);

            Vector3 pos = this.transform.position;
            //pos += speed * (Vector3)stick * Time.deltaTime;

            if (mainCam != null) {
                Vector3 viewportPos = mainCam.WorldToViewportPoint(pos);
                viewportPos.x = Mathf.Clamp(viewportPos.x, left, right);
                viewportPos.y = Mathf.Clamp(viewportPos.y, down, up);
                pos = mainCam.ViewportToWorldPoint(viewportPos);
                pos.z = 0f;
            }
            this.transform.position = pos;

            //OptionMovement(stick);
            
        }

        /*private void OptionMovement(Vector2 stick) {
            if (stick.sqrMagnitude > 0.0f) {
                options?.SetCashActive(true);
            } else options?.SetCashActive(false);
        }*/
    }
}