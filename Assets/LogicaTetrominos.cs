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

        //MOVIMIENTO DE LA FICHA
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

        //CAIDA DE LA FICHA
        if(Time.time - tiempoAmterior > (Input.GetKey(KeyCode.DownArrow) ? tiempoCaida / 20: tiempoCaida))

        {
            transform.position += new Vector3(0, -1, 0);
            if (!Limites())
            {
                transform.position -= new Vector3(0, -1, 0);

                AñadirAlGrid();

                RevisarLineas();
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

    //LIMITES DE MOVIMIENTO
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

    //AÑADIR FICHAS CUANDO LEGUEN AL LIMITE Y
    void AñadirAlGrid()
    {
        foreach(Transform hijo in transform)
        {
            int enteroX = Mathf.RoundToInt(hijo.transform.position.x);
            int enteroY = Mathf.RoundToInt(hijo.transform.position.y);

            grid[enteroX, enteroY] = hijo;

            if(enteroY >= 19)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    //ELIMINAR LINEAS DE FICHAS COMPLETAS
    void RevisarLineas()
    {
        for (int i = alto -1; i >= 0; i--)
        {
            if (TieneLinea(i))
            {
                BorrarLinea(i);
                BajarLinea(i);
            }
        }
    }


    bool TieneLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            if(grid[j,i] == null)
            {
                return false;
            }
             
                
              
        }

        return true;
    }


   
    void BorrarLinea(int i)
    {
        for (int j = 0; j < ancho; j++)
        {
            
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }


    void BajarLinea(int i)
    {
        for (int y = i; y < alto; y++)
        {
            for (int j = 0; j < ancho; j++)
            {
                if(grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
}
