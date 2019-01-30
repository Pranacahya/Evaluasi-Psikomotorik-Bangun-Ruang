using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColor : MonoBehaviour {

    [SerializeField]
    private GameObject _mesh;

    [SerializeField]
    private Color _drawColor = Color.black;

    private Renderer m_Renderer;

    public void CRed()
    {
        _drawColor = Color.red;
        return;
    }
    public void CBlue()
    {
        _drawColor = Color.blue;
        return;
    }
    public void CGreen()
    {
        _drawColor = Color.green;
        return;
    }
    public void CBlack()
    {
        _drawColor = Color.black;
        return;
    }
    public void CYellow()
    {
        _drawColor = Color.yellow;
        return;
    }

    public Color DrawColor
    {
        get
        {
            return _drawColor;
        }
        set
        {
            _drawColor = value;
        }
    }

    // Use this for initialization
    void Start () {
        m_Renderer = GetComponent<Renderer>();
       
    }
	
	// Update is called once per frame
	void Update () {
        m_Renderer.material.color = _drawColor;
    }
}
