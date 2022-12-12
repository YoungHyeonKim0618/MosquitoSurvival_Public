using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public static class ObjectType
{
    
    public const int EXP1 = 101;
    public const int EXP5 = 102;
    public const int EXP25 = 103;

    public const int EXP_OBTAINED = 105;

    public const int MINI_SLIME = 1000;
    public const int SLIME = 1001;
    public const int BLUE_SLIME = 1002;
    //public const int IRON_SLIME = 1003;

}
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;


    [SerializeField] private GameObject exp1Prefab, exp5Prefab, exp25Prefab, expObtainedPrefab;
    
    [Space(10f)] [SerializeField] private Slime miniSlimePrefab, slimePrefab, blueSlimePrefab;

    
    private List<GameObject> _exp1s = new List<GameObject>();
    private List<GameObject> _exp5s = new List<GameObject>();
    private List<GameObject> _exp25s = new List<GameObject>();
    private List<GameObject> _expObtained = new List<GameObject>();

    private List<Slime> _miniSlimes = new List<Slime>();
    private List<Slime> _slimes = new List<Slime>();
    private List<Slime> _blueSlimes = new List<Slime>();

    private int numOfExps = 0;

    private void Awake()
    {
        if (!instance)
            instance = this;
        Create(ObjectType.EXP1);
        Create(ObjectType.EXP1);
        Create(ObjectType.EXP1);
        Create(ObjectType.EXP1);
        Create(ObjectType.EXP1);
        Create(ObjectType.EXP5);
        Create(ObjectType.EXP5);
        Create(ObjectType.EXP5);
        Create(ObjectType.EXP5);
        Create(ObjectType.EXP_OBTAINED);
        //Create(ObjectType.EXP25);
        
        Create(ObjectType.MINI_SLIME);
        Create(ObjectType.MINI_SLIME);
        Create(ObjectType.MINI_SLIME);
        Create(ObjectType.MINI_SLIME);
        Create(ObjectType.SLIME);
        Create(ObjectType.SLIME);
        Create(ObjectType.SLIME);
        Create(ObjectType.SLIME);
        Create(ObjectType.SLIME);
        Create(ObjectType.BLUE_SLIME);
        Create(ObjectType.BLUE_SLIME);
        Create(ObjectType.BLUE_SLIME);
        Create(ObjectType.BLUE_SLIME);
        Create(ObjectType.BLUE_SLIME);
    }

    public GameObject Pop(int type)
    {
        GameObject go = null;
        switch (type)
        {
                
            case ObjectType.EXP1:
                if(_exp1s.Count == 0) Create(type);
                go = _exp1s[0];
                _exp1s.Remove(go);
                numOfExps++;
                break;
            case ObjectType.EXP5:
                if(_exp5s.Count == 0) Create(type);
                go = _exp5s[0];
                _exp5s.Remove(go);
                numOfExps++;
                break;
            case ObjectType.EXP25:
                if(_exp25s.Count == 0) Create(type);
                go = _exp25s[0];
                _exp25s.Remove(go);
                break;
            case ObjectType.EXP_OBTAINED:
                if (_expObtained.Count == 0) Create(type);
                go = _expObtained[0];
                _expObtained.Remove(go);
                break;
        }

        go.SetActive(true);
        return go;
    }
    

    public void Push(GameObject go, int type)
    {
        go.SetActive(false);

        switch (type)
        {
            case ObjectType.EXP1:
                _exp1s.Add(go);
                numOfExps--;
                break;
            case ObjectType.EXP5:
                _exp5s.Add(go);
                numOfExps--;
                break;
            case ObjectType.EXP25:
                _exp25s.Add(go);
                break;
            case ObjectType.EXP_OBTAINED:
                _expObtained.Add(go);
                break;
            
            
        }
    }

    public void Create(int type)
    {
        switch (type)
        {
                
            case ObjectType.EXP1:
                for (int i = 0; i < 20; i++)
                {
                    GameObject go = Instantiate(exp1Prefab, null);
                    go.SetActive(false);
                    _exp1s.Add(go);
                }
                break;
            case ObjectType.EXP5:
                for (int i = 0; i < 20; i++)
                {
                    GameObject go = Instantiate(exp5Prefab, null);
                    go.SetActive(false);
                    _exp5s.Add(go);
                }
                break;
            case ObjectType.EXP25:
                for (int i = 0; i < 20; i++)
                {
                    GameObject go = Instantiate(exp25Prefab, null);
                    go.SetActive(false);
                    _exp25s.Add(go);
                }
                break;
            case ObjectType.EXP_OBTAINED:
                for (int i = 0; i < 20; i++)
                {
                    GameObject go = Instantiate(expObtainedPrefab, null);
                    go.SetActive(false);
                    _expObtained.Add(go);
                }

                break;
        }
        
        
        
        
    }

    public Slime PopSlime(int type)
    {
        Slime s = null;
        switch (type)
        {
            case ObjectType.SLIME:
                if(_slimes.Count == 0) CreateSlime(type);
                s = _slimes[0];
                _slimes.Remove(s);
                break;
            case ObjectType.BLUE_SLIME:
                if(_blueSlimes.Count == 0) CreateSlime(type);
                s = _blueSlimes[0];
                _blueSlimes.Remove(s);
                break;
            case ObjectType.MINI_SLIME:
                if(_miniSlimes.Count == 0) CreateSlime(type);
                s = _miniSlimes[0];
                _miniSlimes.Remove(s);
                break;
        }
        s.gameObject.SetActive(true);

        return s;
    }

    public void PushSlime(Slime s, int type)
    {
        s.gameObject.SetActive(false);
        switch (type)
        {
            
            case ObjectType.SLIME:
                _slimes.Add(s);
                break;
            case ObjectType.BLUE_SLIME:
                _blueSlimes.Add(s);
                break;
            case ObjectType.MINI_SLIME:
                _miniSlimes.Add(s);
                break;
        }
    }

    public void CreateSlime(int type)
    {
        switch (type)
        {
            case ObjectType.SLIME:
                for (int i = 0; i < 20; i++)
                {
                    Slime s = Instantiate(slimePrefab, null);
                    s.gameObject.SetActive(false);
                    _slimes.Add(s);
                }
                break;
            case ObjectType.BLUE_SLIME:
                for (int i = 0; i < 20; i++)
                {
                    Slime s = Instantiate(blueSlimePrefab, null);
                    s.gameObject.SetActive(false);
                    _blueSlimes.Add(s);
                }
                break;
            case ObjectType.MINI_SLIME:
                for (int i = 0; i < 20; i++)
                {
                    Slime s = Instantiate(miniSlimePrefab, null);
                    s.gameObject.SetActive(false);
                    _miniSlimes.Add(s);
                }
                break;
        }
    }
    
    [SerializeField] private ParticleSystem _particlePrefab;
    private List<ParticleSystem> _particles = new List<ParticleSystem>();
    public ParticleSystem PopParticle(Vector2 pos)
    {
        if(_particles.Count == 0) CreateParticle();

        ParticleSystem ps = _particles[0];
        _particles.Remove(ps);
        ps.transform.position = pos;
        ps.gameObject.SetActive(true);
        return ps;
    }

    public void PushParticle(ParticleSystem particle)
    {
        _particles.Add(particle);
        particle.gameObject.SetActive(false);
    }

    public void CreateParticle()
    {
        for (int i = 0; i < 20; i++)
        {
            ParticleSystem ps = Instantiate(_particlePrefab, null);
            ps.gameObject.SetActive(false);
            _particles.Add(ps);
        }
    }

    [SerializeField]
    private AutoPrj autoPrjPrefab, autoPrjEvolvedPrefab;

    private List<AutoPrj> _autoPrjs = new List<AutoPrj>();
    List<AutoPrj> _autoPrjsEvolved = new List<AutoPrj>();

    public AutoPrj PopAutoPrj(bool evolved = false)
    {
        if (evolved)
        {
            if(_autoPrjsEvolved.Count == 0)
                CreateAutoPrj(true);

            AutoPrj prj = _autoPrjsEvolved[0];
            _autoPrjsEvolved.Remove(prj);
            prj.gameObject.SetActive(true);
            return prj;
        }
        else
        {
            if (_autoPrjs.Count == 0)
                CreateAutoPrj();

            AutoPrj prj = _autoPrjs[0];
            _autoPrjs.Remove(prj);
            prj.gameObject.SetActive(true);
            return prj;
        }

        return null;
    }

    public void PushAutoPrj(AutoPrj prj, bool evolved = false)
    {
        if (evolved)
        {
            prj.gameObject.SetActive(false);
            _autoPrjsEvolved.Add(prj);
        }
        else
        {
            prj.gameObject.SetActive(false);
            _autoPrjs.Add(prj);
        }
    }

    void CreateAutoPrj(bool evolved = false)
    {
        if (evolved)
        {
            for(int i = 0; i < 20; i++)
            {
                AutoPrj prj = Instantiate(autoPrjEvolvedPrefab, null);
                prj.gameObject.SetActive(false);
                _autoPrjsEvolved.Add(prj);
            }
        }
        else
        {
            for(int i = 0; i < 20; i++)
            {
                AutoPrj prj = Instantiate(autoPrjPrefab, null);
                prj.gameObject.SetActive(false);
                _autoPrjs.Add(prj);
            }
        }
    }
    [SerializeField]
    private ParticleSystem innerQuakeParticlePrefab;

    private List<ParticleSystem> innerQuakeParticles = new List<ParticleSystem>();

    public ParticleSystem PopInnerParticle()
    {
        if(innerQuakeParticles.Count == 0)
            CreateInnerParticle(true);

        ParticleSystem prj = innerQuakeParticles[0];
        innerQuakeParticles.Remove(prj);
        prj.gameObject.SetActive(true);
        return prj;

    }

    public void PushInnerParticle(ParticleSystem prj)
    {
        prj.gameObject.SetActive(false);
        innerQuakeParticles.Add(prj);
    }


    void CreateInnerParticle(bool evolved = false)
    {
        for(int i = 0; i < 20; i++)
        {
            ParticleSystem prj = Instantiate(innerQuakeParticlePrefab, null);
            prj.gameObject.SetActive(false);
            innerQuakeParticles.Add(prj);
        }
    }
}
