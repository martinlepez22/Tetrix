using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaTetrominos : MonoBehaviour
{

    private float tiempoAmterior;
    public float tiempoCaida = 0.8f;

    public static int alto = 20;
    public static int ancho = 10;

    public Vector3 puntoRotacion;

    private static Transform[,] grid = new Transform[ancho, alto];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(1, 0, 0);


            }
        }

        if(Time.time - tiempoAmterior > (Input.GetKey(KeyCode.DownArrow) ? tiempoCaida / 20: tiempoCaida))

        {
            transform.position += new Vector3(0, -1, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(0, -1, 0);

                AñadirAlGrid();

                this.enabled = false;
                FindObjectOfType<LogicaGenerador>().NuevoTetromino();
            }

            tiempoAmterior = Time.time;

        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3(0, 0, 1), -90);
            if (!Limites())
            {
                transform.RotateAround(transform.TransformPoint(puntoRotacion), new Vector3(0, 0, 1), 90);
            }
        }
    }


    bool Limites()
    {
        foreach (Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            if (enteroX < 0 || enteroX >= ancho || enteroY < 0 || enteroY >= alto)
            {
                return false;
            }

            if (grid[enteroX, enteroY] != null)
            {
                return false;
            }
        }

       
        return true;
    }


    void AñadirAlGrid()
    {
        foreach(Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            grid[enteroX, enteroY] = hijo;

        }
    }
}
